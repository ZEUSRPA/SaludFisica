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
    class Expediente
    {
        private int _id;
        private int _idPac;
        private int _peso;
        private int _talla;
        private int _imc;
        private int _cintura;
        private int _abdomen;
        private int _cadera;
        private int _grasa;
        private int _grasaVisceral;
        private int _musculo;
        private int _aguaPorcentaje;

        public int id { get { return _id; } set { _id = value; } }
        public int idPac { get { return _idPac; } set { _idPac = value; } }
        public int peso { get { return _peso; } set { _peso = value; } }
        public int talla { get { return _talla; } set { _talla = value; } }
        public int imc { get { return _imc; } set { _imc = value; } }
        public int cintura { get { return _cintura; } set { _cintura = value; } }
        public int abdomen { get { return _abdomen; } set { _abdomen = value; } }
        public int cadera { get { return _cadera; } set { _cadera = value; } }
        public int grasa { get { return _grasa; } set { _grasa = value; } }
        public int grasaVisceral { get { return _grasaVisceral; } set { _grasaVisceral = value; } }
        public int musculo { get { return _musculo; } set { _musculo = value; } }
        public int aguaPorcentaje { get { return _aguaPorcentaje; } set { _aguaPorcentaje = value; } }

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
                var cmd = new MySqlCommand(string.Format("SELECT * FROM expedientes WHERE ID_Pac = '{0}'", value), con.getConnection());
                MySqlDataReader datos = cmd.ExecuteReader();
                //con.end(context);
                while (datos.Read())
                {
                    Toast.MakeText(context, "siiiii", ToastLength.Short).Show();
                    id = datos.GetInt32("ID");
                    idPac = datos.GetInt32("ID_Pac");
                    peso = datos.GetInt32("Peso");
                    talla = datos.GetInt32("Talla");
                    imc = datos.GetInt32("IMC");
                    cintura = datos.GetInt32("Cintura");
                    abdomen = datos.GetInt32("Abdomen");
                    cadera = datos.GetInt32("Cadera");
                    grasa = datos.GetInt32("Grasa");
                    grasaVisceral = datos.GetInt32("Grasa_Visceral");
                    musculo = datos.GetInt32("Musculo");
                    aguaPorcentaje = datos.GetInt32("AguaPorcentaje");
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
                var cmd = new MySqlCommand(string.Format("UPDATE expedientes SET Peso='{0}', Talla='{1}', IMC='{2}', Cintura='{3}', Abdomen='{4}', Cadera='{5}', Grasa='{6}', Grasa_Visceral='{7}', Musculo='{8}', AguaPorcentaje='{9}' WHERE ID='{10}'", peso, talla, imc, cintura, abdomen, cadera, grasa,grasaVisceral,musculo,aguaPorcentaje,id), con.getConnection());
                cmd.ExecuteNonQuery();
                con.end(context);
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.ToString(), ToastLength.Long).Show();
                con.end(context);
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