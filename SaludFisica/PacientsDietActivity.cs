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
using Java.Lang;
using Newtonsoft.Json;
using Org.Apache.Http.Authentication;

namespace SaludFisica
{
    [Activity(Label = "PacientsDietActivity")]
    public class PacientsDietActivity : Activity
    {
        Button changePacientDietPB;
        Button cancelPacientDietUpdatePB;
        Button savePacientDietUpdatePB;

        Spinner name;
        EditText description;
        EditText nameET;
        EditText kcal;

        List<Dieta> dietas;

        Usuario pacient=new Usuario();

        Dieta diet=new Dieta();

        int authLevel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientDiet);

            pacient.id = Intent.GetIntExtra("Pacient",0);
            pacient.findAndLoad(this, pacient.id);
            authLevel = Intent.GetIntExtra("Auth", 4);
            diet.findAndLoad(this, pacient.paciente.idDieta);

            nameET = FindViewById<EditText>(Resource.Id.dietNameDetailsET);
            kcal = FindViewById<EditText>(Resource.Id.dietKcalDetailsET);


            

            name = FindViewById<Spinner>(Resource.Id.dietNameDetailsSP);
            description = FindViewById<EditText>(Resource.Id.dietDescriptionDetailsET);
            var dieta = new Dieta();
            dietas = dieta.all(this);
            nameET.Text = diet.name;
            kcal.Text = diet.kcal.ToString();
            ArrayAdapter<Dieta> adapter = new ArrayAdapter<Dieta>(this, Resource.Layout.support_simple_spinner_dropdown_item, dietas);
            name.Adapter = adapter;

            if (pacient.paciente.idDieta == 0)
            {
                pacient.paciente.idDieta = dietas[dietas.FindIndex(a=>a.name=="Sin dieta")].id;
            }
            
            name.SetSelection(dietas.FindIndex(a => a.id == pacient.paciente.idDieta));
            name.ItemSelected += dietSelectionChange;

            changePacientDietPB = FindViewById<Button>(Resource.Id.changePacientDietPB);
            changePacientDietPB.Click += changePacientDiet;

            if (authLevel == 4 || authLevel==3)
            {
                changePacientDietPB.Visibility = ViewStates.Invisible;
            }

            cancelPacientDietUpdatePB = FindViewById<Button>(Resource.Id.cancelPacientDietUpdatePB);
            cancelPacientDietUpdatePB.Click += cancelPacientDietUpdate;

            savePacientDietUpdatePB = FindViewById<Button>(Resource.Id.savePacientDietUpdatePB);
            savePacientDietUpdatePB.Click += savePacientDietUpdate;



            // Create your application here
        }


        private void dietSelectionChange(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            description.Text = dietas[e.Position].content;
            kcal.Text = dietas[e.Position].kcal.ToString();
        }

        private void savePacientDietUpdate(object sender, EventArgs e)
        {
            pacient.paciente.idDieta = dietas[name.SelectedItemPosition].id;
            pacient.paciente.save(this);
            var intent = new Intent(this, typeof(PacientsDietActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
            Finish();
        }

        private void cancelPacientDietUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsDietActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
            Finish();
        }

        private void changePacientDiet(object sender, EventArgs e)
        {
            changePacientDietPB.Visibility = ViewStates.Invisible;
            cancelPacientDietUpdatePB.Visibility = ViewStates.Visible;
            savePacientDietUpdatePB.Visibility = ViewStates.Visible;
            nameET.Visibility = ViewStates.Invisible;
            name.Visibility = ViewStates.Visible;
        }
    }
}