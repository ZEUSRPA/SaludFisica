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
    class RutinaAdapter : ArrayAdapter<Rutina>
    {
        public RutinaAdapter(Context context, int resource, List<Rutina> rutinas) : base(context, resource, rutinas) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layout = LayoutInflater.From(Context);

            convertView = layout.Inflate(Resource.Layout.itemRutine, null);
            var item = GetItem(position);
            var text = convertView.FindViewById<TextView>(Resource.Id.itemofRutine);
            text.Text = item.name;
            return convertView;
        }
    }
}