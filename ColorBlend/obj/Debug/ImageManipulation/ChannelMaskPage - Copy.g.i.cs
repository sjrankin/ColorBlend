﻿#pragma checksum "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B21036D60C5BD596A2E7A67F94847286"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
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
    /// ChannelMaskPage
    /// </summary>
    public partial class ChannelMaskPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 60 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AlphaValueInput;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseAlphaValue;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RedValueInput;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseRedValue;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GreenValueInput;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseGreenValue;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BlueValueInput;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseBlueValue;
        
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/channelmaskpage%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
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
            
            #line 42 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExecuteFilter);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 50 "..\..\..\ImageManipulation\ChannelMaskPage - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetLocalImage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AlphaValueInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.UseAlphaValue = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.RedValueInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.UseRedValue = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.GreenValueInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.UseGreenValue = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.BlueValueInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.UseBlueValue = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

