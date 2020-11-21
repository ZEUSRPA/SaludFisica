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
    [Activity(Label = "RutineAddActivity")]
    public class RutineAddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_rutinas);
            var agregarRutina = FindViewById<Button>(Resource.Id.agregarRutina);
            agregarRutina.Click += AgregarRutina;
            // Create your application here
        }

        private void AgregarRutina(object sender, EventArgs e)
        {
            Rutina rutine = new Rutina();
            var name = FindViewById<EditText>(Resource.Id.rutineNameAdd);
            rutine.name = name.Text;
            var content = FindViewById<EditText>(Resource.Id.rutineDescriptionAdd);
            rutine.content = content.Text;
            var kcal = FindViewById<EditText>(Resource.Id.rutineKcalAdd);
            if (kcal.Text != "")
            {
                rutine.kcal = int.Parse(kcal.Text);
            }
            else
            {
                rutine.kcal = 0;
            }
            if (!(rutine.name.Trim() != "" && rutine.content.Trim() != ""))
            {
                Toast.MakeText(this, "Debe llenar todos los campos", ToastLength.Short).Show();
                return;
            }
            if (rutine.findAndLoad(this, rutine.name))
            {
                Toast.MakeText(this, "La rutina ya esta registrada", ToastLength.Short).Show();

            }
            else
            {
                rutine.save(this);
                name.Text = "";
                content.Text = "";
                kcal.Text = "";
                Finish();
            }
        }
    }
}