﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Android.Webkit;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FoodTruckApp.Droid
{
    [Activity(Label = "FoodTruckApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        MobileServiceUser user;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            CrashManager.Register(this, "be569339f5274c84a0064669d61abd17");
            MetricsManager.Register(Application, "be569339f5274c84a0064669d61abd17");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();


            App.Init((IAuthenticate)this);
            LoadApplication(new App());
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {



                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await FoodTruckManager.DefaultManager.CurrentClient.LoginAsync(this,MobileServiceAuthenticationProvider.Twitter, "foodtruck");

                    if (user != null)
                    {
                        CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    CookieManager.Instance.RemoveAllCookie();
                    await FoodTruckManager.DefaultManager.CurrentClient.LogoutAsync();
                    CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

        void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetNeutralButton("OK", (sender, args) =>
            {
            });
            builder.Create().Show();
        }
    }
}

