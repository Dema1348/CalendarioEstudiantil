using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.IO.IsolatedStorage;


namespace Calendario_Estudiantil
{
    public partial class AgregarActividad : PhoneApplicationPage
    {
        private string[] tipo = { AppResources.Prueba, AppResources.Control, AppResources.Trabajo, AppResources.Disertacion };
        private IsolatedStorageSettings Settings =  IsolatedStorageSettings.ApplicationSettings;
        private string textoActividades = "textoActividades.txt";

      
        public AgregarActividad()
        {
            InitializeComponent();
            this.Tipo.ItemsSource = tipo;
        }

        private void btnGuardarActividad_Click(object sender, RoutedEventArgs e)
        {
            
           
            // Obtener instancia del Almacenamiento Aislado
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            // Instancia de StreamWriter para crear el archivo
            using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(textoActividades, FileMode.Append, FileAccess.Write, isolatedStorage)))
            {
                // Copiado de datos al archivo
                string tipoDato=null;
                if (Tipo.SelectedItem.Equals("Test") || Tipo.SelectedItem.Equals("Prueba"))
                    tipoDato= "Prueba";

                if (Tipo.SelectedItem.Equals("Work") || Tipo.SelectedItem.Equals("Trabajo"))
                    tipoDato = "Trabajo";

                if (Tipo.SelectedItem.Equals("Dissertation")||Tipo.SelectedItem.Equals("Disertacion"))
                    tipoDato = "Disertacion";

                if (Tipo.SelectedItem.Equals("Control") )
                    tipoDato = "Control";

                writer.WriteLine(String.Format("Tipo: " + tipoDato));
                writer.WriteLine(String.Format("Fecha: " + Fecha_picker.ValueString));
                writer.WriteLine(String.Format("Hora: " + Time_Picker.ValueString));
                if(!comentariosActividad.Text.Equals(""))
                    writer.WriteLine(String.Format("Comentario: " + comentariosActividad.Text));
                else
                    writer.WriteLine(String.Format("Comentario: no registra detalles"));
                writer.Close();
            }

            MessageBox.Show(AppResources.ActGuardada);
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

       

    }
}