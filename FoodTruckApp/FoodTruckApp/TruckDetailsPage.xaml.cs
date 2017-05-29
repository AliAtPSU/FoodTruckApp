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
    public partial class TruckDetailsPage : ContentPage
    {
        FoodTruck foodTruckToDisplay;
        MapPage mapPage;
        public TruckDetailsPage(FoodTruck truckToDisplay,MapPage previousPage)
        {
            InitializeComponent();
            foodTruckToDisplay = truckToDisplay;
            mapPage = previousPage;
            
        }

        private void NavigateButton_Clicked(object sender, EventArgs e)
        {

        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}