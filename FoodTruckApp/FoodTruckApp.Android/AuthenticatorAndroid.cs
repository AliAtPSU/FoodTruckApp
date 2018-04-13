using System.Threading.Tasks;
using Android.App;
using FoodTruckApp;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace FoodTruckApp.Droid
{

    public partial class AuthenticatorAndroid : Authenticator
    {
        Activity loginActivity;
        public AuthenticatorAndroid(Activity current) : base()
        {
            loginActivity = current;

        }

        public override async Task AuthenticatePlatformSpecific()
        {

            // The authentication provider could also be Facebook, Twitter, or Microsoft
            client.CurrentUser = await client.LoginAsync(loginActivity, MobileServiceAuthenticationProvider.Twitter, "foodtruck");

        }

    }


}