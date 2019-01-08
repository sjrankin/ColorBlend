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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for ImageFrame.xaml
    /// </summary>
    public partial class ImageFrame : UserControl
    {
        public ImageFrame()
        {
            InitializeComponent();
            DisplayImage = null;
            ShowCheckerboardBackground = true;
            BGBrush = Brushes.Gray;
            ShowHeaderRow = false;
            ShowFooterRow = false;
            IsImageDropEnabled = true;
            DraggedFileNames = new List<string>();
            SetOverlay(Colors.White, 0.0);
        }

        private ColorBlenderInterface CBI = new ColorBlenderInterface();

        /// <summary>
        /// Set the overlay color and opacity.
        /// </summary>
        /// <param name="OverlayColor">Color of the overlay.</param>
        /// <param name="OverlayOpacity">Opacity of the overlay.</param>
        private void SetOverlay(Color OverlayColor, double OverlayOpacity)
        {
            ImageOverlayLayer.Background = new SolidColorBrush(OverlayColor);
            ImageOverlayLayer.Opacity = OverlayOpacity;
        }

        /// <summary>
        /// Get or set the display image.
        /// </summary>
        public WriteableBitmap DisplayImage
        {
            get
            {
                return Target.Source as WriteableBitmap;
            }
            set
            {
                Target.Source = value;
                if (Target.Source != null)
                {
                    SourceWidth = (int)Target.Source.Width;
                    SourceHeight = (int)Target.Source.Height;
                    WriteableBitmap WB = new WriteableBitmap(value);
                    ImageStride = WB.BackBufferStride;
                    int PixelArraySize = SourceWidth * SourceHeight;
                    ScrapedPixels = new byte[PixelArraySize * 4];
                    value.CopyPixels(ScrapedPixels, ImageStride, 0);
                }
                else
                {
                    SourceWidth = 0;
                    SourceHeight = 0;
                }
            }
        }

        /// <summary>
        /// Get the image stride.
        /// </summary>
        public int ImageStride { get; private set; } = 0;

        /// <summary>
        /// Holds pixels for loaded images. Needed because of WPF's gratuitous lack of individual
        /// pixel addressing in images.
        /// </summary>
        private byte[] ScrapedPixels = null;

        /// <summary>
        /// Get or set the overlay image (different from the overlay layer).
        /// </summary>
        public WriteableBitmap OverlayImage
        {
            get
            {
                return ImageOverlaySurface.Source as WriteableBitmap;
            }
            set
            {
                ImageOverlaySurface.Source = value;
            }
        }

        private bool _ShowCheckerboardBackground = true;
        /// <summary>
        /// Show or hide a checkerboard background (useful for viewing transparency effects).
        /// </summary>
        public bool ShowCheckerboardBackground
        {
            get
            {
                return _ShowCheckerboardBackground;
            }
            set
            {
                _ShowCheckerboardBackground = value;
                if (_ShowCheckerboardBackground)
                {
                    TargetContainer.Background = Utility.CheckerboardPatternBrush(24.0, 24.0, Brushes.WhiteSmoke, Brushes.LightGray);
                }
                else
                {
                    TargetContainer.Background = BGBrush;
                }
            }
        }

        private Brush _BGBrush = Brushes.Gray;
        /// <summary>
        /// Get or set the background brush.
        /// </summary>
        public Brush BGBrush
        {
            get
            {
                return _BGBrush;
            }
            set
            {
                _BGBrush = value;
                if (!ShowCheckerboardBackground)
                    TargetContainer.Background = _BGBrush;
            }
        }

        private bool _ShowHeaderRow = false;
        /// <summary>
        /// Show or hide the header row.
        /// </summary>
        public bool ShowHeaderRow
        {
            get
            {
                return _ShowHeaderRow;
            }
            set
            {
                _ShowHeaderRow = value;
                if (_ShowHeaderRow)
                    HeaderRowDefinition.Height = new GridLength(1.0, GridUnitType.Auto);
                else
                    HeaderRowDefinition.Height = new GridLength(0.0, GridUnitType.Pixel);
            }
        }

        /// <summary>
        /// Get or set the header text.
        /// </summary>
        public string HeaderText
        {
            get
            {
                return HeaderTextBlock.Text;
            }
            set
            {
                HeaderTextBlock.Text = value;
            }
        }

        private bool _ShowFooterRow = false;
        /// <summary>
        /// Show or hide the footer row.
        /// </summary>
        public bool ShowFooterRow
        {
            get
            {
                return _ShowFooterRow;
            }
            set
            {
                _ShowFooterRow = value;
                if (_ShowFooterRow)
                    FooterRowDefinition.Height = new GridLength(1.0, GridUnitType.Auto);
                else
                    FooterRowDefinition.Height = new GridLength(0.0, GridUnitType.Pixel);
            }
        }

        /// <summary>
        /// Get or set the footer text.
        /// </summary>
        public string FooterText
        {
            get
            {
                return FooterTextBlock.Text;
            }
            set
            {
                FooterTextBlock.Text = value;
            }
        }

        /// <summary>
        /// Get or set the footer tool tip text.
        /// </summary>
        public string FooterTooltip
        {
            get
            {
                return (string)FooterTextBlock.ToolTip;
            }
            set
            {
                FooterTextBlock.ToolTip = value;
            }
        }

        /// <summary>
        /// Get or set the image tool tip text.
        /// </summary>
        public string ImageToolTip
        {
            get
            {
                return (string)Target.ToolTip;
            }
            set
            {
                Target.ToolTip = value;
            }
        }

        /// <summary>
        /// Get or set the image source.
        /// </summary>
        public ImageSource Source
        {
            get
            {
                return Target.Source;
            }
            set
            {
                Target.Source = value;
                if (Target.Source != null)
                {
                    SourceWidth = (int)Target.Source.Width;
                    SourceHeight = (int)Target.Source.Height;
                    WriteableBitmap WB = new WriteableBitmap((BitmapSource)value);
                    ImageStride = WB.BackBufferStride;
                    int PixelArraySize = SourceWidth * SourceHeight * 4;
                    ScrapedPixels = new byte[PixelArraySize];
                    WB.CopyPixels(ScrapedPixels, ImageStride, 0);
                }
                else
                {
                    SourceWidth = 0;
                    SourceHeight = 0;
                }
            }
        }

        /// <summary>
        /// Get the actual width of the loaded image.
        /// </summary>
        public int SourceWidth { get; private set; } = 0;

        /// <summary>
        /// Get the actual height of the loaded image.
        /// </summary>
        public int SourceHeight { get; private set; } = 0;

        /// <summary>
        /// Get the image control.
        /// </summary>
        public Image Image
        {
            get
            {
                return Target;
            }
        }

        /// <summary>
        /// Get or set the stretch value.
        /// </summary>
        public Stretch Stretch
        {
            get
            {
                return Target.Stretch;
            }
            set
            {
                Target.Stretch = value;
                ImageOverlaySurface.Stretch = value;
            }
        }

        /// <summary>
        /// Get or set the drop enabled flag.
        /// </summary>
        public bool IsImageDropEnabled { get; set; }

        /// <summary>
        /// Handle drag entered events.
        /// </summary>
        /// <param name="Sender">The drag target.</param>
        /// <param name="e">Event data.</param>
        private void DragObjectEntered(object Sender, DragEventArgs e)
        {
            if (Sender == null)
                return;
            if (!IsImageDropEnabled)
                return;
            DraggedObjectCount = 0;
            DraggedFileNames.Clear();
            DragObjectValid(e);
            if (!InDragDropOperation)
            {
                e.Effects = DragDropEffects.None;
                base.Cursor = Cursors.No;
            }
            if (
                (e.Data.GetDataPresent(DataFormats.FileDrop)) ||
                (e.Data.GetDataPresent(DataFormats.SymbolicLink))
               )
            {
                e.Effects = DragDropEffects.Copy;
                SetOverlay(Colors.Yellow, 0.5);
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            if (DragOperationStartEvent != null)
            {
                DragOperationStartEventArgs args = new DragOperationStartEventArgs();
                DragOperationStartEvent(this, args);
                if (args.CancelDragOperation || !args.ValidDrop)
                {
                    DragCanceled = true;
                }
            }
        }

        /// <summary>
        /// Handle drag exited events.
        /// </summary>
        /// <param name="Sender">The drag target the was exited.</param>
        /// <param name="e">Event data.</param>
        private void DragObjectExited(object Sender, DragEventArgs e)
        {
            SetOverlay(Colors.White, 0.0);
            base.Cursor = Cursors.Arrow;
            InDragDropOperation = false;
            if (!IsImageDropEnabled)
                return;
        }

        /// <summary>
        /// Handle drag over events.
        /// </summary>
        /// <param name="Sender">The drag target where the drag over even occurred.</param>
        /// <param name="e">Event data.</param>
        private void DragObjectOver(object Sender, DragEventArgs e)
        {
            //            Debugger.Break();
            if (!IsImageDropEnabled)
                return;
            if (Sender == null)
                return;
            SetOverlay(Colors.Yellow, 0.0);
            InDragDropOperation = true;
        }

        /// <summary>
        /// In drag operation flag.
        /// </summary>
        private bool InDragDropOperation = false;

        /// <summary>
        /// Number of dragged objects.
        /// </summary>
        private int DraggedObjectCount = 0;

        /// <summary>
        /// Handle drag drop events.
        /// </summary>
        /// <param name="Sender">Drag target where the drop occurred.</param>
        /// <param name="e">Event data.</param>
        private void DragObjectDropped(object Sender, DragEventArgs e)
        {
            SetOverlay(Colors.White, 0.0);
            if (Sender == null)
                return;
            if (!IsImageDropEnabled)
                return;
            InDragDropOperation = false;
            base.Cursor = Cursors.Arrow;
            if (DraggedFileNames.Count < 1)
                return;
            if (ObjectsDroppedEvent != null)
            {
                ObjectsDroppedEventArgs args = new ObjectsDroppedEventArgs();
                args.DroppedFileNames.AddRange(DraggedFileNames);
                ObjectsDroppedEvent(this, args);
            }
        }

        /// <summary>
        /// Determines if the dragged object is valid.
        /// </summary>
        /// <param name="e">Drag event data.</param>
        private void DragObjectValid(DragEventArgs e)
        {
            string[] Objects = (string[])e.Data.GetData(DataFormats.FileDrop);
            DraggedObjectCount = Objects.Length;
            if (DraggedObjectCount < 1)
            {
                InDragDropOperation = false;
                return;
            }
            InDragDropOperation = true;
            DraggedFileNames = Objects.ToList();
        }

        /// <summary>
        /// Get the list of file names dropped onto the control.
        /// </summary>
        public List<string> DraggedFileNames { get; internal set; }

        /// <summary>
        /// Drag canceled flag.
        /// </summary>
        private bool DragCanceled = false;

        /// <summary>
        /// Determines if the OS should continue with dragging.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Where to store the continue dragging flag.</param>
        private void QueryKeepOnDragging(object Sender, QueryContinueDragEventArgs e)
        {
            if (DragCanceled)
            {
                DragCanceled = false;
                e.Action = DragAction.Cancel;
            }
        }

        private void HandleMouseMove(object Sender, MouseEventArgs e)
        {
            Image I = Sender as Image;
            if (I == null)
                return;
            if (ImageMousePositionChanged == null)
                return;
            Point MousePoint = e.GetPosition(I);
            double ViewPortWidth = I.ActualWidth;
            double ViewPortHeight = I.ActualHeight;
            ImageMousePositionChanged(this, new ImageMousePositionChangeArgs(MousePoint.X,
                MousePoint.Y, ViewPortWidth, ViewPortHeight));
        }

        /// <summary>
        /// Get the color at (<paramref name="X"/>,<paramref name="Y"/>).
        /// </summary>
        /// <param name="X">Horizontal value.</param>
        /// <param name="Y">Vertical value.</param>
        /// <returns>Color at (<paramref name="X"/>,<paramref name="Y"/>).</returns>
        public Color ColorAt(int X, int Y)
        {
            if (X > SourceWidth - 1)
                return Colors.Transparent;
            if (Y > SourceHeight - 1)
                return Colors.Transparent;
            int Address = (X * 4) + (Y * ImageStride);
            byte B = ScrapedPixels[Address + 0];
            byte G = ScrapedPixels[Address + 1];
            byte R = ScrapedPixels[Address + 2];
            return Color.FromRgb(R, G, B);
        }

        /// <summary>
        /// Handle mouse moves over the image.
        /// </summary>
        /// <param name="Sender">The image where the mouse moved.</param>
        /// <param name="e">Event data.</param>
        private void HandleMouseMovedOverImage(object Sender, MouseEventArgs e)
        {
            Image I = Sender as Image;
            if (I == null)
                return;
            if (ImageMousePositionChanged == null)
                return;
            if (SourceHeight == 0 || SourceWidth == 0)
                return;
            Point MousePoint = e.GetPosition(I);
            double ViewPortWidth = I.ActualWidth;
            double ViewPortHeight = I.ActualHeight;
            double HeightRatio = (double)SourceHeight / ViewPortHeight;
            double WidthRatio = (double)SourceWidth / ViewPortWidth;
            int ActualX = (int)(MousePoint.X * WidthRatio);
            int ActualY = (int)(MousePoint.Y * HeightRatio);
            ImageMousePositionChanged(this, new ImageMousePositionChangeArgs(MousePoint.X,
                MousePoint.Y, ViewPortWidth, ViewPortHeight, ActualX, ActualY, ColorAt(ActualX, ActualY)));
        }

        private void ImageFrameScrollerScrollingChanged(object Sender, ScrollChangedEventArgs e)
        {
        }
    }
}
