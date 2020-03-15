using System;

namespace Capstone.Actions
{
    public class TimeAction : Action
    {

        public TimeAction(string CommandString)
        {
            this.CommandString = CommandString;
        }

        public override void PerformAction()
        {
            // format the current date and time to be readable by our text to speech functionality
            string currentDateTime = $"Currently, it is {DateTime.Now.ToString("f")}";
            // TODO hook up our text to speech library to output the above string
        }
    }
}
