﻿#pragma checksum "..\..\..\pregled\EtiketaList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "756D6AB618D0A605F5D91DE5EFD75C70"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace HCI_Lokali {
    
    
    /// <summary>
    /// EtiketaList
    /// </summary>
    public partial class EtiketaList : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\pregled\EtiketaList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tabela;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\pregled\EtiketaList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button izmena;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\pregled\EtiketaList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button brisanje;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\pregled\EtiketaList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button izlazak;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\pregled\EtiketaList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nova;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\pregled\EtiketaList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button help;
        
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
            System.Uri resourceLocater = new System.Uri("/HCI_Lokali;component/pregled/etiketalist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\pregled\EtiketaList.xaml"
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
            this.tabela = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.izmena = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\pregled\EtiketaList.xaml"
            this.izmena.Click += new System.Windows.RoutedEventHandler(this.izmena_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.brisanje = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\pregled\EtiketaList.xaml"
            this.brisanje.Click += new System.Windows.RoutedEventHandler(this.brisanje_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.izlazak = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\pregled\EtiketaList.xaml"
            this.izlazak.Click += new System.Windows.RoutedEventHandler(this.izlazak_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.nova = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\pregled\EtiketaList.xaml"
            this.nova.Click += new System.Windows.RoutedEventHandler(this.novi_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.help = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\pregled\EtiketaList.xaml"
            this.help.Click += new System.Windows.RoutedEventHandler(this.help_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

