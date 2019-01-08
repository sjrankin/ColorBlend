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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for BayerDecoderWindow.xaml
    /// </summary>
    public partial class BayerDecoderWindow : Window
    {
        public BayerDecoderWindow ()
        {
            InitializeComponent();
        }

        private void HandleCloseButtonClick (object Sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ColorClick (object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string MIName = MI.Name;
            if (string.IsNullOrEmpty(MIName))
                return;
            string[] Parts = MIName.Split(new char[] { '_' });
            if (Parts.Length != 2)
                return;
            Color FilterColor = NameToColor(Parts[0]);
            Tuple<int, int> Where = GetCoordinate(Parts[1]);
            FilterMap[Where] = FilterColor;
            Border Cell = FilterCell(Where);
            if (Cell == null)
                throw new InvalidOperationException("Cannot find cell.");
            Cell.Background = new SolidColorBrush(FilterColor);
        }

        private Border FilterCell(Tuple<int,int> Where)
        {
            foreach(object Something in BayesFilterGrid.Children)
            {
                Border B = Something as Border;
                if (B == null)
                    continue;
                int BX = Grid.GetColumn(B);
                int BY = Grid.GetRow(B);
                if (Where.Item1 == BX && Where.Item2 == BY)
                    return B;
            }
            return null;
        }

        private Tuple<int,int> GetCoordinate(string Raw)
        {
            if (string.IsNullOrEmpty(Raw))
                throw new ArgumentNullException("Raw");
            int NotUsed = 0;
            if (Raw.Length != 2)
                throw new InvalidOperationException("Bad coordinate string length.");
            if (!int.TryParse(Raw, out NotUsed))
                throw new InvalidOperationException("Bad coordinate string.");
            int X = -1;
            int Y = -1;
            if (Raw[0] == '0')
                X = 0;
            if (Raw[0] == '1')
                X = 1;
            if (Raw[1] == '0')
                Y = 0;
            if (Raw[1] == '1')
                Y = 1;
            if (X < 0)
                throw new InvalidOperationException("Invalid X coordinate.");
            if (Y < 0)
                throw new InvalidOperationException("Invalid Y coordinate.");
            return new Tuple<int, int>(X, Y);
        }

        private Color NameToColor(string ColorName)
        {
            if (string.IsNullOrEmpty(ColorName))
                throw new ArgumentNullException("ColorName");
            switch(ColorName.ToLower())
            {
                case "red":
                    return Colors.Red;

                case "green":
                    return Colors.LimeGreen;

                case "blue":
                    return Colors.Blue;

                default:
                    throw new InvalidOperationException("Unknown color name.");
            }
        }

        private Dictionary<Tuple<int, int>, Color> FilterMap = new Dictionary<Tuple<int, int>, Color>()
        {
            {new Tuple<int,int>(0,0), Colors.Red},
            {new Tuple<int,int>(0,1), Colors.Green },
            {new Tuple<int,int>(1,0), Colors.Green },
            {new Tuple<int,int>(1,1), Colors.Blue },
        };

        private void SourceImage_DragOperationStartEvent (object Sender, DragOperationStartEventArgs e)
        {
        }

        private void SourceImage_ObjectsDroppedEvent (object Sender, ObjectsDroppedEventArgs e)
        {
            ImageFrame IF = Sender as ImageFrame;
            if (IF == null)
                return;
            IF.ShowFooterRow = true;
            WriteableBitmap WB = null;
            bool OpenedOK = DoOpenImageFile(e.DroppedFileNames[0], out WB);
            if (WB.PixelHeight % 2 != 0 || WB.PixelWidth % 2 != 0)
                return;
            if (!OpenedOK || WB == null)
                return;
            IF.FooterText = MakeShortName(e.DroppedFileNames[0]);
            IF.FooterTooltip = e.DroppedFileNames[0];
            IF.DisplayImage = WB;
        }

        private string MakeShortName (string LongName)
        {
            if (string.IsNullOrEmpty(LongName))
                return "";
            return System.IO.Path.GetFileName(LongName);
        }

        private bool DoOpenImageFile (string FileName, out WriteableBitmap WB)
        {
            WB = null;
            if (string.IsNullOrEmpty(FileName))
                return false;

            BitmapImage BI = null;
            using (Stream TheStream = ImageStream(FileName))
            {
                BI = new BitmapImage();
                BI.BeginInit();
                BI.CacheOption = BitmapCacheOption.OnLoad;
                BI.StreamSource = TheStream;
                BI.EndInit();
                if (BI.CanFreeze)
                    BI.Freeze();
            }

            BitmapSource ImageSource = BI as BitmapSource;
            WB = GetWriteableBitmap(ImageSource);

            return true;
        }

        /// <summary>
        /// Get a writeable bitmap from <paramref name="TheImage"/>. Converts the format to BRGA32 if needed.
        /// </summary>
        /// <param name="TheImage">The image from which a writeable bitmap will be returned.</param>
        /// <returns>WriteableBitmap in the format of BGRA32.</returns>
        private WriteableBitmap GetWriteableBitmap (BitmapSource TheImage)
        {
            if (TheImage.Format != PixelFormats.Bgra32)
            {
                FormatConvertedBitmap NewImage = new FormatConvertedBitmap();
                NewImage.BeginInit();
                NewImage.Source = TheImage;
                NewImage.DestinationFormat = PixelFormats.Bgra32;
                NewImage.EndInit();
                return new WriteableBitmap(NewImage);
            }
            else
                return new WriteableBitmap(TheImage);
        }

        /// <summary>
        /// Return a stream of the contents of the file <paramref name="FileLocation"/>.
        /// </summary>
        /// <param name="FileLocation">The file whose contents will be streamed to the caller.</param>
        /// <returns>Stream of the content of <paramref name="FileLocation"/>.</returns>
        private Stream ImageStream (string FileLocation)
        {
            try
            {
                Stream TheStream = new FileStream(FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
                return TheStream;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Exception reading stream from " + FileLocation, e);
            }
        }
    }
}
