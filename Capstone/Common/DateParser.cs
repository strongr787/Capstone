using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Capstone.Common
{
    public static class DateParser
    {
        /// <summary>
        /// scans the pased text for one of the days of the week and returns which <see cref="System.DayOfWeek"/> matches the string. The check is case-insensitive.
        /// <br />
        /// If no day of week was matched, a <see cref="DateParseException"/> will be thrown
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
                    throw new DateParseException($"The string [{text}] does not contain a day of the week!");
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

        public static DateTime GetDateForThisWeekDay(System.DayOfWeek nextDay, DateTime date)
        {
            // get the number of days in between the date's day of the week and the passed nextDay
            DayOfWeek today = date.DayOfWeek;
            // if today is after the passed nextDay, then throw an error
            if (today > nextDay)
            {
                throw new DateParseException($"[{nextDay.ToString()}] has already passed");
            }
            // formula to get the number of days until the specified next day = Math.Abs(today - nextDay)
            int numberOfDaysUntilNextDay = Math.Abs(today - nextDay);
            // create a new DateTime from the passed date so that we don't modify the passed date
            DateTime newDate = new DateTime(date.Ticks).AddDays(numberOfDaysUntilNextDay);
            return newDate;
        }

        public static DateTime ParseExactDate(string text)
        {
            // get the first occurrence of a month within the text
            Months referencedMonth = GetMonthFromString(text);
            if (referencedMonth == Months.NONE)
            {
                throw new DateParseException("Month not found in text!");
            }
            // the datetime to return
            DateTime dateTime;
            // get the index of the month in the string and determine where the day of the month is using a regex
            string pattern = $"[0-9]{{1,2}}(?=(th|rd|st) of {referencedMonth.ToString().ToLower()})|(?<={referencedMonth.ToString().ToLower()} )[0-9]{{1,2}}";
            Regex dayOfMonthRegex = new Regex(pattern);
            var match = dayOfMonthRegex.Match(text.ToLower());
            if (match.Success)
            {
                dateTime = new DateTime(DateTime.Now.Year, (int)referencedMonth, int.Parse(match.Value));
            }
            else
            {
                throw new DateParseException("Regex match could not find the month's day!");
            }
            return dateTime;
        }

        private static int FindFirstIndexOfMonth(string text, Months month)
        {
            text = text.ToLower();
            int matchingIndex = text.ToLower().IndexOf(month.ToString().ToLower());
            return matchingIndex;
        }

        private static Months GetMonthFromString(string text)
        {
            text = text.ToLower();
            Months foundMonth = Months.NONE;
            // get an array of the months
            var months = Enum.GetValues(typeof(Months));
            foreach (Months month in months)
            {
                if (text.Contains(month.ToString().ToLower()))
                {
                    foundMonth = month;
                    break;
                }
            }
            return foundMonth;
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

        private enum Months
        {
            // used to indicate an error
            NONE = -1,
            JANUARY = 1,
            FEBRUARY = 2,
            MARCH = 3,
            APRIL = 4,
            MAY = 5,
            JUNE = 6,
            JULY = 7,
            AUGUST = 8,
            SEPTEMBER = 9,
            OCTOBER = 10,
            NOVEMBER = 11,
            DECEMBER = 12
        }
    }

    public class DateParseException : Exception
    {
        public DateParseException()
        {
        }

        public DateParseException(string message) : base(message)
        {
        }

        public DateParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DateParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
