﻿using System;
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

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for MiscellaneousConvolutionPage.xaml
    /// </summary>
    public partial class MiscellaneousConvolutionPage : Page, IFilterPage
    {
        public MiscellaneousConvolutionPage () : base()
        {
            InitializeComponent();
        }

        public void Clear ()
        {
            Original = null;
        }

        public StageBase EmitPipelineStage ()
        {
            return null;
        }

        public MiscellaneousConvolutionPage (ImageMan ParentWindow, ColorBlenderInterface CBI, Image ImageSurface) : base()
        {
            InitializeComponent();
            this.ParentWindow = ParentWindow;
            this.CBI = CBI;
            this.ImageSurface = ImageSurface;
            Original = null;// new WriteableBitmap((BitmapSource)ImageSurface.Source);
        }

        private ColorBlenderInterface CBI;
        public WriteableBitmap Original = null;
        private Image ImageSurface;
        public ImageMan ParentWindow = null;

        private void ResetLocalImage (object Sender, RoutedEventArgs e)
        {
            if (ParentWindow == null)
                return;
            ImageSurface.Source = ParentWindow.GetImage();
            ParentWindow.DrawHistogram(ParentWindow.GetImage());
        }

        private KernelTypes GetMiscellaneousType()
        {
            if (Identity.IsChecked.Value)
                return KernelTypes.Identity;
            return KernelTypes.Identity;
        }

        double GetInputDouble (TextBox Source)
        {
            if (Source == null)
                throw new ArgumentNullException("Source");
            string SourceValue = Source.Text;
            double Final = 0.0;
            bool OK = double.TryParse(SourceValue, out Final);
            if (!OK)
                return 0.0;
            return Final;
        }

        private void ExecuteFilter (object sender, RoutedEventArgs e)
        {
            if (!ParentWindow.ImageAvailable)
                return;
            Original = new WriteableBitmap((BitmapSource)ParentWindow.GetImageControl().Source);
            WriteableBitmap DB = new WriteableBitmap(Original.PixelWidth, Original.PixelHeight, Original.DpiX, Original.DpiY,
              PixelFormats.Bgra32, null);
            KernelTypes MiscellaneousType = GetMiscellaneousType();
            double LuminanceValue = GetInputDouble(LuminanceThresholdInput);
            bool SkipTransparent = SkipTransparentPix.IsChecked.Value;
            bool UseLuminance = UseLuminanceThresholdCheck.IsChecked.Value;
            bool IncludeTransparent = UseTransparentPix.IsChecked.Value;

            bool OK = CBI.ImageKernelConvolution2(Original, DB, Original.PixelWidth, Original.PixelHeight, Original.BackBufferStride,
                MiscellaneousType, SkipTransparent, IncludeTransparent, UseLuminance, LuminanceValue);
            if (OK)
            {
                ImageSurface.Source = DB;
                ParentWindow.DrawHistogram(DB);
                ParentWindow.SetMessage("OK");
            }
            else
                ParentWindow.SetMessage("Error");
        }
    }
}
