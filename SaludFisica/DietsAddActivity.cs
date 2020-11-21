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
using Android.Support.V7.Widget;

namespace SaludFisica
{
    [Activity(Label = "DietsAddActivity")]
    public class DietsAddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_dietas);
            var agregarDieta = FindViewById<Button>(Resource.Id.agregarDieta);
            agregarDieta.Click += AgregarDieta;
            // Create your application here
        }

        private void AgregarDieta(object sender, EventArgs e)
        {
            Dieta diet = new Dieta();
            var name = FindViewById<EditText>(Resource.Id.dietNameAdd);
            diet.name = name.Text;
            var content = FindViewById<EditText>(Resource.Id.dietDescriptionAdd);
            diet.content = content.Text;
            var kcal = FindViewById<EditText>(Resource.Id.dietKcalAdd);
            if (kcal.Text != "")
            {
                diet.kcal = int.Parse(kcal.Text);
            }
            else
            {
                diet.kcal = 0;
            }
            if (!(diet.name.Trim() != "" && diet.content.Trim() != ""))
            {
                Toast.MakeText(this, "Debe llenar todos los campos", ToastLength.Short).Show();
                return;
            }
            if (diet.findAndLoad(this, diet.name))
            {
                Toast.MakeText(this, "La dieta ya esta registrada", ToastLength.Short).Show();

            }
            else
            {
                diet.save(this);
                name.Text = "";
                content.Text = "";
                kcal.Text = "";
                Finish();
            }
        }
    }

    
}