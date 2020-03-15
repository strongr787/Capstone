using System;

namespace Capstone.Actions
{
    public class AlarmAction : Action
    {
        public string Title { get; set; }
        public DateTime ActivateDateAndTime { get; set; }
        public AlarmActionTypes ActionType { get; set; }

        public AlarmAction(string Title, DateTime ActivateDateTime, AlarmActionTypes ActionType, string CommandString)
        {
            this.Title = Title;
            this.ActivateDateAndTime = ActivateDateTime;
            this.ActionType = ActionType;
            this.CommandString = CommandString;
        }

        public AlarmAction(AlarmActionTypes ActionType, string CommandString)
        {
            this.ActionType = ActionType;
            this.CommandString = CommandString;
        }

        private string RequestTitle()
        {
            var title = "";
            // TODO speak out to request title, and listen for title input

            return title;
        }

        private DateTime RequestActivatedDateAndTime()
        {
            var activatedDateTime = System.DateTime.Now;
            // TODO implement a text -> dateTime parser

            return activatedDateTime;
        }

        public override void PerformAction()
        {
            // TODO
        }

        public enum AlarmActionTypes
        {
            CREATE = 0,
            EDIT = 1,
            DELETE = 2
        }
    }
}
