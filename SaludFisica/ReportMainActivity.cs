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
    [Activity(Label = "ReportMainActivity")]
    public class ReportMainActivity : Activity
    {

        TextView dietDays;
        TextView rutineDays;
        TextView kcalTot;
        TextView kcalBurn;
        Usuario pacient=new Usuario();

        Dieta dietas=new Dieta();
        Rutina rutinas=new Rutina();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.reportMain);
            pacient.id = Intent.GetIntExtra("Pacient", 0);
            pacient.findAndLoad(this, pacient.id);

            var dietasList = dietas.all(this);
            var rutinasList = rutinas.all(this);

            Dictionary<int,int> diets=new Dictionary<int, int>();
            Dictionary<int,int> rutines=new Dictionary<int, int>();
            diets = pacient.paciente.getKcalTot(this);
            rutines = pacient.paciente.getKcalBurn(this);

            dietDays = FindViewById<TextView>(Resource.Id.dietDays);
            rutineDays = FindViewById<TextView>(Resource.Id.rutineDays);
            kcalTot = FindViewById<TextView>(Resource.Id.kcal);
            kcalBurn = FindViewById<TextView>(Resource.Id.kcalRutina);
            int auxiliar = 0;
            int days = 0;
            foreach(var x in rutines)
            {
                auxiliar += rutinasList[rutinasList.FindIndex(a => a.id == x.Key)].kcal*x.Value;
                days++;
            }

            rutineDays.Text = days.ToString();
            kcalBurn.Text = auxiliar.ToString();
            auxiliar = 0;
            days = 0;
            foreach (var x in diets)
            {
                auxiliar += dietasList[dietasList.FindIndex(a => a.id == x.Key)].kcal * x.Value;
                days++;
            }
            kcalTot.Text = auxiliar.ToString();
            dietDays.Text = days.ToString();

            var historyPB = FindViewById<Button>(Resource.Id.historyPB);
            historyPB.Click += history;


            // Create your application here
        }

        private void history(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HistoryMainActivity));
            intent.PutExtra("Pacient", pacient.id);
            this.StartActivity(intent);
        }
    }
}