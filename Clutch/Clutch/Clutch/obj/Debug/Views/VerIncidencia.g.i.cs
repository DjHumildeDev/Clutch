﻿#pragma checksum "..\..\..\Views\VerIncidencia.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B4F562B07BF638234919E49615D37F515390F4779D5CD8E603ACC6BC28E3BA3D"
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
using System.Windows.Forms.Integration;
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
    /// VerIncidencia
    /// </summary>
    public partial class VerIncidencia : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\Views\VerIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBxDescripcionIncidencia;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\VerIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBxEmpleado;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\VerIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmBxTipo;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Views\VerIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chBxResuelta;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Views\VerIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtPckFecha;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\VerIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\VerIncidencia.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Clutch;component/views/verincidencia.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\VerIncidencia.xaml"
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
            this.txtBxDescripcionIncidencia = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtBxEmpleado = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cmBxTipo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\..\Views\VerIncidencia.xaml"
            this.cmBxTipo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmBxTipo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.chBxResuelta = ((System.Windows.Controls.CheckBox)(target));
            
            #line 35 "..\..\..\Views\VerIncidencia.xaml"
            this.chBxResuelta.Click += new System.Windows.RoutedEventHandler(this.chBxResuelta_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dtPckFecha = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Views\VerIncidencia.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.btnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Views\VerIncidencia.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

