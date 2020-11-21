using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.SE.Omapi;
using Android.Security.Keystore;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace SaludFisica
{
    class Usuario
    {
        private int _id;
        private string _userName;
        private string _password;
        private string _rol;

        public int id { get { return _id; } set { _id = value; } }
        public string userName { get { return _userName; } set { _userName = value; } }
        public string password { get { return _password; } set { _password = value; } }
        public string rol { get { return _rol; }set { _rol = value; } }
        public Paciente paciente = new Paciente();
        public Expediente expediente = new Expediente();

        public bool findAndLoad(Context context, int value)
        {
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return true;
            }
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM usuarios WHERE ID = '{0}'", value), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    Toast.MakeText(context, "siiiii", ToastLength.Short).Show();
                    id = datos.GetInt32("ID");
                    userName = datos.GetString("userName");
                    password = datos.GetString("password");
                    rol = datos.GetString("rol");
                    paciente.findAndLoad(context, id);
                    expediente.findAndLoad(context, paciente.id);
                    con.end(context);
                    return true;
                }
                con.end(context);
                return false;
            }
            catch (Exception ex)
            {
                con.end(context);
                return false;
            }

        }
        public bool findAndLoad(Context context,string value)
        {
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor",ToastLength.Short).Show();
                return true;
            }
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM usuarios WHERE userName = '{0}'",value),con.getConnection());
                MySqlDataReader datos=cmd.ExecuteReader();
                //con.end(context);
                while(datos.Read())
                {
                    Toast.MakeText(context, "siiiii", ToastLength.Short).Show();
                    id = datos.GetInt32("ID");
                    userName = datos.GetString("userName");
                    password = datos.GetString("password");
                    rol = datos.GetString("rol");
                    paciente.findAndLoad(context, id);
                    expediente.findAndLoad(context, paciente.id);
                    con.end(context);
                    return true;
                }
                con.end(context);
                return false;
            }catch(Exception ex) { 
                con.end(context);
                return false;
            }

        }
        public void save(Context context)
        {
            
            if (findAndLoad(context, this.userName))
            {
                Toast.MakeText(context, "Usuario no disponible",ToastLength.Short).Show();
                return;
            }
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("INSERT INTO usuarios (username,password,rol) VALUES ('{0}','{1}','{2}')",userName,password,rol),con.getConnection());
                

                if (cmd.ExecuteNonQuery()>0)
                {
                    findAndLoad(context, this.userName);

                }
                var cmd2 = new MySqlCommand(string.Format("INSERT INTO pacientes (Edad,Nombre,ID_user) VALUES (0,'No asignado','{0}')", id), con.getConnection());

                if (cmd2.ExecuteNonQuery() > 0)
                {
                    this.paciente.findAndLoad(context,id);
                }

                var cmd3 = new MySqlCommand(string.Format("INSERT INTO expedientes (ID_Pac) VALUES ('{0}')", paciente.id), con.getConnection());
                if (cmd3.ExecuteNonQuery() > 0)
                {
                    this.expediente.findAndLoad(context, paciente.id);
                    Toast.MakeText(context, "Usuario Creado", ToastLength.Short).Show();

                }
                con.end(context);
            }
            catch(Exception ex) 
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }
            
        }

        public bool update(Context context,Usuario aux)
        {
            
            if (aux.userName!=userName && findAndLoad(context, aux.userName))
            {
                Toast.MakeText(context, "Usuario no disponible", ToastLength.Short).Show();
                return false;
            }
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("UPDATE usuarios SET username='{0}', password='{1}' WHERE ID='{2}'", aux.userName, aux.password,id), con.getConnection());


                if (cmd.ExecuteNonQuery() > 0)
                {
                    userName = aux.userName;
                    password = aux.password;
                    Toast.MakeText(context, "Usuario Actualizado", ToastLength.Short).Show();
                }
                con.end(context);
                return true;
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
                return false;
            }
        }

        public List<Usuario> where(Context context,string value)
        {
            List<Usuario> list = new List<Usuario>();
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return list;
            }
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM usuarios WHERE rol = '{0}'", value), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    Usuario u = new Usuario();
                    u.userName = datos.GetString("userName");
                    u.password = datos.GetString("password");
                    u.rol = datos.GetString("rol");
                    u.id = datos.GetInt32("ID");
                    list.Add(u);
                }
                datos.Close();
                con.end(context);
                return list;
            }
            catch (Exception ex)
            {
                con.end(context);
                return list;
            }
        }

        public override string ToString()
        {
            return userName;
        }

        public List<Usuario> all(Context context)
        {
            List<Usuario> list = new List<Usuario>();
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return list;
            }
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM usuarios"), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    Usuario u = new Usuario();
                    u.userName = datos.GetString("userName");
                    u.password = datos.GetString("password");
                    u.rol = datos.GetString("rol");
                    u.id = datos.GetInt32("ID");
                    list.Add(u);
                }
                datos.Close();
                con.end(context);
                return list;
            }
            catch (Exception ex)
            {
                con.end(context);
                return list;
            }
        }
    }
}