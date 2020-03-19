using Capstone.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class DateParserTests
    {
        [TestMethod]
        public void TestParseWeekDayFromStringFindsCorrectDay()
        {
            Assert.AreEqual(DayOfWeek.Wednesday, DateParser.ParseWeekDayFromString("The day of the week is WednesDay"));
        }

        [TestMethod]
        public void TestParseWeekDayFromStringThrowsErrorIfNoneMatched()
        {
            try
            {
                DateParser.ParseWeekDayFromString("The day of the week is");
            }
            catch (DateParseException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail("A general exception should not be thrown");
            }
        }

        [TestMethod]
        public void TestGetDateForNextWeekDayReturnsCorrectDate()
        {
            // we need to use a fixed date for this test to work. The date in this case is Sunday, March 1st 2020
            var today = new DateTime(2020, 3, 1);
            var nextDay = DayOfWeek.Wednesday;
            // the date we're expecting is Wednesday, March 11th 2020
            var expectedDate = new DateTime(2020, 3, 11);
            var actualDate = DateParser.GetDateForNextWeekDay(nextDay, today);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestGetDateForThisWeekDayReturnsCorrectDate()
        {
            // we need to use a fixed date for this test to work. The date in this case is Sunday, March 1st 2020
            var today = new DateTime(2020, 3, 1);
            var nextDay = DayOfWeek.Wednesday;
            // the date we're expecting is Wednesday, March 4th 2020
            var expectedDate = new DateTime(2020, 3, 4);
            var actualDate = DateParser.GetDateForThisWeekDay(nextDay, today);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestParseExactDateReturnsCorrectDateObject()
        {
            var dayBeforeMonth = "set an alarm for the 05th of March";
            var dayAfterMonth = "add a reminder for october 22nd";

            DateTime beforeMonthDate = DateParser.ParseExactDate(dayBeforeMonth);
            DateTime afterMonthDate = DateParser.ParseExactDate(dayAfterMonth);
            // the expected values
            var expectedBeforeMonth = new DateTime(2020, 3, 5);
            var expectedAfterMonth = new DateTime(2020, 10, 22);

            Assert.AreEqual(expectedBeforeMonth.Day, beforeMonthDate.Day);
            Assert.AreEqual(expectedBeforeMonth.Month, beforeMonthDate.Month);
            Assert.AreEqual(expectedAfterMonth.Day, afterMonthDate.Day);
            Assert.AreEqual(expectedAfterMonth.Month, afterMonthDate.Month);
        }

        [TestMethod]
        public void TestParseExactDateThrowsExceptionIfParseFails()
        {
            try
            {
                DateParser.ParseExactDate("");
            }
            catch (DateParseException)
            {
                Assert.IsTrue(true); // just to have an assert
            }
            catch (Exception e)
            {
                Assert.Fail("A general exception means something else failed: " + e.Message);
            }
        }

        [TestMethod]
        public void TestParseExactDateShiftsYearIfMonthIsPassed()
        {
            // the "today" used to prevent this test from failing after the months have changed
            var today = new DateTime(2020, 6, 1);

            // another test handles the placement of the day, this one only needs to check for one day placement
            var input = "Set an alarm for the 1st of May";
            DateTime expectedDate = new DateTime(2021, 5, 1);
            DateTime actualDate = DateParser.ParseExactDate(input, today);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestGetNextOccurrenceOfDateProperlyShiftsDate()
        {
            // the date we're using as the "now" for the test method
            var now = new DateTime(2020, 3, 19);
            var day = 18;
            var month = 3;
            Assert.AreEqual(new DateTime(2021, month, day), DateParser.GetNextOccurrenceOfDate(day, month, now));
        }

        [TestMethod]
        public void TestGetNextOccurrenceOfDateDoesNotShiftDateIfItHasNotPassed()
        {
            var now = new DateTime(2020, 3, 19);
            var day = 20;
            var month = 3;
            Assert.AreEqual(new DateTime(2020, month, day), DateParser.GetNextOccurrenceOfDate(day, month, now));
        }

        [TestMethod]
        public void TestParseSlashOrDashNotationDoesNotShiftYearIfSpecified()
        {
            var input = "Set an alarm for 5/5/2020";
            var expectedDate = new DateTime(2020, 5, 5);
            var actualDate = DateParser.ParseSlashOrDashNotation(input);

            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestParseSlashOrDashNotationProvidesYearIfNotSpecified()
        {
            var input = "Set an alarm for 5-5";
            // the date used for the starting point so that the test is stable
            var now = new DateTime(2020, 3, 19);
            var expectedDate = new DateTime(2020, 5, 5);
            var actualDate = DateParser.ParseSlashOrDashNotation(input, now);
            Assert.AreEqual(expectedDate, actualDate);
        }
    }
}
