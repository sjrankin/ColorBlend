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
    /// Interaction logic for HSLConditionalSheet.xaml
    /// </summary>
    public partial class HSLConditionalSheet : UserControl
    {
        public HSLConditionalSheet()
        {
            InitializeComponent();
            Width = 250;
            Height = 90;
        }

        public bool Validate()
        {
            bool OK = true;
            if (LocalConditions.RangeLow > LocalConditions.RangeHigh)
            {
                OK = false;
                LowRangeOut.Background = Brushes.Red;
                HighRangeOut.Background = Brushes.Red;
            }
            else
            {
                LowRangeOut.Background = Brushes.Transparent;
                HighRangeOut.Background = Brushes.Transparent;
            }
            return OK;
        }

        public HSLConditionalSheet(ColorBlenderInterface.ConditionalHSLAdjustment Conditions)
        {
            InitializeComponent();
            Width = 260;
            Height = 90;
            SetValues(Conditions);
        }

        private string MakeNiceConditionalString(string Channel, int Operation, double Operand)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Channel);
            sb.Append(" = ");
            string OperandString = Operand.ToString("N3");
            switch (Operation)
            {
                case 0:
                    sb.Append(Channel);
                    sb.Append(" * ");
                    sb.Append(OperandString);
                    break;

                case 1:
                    sb.Append(Channel);
                    sb.Append(" + ");
                    sb.Append(OperandString);
                    break;

                case 2:
                    sb.Append(Channel);
                    sb.Append(" / ");
                    sb.Append(OperandString);
                    break;

                case 3:
                    sb.Append(Channel);
                    sb.Append(" - ");
                    sb.Append(OperandString);
                    break;

                case 4:
                    sb.Append(OperandString);
                    break;
            }
            return sb.ToString();
        }

        public ColorBlenderInterface.ConditionalHSLAdjustment LocalConditions { get; set; }

        public void SetValues(ColorBlenderInterface.ConditionalHSLAdjustment Conditions)
        {
            LocalConditions = Conditions;
            LowRangeOut.Text = Conditions.RangeLow.ToString();
            HighRangeOut.Text = Conditions.RangeHigh.ToString();
            if (Conditions.ModifyHue)
            {
                HueCondition.Text = MakeNiceConditionalString("Hue", Conditions.HueOperation, Conditions.HueOperand);
            }
            else
            {
                HueCondition.Text = "No change";
            }
            if (Conditions.ModifySaturation)
            {
                SaturationCondition.Text = MakeNiceConditionalString("Saturation", Conditions.SaturationOperation, Conditions.SaturationOperand);
            }
            else
            {
                SaturationCondition.Text = "No change";
            }
            if (Conditions.ModifyLuminance)
            {
                LuminanceCondition.Text = MakeNiceConditionalString("Luminance", Conditions.LuminanceOperation, Conditions.LuminanceOperand);
            }
            else
            {
                LuminanceCondition.Text = "No change";
            }
        }
    }
}
