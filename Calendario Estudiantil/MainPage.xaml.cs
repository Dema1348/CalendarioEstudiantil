using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;



namespace Calendario_Estudiantil
{
    public partial class MainPage : PhoneApplicationPage
    {
       

        private DateTime fechaActual;
        private string textoActividades = "textoActividades.txt";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InitializeObjects();
          // Evaluar();
            
            

            // Establecer el contexto de datos del control ListBox control en los datos de ejemplo
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Cargar datos para los elementos ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<ElementoMenu> menu = new List<ElementoMenu>();

            
            menu.Add(new ElementoMenu("/OxygenIcons/verActividadOX.png", AppResources.VerAct));
            menu.Add(new ElementoMenu("/OxygenIcons/nuevaActividadOX.png", AppResources.AgregarAct));
            menu.Add(new ElementoMenu("/OxygenIcons/deleteOX.png", AppResources.BorrarAct));
           // menu.Add(new ElementoMenu("/OxygenIcons/usuarioOX.png", "Perfil usuario"));
            //menu.Add(new ElementoMenu("/OxygenIcons/notasOX.png", "Recordatorios"));
            menu.Add(new ElementoMenu("/OxygenIcons/alarma2OX.png", AppResources.Alarma));
            
            listaMenu.ItemsSource = menu;
        }


        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

            InizializeCalendar(DateTime.Now);
        }

        private void Evaluar() 
        {
            IsolatedStorageSettings configuracion = IsolatedStorageSettings.ApplicationSettings;
            IsolatedStorageSettings primeraVez = IsolatedStorageSettings.ApplicationSettings;


            if (!(bool)configuracion.Contains("evaluado") )
            {
                if ((bool)primeraVez.Contains("yes"))
                {
                    MessageBoxResult resultado = MessageBox.Show("¿Deseas evaluar esta aplicación?", "Evaluación", MessageBoxButton.OKCancel);

                    if (resultado == MessageBoxResult.OK)
                    {
                        MarketplaceReviewTask evaluacionApp = new MarketplaceReviewTask();
                        evaluacionApp.Show();
                        configuracion.Add("evaluado", true);

                    }
                }

                else
                {
                    primeraVez.Add("yes", false);

                }

            }

          

            
        }

        private void InitializeObjects()
        {

            mDefaultHolidayBrush = new SolidColorBrush(Colors.Red);

          
        }

        private DateTime BringToFirst(DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }



        private void InizializeCalendar(DateTime dateTime)
        {
           
            SetMonthYear(dateTime);

            int days = DateTime.DaysInMonth(dateTime.Year, dateTime.Month); //cuantos dias tiene el mes
            DateTime firstOfMonth = BringToFirst(dateTime);
            DayOfWeek startingDay = firstOfMonth.DayOfWeek;
            List<Actividad> actividadesCalentadario = new List<Actividad>();
            actividadesCalentadario = Leer();
            int[] dia;
            int[] mes;
            int[] anio;
            string[] tipo;
            dia = new int[100];
            mes = new int[100];
            anio = new int[100];
            tipo = new string[100];
            if (actividadesCalentadario != null) 

            {
                for (int x=0; x < actividadesCalentadario.Count; x++)
                {
                    string[] DiaDigito = actividadesCalentadario[x].Fecha.Split('/','-');
                    String TipoCalendario= actividadesCalentadario[x].Tipo;
                    dia[x] = Convert.ToInt16(DiaDigito[0].Substring(6));
                    mes[x] = Convert.ToInt16(DiaDigito[1]);
                    anio[x] = Convert.ToInt16(DiaDigito[2]);
                    tipo[x] = TipoCalendario.Substring(6);
                }
            }

            else
                actividadesCalentadario=null;

            //eliminar actividades repetidas
            if (actividadesCalentadario != null)
            {
                for (int i = 0; i < actividadesCalentadario.Count; i++)
                {

                    for (int x = i + 1; x < actividadesCalentadario.Count; x++)
                    {
                        if (dia[i] == dia[x] & tipo[i].Equals(tipo[x]) & mes[i] == mes[x] & anio[i] == anio[x])
                        {

                            dia[i] = 0;
                            mes[i] = 0;
                            anio[i] = 0;
                            tipo[i] = "";
                        }
                    }
                }
            }
            //unir dias repetidos de actividades;
          

            if(actividadesCalentadario!=null)
            for (int i = 0; i < actividadesCalentadario.Count; i++) {
                
                for (int x = i+1; x < actividadesCalentadario.Count; x++) {
                    
                    if (dia[i] == dia[x] & !tipo[i].Equals(tipo[x]) & mes[i]==mes[x] & anio[i]==anio[x]) {


                          
                            tipo[x] = tipo[x] + tipo[i];
                            dia[i] = 0;
                            mes[i] = 0;
                            anio[i] = 0;
                            tipo[i] = "";
                           
                    }
                }
            }

                ClearDays();

            int linea = 2; 
            int columna = GetDayNumber(startingDay);
            for (int i = 0; i < days; i++)
            {
                Border bord = new Border();
                bord.Name = "brd" + (i + 1).ToString();

                bord.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                bord.Margin = new Thickness(0);

               
                Button ba = new Button();
                ba.BorderBrush = new SolidColorBrush(Colors.Transparent);
                ba.Width = 99;
                ba.Height = 99;
                ba.Tag = (i + 1);
                ba.Margin = new Thickness(0);
                ba.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                ba.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                ba.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                ba.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                ba.Content = (i + 1).ToString();
                ba.Click += new RoutedEventHandler(Click);

                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && tipo[x].Equals("Prueba") && mes[x] == dateTime.Month && anio[x] == dateTime.Year) 
                    {
                    
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/Blue.png", UriKind.Relative));
                        ba.Background = brush;
                       
                    }
                }

                

                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && tipo[x].Equals("Disertacion") && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/Green.png", UriKind.Relative));
                        ba.Background = brush;
                    }
                }

                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && tipo[x].Equals("Trabajo") && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/Marron.png", UriKind.Relative));
                        ba.Background = brush;
                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && tipo[x].Equals("Control") && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/Orange.png", UriKind.Relative));
                        ba.Background = brush;
                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("ControlPrueba") || tipo[x].Equals("PruebaControl")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/BlueOrange.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }

                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("ControlDisertacion") || tipo[x].Equals("DisertacionControl")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/OrangeGreen.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }

                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("ControlTrabajo") || tipo[x].Equals("TrabajoControl")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/MarronOrange.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("DisertacionTrabajo") || tipo[x].Equals("TrabajoDisertacion")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/MarronGreen.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("DisertacionPrueba") || tipo[x].Equals("PruebaDisertacion")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/BlueGreen.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("TrabajoPrueba") || tipo[x].Equals("PruebaTrabajo")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/BlueMarron.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && ( tipo[x].Equals("ControlPruebaDisertacion")|| tipo[x].Equals("ControlDisertacionPrueba") ||  tipo[x].Equals("PruebaControlDisertacion") 
                        ||  tipo[x].Equals("PruebaDisertacionControl") ||  tipo[x].Equals("DisertacionControlPrueba") ||  tipo[x].Equals("DisertacionPruebaControl") )
                        && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/OrangeBlueGreen.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("ControlPruebaTrabajo") || tipo[x].Equals("ControlTrabajoPrueba") || tipo[x].Equals("PruebaControlTrabajo")
                        || tipo[x].Equals("PruebaTrabajoControl") || tipo[x].Equals("TrabajoControlPrueba") || tipo[x].Equals("TrabajoPruebaControl"))
                        && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/OrangeBlueMarron.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("ControlDisertacionTrabajo") || tipo[x].Equals("ControlTrabajoDisertacion") || tipo[x].Equals("DisertacionControlTrabajo")
                        || tipo[x].Equals("DisertacionTrabajoControl") || tipo[x].Equals("TrabajoControlDisertacion") || tipo[x].Equals("TrabajoDisertacionControl"))
                        && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/OrangeGreenMarron.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("PruebaDisertacionTrabajo") || tipo[x].Equals("PruebaTrabajoDisertacion") || tipo[x].Equals("DisertacionPruebaTrabajo")
                        || tipo[x].Equals("DisertacionTrabajoPrueba") || tipo[x].Equals("TrabajoPruebaDisertacion") || tipo[x].Equals("TrabajoDisertacionPrueba"))
                        && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/BlueGreenMarron.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                for (int x = 0; x < dia.Length; x++)
                {

                    if (dia[x] == (i + 1) && (tipo[x].Equals("ControlPruebaDisertacionTrabajo")||
                    tipo[x].Equals("ControlPruebaTrabajoDisertacion")||
                    tipo[x].Equals("ControlDisertacionPruebaTrabajo") ||
                    tipo[x].Equals("ControlDisertacionTrabajoPrueba") ||
                    tipo[x].Equals("ControlTrabajoPruebaDisertacion") ||
                    tipo[x].Equals("ControlTrabajoDisertacionPrueba") ||
                    tipo[x].Equals("PruebaControlDisertacionTrabajo") ||
                    tipo[x].Equals("PruebaControlTrabajoDisertacion") ||
                    tipo[x].Equals("PruebaDisertacionControlTrabajo") ||
                    tipo[x].Equals("PruebaDisertacionTrabajoControl") ||
                    tipo[x].Equals("PruebaTrabajoControlDisertacion") ||
                    tipo[x].Equals("PruebaTrabajoDisertacionControl") ||
                    tipo[x].Equals("DisertacionControlPruebaTrabajo") ||
                    tipo[x].Equals("DisertacionControlTrabajoPrueba") ||
                    tipo[x].Equals("DisertacionPruebaControlTrabajo") ||
                    tipo[x].Equals("DisertacionPruebaTrabajoControl") ||
                    tipo[x].Equals("DisertacionTrabajoControlPrueba") ||
                    tipo[x].Equals("DisertacionTrabajoPruebaControl") ||
                    tipo[x].Equals("TrabajoControlPruebaDisertacion") ||
                    tipo[x].Equals("TrabajoControlDisertacionPrueba") ||
                    tipo[x].Equals("TrabajoPruebaControlDisertacion") ||
                    tipo[x].Equals("TrabajoPruebaDisertacionControl") ||
                    tipo[x].Equals("TrabajoDisertacionControlPrueba") ||
                    tipo[x].Equals("TrabajoDisertacionPruebaControl")) && mes[x] == dateTime.Month && anio[x] == dateTime.Year)
                    {

                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("/Colors/Full.png", UriKind.Relative));
                        ba.Background = brush;

                    }
                }
                if (columna > 5) // feriado
                {
                    ba.Foreground = HolidayBrush;
                }

                bord.Child = ba;

                AddElementToGrid(linea, columna++, bord);

               
                if (columna % 7 == 0)
                {
                    linea++;
                    columna = 0;
                }
            }

            fechaActual = dateTime;
            LayoutRoot.UpdateLayout();

        }
        private void Click(object sender, EventArgs e)
        {
            
            DateTime date = new DateTime(fechaActual.Year, fechaActual.Month, (int)(sender as Button).Tag);

            var fechanew=date;
            int dia = date.Day;
            int mes = date.Month;
            LeerActividadesHoy(dia,mes);
            
        }

        private void LeerActividadesHoy(int dia, int mes)
        {
            List<Actividad> actividadesCalentadario = new List<Actividad>();
            
            actividadesCalentadario = Leer();
            int diaActividad;
            int mesActividad;
            if (actividadesCalentadario != null)
            {
                for (int i = 0; i < actividadesCalentadario.Count; i++)
                {
                    string[] digitoFecha = actividadesCalentadario[i].Fecha.Split('/','-');
                    diaActividad = Convert.ToInt16(digitoFecha[0].Substring(6));
                    mesActividad= Convert.ToInt16(digitoFecha[1]);
                    if (diaActividad == dia && mesActividad==mes)
                    {
                        new Actividad(actividadesCalentadario[i].Tipo, actividadesCalentadario[i].Fecha, actividadesCalentadario[i].Hora, actividadesCalentadario[i].Comentario);

                        if (AppResources.Idioma.Equals("Ingles"))
                        {
                            MessageBox.Show(TraducirTipo(actividadesCalentadario[i].Tipo) + " " +
                                TraducirFecha(actividadesCalentadario[i].Fecha) + " " +
                                TraducirHora(actividadesCalentadario[i].Hora )+ " " +
                                TraducirComentario(actividadesCalentadario[i].Comentario));
                        }

                        else
                        {
                            MessageBox.Show(actividadesCalentadario[i].Tipo + " " +
                               actividadesCalentadario[i].Fecha + " " +
                               actividadesCalentadario[i].Hora + " " +
                               actividadesCalentadario[i].Comentario);
                        }

                       
                    }
                   
                }
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
                return "Date: " + Fecha[1];

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

        private void SetMonthYear(DateTime dateTime)
        {
            mes.Text = String.Format(dateTime.ToString("MMMM").ToUpper()+" "+ dateTime.Year);
        }

        private int GetDayNumber(DayOfWeek d)
        {
            if (d == DayOfWeek.Sunday)
            {
                return 6;
            }
            else return ((int)d) - 1;
        }

        private void ClearDays()
        {
            LayoutRoot.Children.ToList().ForEach(x =>
            {
                if (x is FrameworkElement && (x as FrameworkElement).Name.StartsWith("brd"))
                {
                    LayoutRoot.Children.Remove(x);
                }
            });
        }

        private void AddElementToGrid(int riga, int colonna, FrameworkElement el)
        {
            Grid.SetRow(el, riga);
            Grid.SetColumn(el, colonna);
            LayoutRoot.Children.Add(el);
            
        }

        private void OnChangeMonth(object sender, RoutedEventArgs e)
        {
            Button butSender = sender as Button;

            if (butSender.Name == NextBtn.Name)
            {
                fechaActual = fechaActual.AddMonths(+1);
            }
            else if (butSender.Name == BackBtn.Name)
            {
                fechaActual = fechaActual.AddMonths(-1);
            }

            InizializeCalendar(fechaActual);
        }




        private SolidColorBrush mDefaultHolidayBrush;

       

        public SolidColorBrush HolidayBrush
        {
            get
            {
                return mDefaultHolidayBrush;
            }
            set
            {
                mDefaultHolidayBrush = value;
            }
        }
       
      
        private void listaMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listaMenu.SelectedIndex == -1)
                return;

            if (listaMenu.SelectedIndex == 1) 
            { 
            NavigationService.Navigate(new Uri("/AgregarActividad.xaml", UriKind.Relative));
                listaMenu.SelectedIndex = -1;
            }


            if (listaMenu.SelectedIndex == 2)
            {
                NavigationService.Navigate(new Uri("/BorrarActividad.xaml", UriKind.Relative));
                listaMenu.SelectedIndex = -1;
            }

            
           
            if (listaMenu.SelectedIndex == 0)
            {
                NavigationService.Navigate(new Uri("/ActividadesHoy.xaml", UriKind.Relative));
                listaMenu.SelectedIndex = -1;
            }
            
        
            

            if (listaMenu.SelectedIndex ==3)
            {
                NavigationService.Navigate(new Uri("/Alarma.xaml", UriKind.Relative));
                listaMenu.SelectedIndex = -1;
            }
        }

       

        public List<Actividad> Leer() { 

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
                           
                       }

                       if (i == 1)
                       {
                           fechaNew= aux;
                         
                       }

                       if (i == 2)
                       {
                           horaNew = aux;
                       
                       }

                       if (i == 3)
                       {
                           comentarioNew = aux;
                           actividades.Add(new Actividad(tipoNew, fechaNew, horaNew, comentarioNew));
                        
                          
                       }
                       
                        i++;
                    }
                    return actividades;


                }
            }
            else
            {
               
                return null;
               
            }
            
            
        }

      
         



        }

       
    
}
