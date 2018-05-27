using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Android.Webkit;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FoodTruckApp;
namespace FoodTruckApp.Droid
{
    [Activity(Label = "FoodTruckApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();


            App.Init(new AuthenticatorAndroid(this));
            LoadApplication(new App());
            //FoodTruckManager.DefaultManager.CurrentClient.LoginAsync(this,MobileServiceAuthenticationProvider.Twitter,"");
        }

       


    }
}

