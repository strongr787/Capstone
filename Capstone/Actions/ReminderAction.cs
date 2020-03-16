using System;

namespace Capstone.Actions
{
    public class ReminderAction : Action
    {
        private string ReminderTitle { get; set; }
        public ReminderActionTypes ActionType { get; set; }
        private string ReminderDescription { get; set; }
        public DateTime ActivateDateAndTime { get; set; }

        public ReminderAction(ReminderActionTypes ActionType, string CommandString)
        {
            this.ActionType = ActionType;
            this.CommandString = CommandString;
        }

        public override void PerformAction()
        {
            // TODO
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

        private string RequestDescription()
        {
            var description = "";
            // TODO speak out to request the description, and await description input

            return description;
        }

        public enum ReminderActionTypes
        {
            CREATE = 0,
            DELETE = 1,
            EDIT = 2
        }
    }
}
