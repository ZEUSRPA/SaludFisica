using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Icu.Util;

namespace SaludFisica
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        RelativeLayout loginPage;
        RelativeLayout homePage;
        RelativeLayout welcomePage;


        Usuario userLogged;
        int authLevel;

        IMenuItem navLogin;
        IMenuItem navLogout;
        IMenuItem navUser;
        IMenuItem navRutinas;
        IMenuItem navDietas;
        IMenuItem navPacientes;
        IMenuItem navReporte;
        IMenuItem navActivityRegister;
        IMenuItem navMensajes;
        

        NavigationView navigationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            //Notifications

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            userLogged = new Usuario();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            loginPage = (RelativeLayout)FindViewById(Resource.Id.login_page);
            homePage = (RelativeLayout)FindViewById(Resource.Id.homePage);
            welcomePage = (RelativeLayout)FindViewById(Resource.Id.welcomePage);

            setAllPagesInvisible();
            welcomePage.Visibility = ViewStates.Visible;

            navLogin = navigationView.Menu.FindItem(Resource.Id.nav_login);
            navLogout = navigationView.Menu.FindItem(Resource.Id.nav_logout);
            navUser = navigationView.Menu.FindItem(Resource.Id.nav_user);
            navDietas = navigationView.Menu.FindItem(Resource.Id.nav_dietas);
            navRutinas = navigationView.Menu.FindItem(Resource.Id.nav_rutinas);
            navReporte = navigationView.Menu.FindItem(Resource.Id.nav_reporte);
            navPacientes = navigationView.Menu.FindItem(Resource.Id.nav_pacientes);
            navMensajes = navigationView.Menu.FindItem(Resource.Id.nav_mensajes);
            navActivityRegister = navigationView.Menu.FindItem(Resource.Id.nav_activityRegister);
            hideAllMenus();
            navLogin.SetVisible(true);

            var ingresar = FindViewById<Button>(Resource.Id.ingresar_button);
            ingresar.Click += Ingresar;

            

            

            

            var logButton = FindViewById<Button>(Resource.Id.setLoginButton);
            logButton.Click += setLoginView;

        }

        
        public void setAllPagesInvisible()
        {
            loginPage.Visibility = ViewStates.Invisible;
            homePage.Visibility = ViewStates.Invisible;
            welcomePage.Visibility = ViewStates.Invisible;
        }


        public void Ingresar(object sender, EventArgs e)
        {
            var user = FindViewById<Android.Support.V7.Widget.AppCompatEditText>(Resource.Id.username);
            var password = FindViewById<Android.Support.V7.Widget.AppCompatEditText>(Resource.Id.password);
            Usuario u = new Usuario();
            u.findAndLoad(this, user.Text);
            if (user.Text == u.userName && password.Text == u.password)
            {
                userLogged.id = u.id;
                userLogged.userName=u.userName;
                userLogged.password = u.userName;
                userLogged.rol = u.rol;
                if (userLogged.rol == "admin")
                {
                    authLevel = 1;
                }else if (userLogged.rol == "nutriologo")
                {
                    authLevel = 2;
                }else if (userLogged.rol == "especialista")
                {
                    authLevel = 3;
                }
                else
                {
                    authLevel = 4;
                }

                Toast.MakeText(Application.Context, "Hola", ToastLength.Short).Show();

                setAllPagesInvisible();
                homePage.Visibility = ViewStates.Visible;

                navLogin.SetVisible(false);
                navLogout.SetVisible(true);
                navUser.SetVisible(true);
                navMensajes.SetVisible(true);

                //opciones para usuarios administradores
                if (userLogged.rol == "nutriologo"||userLogged.rol=="especialista"||userLogged.rol=="admin")
                {
                    navDietas.SetVisible(true);
                    navRutinas.SetVisible(true);
                    navPacientes.SetVisible(true);
                }
                else
                {
                    navDietas.SetVisible(true);
                    navRutinas.SetVisible(true);
                    navReporte.SetVisible(true);
                    navActivityRegister.SetVisible(true);
                }
            }
            else
            {
                Toast.MakeText(Application.Context, "Usuario o contrasena incorrectos", ToastLength.Short).Show();
            }
            user.Text = "";
            password.Text = "";

        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public void hideAllMenus()
        {
            navLogin.SetVisible(false);
            navLogout.SetVisible(false);
            navUser.SetVisible(false);
            navDietas.SetVisible(false);
            navRutinas.SetVisible(false);
            navReporte.SetVisible(false);
            navPacientes.SetVisible(false);
            navActivityRegister.SetVisible(false);
            navMensajes.SetVisible(false);

        }


        public void setLoginView(object sender, EventArgs e)
        {
            setAllPagesInvisible();
            loginPage.Visibility = ViewStates.Visible;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_login)
            {
                // Handle the camera action
                setAllPagesInvisible();
                loginPage.Visibility = ViewStates.Visible;
            }
            else if (id == Resource.Id.nav_user)
            {
                if (authLevel == 4)
                {
                    var intent = new Intent(this, typeof(PacientsDetailsActivity));
                    intent.PutExtra("Pacient", userLogged.id);
                    intent.PutExtra("Auth", authLevel);
                    this.StartActivity(intent);
                }
                else
                {
                    var intent = new Intent(this, typeof(PacientsUserActivity));
                    intent.PutExtra("User", userLogged.id);
                    this.StartActivity(intent);
                }
            }
            else if (id == Resource.Id.nav_dietas)
            {
                if (authLevel == 4)
                {
                    var intent = new Intent(this, typeof(PacientsDietActivity));
                    intent.PutExtra("Pacient", userLogged.id);
                    intent.PutExtra("Auth", authLevel);
                    this.StartActivity(intent);
                }
                else
                { 
                    var intent = new Intent(this, typeof(DietsMainActivity));
                    intent.PutExtra("Auth", authLevel);
                    this.StartActivity(intent);
                }
            }
            else if (id == Resource.Id.nav_logout)
            {
                hideAllMenus();
                navLogin.SetVisible(true);
                userLogged = new Usuario();
                setAllPagesInvisible();
                welcomePage.Visibility = ViewStates.Visible;

            }
            else if (id == Resource.Id.nav_rutinas)
            {
                if (authLevel == 4)
                {
                    var intent = new Intent(this, typeof(PacientsRutineActivity));
                    intent.PutExtra("Pacient", userLogged.id);
                    intent.PutExtra("Auth", authLevel);
                    this.StartActivity(intent);
                }
                else
                {

                    var intent = new Intent(this, typeof(RutinesMainActivity));
                    intent.PutExtra("Auth", authLevel);
                    this.StartActivity(intent);
                }
            }
            else if (id == Resource.Id.nav_activityRegister)
            {
                var intent = new Intent(this, typeof(ActivityRegisterActivity));
                intent.PutExtra("Pacient", userLogged.id);
                intent.PutExtra("Date", "");
                this.StartActivity(intent);
            }
            else if (id == Resource.Id.nav_reporte)
            {
                var intent = new Intent(this, typeof(ReportMainActivity));
                intent.PutExtra("Pacient", userLogged.id);
                this.StartActivity(intent);
            }
            else if (id == Resource.Id.nav_developers)
            {
                var intent = new Intent(this, typeof(DevelopersActivity));
                this.StartActivity(intent);
            }
            else if (id == Resource.Id.nav_mensajes)
            {
                var intent = new Intent(this, typeof(MessageMainActivity));
                intent.PutExtra("Pacient", userLogged.id);
                this.StartActivity(intent);
            }
            else if (id == Resource.Id.nav_pacientes)
            {
                var intent = new Intent(this, typeof(PacientsMainActivity));
                intent.PutExtra("Auth", authLevel);
                this.StartActivity(intent);
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

