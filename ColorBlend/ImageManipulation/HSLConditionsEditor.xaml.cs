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
    /// Interaction logic for HSLConditionsEditor.xaml
    /// </summary>
    public partial class HSLConditionsEditor : Window
    {
        public HSLConditionsEditor()
        {
            InitializeComponent();
            PopulateDialog(null);
        }

        public HSLConditionsEditor(ColorBlenderInterface.ConditionalHSLAdjustment Conditional)
        {
            InitializeComponent();
            PopulateDialog(Conditional);
        }

        private void PopulateDialog(Nullable<ColorBlenderInterface.ConditionalHSLAdjustment> Conditional = null)
        {
            if (Conditional == null)
            {
                TitleBlock.Text = "Add New Condition";
                LowHueRangeEntry.Text = "0.0";
                HighHueRangeEntry.Text = "360.0";
                HSLHueConditional.IsChecked = false;
                HSLSaturationConditional.IsChecked = false;
                HSLLuminanceConditional.IsChecked = false;
                HueOperandBox.Text = "0.0";
                SatOperandBox.Text = "0.0";
                LumOperandBox.Text = "0.0";
                HSLHueOperandSelection.SelectedIndex = 0;
                HSLSatOperandSelection.SelectedIndex = 0;
                HSLLumOperandSelection.SelectedIndex = 0;
            }
            else
            {
                TitleBlock.Text = "Edit Condition";
                LowHueRangeEntry.Text = Conditional.Value.RangeLow.ToString();
                HighHueRangeEntry.Text = Conditional.Value.RangeHigh.ToString();
                HueOperandBox.Text = Conditional.Value.HueOperand.ToString();
                SatOperandBox.Text = Conditional.Value.SaturationOperand.ToString();
                LumOperandBox.Text = Conditional.Value.LuminanceOperand.ToString();
                HSLHueConditional.IsChecked = Conditional.Value.ModifyHue;
                HSLSaturationConditional.IsChecked = Conditional.Value.ModifySaturation;
                HSLLuminanceConditional.IsChecked = Conditional.Value.ModifyLuminance;
                HSLHueOperandSelection.SelectedIndex = Conditional.Value.HueOperation;
                HSLSatOperandSelection.SelectedIndex = Conditional.Value.SaturationOperation;
                HSLLumOperandSelection.SelectedIndex = Conditional.Value.LuminanceOperation;
            }
        }

        public bool OKClicked { get; private set; }

        public ColorBlenderInterface.ConditionalHSLAdjustment NewCondition { get; private set; }

        private int GetComboTagInt(ComboBoxItem CBI)
        {
            if (CBI == null)
                return 0;
            string TagString = CBI.Tag as string;
            if (string.IsNullOrEmpty(TagString))
                return 0;
            if (!int.TryParse(TagString, out int Result))
                return 0;
            return Result;
        }

        private ColorBlenderInterface.ConditionalHSLAdjustment GetNewCondition()
        {
            ColorBlenderInterface.ConditionalHSLAdjustment NewCondition = new ColorBlenderInterface.ConditionalHSLAdjustment
            {
                RangeLow = LowHueRangeEntry.DoubleValue(),
                RangeHigh = HighHueRangeEntry.DoubleValue(),
                ModifyHue = HSLHueConditional.IsChecked.Value,
                ModifySaturation = HSLSaturationConditional.IsChecked.Value,
                ModifyLuminance = HSLLuminanceConditional.IsChecked.Value,
                HueOperand = HueOperandBox.DoubleValue(),
                SaturationOperand = SatOperandBox.DoubleValue(),
                LuminanceOperand = LumOperandBox.DoubleValue(),
                HueOperation = GetComboTagInt(HSLHueOperandSelection.SelectedItem as ComboBoxItem),
                SaturationOperation = GetComboTagInt(HSLSatOperandSelection.SelectedItem as ComboBoxItem),
                LuminanceOperation = GetComboTagInt(HSLLumOperandSelection.SelectedItem as ComboBoxItem)
            };
            return NewCondition;
        }

        private void SetTextBoxHighlight(TextBox TB, bool DoHighlight)
        {
            if (TB == null)
                return;
            if (DoHighlight)
            {
                TB.Background = Brushes.Pink;
            }
            else
            {
                TB.Background = SystemColors.WindowBrush;
            }
        }

        private bool ValidTextBoxValue(TextBox TB, double Minimum, double Maximum, out Nullable<double> TBValue)
        {
            TBValue = null;
            if (TB == null)
                return false;
            string TBString = TB.Text;
            if (string.IsNullOrEmpty(TBString))
                return false;
            if (!double.TryParse(TBString, out double Value))
                return false;
            if (Value < Minimum)
                return false;
            if (Value > Maximum)
                return false;
            TBValue = Value;
            return true;
        }

        private bool ValidTextBoxValue(TextBox TB, out Nullable<double> TBValue)
        {
            TBValue = null;
            if (TB == null)
                return false;
            string TBString = TB.Text;
            if (string.IsNullOrEmpty(TBString))
                return false;
            if (!double.TryParse(TBString, out double Value))
                return false;
            TBValue = Value;
            return true;
        }

        private bool DoValidate()
        {
            bool OK = true;
            SetTextBoxHighlight(LowHueRangeEntry, false);
            bool LowIsOK = ValidTextBoxValue(LowHueRangeEntry, 0.0, 360.0, out Nullable<double> LowValue);
            if (!LowIsOK)
            {
                SetTextBoxHighlight(LowHueRangeEntry, true);
                OK = false;
            }
            SetTextBoxHighlight(HighHueRangeEntry, false);
            bool HighIsOK = ValidTextBoxValue(HighHueRangeEntry, 0.0, 360.0, out Nullable<double> HighValue);
            if (!HighIsOK)
            {
                SetTextBoxHighlight(HighHueRangeEntry, true);
                OK = false;
            }
            if (LowIsOK && HighIsOK)
                if (LowValue > HighValue)
                {
                    SetTextBoxHighlight(LowHueRangeEntry, true);
                    SetTextBoxHighlight(HighHueRangeEntry, true);
                    OK = false;
                }
            SetTextBoxHighlight(HueOperandBox, false);
            if (HSLHueConditional.IsChecked.Value)
            {

                bool HueOpOK = ValidTextBoxValue(HueOperandBox, 0.0, 360.0, out Nullable<double> HueOpValue);
                if (!HueOpOK)
                {
                    SetTextBoxHighlight(HueOperandBox, true);
                    OK = false;
                }
            }
            SetTextBoxHighlight(SatOperandBox, false);
            if (HSLSaturationConditional.IsChecked.Value)
            {
                bool SatOpOK = ValidTextBoxValue(SatOperandBox, out Nullable<double> SatOpValue);
                if (!SatOpOK)
                {
                    SetTextBoxHighlight(SatOperandBox, true);
                    OK = false;
                }
            }
            SetTextBoxHighlight(LumOperandBox, false);
            if (HSLLuminanceConditional.IsChecked.Value)
            {
                bool LumOpOK = ValidTextBoxValue(LumOperandBox, out Nullable<double> LumOpValue);
                if (!LumOpOK)
                {
                    SetTextBoxHighlight(LumOperandBox, true);
                    OK = false;
                }
            }
            return OK;
        }

        private void HandleValidateClick(object Sender, RoutedEventArgs e)
        {
            DoValidate();
        }

        private void HandleOKButtonClick(object Sender, RoutedEventArgs e)
        {
            bool IgnoreErrors = IgnoreErrorsCheck.IsChecked.Value;
            bool NoErrors = DoValidate();
            if (!NoErrors)
                if (!IgnoreErrors)
                    return;
            OKClicked = true;
            NewCondition = GetNewCondition();
            this.Close();
        }

        private void HandleCancelButtonClick(object Sender, RoutedEventArgs e)
        {
            OKClicked = false;
            this.Close();
        }
    }
}
