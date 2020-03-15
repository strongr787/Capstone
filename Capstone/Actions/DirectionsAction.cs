namespace Capstone.Actions
{
    public class DirectionsAction : Action
    {
        private string DestinationName { get; set; }
        // TODO change to a different type once we know more about the location stuff for objects
        private string StartingPoint { get; set; }

        public DirectionsAction(string CommandString)
        {
            this.CommandString = CommandString;
        }

        private void getDestinationName()
        {
            // TODO ask for destination name and set it to our destinationName
        }

        private void getStartingPointName()
        {
            // TODO ask for starting location name and set it to our startingPoint
        }

        public override void PerformAction()
        {
            // TODO get map provider and do the stuff
        }
    }
}
