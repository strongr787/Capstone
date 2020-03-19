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

            Assert.AreEqual(new DateTime(2020, 3, 5), beforeMonthDate);
            Assert.AreEqual(new DateTime(2020, 10, 22), afterMonthDate);
        }

        [TestMethod]
        public void TestParseExactDateThrowsExceptionIfParseFails()
        {
            try
            {
                DateParser.ParseExactDate("");
            } catch(DateParseException)
            {
                Assert.IsTrue(true); // just to have an assert
            } catch(Exception e)
            {
                Assert.Fail("A general exception means something else failed: " + e.Message);
            }
        }
    }
}
