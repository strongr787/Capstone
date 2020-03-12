using System.Collections.Generic;

namespace Capstone.Models
{
    public class MapProvider
    {
        public int MapProviderID { get; set; }
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public MapProviderAccessTypes AccessType { get; set; }
        /// <summary>
        /// Parts of the url (such as url queries). Each part will be used to build the final url to make the request to the map API with
        /// </summary>
        public Dictionary<string, string> URLParts { get; } = new Dictionary<string, string>();
        public string APIKey { get; }

        public MapProvider() : this(-1, "", "", MapProviderAccessTypes.EXTERNAL_URL, "") { }

        public MapProvider(int MapProviderID, string Name, string BaseURL, MapProviderAccessTypes AccessType, string APIKey)
        {
            this.MapProviderID = MapProviderID;
            this.Name = Name;
            this.BaseURL = BaseURL;
            this.AccessType = AccessType;
            this.APIKey = APIKey;
        }

        public enum MapProviderAccessTypes
        {
            EXTERNAL_URL = 1, // accessible through browser
            APP = 2, // accessible through app installed on machine
        }
    }
}
