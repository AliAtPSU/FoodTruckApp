using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace FoodTruckApp
{

    public class Authenticator 
    {
        
        public AccountStore AccountStore { get; private set; }

        MobileServiceUser user;


        public Authenticator()
        {
            AccountStore = AccountStore.Create();
        }

        public async Task autoLoginAsync()
        {
            var accounts = AccountStore.FindAccountsForService("tasklist");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    string token;

                    if (acct.Properties.TryGetValue("token", out token))
                    {
                        if (!IsTokenExpired(token))
                        {
                            FoodTruckManager.DefaultManager.CurrentClient.CurrentUser = new MobileServiceUser(acct.Username);
                            FoodTruckManager.DefaultManager.CurrentClient.CurrentUser.MobileServiceAuthenticationToken = token;
                            return;
                        }
                    }
                }
            }
        }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    var token = new JObject();
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await FoodTruckManager.DefaultManager.CurrentClient.LoginAsync(MobileServiceAuthenticationProvider.Twitter, "foodtruck");

                    if (user != null)
                    {
                        //       CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                }
                success = true;
                var account = new Account(FoodTruckManager.DefaultManager.CurrentClient.CurrentUser.UserId);
                account.Properties.Add("token", FoodTruckManager.DefaultManager.CurrentClient.CurrentUser.MobileServiceAuthenticationToken);
                AccountStore.Save(account, "tasklist");

                
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
                var accountsToDelete = AccountStore.FindAccountsForService("tasklist");
                foreach (var account in accountsToDelete)
                {
                    AccountStore.Delete(account, "tasklist");
                }
                success = true;
            }
            catch (Exception ex)
            {
                //CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

        bool IsTokenExpired(string token)
        {
            // Get just the JWT part of the token (without the signature).
            var jwt = token.Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+').Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new ArgumentException("The token is not a valid Base64 string.");
            }

            // Convert to a JSON String
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value,
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            return (expire < DateTime.UtcNow);
        }

    }




}
