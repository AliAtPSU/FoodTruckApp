using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodTruckApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView { get { return MenuItemsListView; } }

        public MainPageMaster()
        {
            InitializeComponent();

            var masterPageItems = new List<MainPageMenuItem>();
            masterPageItems.Add(new MainPageMenuItem
            {
                Title = "Map",
               // IconSource = "contacts.png",
                TargetType = typeof(MapPage)
            });
            masterPageItems.Add(new MainPageMenuItem
            {
                Title = "Settings",
     //           IconSource = "todo.png",
      //          TargetType = typeof(MainPageMenuItem)
            });
            masterPageItems.Add(new MainPageMenuItem
            {
                Title = "Dashboard",
//                IconSource = "reminders.png",
//                TargetType = typeof(MainPageMenuItem)
            });

            MenuItemsListView.ItemsSource = masterPageItems;
        }
    }
}