using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public Contact addContact(string usuario, string nombre, string mail, string telefono)
        {
            Contact contacto = new Contact();

            Connection.Connection conn = new Connection.Connection();
            string conexion = conn.getConnectionString();

            SqlConnection con = new SqlConnection(conexion);

            con.Open();

            SqlCommand cmd = new SqlCommand("addPersona", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@mail", mail));
            cmd.Parameters.Add(new SqlParameter("@telefono", telefono));

            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                if (dr.Read())
                {
                    if (dr["ERROR"].ToString() == "")
                    {
                        contacto.cve_usuario = dr[""].ToString();
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

            if (dr.Read())
            {

                cveClasificacion = int.Parse(dr["cve_clasificacion"].ToString());
                descripcion = dr["descripcion"].ToString();

                Resultado.Add(descripcion);
            }
            else
            {
                Resultado.Add("Error");
            }

            return Resultado;
        }

    }

}
}
