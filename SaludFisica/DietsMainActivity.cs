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
    [Activity(Label = "DietsMainActivity")]
    public class DietsMainActivity : Activity
    {
        List<Dieta> dietas;
        ListView list;
        Intent intent;
        int authLevel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dietasMain);
            intent = new Intent(this, typeof(DietsDetailsActivity));
            authLevel = Intent.GetIntExtra("Auth", 4);
            list = FindViewById<ListView>(Resource.Id.listDietas);
            list.ItemClick += itemDietClick;
            var newDiet = FindViewById<Button>(Resource.Id.newDietButton);
            newDiet.Click += newDiets;

            if (authLevel == 4 || authLevel == 3)
            {
                newDiet.Visibility = ViewStates.Invisible;
            }
            // Create your application here
        }

        protected override void OnStart()
        {
            base.OnStart();
            var diet = new Dieta();
            dietas = diet.all(this);
            list.Adapter = new DietaAdapter(this, Resource.Layout.itemDiet, dietas);
            
        }

        private void newDiets(object sender, EventArgs e)
        {
            var intentAdd = new Intent(this, typeof(DietsAddActivity));
            this.StartActivity(intentAdd);
        }

        private void itemDietClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var d = dietas[e.Position];
            intent.PutExtra("Diet", d.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
        }
    }
}