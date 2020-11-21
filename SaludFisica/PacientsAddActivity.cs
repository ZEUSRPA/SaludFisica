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

namespace SaludFisica
{
    [Activity(Label = "PacientsAddActivity")]
    public class PacientsAddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_user);

            var agregarUsuario = FindViewById<Button>(Resource.Id.agregarNuevoPaciente);
            agregarUsuario.Click += Agregar;
            // Create your application here
        }

        private void Agregar(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            var usuario = FindViewById<EditText>(Resource.Id.usernameAgregarPaciente);
            user.userName = usuario.Text;
            var password = FindViewById<EditText>(Resource.Id.passwordAgregarPaciente);
            user.password = password.Text;
            var confirmation = FindViewById<EditText>(Resource.Id.passwordConfirm);
            if (!(user.password.Trim() != "" && user.password == confirmation.Text))
            {
                Toast.MakeText(this, "Las claves no coinciden o son nulas", ToastLength.Short).Show();
                return;
            }
            if (user.findAndLoad(this, user.userName))
            {
                Toast.MakeText(this, "El usuario ya esta registrado", ToastLength.Short).Show();
            }
            else
            {
                user.rol = "paciente";
                user.save(this);
                usuario.Text = "";
                password.Text = "";
                confirmation.Text = "";
                Finish();
            }


        }
    }
}