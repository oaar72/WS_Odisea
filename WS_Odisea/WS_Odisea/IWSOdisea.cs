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
        Person addUser(string mail, string pass, string nombre, string paterno, string phone);

        [OperationContract]
        Person getUser(string username, string pass);
    }
}
