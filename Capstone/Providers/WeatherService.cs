using Capstone.Common;
using Capstone.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Web.Http;

namespace Capstone.Providers
{
    public static class WeatherService
    {
        public static async Task<WeatherInfo> GetWeather()
        {
            // TODO get preferred weather provider
            WeatherProvider weatherProvider = StoredProcedures.QueryWeatherProvider("National Weather Service");
            if (weatherProvider.Name.Equals("National Weather Service"))
            {
                await GetWeatherFromNWS(weatherProvider);
            }
            return null;
        }

        private static async Task<dynamic> GetWeatherFromNWS(WeatherProvider provider)
        {
            // get the office and grid points
            string getGridPointsURL = provider.urls.Find(url => url.Contains("/points/:latitude,:longitude"));
            string forecastURL = provider.urls.Find(url => url.Contains("/forecast"));
            // get the location's latitude and longitude
            Dictionary<string, double> coordinates = await LocationProvider.GetLatitudeAndLongitude();
            if (coordinates != null)
            {
                double latitude = coordinates["latitude"];
                double longitude = coordinates["longitude"];
                // fill in the url's values
                getGridPointsURL = getGridPointsURL.Replace(":latitude", latitude.ToString()).Replace(":longitude", longitude.ToString());
                Dictionary<string, dynamic> GridPointResponse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await MakeSimpleGetRequest(getGridPointsURL));
                // get the office, gridX, and gridY
                var properties = GridPointResponse["properties"];
                string office = ((dynamic)properties)["cwa"].Value;
                string gridX = ((dynamic)properties)["gridX"].Value.ToString();
                string gridY = ((dynamic)properties)["gridY"].Value.ToString();
                // replace the parts of the forecast url with our values
                forecastURL = forecastURL.Replace(":office", office).Replace(":gridX", gridX).Replace(":gridY", gridY);
                var ForecastResponse = JsonConvert.DeserializeObject/*<Dictionary<string, dynamic>>*/(await MakeSimpleGetRequest(forecastURL));
                dynamic firstApplicableForecast = ((JArray)((dynamic)ForecastResponse).properties.periods).ToList<dynamic>().Find(period => period.endTime.Value >= DateTime.Now);
                // TODO parse out the periods in a separate method
                return ForecastResponse;
            }
            else
            {
                // TODO change this
                return "{error: true}";
            }
            return null;
        }

        private static async Task<string> MakeSimpleGetRequest(string url)
        {
            HttpClient httpClient = new HttpClient();
            // the request will require a user agent string
            var headers = httpClient.DefaultRequestHeaders;
            string userAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(userAgent))
            {
                throw new Exception("Invalid User Agent Header: " + userAgent);
            }
            // the uri to the resource
            Uri requestUri = new Uri(url);
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string responseBody = null;
            try
            {
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                responseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Failed to make request: " + e.Message);
            }
            return responseBody;
        }
    }
}
