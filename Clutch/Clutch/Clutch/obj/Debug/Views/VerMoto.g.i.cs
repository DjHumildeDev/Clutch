﻿#pragma checksum "..\..\..\Views\VerMoto.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E9785FB676264972706980E5DCD6BCBADCA9DF8EDE035B816E7195F556DD2504"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Clutch.Views;
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


namespace Clutch.Views {
    
    
    /// <summary>
    /// VerMoto
    /// </summary>
    public partial class VerMoto : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Views\VerMoto.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBxMatricula;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\VerMoto.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBxNumero;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\VerMoto.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbBxEstado;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\VerMoto.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbBxCC;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\VerMoto.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\VerMoto.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
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
            System.Uri resourceLocater = new System.Uri("/Clutch;component/views/vermoto.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\VerMoto.xaml"
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
            this.txtBxMatricula = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtBxNumero = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\Views\VerMoto.xaml"
            this.txtBxNumero.SelectionChanged += new System.Windows.RoutedEventHandler(this.txtBxNumero_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmbBxEstado = ((System.Windows.Controls.ComboBox)(target));
            
            #line 28 "..\..\..\Views\VerMoto.xaml"
            this.cmbBxEstado.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbBxEstado_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbBxCC = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Views\VerMoto.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.btnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Views\VerMoto.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

