﻿#pragma checksum "..\..\GetColors.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "02F298F6A4C1AC5F4EB782F40E153ABE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// GetColors
    /// </summary>
    public partial class GetColors : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Point0Color;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Point1Color;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Point2Color;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Point3Color;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Point4Color;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SP0;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SP1;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SP2;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SP3;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SP4;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OKButton;
        
        #line default
        #line hidden
        
        
        #line 232 "..\..\GetColors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/getcolors.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GetColors.xaml"
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
            this.Point0Color = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\GetColors.xaml"
            this.Point0Color.LostFocus += new System.Windows.RoutedEventHandler(this.ColorBlockLostFocus);
            
            #line default
            #line hidden
            
            #line 33 "..\..\GetColors.xaml"
            this.Point0Color.KeyDown += new System.Windows.Input.KeyEventHandler(this.CheckForReturn);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Point1Color = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\GetColors.xaml"
            this.Point1Color.LostFocus += new System.Windows.RoutedEventHandler(this.ColorBlockLostFocus);
            
            #line default
            #line hidden
            
            #line 53 "..\..\GetColors.xaml"
            this.Point1Color.KeyDown += new System.Windows.Input.KeyEventHandler(this.CheckForReturn);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Point2Color = ((System.Windows.Controls.TextBox)(target));
            
            #line 72 "..\..\GetColors.xaml"
            this.Point2Color.LostFocus += new System.Windows.RoutedEventHandler(this.ColorBlockLostFocus);
            
            #line default
            #line hidden
            
            #line 73 "..\..\GetColors.xaml"
            this.Point2Color.KeyDown += new System.Windows.Input.KeyEventHandler(this.CheckForReturn);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Point3Color = ((System.Windows.Controls.TextBox)(target));
            
            #line 92 "..\..\GetColors.xaml"
            this.Point3Color.LostFocus += new System.Windows.RoutedEventHandler(this.ColorBlockLostFocus);
            
            #line default
            #line hidden
            
            #line 93 "..\..\GetColors.xaml"
            this.Point3Color.KeyDown += new System.Windows.Input.KeyEventHandler(this.CheckForReturn);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Point4Color = ((System.Windows.Controls.TextBox)(target));
            
            #line 112 "..\..\GetColors.xaml"
            this.Point4Color.LostFocus += new System.Windows.RoutedEventHandler(this.ColorBlockLostFocus);
            
            #line default
            #line hidden
            
            #line 113 "..\..\GetColors.xaml"
            this.Point4Color.KeyDown += new System.Windows.Input.KeyEventHandler(this.CheckForReturn);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SP0 = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.SP1 = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.SP2 = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.SP3 = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.SP4 = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            this.OKButton = ((System.Windows.Controls.Button)(target));
            
            #line 227 "..\..\GetColors.xaml"
            this.OKButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 234 "..\..\GetColors.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

