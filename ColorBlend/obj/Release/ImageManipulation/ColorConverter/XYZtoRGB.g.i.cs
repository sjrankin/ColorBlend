﻿#pragma checksum "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9108CAB50D3FA77417E23BBE3DB2A60E"
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
    /// XYZtoRGB
    /// </summary>
    public partial class XYZtoRGB : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox XInput;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YInput;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ZInput;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RGBDecResult;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RGBByteResult;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/colorconverter/xyztorgb.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
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
            this.XInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.YInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ZInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 93 "..\..\..\..\ImageManipulation\ColorConverter\XYZtoRGB.xaml"
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

