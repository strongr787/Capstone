using System.Collections.Generic;

namespace Capstone.Models
{
    class WeatherProvider
    {
        public int WeatherProviderID { get; set; }
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public WeatherProviderAccessTypes AccessType { get; set; }
        /// <summary>
        /// Parts of the url (such as url queries). Each part will be used to build the final url to make the request to the weather API with
        /// </summary>
        public Dictionary<string, string> URLParts { get; } = new Dictionary<string, string>();
        public string APIKey { get; }

        public WeatherProvider() : this(-1, "", "", WeatherProviderAccessTypes.EXTERNAL_URL, "") { }

        public WeatherProvider(int WeatherProviderID, string Name, string BaseURL, WeatherProviderAccessTypes AccessType, string APIKey)
        {
            this.WeatherProviderID = WeatherProviderID;
            this.Name = Name;
            this.BaseURL = BaseURL;
            this.AccessType = AccessType;
            this.APIKey = APIKey;
        }

        public enum WeatherProviderAccessTypes
        {
            EXTERNAL_URL = 1, // accessible through browser
            APP = 2 // accessible through app installed on machine
        }
    }
}
