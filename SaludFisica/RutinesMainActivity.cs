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
    [Activity(Label = "RutinesMainActivity")]
    public class RutinesMainActivity : Activity
    {
        List<Rutina> rutinas;
        ListView list;
        Intent intent;
        int authLevel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.rutinesMain);
            intent = new Intent(this, typeof(RutineDetailsActivity));

            authLevel = Intent.GetIntExtra("Auth", 4);
            list = FindViewById<ListView>(Resource.Id.listRutines);
            list.ItemClick += itemRutineClick;
            var newRutine = FindViewById<Button>(Resource.Id.newRutineButton);
            newRutine.Click += newRutines;
            if (authLevel == 4 || authLevel == 2)
            {
                newRutine.Visibility = ViewStates.Invisible;
            }
            // Create your application here
        }

        protected override void OnStart()
        {
            base.OnStart();
            var rutine = new Rutina();
            rutinas = rutine.all(this);
            list.Adapter = new RutinaAdapter(this, Resource.Layout.itemRutine, rutinas);

        }

        private void newRutines(object sender, EventArgs e)
        {
            var intentAdd = new Intent(this, typeof(RutineAddActivity));
            this.StartActivity(intentAdd);
        }

        private void itemRutineClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var d = rutinas[e.Position];
            intent.PutExtra("Rutine", d.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
        }
    }
}