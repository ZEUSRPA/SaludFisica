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
    [Activity(Label = "MessageMainActivity")]
    public class MessageMainActivity : Activity
    {
        ListView list;
        List<ZMessage> mensajes;
        Usuario pacient = new Usuario();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.messageMain);
            pacient.id = Intent.GetIntExtra("Pacient", 0);
            list = FindViewById<ListView>(Resource.Id.listMessages);
            list.ItemClick += itemMessageClick;

            var newMessageButton = FindViewById<Button>(Resource.Id.newMessageButton);
            newMessageButton.Click += newMessage;
            // Create your application here
        }

        private void newMessage(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MessageDescriptionActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Mess", 0);
            this.StartActivity(intent);
        }

        protected override void OnStart()
        {
            base.OnStart();
            var mensaje = new ZMessage();
            mensajes = mensaje.loadReceived(this,pacient.id);
            list.Adapter = new MessageAdapter(this, Resource.Layout.itemMessage, mensajes);
        }
        private void itemMessageClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var p = mensajes[e.Position];
            var intent = new Intent(this, typeof(MessageDescriptionActivity));
            intent.PutExtra("Pacient", pacient.id);
            intent.PutExtra("Mess", p.id);
            this.StartActivity(intent);
        }
    }
}