﻿#pragma checksum "C:\Users\Dema op\Documents\Visual Studio 2012\Projects\Calendario Estudiantil\Calendario Estudiantil\Notas.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "420A64D579682F815FC1F6015323B1F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.33440
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Calendario_Estudiantil {
    
    
    public partial class Notas : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox NameNota;
        
        internal System.Windows.Controls.TextBox Contenido;
        
        internal System.Windows.Controls.Button Save;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Calendario%20Estudiantil;component/Notas.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.NameNota = ((System.Windows.Controls.TextBox)(this.FindName("NameNota")));
            this.Contenido = ((System.Windows.Controls.TextBox)(this.FindName("Contenido")));
            this.Save = ((System.Windows.Controls.Button)(this.FindName("Save")));
        }
    }
}

