using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace FoodTruckApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate,IAuthenticate
    {
        MobileServiceUser user;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            App.Init(this);
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await FoodTruckManager.DefaultManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Google);
                    if (user != null)
                    {
                        var authAlert = new UIAlertView("Authentication", "You are now logged in " + user.UserId, null, "OK", null);
                        authAlert.Show();
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                var authAlert = new UIAlertView("Authentication failed", ex.Message, null, "OK", null);
                authAlert.Show();
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
                    foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                    {
                        NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                    }

                    await FoodTruckManager.DefaultManager.CurrentClient.LogoutAsync();
                    var logoutAlert = new UIAlertView("Authentication", "You are now logged out " + user.UserId, null, "OK", null);
                    logoutAlert.Show();
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                var logoutAlert = new UIAlertView("Logout failed", ex.Message, null, "OK", null);
                logoutAlert.Show();
            }
            return success;
        }
    }
}
