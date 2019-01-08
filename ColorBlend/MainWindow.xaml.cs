using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent();
#if true
            InitializePoints();
#else
            PurePoints = Configurator.StandardPoints();
#endif
            MotionPoint.Text = "";
            DoReset();
            SetStatus("");
            //            SurfaceContainer.Background = GridBrush(GridBrushTypes.Blueprint, 25, 25);
            SurfaceContainer.Background = GridBrush(GridBrushTypes.GridPaper, 16, 16);
            //            SurfaceContainer.Background = CheckerboardPatternBrush(16, 16, new SolidColorBrush(Colors.WhiteSmoke),
            //                new SolidColorBrush(Colors.LightGray));
        }

        private void WindowLoaded (object Sender, RoutedEventArgs e)
        {
            ColorsInitialized = true;
            Renderer();
        }

        private void InitializePoints ()
        {
            ColorPoint InitialPoint = new ColorPoint
            {
                Name = "Initial",
                PointColor = Colors.Red
            };
            InitialPoint.RelativePoint.X = 0.5;
            InitialPoint.RelativePoint.Y = 0.5;
            InitialPoint.UseRadius = true;
            InitialPoint.Radius = 50.0;
            InitialPoint.AbsolutePoint = new PointEx(100, 100);
            InitialPoint.Top = 100.0;
            InitialPoint.Left = 100.0;
            InitialPoint.Width = 100.0;
            InitialPoint.Height = 100.0;
            InitialPoint.UseAbsoluteOnly = true;
            InitialPoint.UseAlpha = true;
            InitialPoint.AlphaStart = 1.0;
            InitialPoint.AlphaEnd = 0.0;
            PurePoints = new List<ColorPoint>();
            PurePoints.Add(InitialPoint);
        }

        public List<ColorPoint> PurePoints = new List<ColorPoint>();

        //http://stackoverflow.com/questions/11928884/writing-a-2d-array-of-colors-to-a-wpf-image
        //http://stackoverflow.com/questions/10309044/how-to-calculate-gradient-color-by-percent
        //http://stackoverflow.com/questions/27532/generating-gradients-programmatically
        //http://stackoverflow.com/questions/25007/conditional-formatting-percentage-to-color-conversion
        //http://gamedev.stackexchange.com/questions/69263/how-do-you-calculate-a-gradient-value-within-multiple-colors-gradient-stops

        double Distance (int X1, int Y1, int X2, int Y2)
        {
            return Math.Sqrt(Math.Pow((X1 - X2), 2) + Math.Pow((Y1 - Y2), 2));
        }

        private void MenuRender (object Sender, RoutedEventArgs e)
        {
            ColorsInitialized = true;
            Renderer();
        }

        private void MenuReset (object Sender, RoutedEventArgs e)
        {
            ColorsInitialized = false;
            InitializePoints();
            SurfaceDestination.Source = null;
        }

        private void MenuExit (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private int GetPointCount ()
        {
            PointCount = 0;
            foreach (ColorPoint CP in PurePoints)
                PointCount += CP.Enabled ? 1 : 0;
            return PointCount;
        }

        private List<Color> ColorSet = new List<Color>();
        private int PointCount = 1;
        private bool ColorsInitialized = false;

        private void WindowResized (object sender, SizeChangedEventArgs e)
        {
            Renderer();
        }

        void SetStatus (string Message)
        {
            StatusText.Text = Message;
        }

        void AppendStatus (string Message)
        {
            string OldMessage = StatusText.Text;
            StatusText.Text = OldMessage + Message;
        }

        private RandomWalker RandomWalk;

        private void HandleMotionToggleChange (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            ToggleButton TB = Sender as ToggleButton;
            if (TB == null)
                return;
#if true
            if (TB.IsChecked.Value)
            {
                RandomWalk = new RandomWalker(GlobalRand, (int)SurfaceContainer.ActualWidth, (int)SurfaceContainer.ActualHeight);
                RandomWalk.NewPointAvailableEvent += NewRandomWalkPoint;
                PointEx Start = PointEx.RandomPoint(GlobalRand, 0, (int)SurfaceContainer.ActualWidth,
                    0, (int)SurfaceContainer.ActualHeight);
                PointEx End = PointEx.RandomPoint(GlobalRand, 0, (int)SurfaceContainer.ActualWidth,
                    0, (int)SurfaceContainer.ActualHeight);
                PurePoints[0].AbsoluteMove(Start.IntX, Start.IntY);
                RandomWalk.StartWalking(Start, End, 100, 0.05);
            }
            else
            {
                if (RandomWalk != null)
                    RandomWalk.StopWalking();
                RandomWalk = null;
            }
#else
            if (TB.IsChecked.Value)
            {
                PointMover = new Meander(SurfaceContainer, PurePoints);
                PointMover.MeanderPositionChangeEvent += PointMover_MeanderPositionChangeEvent;
                PointMover.MeanderMotionStateChangeEvent += PointMover_MeanderMotionStateChangeEvent;
                PointMover.DoStartMotion();
            }
            else
            {
                if (PointMover != null)
                    PointMover.DoEndMotion();
                PointMover = null;
            }
#endif
        }

        private void NewRandomWalkPoint (object Sender, NewPointAvailableEventArgs e)
        {
            PurePoints.Last().AbsoluteMove(e.NewPoint.IntX, e.NewPoint.IntY);
            AddDebugPoint(e.NewPoint.IntX, e.NewPoint.IntY);
            Renderer();
        }

        private void AddDebugPoint (int X, int Y)
        {
            ColorPoint CP = new ColorPoint();
            CP.AbsoluteMove(X, Y);
            CP.Radius = 5.0;
            CP.Width = 5;
            CP.Height = 5;
            CP.PointColor = Colors.Red;
            CP.UseRadius = true;
            CP.AlphaStart = 1.0;
            CP.AlphaEnd = 1.0;
            PurePoints.Insert(0, CP);
        }

        void PointMover_MeanderMotionStateChangeEvent (object Sender, MeanderMotionStateChangedEventArgs e)
        {
            switch (e.State)
            {
                case MotionEventStates.MotionStarted:
                    break;

                case MotionEventStates.MotionEnded:
                    MotionButton.IsChecked = false;
                    break;

                case MotionEventStates.Removed:
                    break;
            }
        }

        void PointMover_MeanderPositionChangeEvent (object Sender, MeanderPositionChangeEventArgs e)
        {
            MotionPoint.Text = e.PointText;
            Renderer();
        }

        private Meander PointMover = null;

        DispatcherTimer AutoTimer = null;

        private void HandleContinuousTestToggleChange (object sender, RoutedEventArgs e)
        {
            ColorsInitialized = true;
            ToggleButton TB = sender as ToggleButton;
            if (TB == null)
                return;
            if (TB.IsChecked.Value)
            {
                if (TestContinuous.IsChecked.Value)
                {
                    TestTimer.Stop();
                    TestTimer.IsEnabled = false;
                    TestTimer = null;
                    TestContinuous.IsChecked = false;
                }
                AutoTimer = new DispatcherTimer();
                AutoTimer.Tick += AutoTimer_Tick;
                AutoTimer.Interval = new TimeSpan(0, 0, 1);
                AutoTimer.IsEnabled = true;
                AutoTimer.Start();
            }
            else
            {
                if (AutoTimer != null)
                {
                    AutoTimer.Stop();
                    AutoTimer.IsEnabled = false;
                    AutoTimer = null;
                }
            }
        }

        private void AutoTimer_Tick (object sender, EventArgs e)
        {
            Random Rand = new Random();
            PointCount = Rand.Next(1, 5);
            ColorSet.Clear();
            for (int i = 0; i < 5; i++)
            {
                ColorSet.Add(RandomColor(Rand));
            }
            DoRandomizePoints();
            Renderer();
        }

        DispatcherTimer TestTimer = null;

        private void SameContinuous (object sender, RoutedEventArgs e)
        {
            ToggleButton TB = sender as ToggleButton;
            if (TB == null)
                return;
            ColorsInitialized = true;
            if (TB.IsChecked.Value)
            {
                if (NormalContinuous.IsChecked.Value)
                {
                    AutoTimer.Stop();
                    AutoTimer.IsEnabled = false;
                    AutoTimer = null;
                    NormalContinuous.IsChecked = false;
                }
                TestTimer = new DispatcherTimer();
                TestTimer.Tick += TestTimer_Tick;
                string RawDuration = DurationIn.Text;
                int MS = MakeMS(RawDuration);
                TestTimer.Interval = TimeSpan.FromMilliseconds(MS);
                TestTimer.IsEnabled = true;
                TestTimer.Start();
            }
            else
            {
                if (TestTimer == null)
                    return;
                TestTimer.Stop();
                TestTimer.IsEnabled = false;
                TestTimer = null;
            }
        }

        private void DoRandomizePoints ()
        {
            Random R = new Random();
            foreach (ColorPoint CP in PurePoints)
                CP.DoRandomize(R);
        }

        private void TestTimer_Tick (object sender, EventArgs e)
        {
            DoRandomizePoints();
            Renderer();
        }

        public Color RandomColor (Random Rand, byte LowValue = 0xa0, byte HighValue = 0xff, bool IncludeAlpha = false)
        {
            Color Final = Colors.Transparent;
            if (IncludeAlpha)
                Final = Color.FromArgb((byte)(Rand.Next(LowValue, HighValue)),
                                      (byte)(Rand.Next(LowValue, HighValue)),
                                      (byte)(Rand.Next(LowValue, HighValue)),
                                      (byte)(Rand.Next(LowValue, HighValue)));
            else
                Final = Color.FromRgb((byte)(Rand.Next(LowValue, HighValue)),
                                      (byte)(Rand.Next(LowValue, HighValue)),
                                      (byte)(Rand.Next(LowValue, HighValue)));
            return Final;
        }

        void DoMakeMousePoint (Point ClickPoint)
        {
#if false
            ColorSet.Add(ColorSet[0]);
            double pX = ClickPoint.X / SurfaceContainer.ActualWidth;
            double pY = ClickPoint.Y / SurfaceContainer.ActualHeight;
            ColorsInitialized = true;
            Renderer();
#endif
        }

        private void HandleMouseClickInImageContainer (object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                return;
            Border B = sender as Border;
            Point NewPoint = e.GetPosition(B);
            DoMakeMousePoint(NewPoint);
        }

        private void HandleRightButtonDownInTarget (object Sender, MouseButtonEventArgs e)
        {
            if (!IsInInteractive)
                return;
            Border B = Sender as Border;
            if (B == null)
                return;
            Point MousePoint = e.GetPosition(B);
            int Index = ((int)MousePoint.Y * Stride) + ((int)MousePoint.X * 4);
        }

        void DoMakeInteractiveMousePoint (Point ClickPoint)
        {
#if true
            PurePoints[0].AbsoluteMove((int)ClickPoint.X, (int)ClickPoint.Y, true);
#else
            double pX = ClickPoint.X / SurfaceContainer.ActualWidth;
            double pY = ClickPoint.Y / SurfaceContainer.ActualHeight;
            PurePoints[0].RelativePoint.X = pX;
            PurePoints[0].RelativePoint.Y = pY;
            PurePoints[0].CreateAbsolutePoint(FinalWidth, FinalHeight);
#endif
            ColorsInitialized = true;
            Renderer();
        }

        private void HandleMouseMoveInTarget (object sender, MouseEventArgs e)
        {
            Border B = sender as Border;
            if (B == null)
                return;
            if (GlobalPixelMap == null)
                return;

            Point MousePoint = e.GetPosition(B);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (PointCount < 1)
                    return;
                DoMakeInteractiveMousePoint(MousePoint);
                return;
            }

            string CursorLoc = "(" + ((int)MousePoint.X).ToString() + "," + ((int)MousePoint.Y).ToString() + ")";
            CursorPosOut.Text = CursorLoc;
            int Index = ((int)MousePoint.Y * GlobalPixelMapStride) + ((int)MousePoint.X * 4);
            Color TheColor = GlobalPixelMap.PixelGet(Index);
            ColorValueOut.Text = TheColor.ToHexColor();
            double mpdist = Distance((int)PurePoints[0].AbsolutePoint.X, (int)PurePoints[0].AbsolutePoint.Y, (int)MousePoint.X, (int)MousePoint.Y);
            PointDistance.Text = mpdist.ToString("F2");
            ColorUnderMouse.Background = new SolidColorBrush(TheColor);
        }

        bool IsInInteractive = false;

        private void RunConfiguratorHandle (object sender, RoutedEventArgs e)
        {
            Configurator CFG = new Configurator();
            CFG.Start(ref PurePoints);
            CFG.ShowDialog();
        }

        private void HandleSaveButtonClick (object Sender, RoutedEventArgs e)
        {
            SaveDialog SD = new SaveDialog();
            SaveRecord SR = new SaveRecord
            {
                Height = FinalHeight,
                Width = FinalWidth,
                DPIX = 96.0,
                DPIY = 96.0
            };
            SD.SaveAs = SR;
            SD.ShowDialog();
            SR = SD.SaveAs;
            DoSave(SR);
        }

        private void DoSave (SaveRecord SaveAs)
        {
            if (SaveAs == null)
                return;
            if (!SaveAs.IsValid())
                return;
        }

        private void RunCropTest (object sender, RoutedEventArgs e)
        {
            if (PurePoints.Count < 1)
                return;
            PurePoints[0].AbsoluteMove(-25, 140, true);
            //            PurePoints[0].AbsoluteMove(0, 0, true);
            //            PurePoints[0].AbsoluteMove((int)(SurfaceContainer.ActualWidth - 50), 200, true);
            Renderer();
        }

        private void DoReset ()
        {
            CumulativeMS = 0;
            TotalBlockCount = 0;
            if (AutoTimer != null)
            {
                NormalContinuous.IsChecked = false;
                AutoTimer.Stop();
                AutoTimer.IsEnabled = false;
                AutoTimer = null;
            }
            if (TestTimer != null)
            {
                TestContinuous.IsChecked = false;
                TestTimer.Stop();
                TestTimer.IsEnabled = false;
                TestTimer = null;
            }
            if (PointMover != null)
            {
                PointMover.DoEndMotion();
                MotionButton.IsChecked = false;
                PointMover = null;
            }
            SetStatus("Parameters reset.");
        }

        int MakeMS (string RawMS)
        {
            int Final = 0;
            if (int.TryParse(RawMS, out Final))
                return Final;
            DurationIn.Text = "500";
            return 500;
        }

        private void HandleStepMoveClick (object Sender, RoutedEventArgs e)
        {
            if (PointMover == null)
            {
                PointMover = new Meander(SurfaceContainer, PurePoints, 0, true);
                PointMover.MeanderPositionChangeEvent += PointMover_MeanderPositionChangeEvent;
                PointMover.DoStartMotion();
                PointMover.DoStep();
            }
            else
            {
                if (PointMover.InStepMode)
                    PointMover.DoStep();
            }
        }

        private void HandleBlendTestButtonClick (object sender, RoutedEventArgs e)
        {
            DoReset();
            Random Rand = new Random();
            DrawRandomBlocks(Rand.Next(2, 10), Rand);
        }

        public void ToDebugOut (string Message, bool ShowSeparator = true)
        {
            if (!DebugOutputEnabled)
                return;
            if (ShowSeparator)
                DebugOut.Items.Insert(0, new Separator());
            DebugOut.Items.Insert(0, Message);
        }

        public void ToDebugOut (object Something, bool ShowSeparator = true)
        {
            if (!DebugOutputEnabled)
                return;
            if (ShowSeparator)
                DebugOut.Items.Insert(0, new Separator());
            DebugOut.Items.Insert(0, Something);
        }

        public void ToDebugOutObject (List<string> Messages, bool ShowSeparator = true)
        {
            if (!DebugOutputEnabled)
                return;
            if (ShowSeparator)
                DebugOut.Items.Insert(0, new Separator());
            foreach (string Message in Messages)
                DebugOut.Items.Insert(0, Message);
        }

        public void ToDebugOutObjectList (List<UIElement> Objects, bool ShowSeparator = true)
        {
            if (!DebugOutputEnabled)
                return;
            if (ShowSeparator)
                DebugOut.Items.Insert(0, new Separator());
            foreach (object Something in Objects)
                DebugOut.Items.Insert(0, Something);
        }

        DispatcherTimer BlendTimer = null;
        private void HandleContinuousBlendCheck (object Sender, RoutedEventArgs e)
        {
            ToggleButton TB = Sender as ToggleButton;
            if (TB == null)
                return;
            ColorsInitialized = true;
            if (TB.IsChecked.Value)
            {
                CumulativeMS = 0;
                TotalBlockCount = 0;
                BlendTimer = new DispatcherTimer();
                BlendTimer.Tick += BlendTimer_Tick;
                BlendTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                BlendTimer.IsEnabled = true;
                BlendTimer.Start();
            }
            else
            {
                if (BlendTimer != null)
                {
                    BlendTimer.Stop();
                    BlendTimer.IsEnabled = false;
                    BlendTimer = null;
                }
            }
        }

        void BlendTimer_Tick (object sender, EventArgs e)
        {
            DrawRandomBlocks(GlobalRand.Next(1, 20), GlobalRand);
        }

        public Random GlobalRand = new Random();

        private void TestBG_Click (object sender, RoutedEventArgs e)
        {
            Color BGColor = RandomColor(GlobalRand);
            Color FGColor = RandomColor(GlobalRand, 0xa0, 0xff);
            int GridDimensions = GlobalRand.Next(4, 32);
            TestBackgroundDrawing(BGColor, FGColor, GridDimensions, GridDimensions);
        }

        private void EnableDebugOutputCheck_Checked (object Sender, RoutedEventArgs e)
        {
            CheckBox CB = Sender as CheckBox;
            if (CB == null)
                return;
            if (CB.IsChecked.HasValue)
                DebugOutputEnabled = CB.IsChecked.Value;
        }

        bool DebugOutputEnabled = false;

        private void DrawLineButton_Click (object sender, RoutedEventArgs e)
        {
            PointEx Point1 = new PointEx(GlobalRand.Next(0, (int)(SurfaceContainer.ActualWidth)), GlobalRand.Next(0, (int)(SurfaceContainer.ActualHeight)));
            PointEx Point2 = new PointEx(GlobalRand.Next(0, (int)(SurfaceContainer.ActualWidth)), GlobalRand.Next(0, (int)(SurfaceContainer.ActualHeight)));
            Color LineColor = RandomColor(GlobalRand, 0x0, 0xa0);
            DrawLine2(Point1, Point2, LineColor, false, 1);
        }

        DispatcherTimer ManyBlobTimer = null;

        private void TestManyBlobs (object Sender, RoutedEventArgs e)
        {
            ToggleButton TB = Sender as ToggleButton;
            if (TB == null)
                return;
            ColorsInitialized = true;
            if (TB.IsChecked.Value)
            {
                ManyBlobTimer = new DispatcherTimer();
                ManyBlobTimer.Tick += ManyBlobTimer_Tick;
                ManyBlobTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                ManyBlobTimer.IsEnabled = true;
                ManyBlobTimer.Start();
            }
            else
            {
                if (ManyBlobTimer != null)
                {
                    ManyBlobTimer.Stop();
                    ManyBlobTimer.IsEnabled = false;
                    ManyBlobTimer = null;
                }
            }
        }

        private void ManyBlobTimer_Tick (object sender, EventArgs e)
        {
            PurePoints.Clear();
            int Count = GlobalRand.Next(1, 50);
            for (int i = 0; i < Count; i++)
            {
                ColorPoint CP = new ColorPoint
                {
#if true
                    AlphaStart = 1.0,
                    AlphaEnd = 0.0,
#else
                if (GlobalRand.PercentGreaterThan(0.6))
                    CP.AlphaEnd = GlobalRand.NormalizedNext();
                else
                    CP.AlphaEnd = 0.0;
                if (GlobalRand.PercentGreaterThan(0.9))
                    CP.AlphaStart = GlobalRand.NormalizedNext();
                else
                    CP.AlphaStart = 1.0;
#endif
                    UseAlpha = true,
                    UseAbsoluteOnly = true,
                    PointColor = RandomColor(GlobalRand, 0x0a0, 0xff, false),
                    Width = GlobalRand.NextEven(64, 200)
                };
                CP.Height = CP.Width;
                int Left = GlobalRand.Next(0, (int)SurfaceContainer.ActualWidth);
                int Top = GlobalRand.Next(0, (int)SurfaceContainer.ActualHeight);
                CP.AbsolutePoint = new PointEx(Left, Top);
                PurePoints.Add(CP);
            }
            Renderer();
        }

#if true
        private void PointOverLapCheck (object Sender, EventArgs e)
        {
            ColorsInitialized = true;
            PurePoints.Clear();
            ColorPoint CP1 = new ColorPoint
            {
                AlphaEnd = 0.0,
                AlphaStart = 1.0,
                UseAlpha = true,
                UseAbsoluteOnly = true,
                PointColor = Colors.Red,
                Width = 150,
                Height = 150,
                AbsolutePoint = new PointEx(150, 150)
            };
            PurePoints.Add(CP1);
            ColorPoint CP2 = new ColorPoint
            {
                AlphaEnd = 1.0,
                AlphaStart = 1.0,
                UseAlpha = true,
                UseAbsoluteOnly = true,
                PointColor = Colors.Green,
                Width = 150,
                Height = 150,
                AbsolutePoint = new PointEx(250, 150)
            };
            PurePoints.Add(CP2);
            Renderer();
        }
#endif

        private void RunColorConverter (object Sender, RoutedEventArgs e)
        {
            ColorConversionTest CCT = new ColorConversionTest();
            CCT.ShowDialog();
            //ColorConverter CC = new ColorConverter();
            //CC.ShowDialog2();
        }

        //https://social.msdn.microsoft.com/forums/vstudio/en-US/633b9bb0-c3cb-4ab2-aff3-df48065a14f4/how-to-make-a-drop-down-menu-in-wpf
        private void HandleCropButtonClick (object Sender, RoutedEventArgs e)
        {
            (Sender as Button).ContextMenu.IsEnabled = true;
            (Sender as Button).ContextMenu.PlacementTarget = (Sender as Button);
            (Sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (Sender as Button).ContextMenu.IsOpen = true;
        }

        private void CropLeftSide (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            DoReset();
            if (PurePoints.Count < 1)
                return;
            double WindowWidth = SurfaceContainer.ActualWidth;
            double WindowHeight = SurfaceContainer.ActualHeight;
            PurePoints[0].AbsoluteMove((int)(-PurePoints[0].Radius), (int)(WindowHeight / 2.0), true);
            Renderer();
        }

        private void CropRightSide (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            DoReset();
            if (PurePoints.Count < 1)
                return;
            double WindowWidth = SurfaceContainer.ActualWidth;
            double WindowHeight = SurfaceContainer.ActualHeight;
            PurePoints[0].AbsoluteMove((int)(WindowWidth - PurePoints[0].Radius), (int)(WindowHeight / 2.0), true);
            Renderer();
        }

        private void CropTopSide (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            DoReset();
            if (PurePoints.Count < 1)
                return;
            double WindowWidth = SurfaceContainer.ActualWidth;
            double WindowHeight = SurfaceContainer.ActualHeight;
            PurePoints[0].AbsoluteMove((int)(WindowWidth / 2.0), (int)(-PurePoints[0].Radius), true);
            Renderer();
        }

        private void CropBottomSide (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            DoReset();
            if (PurePoints.Count < 1)
                return;
            double WindowWidth = SurfaceContainer.ActualWidth;
            double WindowHeight = SurfaceContainer.ActualHeight;
            PurePoints[0].AbsoluteMove((int)(WindowWidth / 2.0), (int)(WindowHeight - PurePoints[0].Radius), true);
            Renderer();
        }

        private void HandleObjectBoundryClick (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            ToggleButton TB = Sender as ToggleButton;
            if (TB == null)
                return;
            BoundryColor = TB.IsChecked.Value ? Colors.Red : Colors.Transparent;
            Renderer();
        }

        private void HandlePictureBackgroundToggle (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            ToggleButton TB = Sender as ToggleButton;
            if (TB == null)
                return;
            ShowPictureBackground = TB.IsChecked.Value;
            Renderer();
        }

        private void GradientBlockToggle (object Sender, RoutedEventArgs e)
        {
            if (!ColorsInitialized)
                return;
            ToggleButton TB = Sender as ToggleButton;
            if (TB == null)
                return;
            UseGradientBlocks = TB.IsChecked.Value;
        }

        bool UseGradientBlocks = false;

        private bool ShowPictureBackground = false;
        private Image BackgroundPicture = null;

        private void HistogramClick (object Sender, RoutedEventArgs e)
        {
            HistogramWindow HW = new HistogramWindow();
            HW.ShowDialog();
        }

        private void BayesClick(object Sender, RoutedEventArgs e)
        {
            BayerDecoderWindow BDW = new BayerDecoderWindow();
            BDW.ShowDialog();
        }

        private void GenerateRandomColors (object Sender, RoutedEventArgs e)
        {
            GenerativeTest.CreateRandomColorImage(SurfaceDestination);
        }

        private void GenerateVerticalColorRamp (object Sender, RoutedEventArgs e)
        {
            GenerativeTest.CreateGradientColorImage(SurfaceDestination, false);
        }

        private void GenerateHorizontalColorRamp (object Sender, RoutedEventArgs e)
        {
            GenerativeTest.CreateGradientColorImage(SurfaceDestination, true);
        }

        private void GenerateRampingColors (object Sender, RoutedEventArgs e)
        {
            GenerativeTest.CreateRampingColorImage(SurfaceDestination);
        }

        private void ColorChannelShifting (object Sender, RoutedEventArgs e)
        {
        }

        private void GenerateVerticalGradients (object Sender, RoutedEventArgs e)
        {
            GenerativeTest.CreateGradientImage(SurfaceDestination, false, GlobalRand);
        }

        private void GenerateHorizontalGradients (object Sender, RoutedEventArgs e)
        {
            GenerativeTest.CreateGradientImage(SurfaceDestination, true, GlobalRand);
        }

        private void RunImageManipulationWindow (object Sender, RoutedEventArgs e)
        {
            ImageMan IM = new ImageMan();
            IM.ShowDialog();
        }

        private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (sender == null)
            //    return;
        }

        private void RunImagerMerger (object Sender, RoutedEventArgs e)
        {
            ImageMerger IM = new ImageMerger();
            IM.ShowDialog();
        }

        private void RunPrettyImageRenderer (object Sender, RoutedEventArgs e)
        {
            PrettyImageRenderer PIR = new PrettyImageRenderer(this,new Random());
            PIR.ShowDialog();
        }
    }
}
