using Capstone.Common;
using Capstone.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capstone.Actions
{
    public class WebsiteSearchAction : Action
    {

        private string WebsiteName { get; set; }
        private string SearchText { get; set; }

        public enum SearchActionTypes
        {
            Google = 1,
            Bing = 2,
            DuckDuckGo = 3,
            Amazon = 4,
            Youtube = 5,
            Walmart = 6
        }
        public WebsiteSearchAction(string CommandString)
        {
            this.CommandString = CommandString;
        }


    public override void PerformAction()
        {
      
            SearchableWebsite searchableWebsite = new SearchableWebsite();
            //find which search phrase is wanted
            SearchActionTypes desiredAction = this.GetActionFromCommand();
            string searchParameters;
            string searchQuery;

            switch (desiredAction)
            {
                case SearchActionTypes.Google:
                    //load information from database
                    searchableWebsite = LoadSearchModelFromDatabase(SearchActionTypes.Google);
                    //find what is wanted to be searched and concatenate with + for end of url
                    searchParameters = GetSearchParameters();
                    //return the full url that contains the search 
                    searchQuery = BuildSearchQuery(searchableWebsite, searchParameters);
                    //launch browser. this will be done with the default browser
                    LaunchSearch(searchQuery);
                    break;
                case SearchActionTypes.Bing:
                    searchableWebsite = LoadSearchModelFromDatabase(SearchActionTypes.Bing);
                    searchParameters = GetSearchParameters();
                    searchQuery = BuildSearchQuery(searchableWebsite, searchParameters);
                    LaunchSearch(searchQuery);
                    break;
                case SearchActionTypes.DuckDuckGo:
                    searchableWebsite = LoadSearchModelFromDatabase(SearchActionTypes.DuckDuckGo);
                    searchParameters = GetSearchParameters();
                    searchQuery = BuildSearchQuery(searchableWebsite, searchParameters);
                    LaunchSearch(searchQuery);
                    break;
                case SearchActionTypes.Amazon:
                    searchableWebsite = LoadSearchModelFromDatabase(SearchActionTypes.Amazon);
                    searchParameters = GetSearchParameters();
                    searchQuery = BuildSearchQuery(searchableWebsite, searchParameters);
                    LaunchSearch(searchQuery);
                    break;
                case SearchActionTypes.Youtube:
                    searchableWebsite = LoadSearchModelFromDatabase(SearchActionTypes.Youtube);
                    searchParameters = GetSearchParameters();
                    searchQuery = BuildSearchQuery(searchableWebsite, searchParameters);
                    LaunchSearch(searchQuery);
                    break;
                case SearchActionTypes.Walmart:
                    searchableWebsite = LoadSearchModelFromDatabase(SearchActionTypes.Walmart);
                    searchParameters = GetSearchParameters();
                    searchQuery = BuildSearchQuery(searchableWebsite, searchParameters);
                    LaunchSearch(searchQuery);
                    break;

            }
            // TODO parse out the website to search, pull its info from the database, parse out what the user wants to search on, and search the website
        }

        private SearchActionTypes GetActionFromCommand()
        {
            var googleRegex = new Regex("(?i)(google)(?-i)");
            var bingRegex = new Regex("(?i)(bing)(?-i)");
            var duckDuckGoRegex = new Regex("(?i)(duckduckgo)(?-i)");
            var amazonRegex = new Regex("(?i)(amazon)(?-i)");
            var youtubeRegex = new Regex("(?i)(youtube)(?-i)");
            var walmartRegex = new Regex("(?i)(walmart)(?-i)");
            if (googleRegex.IsMatch(this.CommandString))
            {
                Setting setting = StoredProcedures.QuerySetting(1);
                return SearchActionTypes.Google;
            }
            else if (bingRegex.IsMatch(this.CommandString))
            {
                return SearchActionTypes.Bing;
            }
            else if (duckDuckGoRegex.IsMatch(this.CommandString))
            {
                return SearchActionTypes.DuckDuckGo;
            }
            else if (amazonRegex.IsMatch(this.CommandString))
            {
                return SearchActionTypes.Amazon;
            }
            else if (youtubeRegex.IsMatch(this.CommandString))
            {
                return SearchActionTypes.Youtube;
            }
            else if (walmartRegex.IsMatch(this.CommandString))
            {
                return SearchActionTypes.Walmart;
            }
            else
            {
                //to do setup to get default search engine from selection
                return SearchActionTypes.Walmart;


            }
        }

        //loads desired search method from database
        private SearchableWebsite LoadSearchModelFromDatabase(SearchActionTypes searchMethod)
        {
            int searchType = Convert.ToInt32(searchMethod);
            SearchableWebsite searchableWebsite = StoredProcedures.QuerySearchableWebsite(searchType);

            return searchableWebsite;

        }
        //concatenate each word with + for end of search query
        private string BuildSearchParameter(string searchQuery)
        {
            int searchWordNumber = 1;
            bool lastWord = false;
            string searchPart = "";
            string[] searchArray = searchQuery.Split(' ');
            int numSearchWords = searchArray.Length;

            foreach (string searchWord in searchArray)
            {
                if (numSearchWords <= searchWordNumber)
                {
                    lastWord = true;
                }
                if (lastWord)
                {
                    searchPart += searchWord;
                }
                else
                {
                    searchPart = searchPart + searchWord + "+";
                }
                searchWordNumber++;
            }

            return searchPart;
        }
        
        //put together base url, query search, and the search parameters
        private string BuildSearchQuery(SearchableWebsite websiteSearch, string toSearch)
        {

            string searchQuery = String.Format(@"{0}{1}{2}", websiteSearch.BaseURL, websiteSearch.QueryString, toSearch);

            return searchQuery;
        }

        private async void LaunchSearch(string search)
        {

            // Create a Uri object from a URI string 
            var uri = new System.Uri(search);

            //launch uri with search
            await Windows.System.Launcher.LaunchUriAsync(uri);

        }

        //find what is wanted to be searched
        private string GetSearchParameters()
        {
            string searchParameters = "";

            try
            {
                var searchRegex = new Regex("(?i)(for)(?-i)");
                searchParameters = searchRegex.Split(this.CommandString)[2].Trim();
                searchParameters = BuildSearchParameter(searchParameters);
            }
            catch(Exception)
            {

            }

            return searchParameters;
        }

    }
}
