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

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ColorConverter.xaml
    /// </summary>
    public partial class ColorConverter : Window
    {
        public ColorConverter ()
        {
            InitializeComponent();
        }

        public void ShowDialog2 ()
        {
            OutputText.Text = "";
            SourceCombo.SelectedIndex = 0;
            DestinationCombo.SelectedIndex = 1;
            base.ShowDialog();
        }

        private void CloseButtonClick (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string GetSourceFormat ()
        {
            if (SourceCombo == null)
                return null;
            ComboBoxItem CBI = SourceCombo.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return null;
            return CBI.Name;
        }

        private string GetDestinationFormat ()
        {
            if (DestinationCombo == null)
                return null;
            ComboBoxItem CBI = DestinationCombo.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return null;
            return CBI.Name;
        }

        private void ConvertClickHandler (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            string C1 = "";
            string C2 = "";
            string C3 = "";
            string C4 = "";
            switch (GetSourceFormat())
            {
                case "sRGB":
                    switch (GetDestinationFormat())
                    {
                        case "dRGB":
                            return;

                        case "dHSL":
                            GetRawChannelData(ref C1, ref C2, ref C3, ref C4);
                            Tuple<double, double, double> Normalized = NormalizeRGB(C1, C2, C3);
                            Tuple<double, double, double> Result = FromRGBToHSL(Normalized.Item1, Normalized.Item2, Normalized.Item3);
                            AddHSLResult(Result);
                            return;

                        case "dYUV":
                            break;

                        default:
                            break;
                    }
                    break;

                case "sHSL":
                    switch (GetDestinationFormat())
                    {
                        case "dRGB":
                            GetRawChannelData(ref C1, ref C2, ref C3, ref C4);
                            Tuple<double, double, double> NormalizedH = NormalizeHSL(C1, C2, C3);
                            Tuple<double, double, double> HOutResult = FromHSLToRGB(NormalizedH.Item1, NormalizedH.Item2, NormalizedH.Item3);
                            AddRGBResult(HOutResult);
                            break;

                        default:
                            break;
                    }
                    break;

                case "sYUV":
                    switch (GetDestinationFormat())
                    {
                        default:
                            break;
                    }
                    break;
            }
        }

        private void AddHSLResult (Tuple<double, double, double> HSL)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HSL.Item1.ToString("n2"));
            sb.Append(", ");
            sb.Append(HSL.Item2.ToString("n2"));
            sb.Append(", ");
            sb.Append(HSL.Item3.ToString("n2"));
            OutputText.Text = sb.ToString();
        }

        private void AddRGBResult (Tuple<double, double, double> RGB)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(((byte)(RGB.Item1 * 255.0)).ToString());
            sb.Append(", ");
            sb.Append(((byte)(RGB.Item2 * 255.0)).ToString());
            sb.Append(", ");
            sb.Append(((byte)(RGB.Item3 * 255.0)).ToString());
            OutputText.Text = sb.ToString();
        }

        private Tuple<double, double, double> NormalizeHSL (string Hs, string Ss, string Ls)
        {
            bool ParsedOK = false;
            double H = 0;
            double S = 0;
            double L = 0;

            ParsedOK = double.TryParse(Hs, out H);
            if (!ParsedOK)
                return null;
            ParsedOK = double.TryParse(Ss, out S);
            if (!ParsedOK)
                return null;
            ParsedOK = double.TryParse(Ls, out L);
            if (!ParsedOK)
                return null;

            return new Tuple<double, double, double>(H, S, L);
        }

        private Tuple<double, double, double> NormalizeRGB (string Rs, string Gs, string Bs)
        {
            bool ParsedOK = false;
            byte R, G, B;
            ParsedOK = byte.TryParse(Rs, out R);
            if (!ParsedOK)
                return null;
            ParsedOK = byte.TryParse(Gs, out G);
            if (!ParsedOK)
                return null;
            ParsedOK = byte.TryParse(Bs, out B);
            if (!ParsedOK)
                return null;
            double Rn = (double)R / 255.0;
            double Gn = (double)G / 255.0;
            double Bn = (double)B / 255.0;
            return new Tuple<double, double, double>(Rn, Gn, Bn);
        }

        private bool GetRawChannelData (ref string Chl1, ref string Chl2, ref string Chl3, ref string Chl4)
        {
            string C1 = Channel1Input.Text;
            if (string.IsNullOrEmpty(C1))
                return false;
            Chl1 = C1;
            string C2 = Channel2Input.Text;
            if (string.IsNullOrEmpty(C2))
                return false;
            Chl2 = C2;
            string C3 = Channel3Input.Text;
            if (string.IsNullOrEmpty(C3))
                return false;
            Chl3 = C3;
            if (Channel4Input.Visibility == System.Windows.Visibility.Hidden)
                Chl4 = "";
            else
            {
                string C4 = Channel4Input.Text;
                if (string.IsNullOrEmpty(C4))
                    return false;
            }
            return true;
        }

        private void ConvertFromRGB ()
        {
            string Rs = Channel1Input.Text;
            if (string.IsNullOrEmpty(Rs))
                return;
            string Gs = Channel2Input.Text;
            if (string.IsNullOrEmpty(Gs))
                return;
            string Bs = Channel3Input.Text;
            if (string.IsNullOrEmpty(Bs))
                return;
            bool ParsedOK = false;
            byte R, G, B;
            ParsedOK = byte.TryParse(Rs, out R);
            if (!ParsedOK)
                return;
            ParsedOK = byte.TryParse(Gs, out G);
            if (!ParsedOK)
                return;
            ParsedOK = byte.TryParse(Bs, out B);
            if (!ParsedOK)
                return;
            double Rn = (double)R / 255.0;
            double Gn = (double)G / 255.0;
            double Bn = (double)B / 255.0;
        }

        private void ConvertFromYUV ()
        {
        }

        private void ConvertFromHSL ()
        {
        }

        private void SourceFormatChanged (object Sender, SelectionChangedEventArgs e)
        {
            if (!this.IsInitialized)
                return;
            ComboBox CB = Sender as ComboBox;
            if (CB == null)
                return;
            ComboBoxItem CBI = CB.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return;
            if (CurrentSourceFormat == CBI.Name)
                return;
            CurrentSourceFormat = CBI.Name;
            InitializeSourceInput();
        }

        private string CurrentSourceFormat = "";

        private void InitializeSourceInput ()
        {
            if (string.IsNullOrEmpty(CurrentSourceFormat))
                return;
            switch (CurrentSourceFormat)
            {
                case "sRGB":
                    MakeRGBInput();
                    break;

                case "sHSL":
                    MakeHSLInput();
                    break;

                case "sYUV":
                    MakeYUVInput();
                    break;

                default:
                    ClearInput();
                    break;
            }
        }

        private void ClearInput ()
        {
            ResetOutput();
            Channel1.Visibility = System.Windows.Visibility.Hidden;
            Channel2.Visibility = System.Windows.Visibility.Hidden;
            Channel3.Visibility = System.Windows.Visibility.Hidden;
            Channel4.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ResetOutput ()
        {
            if (OutputText != null)
                OutputText.Text = "";
        }

        private void MakeRGBInput ()
        {
            ResetOutput();
            Channel1Title.Text = "Red";
            Channel2Title.Text = "Green";
            Channel3Title.Text = "Blue";
            Channel1.Visibility = System.Windows.Visibility.Visible;
            Channel2.Visibility = System.Windows.Visibility.Visible;
            Channel3.Visibility = System.Windows.Visibility.Visible;
            Channel4.Visibility = System.Windows.Visibility.Hidden;
        }

        private void MakeHSLInput ()
        {
            ResetOutput();
            Channel1Title.Text = "H";
            Channel2Title.Text = "S";
            Channel3Title.Text = "L";
            Channel1.Visibility = System.Windows.Visibility.Visible;
            Channel2.Visibility = System.Windows.Visibility.Visible;
            Channel3.Visibility = System.Windows.Visibility.Visible;
            Channel4.Visibility = System.Windows.Visibility.Hidden;
        }

        private void MakeYUVInput ()
        {
            ResetOutput();
            Channel1Title.Text = "Y";
            Channel2Title.Text = "U";
            Channel3Title.Text = "V";
            Channel1.Visibility = System.Windows.Visibility.Visible;
            Channel2.Visibility = System.Windows.Visibility.Visible;
            Channel3.Visibility = System.Windows.Visibility.Visible;
            Channel4.Visibility = System.Windows.Visibility.Hidden;
        }

        private void DestinationFormatChanged (object Sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = Sender as ComboBox;
            if (CB == null)
                return;
            ComboBoxItem CBI = CB.SelectedItem as ComboBoxItem;
            if (CBI == null)
                return;
            CurrentDestinationFormat = CBI.Name;
            ResetOutput();
        }

        private string CurrentDestinationFormat = "";

        #region Conversion routines.
        private Tuple<double, double, double> FromHSLToRGB (double H, double S, double L)
        {
            double R = 0;
            double G = 0;
            double B = 0;
            double C = (1 - Math.Abs(2 * (L - 1))) * S;
            double X = C * (1 - Math.Abs((int)(H / 60.0) % 2 - 1));
            double m = L - (C / 2.0);
            int sect = (int)H % 60;
            switch (sect)
            {
                case 0:
                    R = C + m;
                    G = X + m;
                    B = m;
                    break;

                case 1:
                    R = X + m;
                    G = C + m;
                    B = m;
                    break;

                case 2:
                    R = m;
                    G = C + m;
                    B = X + m;
                    break;

                case 3:
                    R = m;
                    G = X + m;
                    B = C + m;
                    break;

                case 4:
                    R = X + m;
                    G = m;
                    B = C + m;
                    break;

                default:
                    R = C + m;
                    G = m;
                    B = X + m;
                    break;
            }

            return new Tuple<double, double, double>(R, G, B);
        }

        private Tuple<double, double, double> FromRGBToHSL (double R, double G, double B)
        {
            double H = 0;
            double S = 0;
            double L = 0;
            double Max = Math.Max(R, Math.Max(G, B));
            double Min = Math.Min(R, Math.Min(G, B));
            double Delta = Max - Min;
            if (Delta == 0)
            {
                H = 0;
            }
            else
            {
                if (R == Max)
                {
                    if (G >= B)
                        H = ((G - B) / Delta);
                }
                if (G == Max)
                {
                    H = ((B - R) / Delta) + 2;
                }
                if (B == Max)
                {
                    H = ((R - G) / Delta) + 4;
                }
            }
            H *= 60.0;
            L = (Max + Min) / 2;
            if (Delta == 0.0)
            {
                S = 0.0;
            }
            else
            {
                S = Delta / (1 - Math.Abs(2 * L - 1));
            }

            return new Tuple<double, double, double>(H, S, L);
        }

        private Tuple<double, double, double> FromRGBToYUV (double R, double G, double B)
        {
            double Y = (R * 0.299) + (G * 0.587) + (B * 0.114);
            double U = (R * -0.14713) + (G * -0.28886) + (B * 0.436);
            double V = (R * 0.615) + (G * -0.51499) + (B * -0.10001);
            return new Tuple<double, double, double>(Y, U, V);
        }

        private Tuple<double, double, double> FromYUVToRGB (double Y, double U, double V)
        {
            double R = (1.0 * Y) + (0.0 * U) + (1.13983 * V);
            double G = (1.0 * Y) + (-0.39465 * U) + (-0.58060 * V);
            double B = (1.0 * Y) + (2.03211 * U) + (0.0 * V);
            return new Tuple<double, double, double>(R, G, B);
        }

        private Tuple<double, double, double> FromYCbCrToRGB (double Y, double Cb, double Cr)
        {
            double R = Y + (1.371 * (Cr - 128));
            double G = Y - (0.698 * (Cr - 128)) - (0.336 * (Cb - 128));
            double B = Y + (1.732 * (Cr - 128));
            return new Tuple<double, double, double>(R, G, B);
        }

        private Tuple<double, double, double> FromRGBtoYCbCr (double R, double G, double B)
        {
            double Y = (double)(R * (77 / 255)) + (G * (150 / 255)) + (B * (29 / 255));
            double Cb = (double)(R * (44 / 255)) - (G * (87 / 255)) + (B * (131 / 255)) + 128;
            double Cr = (double)(R * (131 / 255)) - (G * (110 / 255)) - (B * (21 / 255)) + 128;
            return new Tuple<double, double, double>(Y, Cb, Cr);
        }

        private Tuple<double, double, double> FromRGBToYIQ (double R, double G, double B)
        {
            double Y = (0.299 * R) + (0.587 * G) + (0.114 * B);
            double I = (0.596 * R) - (0.275 * G) - (0.321 * B);
            double Q = (0.212 * R) - (0.523 * G) + (0.311 * B);
            return new Tuple<double, double, double>(Y, I, Q);
        }

        private Tuple<double, double, double> FromYIQToRGB (double Y, double I, double Q)
        {
            double R = (1.0 * Y) + (0.9563 * I) + (0.6210 * Q);
            double G = (1.0 * Y) - (0.2721 * I) - (0.6474 * Q);
            double B = (1.0 * Y) - (1.1070 * I) + (1.7046 * Q);
            return new Tuple<double, double, double>(R, G, B);
        }

        private Tuple<double, double, double> FromRGBToXYZ (double R, double G, double B)
        {
            double X = (R * 0.412456) + (G * 0.357580) + (B * 0.180423);
            double Y = (R * 0.212671) + (G * 0.715160) + (B * 0.072169);
            double Z = (R * 0.019334) + (G * 0.119193) + (B * 0.950227);
            return new Tuple<double, double, double>(X, Y, Z);
        }

        private Tuple<double, double, double> FromXYZToRGB (double X, double Y, double Z)
        {
            double R = (X * 3.240479) - (1.537150 * Y) - (0.498535 * Z);
            double G = (X * -0.969255) + (1.875992 * Y) + (0.041556 * Z);
            double B = (X * 0.055648) - (0.204043 * Y) + (1.057311 * Z);
            return new Tuple<double, double, double>(R, G, B);
        }

        private Tuple<double, double, double> FromRGBToCMY (double R, double G, double B)
        {
            double C = 1.0 - R;
            double M = 1.0 - G;
            double Y = 1.0 - B;
            return new Tuple<double, double, double>(C, M, Y);
        }

        private Tuple<double, double, double> FromCMYToRGB (double C, double M, double Y)
        {
            double R = 1.0 - C;
            double G = 1.0 - M;
            double B = 1.0 - Y;
            return new Tuple<double, double, double>(R, G, B);
        }

        private Tuple<double, double, double, double> FromCMYToCMYK (double iC, double iM, double iY)
        {
            double C = 0;
            double M = 0;
            double Y = 0;
            double K = 0;
            double tK = 1.0;
            if (iC < tK)
                tK = iC;
            if (iM < tK)
                tK = iM;
            if (iY < tK)
                tK = iY;
            if (tK == 0.0)
                return new Tuple<double, double, double, double>(0.0, 0.0, 0.0, 0.0);
            C = (iC - tK) / (1.0 - tK);
            M = (iM - tK) / (1.0 - tK);
            Y = (iY - tK) / (1.0 - tK);
            K = tK;
            return new Tuple<double, double, double, double>(C, M, Y, K);
        }

        private Tuple<double, double, double> FromCMYKToCMY (double iC, double iM, double iY, double iK)
        {
            double C = (iC * (1.0 - iK)) + iK;
            double M = (iM * (1.0 - iK)) + iK;
            double Y = (iY * (1.0 - iK)) + iK;
            return new Tuple<double, double, double>(C, M, Y);
        }

        const double RefX = 95.047;
        const double RefY = 100.00;
        const double RefZ = 100.00;

        private Tuple<double, double, double> XYZtoCIELab (double X, double Y, double Z)
        {
            double Xp = X / RefX;
            double Yp = Y / RefY;
            double Zp = Z / RefZ;

            if (Xp < 0.008856)
                Xp = Math.Pow(Xp, (double)(1 / 3));
            else
                Xp = (7.787 * Xp) + (16 / 116);
            if (Yp < 0.008856)
                Yp = Math.Pow(Yp, (double)(1 / 3));
            else
                Yp = (7.787 * Yp) + (16 / 116);
            if (Zp < 0.008856)
                Zp = Math.Pow(Zp, (double)(1 / 3));
            else
                Zp = (7.787 * Zp) + (16 / 116);
            double L = (116 * Yp) - 16;
            double A = 500 * (Xp - Yp);
            double B = 500 * (Yp - Zp);
            return new Tuple<double, double, double>(L, A, B);
        }

        private Tuple<double, double, double> CIELABtoXYZ (double L, double A, double B)
        {
            double Yp = (L + 16) / 116;
            double Xp = (A / 500) + Yp;
            double Zp = Yp - (B / 200);

            if (Math.Pow(Xp, 3) > 0.008856)
                Xp = Math.Pow(Xp, 3);
            else
                Xp = (Xp - (16 / 116) / 7.787);
            if (Math.Pow(Yp, 3) > 0.008856)
                Yp = Math.Pow(Yp, 3);
            else
                Yp = (Yp - (16 / 116) / 7.787);
            if (Math.Pow(Zp, 3) > 0.008856)
                Zp = Math.Pow(Zp, 3);
            else
                Zp = (Zp - (16 / 116) / 7.787);

            double X = RefX * Xp;
            double Y = RefY * Yp;
            double Z = RefZ * Zp;
            return new Tuple<double, double, double>(X, Y, Z);
        }
        #endregion

        private void ColorControlUpdated (object Sender, ColorInputColorChangedEventArgs e)
        {
            SimpleColor SC = Sender as SimpleColor;
            if (SC == null)
                return;
            UInt32 NewRawColor = SC.RawColorValue;
            byte R = (byte)((NewRawColor & 0x00ff0000) >> 16);
            byte G = (byte)((NewRawColor & 0x0000ff00) >> 8);
            byte B = (byte)((NewRawColor & 0x000000ff) >> 0);
            Channel1Input.Text = R.ToString();
            Channel2Input.Text = G.ToString();
            Channel3Input.Text = B.ToString();
        }

        private void HandleChannelKeyDown ( object Sender, KeyEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            if (e.Key == Key.Return)
            {
                string C1 = "";
                string C2 = "";
                string C3 = "";
                string C4 = "";
                GetRawChannelData(ref C1, ref C2, ref C3, ref C4);
            }
        }
    }
}
