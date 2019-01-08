using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for MathPage.xaml
    /// </summary>
    public partial class MathPage : Page, IFilterPage
    {
        public MathPage () : base()
        {
            InitializeComponent();
        }

        public void Clear ()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return null;
        }

        public MathPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;
        public ImageMan ParentWindow = null;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            ImageSurface.Source = (WriteableBitmap)Original;
        }

        private int GetOperation ()
        {
            ComboBoxItem CBI = OperationCombo.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return -1;
            string raw = CBI.Name as string;
            if (string.IsNullOrEmpty(raw))
                return -1;
            raw = raw.ToLower();
            if (!MathOps.ContainsKey(raw))
                return -1;
            return MathOps[raw];
        }

        private string NormalizeConstantVal (string Raw)
        {
            Raw = Raw.Trim();
            if (string.IsNullOrEmpty(Raw))
                return "0";

            if (Raw.Length >= 1)
            {
                if (Raw[0] == '#')
                {
                    Raw = Raw.Substring(1);
                    if (string.IsNullOrEmpty(Raw))
                        return "0";
                    return Convert.ToInt32(Raw, 16).ToString();
                }
            }
            if (Raw.Length >= 2)
            {
                if (Raw.Substring(0, 2).ToLower() == "0x")
                {
                    Raw = Raw.Substring(2);
                    if (string.IsNullOrEmpty(Raw))
                        return "0";
                    return Convert.ToInt32(Raw, 16).ToString();
                }
            }

            return Raw;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            string ConstantVal = ConstantValueInput.Text;
            if (string.IsNullOrEmpty(ConstantVal))
                return;
            ConstantVal = NormalizeConstantVal(ConstantVal);
            int Operand = 0;
            if (!int.TryParse(ConstantVal, out Operand))
                return;
            List<OptionalValue> Options = new List<OptionalValue>();
            int Operation = GetOperation();
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);
            bool DoAlpha = DoAlphaCheck.IsChecked.Value;
            bool DoRed = DoRedCheck.IsChecked.Value;
            bool DoGreen = DoGreenCheck.IsChecked.Value;
            bool DoBlue = DoBlueCheck.IsChecked.Value;
            bool OK = false;
            OK = CBI.ImageByteOperationByChannel(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                Operation, Operand, DoAlpha, DoRed, DoGreen, DoBlue);
            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }

        private Dictionary<string, int> MathOps = new Dictionary<string, int>()
        {
            {"add", 0 },
            {"sub", 1 },
            {"mul", 2 },
            {"div", 3 },
            {"mod", 4 },
            {"and", 5 },
            {"or", 6 },
            {"xor", 7 },
            {"shl", 23 },
            {"shr", 22 },
            {"rol", 24 },
            {"ror", 25 },
            {"maxrg", 26 },
            {"maxrb", 27 },
            {"maxgb", 28 },
            {"minrg", 29 },
            {"minrb", 30 },
            {"mingb", 31 },
            {"biggest", 32 },
            {"smallest", 33 },
        };
    }
}
