﻿#pragma checksum "..\..\NewProduct.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CC810432084705B1CF87157CF21A7A3E39407DA8AA1C80AFAD0D0F7A431D5187"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CakeShop;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace CakeShop {
    
    
    /// <summary>
    /// NewProduct
    /// </summary>
    public partial class NewProduct : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel Top;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgSave;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgCancel;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxName;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxPrice;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxitemType;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxDescription;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Img;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\NewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChooseImg;
        
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
            System.Uri resourceLocater = new System.Uri("/CakeShop;component/newproduct.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewProduct.xaml"
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
            this.Top = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 2:
            this.imgSave = ((System.Windows.Controls.Image)(target));
            
            #line 23 "..\..\NewProduct.xaml"
            this.imgSave.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgSave_MouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.imgCancel = ((System.Windows.Controls.Image)(target));
            
            #line 24 "..\..\NewProduct.xaml"
            this.imgCancel.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgCancel_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.textBoxName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.textBoxPrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 56 "..\..\NewProduct.xaml"
            this.textBoxPrice.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Price_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.comboBoxitemType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.textBoxDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Img = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.ChooseImg = ((System.Windows.Controls.Button)(target));
            
            #line 141 "..\..\NewProduct.xaml"
            this.ChooseImg.Click += new System.Windows.RoutedEventHandler(this.ChooseImg_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

