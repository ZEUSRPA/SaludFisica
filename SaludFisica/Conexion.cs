using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telecom;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace SaludFisica
{
    class Conexion
    {
        private MySqlConnection connection;
        public bool start(Context context)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Port = 3306;
            builder.Server = "bj8axwm2jf6buo4dcrpf-mysql.services.clever-cloud.com";
            builder.Database = "bj8axwm2jf6buo4dcrpf";
            builder.UserID = "uzvsrmhiatyjipbk";
            builder.Password = "vjwmoGBSSg0AiMe61JjV";
            try
            {
                connection = new MySqlConnection(builder.ToString());
                connection.Open();
                //Toast.MakeText(context,"Everything is good", ToastLength.Long).Show();
                return true;


            }catch(Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                return false;
            }
        }

        public void end(Context context)
        {
            try
            {
                connection.Close();
                //Toast.MakeText(context, "The conexion is close", ToastLength.Short).Show();
            }
            catch(Exception ex)
            {
                connection.Close();
                Toast.MakeText(context, ex.ToString(), ToastLength.Short).Show();
            }
        }
        
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}