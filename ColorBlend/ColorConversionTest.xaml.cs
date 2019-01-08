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
using System.Windows.Shapes;
using Iro3.Controls.ColorInput;
using Iro3.Data.ColorSpaces;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ColorConvertionTest.xaml
    /// </summary>
    public partial class ColorConversionTest : Window
    {
        public ColorConversionTest ()
        {
            InitializeComponent();
            CurrentColorSpace = new RGB(0xffffffff);
            SetColorSpaceColor();
            ColorSpacesCombo.SelectedIndex = 0;
        }

        private void SetColorSpaceColor()
        {
            SampleColor.Background = new SolidColorBrush(CurrentColorSpace.ToRGBColor());
            ColorInput.Text = CurrentColorSpace.ToInputString();
            RGBOut.SetColorSpaceAndColor(CurrentColorSpace);
        }

        private IColorSpace CurrentColorSpace { get;  set; }

        private void HandleUIClose (object Sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExecuteConversion (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            string Raw = ColorInput.Text;
            if (string.IsNullOrEmpty(Raw))
                return;
            ComboBoxItem CBI = ColorSpacesCombo.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return;
            string NewSpace = CBI.Content as string;
            if (string.IsNullOrEmpty(NewSpace))
                return;
            NewSpace = NewSpace.ToLower();
            if (!ColorSpaceMap.ContainsKey(NewSpace))
                return;
            ColorSpaces NewColorSpace = ColorSpaces.RGB;
            if (!ColorSpaceMap.TryGetValue(NewSpace, out NewColorSpace))
                return;
            IColorSpace NewCSpace = IroColorSpace.ConvertColorSpace(NewColorSpace, CurrentColorSpace);
            CurrentColorSpace = NewCSpace;
            SetColorSpaceColor();
        }

        private Dictionary<string, ColorSpaces> ColorSpaceMap = new Dictionary<string, ColorSpaces>()
        {
            {"rgb", ColorSpaces.RGB },
            {"hsl", ColorSpaces.HSL },
            {"hsv", ColorSpaces.HSV },
            {"yuv", ColorSpaces.YUV },
            {"yiq", ColorSpaces.YIQ },
            {"ycbcr", ColorSpaces.YCbCr },
            {"tsl", ColorSpaces.TSL },
            {"cmy", ColorSpaces.CMY },
            {"cmyk", ColorSpaces.CMYK },
            {"xyz", ColorSpaces.XYZ },
            {"cielab", ColorSpaces.CIELab },
        };

        private void UpdateSameColor (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            string Raw = ColorInput.Text;
            if (string.IsNullOrEmpty(Raw))
                return;
            CurrentColorSpace.ParseSet(Raw);
            SetColorSpaceColor();
        }
    }
}
