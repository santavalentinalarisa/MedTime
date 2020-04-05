using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using SearchPlaces;


namespace MedTime
{
   public partial class GasesteFarmacii : ContentPage
    {
        string GooglePlacesApiKey = "AIzaSyAym5SBTwBZsGDwG6U29SIDUeow82jLqkY";

        public GasesteFarmacii()
        {
            InitializeComponent();
            DisplayCurLoc();
            search_bar.ApiKey = GooglePlacesApiKey; 

        }
        public async void DisplayCurLoc()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Position p = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(.444));
                    mylocalmap.MoveToRegion(mapSpan);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    
}
}
