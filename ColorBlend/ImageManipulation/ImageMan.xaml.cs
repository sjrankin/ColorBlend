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
using System.IO;
using Microsoft.Win32;
using HistogramDisplay;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ImageMan.xaml
    /// </summary>
    public partial class ImageMan : Window
    {
        public ImageMan()
        {
            InitializeComponent();
            ImageInfo.Text = "";
            SetMessage("");
            LastReferencedDirectory = Environment.CurrentDirectory;
            ImageSurface.Source = null;
            CBI = new ColorBlenderInterface();
            ImageAvailable = false;
            CurrentImageData = null;
            ImageStack = new Stack<Tuple<string, byte[]>>();
            //CheckerboardBorder.Background = Utility.CheckerboardPatternBrush(24.0, 24.0, Brushes.WhiteSmoke, Brushes.LightGray);
            Pipeline = new ImagePipeline();
            Pipeline.PipelineStageChanged += HandlePipelineChangeEvents;
            //PipelineStagesList.AddStage(new NOPStage());
            ImagesList.Items.Clear();
            FitImageToSize = true;
            MousePositionOverImage.Text = "";
            ColorUnderMouseBlock.Text = "";
            ColorUnderMouseSample.Background = Brushes.Transparent;
        }

        private void HandlePipelineChangeEvents(object Sender, PipelineStageChangeEventArgs e)
        {
            ImagePipeline IP = Sender as ImagePipeline;
            if (IP == null)
                return;
            //PipelineStagesList.Clear();
            List<StageBase> Stages = new List<StageBase>();
            foreach (StageBase Stage in IP)
                Stages.Add(Stage);
            //PipelineStagesList.BatchAdd(Stages, true);
        }

        private void HandleQDNodeCommandEvent(object Sender, QDNodeMouseCommandEventArgs e)
        {
            QDPipelineNode QDNode = Sender as QDPipelineNode;
            if (QDNode == null)
                return;
            if (e.RemoveItem)
                RemoveStage(QDNode.NodeID);
        }

        public bool RemoveStage(Guid StageID)
        {
            int Index = StageIndex(StageID);
            if (Index < 0)
                return false;
            Pipeline.RemoveAt(Index);
            //PipelineStagesList.RemoveStage(StageID);
            return true;
        }

        public int StageIndex(Guid StageID)
        {
            int Index = 0;
            foreach (StageBase Stage in Pipeline)
            {
                if (Stage.StageID == StageID)
                    return Index;
                Index++;
            }
            return -1;
        }

        public byte[] CurrentImageData { get; private set; }

        public bool ImageAvailable { get; private set; }

        public ColorBlenderInterface CBI;

        private void HandleCloseButtonClick(object Sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HandleCloseMenuClick(object Sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HandleMenuClick(object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string Command = MI.Tag as string;
            if (string.IsNullOrEmpty(Command))
                return;
            LoadPage(Command);
        }

        private void DoSaveImage(string TheFileName)
        {
            if (string.IsNullOrEmpty(TheFileName))
                return;

            string ImageType = System.IO.Path.GetExtension(TheFileName).ToLower();

            switch (ImageType)
            {
                case null:
                    SetMessage("Need file extension - not saved.");
                    return;

                case "":
                    SetMessage("Need file extension - not saved.");
                    return;

                case ".jpg":
                case ".jpeg":
                    var jencoder = new JpegBitmapEncoder();
                    jencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                    using (FileStream stream = new FileStream(TheFileName, FileMode.Create))
                        jencoder.Save(stream);
                    break;

                case ".png":
                    var pencoder = new PngBitmapEncoder();
                    pencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                    using (FileStream stream = new FileStream(TheFileName, FileMode.Create))
                        pencoder.Save(stream);
                    break;

                case ".tif":
                case ".tiff":
                    var tencoder = new TiffBitmapEncoder();
                    tencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                    using (FileStream stream = new FileStream(TheFileName, FileMode.Create))
                        tencoder.Save(stream);
                    break;

                case ".gif":
                    var gencoder = new GifBitmapEncoder();
                    gencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                    using (FileStream stream = new FileStream(TheFileName, FileMode.Create))
                        gencoder.Save(stream);
                    break;

                case ".bmp":
                    var bencoder = new BmpBitmapEncoder();
                    bencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                    using (FileStream stream = new FileStream(TheFileName, FileMode.Create))
                        bencoder.Save(stream);
                    break;

                default:
                    SetMessage("Unknown extension - not saved.");
                    return;
            }
        }

        private void SaveImage(object Sender, RoutedEventArgs e)
        {
            if (!ImageAvailable)
                return;
            DoSaveImage(ImageFileName);
        }

        private void SaveImageAs(object Sender, RoutedEventArgs e)
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
#if true
                    DoSaveImage(SaveName);
#else
                    string ImageType = System.IO.Path.GetExtension(SaveName).ToLower();

                    switch (ImageType)
                    {
                        case null:
                            SetMessage("Need file extension - not saved.");
                            return;

                        case "":
                            SetMessage("Need file extension - not saved.");
                            return;

                        case ".jpg":
                        case ".jpeg":
                            var jencoder = new JpegBitmapEncoder();
                            jencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                jencoder.Save(stream);
                            break;

                        case ".png":
                            var pencoder = new PngBitmapEncoder();
                            pencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                pencoder.Save(stream);
                            break;

                        case ".tif":
                        case ".tiff":
                            var tencoder = new TiffBitmapEncoder();
                            tencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                tencoder.Save(stream);
                            break;

                        case ".gif":
                            var gencoder = new GifBitmapEncoder();
                            gencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                gencoder.Save(stream);
                            break;

                        case ".bmp":
                            var bencoder = new BmpBitmapEncoder();
                            bencoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSurface.Source));
                            using (FileStream stream = new FileStream(SaveName, FileMode.Create))
                                bencoder.Save(stream);
                            break;

                        default:
                            SetMessage("Unknown extension - not saved.");
                            return;
                    }
#endif
                }
            }
        }

        private void OpenImageFile(object Sender, RoutedEventArgs e)
        {
            string FileName = "";
            bool ValidFileName = DoOpenImageFile("", out FileName);
        }

        private string ImageFileName = "";

        private bool DoOpenImageFile(string Title, out string FileName)
        {
            FileName = "";
            OpenFileDialog OFD = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".jpg",
                DereferenceLinks = true,
                Filter = "JPEG Images|*.jpg;*.jpeg|PNG Images|*.png|TIF Images|*.tif;*.tiff|GIF Images|*.gif|Bitmap Images|*.bmp|All Files|*.*"
            };
            if (string.IsNullOrEmpty(LastReferencedDirectory))
                LastReferencedDirectory = Environment.CurrentDirectory;
            OFD.FilterIndex = 6;
            OFD.InitialDirectory = LastReferencedDirectory;
            OFD.Multiselect = false;
            if (string.IsNullOrEmpty(Title))
                OFD.Title = "Open Test Image";
            else
                OFD.Title = Title;
            Nullable<bool> OK = OFD.ShowDialog();
            if (OK.HasValue)
            {
                if (OK.Value)
                {
                    double SourceOrientation = GetOrientation(OFD.FileName);
                    ImageFileName = "";
                    LastReferencedDirectory = OFD.InitialDirectory;
                    ImageAvailable = true;
                    FileName = OFD.FileName;
                    BitmapImage BI = new BitmapImage();
                    BI.BeginInit();
                    BI.StreamSource = OFD.OpenFile();
                    BitmapSource ImageSource = BI as BitmapSource;
                    switch ((int)SourceOrientation)
                    {
                        case 0:
                            BI.Rotation = Rotation.Rotate0;
                            break;

                        case 90:
                            BI.Rotation = Rotation.Rotate90;
                            break;

                        case 180:
                            BI.Rotation = Rotation.Rotate180;
                            break;

                        case 270:
                            BI.Rotation = Rotation.Rotate270;
                            break;
                    }
                    BI.EndInit();
                    WB = GetWriteableBitmap(ImageSource);
                    ImageSurface.Source = ImageSource;
                    DrawHistogram(WB);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(WB.PixelWidth.ToString());
                    sb.Append(" x ");
                    sb.Append(WB.PixelHeight.ToString());
                    sb.Append(", DPI: ");
                    sb.Append(WB.DpiX.ToString("n0"));
                    sb.Append(",");
                    sb.Append(WB.DpiY.ToString("n0"));
                    sb.Append(", ");
                    sb.Append(WB.Format.ToString());
                    ImageInfo.Text = sb.ToString();
                    ClearImages();
                    HeaderTitle.Text = System.IO.Path.GetFileName(OFD.FileName);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the orientation of an image.
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/688990/reading-metadata-from-images-in-wpf
        /// </remarks>
        /// <param name="ImageFileName">The name of the image whose rotation will be returned.</param>
        /// <returns>The rotation of an image in cardinal angles.</returns>
        public double GetOrientation(string ImageFileName)
        {
            using (FileStream Stream = new FileStream(ImageFileName, FileMode.Open, FileAccess.Read))
            {
                BitmapFrame BMFrame = BitmapFrame.Create(Stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                BitmapMetadata BMMeta = BMFrame.Metadata as BitmapMetadata;
                if ((BMMeta != null) && (BMMeta.ContainsQuery("System.Photo.Orientation")))
                {
                    object Obj = BMMeta.GetQuery("System.Photo.Orientation");
                    if (Obj != null)
                    {
                        switch ((ushort)Obj)
                        {
                            case 6:
                                return 90.0;

                            case 3:
                                return 180.0;

                            case 8:
                                return 270.0;
                        }
                    }
                }
            }
            return 0.0;
        }

        private void ClearImages()
        {
            if (PageMap == null)
                return;
            ImageFileName = "";
            foreach (KeyValuePair<string, Page> KVP in PageMap)
            {
                if (KVP.Value == null)
                    continue;
                ((IFilterPage)(KVP.Value)).Clear();
            }
        }

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

        public WriteableBitmap GetImage()
        {
            return WB;
        }

        private WriteableBitmap WB;

        public Image GetImageControl()
        {
            return ImageSurface.Image;
            //            return ImageSurface;
        }

        public void SetMessage(string Message, string ToolTipText = null)
        {
            if (Message.ToLower().Contains("error"))
            {
                StatusBlock.Background = Brushes.Yellow;
                StatusBlock.Foreground = Brushes.Maroon;
            }
            else
            {
                StatusBlock.Background = Brushes.Transparent;
                StatusBlock.Foreground = Brushes.Black;
            }
            StatusBlock.Text = Message;
            if (string.IsNullOrEmpty(ToolTipText))
                StatusBlock.ToolTip = null;
            else
                StatusBlock.ToolTip = ToolTipText;
        }

        public bool DrawImage(WriteableBitmap WB)
        {
            if (WB == null)
                return false;
            return true;
        }

        public bool FitImageToSize { get; set; }

        private void FitToSizeCheckChanged(object Sender, RoutedEventArgs e)
        {
            CheckBox CB = Sender as CheckBox;
            if (CB == null)
                return;
            bool IsChecked = CB.IsChecked.HasValue ? CB.IsChecked.Value : false;
            Stretch ImageStretch = IsChecked ? Stretch.Uniform : Stretch.None;
            ImageSurface.Stretch = ImageStretch;
            FitImageToSize = IsChecked;
        }

        public string LastReferencedDirectory = null;

        private void ResetImage(object Sender, RoutedEventArgs e)
        {
            DoResetImage();
        }

        public void DoResetImage()
        {
            ImageSurface.Source = WB;
        }

        private void ClearImage(object Sender, RoutedEventArgs e)
        {
            ImageSurface.Source = null;
            ControlsFrame.Navigate(new EmptyPage());
            ImageAvailable = false;
        }

        WriteableBitmap ExecuteBitmap = null;

        public Stack<Tuple<string, byte[]>> ImageStack { get; private set; }

        private void LoadPage(string PageName)
        {
            if (PageName == LoadedPageName)
                return;
            string LowPageName = PageName.ToLower();
            if (PageMap == null)
                MakePageMap(CBI);
            if (PageMap.Count < 1)
                return;
            if (!PageMap.ContainsKey(LowPageName))
                return;
            if (PageMap[LowPageName] == null)
                return;
            ClearImages();
            ControlsFrame.Navigate(PageMap[LowPageName]);
        }

        public string LoadedPageName = "";

        private void MakePageMap(ColorBlenderInterface CBI)
        {
            PageMap = new Dictionary<string, Page>
            {
                { "solarize", new SolarizationPage(this, CBI, ImageSurface.Image) },
                { "grayscale", new GrayscalePage(this, CBI, ImageSurface.Image) },
                { "invert", new ImageInversionPage(this, CBI, ImageSurface.Image) },
                { "mirror", new MirrorPage(this, CBI, ImageSurface.Image) },
                { "rgbmerge", new RGBColorMergePage(this, CBI, ImageSurface.Image) },
                { "splitimage", new ImageSplitPage(this, CBI, ImageSurface.Image) },
                { "sortchannels", new SortChannelsPage(this, CBI, ImageSurface.Image) },
                { "rollingmeanchannels", null },
                { "channelswap", new SwapChannelsPage(this, CBI, ImageSurface.Image) },
                { "channelselection", new ChannelSelectionPage(this, CBI, ImageSurface.Image) },
                { "autos", new AutosPage(this, CBI, ImageSurface.Image) },
                { "thresholds", new ThresholdPage(this, CBI, ImageSurface.Image) },
                { "colorspaceconversion", new ColorSpaceConversionPage(this, CBI, ImageSurface.Image) },
                { "colorreduction", new ColorReductionPage(this, CBI, ImageSurface.Image) },
                { "constantmath", new ConstantMathPage(this, CBI, ImageSurface.Image) },
                { "mathop", new MathPage(this, CBI, ImageSurface.Image) },
                { "conv_sharpen", new SharpenConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_blur", new BlurConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_edgedetection", new EdgeDetectionConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_emboss", new EmbossConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_misc", new MiscellaneousConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_user", new UserConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_mean", new MeanConvolutionPage(this, CBI, ImageSurface.Image) },
                { "conv_bayer", new BayerConvolutionPage(this, CBI, ImageSurface.Image) },
                { "channelmask", new ChannelMaskPage(this, CBI, ImageSurface.Image) },
                { "singleimagemask", new SingleImageMaskPage(this, CBI, ImageSurface.Image) },
                { "regionops", new RegionPage(this, CBI, ImageSurface.Image) },
                { "segment", new SegmentPage(this, CBI, ImageSurface.Image) },
                { "edittest", new EditPage(this, CBI, ImageSurface.Image) },
                { "colorhighlight", new ChannelHighlightPage(this, CBI, ImageSurface.Image) },
                { "squish", new ImageDistortionsPage(this, CBI, ImageSurface.Image) },
                { "frequencymasking", new FrequencyMaskPage(this, CBI, ImageSurface.Image) },
                { "resize", new ScalingPage(this, CBI, ImageSurface.Image) },
                { "rendergrid", new RenderGridPage(this, CBI, ImageSurface.Image) },
                { "bayerdecode", new BayerDecoderPage(this, CBI, ImageSurface.Image) },
                { "luminancemanipulation", new LuminancePage(this, CBI, ImageSurface.Image) },
                { "houghtransform", new HoughTransformPage(this, CBI, ImageSurface.Image) },
                { "silhouetteif", new SilhouetteIfPage(this, CBI, ImageSurface.Image) },
                { "dither", new DitherPage(this, CBI, ImageSurface.Image) },
                { "deinterlace", new DeinterlacePage(this, CBI, ImageSurface.Image) },
                { "steganographywrite", new SteganographyPage(this, CBI, ImageSurface.Image) },
                { "steganographyread", new SteganographyReadPage(this, CBI, ImageSurface.Image) },
                { "blocksegment", new BlockSegmentsPage(this, CBI, ImageSurface.Image) },
                { "addimageborder", new BordersPage(this, CBI, ImageSurface.Image) },
                { "imagecropper", new CroppingPage(this, CBI, ImageSurface.Image) },
                { "subhistorgram", new SubHistogramPage(this, CBI, ImageSurface.Image) },
                { "rotateimage", new RotatePage(this, CBI, ImageSurface.Image) },
                { "hsladjustments", new HSLPage(this, CBI, ImageSurface.Image) },
                { "linearizechannels", new SIMDLinearizePage(this, CBI, ImageSurface.Image) },
                { "pixelmath", new PixelMathPage(this, CBI, ImageSurface.Image) },
                { "pixelmathfunction", new PixelMathFunctionPage(this, CBI, ImageSurface.Image) },
                { "testhslconversions", new TestHSLConversionsPage(this, CBI, ImageSurface.Image) },
                { "hslconditionaladjustments", new HSLConditionalAdjustmentPage(this, CBI, ImageSurface.Image) },
                { "isolatehueranges", new IsolateHueRangePage(this, CBI, ImageSurface.Image) }
            };
        }

        private Dictionary<string, Page> PageMap = null;

        double[] Combined = null;
        double[] RedPercent = null;
        double[] GreenPercent = null;
        double[] BluePercent = null;
        UInt32[] RawRed = null;
        UInt32[] RawGreen = null;
        UInt32[] RawBlue = null;
        UInt32 RedSum = 0;
        UInt32 GreenSum = 0;
        UInt32 BlueSum = 0;
        UInt32 GraySum = 0;
        int BinSize = 256;

        public void DrawHistogram(WriteableBitmap Img, HistogramViewer HV = null)
        {
            if (Img == null)
                return;
            if (HV == null)
                HV = HDisplay;
            int ImageWidth = Img.PixelWidth;
            int ImageHeight = Img.PixelHeight;
            int ImageStride = Img.BackBufferStride;
            ColorBlenderInterface CBI = new ColorBlenderInterface();
            RedPercent = new double[BinSize];
            RawRed = new UInt32[BinSize];
            RedSum = 0;
            GreenPercent = new double[BinSize];
            RawGreen = new UInt32[BinSize];
            GreenSum = 0;
            BluePercent = new double[BinSize];
            RawBlue = new UInt32[BinSize];
            BlueSum = 0;
            unsafe
            {
                unsafe
                {
                    CBI.MakeHistogram((byte*)Img.BackBuffer, ImageWidth, ImageHeight, ImageStride,
                      BinSize,
                      ref RawRed, ref RedPercent, out RedSum,
                      ref RawGreen, ref GreenPercent, out GreenSum,
                      ref RawBlue, ref BluePercent, out BlueSum);
                }
            }

            MaxRedPercent = 0.0;
            MaxGreenPercent = 0.0;
            MaxBluePercent = 0.0;
            for (int i = 0; i < BinSize; i++)
            {
                if (RedPercent[i] > MaxRedPercent)
                    MaxRedPercent = RedPercent[i];
                if (GreenPercent[i] > MaxGreenPercent)
                    MaxGreenPercent = GreenPercent[i];
                if (BluePercent[i] > MaxBluePercent)
                    MaxBluePercent = BluePercent[i];
            }
            //            HDisplay.Clear();
            List<HistogramTriplet> Triplets = new List<HistogramTriplet>();
            for (int i = 0; i < BinSize; i++)
            {
                HistogramTriplet Triplet = new HistogramTriplet(RedPercent[i], GreenPercent[i], BluePercent[i])
                {
                    RawRed = RawRed[i],
                    RawGreen = RawGreen[i],
                    RawBlue = RawBlue[i]
                };
                Triplets.Add(Triplet);
            }
            HV.Clear();
            HV.BatchAdd(Triplets);
            //            HDisplay.Clear();
            //            HDisplay.BatchAdd(Triplets);
        }

        double MaxRedPercent = 0.0;
        double MaxGreenPercent = 0.0;
        double MaxBluePercent = 0.0;

        /// <summary>
        /// Draw a histogram with the passed data.
        /// </summary>
        /// <param name="Red">Red channel data.</param>
        /// <param name="Green">Green channel data.</param>
        /// <param name="Blue">Blue channel data.</param>
        private void DoDrawHistogram(double[] Red, double[] Green, double[] Blue)
        {
            HDisplay.Clear();
            if (Red == null || Green == null || Blue == null)
                return;
            List<HistogramTriplet> Triplets = new List<HistogramTriplet>();
            for (int i = 0; i < Red.Length; i++)
            {
                Triplets.Add(new HistogramTriplet(Red[i], Green[i], Blue[i]));
            }
            HDisplay.BatchAdd(Triplets);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Pipeline.Clear();
            //PipelineStagesList.Clear();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            //if (OnlyOneStage.IsChecked.Value)
            // {
            //   Pipeline.Clear();
            //PipelineStagesList.Clear();
            //}
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //if (OnlyOneStage.IsChecked.Value)
            // {
            //   Pipeline.Clear();
            //PipelineStagesList.Clear();
            // }
        }

        private ImagePipeline Pipeline = new ImagePipeline();

        private void HandleColorConversions(object Sender, RoutedEventArgs e)
        {
            ColorConversions CC = new ColorConversions();
            CC.ShowDialog();
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

        private void HandleObjectDrop(object Sender, ObjectsDroppedEventArgs e)
        {
            ImageFrame IF = Sender as ImageFrame;
            if (IF == null)
                return;
            //            WriteableBitmap WB = null;
            WB = null;
            bool OpenedOK = DoOpenImageFile(e.DroppedFileNames[0], out WB);
            if (!OpenedOK || WB == null)
                return;
            LastReferencedDirectory = System.IO.Path.GetDirectoryName(e.DroppedFileNames[0]);
            IF.DisplayImage = WB;
            DrawHistogram(WB);
            StringBuilder sb = new StringBuilder();
            sb.Append(WB.PixelWidth.ToString());
            sb.Append(" x ");
            sb.Append(WB.PixelHeight.ToString());
            sb.Append(", DPI: ");
            sb.Append(WB.DpiX.ToString("n0"));
            sb.Append(",");
            sb.Append(WB.DpiY.ToString("n0"));
            sb.Append(", ");
            sb.Append(WB.Format.ToString());
            ImageInfo.Text = sb.ToString();
            ImageAvailable = true;
            ClearImages();
        }

        private void DragStartedDoNothing(object Sender, DragOperationStartEventArgs e)
        {

        }

        private void HandleClearImagesList(object Sender, RoutedEventArgs e)
        {
            ImagesList.Items.Clear();
        }

        private void HandleLoadImagesList(object Sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".jpg",
                DereferenceLinks = true,
                Filter = "JPEG Images|*.jpg;*.jpeg|PNG Images|*.png|TIF Images|*.tif;*.tiff|GIF Images|*.gif|Bitmap Images|*.bmp|All Files|*.*"
            };
            if (string.IsNullOrEmpty(LastReferencedDirectory))
                LastReferencedDirectory = Environment.CurrentDirectory;
            OFD.FilterIndex = 6;
            OFD.InitialDirectory = LastReferencedDirectory;
            OFD.Multiselect = true;
            OFD.Title = "Select Multiple Images";
            Nullable<bool> OK = OFD.ShowDialog();
            if (OK.HasValue)
            {
                if (OK.Value)
                {
                    ImagesList.Items.Clear();
                    ImageFileName = "";
                    LastReferencedDirectory = OFD.InitialDirectory;
                    ImageAvailable = true;
                    AddImageStreamsToImages(OFD.OpenFiles(), OFD.FileNames);
                }
            }
        }

        private void AddImageStreamsToImages(Stream[] Images, string[] ImageNames)
        {
            if (Images.Length != ImageNames.Length)
                throw new InvalidOperationException("Image streams and names are of different quantities.");
            for (int i = 0; i < Images.Length; i++)
                AddImageToImages(Images[i], ImageNames[i]);
        }

        private void AddImageToImages(Stream ImageStream, string FileName)
        {
            BitmapImage BI = new BitmapImage();
            BI.BeginInit();
            BI.StreamSource = ImageStream;
            BI.DecodePixelWidth = 96;
            BI.EndInit();
            BitmapSource ImageSource = BI as BitmapSource;
            WriteableBitmap WB = GetWriteableBitmap(ImageSource);
            HistogramData HD = new HistogramData(FileName)
            {
                BinSize = 256,
                Img = WB
            };
            HistogramViewer.GenerateHistogramData(ref HD);
            ImageFrame IF = new ImageFrame
            {
                Tag = HD,
                Source = ImageSource,
                Margin = new Thickness(2),
                //            IF.HeaderText = System.IO.Path.GetFileName(FileName);
                //            IF.ShowHeaderRow = true;
                HorizontalContentAlignment = HorizontalAlignment.Left
            };
            ImagesList.Items.Add(IF);
        }

        private void ImagesList_SelectionChanged(object Sender, SelectionChangedEventArgs e)
        {
            ListBox LB = Sender as ListBox;
            if (LB == null)
                return;
            ImageFrame TheImage = LB.SelectedItem as ImageFrame;
            if (TheImage == null)
                return;
            HistogramData HD = TheImage.Tag as HistogramData;
            if (HD == null)
                return;
            HDisplay.Clear();
            HDisplay.BatchAdd(HD.Triplets);
        }

        /// <summary>
        /// Handle mouse motion over the image.
        /// </summary>
        /// <param name="Sender">The image frame where the motion occurred.</param>
        /// <param name="e">Event data.</param>
        private void HandleMouseMovedOverImage(object Sender, ImageMousePositionChangeArgs e)
        {
            ImageFrame IF = Sender as ImageFrame;
            if (IF == null)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append(e.ImageX);
            sb.Append(",");
            sb.Append(e.ImageY);
            MousePositionOverImage.Text = sb.ToString();
            ColorUnderMouse = e.UnderMouse;
            ShowColorValue(CurrentDisplaySpace);
            ColorUnderMouseSample.Background = new SolidColorBrush(e.UnderMouse);
        }

        /// <summary>
        /// Display the color's numeric value in the appropriate color space.
        /// </summary>
        /// <param name="ColorSpaceOut">The color space to use to display the color value.</param>
        private void ShowColorValue(DisplayColorSpaces ColorSpaceOut)
        {
            StringBuilder sb = new StringBuilder();
            switch (ColorSpaceOut)
            {
                case DisplayColorSpaces.Hex:
                    sb.Append(ColorUnderMouse.ToHexColor(true));
                    break;

                case DisplayColorSpaces.RGB:
                    sb.Append("RGB: ");
                    sb.Append(ColorUnderMouse.A);
                    sb.Append(",");
                    sb.Append(ColorUnderMouse.R);
                    sb.Append(",");
                    sb.Append(ColorUnderMouse.G);
                    sb.Append(",");
                    sb.Append(ColorUnderMouse.B);
                    break;

                case DisplayColorSpaces.HSL:
                    CBI.ConvertRGBtoHSL2A(ColorUnderMouse.R, ColorUnderMouse.G, ColorUnderMouse.B,
                        out double Hue, out double Saturation, out double Luminance);
                    sb.Append("HSL: ");
                    sb.Append(Hue.ToString("N2"));
                    sb.Append("°,");
                    sb.Append(Saturation.ToString("N2"));
                    sb.Append(",");
                    sb.Append(Luminance.ToString("N2"));
                    break;

                case DisplayColorSpaces.CMYK:
                    CBI.ConvertRGBtoCMYK(ColorUnderMouse.R, ColorUnderMouse.G, ColorUnderMouse.B,
                        out double C, out double M, out double Y, out double K);
                    C *= 100.0;
                    int ci = (int)C;
                    M *= 100.0;
                    int mi = (int)M;
                    Y *= 100.0;
                    int yi = (int)Y;
                    K *= 100.0;
                    int ki = (int)K;
                    sb.Append("CMYK: ");
                    sb.Append(ci);
                    sb.Append(",");
                    sb.Append(mi);
                    sb.Append(",");
                    sb.Append(yi);
                    sb.Append(",");
                    sb.Append(ki);
                    break;
            }
            ColorUnderMouseBlock.Text = sb.ToString();
        }

        /// <summary>
        /// Get the color under the mouse.
        /// </summary>
        public Color ColorUnderMouse { get; private set; } = Colors.Transparent;

        private DisplayColorSpaces CurrentDisplaySpace = DisplayColorSpaces.Hex;

        private void ShowColorValueAs(object Sender, RoutedEventArgs e)
        {
            MenuItem MI = Sender as MenuItem;
            if (MI == null)
                return;
            string MenuName = MI.Name;
            if (string.IsNullOrEmpty(MenuName))
                return;
            HexContextMenu.IsChecked = false;
            RGBContextMenu.IsChecked = false;
            HSLContextMenu.IsChecked = false;
            CMYKContextMenu.IsChecked = false;
            switch (MenuName)
            {
                case "HexContextMenu":
                    CurrentDisplaySpace = DisplayColorSpaces.Hex;
                    HexContextMenu.IsChecked = true;
                    break;

                case "RGBContextMenu":
                    CurrentDisplaySpace = DisplayColorSpaces.RGB;
                    RGBContextMenu.IsChecked = true;
                    break;

                case "HSLContextMenu":
                    CurrentDisplaySpace = DisplayColorSpaces.HSL;
                    HSLContextMenu.IsChecked = true;
                    break;

                case "CMYKContextMenu":
                    CurrentDisplaySpace = DisplayColorSpaces.CMYK;
                    CMYKContextMenu.IsChecked = true;
                    break;

                default:
                    return;
            }
            ShowColorValue(CurrentDisplaySpace);
        }

        private enum DisplayColorSpaces
        {
            Hex,
            RGB,
            HSL,
            CMYK
        }
    }
}
