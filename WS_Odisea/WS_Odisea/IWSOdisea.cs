using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WS_Odisea.Data;

namespace WS_Odisea
{
    [ServiceContract]
    public interface IWSOdisea
    {
        [OperationContract]
        string ping();

        [OperationContract]
        string testDB();

        [OperationContract]
        Person getPersona(string codUser);

        [OperationContract]
        Person addUser(string mail, string pass, string nombre, string paterno, string phone, string token);

        [OperationContract]
        Person getUser(string username, string pass);

        [OperationContract]
        string getInfo(string codUser);

        [OperationContract]
        Contact addContact(string usuario, string nombre, string mail, string telefono, string descripcion);

        [OperationContract]
        List<string> getContactos(string codUser);

        [OperationContract]
        List<Contact> getContacts(string codUser);

        [OperationContract]
        Dato addDatoMedico(string valor, string descripcion, string usuario);

        [OperationContract]
        List<string> traerGrupos();

        [OperationContract]
        string recoverPassword(string mail);

        [OperationContract]
        Person updatePersona(string nombre, string paterno, string numero, string codUser);

        [OperationContract]
        void updatePass(string codUser, string pass1, string pass2);

        [OperationContract]
        void deletePersona(string codUser);
    }
}
