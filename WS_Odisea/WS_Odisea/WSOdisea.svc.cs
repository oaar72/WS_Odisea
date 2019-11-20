using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web.Services;
using WS_Odisea.Data;

namespace WS_Odisea
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IWSOdisea
    {
        [WebMethod]
        public string ping()
        {
            return "Servicio Activo";
        }

        [WebMethod]
        public string testDB()
        {
            string conexion = "";
            string resultado = "";

            Connection.Connection conn = new Connection.Connection();
            Person user = new Person();

            conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            try
            {
                con.Open();
                resultado = "Conexion exitosa";
                con.Close();
            }
            catch (Exception e)
            {
                resultado = "Error en la conexión " + e.StackTrace;
            }

            return resultado;
        }

        [WebMethod]
        public Person getPersona(string codUser)
        {
            Connection.Connection conn = new Connection.Connection();
            Person user = new Person();

            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();
            SqlCommand cmd = new SqlCommand("getPersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codUser", codUser));

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                user.idPersona = long.Parse(dr["cve_usuario"].ToString());
                user.codUser = dr["cod_usuario"].ToString();
                user.paterno = dr["paterno"].ToString();
                user.nombre = dr["nombre"].ToString();
                user.telefono = dr["telefono"].ToString();
            }
            return user;
        }

        [WebMethod]
        public Person addUser(string mail, string pass, string nombre, string paterno, string phone, string token)
        {
            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("addPersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@paterno", paterno));
            cmd.Parameters.Add(new SqlParameter("@codUser", mail));
            cmd.Parameters.Add(new SqlParameter("@password", pass));
            cmd.Parameters.Add(new SqlParameter("@phone", phone));
            cmd.Parameters.Add(new SqlParameter("@token", token));

            SqlDataReader dr = cmd.ExecuteReader();

            Person user = new Person();

            try
            {
                if (dr.Read())
                {
                    if (dr["ERROR"].ToString() == "")
                    {
                        user.idPersona = long.Parse(dr["cve_usuario"].ToString());
                        user.nombre = dr["nombre"].ToString();
                        user.paterno = dr["paterno"].ToString();
                        user.codUser = dr["cod_usuario"].ToString();
                        user.telefono = dr["telefono"].ToString();
                        user.token = dr["token"].ToString();
                        user.mensaje = " ";
                    }
                    else
                    {
                        user.mensaje = dr["ERROR"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                user.mensaje = "Error al invocar SP (addUser). " + e.StackTrace;
            }

            return user;
        }

        [WebMethod]
        public Person getUser(string username, string pass)
        {
            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("login", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codUser", username));
            cmd.Parameters.Add(new SqlParameter("@password", pass));

            SqlDataReader dr = cmd.ExecuteReader();

            Person user = new Person();

            try
            {
                if (dr.Read())
                {
                    if (dr["ERROR"].ToString() == "")
                    {
                        user.nombre = dr["nombre"].ToString();
                        user.paterno = dr["paterno"].ToString();
                        user.codUser = dr["cod_usuario"].ToString();
                        user.telefono = dr["telefono"].ToString();
                        user.token = dr["token"].ToString();
                        user.mensaje = " ";
                    }
                    else
                    {
                        user.mensaje = dr["ERROR"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                user.mensaje = "Error al invocar SP (login). " + e.StackTrace;
            }

            return user;
        }

        [WebMethod]
        public Contact addContact(string usuario, string nombre, string mail, string telefono,string descripcion)
        {
            Contact contacto = new Contact();

            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("addContact", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@mail", mail));
            cmd.Parameters.Add(new SqlParameter("@telefono", telefono));
            cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));

            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                if (dr.Read())
                {
                    if (dr["ERROR"].ToString() == "")
                    {
                        //contacto.cve_usuario = dr[""].ToString();
                        contacto.nombre = dr["nombre"].ToString();
                        contacto.telefono = dr["telefono"].ToString();
                        contacto.email = dr["email"].ToString();
                        contacto.mensaje = " ";
                    }
                    else
                    {
                        contacto.mensaje = dr["ERROR"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                contacto.mensaje = "Error al invocar SP (addContact). " + e.StackTrace;
            }

            return contacto;
        }

        [WebMethod]
        public List<string> getContactos(string codUser)
        {
            List<string> contactos = new List<string>();

            Connection.Connection conn = new Connection.Connection();

            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();
            SqlCommand cmd = new SqlCommand("getContact", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codUser", codUser));

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                contactos.Add(dr["nombre"].ToString());
            }
            return contactos;
        }

        [WebMethod]
        public Dato addDatoMedico(string valor, string descripcion, string usuario)
        {
            Dato dato = new Dato();

            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("addDatoMedico", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
            cmd.Parameters.Add(new SqlParameter("@Valor", valor));

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                dato.mensaje = "Error al invocar SP (addDatoMedico). " + e.StackTrace;
            }

            return dato;
        }

        [WebMethod]
        public List<string> traerGrupos()
        {
            List<string> Resultado = new List<string>();

            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("getGrupos", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            int cveClasificacion = 0;
            string descripcion = "";

            while (dr.Read())
            {
                cveClasificacion = int.Parse(dr["cve_clasificacion"].ToString());
                descripcion = dr["descripcion"].ToString();

                Resultado.Add(descripcion);
            }

            return Resultado;
        }

        private string generateProvitionalPass(int length)
        {
            string pass = "";
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            pass = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return pass;
        }

        [WebMethod]
        public string recoverPassword(string mail)
        {
            string mensaje = "";
            long cveUsuario = 0;

            Connection.Connection conn = new Connection.Connection();

            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();
            SqlCommand cmd = new SqlCommand("getPersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codUser", mail));

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                cveUsuario = long.Parse(dr["cve_usuario"].ToString());
            }

            dr.Close();

            if (cveUsuario == 0)
            {
                mensaje = "No se ha encontrado la dirección de correo electronico";
            }
            else
            {
                string pass = generateProvitionalPass(7);

                cmd = new SqlCommand("setPass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@pass", pass));
                cmd.Parameters.Add(new SqlParameter("@codUser", mail));

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = "Mensaje enviado, " + pass;
                }
                catch (Exception e)
                {
                    mensaje += e.StackTrace;
                }
            }
            return mensaje;
        }

        [WebMethod]
        public void updatePass(string codUser, string pass1, string pass2)
        {
            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("updatePass", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codUser", codUser));
            cmd.Parameters.Add(new SqlParameter("@pass1", pass1));
            cmd.Parameters.Add(new SqlParameter("@pass2", pass2));

            cmd.ExecuteNonQuery();
        }

        [WebMethod]
        public Person updatePersona(string nombre, string paterno, string numero, string codUser)
        {
            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("updatePersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@paterno", paterno));
            cmd.Parameters.Add(new SqlParameter("@codUser", codUser));
            cmd.Parameters.Add(new SqlParameter("@phone", numero));

            SqlDataReader dr = cmd.ExecuteReader();

            Person user = new Person();

            try
            {
                if (dr.Read())
                {
                    if (dr["ERROR"].ToString() == "")
                    {
                        user.idPersona = long.Parse(dr["cve_usuario"].ToString());
                        user.nombre = dr["nombre"].ToString();
                        user.paterno = dr["paterno"].ToString();
                        user.codUser = dr["cod_usuario"].ToString();
                        user.telefono = dr["telefono"].ToString();
                        user.mensaje = " ";
                    }
                    else
                    {
                        user.mensaje = dr["ERROR"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                user.mensaje = "Error al invocar SP (updateUser). " + e.StackTrace;
            }

            return user;
        }

        [WebMethod]
        public void deletePersona(string codUser)
        {
            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd  = new SqlCommand("deletePersona", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@codUser", codUser));

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
