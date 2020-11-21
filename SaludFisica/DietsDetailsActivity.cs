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
    [Activity(Label = "DietsDetailsActivity")]
    public class DietsDetailsActivity : Activity
    {
        EditText name;
        EditText description;
        EditText kcal;
        Dieta diet=new Dieta();

        Button changeDietPB;
        Button cancelDietUpdatePB;
        Button saveDietUpdatePB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dietDetails);
            name = FindViewById<EditText>(Resource.Id.dietNameDetails);
            description = FindViewById<EditText>(Resource.Id.dietDescriptionDetails);
            kcal = FindViewById<EditText>(Resource.Id.dietKcalDetails);
            diet.id = Intent.GetIntExtra("Diet",0);
            diet.findAndLoad(this, diet.id);
            name.Text = diet.name;
            description.Text = diet.content;
            kcal.Text = diet.kcal.ToString();

            changeDietPB = FindViewById<Button>(Resource.Id.changeDietPB);
            changeDietPB.Click += changeDiet;

            cancelDietUpdatePB = FindViewById<Button>(Resource.Id.cancelDietUpdatePB);
            cancelDietUpdatePB.Click += cancelDietUpdate;

            saveDietUpdatePB = FindViewById<Button>(Resource.Id.saveDietUpdatePB);
            saveDietUpdatePB.Click += saveDietUpdate;
            

            // Create your application here
        }

        private void saveDietUpdate(object sender, EventArgs e)
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
            
            if(diet.update(this, aux))
            {
                var intent = new Intent(this, typeof(DietsDetailsActivity));
                intent.PutExtra("Diet", diet.id);
                this.StartActivity(intent);
                Finish();
            }

        }

        private void cancelDietUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(DietsDetailsActivity));
            intent.PutExtra("Diet", diet.id);
            this.StartActivity(intent);
            Finish();
        }

        private void changeDiet(object sender, EventArgs e)
        {
            name.Enabled = true;
            description.Enabled = true;
            kcal.Enabled = true;
            changeDietPB.Visibility = ViewStates.Invisible;
            cancelDietUpdatePB.Visibility = ViewStates.Visible;
            saveDietUpdatePB.Visibility = ViewStates.Visible;
        }
    }
}