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
    /// Interaction logic for Configurator.xaml. Allows the management of test points for color blending.
    /// </summary>
    public partial class Configurator : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Configurator ()
        {
            InitializeComponent();
            ColorData = new List<ColorPoint>();
            OKClicked = false;
        }

        /// <summary>
        /// Global random number generator.
        /// </summary>
        private Random Rand = new Random();

        /// <summary>
        /// On successful (e.g., OK) exit, contains the results of the edit session. If Cancel was clicked, this property is undefined.
        /// </summary>
        public List<ColorPoint> ColorData { get; internal set; }

        /// <summary>
        /// Initialize the dialog with the passed data.
        /// </summary>
        /// <param name="ColorData">The set of color points to potentially edit.</param>
        public void Start (ref List<ColorPoint> ColorData)
        {
            this.ColorData = ColorData;
            if (ColorData == null)
                throw new InvalidOperationException("Invalid color data.");
            if (ColorData.Count < 1)
                MakeDefaultColor();
            PointListBox.Items.Clear();
            foreach (ColorPoint SomeColor in ColorData)
                AddColorPoint(SomeColor);
            PointListBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Make a default color in case the data source from the caller is empty.
        /// </summary>
        private void MakeDefaultColor ()
        {
            ColorPoint CP = new ColorPoint("Default");
            CP.RelativePoint.X = 0.5;
            CP.RelativePoint.Y = 0.5;
            CP.PointColor = Colors.Red;
            CP.UseRadius = true;
            CP.UseAlpha = true;
            CP.Radius = 100.0;
            CP.AlphaStart = 1.0;
            CP.AlphaEnd = 0.0;
            ColorData.Add(CP);
        }

        /// <summary>
        /// Add a color point to the list box.
        /// </summary>
        /// <param name="TheColor"></param>
        private void AddColorPoint (ColorPoint TheColor)
        {
            PointListBox.Items.Add(ColorPointUI(TheColor));
        }

        /// <summary>
        /// Create the color point list box user interface object.
        /// </summary>
        /// <param name="TheColor">The color.</param>
        /// <returns>UI object to display in the list box.</returns>
        private ColorPointItem ColorPointUI (ColorPoint TheColor)
        {
            ColorPointItem CPI = new ColorPointItem();
            PopulateListItem(CPI, TheColor.ColorPointID);
            return CPI;
        }

        /// <summary>
        /// Update the list box item whose ID is <paramref name="ItemID"/>.
        /// </summary>
        /// <param name="ItemID">Determines which item is updated.</param>
        private void UpdateListItemUI (Guid ItemID)
        {
            foreach (ColorPointItem CPI in PointListBox.Items)
                if (CPI.ItemID == ItemID)
                {
                    PopulateListItem(CPI, ItemID);
                    return;
                }
        }

        /// <summary>
        /// Populate the UI object that will be displayed in the list box.
        /// </summary>
        /// <param name="TheItem">The UI object.</param>
        /// <param name="ItemID">ID of the color to display.</param>
        private void PopulateListItem (ColorPointItem TheItem, Guid ItemID)
        {
            ColorPoint TheColor = GetColorPoint(ItemID);
            TheItem.ItemID = ItemID;
            TheItem.PointNameBlock.Text = TheColor.Name;
            TheItem.ItemNameNormal = TheColor.Enabled;
            TheItem.RelativePointBlock.Text = "(" + TheColor.RelativePoint.X.ToString("f2") + ", " + TheColor.RelativePoint.Y.ToString("f2") + ")";
            TheItem.ColorSample.Background = new SolidColorBrush(TheColor.PointColor);
            TheItem.ColorValueBlock.Text = MakeColorString(TheColor.PointColor);
        }

        /// <summary>
        /// Get the OK clicked flag.
        /// </summary>
        public bool OKClicked { get; internal set; }

        /// <summary>
        /// Handle clicks in OK.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleOKCliked (object Sender, RoutedEventArgs e)
        {
            OKClicked = true;
            this.Close();
        }

        /// <summary>
        /// Handle the cancel clicked flag.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleCancelClicked (object Sender, RoutedEventArgs e)
        {
            OKClicked = false;
            ColorData = null;
            this.Close();
        }

        /// <summary>
        /// Handle addition of new color points.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleAddPointClick (object Sender, RoutedEventArgs e)
        {
            ColorPoint NewColor = new ColorPoint();
            PointListBox.Items.Add(ColorPointUI(NewColor));
            PointListBox.SelectedItem = PointListBox.Items.Count - 1;
        }

        /// <summary>
        /// Handle removal of color points.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleRemovePointClick (object Sender, RoutedEventArgs e)
        {
            int Index = PointListBox.SelectedIndex;
            if (Index < 0)
                return;
            PointListBox.Items.RemoveAt(Index);
            ColorData.RemoveAt(Index);
            int NewSelection = Index - 1 > ColorData.Count - 1 ? ColorData.Count - 1 : Index - 1;
            PointListBox.SelectedIndex = NewSelection;
        }

        /// <summary>
        /// Handle move point up.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleMovePointUpClick (object Sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Handle move point down.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleMovePointDownClick (object Sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Handle reset points.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleResetPointsClick (object Sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Handle randomize all points in the color point list.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleRandomizeEverything (object Sender, RoutedEventArgs e)
        {
            if (Sender == null)
                return;
            foreach (ColorPoint PointData in ColorData)
                DoRandomize(PointData.ColorPointID);
        }

        /// <summary>
        /// Remove all points.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void HandleClearAllPoints (object Sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Handle selection changes in the color point list.
        /// </summary>
        /// <param name="Sender">Where the change occurred.</param>
        /// <param name="e">Not used.</param>
        private void PointListSelectionChanged (object Sender, SelectionChangedEventArgs e)
        {
            ListBox LB = Sender as ListBox;
            if (LB == null)
                return;
            int Index = LB.SelectedIndex;
            if (Index < 0)
                return;
            ColorPointItem CPI = LB.SelectedItem as ColorPointItem;
            if (CPI == null)
                return;
            CurrentlySelectedColor = CPI.ItemID;
            PopulatePoint(CurrentlySelectedColor);
        }

        /// <summary>
        /// Currently selected point color ID.
        /// </summary>
        private Guid CurrentlySelectedColor = Guid.Empty;

        /// <summary>
        /// Given <paramref name="PointID"/>, return the associated color point class.
        /// </summary>
        /// <param name="PointID">The ID of the color point to return.</param>
        /// <returns>The color point on success, null if not found.</returns>
        private ColorPoint GetColorPoint (Guid PointID)
        {
            foreach (ColorPoint CP in ColorData)
                if (CP.ColorPointID == PointID)
                    return CP;
            return null;
        }

        /// <summary>
        /// Populate the edit section of the user interface with color data from <paramref name="PointID"/>.
        /// </summary>
        /// <param name="PointID">Determines which color point is used to populate the fields.</param>
        private void PopulatePoint (Guid PointID)
        {
            ColorPoint CP = GetColorPoint(PointID);
            if (CP == null)
                throw new InvalidOperationException("Cannot find selected color.");
            if (string.IsNullOrEmpty(CP.Name))
                PointNameBox.Text = "no name";
            else
                PointNameBox.Text = CP.Name;
            RelativeXBox.Text = CP.RelativePoint.X.ToString("f2");
            RelativeYBox.Text = CP.RelativePoint.Y.ToString("f2");
            ColorBox.Text = MakeColorString(CP.PointColor);
            ColorSample.Background = new SolidColorBrush(CP.PointColor);
            EnableRadiusCheck.IsChecked = CP.UseRadius;
            EnableAlphaCheck.IsChecked = CP.UseAlpha;
            EnablePointCheck.IsChecked = CP.Enabled;
            RadiusBox.Text = CP.Radius.ToString("f2");
            OriginAlphaBox.Text = CP.AlphaStart.ToString("f2");
            RadialAlphaBox.Text = CP.AlphaEnd.ToString("f2");
            EnableIndicatorsCheck.IsChecked = CP.DrawHorizontalIndicators | CP.DrawPointIndicator | CP.DrawVerticalIndicators;
            foreach (ColorPointItem CPI in PointListBox.Items)
                if (CPI.ItemID == PointID)
                {
                    PopulateListItem(CPI, PointID);
                    break;
                }
        }

        /// <summary>
        /// Convert a color to a string in the form #aarrggbb.
        /// </summary>
        /// <param name="From">The color to convert.</param>
        /// <returns>String representation of <paramref name="From"/>.</returns>
        private string MakeColorString (Color From)
        {
            string Final = "#";
            Final += From.A.ToString("x2");
            Final += From.R.ToString("x2");
            Final += From.G.ToString("x2");
            Final += From.B.ToString("x2");
            return Final;
        }

        private void GetPointData (Guid PointID)
        {
        }

        /// <summary>
        /// Randomize the color point and UI for the color point whose ID is <paramref name="WhichPoint"/>.
        /// </summary>
        /// <param name="WhichPoint">Determines which color point to randomzie.</param>
        private void DoRandomize (Guid WhichPoint)
        {
            ColorPoint CP = GetColorPoint(WhichPoint);
            if (CP == null)
                throw new InvalidOperationException("Invalid color point ID.");
            CP.PointColor = RandomColor(0xa0, 0xff);
            CP.RelativePoint.X = RandomNormal();
            CP.RelativePoint.Y = RandomNormal();
            CP.AlphaStart = RandomNormal();
            CP.AlphaEnd = RandomNormal();
            CP.UseAlpha = RandomBoolean();
            CP.UseRadius = RandomBoolean();
            CP.Radius = (double)Rand.Next(20, 200);
            if (WhichPoint == CurrentlySelectedColor)
                PopulatePoint(WhichPoint);
            UpdateListItemUI(CP.ColorPointID);
        }

        /// <summary>
        /// Return a randomly constructed color.
        /// </summary>
        /// <param name="LowValue">Lowest legal value for any channel.</param>
        /// <param name="HighValue">Highest legal value for any channel.</param>
        /// <param name="IgnoreAlpha">Determines if the alpha channel is ignored. If so, the alpha value will be 0xff.</param>
        /// <returns>Color constructed with random values.</returns>
        private Color RandomColor (byte LowValue, byte HighValue, bool IgnoreAlpha = true)
        {
            if (LowValue > HighValue)
                throw new InvalidOperationException("Low and High values mixed up.");
            List<Byte> Channels = new List<byte>();
            for (int i = 0; i < 4; i++)
                Channels.Add((byte)Rand.Next((int)LowValue, (int)HighValue));
            if (IgnoreAlpha)
                Channels[0] = 0xff;
            Color Final = Color.FromArgb(Channels[0], Channels[1], Channels[2], Channels[3]);
            return Final;
        }

        /// <summary>
        /// Return a value between 0.0 and 1.0.
        /// </summary>
        /// <returns>Random normal value.</returns>
        private double RandomNormal ()
        {
            int intRand = Rand.Next(0, 1000);
            return (double)intRand / 1000.0;
        }

        /// <summary>
        /// Return a random boolean value.
        /// </summary>
        /// <returns>Random boolean value.</returns>
        private bool RandomBoolean ()
        {
            return Rand.Next(10000) > 5000 ? true : false;
        }

        /// <summary>
        /// Randomize the currently selected color point.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void RandomizeSinglePoint (object Sender, RoutedEventArgs e)
        {
            DoRandomize(CurrentlySelectedColor);
        }

        #region UI response methods.
        /// <summary>
        /// Handle text boxes loosing focus.
        /// </summary>
        /// <param name="Sender">The text box that lost focus.</param>
        /// <param name="e">Not used.</param>
        private void TextBoxLostFocus (object Sender, RoutedEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            ColorPoint CP = GetColorPoint(CurrentlySelectedColor);
            if (CP == null)
                return;
            string BoxValue = TB.Text;
            double FinalValue = 0.0;
            if ((TB.Name != "PointNameBox") && (TB.Name != "ColorBox"))
            {
                if (string.IsNullOrEmpty(BoxValue))
                    BoxValue = "0.0";
                if (!double.TryParse(BoxValue, out FinalValue))
                    FinalValue = 0.0;
            }
            switch (TB.Name)
            {
                case "ColorBox":
                    if (string.IsNullOrEmpty(BoxValue))
                    {
                        BoxValue = "#ffffffff";
                        TB.Text = BoxValue;
                    }
                    CP.PointColor = MakeColor(BoxValue);
                    UpdateListItemUI(CP.ColorPointID);
                    ColorSample.Background = new SolidColorBrush(CP.PointColor);
                    break;

                case "PointNameBox":
                    if (string.IsNullOrEmpty(BoxValue))
                    {
                        CP.Name = "Point " + Rand.Next(100, 1000).ToString();
                        TB.Text = CP.Name;
                    }
                    else
                        CP.Name = BoxValue;
                    break;

                case "RelativeXBox":
                    CP.RelativePoint.X = ForceNormal(FinalValue);
                    UpdateListItemUI(CP.ColorPointID);
                    break;

                case "RelativeYBox":
                    CP.RelativePoint.Y = ForceNormal(FinalValue);
                    UpdateListItemUI(CP.ColorPointID);
                    break;

                case "RadiusBox":
                    CP.Radius = Math.Abs(FinalValue);
                    break;

                case "OriginAlphaBox":
                    CP.AlphaStart = ForceNormal(FinalValue);
                    break;

                case "RadiusAlphaBox":
                    CP.AlphaEnd = ForceNormal(FinalValue);
                    break;
            }
        }

        /// <summary>
        /// Given the string representation of a color, return the color value.
        /// </summary>
        /// <param name="Raw">String representation of a color.</param>
        /// <returns>Color value based on <paramref name="Raw"/>.</returns>
        private Color MakeColor (string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                Raw = "#ffffffff";
            int Start = 0;
            if (Raw[0] == '#')
                Start = 1;
            Raw = Raw.Substring(Start);
            List<byte> Channels = ColorParts(Raw);
            Color FinalColor = Color.FromArgb(Channels[0], Channels[1], Channels[2], Channels[3]);
            return FinalColor;
        }

        /// <summary>
        /// Return a list of color channel values based on <paramref name="RawValue"/>.
        /// </summary>
        /// <param name="RawValue">The string value that is converted to color channel values.</param>
        /// <returns>List of color channel values.</returns>
        private List<byte> ColorParts (string RawValue)
        {
            byte A = 0xff;
            byte R = 0x0;
            byte G = 0x0;
            byte B = 0x0;
            List<byte> Parts = new List<byte>() { 0xff, 0x0, 0x0, 0x0 };
            UInt32 FullRaw = Convert.ToUInt32(RawValue, 16);
            A = (byte)((FullRaw & 0xff000000) >> 24);
            R = (byte)((FullRaw & 0x00ff0000) >> 16);
            G = (byte)((FullRaw & 0x0000ff00) >> 8);
            B = (byte)((FullRaw & 0x000000ff) >> 0);
            Parts.Clear();
            Parts.Add(A);
            Parts.Add(R);
            Parts.Add(G);
            Parts.Add(B);
            return Parts;
        }

        /// <summary>
        /// Force <paramref name="Source"/> into the range of 0.0 to 1.0.
        /// </summary>
        /// <param name="Source">The value to coerce into a normal.</param>
        /// <returns>Normal value based on <paramref name="Source"/>.</returns>
        private double ForceNormal (double Source)
        {
            if (Source < 0.0)
                return 0.0;
            if (Source > 1.0)
                return 1.0;
            return Source;
        }

        /// <summary>
        /// Handle key pressed in text boxes - specifically look for return presses.
        /// </summary>
        /// <param name="Sender">The text box where the key press occurred.</param>
        /// <param name="e">Key press data.</param>
        private void TextBoxKeyDown (object Sender, KeyEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            if (e.Key == Key.Return)
            {
                ColorPoint CP = GetColorPoint(CurrentlySelectedColor);
                if (CP == null)
                    return;
                string BoxValue = TB.Text;
                double FinalValue = 0.0;
                if ((TB.Name != "PointNameBox") && (TB.Name != "ColorBox"))
                {
                    if (string.IsNullOrEmpty(BoxValue))
                        BoxValue = "0.0";
                    if (!double.TryParse(BoxValue, out FinalValue))
                        FinalValue = 0.0;
                }
                switch (TB.Name)
                {
                    case "ColorBox":
                        if (string.IsNullOrEmpty(BoxValue))
                        {
                            BoxValue = "#ffffffff";
                            TB.Text = BoxValue;
                        }
                        CP.PointColor = MakeColor(BoxValue);
                        UpdateListItemUI(CP.ColorPointID);
                        ColorSample.Background = new SolidColorBrush(CP.PointColor);
                        break;

                    case "PointNameBox":
                        if (string.IsNullOrEmpty(BoxValue))
                        {
                            CP.Name = "Point " + Rand.Next(100, 1000).ToString();
                            TB.Text = CP.Name;
                        }
                        else
                            CP.Name = BoxValue;
                        UpdateListItemUI(CP.ColorPointID);
                        break;

                    case "RelativeXBox":
                        CP.RelativePoint.X = ForceNormal(FinalValue);
                        UpdateListItemUI(CP.ColorPointID);
                        break;

                    case "RelativeYBox":
                        CP.RelativePoint.Y = ForceNormal(FinalValue);
                        UpdateListItemUI(CP.ColorPointID);
                        break;

                    case "RadiusBox":
                        CP.Radius = Math.Abs(FinalValue);
                        break;

                    case "OriginAlphaBox":
                        CP.AlphaStart = ForceNormal(FinalValue);
                        break;

                    case "RadiusAlphaBox":
                        CP.AlphaEnd = ForceNormal(FinalValue);
                        break;
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// Return the color point item whose ID is <paramref name="ItemID"/>.
        /// </summary>
        /// <param name="ItemID">ID of the item to return.</param>
        /// <returns>Color point item with the ID of <paramref name="ItemID"/> on success, null if not found.</returns>
        private ColorPointItem GetColorPointItem (Guid ItemID)
        {
            foreach (ColorPointItem CPI in PointListBox.Items)
                if (CPI.ItemID == ItemID)
                    return CPI;
            return null;
        }

        /// <summary>
        /// Handle check box value changes.
        /// </summary>
        /// <param name="Sender">The check box whose value change.</param>
        /// <param name="e">Not used.</param>
        private void HandleChecked (object Sender, RoutedEventArgs e)
        {
            CheckBox CB = Sender as CheckBox;
            if (CB == null)
                return;
            ColorPoint CP = GetColorPoint(CurrentlySelectedColor);
            if (CP == null)
                return;
            switch (CB.Name)
            {
                case "EnableRadiusCheck":
                    CP.UseRadius = CB.IsChecked.Value;
                    break;

                case "EnableAlphaCheck":
                    CP.UseAlpha = CB.IsChecked.Value;
                    break;

                case "EnablePointCheck":
                    CP.Enabled = CB.IsChecked.Value;
                    ColorPointItem CPI = GetColorPointItem(CP.ColorPointID);
                    if (CPI == null)
                        throw new InvalidOperationException("Can't find color point item.");
                    CPI.ItemNameNormal = CP.Enabled;
                    break;

                case "EnableIndicatorsCheck":
                    CP.DrawHorizontalIndicators = CB.IsChecked.Value;
                    CP.DrawVerticalIndicators = CB.IsChecked.Value;
                    CP.DrawPointIndicator = CB.IsChecked.Value;
                    break;
            }
        }
        #endregion

        #region Static factory method.
        /// <summary>
        /// Create the standard set of color points.
        /// </summary>
        /// <returns>List of standard color points.</returns>
        public static List<ColorPoint> StandardPoints ()
        {
            List<ColorPoint> Standard = new List<ColorPoint>();
            /*
            ColorPoint CP0 = new ColorPoint("Point A", 0.0, 0.0, Colors.Red);
            ColorPoint CP1 = new ColorPoint("Point B", 1.0, 1.0, Colors.Green);
            ColorPoint CP2 = new ColorPoint("Point C", 0.0, 1.0, Colors.Blue);
            ColorPoint CP3 = new ColorPoint("Point D", 1.0, 0.0, Colors.Yellow);
            */
            ColorPoint CP0 = new ColorPoint("Point A", 100, 100, 100, 100, Colors.Red);
            ColorPoint CP1 = new ColorPoint("Point B", 50, 50, 60, 60, Colors.Green);
            ColorPoint CP2 = new ColorPoint("Point C", 200, 200, 50, 50, Colors.Blue);
            ColorPoint CP3 = new ColorPoint("Point D", 0, 0, 64, 64, Colors.Yellow);
            Standard.Add(CP0);
            Standard.Add(CP1);
            Standard.Add(CP2);
            Standard.Add(CP3);
            return Standard;
        }

        /// <summary>
        /// Used when generating color point names.
        /// </summary>
        private static int FactoryPointID = 0;

        /// <summary>
        /// Return a new color point.
        /// </summary>
        /// <returns>New color point.</returns>
        public static ColorPoint MakeColorPoint ()
        {
            string PointName = "Point " + ('A' + FactoryPointID).ToString();
            FactoryPointID++;
            ColorPoint CP = new ColorPoint(PointName, 0.5, 0.5, Colors.White);
            return CP;
        }
        #endregion
    }
}
