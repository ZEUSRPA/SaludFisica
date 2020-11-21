using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media.Audiofx;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace SaludFisica
{
    [Activity(Label = "PacientPersonalDataActivity")]
    public class PacientPersonalDataActivity : Activity
    {
        Usuario user=new Usuario();

        EditText name;
        EditText age;
        EditText gender;
        Spinner genderSP;
        EditText job;
        EditText address;
        EditText phone;
        Button changePersonalDataPB;
        Button cancelPersonalDataUpdatePB;
        Button savePersonalDataUpdatePB;

        List<String> genders = new List<string>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientPersonalData);

            name = FindViewById<EditText>(Resource.Id.pacientNameDetailsET);
            age = FindViewById<EditText>(Resource.Id.pacientAgeDetailsET);
            gender = FindViewById <EditText>(Resource.Id.pacientGenderDetailsET);
            genderSP = FindViewById<Spinner>(Resource.Id.pacientGenderDetailsSP);
            job = FindViewById<EditText>(Resource.Id.pacientJobDetailsET);
            address = FindViewById<EditText>(Resource.Id.pacientAddressDetailsET);
            phone = FindViewById<EditText>(Resource.Id.pacientPhoneDetailsET);

            

            user.id =Intent.GetIntExtra("Pacient",0);
            user.findAndLoad(this, user.id);

            name.Text = user.paciente.name;
            age.Text = user.paciente.edad.ToString();
            
            genders.Add("Hombre");
            genders.Add("Mujer");
            genders.Add("No definido");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, genders);
            genderSP.Adapter = adapter;

            if (user.paciente.sexo == "M")
            {
                genderSP.SetSelection(1);
                gender.Text = "Mujer";
            }
            else if (user.paciente.sexo == "H")
            {
                genderSP.SetSelection(0);
                gender.Text = "Hombre";
            }
            else
            {
                genderSP.SetSelection(2);
                gender.Text = "No definido";
            }
            
            job.Text = user.paciente.ocupacion;
            address.Text = user.paciente.domicilio;
            phone.Text = user.paciente.telefono;

            changePersonalDataPB = FindViewById<Button>(Resource.Id.changePersonalData);
            changePersonalDataPB.Click += changePersonalData;

            cancelPersonalDataUpdatePB = FindViewById<Button>(Resource.Id.cancelUpdatePersonalDataPB);
            cancelPersonalDataUpdatePB.Click += cancelPersonalDataUpdate;

            savePersonalDataUpdatePB = FindViewById<Button>(Resource.Id.savePersonalDataUpdatePB);
            savePersonalDataUpdatePB.Click += savePersonalDataUpdate;
            // Create your application here
        }

        private void savePersonalDataUpdate(object sender, EventArgs e)
        {
            user.paciente.name = name.Text;
            user.paciente.edad = int.Parse(age.Text);
            if (genderSP.SelectedItemPosition == 0)
            {
                user.paciente.sexo = "H";
            }else if (genderSP.SelectedItemPosition == 1)
            {
                user.paciente.sexo = "M";
            }
            else
            {
                user.paciente.sexo = "U";
            }
            user.paciente.ocupacion = job.Text;
            user.paciente.domicilio = address.Text;
            user.paciente.telefono = phone.Text;
            user.paciente.save(this);
            var intent = new Intent(this, typeof(PacientPersonalDataActivity));
            intent.PutExtra("Pacient", user.id);
            this.StartActivity(intent);
            Finish();
        }

        private void cancelPersonalDataUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientPersonalDataActivity));
            intent.PutExtra("Pacient", user.id);
            this.StartActivity(intent);
            Finish();
        }

        private void changePersonalData(object sender, EventArgs e)
        {
            name.Enabled = true;
            age.Enabled = true;
            genderSP.Visibility = ViewStates.Visible;
            gender.Visibility = ViewStates.Invisible;
            job.Enabled = true;
            address.Enabled = true;
            phone.Enabled = true;
            cancelPersonalDataUpdatePB.Visibility = ViewStates.Visible;
            savePersonalDataUpdatePB.Visibility = ViewStates.Visible;
            changePersonalDataPB.Visibility = ViewStates.Invisible;
        }


    }
}