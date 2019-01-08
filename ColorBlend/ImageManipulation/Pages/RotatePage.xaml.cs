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
using System.Diagnostics.Contracts;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for RotatePage.xaml
    /// </summary>
    public partial class RotatePage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RotatePage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public RotatePage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            Contract.Assert(ParentWindow != null);
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;
            if (ParentWindow.GetImageControl().Source == null)
                return;
            WriteableBitmap Scratch = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            DisplayOriginalSize(Scratch.PixelWidth, Scratch.PixelHeight);
            OriginalWidth = Scratch.PixelWidth;
            OriginalHeight = Scratch.PixelHeight;
            UpdateCurrentRotation();
        }

        /// <summary>
        /// Current angle of the image. Increments of 90°.
        /// </summary>
        public int CurrentAngle = 0;
        /// <summary>
        /// Original image width.
        /// </summary>
        private int OriginalWidth = 0;
        /// <summary>
        /// Original image height.
        /// </summary>
        private int OriginalHeight = 0;

        /// <summary>
        /// Clear the image.
        /// </summary>
        public void Clear()
        {
            Original = null;
        }

        /// <summary>
        /// Emit a pipeline stage.
        /// </summary>
        /// <returns>Pipeline stage for the rotation of images.</returns>
        public StageBase EmitPipelineStage()
        {
            return null;
        }

        /// <summary>
        /// Holds a reference to the color blender interface.
        /// </summary>
        private ColorBlenderInterface CBI;
        /// <summary>
        /// The original image.
        /// </summary>
        public WriteableBitmap Original = null;
        /// <summary>
        /// The image surface.
        /// </summary>
        private Image ImageSurface;
        /// <summary>
        /// The parent window.
        /// </summary>
        public ImageMan ParentWindow = null;

        /// <summary>
        /// Reset the local image.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ResetLocalImage(object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
            WriteableBitmap Scratch = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            DisplayOriginalSize(Scratch.PixelWidth, Scratch.PixelHeight);
            CurrentAngle = 0;
            CurrentRotation = RotateRightByDegrees.By0;
            UpdateCurrentRotation();
            NewDimensionsOut.Text = MakeDimensionalString(Scratch.PixelWidth, Scratch.PixelHeight).ToString();
        }

        /// <summary>
        /// Update the rotational indicators.
        /// </summary>
        private void UpdateCurrentRotation()
        {
            CurrentRotationOut.Text = ((int)(CurrentAngle)).ToString() + "°";
            string ImageName = "";
            switch (CurrentRotation)
            {
                case RotateRightByDegrees.By0:
                    ImageName = @"/ColorBlend;component/Images/0°.png";
                    break;

                case RotateRightByDegrees.RightBy90:
                    ImageName = @"/ColorBlend;component/Images/90°.png";
                    break;

                case RotateRightByDegrees.By180:
                    ImageName = @"/ColorBlend;component/Images/180°.png";
                    break;

                case RotateRightByDegrees.RightBy270:
                    ImageName = @"/ColorBlend;component/Images/270°.png";
                    break;

                default:
                    return;
            }

            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(ImageName, UriKind.RelativeOrAbsolute);
            bi3.EndInit();
            RotationalIndicator.Source = bi3;
        }

        /// <summary>
        /// Make a pretty string with the supplied dimensions.
        /// </summary>
        /// <param name="Width">Width of something.</param>
        /// <param name="Height">Height of something.</param>
        /// <returns>StringBuilder with a pretty string.</returns>
        private StringBuilder MakeDimensionalString(int Width, int Height)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Width);
            sb.Append(" x ");
            sb.Append(Height);
            return sb;
        }

        /// <summary>
        /// Display the original image size in the UI.
        /// </summary>
        /// <param name="Width">Width of the image.</param>
        /// <param name="Height">Height of the iamge.</param>
        private void DisplayOriginalSize(int Width, int Height)
        {
            OriginalDimensionsOut.Text = MakeDimensionalString(Width, Height).ToString();
        }

        /// <summary>
        /// Value used to control the visual rotational indicator.
        /// </summary>
        private RotateRightByDegrees CurrentRotation = RotateRightByDegrees.By0;

        /// <summary>
        /// Update the various angle indicators.
        /// </summary>
        /// <param name="ByDegrees">Angle update value.</param>
        private void UpdateAngles(int ByDegrees)
        {
            CurrentAngle += ByDegrees;
            if (CurrentAngle > 270)
                CurrentAngle = 0;
            switch (CurrentAngle)
            {
                case 0:
                    CurrentRotation = RotateRightByDegrees.By0;
                    break;

                case 90:
                    CurrentRotation = RotateRightByDegrees.RightBy90;
                    break;

                case 180:
                    CurrentRotation = RotateRightByDegrees.By180;
                    break;

                case 270:
                    CurrentRotation = RotateRightByDegrees.RightBy270;
                    break;

                default:
                    CurrentRotation = RotateRightByDegrees.By0;
                    break;
            }
        }

        private void ExecuteFilter(object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;

            int NewX = 0;
            int NewY = 0;
            ColorBlenderInterface.ReturnCode Result = ColorBlenderInterface.ReturnCode.NotSet;
            WriteableBitmap DB = null;
            if (AbsoluteRotationCheck.IsChecked.Value)
            {
                if (RotateBy90.IsChecked.Value)
                {
                }
                else
                {
                    if (RotateBy180.IsChecked.Value)
                    {
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                if (RelativeRotationCheck.IsChecked.Value)
                {
                    if (RotateRelative90CW.IsChecked.Value)
                    {
                        UpdateAngles(90);
                        if (CurrentAngle == 0 || CurrentAngle == 180)
                        {
                            NewX = OriginalWidth;
                            NewY = OriginalHeight;
                        }
                        else
                        {
                            NewX = OriginalHeight;
                            NewY = OriginalWidth;
                        }
                        DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
                           PixelFormats.Bgra32, null);
                        OK = CBI.RotateImageRightBy(Original, Original.PixelWidth, Original.PixelHeight,
                              DB, DB.PixelWidth, DB.PixelHeight, RotateRightByDegrees.RightBy90,
                              out Result);
                    }
                    else
                    {
                        UpdateAngles(270);
                        if (CurrentAngle == 0 || CurrentAngle == 180)
                        {
                            NewX = OriginalWidth;
                            NewY = OriginalHeight;
                        }
                        else
                        {
                            NewX = OriginalHeight;
                            NewY = OriginalWidth;
                        }
                        DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
                           PixelFormats.Bgra32, null);
                        OK = CBI.RotateImageRightBy(Original, Original.PixelWidth, Original.PixelHeight,
                              DB, DB.PixelWidth, DB.PixelHeight, RotateRightByDegrees.LeftBy90,
                              out Result);
                    }
                }
            }

            NewDimensionsOut.Text = MakeDimensionalString(NewX, NewY).ToString();
            /*
            WriteableBitmap DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);
            OK = CBI.RotateImageRightBy(Original, Original.PixelWidth, Original.PixelHeight,
                  DB, DB.PixelWidth, DB.PixelHeight, CurrentRotation,
                  out ColorBlenderInterface.ReturnCode Result);
                  */

            bool ShowGrid = ShowGridCheck.IsChecked.Value;
            WriteableBitmap GDB = null;
            if (ShowGrid)
            {
                GDB = new WriteableBitmap(DB.PixelWidth, DB.PixelHeight, Original.DpiX, Original.DpiY, PixelFormats.Bgra32, null);
                OK = CBI.OverlayBufferWithGrid(DB, GDB, DB.PixelWidth, DB.PixelHeight, 32, 32, Colors.Red);
            }

            if (OK)
            {
                if (ShowGrid)
                {
                    ImageSurface.Source = GDB;
                    ParentWindow.DrawHistogram(DB);
                }
                else
                {
                    ImageSurface.Source = DB;
                    ParentWindow.DrawHistogram(DB);
                }
                ParentWindow.SetMessage("OK");
                UpdateCurrentRotation();
            }
            else
            {
                string ErrorMessage = CBI.ErrorMessage((int)Result);
                StringBuilder sb = new StringBuilder();
                sb.Append(ErrorMessage);
                sb.Append(" (");
                sb.Append(Result.ToString());
                sb.Append(")");
                ParentWindow.SetMessage("Error", sb.ToString());
            }
        }

        /// <summary>
        /// Execute the filter.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ExecuteFilterOld(object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;

            int NewX = 0;
            int NewY = 0;
            RotateRightByDegrees RelativeRotation = RotateRightByDegrees.By0;
            if (RotateBy90.IsChecked.Value)
            {
                NewX = OriginalHeight;
                NewY = OriginalWidth;
                UpdateAngles(90);
                RelativeRotation = RotateRightByDegrees.RightBy90;
                if (CurrentRotation == RotateRightByDegrees.By0 || CurrentRotation == RotateRightByDegrees.By180)
                {
                    NewX = OriginalWidth;
                    NewY = OriginalHeight;
                }
            }
            if (RotateBy180.IsChecked.Value)
            {
                NewX = OriginalWidth;
                NewY = OriginalHeight;
                UpdateAngles(180);
                RelativeRotation = RotateRightByDegrees.By180;
                if (CurrentRotation == RotateRightByDegrees.RightBy90 || CurrentRotation == RotateRightByDegrees.RightBy270)
                {
                    NewX = OriginalHeight;
                    NewY = OriginalWidth;
                }
            }
            if (RotateBy270.IsChecked.Value)
            {
                NewX = OriginalHeight;
                NewY = OriginalWidth;
                UpdateAngles(270);
                RelativeRotation = RotateRightByDegrees.RightBy270;
                if (CurrentRotation == RotateRightByDegrees.By0 || CurrentRotation == RotateRightByDegrees.By180)
                {
                    NewX = OriginalWidth;
                    NewY = OriginalHeight;
                }
            }

            NewDimensionsOut.Text = MakeDimensionalString(NewX, NewY).ToString();

            WriteableBitmap DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);
            OK = CBI.RotateImageRightBy(Original, Original.PixelWidth, Original.PixelHeight,
                  DB, DB.PixelWidth, DB.PixelHeight, CurrentRotation,
                  out ColorBlenderInterface.ReturnCode Result);

#if false
#if false
            int Increment = 0;
            if (RotateBy90.IsChecked.Value)
                Increment = 1;
            if (RotateBy180.IsChecked.Value)
                Increment = 2;
            if (RotateBy270.IsChecked.Value)
                Increment = 3;
            OK = DoRotate(Increment, CurrentAngle, Original, out int EndingAngle, out WriteableBitmap DB,
                out ColorBlenderInterface.ReturnCode Result);
            if (OK)
                CurrentAngle = EndingAngle;
#else
            CurrentAngle += 90;
            if (CurrentAngle > 270)
                CurrentAngle = 0;

            int NewX = 0;
            int NewY = 0;
#if false
            NewX = OriginalHeight;
            NewY = OriginalWidth;
#else
            if (CurrentAngle == 0 || CurrentAngle == 180)
            {
                NewX = OriginalWidth;
                NewY = OriginalHeight;
            }
            if (CurrentAngle == 90 || CurrentAngle == 270)
            {
                NewX = OriginalHeight;
                NewY = OriginalWidth;
            }
#endif
            NewDimensionsOut.Text = MakeDimensionalString(NewX, NewY).ToString();

            WriteableBitmap DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
                 PixelFormats.Bgra32, null);

            OK = CBI.RotateImageRightBy270(Original, Original.PixelWidth, Original.PixelHeight,
                DB, DB.PixelWidth, DB.PixelHeight, out ColorBlenderInterface.ReturnCode Result);
#endif
#endif

            bool ShowGrid = ShowGridCheck.IsChecked.Value;
            WriteableBitmap GDB = null;
            if (ShowGrid)
            {
                GDB = new WriteableBitmap(DB.PixelWidth, DB.PixelHeight, Original.DpiX, Original.DpiY, PixelFormats.Bgra32, null);
                OK = CBI.OverlayBufferWithGrid(DB, GDB, DB.PixelWidth, DB.PixelHeight, 32, 32, Colors.Red);
            }

            if (OK)
            {
                if (ShowGrid)
                {
                    ImageSurface.Source = GDB;
                    ParentWindow.DrawHistogram(DB);
                }
                else
                {
                    ImageSurface.Source = DB;
                    ParentWindow.DrawHistogram(DB);
                }
                ParentWindow.SetMessage("OK");
                UpdateCurrentRotation();
            }
            else
            {
                string ErrorMessage = CBI.ErrorMessage((int)Result);
                StringBuilder sb = new StringBuilder();
                sb.Append(ErrorMessage);
                sb.Append(" (");
                sb.Append(Result.ToString());
                sb.Append(")");
                ParentWindow.SetMessage("Error", sb.ToString());
            }
        }

        private bool DoRotate(int Increments, int StartingAngle, WriteableBitmap Original,
            out int EndingAngle, out WriteableBitmap DB, out ColorBlenderInterface.ReturnCode Result)
        {
            EndingAngle = 0;
            DB = null;
            Result = ColorBlenderInterface.ReturnCode.NotSet;

            if (Increments < 1)
                return false;
            EndingAngle = StartingAngle;

            for (int i = 0; i < Increments; i++)
            {
                EndingAngle += 90;
                int NewX = 0;
                int NewY = 0;
                if (CurrentAngle == 0 || CurrentAngle == 180)
                {
                    NewX = OriginalWidth;
                    NewY = OriginalHeight;
                }
                if (CurrentAngle == 90 || CurrentAngle == 270)
                {
                    NewX = OriginalHeight;
                    NewY = OriginalWidth;
                }
                NewDimensionsOut.Text = MakeDimensionalString(NewX, NewY).ToString();

                DB = new WriteableBitmap(NewX, NewY, Original.DpiX, Original.DpiY,
                   PixelFormats.Bgra32, null);

                bool OK = CBI.RotateImageRightBy90(Original, Original.PixelWidth, Original.PixelHeight,
                  DB, DB.PixelWidth, DB.PixelHeight, out Result);
                if (!OK)
                    return false;
            }

            return true;
        }
    }
}
