using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms;
using FoodTruckApp.Droid;
using FoodTruckApp;
using Android.Gms.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps.Model;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace FoodTruckApp.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {
        GoogleMap map;
        List<CustomPin> customPins;
        //bool isDrawn;

            public CustomMapRenderer(Context context): base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                map.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                ((MapView)Control).GetMapAsync(this);
            }
        }
        protected override void OnMapReady(GoogleMap googleMap)
        {
            base.OnMapReady(googleMap);
            //InvokeOnMapReadyBaseClassHack(googleMap);
            map = googleMap;
            map.InfoWindowClick += OnInfoWindowClick;
            map.SetInfoWindowAdapter(this);

        }

        private void InvokeOnMapReadyBaseClassHack(GoogleMap googleMap)
        {
            System.Reflection.MethodInfo onMapReadyMethodInfo = null;

            Type baseType = typeof(MapRenderer);
            foreach (var currentMethod in baseType.GetMethods(System.Reflection.BindingFlags.NonPublic |
                                                             System.Reflection.BindingFlags.Instance |
                                                              System.Reflection.BindingFlags.DeclaredOnly))
            {

                if (currentMethod.IsFinal && currentMethod.IsPrivate)
                {
                    if (string.Equals(currentMethod.Name, "OnMapReady", StringComparison.Ordinal))
                    {
                        onMapReadyMethodInfo = currentMethod;

                        break;
                    }

                    if (currentMethod.Name.EndsWith(".OnMapReady", StringComparison.Ordinal))
                    {
                        onMapReadyMethodInfo = currentMethod;

                        break;
                    }
                }
            }

            if (onMapReadyMethodInfo != null)
            {
                try
                {
                    onMapReadyMethodInfo.Invoke(this, new[] { googleMap });
                }catch(Exception e)
                {
                    string error = e.Message;
                }
            }
        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
        //    {
        //        map.Clear();

        //        foreach (var pin in customPins)
        //        {
        //            var marker = new MarkerOptions();
        //            marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
        //            marker.SetTitle(pin.Pin.Label);
        //            marker.SetSnippet(pin.Pin.Address);
        //            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));

        //            map.AddMarker(marker);
        //        }
        //        isDrawn = true;
        //    }
        //}

        private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new DetailPage(customPin));
                //var url = Android.Net.Uri.Parse(customPin.Url);
                //var intent = new Intent(Intent.ActionView, url);
                //intent.AddFlags(ActivityFlags.NewTask);
                //Android.App.Application.Context.StartActivity(intent);
            }
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom Pin not found");
                }
                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);


                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }

                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.FoodTruck32));
            
            return marker;
        }
    }
}