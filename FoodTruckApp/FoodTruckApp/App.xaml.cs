using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;

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
        }

        public static void Init()
        {
            Authenticator = new Authenticator();
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
