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
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ImageMerger.xaml
    /// </summary>
    public partial class ImageMerger : Window
    {
        public ImageMerger()
        {
            InitializeComponent();
            SetChannels("RGB");
            UpdateStatus();
        }

        private void HandleMenuClose(object Sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HandleOpenImages(object Sender, RoutedEventArgs e)
        {

        }

        private void HandleCloseAll(object Sender, RoutedEventArgs e)
        {

        }

        private string LastReferencedDirectory = Environment.CurrentDirectory;

        private void HandleSaveAs(object Sender, RoutedEventArgs e)
        {
            if (!ImageAvailable)
                return;
            SaveFileDialog SFD = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                CreatePrompt = false,
                DefaultExt = ".png",
                DereferenceLinks = true,
                Filter = "JPEG Images|*.jpg;*.jpeg|PNG Images|*.png|TIF Images|*.tif;*.tiff|GIF Images|*.gif|Bitmap Images|*.bmp",
                FilterIndex = 2
            };
            if (string.IsNullOrEmpty(LastReferencedDirectory))
                LastReferencedDirectory = Environment.CurrentDirectory;
            SFD.InitialDirectory = LastReferencedDirectory;
            SFD.OverwritePrompt = true;
            SFD.Title = "Save Image File As";
            SFD.ValidateNames = true;
            Nullable<bool> OK = SFD.ShowDialog();
            if (OK.HasValue)
            {
                if (OK.Value)
                {
                    LastReferencedDirectory = SFD.InitialDirectory;
                    string SaveName = SFD.FileName;
                    if (string.IsNullOrEmpty(SaveName))
                        return;
                    string ImageType = System.IO.Path.GetExtension(SaveName).ToLower();

                    switch (ImageType)
                    {
                        case null:
                            CombinedStatus.Text = "Need file extension - not saved.";
                            return;

                        case "":
                            CombinedStatus.Text = "Need file extension - not saved.";
                            return;

                        case ".jpg":
                        case ".jpeg":
                            var jencoder = new JpegBitmapEncoder();
                            jencoder.Frames.Add(BitmapFrame.Create((BitmapSource)MergedImage.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                jencoder.Save(stream);
                            break;

                        case ".png":
                            var pencoder = new PngBitmapEncoder();
                            pencoder.Frames.Add(BitmapFrame.Create((BitmapSource)MergedImage.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                pencoder.Save(stream);
                            break;

                        case ".tif":
                        case ".tiff":
                            var tencoder = new TiffBitmapEncoder();
                            tencoder.Frames.Add(BitmapFrame.Create((BitmapSource)MergedImage.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                tencoder.Save(stream);
                            break;

                        case ".gif":
                            var gencoder = new GifBitmapEncoder();
                            gencoder.Frames.Add(BitmapFrame.Create((BitmapSource)MergedImage.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                gencoder.Save(stream);
                            break;

                        case ".bmp":
                            var bencoder = new BmpBitmapEncoder();
                            bencoder.Frames.Add(BitmapFrame.Create((BitmapSource)MergedImage.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                bencoder.Save(stream);
                            break;

                        default:
                            CombinedStatus.Text = "Unknown extension - not saved.";
                            return;
                    }
                }
            }
        }

        private void HandleSizeSettings(object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string SizeCalc = MI.Name;
            if (string.IsNullOrEmpty(SizeCalc))
                return;
            foreach (MenuItem SizeItem in SizeMenu.Items)
                SizeItem.IsChecked = false;
            MI.IsChecked = true;
            UpdateStatus();
        }

        private string GetSizeCalculation()
        {
            foreach (MenuItem SizeItem in SizeMenu.Items)
                if (SizeItem.IsChecked)
                    return SizeItem.Name;
            return "";
        }

        private void HandleChannelChanges(object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string ChannelsName = MI.Header as string;
            if (string.IsNullOrEmpty(ChannelsName))
                return;
            foreach (MenuItem ChannelItem in ChannelsMenu.Items)
            {
                ChannelItem.IsChecked = false;
            }
            MI.IsChecked = true;
            SetChannels(ChannelsName);
        }

        private void SetChannels(string Which)
        {
            if (string.IsNullOrEmpty(Which))
                return;
            ChannelsBlock.Text = Which;
            switch (Which.ToLower())
            {
                case "rgb":
                    MergedImage.HeaderText = "Combined RGB";
                    Channel1.HeaderText = "Red";
                    Channel2.HeaderText = "Green";
                    Channel3.HeaderText = "Blue";
                    Channel1SizeItem.Header = "Red Channel Size";
                    Channel2SizeItem.Header = "Green Channel Size";
                    Channel3SizeItem.Header = "Blue Channel Size";
                    Channel1Alpha.Header = "Set alpha to Red";
                    Channel2Alpha.Header = "Set alpha to Green";
                    Channel3Alpha.Header = "Set alpha to Blue";
                    CMYKSplitter.Width = new GridLength(0.0, GridUnitType.Pixel);
                    CMYKColumn.Width = new GridLength(0.0, GridUnitType.Pixel);
                    break;

                case "hsl":
                    MergedImage.HeaderText = "Combined HSL";
                    Channel1.HeaderText = "Hue";
                    Channel2.HeaderText = "Saturation";
                    Channel3.HeaderText = "Luminance";
                    Channel1SizeItem.Header = "Hue Channel Size";
                    Channel2SizeItem.Header = "Saturation Channel Size";
                    Channel3SizeItem.Header = "Luminance Channel Size";
                    Channel1Alpha.Header = "Set alpha to Hue";
                    Channel2Alpha.Header = "Set alpha to Saturation";
                    Channel3Alpha.Header = "Set alpha to Luminance";
                    CMYKSplitter.Width = new GridLength(0.0, GridUnitType.Pixel);
                    CMYKColumn.Width = new GridLength(0.0, GridUnitType.Pixel);
                    break;

                case "yuv":
                    MergedImage.HeaderText = "Combined YUV";
                    Channel1.HeaderText = "Y";
                    Channel2.HeaderText = "U";
                    Channel3.HeaderText = "V";
                    Channel1SizeItem.Header = "Y Channel Size";
                    Channel2SizeItem.Header = "U Channel Size";
                    Channel3SizeItem.Header = "V Channel Size";
                    Channel1Alpha.Header = "Set alpha to Y";
                    Channel2Alpha.Header = "Set alpha to U";
                    Channel3Alpha.Header = "Set alpha to V";
                    CMYKSplitter.Width = new GridLength(0.0, GridUnitType.Pixel);
                    CMYKColumn.Width = new GridLength(0.0, GridUnitType.Pixel);
                    break;

                case "cmyk":
                    MergedImage.HeaderText = "Combined CMYK";
                    Channel1.HeaderText = "Cyan";
                    Channel2.HeaderText = "Magenta";
                    Channel3.HeaderText = "Yellow";
                    Channel4.HeaderText = "Black";
                    Channel1SizeItem.Header = "Cyan Channel Size";
                    Channel2SizeItem.Header = "Magenta Channel Size";
                    Channel3SizeItem.Header = "Black Channel Size";
                    Channel1Alpha.Header = "Set alpha to Cyan";
                    Channel2Alpha.Header = "Set alpha to Magenta";
                    Channel3Alpha.Header = "Set alpha to Yellow";
                    CMYKSplitter.Width = new GridLength(4.0, GridUnitType.Pixel);
                    CMYKColumn.Width = new GridLength(1.0, GridUnitType.Auto);
                    break;

                default:
                    throw new InvalidOperationException("Unknown which.");
            }
        }

        private void AlphaMenuItemClick(object Sender, RoutedEventArgs e)
        {

        }

        private void GrayscaleMenuItemClick(object Sender, RoutedEventArgs e)
        {

        }

        private void ChannelViewMenuItemClick(object Sender, RoutedEventArgs e)
        {

        }

        private void SourceChannelDragStart(object Sender, DragOperationStartEventArgs e)
        {
        }

        private void SourceChannelObjectDropped(object Sender, ObjectsDroppedEventArgs e)
        {
            ImageFrame IF = Sender as ImageFrame;
            if (IF == null)
                return;
            IF.ShowFooterRow = true;
            IF.FooterText = MakeShortName(e.DroppedFileNames[0]);
            IF.FooterTooltip = e.DroppedFileNames[0];
            WriteableBitmap WB = null;
            bool OpenedOK = DoOpenImageFile(e.DroppedFileNames[0], out WB);
            if (!OpenedOK || WB == null)
                return;
            if (ImageMap.ContainsKey(IF.Name))
                ImageMap[IF.Name] = WB;
            else
                ImageMap.Add(IF.Name, WB);
            IF.DisplayImage = WB;
            IF.ToolTip = IF.FooterText + Environment.NewLine + WB.PixelWidth.ToString() + " x " + WB.PixelHeight.ToString() +
                Environment.NewLine + WB.DpiX.ToString() + ", " + WB.DpiY.ToString();
            UpdateStatus();
        }

        private bool AllChannelsAvailable = false;

        private void UpdateStatus()
        {
            AllChannelsAvailable = true;
            ImageAvailable = false;
            Run C1Run = MakeRun("Red ", Brushes.Red, FontWeights.Bold);
            if (!ImageMap.ContainsKey("Channel1") || (ImageMap["Channel1"] == null))
            {
                AllChannelsAvailable = false;
                C1Run.Foreground = SystemColors.GrayTextBrush;
                C1Run.FontWeight = FontWeights.Normal;
            }
            Run C2Run = MakeRun("Green ", Brushes.Green, FontWeights.Bold);
            if (!ImageMap.ContainsKey("Channel2") || (ImageMap["Channel2"] == null))
            {
                AllChannelsAvailable = false;
                C2Run.Foreground = SystemColors.GrayTextBrush;
                C2Run.FontWeight = FontWeights.Normal;
            }
            Run C3Run = MakeRun("Blue ", Brushes.Blue, FontWeights.Bold);
            if (!ImageMap.ContainsKey("Channel3") || (ImageMap["Channel3"] == null))
            {
                AllChannelsAvailable = false;
                C3Run.Foreground = SystemColors.GrayTextBrush;
                C3Run.FontWeight = FontWeights.Normal;
            }
            CombinedStatus.Inlines.Clear();
            CombinedStatus.Inlines.Add(C1Run);
            CombinedStatus.Inlines.Add(C2Run);
            CombinedStatus.Inlines.Add(C3Run);

            if (AllChannelsAvailable)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("    Final Size: ");
                Tuple<double, double> FinalSize = CalculateFinalImageSize();
                if (FinalSize == null)
                    return;
                sb.Append(((int)(FinalSize.Item1)).ToString());
                sb.Append(" x ");
                sb.Append(((int)(FinalSize.Item2)).ToString());

                Run SizeRun = new Run
                {
                    FontSize = 14.0,
                    FontWeight = FontWeights.Bold,
                    Text = sb.ToString()
                };
                CombinedStatus.Inlines.Add(SizeRun);
            }
            else
                return;

            string CombineMessage = "";
            bool OK = DoCreateCombinedImage(out CombineMessage);
            if (OK)
            {
                CombinedStatus.Inlines.Add(new Run("   "));
                Run OKRun = new Run("OK")
                {
                    FontSize = 14.0
                };
                CombinedStatus.Inlines.Add(OKRun);
                ImageAvailable = true;
            }
            else
            {
                CombinedStatus.Inlines.Add(new Run("   "));
                Run ErrorRun = new Run("Error");
                if (!string.IsNullOrEmpty(CombineMessage))
                    ErrorRun.ToolTip = CombineMessage;
                ErrorRun.Foreground = Brushes.Maroon;
                ErrorRun.Background = Brushes.Yellow;
                ErrorRun.FontSize = 15.0;
                ErrorRun.FontWeight = FontWeights.Bold;
                CombinedStatus.Inlines.Add(ErrorRun);
            }
        }

        private bool ImageAvailable = false;

        private bool DoCreateCombinedImage(out string StatusMessage)
        {
            StatusMessage = "";
            Tuple<double, double> FinalSize = CalculateFinalImageSize();
            if (FinalSize == null)
            {
                StatusMessage = "Cannot create final size.";
                return false;
            }
            WriteableBitmap WB1 = new WriteableBitmap(ImageMap["Channel1"] as BitmapSource);
            WriteableBitmap WB2 = new WriteableBitmap(ImageMap["Channel2"] as BitmapSource);
            WriteableBitmap WB3 = new WriteableBitmap(ImageMap["Channel3"] as BitmapSource);

            if (!WB1.DpiX.AllSameValue(WB2.DpiX, WB3.DpiX))
            {
                StatusMessage = "DPI X deltas.";
                return false;
            }
            if (!WB1.DpiY.AllSameValue(WB2.DpiY, WB3.DpiY))
            {
                StatusMessage = "DPI X deltas.";
                return false;
            }

            ColorBlenderInterface CBI = new ColorBlenderInterface();
            bool OK = false;
            if (WB1.PixelWidth != (int)FinalSize.Item1 || WB1.PixelHeight != (int)FinalSize.Item2)
            {
                OK = CBI.ImageScale(WB1, WB1.PixelWidth, WB1.PixelHeight, (int)FinalSize.Item1, (int)FinalSize.Item2, 1);
                if (!OK)
                {
                    StatusMessage = "Error scaling channel 1.";
                    return false;
                }
            }

            if (WB2.PixelWidth != (int)FinalSize.Item1 || WB2.PixelHeight != (int)FinalSize.Item2)
            {
                OK = CBI.ImageScale(WB2, WB2.PixelWidth, WB2.PixelHeight, (int)FinalSize.Item1, (int)FinalSize.Item2, 1);
                if (!OK)
                {
                    StatusMessage = "Error scaling channel 2.";
                    return false;
                }
            }

            if (WB3.PixelWidth != (int)FinalSize.Item1 || WB3.PixelHeight != (int)FinalSize.Item2)
            {
                OK = CBI.ImageScale(WB3, WB3.PixelWidth, WB3.PixelHeight, (int)FinalSize.Item1, (int)FinalSize.Item2, 1);
                if (!OK)
                {
                    StatusMessage = "Error scaling channel 3.";
                    return false;
                }
            }

            WriteableBitmap Final = new WriteableBitmap((int)FinalSize.Item1, (int)FinalSize.Item2, WB1.DpiX, WB1.DpiY,
                PixelFormats.Bgra32, null);
            OK = CBI.RGBImageCombine(WB1, WB2, WB3, (int)FinalSize.Item1, (int)FinalSize.Item2, Final.BackBufferStride, Final);
            if (!OK)
            {
                StatusMessage = "Error combining images.";
                return false;
            }
            MergedImage.DisplayImage = Final;
            return true;
        }

        private Tuple<double, double> CalculateFinalImageSize()
        {
            if (!AllChannelsAvailable)
                return null;
            if (MeanSizeItem.IsChecked)
            {
                double MeanWidth = (ImageMap["Channel1"].PixelWidth + ImageMap["Channel2"].PixelWidth + ImageMap["Channel3"].PixelWidth) / 3.0;
                double MeanHeight = (ImageMap["Channel1"].PixelHeight + ImageMap["Channel2"].PixelHeight + ImageMap["Channel3"].PixelHeight) / 3.0;
                return new Tuple<double, double>(MeanWidth, MeanHeight);
            }
            if (SmallestSizeItem.IsChecked)
            {
                double Channel1Size = ImageMap["Channel1"].PixelWidth * ImageMap["Channel1"].PixelHeight;
                double Channel2Size = ImageMap["Channel2"].PixelWidth * ImageMap["Channel2"].PixelHeight;
                double Channel3Size = ImageMap["Channel3"].PixelWidth * ImageMap["Channel3"].PixelHeight;
                if (Math.Min(Channel1Size, Math.Min(Channel2Size, Channel3Size)) == Channel1Size)
                    return new Tuple<double, double>(ImageMap["Channel1"].PixelWidth, ImageMap["Channel1"].PixelHeight);
                if (Math.Min(Channel1Size, Math.Min(Channel2Size, Channel3Size)) == Channel2Size)
                    return new Tuple<double, double>(ImageMap["Channel2"].PixelWidth, ImageMap["Channel2"].PixelHeight);
                if (Math.Min(Channel1Size, Math.Min(Channel2Size, Channel3Size)) == Channel3Size)
                    return new Tuple<double, double>(ImageMap["Channel3"].PixelWidth, ImageMap["Channel3"].PixelHeight);
            }
            if (LargestSizeItem.IsChecked)
            {
                double Channel1Size = ImageMap["Channel1"].PixelWidth * ImageMap["Channel1"].PixelHeight;
                double Channel2Size = ImageMap["Channel2"].PixelWidth * ImageMap["Channel2"].PixelHeight;
                double Channel3Size = ImageMap["Channel3"].PixelWidth * ImageMap["Channel3"].PixelHeight;
                if (Math.Max(Channel1Size, Math.Max(Channel2Size, Channel3Size)) == Channel1Size)
                    return new Tuple<double, double>(ImageMap["Channel1"].PixelWidth, ImageMap["Channel1"].PixelHeight);
                if (Math.Max(Channel1Size, Math.Max(Channel2Size, Channel3Size)) == Channel2Size)
                    return new Tuple<double, double>(ImageMap["Channel2"].PixelWidth, ImageMap["Channel2"].PixelHeight);
                if (Math.Max(Channel1Size, Math.Max(Channel2Size, Channel3Size)) == Channel3Size)
                    return new Tuple<double, double>(ImageMap["Channel3"].PixelWidth, ImageMap["Channel3"].PixelHeight);
            }
            if (Channel1SizeItem.IsChecked)
            {
                return new Tuple<double, double>(ImageMap["Channel1"].PixelWidth, ImageMap["Channel1"].PixelHeight);
            }
            if (Channel2SizeItem.IsChecked)
            {
                return new Tuple<double, double>(ImageMap["Channel2"].PixelWidth, ImageMap["Channel2"].PixelHeight);
            }
            if (Channel3SizeItem.IsChecked)
            {
                return new Tuple<double, double>(ImageMap["Channel3"].PixelWidth, ImageMap["Channel3"].PixelHeight);
            }
            return null;
        }

        private Run MakeRun(string RunText, Brush RunBrush, FontWeight RunWeight)
        {
            Run ChannelRun = new Run(RunText)
            {
                FontSize = 14.0,
                Foreground = RunBrush,
                FontWeight = RunWeight
            };
            return ChannelRun;
        }

        /// <summary>
        /// Return a stream of the contents of the file <paramref name="FileLocation"/>.
        /// </summary>
        /// <param name="FileLocation">The file whose contents will be streamed to the caller.</param>
        /// <returns>Stream of the content of <paramref name="FileLocation"/>.</returns>
        private Stream ImageStream(string FileLocation)
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

        private bool DoOpenImageFile(string FileName, out WriteableBitmap WB)
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

        private Dictionary<string, WriteableBitmap> ImageMap = new Dictionary<string, WriteableBitmap>();

        /// <summary>
        /// Get a writeable bitmap from <paramref name="TheImage"/>. Converts the format to BRGA32 if needed.
        /// </summary>
        /// <param name="TheImage">The image from which a writeable bitmap will be returned.</param>
        /// <returns>WriteableBitmap in the format of BGRA32.</returns>
        private WriteableBitmap GetWriteableBitmap(BitmapSource TheImage)
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

        private string MakeShortName(string LongName)
        {
            if (string.IsNullOrEmpty(LongName))
                return "";
            return System.IO.Path.GetFileName(LongName);
        }

        private void ClearChannelMenuClick(object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string ChannelName = MI.Name;
            if (string.IsNullOrEmpty(ChannelName))
                return;
            switch (ChannelName.ToLower())
            {
                case "channel1clear":
                    Channel1.DisplayImage = null;
                    if (ImageMap.ContainsKey("Channel1"))
                        ImageMap.Remove("Channel1");
                    break;

                case "channel2clear":
                    Channel2.DisplayImage = null;
                    if (ImageMap.ContainsKey("Channel2"))
                        ImageMap.Remove("Channel2");
                    break;

                case "channel3clear":
                    Channel3.DisplayImage = null;
                    if (ImageMap.ContainsKey("Channel3"))
                        ImageMap.Remove("Channel3");
                    break;

                default:
                    return;
            }

            UpdateStatus();
        }
    }
}
