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

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ThresholdEditor.xaml
    /// </summary>
    public partial class ThresholdEditor : Window
    {
        public ThresholdEditor ()
        {
            InitializeComponent();
            ThresholdCanceled = true;
            LowThreshold = 0.0;
            HighThreshold = 1.0;
        }

        public bool ThresholdCanceled { get; private set; }
        public bool ValidThreshold
        {
            get
            {
                return !ThresholdCanceled;
            }
        }

        private Color _ThresholdColor = Colors.Black;
        public Color ThresholdColor
        {
            get
            {
                return _ThresholdColor;
            }
            set
            {
                _ThresholdColor = value;
                ThresholdColorEditor.CurrentColor = value;
            }
        }

        private double _LowThreshold = 0.0;
        public double LowThreshold
        {
            get
            {
                return _LowThreshold;
            }
            set
            {
                _LowThreshold = value;
                LowThresholdBox.Text = value.ToString();
            }
        }

        private double _HighThreshold = 1.0;
        public double HighThreshold
        {
            get
            {
                return _HighThreshold;
            }
            set
            {
                _HighThreshold = value;
                HighThresholdBox.Text = value.ToString();
            }
        }

        private void OKClicked (object Sender, RoutedEventArgs e)
        {
            ThresholdColor = ThresholdColorEditor.CurrentColor;
            string sLowVal = LowThresholdBox.Text;
            if (string.IsNullOrEmpty(sLowVal))
            {
                sLowVal = "0.0";
            }
            double TVal = 0.0;
            if (!double.TryParse(sLowVal, out TVal))
            {
                TVal = 0.0;
            }
            if (TVal < 0.0)
                TVal = 0.0;
            if (TVal > 1.0)
                TVal = 1.0;
            LowThreshold = TVal;
            string sHighVal = HighThresholdBox.Text;
            if (string.IsNullOrEmpty(sHighVal))
            {
                sHighVal = "1.0";
            }
            if (!double.TryParse(sHighVal, out TVal))
            {
                TVal = 1.0;
            }
            if (TVal < 0.0)
                TVal = 0.0;
            if (TVal > 1.0)
                TVal = 1.0;
            HighThreshold = TVal;
            ThresholdCanceled = false;
            this.Close();
        }

        private void CancelClicked (object Sender, RoutedEventArgs e)
        {
            ThresholdCanceled = true;
            this.Close();
        }
    }
}
