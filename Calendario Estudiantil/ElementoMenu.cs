using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calendario_Estudiantil
{
    public class ElementoMenu
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }

        public ElementoMenu(string imagen, string nombre) 
        {
            this.Imagen = imagen;
            this.Nombre = nombre;
        }
    }
}
