namespace Capstone.Models
{
    class SearchableWebsite
    {
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public string QueryString { get; set; }

        public SearchableWebsite() : this("", "", "")
        {

        }

        public SearchableWebsite(string Name, string BaseURL, string QueryString)
        {
            this.Name = Name;
            this.BaseURL = BaseURL;
            this.QueryString = QueryString;
        }
    }
}
