namespace Capstone.Actions
{
    class WebsiteSearchAction : Action
    {
        private string WebsiteName { get; set; }
        private string SearchText { get; set; }

        public WebsiteSearchAction(string CommandString)
        {
            this.CommandString = CommandString;
        }

        public override void PerformAction()
        {
            // TODO parse out the website to search, pull its info from the database, parse out what the user wants to search on, and search the website
        }
    }
}
