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
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Auth;

namespace FoodTruckApp.Droid
{
    class AndroidAuthenticator : IAuthenticate
    {
        public Context RootView { get; private set; }

        public AccountStore AccountStore { get; private set; }

        MobileServiceUser user;
        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            AccountStore.Create(RootView);
            try
            {
                if (user == null)
                {

                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await FoodTruckManager.DefaultManager.CurrentClient.LoginAsync(MobileServiceAuthenticationProvider.Twitter, "foodtruck");

                    if (user != null)
                    {
                 //       CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
               // CreateAndShowDialog(ex.Message, "Authentication failed");
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
                    
                    await FoodTruckManager.DefaultManager.CurrentClient.LogoutAsync();
                  //  CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                //CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

    }
}