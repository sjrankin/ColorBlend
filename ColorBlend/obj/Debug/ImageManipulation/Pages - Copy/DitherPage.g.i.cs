﻿#pragma checksum "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "316DF2A50B8A59695388701E227B6AF8"
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
    /// DitherPage
    /// </summary>
    public partial class DitherPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 61 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton FloydSteinbergDithering;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton FalseFloydSteinbergDithering;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton AtkinsonDithering;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton JarvisJudiceNinkeDithering;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton StuckiDithering;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton BurkesDithering;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Sierra1Dithering;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Sierra2Dithering;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Sierra3Dithering;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ShowGridCheck;
        
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/pages%20-%20copy/ditherpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
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
            
            #line 42 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExecuteFilter);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 50 "..\..\..\..\ImageManipulation\Pages - Copy\DitherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetLocalImage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FloydSteinbergDithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.FalseFloydSteinbergDithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.AtkinsonDithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.JarvisJudiceNinkeDithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.StuckiDithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.BurkesDithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 9:
            this.Sierra1Dithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 10:
            this.Sierra2Dithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 11:
            this.Sierra3Dithering = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 12:
            this.ShowGridCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

