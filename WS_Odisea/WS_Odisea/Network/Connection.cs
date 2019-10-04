using System.Configuration;
using System.Runtime.Serialization;

namespace WS_Odisea.Connection
{
    public class Connection
    {
        private string connectionString;
        private string server;
        private string username;
        private string password;

        public Connection()
        {
            server = ConfigurationManager.AppSettings["ServerDB"].ToString();
            username = ConfigurationManager.AppSettings["UserDB"].ToString();
            password = ConfigurationManager.AppSettings["PassDB"].ToString();

            connectionString = "Server=tcp:" + server + ",1433;Initial Catalog=OdiseaDB;Persist Security Info=False; " +
                   "User ID = " + username + ";Password=" + password + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public string getConnectionString()
        {
            return this.connectionString;
        }

        public string getUsername()
        {
            return this.username;
        }

        public string getPassword()
        {
            return this.password;
        }
    }
}