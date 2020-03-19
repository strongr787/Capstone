using Capstone.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail("A general exception should not be thrown");
            }
        }

        [TestMethod]
        public void TestGetDateForNextWeekDay()
        {
            // we need to use a fixed date for this test to work. The date in this case is Sunday, March 1st 2020
            var today = new DateTime(2020, 3, 1);
            var nextDay = DayOfWeek.Wednesday;
            // the date we're expecting is Wednesday, March 11th 2020
            var expectedDate = new DateTime(2020, 3, 11);
            var actualDate = DateParser.GetDateForNextWeekDay(nextDay, today);
            Assert.AreEqual(expectedDate, actualDate);
        }
    }
}
