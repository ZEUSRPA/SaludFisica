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
    [Activity(Label = "PacientsMainActivity")]
    public class PacientsMainActivity : Activity
    {
        List<Usuario> pacientes;
        ListView list;
        int authLevel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientesMain);
            list = FindViewById<ListView>(Resource.Id.listPacientes);
            authLevel = Intent.GetIntExtra("Auth", 4);
            list.ItemClick += itemPacientClick;
            var newPac = FindViewById<Button>(Resource.Id.newPacButton);
            newPac.Click += newPacients;

            // Create your application here
        }

        protected override void OnStart()
        {
            base.OnStart();
            var pacient = new Usuario();
            pacientes = pacient.where(this, "paciente");
            list.Adapter = new UsuarioAdapter(this, Resource.Layout.itemPaciente, pacientes);
            
        }

        private void itemPacientClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var p = pacientes[e.Position];
            var intent = new Intent(this, typeof(PacientsDetailsActivity));
            intent.PutExtra("Pacient", p.id);
            intent.PutExtra("Auth", authLevel);
            this.StartActivity(intent);
        }

        private void newPacients(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsAddActivity));
            this.StartActivity(intent);
        }


    }
}