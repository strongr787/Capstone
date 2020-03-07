using System;

namespace Capstone.Models
{
    class Alarm
    {
        protected string Title { get; set; }
        public DateTime ActivateDateAndTime { get; set; }
        public bool IsDeleted { get; set; }

        public Alarm() : this("", DateTime.MaxValue, false)
        {
        }

        /// <summary>
        /// Creates a new alarm, with the title, date, and deleted status
        /// </summary>
        /// <param name="Title">The title of the alarm that shows up on the UI</param>
        /// <param name="ActivateDateAndTime">The date and time that the alarm is set to go off</param>
        /// <param name="IsDeleted">whether or not the alarm is set to be deleted. If marked as true, it will not go off and will not appear on the UI</param>
        public Alarm(string Title, DateTime ActivateDateAndTime, bool IsDeleted)
        {
            this.Title = Title;
            this.ActivateDateAndTime = ActivateDateAndTime;
            this.IsDeleted = IsDeleted;
        }

    }
}
