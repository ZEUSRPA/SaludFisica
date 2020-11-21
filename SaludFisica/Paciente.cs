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
using Java.Util;
using MySql.Data.MySqlClient;

namespace SaludFisica
{
    class Paciente
    {
        private int _id;
        private int _idUser;
        private int _idDieta;
        private int _idRutina;
        private string _name;
        private int _edad;
        private string _ocupacion;
        private string _sexo;
        private string _domicilio;
        private string _telefono;


        public int id { get { return _id; } set { _id = value; } }
        public int idUser { get { return _idUser; } set { _idUser = value; } }
        public int idDieta { get { return _idDieta; } set { _idDieta = value; } }
        public int idRutina { get { return _idRutina; } set { _idRutina = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public int edad { get { return _edad; } set { _edad = value; } }
        public string ocupacion { get { return _ocupacion; } set { _ocupacion = value; } }
        public string sexo { get { return _sexo; } set { _sexo = value; } }
        public string domicilio { get { return _domicilio; } set { _domicilio = value; } }
        public string telefono { get { return _telefono; } set { _telefono = value; } }

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
                var cmd = new MySqlCommand(string.Format("SELECT * FROM pacientes WHERE ID_user = '{0}'", value), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    Toast.MakeText(context, "siiiii", ToastLength.Short).Show();
                    id = datos.GetInt32("ID");
                    idUser = datos.GetInt32("ID_user");
                    name = datos.GetString("Nombre");
                    edad = datos.GetInt32("Edad");
                    ocupacion = datos.GetString("Ocupacion");
                    sexo = datos.GetString("Sexo");
                    domicilio = datos.GetString("Domicilio");
                    telefono = datos.GetString("Telefono");
                    idDieta = datos.GetInt32("ID_dieta");
                    idRutina = datos.GetInt32("ID_rutina");

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
        public void save(Context context)
        {

            Conexion con = new Conexion();
            con.start(context);
            try
            {
                
                var cmd = new MySqlCommand(string.Format("UPDATE pacientes SET Nombre='{0}', Edad={1}, Ocupacion='{2}', Sexo='{3}', Domicilio='{4}', Telefono='{5}' WHERE ID='{6}'", name, edad, ocupacion,sexo,domicilio,telefono,id), con.getConnection());
                cmd.ExecuteNonQuery();
                if (idDieta != 0)
                {
                    cmd = new MySqlCommand(string.Format("UPDATE pacientes SET id_dieta={0} where id={1}", idDieta, id), con.getConnection());
                    cmd.ExecuteNonQuery();
                }
                if (idRutina != 0)
                {
                    cmd = new MySqlCommand(string.Format("UPDATE pacientes SET id_rutina={0} where id={1}", idRutina, id), con.getConnection());
                    cmd.ExecuteNonQuery();
                }
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }

        }
        public void updateDieta(Context context)
        {

            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("UPDATE pacientes SET ID_dieta='{0}' WHERE ID='{1}'", idDieta, id), con.getConnection());
                cmd.ExecuteNonQuery();
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }

        }

        public void saveRutineActivity(Context context, string fecha)
        {
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("INSERT INTO pac_rutinas (id_pac,fecha,id_rutina) VALUES('{0}','{1}','{2}')", id, fecha, idRutina), con.getConnection());
                cmd.ExecuteNonQuery();
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }
        }
        public void saveDietActivity(Context context,string fecha)
        {
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("INSERT INTO pac_dietas (id_pac,fecha,id_dieta) VALUES('{0}','{1}','{2}')", id, fecha,idDieta), con.getConnection());
                cmd.ExecuteNonQuery();
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }
        }

        public bool existRutineActivity(Context context, string fecha)
        {
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM pac_rutinas WHERE fecha='{0}' AND id_pac='{1}'", fecha, id), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                if (datos.Read())
                {
                    return true;
                }
                con.end(context);
                return false;
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
                return true;
            }
        }

        public bool existDietActivity(Context context,string fecha)
        {
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM pac_dietas WHERE fecha='{0}' AND id_pac='{1}'", fecha, id), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                if (datos.Read())
                {
                    return true;
                }
                con.end(context);
                return false;
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
                return true;
            }
        }

        public Dictionary<int,int> getKcalTot(Context context)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM pac_dietas WHERE id_pac='{0}'", id), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    if (result.ContainsKey(datos.GetInt32("id_dieta")))
                    {
                        result[datos.GetInt32("id_dieta")]++;
                    }
                    else
                    {
                        result.Add(datos.GetInt32("id_dieta"), 1);
                    }
                }
                con.end(context);
                return result;
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
                return result;
            }
        }

        public Dictionary<int,int> getKcalBurn(Context context)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("SELECT * FROM pac_rutinas WHERE id_pac='{0}'", id), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    if (result.ContainsKey(datos.GetInt32("id_rutina")))
                    {
                        result[datos.GetInt32("id_rutina")]++;
                    }
                    else
                    {
                        result.Add(datos.GetInt32("id_rutina"), 1);
                    }
                }
                con.end(context);
                return result;
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
                return result;
            }
        }

        public List<String> getHistoryDates(Context context)
        {
            List<String> list = new List<string>();
            HashSet<String> data = new HashSet<string>();
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return list;
            }
            try
            {
                
                var cmd = new MySqlCommand(string.Format("SELECT * FROM pac_dietas WHERE id_pac = {0}", id), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    data.Add(datos.GetString("fecha"));
                }
                datos.Close();
                cmd = new MySqlCommand(string.Format("SELECT * FROM pac_rutinas WHERE id_pac = {0}", id), con.getConnection());
                datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    data.Add(datos.GetString("fecha"));
                }
                datos.Close();
                con.end(context);
                foreach(var x in data)
                {
                    list.Add(x);
                }
                return list;
            }
            catch (Exception ex)
            {
                con.end(context);
                return list;
            }
        }

        //public List<Usuario> where(Context context, string value)
        //{
        //    List<Usuario> list = new List<Usuario>();
        //    Conexion con = new Conexion();
        //    if (!con.start(context))
        //    {
        //        Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
        //        return list;
        //    }
        //    try
        //    {
        //        var cmd = new MySqlCommand(string.Format("SELECT * FROM usuarios WHERE rol = '{0}'", value), con.getConnection());
        //        MySqlDataReader datos = cmd.ExecuteReader();
        //        //con.end(context);
        //        while (datos.Read())
        //        {
        //            Usuario u = new Usuario();
        //            u.userName = datos.GetString("userName");
        //            u.password = datos.GetString("password");
        //            u.rol = datos.GetString("rol");
        //            u.id = datos.GetInt32("ID");

        //            list.Add(u);
        //        }
        //        datos.Close();
        //        con.end(context);
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.end(context);
        //        return list;
        //    }
        //}
    }
}