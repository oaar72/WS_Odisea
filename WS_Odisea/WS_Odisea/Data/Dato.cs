using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Odisea.Data
{
    public class Dato
    {
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public string valor { get; set; }
        public string mensaje { get; set; }

        public Dato()
        {
            tipo        = "";
            descripcion = "";
            valor       = "";
            mensaje     = "";
        }

        public Dato(string tipo, string descripcion, string valor)
        {
            this.tipo        = tipo;
            this.descripcion = descripcion;
            this.valor       = valor;
        }

    }
}