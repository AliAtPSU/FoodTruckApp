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
	public partial class DetailPage : ContentPage
	{        

        public DetailPage(CustomPin pin)
		{

			InitializeComponent ();
            foodTruckName.Text = pin.foodTruck.Name;
            description.Text = pin.foodTruck.Description;

        }
	}
}