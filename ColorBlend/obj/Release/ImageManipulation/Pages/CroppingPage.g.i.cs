﻿#pragma checksum "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "31BF66B63C6FB9DD0360A70BD3632BFF"
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
    /// CroppingPage
    /// </summary>
    public partial class CroppingPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LeftCropIn;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TopCropIn;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RightCropIn;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BottomCropIn;
        
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/pages/croppingpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
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
            
            #line 43 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExecuteFilterParamaters);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 51 "..\..\..\..\ImageManipulation\Pages\CroppingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetLocalImage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LeftCropIn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TopCropIn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RightCropIn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.BottomCropIn = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
