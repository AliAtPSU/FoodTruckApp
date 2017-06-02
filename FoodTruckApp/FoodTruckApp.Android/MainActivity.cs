using System;
using Android.Gms.Ads;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
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
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-4283383002881125~1209408893");
            CrashManager.Register(this, "be569339f5274c84a0064669d61abd17");
            MetricsManager.Register(Application, "be569339f5274c84a0064669d61abd17");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            
            LoadApplication(new App());
        }
    }
}

