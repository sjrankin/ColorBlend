﻿#pragma checksum "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "55F38E06656D5C4E97BD72B47844C39F"
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
    /// BayesDecoderPage
    /// </summary>
    public partial class BayesDecoderPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RGGB;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton BGGR;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Nearest;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Linear;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Cubic;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
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
            System.Uri resourceLocater = new System.Uri("/ColorBlend;component/imagemanipulation/pages/bayesdecoderpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
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
            
            #line 42 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExecuteFilter);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 50 "..\..\..\..\ImageManipulation\Pages\BayesDecoderPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetLocalImage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RGGB = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.BGGR = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.Nearest = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.Linear = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.Cubic = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.ShowGridCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
