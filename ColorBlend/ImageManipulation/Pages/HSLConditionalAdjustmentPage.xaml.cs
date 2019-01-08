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
    /// Interaction logic for HSLConditionalAdjustmentPage.xaml
    /// </summary>
    public partial class HSLConditionalAdjustmentPage : Page, IFilterPage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HSLConditionalAdjustmentPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ParentWindow">Reference to the parent window.</param>
        /// <param name="CBI">Color blender interface.</param>
        /// <param name="ImageSurface">The image surface.</param>
        public HSLConditionalAdjustmentPage(ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            Contract.Assert(ParentWindow != null);
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;
        }

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
        }



        /// <summary>
        /// Execute the filter.
        /// </summary>
        /// <param name="Sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ExecuteFilter(object Sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            bool OK = false;
            ColorBlenderInterface.ReturnCode Result = ColorBlenderInterface.ReturnCode.NotSet;
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);

            List<ColorBlenderInterface.ConditionalHSLAdjustment> Conditions = new List<ColorBlenderInterface.ConditionalHSLAdjustment>();
            foreach (HSLConditionalSheet HCS in HSLConditionalListBox.Items)
            {
                if (HCS == null)
                    continue;
                Conditions.Add(HCS.LocalConditions);
            }
            if (Conditions.Count < 1)
                return;

            OK = CBI.ModifyHSLConditionally(Original, Original.PixelWidth, Original.PixelHeight, DB,
                Conditions, out Result);

            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
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

        private void HandleConditionalButton(object Sender, RoutedEventArgs e)
        {
            Button B = Sender as Button;
            if (B == null)
                return;
            string ButtonName = B.Name;
            if (string.IsNullOrEmpty(ButtonName))
                return;
            switch (ButtonName)
            {
                case "AddHSLConditionButton":
                    HSLConditionsEditor HCE = new HSLConditionsEditor();
                    HCE.ShowDialog();
                    if (HCE.OKClicked)
                    {
                        ColorBlenderInterface.ConditionalHSLAdjustment NewCondition = HCE.NewCondition;
                        HSLConditionalSheet HCS = new HSLConditionalSheet(NewCondition);
                        HSLConditionalListBox.Items.Add(HCS);
                    }
                    break;

                case "EditHSLConditionButton":
                    int EditCurrentItem = HSLConditionalListBox.SelectedIndex;
                    if (EditCurrentItem < 0)
                        break;
                    HSLConditionalSheet EHCS = HSLConditionalListBox.SelectedItem as HSLConditionalSheet;
                    if (EHCS != null)
                    {
                        HSLConditionsEditor EHCE = new HSLConditionsEditor(EHCS.LocalConditions);
                        EHCE.ShowDialog();
                        if (EHCE.OKClicked)
                        {
                            EHCS.SetValues(EHCE.NewCondition);
                        }
                    }
                    break;

                case "DeleteHSLConditionButton":
                    int CurrentItem = HSLConditionalListBox.SelectedIndex;
                    if (CurrentItem >= 0)
                        HSLConditionalListBox.Items.RemoveAt(CurrentItem);
                    break;

                case "ClearHSLConditionsButton":
                    HSLConditionalListBox.Items.Clear();
                    break;
            }
        }
    }
}
