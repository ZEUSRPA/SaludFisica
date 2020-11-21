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
    class UsuarioAdapter:ArrayAdapter<Usuario>
    {
        public UsuarioAdapter(Context context, int resource, List<Usuario> usuarios) : base(context, resource, usuarios) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layout = LayoutInflater.From(Context);

            convertView = layout.Inflate(Resource.Layout.itemPaciente, null);
            var item = GetItem(position);
            var text = convertView.FindViewById<TextView>(Resource.Id.itemofPaciente);
            text.Text = item.userName;
            return convertView;
        }
    }
}