using Capstone.Common;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace Capstone.Actions
{
    public class WebsiteSearchAction : Action
    {
        private SearchableWebsite desiredSearchableWebsite { get; set;}
        private SearchEngine desiredSearchEngine { get; set; }
        public WebsiteSearchAction(string CommandString)
        {
            this.CommandString = CommandString;
        }


    public override void PerformAction()
        {
             //get list of searchable websites from database
            List<SearchableWebsite> allSearchableWebsites =  StoredProcedures.QueryAllSearchableWebsites();
            //need to check if user provided a website to search
            bool isUserProvidedWebsiteSearch = false;
            //go through list of searchable websites. return true if user included the searchable website in search
            //this will also set the website if there is a match
            isUserProvidedWebsiteSearch = GetActionFromCommand(allSearchableWebsites);
            string searchParameters;
            string searchQuery;

            if (isUserProvidedWebsiteSearch)
            {
                //find what is wanted to be searched and concatenate with + for end of url
                searchParameters = GetSearchParameters();
                searchQuery = BuildSearchQuery(desiredSearchableWebsite, searchParameters);
                //launch browser. this will be done with the default browser
                LaunchSearch(searchQuery);
            }else
            {
                //sets desiredSearchEngine, which is the default selected in settings
                GetDefaultSearchEngine();
                searchParameters = GetSearchParameters();
                searchQuery = BuildSearchQuery(desiredSearchEngine, searchParameters);
                //launch browser. this will be done with the default browser
                LaunchSearch(searchQuery);
            }
        }

        private void GetDefaultSearchEngine()
        {
            Setting preferredSearchEngineSetting = StoredProcedures.QuerySettingByName("Search Engine");
            string preferredSearchEngineName = preferredSearchEngineSetting.GetSelectedOption().DisplayName;
            // after this, use the name to query info from the search engine table
            desiredSearchEngine = StoredProcedures.QuerySearchEngineByName(preferredSearchEngineName);
        }
        private bool GetActionFromCommand(List<SearchableWebsite> allSearchableWebsites)
        {
            var amazonRegex = new Regex("(?i)(amazon)(?-i)");
            var youtubeRegex = new Regex("(?i)(youtube)(?-i)");
            var walmartRegex = new Regex("(?i)(walmart)(?-i)");
 

            bool isDesiredSearchableWebsite;
            foreach (SearchableWebsite searchableWebsite in allSearchableWebsites)
            { 
                if (amazonRegex.IsMatch(this.CommandString) || youtubeRegex.IsMatch(this.CommandString) || walmartRegex.IsMatch(this.CommandString))
                {
                    desiredSearchableWebsite = searchableWebsite;
                    isDesiredSearchableWebsite = true;
                    return isDesiredSearchableWebsite;
                }
            }
            isDesiredSearchableWebsite = false;
            return isDesiredSearchableWebsite;
        }
        
        //put together base url, query search, and the search parameters
        private string BuildSearchQuery(SearchableWebsite websiteSearch, string toSearch)
        {
            string searchQuery = websiteSearch.BaseURL + websiteSearch.QueryString + WebUtility.UrlEncode(toSearch);

            return searchQuery;
        }

        private string BuildSearchQuery(SearchEngine searchEngine, string toSearch)
        {
            string searchQuery = searchEngine.BaseURL + searchEngine.QueryString + WebUtility.UrlEncode(toSearch);

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
            }
            catch(Exception)
            {

            }

            return searchParameters;
        }

    }
}
