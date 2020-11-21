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
    [Activity(Label = "PacientRecordActivity")]
    public class PacientRecordActivity : Activity
    {
        EditText name;
        EditText weight;
        EditText size;
        EditText imc;
        EditText waist;
        EditText abdomen;
        EditText hip;
        EditText fat;
        EditText visceralFat;
        EditText muscle;
        EditText waterPercentage;
        Usuario user=new Usuario();

        Button saveRecordUpdatePB;
        Button cancelRecordUpdatePB;
        Button changeRecordPB;

        int authLevel;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientRecord);
            user.id = Intent.GetIntExtra("Pacient",0);
            authLevel = Intent.GetIntExtra("Auth", 4);
            user.findAndLoad(this, user.id);
            name = FindViewById<EditText>(Resource.Id.pacientNameRecordDetailsET);
            weight = FindViewById<EditText>(Resource.Id.pacientWeightDetailsET);
            size = FindViewById<EditText>(Resource.Id.pacientSizeDetailsET);
            imc = FindViewById<EditText>(Resource.Id.pacientImcDetailsET);
            waist = FindViewById<EditText>(Resource.Id.pacientWaistDetailsET);
            abdomen = FindViewById<EditText>(Resource.Id.pacientAbdomenDetailsET);
            hip = FindViewById<EditText>(Resource.Id.pacientHipDetailsET);
            fat = FindViewById<EditText>(Resource.Id.pacientFatDetailsET);
            visceralFat = FindViewById<EditText>(Resource.Id.pacientVisceralFatDetailsET);
            muscle = FindViewById<EditText>(Resource.Id.pacientMuscleDetailsET);
            waterPercentage = FindViewById<EditText>(Resource.Id.pacientWaterPercentageDetailsET);

            name.Text = user.paciente.name;
            weight.Text = user.expediente.peso.ToString();
            size.Text = user.expediente.talla.ToString();
            imc.Text = user.expediente.imc.ToString();
            waist.Text = user.expediente.cintura.ToString();
            abdomen.Text = user.expediente.abdomen.ToString();
            hip.Text = user.expediente.cadera.ToString();
            fat.Text = user.expediente.grasa.ToString();
            visceralFat.Text = user.expediente.grasaVisceral.ToString();
            muscle.Text = user.expediente.musculo.ToString();
            waterPercentage.Text = user.expediente.aguaPorcentaje.ToString();

            saveRecordUpdatePB = FindViewById<Button>(Resource.Id.saveRecordUpdatePB);
            saveRecordUpdatePB.Click += saveRecordUpdate;

            cancelRecordUpdatePB = FindViewById<Button>(Resource.Id.cancelUpdateRecordPB);
            cancelRecordUpdatePB.Click += cancelRecordUpdate;

            changeRecordPB = FindViewById<Button>(Resource.Id.changeRecordData);
            changeRecordPB.Click += changeRecord;

            if (authLevel == 4)
            {
                changeRecordPB.Visibility = ViewStates.Invisible;
            }
            // Create your application here
        }
        private void saveRecordUpdate(object sender, EventArgs e)
        {
            user.expediente.peso = int.Parse(weight.Text);
            user.expediente.talla = int.Parse(size.Text);
            user.expediente.cintura = int.Parse(waist.Text);
            user.expediente.abdomen = int.Parse(abdomen.Text);
            user.expediente.cadera = int.Parse(hip.Text);
            user.expediente.grasa = int.Parse(fat.Text);
            user.expediente.grasaVisceral = int.Parse(visceralFat.Text);
            user.expediente.musculo = int.Parse(muscle.Text);
            user.expediente.aguaPorcentaje = int.Parse(waterPercentage.Text);
            
            decimal aux = decimal.Parse(weight.Text);
            decimal aux2 = decimal.Parse(size.Text)/100;
            aux2 *= aux2;
            aux = aux / aux2;


            if (user.expediente.talla != 0 && user.expediente.peso!=0)
            {
                user.expediente.imc = int.Parse(Math.Round(aux).ToString());
            }
            else
            {
                user.expediente.imc = 0;
            }
            user.expediente.save(this);
            var intent = new Intent(this, typeof(PacientRecordActivity));
            intent.PutExtra("Pacient", user.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
            Finish();
        }

        private void cancelRecordUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientRecordActivity));
            intent.PutExtra("Pacient", user.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
            Finish();
        }

        private void changeRecord(object sender, EventArgs e)
        {
            name.Enabled = false;
            weight.Enabled=true;
            size.Enabled=true;
            waist.Enabled=true;
            abdomen.Enabled=true;
            hip.Enabled=true;
            fat.Enabled=true;
            visceralFat.Enabled=true;
            muscle.Enabled=true;
            waterPercentage.Enabled=true;

            cancelRecordUpdatePB.Visibility = ViewStates.Visible;
            saveRecordUpdatePB.Visibility = ViewStates.Visible;
            changeRecordPB.Visibility = ViewStates.Invisible;
        }
    }

}