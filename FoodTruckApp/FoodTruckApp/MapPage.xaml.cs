using Ads;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace FoodTruckApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        Geocoder geoCoder;
        public static ToolbarItem accountPageOpen = new ToolbarItem()
        {
            Text = "account"
        };
        public MapPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            this.ToolbarItems.Add(accountPageOpen);
            geoCoder = new Geocoder();
            FoodTruck foodTruckToDisplay = new FoodTruck() { Name = "test display", Description = "this is just a text description for food truck to be displayed in a display page." };
            var pin = new CustomPin(foodTruckToDisplay)
            {
                Pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(37.79752, -122.40183),
                    Label = "Xamarin San Francisco Office",
                    Address = "394 Pacific Ave, San Francisco CA"
                },
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };
            
            MyMap.CustomPins = new List<CustomPin> { pin };
            MyMap.Pins.Add(pin.Pin);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));
        }


        private async void DisplayPin(List<FoodTruck> foodTrucks)
        {

            MyMap.Pins.Clear();
            foreach (var truck in foodTrucks)
            {
                MyMap.Pins.Add(new Pin
                {
                    Label = truck.Name,
                    Position = new Position(truck.Latitude, truck.Longitude),
                    Type = PinType.SearchResult
                });
            }

        }

        private async void OnGoToClicked(object sender, EventArgs e)
        {
            //var item = (await geoCoder.GetPositionsForAddressAsync(EntryLocation.Text)).FirstOrDefault();
            //if (item == null)
            //{
            //    await DisplayAlert("Error", "Unable to decode position", "OK");
            //    return;
            //}

            //var zoomLevel = SliderZoom.Value; // between 1 and 18
            //var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            //MyMap.MoveToRegion(new MapSpan(item, latlongdegrees, latlongdegrees));
            //try
            //{
            //    Navigation.PushAsync(new LogInPage());
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            ////}
            FoodTruck[] list;
            JToken request;
            try
            {

                FoodTruck test = new FoodTruck() { Name = "testName8", Description = "testDescription8", Latitude = -16.23, Longitude = 42 };
                //await FoodTruckManager.DefaultManager.todoTable.InsertAsync(new FoodTruck { Name = "test", Description = "test" });
                //request = await FoodTruckManager.DefaultManager.CurrentClient.InvokeApiAsync<FoodTruck, JToken>("FoodTrucks", test, HttpMethod.Post, null);
                request = await Authenticator.client.InvokeApiAsync("FoodTrucks", HttpMethod.Get, parameters: null);
                list = request.ToObject<FoodTruck[]>();
                var listPins = list.Select<FoodTruck, CustomPin>(f => new CustomPin
                (
                    new FoodTruck() { Description = f.Description,Name=f.Name,Latitude=f.Latitude,Longitude =f.Longitude,IsAvailable=f.IsAvailable}
                 
                ));
                foreach (var pin in listPins)
                {
                    MyMap.CustomPins.Add(pin);
                    MyMap.Pins.Add(pin.Pin);
                }
            }
            catch (Exception ex)
            {
            System.Diagnostics.Debug.WriteLine("");
            }


        }

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

    }
}