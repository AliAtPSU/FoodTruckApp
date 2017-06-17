using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace FoodTruckApp
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }

    public class CustomPin
    {
        public Pin Pin { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
    }
}
