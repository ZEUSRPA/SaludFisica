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
    class MessageAdapter : ArrayAdapter<ZMessage>
    {
        public MessageAdapter(Context context, int resource, List<ZMessage> messages) : base(context, resource, messages) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layout = LayoutInflater.From(Context);

            convertView = layout.Inflate(Resource.Layout.itemMessage, null);
            var item = GetItem(position);
            var text = convertView.FindViewById<TextView>(Resource.Id.itemofMessage);
            text.Text = item.subject;
            return convertView;
        }
    }
}