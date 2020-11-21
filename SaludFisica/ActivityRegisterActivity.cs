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
using Java.Sql;

namespace SaludFisica
{
    [Activity(Label = "ActivityRegisterActivity")]
    public class ActivityRegisterActivity : Activity
    {
        Usuario pacient=new Usuario();
        CheckBox diet;
        CheckBox rutine;
        Button saveActivityPB;
        TextView date;

        string fecha="";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activityRegister);
            pacient.id = Intent.GetIntExtra("Pacient", 0);
            fecha = Intent.GetStringExtra("Date");
            pacient.findAndLoad(this, pacient.id);
            diet = FindViewById<CheckBox>(Resource.Id.dietCheck);
            rutine = FindViewById<CheckBox>(Resource.Id.rutineCheck);
            date = FindViewById<TextView>(Resource.Id.dateActivityRegister);
            saveActivityPB = FindViewById<Button>(Resource.Id.saveActivity);
            saveActivityPB.Click += saveActivity;

            if (fecha == "")
            {
                if (pacient.paciente.existDietActivity(this, DateTime.Today.ToString("dd-MM-yyyy")))
                {
                    diet.Checked = true;
                    diet.Enabled = false;
                    rutine.Enabled = false;
                    saveActivityPB.Enabled = false;
                }
                if (pacient.paciente.existRutineActivity(this, DateTime.Today.ToString("dd-MM-yyyy")))
                {
                    rutine.Checked = true;
                    diet.Enabled = false;
                    rutine.Enabled = false;
                    saveActivityPB.Enabled = false;
                }
                date.Text = DateTime.Today.ToString("dd-MM-yyyy");
            }
            else
            {
                if (pacient.paciente.existDietActivity(this, fecha))
                {
                    diet.Checked = true;
                    saveActivityPB.Enabled = false;
                }
                if (pacient.paciente.existRutineActivity(this, fecha))
                {
                    rutine.Checked = true;
                    saveActivityPB.Enabled = false;
                }
                diet.Enabled = false;
                rutine.Enabled = false;
                saveActivityPB.Visibility = ViewStates.Invisible;
                date.Text = fecha;
            }



            
            // Create your application here
        }

        private void saveActivity(object sender, EventArgs e)
        {
            if (diet.Checked)
            {
                pacient.paciente.saveDietActivity(this, DateTime.Today.ToString("dd-MM-yyyy"));
            }
            if (rutine.Checked)
            {
                pacient.paciente.saveRutineActivity(this, DateTime.Today.ToString("dd-MM-yyyy"));
            }
            var intent = new Intent(this, typeof(ActivityRegisterActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Data","");
            this.StartActivity(intent);
            Finish();
        }
    }
}