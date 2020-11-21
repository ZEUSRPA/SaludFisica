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
using Newtonsoft.Json;

namespace SaludFisica
{
    [Activity(Label = "RutineDetailsActivity")]
    public class RutineDetailsActivity : Activity
    {
        EditText name;
        EditText description;
        EditText kcal;
        Rutina rutine=new Rutina();

        Button changeRutinePB;
        Button cancelRutineUpdatePB;
        Button saveRutineUpdatePB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.rutineDetails);
            name = FindViewById<EditText>(Resource.Id.rutineNameDetails);
            description = FindViewById<EditText>(Resource.Id.rutineDescriptionDetails);
            kcal = FindViewById<EditText>(Resource.Id.rutineKcalDetails);
            rutine.id = Intent.GetIntExtra("Rutine",0);
            rutine.findAndLoad(this, rutine.id);

            name.Text = rutine.name;
            description.Text = rutine.content;
            kcal.Text = rutine.kcal.ToString();

            changeRutinePB = FindViewById<Button>(Resource.Id.changeRutinePB);
            changeRutinePB.Click += changeRutine;

            cancelRutineUpdatePB = FindViewById<Button>(Resource.Id.cancelRutineUpdatePB);
            cancelRutineUpdatePB.Click += cancelRutineUpdate;

            saveRutineUpdatePB = FindViewById<Button>(Resource.Id.saveRutineUpdatePB);
            saveRutineUpdatePB.Click += saveRutineUpdate;


            // Create your application here
        }

        private void saveRutineUpdate(object sender, EventArgs e)
        {
            Dieta aux = new Dieta();
            aux.name = name.Text;
            aux.content = description.Text;
            if (kcal.Text != "")
            {
                aux.kcal = int.Parse(kcal.Text);
            }
            else
            {
                aux.kcal = 0;
            }
            if (rutine.update(this, aux))
            {
                var intent = new Intent(this, typeof(RutineDetailsActivity));
                intent.PutExtra("Rutine", rutine.id);
                this.StartActivity(intent);
                Finish();
            }

        }

        private void cancelRutineUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RutineDetailsActivity));
            intent.PutExtra("Rutine", rutine.id);
            this.StartActivity(intent);
            Finish();
        }

        private void changeRutine(object sender, EventArgs e)
        {
            name.Enabled = true;
            description.Enabled = true;
            kcal.Enabled = true;
            changeRutinePB.Visibility = ViewStates.Invisible;
            cancelRutineUpdatePB.Visibility = ViewStates.Visible;
            saveRutineUpdatePB.Visibility = ViewStates.Visible;
        }
    }
}