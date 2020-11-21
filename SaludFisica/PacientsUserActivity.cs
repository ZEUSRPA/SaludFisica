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
    [Activity(Label = "PacientsUser")]
    public class PacientsUserActivity : Activity
    {
        Usuario user=new Usuario();

        EditText userName;
        EditText password;
        EditText confirmPassword;

        Button changeUserPB;
        Button saveUserUpdatePB;
        Button cancelUserUpdatePB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pacientUser);
            user.id = Intent.GetIntExtra("User", 0);
            user.findAndLoad(this, user.id);

            userName = FindViewById<EditText>(Resource.Id.userNameDetails);
            password = FindViewById<EditText>(Resource.Id.passwordUserDetailsET);
            confirmPassword = FindViewById<EditText>(Resource.Id.passwordConfirmDetailsET);

            changeUserPB = FindViewById<Button>(Resource.Id.changeUserPB);
            changeUserPB.Click += changeUser;

            saveUserUpdatePB = FindViewById<Button>(Resource.Id.saveUserUpdatePB);
            saveUserUpdatePB.Click += saveUserUpdate;

            cancelUserUpdatePB = FindViewById<Button>(Resource.Id.cancelUserUpdatePB);
            cancelUserUpdatePB.Click += cacelUserUpdate;

            userName.Text = user.userName;
            password.Text = user.password;
            confirmPassword.Text = user.password;
            // Create your application here
        }

        private void cacelUserUpdate(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PacientsUserActivity));
            intent.PutExtra("User", user.id);
            this.StartActivity(intent);
            Finish();

        }

        private void saveUserUpdate(object sender, EventArgs e)
        {
            if (password.Text != confirmPassword.Text)
            {
                Toast.MakeText(this, "Las contraseñas no coinciden", ToastLength.Short).Show();
                return;
            }
            Usuario a=new Usuario();
            a.userName = userName.Text;
            a.password = password.Text;
            if (user.update(this,a))
            {
                var intent = new Intent(this, typeof(PacientsUserActivity));
                intent.PutExtra("User", user.id);
                this.StartActivity(intent);
                Finish();
            }

        }

        private void changeUser(object sender, EventArgs e)
        {
            userName.Enabled = true;
            password.Enabled = true;
            confirmPassword.Enabled = true;
            changeUserPB.Visibility = ViewStates.Invisible;
            saveUserUpdatePB.Visibility = ViewStates.Visible;
            cancelUserUpdatePB.Visibility = ViewStates.Visible;
        }
    }
}