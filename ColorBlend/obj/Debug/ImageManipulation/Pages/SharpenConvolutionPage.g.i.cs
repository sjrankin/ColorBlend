﻿#pragma checksum "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D4C09560EBE13D42CB33DFE20064CC87EF7B9E0B"
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
    /// SharpenConvolutionPage
    /// </summary>
    public partial class SharpenConvolutionPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Sharpen;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Sharpen3x3;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Sharpen5x5;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Highpass;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Lowpass3x3;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton LowPass5x5;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton SobelHorizontal;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton SobelVertical;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Prewitt1;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Prewitt2;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SkipTransparentPix;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseTransparentPix;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseLuminanceThresholdCheck;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LuminanceThresholdInput;
        
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/pages/sharpenconvolutionpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 42 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExecuteFilter);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 50 "..\..\..\..\ImageManipulation\Pages\SharpenConvolutionPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetLocalImage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Sharpen = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.Sharpen3x3 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.Sharpen5x5 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.Highpass = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.Lowpass3x3 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.LowPass5x5 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 9:
            this.SobelHorizontal = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 10:
            this.SobelVertical = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 11:
            this.Prewitt1 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 12:
            this.Prewitt2 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 13:
            this.SkipTransparentPix = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 14:
            this.UseTransparentPix = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 15:
            this.UseLuminanceThresholdCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 16:
            this.LuminanceThresholdInput = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

