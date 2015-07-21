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
    public partial class ActividadesHoy : PhoneApplicationPage
    {

        private string textoActividades = "textoActividades.txt";
        public ActividadesHoy()
        {
            InitializeComponent();

            // Obtener instancia del Almacenamiento Aislado
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            // Verificar si el archivo existe para evitar errores
            if (isolatedStorage.FileExists(textoActividades))
            {
                // Instancia de StreamReader para obtener y leer archivo almadenado
                using (StreamReader reader = new StreamReader(isolatedStorage.OpenFile(textoActividades, FileMode.Open, FileAccess.Read)))
                {
                    int i = 0;
                    List<Actividad> actividades = new List<Actividad>();
                    string aux="",tipoNew="",fechaNew="",horaNew="",comentarioNew="";

                    // Lectura del archivo linea por linea, escribiéndolo en el control en pantalla.
                    
                    while (!reader.EndOfStream)
                    {
                        if (i == 4) {
                            i = 0;
                        }
                      aux=(reader.ReadLine());
                       
                        if (i == 0) {
                           tipoNew = aux;
                           
                           if(AppResources.Idioma.Equals("Ingles"))
                           tipoNew= TraducirTipo(tipoNew);
                           
                           
                       }

                       if (i == 1)
                       {
                           fechaNew= aux;
                           if (AppResources.Idioma.Equals("Ingles"))
                               fechaNew = TraducirFecha(fechaNew);
                         
                       }

                       if (i == 2)
                       {
                           horaNew = aux;
                           if (AppResources.Idioma.Equals("Ingles"))
                               horaNew = TraducirHora(horaNew);
                       
                       }

                       if (i == 3)
                       {
                           comentarioNew = aux;
                           if (AppResources.Idioma.Equals("Ingles"))
                               comentarioNew = TraducirComentario(comentarioNew);
                           actividades.Add(new Actividad(tipoNew, fechaNew, horaNew, comentarioNew));
                           listaActividad.ItemsSource = actividades;
                           
                       }
                       
                        i++;
                    }


                }
            }
            else
            {
                MessageBox.Show(AppResources.NoRegistraAct);
               
               
            }
        }

      
        private string TraducirTipo(string aux)
        {
            if (aux.Equals("Tipo: Prueba"))
                return "Type: Test";

            

            if (aux.Equals("Tipo: Trabajo"))
                return "Type: Work";

           

            if (aux.Equals("Tipo: Control"))
                return "Type: Control";

            

            if (aux.Equals("Tipo: Disertacion"))
                return "Type: Dissertation";

            return "Tipo:";

        }

        private string TraducirFecha(string aux) 
        {
            string[] Fecha = aux.Split(' ');

            if (Fecha[0].Equals("Fecha:"))
                return "Date: "+Fecha[1];

            return "Date: ";
        }

        private string TraducirComentario(string aux)
        {
            string[] comentario = aux.Split(':');

            if (aux.Equals("Comentario: no registra detalles"))
                return "Comment: Not records details";

            else
                return "Comment: " + comentario[1];
            

            
        }

        private string TraducirHora(string aux)
        {
            string[] Hora = aux.Split(' ');

            if (Hora[0].Equals("Hora:"))
                return "Time: " + Hora[1];

            return "Fecha:";
        }

        private void ListaActividades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}