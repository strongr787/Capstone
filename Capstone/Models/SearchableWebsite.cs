namespace Capstone.Models
{
    class SearchableWebsite
    {
        public int SearchableWebsiteID { get; set; }
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public string QueryString { get; set; }

        public SearchableWebsite() : this(-1, "", "", "")
        {

        }

        public SearchableWebsite(int SearchableWebsiteID, string Name, string BaseURL, string QueryString)
        {
            this.SearchableWebsiteID = SearchableWebsiteID;
            this.Name = Name;
            this.BaseURL = BaseURL;
            this.QueryString = QueryString;
        }
    }
}
