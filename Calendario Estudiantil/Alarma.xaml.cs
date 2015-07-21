using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;

namespace Calendario_Estudiantil
{
    public partial class Alarma : PhoneApplicationPage
    {
        
        public Alarma()
        {
            InitializeComponent();
            
        }



        private void btnStarAlarma_Click(object sender, RoutedEventArgs e)
        {
            ComponerAlarma();
        }

        void ComponerAlarma()
        {
            var date = (DateTime)Date_Picker.Value;
            var time = (DateTime)Time_Picker.Value;
            int segundos=time.Second;
            int hora = time.Hour;
            int minutos = time.Minute;
            int mes = date.Month;
            int year = date.Year;
            int dia = date.Day;
            DateTime fechaCompleta = new DateTime(year,mes,dia,hora,minutos,segundos);
            

            if (ScheduledActionService.Find("alarmaCalendario") != null)
            ScheduledActionService.Remove("alarmaCalendario");

            Alarm alamarCalendario= new Alarm("alarmaCalendario");
            alamarCalendario.Content = recordatorio.Text;
            alamarCalendario.Sound = new Uri("Alarma-Calendario.mp3", UriKind.Relative);
            alamarCalendario.BeginTime = fechaCompleta;
            
            
            alamarCalendario.RecurrenceType = RecurrenceInterval.Daily;

            ScheduledActionService.Add(alamarCalendario);
            if(AppResources.Idioma.Equals("Ingles"))
                MessageBox.Show("The alarm is set for the " + fechaCompleta);
            else
                MessageBox.Show("La alarma esta programada para el " + fechaCompleta);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
           
        }
    }
}