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
    /// Interaction logic for ConstantMathPage.xaml
    /// </summary>
    public partial class ConstantMathPage : Page, IFilterPage
    {
        public ConstantMathPage () : base()
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

        public ConstantMathPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        public ImageMan ParentWindow = null;
        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            ImageSurface.Source = (WriteableBitmap)Original;
        }

        private string GetOperation()
        {
            if (AndButton.IsChecked.Value)
                return "And";
            if (OrButton.IsChecked.Value)
                return "Or";
            if (XorButton.IsChecked.Value)
                return "Xor";
            if (AddButton.IsChecked.Value)
                return "Add";
            if (SubtractButton.IsChecked.Value)
                return "Subtract";
            if (MultiplyButton.IsChecked.Value)
                return "Multiply";
            if (DivideButton.IsChecked.Value)
                return "Divide";
            if (ModuloButton.IsChecked.Value)
                return "Modulo";
            return "";
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            List<OptionalValue> Options = new List<OptionalValue>();
            bool IsNormal = NormalConstant.IsChecked.Value;
            string ConstVal = ConstantInput.Text;
            double ConstantValue = 0.0;
            if (!double.TryParse(ConstVal, out ConstantValue))
                return;
            string Operation = GetOperation();
            Options.Add(new OptionalValue("IsNormal", IsNormal.ToString(), typeof(bool)));
            Options.Add(new OptionalValue("Constant", ConstantValue.ToString(),typeof(string)));
            Options.Add(new OptionalValue("Operation", Operation, typeof(string)));
            //ParentWindow.CommandSink("ConstantMath", Options);
        }
    }
}
