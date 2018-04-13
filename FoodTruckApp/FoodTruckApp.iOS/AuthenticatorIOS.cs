using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace FoodTruckApp.iOS
{
    class AuthenticatorIOS : Authenticator
    {
        public override async Task AuthenticatePlatformSpecific()
        {

                // The authentication provider could also be Facebook, Twitter, or Microsoft
                user = await FoodTruckManager.DefaultManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Twitter, "foodtruck");

        }
    }
}