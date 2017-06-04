﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace FoodTruckApp
{
    public partial class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }

        public App()
        {
            
            InitializeComponent();
            
            MainPage = new FoodTruckApp.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        public static void Init(IAuthenticate authenticator)
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
