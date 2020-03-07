namespace Capstone.Models
{
    class SearchEngine
    {
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public string QueryString { get; set; }

        public SearchEngine() : this("", "", "")
        {

        }

        public SearchEngine(string Name, string BaseURL, string QueryString)
        {
            this.Name = Name;
            this.BaseURL = BaseURL;
            this.QueryString = QueryString;
        }
    }
}
