using System;

namespace Capstone.Common
{
    public static class DateParser
    {
        /// <summary>
        /// scans the pased text for one of the days of the week and returns which <see cref="System.DayOfWeek"/> matches the string. The check is case-insensitive.
        /// <br />
        /// If no day of week was matched, a FormatException will be thrown
        /// </summary>
        /// <param name="text">the text to check</param>
        /// <returns>the matching day of week.</returns>
        public static System.DayOfWeek ParseWeekDayFromString(string text)
        {
            text = text.ToLower();
            DaysOfWeek matchedDay = DaysOfWeek.NONE;
            var dayOfWeekList = Enum.GetValues(typeof(DaysOfWeek));
            foreach (DaysOfWeek day in dayOfWeekList)
            {
                string dayName = day.ToString().ToLower();
                if (text.Contains(dayName))
                {
                    matchedDay = day;
                }
            }
            // figure out which System.DayOfWeek to return
            switch (matchedDay)
            {
                case DaysOfWeek.SUNDAY:
                    return DayOfWeek.Sunday;
                case DaysOfWeek.MONDAY:
                    return DayOfWeek.Monday;
                case DaysOfWeek.TUESDAY:
                    return DayOfWeek.Tuesday;
                case DaysOfWeek.WEDNESDAY:
                    return DayOfWeek.Wednesday;
                case DaysOfWeek.THURSDAY:
                    return DayOfWeek.Thursday;
                case DaysOfWeek.FRIDAY:
                    return DayOfWeek.Friday;
                case DaysOfWeek.SATURDAY:
                    return DayOfWeek.Saturday;
                case DaysOfWeek.NONE:
                default:
                    throw new FormatException($"The string [{text}] does not contain a day of the week!");
            }
        }

        /// <summary>
        /// Returns a date that is on the specified <paramref name="nextDay"/> 1 week after the passed <paramref name="date"/>
        /// </summary>
        /// <param name="nextDay">The weekday to get the date for</param>
        /// <param name="date">The date to get the weekday for relative to</param>
        /// <returns>a new date 1 week after the passed date on the passed nextDay</returns>
        public static DateTime GetDateForNextWeekDay(System.DayOfWeek nextDay, DateTime date)
        {
            // get the number of days in between the date's day of the week and the passed nextDay
            DayOfWeek today = date.DayOfWeek;
            // formula to get the number of days until the specified next day = 7 - (today - nextDay)
            int numberOfDaysUntilNextDay = 7 - (today - nextDay);
            // create a new DateTime from the passed date so that we don't modify the passed date
            DateTime newDate = new DateTime(date.Ticks).AddDays(numberOfDaysUntilNextDay);
            return newDate;
        }

        public static DateTime GetDateForNextWeekDay(System.DayOfWeek nextDay)
        {
            return GetDateForNextWeekDay(nextDay, DateTime.Now);
        }

        private enum DaysOfWeek
        {
            // used to determine if there's an error
            NONE = -1,
            SUNDAY = DayOfWeek.Sunday,
            MONDAY = DayOfWeek.Monday,
            TUESDAY = DayOfWeek.Tuesday,
            WEDNESDAY = DayOfWeek.Wednesday,
            THURSDAY = DayOfWeek.Thursday,
            FRIDAY = DayOfWeek.Friday,
            SATURDAY = DayOfWeek.Saturday
        }
    }
}
