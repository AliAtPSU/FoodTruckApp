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
        public MapPage()
        {

            InitializeComponent();
            geoCoder = new Geocoder();
           
            var pin = new CustomPin
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

        private void OnStreetClicked(object sender, EventArgs e) =>
             MyMap.MapType = MapType.Street;

        private void OnHybridClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Hybrid;

        private void OnSatelliteClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Satellite;

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

            JToken request;
            try
            {

            
                //await FoodTruckManager.DefaultManager.todoTable.InsertAsync(new FoodTruck { Name = "test", Description = "test" });
                 request = await FoodTruckManager.DefaultManager.CurrentClient.InvokeApiAsync<FoodTruck,JToken>("FoodTruck", new FoodTruck { Name = "test", Description = "test" },HttpMethod.Post,   null);
            }
            catch (Exception ex)
            {

            }
            System.Diagnostics.Debug.WriteLine("");
            
        }

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pin = new CustomPin
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
    }
}