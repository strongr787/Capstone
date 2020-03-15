using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Actions
{
    public class AlarmAction : Action
    {
        private string Title { get; set; }
        private DateTime ActivateDateAndTime { get; set; }
        private AlarmActionTypes ActionType { get; set; }
        private string CommandString { get; set; }

        public AlarmAction(string Title, DateTime ActivateDateTime, AlarmActionTypes ActionType)
        {
            this.Title = Title;
            this.ActivateDateAndTime = ActivateDateTime;
            this.ActionType = ActionType;
        }

        public AlarmAction(AlarmActionTypes ActionType, string CommandString)
        {
            this.ActionType = ActionType;
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
        }

        public enum AlarmActionTypes
        {
            CREATE = 0,
            EDIT = 1,
            DELETE = 2
        }
    }
}
