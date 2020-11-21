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
    class HistoryAdapter : ArrayAdapter<String>
    {
        public HistoryAdapter(Context context, int resource, List<String> history) : base(context, resource, history) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layout = LayoutInflater.From(Context);

            convertView = layout.Inflate(Resource.Layout.itemHistory, null);
            var item = GetItem(position);
            var text = convertView.FindViewById<TextView>(Resource.Id.itemofHistory);
            text.Text = item;
            return convertView;
        }
    }
}