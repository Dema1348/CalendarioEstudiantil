using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calendario_Estudiantil
{
    public class Actividad
    {
        public string Tipo{get;set;}
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Comentario { get; set; }

        public Actividad()
	{
	}


        public Actividad(string tipo, string fecha, string hora, string comentario ) 
        {
            this.Tipo = tipo;
            this.Fecha = fecha;
            this.Hora = hora;
            this.Comentario = comentario;
        }
        
    }
}
