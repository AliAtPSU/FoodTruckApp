using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace FoodTruckApp
{

    public abstract class Authenticator
    {

        public AccountStore AccountStore { get; private set; }

        public static MobileServiceClient client = new MobileServiceClient(Constants.ApplicationURL);


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

                            client.CurrentUser = new MobileServiceUser(acct.Username);
                            client.CurrentUser.MobileServiceAuthenticationToken = token;
                            return;
                        }
                    }
                }
            }
        }

        public async Task<bool> Authenticate()
        {
            bool success = false;
            try
            {          
                    //JObject token = new JObject(new JProperty("access_token", "278461506-XDgzX5KzRlEMrdrkB7piFPSG22GQGwQV0LznOoUn"),
                    //    new JProperty("access_token_secret", "f7ywdoNIuWXWnRMFP6WpNPlD2Gzw5RWYmrXZAkWDC3feg"));

                    //client.CurrentUser = await client.LoginAsync(MobileServiceAuthenticationProvider.Twitter, token);

                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    await AuthenticatePlatformSpecific();

                    if (client.CurrentUser != null)
                    {
                        //       CreateAndShowDialog(string.Format("You are now logged in - {0}", user.UserId), "Logged in!");
                    }
                
                success = true;
            }catch(Exception ex){
                createAndShowDialog( ex.Message);
            }
            var account = new Account(client.CurrentUser.UserId);
            account.Properties.Add("token",client.CurrentUser.MobileServiceAuthenticationToken);
            AccountStore.Save(account, "tasklist");

            return success;

        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (client != null)
                {

                    await FoodTruckManager.DefaultManager.CurrentClient.LogoutAsync();
                    //  CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                client = null;
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

        void createAndShowDialog(string message)
        {

        }
        private void GetImageBitmapFromUrl(string url)
        {
            var webImage = new UriImageSource { Uri = new Uri(url) };
            FileImageSourceConverter f = new FileImageSourceConverter();
            
            MapPage.accountPageOpen.Icon=new FileImageSource() { };
        }


        public abstract Task AuthenticatePlatformSpecific();

    }




}
