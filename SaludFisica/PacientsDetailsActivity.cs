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
using Org.Apache.Http.Authentication;

namespace SaludFisica
{
    [Activity(Label = "PacientsDetailsActivity")]
    public class PacientsDetailsActivity : Activity
    {
        Usuario pacient=new Usuario();
        Dieta diet=new Dieta();
        Rutina rutine = new Rutina();

        int authLevel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientDetails);
            pacient.id = Intent.GetIntExtra("Pacient",0);
            authLevel = Intent.GetIntExtra("Auth", 4);

            var userDetails = FindViewById<Button>(Resource.Id.userDetailsPB);
            userDetails.Click += userDetailsPB;

            var personalDataDetails = FindViewById<Button>(Resource.Id.personalDataDetailsPB);
            personalDataDetails.Click += personalDataDetailsPB;

            var recordDetails = FindViewById<Button>(Resource.Id.recordDetailsPB);
            recordDetails.Click += recordDetailsPB;

            var dietDetailsPB = FindViewById<Button>(Resource.Id.dietDetailsPB);
            dietDetailsPB.Click += dietDetails;

            var rutineDetailsPB = FindViewById<Button>(Resource.Id.rutineDetailsPB);
            rutineDetailsPB.Click += rutineDetails;

            var reportPB = FindViewById<Button>(Resource.Id.reportPB);
            reportPB.Click += viewReport;
            // Create your application here
        }

        private void viewReport(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ReportMainActivity));
            intent.PutExtra("Pacient", pacient.id);
            this.StartActivity(intent);
        }

        protected override void OnStart()
        {
            base.OnStart();
            pacient.findAndLoad(this, pacient.id);

            diet.findAndLoad(this, pacient.paciente.idDieta);
            rutine.findAndLoad(this, pacient.paciente.idRutina);

            var userName = FindViewById<EditText>(Resource.Id.userNameDetailsET);
            userName.Text = pacient.userName;



            var pacientNameDetails = FindViewById<EditText>(Resource.Id.pacientNameDetails);
            pacientNameDetails.Text = pacient.paciente.name;

            var pacientNameRecordDetails = FindViewById<EditText>(Resource.Id.pacientNameRecordDetails);
            pacientNameRecordDetails.Text = pacient.paciente.name;

            var pacientNameDietDetails = FindViewById<EditText>(Resource.Id.dietNameDetailsET);
            pacientNameDietDetails.Text = diet.name;

            var pacientNameRutineDetails = FindViewById<EditText>(Resource.Id.rutineNameDetailsET);
            pacientNameRutineDetails.Text = rutine.name;
        }

        private void rutineDetails(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsRutineActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
        }

        private void dietDetails(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsDietActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
        }

        public void userDetailsPB(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsUserActivity));
            intent.PutExtra("User", pacient.id);
            this.StartActivity(intent);

        }
        public void recordDetailsPB(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientRecordActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
        }

        public void personalDataDetailsPB(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientPersonalDataActivity));
            intent.PutExtra("Pacient", pacient.id);
            this.StartActivity(intent);
        }
    }
}