﻿#pragma checksum "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2AA7FD92768B5B4644B54E506DD97ED3657AF66F"
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
using Iro3.Controls.ColorInput;
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
    /// TSLtoRGB
    /// </summary>
    public partial class TSLtoRGB : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TInput;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SInput;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LInput;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RGBDecResult;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RGBByteResult;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RGBDoubleResult;
        
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/colorconverter/tsltorgb.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
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
            this.TInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.SInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.LInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 93 "..\..\..\..\ImageManipulation\ColorConverter\TSLtoRGB.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HandleConvertClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RGBDecResult = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.RGBByteResult = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.RGBDoubleResult = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

