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
    [Activity(Label = "HistoryMain")]
    public class HistoryMainActivity : Activity
    {
        Usuario pacient=new Usuario();
        List<String> history;
        ListView list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.reportHistory);
            pacient.id = Intent.GetIntExtra("Pacient", 0);
            pacient.findAndLoad(this, pacient.id);
            list = FindViewById<ListView>(Resource.Id.listHistory);
            list.ItemClick += itemHistoryClick;
            // Create your application here
        }

        protected override void OnStart()
        {
            base.OnStart();
            history = pacient.paciente.getHistoryDates(this);
            list.Adapter = new HistoryAdapter(this, Resource.Layout.itemHistory, history);
        }

        private void itemHistoryClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var h = history[e.Position];
            var intent = new Intent(this, typeof(ActivityRegisterActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Date", h);
            this.StartActivity(intent);
        }

    }
}