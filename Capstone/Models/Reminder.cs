using System;

namespace Capstone.Models
{
    class Reminder
    {

        protected string Title { get; set; }
        public DateTime ActivateDateAndTime { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public Reminder() : this("", DateTime.MaxValue, "", false)
        {
        }

        /// <summary>
        /// Creates a new reminder, with the title, date, and deleted status
        /// </summary>
        /// <param name="Title">The title of the reminder that shows up on the UI</param>
        /// <param name="ActivateDateAndTime">The date and time that the reminder is set to go off</param>
        /// <param name="Description">The description of the reminder to show on the UI</param>
        /// <param name="IsDeleted">whether or not the reminder is set to be deleted. If marked as true, it will not go off and will not appear on the UI</param>
        public Reminder(string Title, DateTime ActivateDateAndTime, string Description, bool IsDeleted)
        {
            this.Title = Title;
            this.ActivateDateAndTime = ActivateDateAndTime;
            this.Description = Description;
            this.IsDeleted = IsDeleted;
        }
    }
}
