﻿using Capstone.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class DateTimeParserTests
    {
        [TestMethod]
        public void TestParseWeekDayFromStringFindsCorrectDay()
        {
            Assert.AreEqual(DayOfWeek.Wednesday, DateTimeParser.ParseWeekDayFromString("The day of the week is WednesDay"));
        }

        [TestMethod]
        public void TestParseWeekDayFromStringThrowsErrorIfNoneMatched()
        {
            try
            {
                DateTimeParser.ParseWeekDayFromString("The day of the week is");
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
            var actualDate = DateTimeParser.GetDateForNextWeekDay(nextDay, today);
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
            var actualDate = DateTimeParser.GetDateForThisWeekDay(nextDay, today);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestParseExactDateReturnsCorrectDateObject()
        {
            var dayBeforeMonth = "set an alarm for the 05th of March";
            var dayAfterMonth = "add a reminder for october 22nd";

            DateTime beforeMonthDate = DateTimeParser.ParseExactDate(dayBeforeMonth);
            DateTime afterMonthDate = DateTimeParser.ParseExactDate(dayAfterMonth);
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
                DateTimeParser.ParseExactDate("");
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
            DateTime actualDate = DateTimeParser.ParseExactDate(input, today);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestGetNextOccurrenceOfDateProperlyShiftsDate()
        {
            // the date we're using as the "now" for the test method
            var now = new DateTime(2020, 3, 19);
            var day = 18;
            var month = 3;
            Assert.AreEqual(new DateTime(2021, month, day), DateTimeParser.GetNextOccurrenceOfDate(day, month, now));
        }

        [TestMethod]
        public void TestGetNextOccurrenceOfDateDoesNotShiftDateIfItHasNotPassed()
        {
            var now = new DateTime(2020, 3, 19);
            var day = 20;
            var month = 3;
            Assert.AreEqual(new DateTime(2020, month, day), DateTimeParser.GetNextOccurrenceOfDate(day, month, now));
        }

        [TestMethod]
        public void TestParseSlashOrDashNotationDoesNotShiftYearIfSpecified()
        {
            var input = "Set an alarm for 5/5/2020";
            var expectedDate = new DateTime(2020, 5, 5);
            var actualDate = DateTimeParser.ParseSlashOrDashNotation(input);

            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestParseSlashOrDashNotationProvidesYearIfNotSpecified()
        {
            var input = "Set an alarm for 5-5";
            // the date used for the starting point so that the test is stable
            var now = new DateTime(2020, 3, 19);
            var expectedDate = new DateTime(2020, 5, 5);
            var actualDate = DateTimeParser.ParseSlashOrDashNotation(input, now);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestGetDateForRelativeOffsetReturnsCorrectlyOffsetDate()
        {
            var testDate = new DateTime(2020, 3, 22);
            var inputString = "add a reminder for 3 weeks from now";
            var expectedDate = new DateTime(2020, 4, 12);
            var actualDate = DateTimeParser.GetDateForRelativeOffset(inputString, testDate);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void TestGetTimePartOfStringFindsProperParts()
        {
            // test several different time formats
            Assert.AreEqual("7", DateTimeParser.GetTimePartOfString("Set an alarm for 7"));
            Assert.AreEqual("7:30", DateTimeParser.GetTimePartOfString("set an alarm for 7:30"));
            Assert.AreEqual("7:30 pm", DateTimeParser.GetTimePartOfString("set an alarm for 7:30 pm called do unit tests"));
            Assert.AreEqual("19", DateTimeParser.GetTimePartOfString("set an alarm for 19 o'clock"));
            Assert.AreEqual("5 am", DateTimeParser.GetTimePartOfString("set an alarm called take the 3 youngest kids to school at 5 am"));
            Assert.AreEqual("905 am", DateTimeParser.GetTimePartOfString("add a reminder for November 12th at 905 am"));
            Assert.AreEqual("12:00", DateTimeParser.GetTimePartOfString("set an alarm at 12:00 called hi"));
            Assert.AreEqual("2359 pm", DateTimeParser.GetTimePartOfString("2359 pm"));
            Assert.AreEqual("0000", DateTimeParser.GetTimePartOfString("0000"));
            Assert.AreEqual("5pm", DateTimeParser.GetTimePartOfString("set an alarm for 5pm on November 12"));
        }

        [TestMethod]
        public void TestFormatTimeFormatsTimeProperlyDependingOnNumberOfDigits()
        {
            Assert.AreEqual("7:45", DateTimeParser.FormatTime("745"));
            Assert.AreEqual("7:00 am", DateTimeParser.FormatTime("7 Am"));
            Assert.AreEqual("23:59", DateTimeParser.FormatTime("23 59"));
        }

    }
}
