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
    [Activity(Label = "MessageDescriptionActivity")]
    public class MessageDescriptionActivity : Activity
    {
        Usuario pacient =new Usuario();
        Spinner messageDestinationSP;
        EditText messageDestinationDetailsET;
        EditText messageSubjectET;
        EditText messageDescription;
        TextView titleNewMessage;
        TextView titleDestination;
        Button sendMessagePB;

        List<Usuario> users = new List<Usuario>();


        ZMessage mess=new ZMessage();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.messageDetails);
            pacient.id = Intent.GetIntExtra("Pacient",0);
            mess.id = Intent.GetIntExtra("Mess", 0);
            messageDestinationSP = FindViewById<Spinner>(Resource.Id.messageDestinationSP);
            messageDestinationDetailsET = FindViewById<EditText>(Resource.Id.messageDestinationDetailsET);
            messageSubjectET = FindViewById<EditText>(Resource.Id.messageSubjectET);
            messageDescription = FindViewById<EditText>(Resource.Id.messageDescription);
            sendMessagePB = FindViewById<Button>(Resource.Id.sendMessagePB);
            titleNewMessage = FindViewById<TextView>(Resource.Id.titleNewMessage);
            titleDestination = FindViewById<TextView>(Resource.Id.titleDestination);
            sendMessagePB.Click += sendMessage;
            var asi= new Usuario();
            users = asi.all(this);

            ArrayAdapter<Usuario> adapter = new ArrayAdapter<Usuario>(this, Resource.Layout.support_simple_spinner_dropdown_item, users);
            messageDestinationSP.Adapter = adapter;

            if (mess.id != 0)
            {
                titleNewMessage.Text = "Detalles del Mensaje";
                titleDestination.Text = "Remitente";
                mess.load(this);
                messageDestinationDetailsET.Text = users[users.FindIndex(a => a.id == mess.idRemitente)].userName;
                messageSubjectET.Text = mess.subject;
                messageDescription.Text = mess.message;
                sendMessagePB.Visibility = ViewStates.Invisible;

            }
            else
            {
                messageDestinationDetailsET.Visibility = ViewStates.Invisible;
                messageDestinationSP.Visibility = ViewStates.Visible;
                messageSubjectET.Enabled = true;
                messageDescription.Enabled = true;
            }


            // Create your application here
        }

        private void sendMessage(object sender, EventArgs e)
        {
            ZMessage ax = new ZMessage();
            ax.idDestinatario = users[messageDestinationSP.SelectedItemPosition].id;
            ax.idRemitente = pacient.id;
            ax.subject = messageSubjectET.Text;
            ax.message = messageDescription.Text;
            ax.sendMessage(this);
            Finish();
        }
    }
}