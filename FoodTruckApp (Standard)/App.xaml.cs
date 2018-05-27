using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
namespace FoodTruckApp
{
    public partial class App : Application
    {
        public static Authenticator Authenticator { get; private set; }

        public App()
        {

            //System.Net.Http.HttpClientHandler s;
            MainPage = new FoodTruckApp.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=9f275c81-20f9-4945-8573-a2a34b039058;" +
                   "android=5acf4c83-2ee9-4168-8fd3-90f2996bacad;",
                   typeof(Analytics), typeof(Crashes));
        }

        public static void Init(Authenticator authenticator)
        {
            Authenticator = authenticator;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
