using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace SaludFisica
{
    class Dieta
    {
        public int id { get; set; }
        public int kcal { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        
        public bool findAndLoad(Context context,string value)
        {
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No ha conexion con el servidor",ToastLength.Short).Show();
                return true;
            }
            try
            {

                var cmd = new MySqlCommand(string.Format("SELECT * FROM dietas WHERE Nombre = '{0}'",value),con.getConnection());
                MySqlDataReader aux = cmd.ExecuteReader();
                if (aux.Read())
                {
                    id = aux.GetInt32("ID");
                    kcal = aux.GetInt32("kcal");
                    name = aux.GetString("Nombre");
                    content = aux.GetString("Contenido");
                    con.end(context);
                    return true;
                }
                else
                {
                    con.end(context);
                    return false;
                }

            }
            catch
            {
                con.end(context);
                Toast.MakeText(context, "Ha ocurrido un error", ToastLength.Short).Show();
                return true;
            }

        }
        public bool findAndLoad(Context context, int value)
        {
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No ha conexion con el servidor", ToastLength.Short).Show();
                return true;
            }
            try
            {

                var cmd = new MySqlCommand(string.Format("SELECT * FROM dietas WHERE id = {0}", value), con.getConnection());
                MySqlDataReader aux = cmd.ExecuteReader();
                if (aux.Read())
                {
                    id = aux.GetInt32("ID");
                    kcal = aux.GetInt32("kcal");
                    name = aux.GetString("Nombre");
                    content = aux.GetString("Contenido");
                    con.end(context);
                    return true;
                }
                else
                {
                    con.end(context);
                    return false;
                }

            }
            catch
            {
                con.end(context);
                Toast.MakeText(context, "Ha ocurrido un error", ToastLength.Short).Show();
                return true;
            }

        }

        public void save(Context context)
        {
            if (findAndLoad(context, this.name))
            {
                Toast.MakeText(context, "El nombre ya esta en uso", ToastLength.Short).Show();
                return;
            }
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("INSERT INTO dietas (nombre,contenido,kcal) VALUES ('{0}','{1}',{2})", name, content,kcal), con.getConnection());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Toast.MakeText(context, "Dieta Agregada", ToastLength.Short).Show();
                }
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }
        }

        public List<Dieta> all(Context context)
        {
            List<Dieta> lista = new List<Dieta>();
            Conexion con = new Conexion();
            con.start(context);
            MySqlDataReader data=null;
            try
            {
                
                var cmd = new MySqlCommand(string.Format("SELECT * FROM dietas"),con.getConnection());
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    Dieta aux = new Dieta();
                    aux.id = data.GetInt32("ID");
                    aux.kcal = data.GetInt32("kcal");
                    aux.name = data.GetString("Nombre");
                    aux.content = data.GetString("Contenido");
                    lista.Add(aux);
                }
                data.Close();
                con.end(context);
                return lista;
            }
            catch(Exception ex)
            {
                con.end(context);
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                return lista;
            }

        }
        
        public override string ToString()
        {
            return name;
        }
        public bool update(Context context, Dieta value)
        {
            if (!findAndLoad(context, this.id))
            {
                Toast.MakeText(context, "La dieta no existe", ToastLength.Short).Show();
                return false;
            }
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                if(this.name != value.name)
                {
                    if(findAndLoad(context, value.name))
                    {
                        Toast.MakeText(context, "El nombre ya esta asignado", ToastLength.Short).Show();
                        return false;
                    }
                }
                var cmd = new MySqlCommand(string.Format("UPDATE dietas SET nombre = '{0}', contenido = '{1}', kcal={2} WHERE id = {3}",value.name,value.content,value.kcal,id), con.getConnection());
                

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Toast.MakeText(context, "Dieta Agregada", ToastLength.Short).Show();
                    this.name = value.name;
                    this.content = value.content;
                    this.kcal = value.kcal;
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

    }
}