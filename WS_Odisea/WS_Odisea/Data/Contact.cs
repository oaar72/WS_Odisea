using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WS_Odisea.Data
{
    public class Contact
    {
        public string cve_usuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string mensaje { get; set; }

        public Contact()
        {
            cve_usuario = "";
            nombre      = "";
            telefono    = "";
            email       = "";
            mensaje     = "";
        }

        public Contact(string cve_usuario, string nombre, string telefono, string email)
        {
            this.cve_usuario    = cve_usuario ;
            this.nombre         = nombre;
            this.telefono       = telefono;
            this.email          = email;
        }
    }
}