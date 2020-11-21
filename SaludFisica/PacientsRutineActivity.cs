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
    [Activity(Label = "PacientRutineActivity")]
    public class PacientsRutineActivity : Activity
    {
        Button changePacientRutinePB;
        Button cancelPacientRutineUpdatePB;
        Button savePacientRutineUpdatePB;

        Spinner name;
        EditText description;
        EditText nameET;
        EditText kcal;

        List<Rutina> rutinas;

        Usuario pacient=new Usuario();

        Rutina rutine = new Rutina();

        int authLevel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientRutine);

            pacient.id =Intent.GetIntExtra("Pacient",0);
            pacient.findAndLoad(this, pacient.id);
            authLevel = Intent.GetIntExtra("Auth", 4);
            rutine.findAndLoad(this, pacient.paciente.idRutina);

            nameET = FindViewById<EditText>(Resource.Id.rutineNameDetailsET);
            kcal = FindViewById<EditText>(Resource.Id.rutineKcalDetailsET);


            name = FindViewById<Spinner>(Resource.Id.rutineNameDetailsSP);
            description = FindViewById<EditText>(Resource.Id.rutineDescriptionDetailsET);
            var rutina = new Rutina();
            rutinas = rutina.all(this);
            nameET.Text = rutine.name;
            kcal.Text = rutine.kcal.ToString();  
            ArrayAdapter<Rutina> adapter = new ArrayAdapter<Rutina>(this, Resource.Layout.support_simple_spinner_dropdown_item, rutinas);
            name.Adapter = adapter;

            if (pacient.paciente.idRutina == 0)
            {
                pacient.paciente.idRutina = rutinas[rutinas.FindIndex(a => a.name == "Sin Rutina")].id;
            }

            name.SetSelection(rutinas.FindIndex(a => a.id == pacient.paciente.idRutina));
            name.ItemSelected += rutineSelectionChange;

            changePacientRutinePB = FindViewById<Button>(Resource.Id.changePacientRutinePB);
            changePacientRutinePB.Click += changePacientRutine;

            if (authLevel == 4 || authLevel == 2)
            {
                changePacientRutinePB.Visibility = ViewStates.Invisible;
            }

            cancelPacientRutineUpdatePB = FindViewById<Button>(Resource.Id.cancelPacientRutineUpdatePB);
            cancelPacientRutineUpdatePB.Click += cancelPacientRutineUpdate;

            savePacientRutineUpdatePB = FindViewById<Button>(Resource.Id.savePacientRutineUpdatePB);
            savePacientRutineUpdatePB.Click += savePacientRutineUpdate;



            // Create your application here
        }


        private void rutineSelectionChange(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            description.Text = rutinas[e.Position].content;
            kcal.Text = rutinas[e.Position].kcal.ToString();
        }

        private void savePacientRutineUpdate(object sender, EventArgs e)
        {
            pacient.paciente.idRutina = rutinas[name.SelectedItemPosition].id;
            pacient.paciente.save(this);
            var intent = new Intent(this, typeof(PacientsRutineActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
            Finish();
        }

        private void cancelPacientRutineUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsRutineActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
            Finish();
        }

        private void changePacientRutine(object sender, EventArgs e)
        {
            changePacientRutinePB.Visibility = ViewStates.Invisible;
            cancelPacientRutineUpdatePB.Visibility = ViewStates.Visible;
            savePacientRutineUpdatePB.Visibility = ViewStates.Visible;
            nameET.Visibility = ViewStates.Invisible;
            name.Visibility = ViewStates.Visible;
        }
    }
}