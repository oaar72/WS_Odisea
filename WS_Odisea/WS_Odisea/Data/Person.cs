using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WS_Odisea.Data
{
    [DataContract]
    public class Person
    {
        public long idPersona { get; set; }
        [DataMember]
        public string codUser { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string paterno { get; set; }
        [DataMember]
        public string materno { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string token { get; set; }

        public Person()
        {

        }

        public Person(string codUser, string nombre, string paterno, string materno, string numero, string mensaje, string token)
        {
            this.codUser = codUser;
            this.nombre = nombre;
            this.paterno = paterno;
            this.materno = materno;
            this.telefono = numero;
            this.token = token;
        }
    }
}