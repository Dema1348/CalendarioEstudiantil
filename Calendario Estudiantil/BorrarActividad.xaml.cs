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
    public partial class BorrarActividad : PhoneApplicationPage
    {
        private string textoActividades = "textoActividades.txt";
        List<Actividad> actividades = new List<Actividad>();
        public BorrarActividad()
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
                    
                    string aux = "", tipoNew = "", fechaNew = "", horaNew = "", comentarioNew = "";

                    // Lectura del archivo linea por linea, escribiéndolo en el control en pantalla.

                    while (!reader.EndOfStream)
                    {
                        if (i == 4)
                        {
                            i = 0;
                        }
                        aux = (reader.ReadLine());

                        if (i == 0)
                        {
                            tipoNew = aux;

                            if (AppResources.Idioma.Equals("Ingles"))
                                tipoNew = TraducirTipo(tipoNew);


                        }

                        if (i == 1)
                        {
                            fechaNew = aux;
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

        private string TraducirTipoInverso(string aux)
        {
            if (aux.Equals("Type: Test"))
                return "Tipo: Prueba";



            if (aux.Equals("Type: Work"))
                return "Tipo: Trabajo";



            if (aux.Equals("Type: Control"))
                return "Tipo: Control";



            if (aux.Equals("Type: Dissertation"))
                return "Tipo: Disertacion";

            return "Tipo:";

        }

        private string TraducirFecha(string aux)
        {
            string[] Fecha = aux.Split(' ');

            if (Fecha[0].Equals("Fecha:"))
                return "Date: " + Fecha[1];

            return "Date: ";
        }

        private string TraducirFechaInverso(string aux)
        {
            string[] Fecha = aux.Split(' ');

            if (Fecha[0].Equals("Date:"))
                return "Fecha: " + Fecha[1];

            return "Fecha: ";
        }

        private string TraducirComentario(string aux)
        {
            string[] comentario = aux.Split(':');

            if (aux.Equals("Comentario: no registra detalles"))
                return "Comment: Not records details";

            else
                return "Comment: " + comentario[1];



        }
        private string TraducirComentarioInverso(string aux)
        {
            string[] comentario = aux.Split(':');

            if (aux.Equals("Comment: Not records details"))
                return "Comentario: no registra detalles";

            else
                return "Comentario: " + comentario[1];



        }

        private string TraducirHora(string aux)
        {
            string[] Hora = aux.Split(' ');

            if (Hora[0].Equals("Hora:"))
                return "Time: " + Hora[1];

            return "Fecha:";
        }

        private string TraducirHoraInverso(string aux)
        {
            string[] Hora = aux.Split(' ');

            if (Hora[0].Equals("Time:"))
                return "Hora: " + Hora[1];

            return "Time:";
        }


        private void listaActividad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int seleccion = listaActividad.SelectedIndex;
            
            // Obtener instancia del Almacenamiento Aislado
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            isolatedStorage.DeleteFile(textoActividades);
            // Instancia de StreamWriter para crear el archivo
            for (int i = 0; i < actividades.Count; i++)
            {
                if (seleccion != i)
                {
                    using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(textoActividades, FileMode.Append, FileAccess.Write, isolatedStorage)))
                    {
                        // Copiado de datos al archivo

                        if(AppResources.Idioma.Equals("Ingles"))
                            writer.WriteLine(String.Format(TraducirTipoInverso(actividades[i].Tipo)));
                        else
                            writer.WriteLine(String.Format(actividades[i].Tipo));


                        if (AppResources.Idioma.Equals("Ingles"))
                            writer.WriteLine(String.Format(TraducirFechaInverso(actividades[i].Fecha)));
                        else
                            writer.WriteLine(String.Format(actividades[i].Fecha));

                        if (AppResources.Idioma.Equals("Ingles"))
                            writer.WriteLine(String.Format(TraducirHoraInverso(actividades[i].Hora)));
                        else
                            writer.WriteLine(String.Format(actividades[i].Hora));

                        if (AppResources.Idioma.Equals("Ingles"))
                            writer.WriteLine(String.Format(TraducirComentarioInverso(actividades[i].Comentario)));
                        else
                            writer.WriteLine(String.Format(actividades[i].Comentario));

                        

                        writer.Close();
                    }
                }

                else
                    MessageBox.Show(AppResources.ActBorrada+'.');
                
            }
            
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}