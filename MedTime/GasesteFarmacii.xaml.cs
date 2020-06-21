using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Serialization;

namespace MedTime
{
    public partial class GasesteFarmacii : ContentPage
    {
        public GasesteFarmacii()
        {
            InitializeComponent();
            DisplayCurLoc();
            AddPins();
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
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(2));
                    mylocalmap.MoveToRegion(mapSpan);
                    System.Diagnostics.Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
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
        
        public async Task<List<Place>> GetNearByPlacesAsync()
        {
            RootObject rootObject = null;
            var client = new HttpClient();
            CultureInfo In = new CultureInfo("en-IN");

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            string latitude = location.Latitude.ToString(In);
            string longitude = location.Longitude.ToString(In);
            string restUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latitude + "," + longitude + "&radius=10000&type=pharmacy&key=AIzaSyAym5SBTwBZsGDwG6U29SIDUeow82jLqkY";
            
            var uri = new Uri(restUrl);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                rootObject = JsonConvert.DeserializeObject<RootObject>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Nici un răspuns", "Încercați din nou", "OK");
            }

            return rootObject.results;
        }
        private async void AddPins()
        {
            try
            {
                var Places = await GetNearByPlacesAsync();
                if (Places.Count == 0 || Places == null)
                    await Application.Current.MainPage.DisplayAlert("Nici o farmacie găsită", "Nici o farmacie gasită în apropierea locației", "OK");
                else
                {
                    mylocalmap.Pins.Clear();
                    foreach (Place place in Places)
                    {

                        mylocalmap.Pins.Add(new Pin()
                        {
                            BindingContext = place,
                            Label = place.name,
                            Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@" ERROR{0}", ex.Message);
            }
        }

    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public Viewport viewport { get; set; }
    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class Place
    {
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public List<Photo> photos { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public double rating { get; set; }
        public string reference { get; set; }
        public string scope { get; set; }
        public List<string> types { get; set; }
        public int user_ratings_total { get; set; }
        public string vicinity { get; set; }
    }

    public class RootObject
    {
        public List<object> html_attributions { get; set; }
        public string next_page_token { get; set; }
        public List<Place> results { get; set; }
        public string status { get; set; }
    }
}
