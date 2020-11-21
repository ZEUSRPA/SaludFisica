using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace SaludFisica
{
    class ZMessage
    {
        public int id { get; set; }
        public int idRemitente { get; set; }
        public int idDestinatario { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        public List<ZMessage> loadSent(Context context,int userId)
        {
            Conexion con = new Conexion();
            List<ZMessage> mensajes = new List<ZMessage>();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return mensajes;
            }
            try
            {

                var cmd = new MySqlCommand(string.Format("SELECT * FROM messages WHERE id_remitente = {0}", userId), con.getConnection());
                MySqlDataReader aux = cmd.ExecuteReader();
                while (aux.Read())
                {
                    ZMessage a = new ZMessage();
                    a.id = aux.GetInt32("ID");
                    a.idDestinatario = aux.GetInt32("id_destinatario");
                    a.idRemitente = aux.GetInt32("id_remitente");
                    a.subject = aux.GetString("asunto");
                    a.message = aux.GetString("mensaje");
                    mensajes.Add(a);
                }
                con.end(context);
                return mensajes;
            }
            catch
            {
                con.end(context);
                Toast.MakeText(context, "Ha ocurrido un error", ToastLength.Short).Show();
                return mensajes;
            }

        }

        public List<ZMessage> loadReceived(Context context, int userId)
        {
            Conexion con = new Conexion();
            List<ZMessage> mensajes = new List<ZMessage>();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return mensajes;
            }
            try
            {

                var cmd = new MySqlCommand(string.Format("SELECT * FROM messages WHERE id_destinatario = {0}", userId), con.getConnection());
                MySqlDataReader aux = cmd.ExecuteReader();
                while (aux.Read())
                {
                    ZMessage a = new ZMessage();
                    a.id = aux.GetInt32("ID");
                    a.idDestinatario = aux.GetInt32("id_destinatario");
                    a.idRemitente = aux.GetInt32("id_remitente");
                    a.subject = aux.GetString("asunto");
                    a.message = aux.GetString("mensaje");
                    mensajes.Add(a);
                }
                con.end(context);
                return mensajes;
            }
            catch
            {
                con.end(context);
                Toast.MakeText(context, "Ha ocurrido un error", ToastLength.Short).Show();
                return mensajes;
            }

        }

        public void load(Context context)
        {
            Conexion con = new Conexion();
            if (!con.start(context))
            {
                Toast.MakeText(context, "No hay conexion con el servidor", ToastLength.Short).Show();
                return;
            }
            try
            {

                var cmd = new MySqlCommand(string.Format("SELECT * FROM messages WHERE ID = {0}", id), con.getConnection());
                MySqlDataReader aux = cmd.ExecuteReader();
                if (aux.Read())
                {
                    id = aux.GetInt32("ID");
                    idDestinatario = aux.GetInt32("id_destinatario");
                    idRemitente = aux.GetInt32("id_remitente");
                    subject = aux.GetString("asunto");
                    message = aux.GetString("mensaje");
                }
                con.end(context);
                return;
            }
            catch
            {
                con.end(context);
                Toast.MakeText(context, "Ha ocurrido un error", ToastLength.Short).Show();
                return;
            }
        }
        

        public void sendMessage(Context context)
        {
            Conexion con = new Conexion();
            con.start(context);
            try
            {
                var cmd = new MySqlCommand(string.Format("INSERT INTO messages (id_remitente,id_destinatario,asunto,mensaje) VALUES ({0},{1},'{2}','{3}')", idRemitente, idDestinatario, subject, message), con.getConnection());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Toast.MakeText(context, "Mensaje Enviado", ToastLength.Short).Show();
                }
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
            }
        }

        //public List<Rutina> all(Context context)
        //{
        //    List<Rutina> lista = new List<Rutina>();
        //    Conexion con = new Conexion();
        //    con.start(context);
        //    MySqlDataReader data = null;
        //    try
        //    {

        //        var cmd = new MySqlCommand(string.Format("SELECT * FROM rutinas"), con.getConnection());
        //        data = cmd.ExecuteReader();
        //        while (data.Read())
        //        {
        //            Rutina aux = new Rutina();
        //            aux.id = data.GetInt32("ID");
        //            aux.kcal = data.GetInt32("kcal");
        //            aux.name = data.GetString("Nombre");
        //            aux.content = data.GetString("Contenido");
        //            lista.Add(aux);
        //        }
        //        data.Close();
        //        con.end(context);
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        con.end(context);
        //        Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
        //        return lista;
        //    }

        //}

        
    }
}