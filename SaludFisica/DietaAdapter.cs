using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace SaludFisica
{
    class DietaAdapter:ArrayAdapter<Dieta>
    {
        public DietaAdapter(Context context,int resource,List<Dieta> dietas) : base(context, resource, dietas) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layout = LayoutInflater.From(Context);

            convertView = layout.Inflate(Resource.Layout.itemDiet,null);
            var item = GetItem(position);
            var text = convertView.FindViewById<TextView>(Resource.Id.itemofDiet);
            text.Text = item.name;
            return convertView;
        }
    }

}