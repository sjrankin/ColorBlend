﻿#pragma checksum "..\..\..\ImageManipulation\ImageMan.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "973E11D2D098C738CBD03D9C7552F5B918CD8C84"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ColorBlend;
using HistogramDisplay;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ColorBlend {
    
    
    /// <summary>
    /// ImageMan
    /// </summary>
    public partial class ImageMan : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HeaderTitle;
        
        #line default
        #line hidden
        
        
        #line 341 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseImagesList;
        
        #line default
        #line hidden
        
        
        #line 355 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChangedFileSuffix;
        
        #line default
        #line hidden
        
        
        #line 381 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ImagesList;
        
        #line default
        #line hidden
        
        
        #line 419 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ColorBlend.ImageFrame ImageSurface;
        
        #line default
        #line hidden
        
        
        #line 449 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox FitToSize;
        
        #line default
        #line hidden
        
        
        #line 459 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock StatusBlock;
        
        #line default
        #line hidden
        
        
        #line 476 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MousePositionOverImage;
        
        #line default
        #line hidden
        
        
        #line 491 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ColorUnderMouseBlock;
        
        #line default
        #line hidden
        
        
        #line 501 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem HexContextMenu;
        
        #line default
        #line hidden
        
        
        #line 505 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem RGBContextMenu;
        
        #line default
        #line hidden
        
        
        #line 509 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem HSLContextMenu;
        
        #line default
        #line hidden
        
        
        #line 513 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CMYKContextMenu;
        
        #line default
        #line hidden
        
        
        #line 522 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ColorUnderMouseSample;
        
        #line default
        #line hidden
        
        
        #line 531 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ImageInfo;
        
        #line default
        #line hidden
        
        
        #line 557 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame ControlsFrame;
        
        #line default
        #line hidden
        
        
        #line 570 "..\..\..\ImageManipulation\ImageMan.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HistogramDisplay.HistogramViewer HDisplay;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/imageman.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ImageManipulation\ImageMan.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.HeaderTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            
            #line 52 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenImageFile);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 54 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveImage);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 56 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveImageAs);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 60 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetImage);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 63 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearImage);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleCloseMenuClick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 71 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 74 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 77 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 80 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 83 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 87 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 91 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 96 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 101 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 104 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 109 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 112 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 115 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 118 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 122 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 125 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 128 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 131 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 134 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 138 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 141 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 145 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 148 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 152 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 161 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleColorConversions);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 166 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 169 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 172 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 177 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 180 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 183 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 188 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 191 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 196 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 200 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 203 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 207 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 210 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 215 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 218 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 220 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 49:
            
            #line 224 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 227 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 230 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 233 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 53:
            
            #line 236 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 54:
            
            #line 240 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 55:
            
            #line 245 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 56:
            
            #line 248 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 57:
            
            #line 251 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 58:
            
            #line 256 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 59:
            
            #line 261 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 60:
            
            #line 266 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleMenuClick);
            
            #line default
            #line hidden
            return;
            case 61:
            this.UseImagesList = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 62:
            this.ChangedFileSuffix = ((System.Windows.Controls.TextBox)(target));
            return;
            case 63:
            
            #line 367 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleClearImagesList);
            
            #line default
            #line hidden
            return;
            case 64:
            
            #line 374 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleLoadImagesList);
            
            #line default
            #line hidden
            return;
            case 65:
            this.ImagesList = ((System.Windows.Controls.ListBox)(target));
            
            #line 384 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.ImagesList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ImagesList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 66:
            this.ImageSurface = ((ColorBlend.ImageFrame)(target));
            return;
            case 67:
            this.FitToSize = ((System.Windows.Controls.CheckBox)(target));
            
            #line 453 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.FitToSize.Checked += new System.Windows.RoutedEventHandler(this.FitToSizeCheckChanged);
            
            #line default
            #line hidden
            
            #line 454 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.FitToSize.Unchecked += new System.Windows.RoutedEventHandler(this.FitToSizeCheckChanged);
            
            #line default
            #line hidden
            return;
            case 68:
            this.StatusBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 69:
            this.MousePositionOverImage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 70:
            this.ColorUnderMouseBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 71:
            this.HexContextMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 502 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.HexContextMenu.Click += new System.Windows.RoutedEventHandler(this.ShowColorValueAs);
            
            #line default
            #line hidden
            return;
            case 72:
            this.RGBContextMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 506 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.RGBContextMenu.Click += new System.Windows.RoutedEventHandler(this.ShowColorValueAs);
            
            #line default
            #line hidden
            return;
            case 73:
            this.HSLContextMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 510 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.HSLContextMenu.Click += new System.Windows.RoutedEventHandler(this.ShowColorValueAs);
            
            #line default
            #line hidden
            return;
            case 74:
            this.CMYKContextMenu = ((System.Windows.Controls.MenuItem)(target));
            
            #line 514 "..\..\..\ImageManipulation\ImageMan.xaml"
            this.CMYKContextMenu.Click += new System.Windows.RoutedEventHandler(this.ShowColorValueAs);
            
            #line default
            #line hidden
            return;
            case 75:
            this.ColorUnderMouseSample = ((System.Windows.Controls.Border)(target));
            return;
            case 76:
            this.ImageInfo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 77:
            this.ControlsFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 78:
            this.HDisplay = ((HistogramDisplay.HistogramViewer)(target));
            return;
            case 79:
            
            #line 630 "..\..\..\ImageManipulation\ImageMan.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleCloseButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

