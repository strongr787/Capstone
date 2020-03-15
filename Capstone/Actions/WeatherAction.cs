namespace Capstone.Actions
{
    class WeatherAction : Action
    {

        public WeatherAction(string CommandString)
        {
            this.CommandString = CommandString;
        }

        public override void PerformAction()
        {
            // TODO pull weather provider info from the database, get current location, and get weather info
        }
    }
}
