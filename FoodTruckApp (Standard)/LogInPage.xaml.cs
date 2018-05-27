using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodTruckApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();

        }


        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            bool authenticated = false;
            try
            {

                if (App.Authenticator != null)
                {
                    authenticated = await App.Authenticator.Authenticate();
                }

                if (authenticated)
                {
                    Navigation.InsertPageBefore(new MapPage(), this);
                    await Navigation.PopAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("Authentication was cancelled"))
                {
                    messageLabel.Text = "Authentication cancelled by the user";
                }
            }
            catch (Exception ex)
            {
                messageLabel.Text = "Authentication failed";
            }
        }
    }
}