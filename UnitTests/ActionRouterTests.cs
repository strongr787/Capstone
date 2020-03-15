using Capstone.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class ActionRouterTests
    {
        [TestMethod]
        public void TestFindKeywordReturnsMatchedKeyword()
        {
            const string keyword = "piano";
            string inputString = $"this is the input string, and the keyword is {keyword}";
            // create the dictionary of the values
            Dictionary<string, dynamic> inputDict = new Dictionary<string, dynamic>();
            inputDict.Add("notIt", "whatever");
            inputDict.Add("weather", "whatever");
            // the uppercase P tests case insensitivity
            inputDict.Add("Piano", "whatever");
            inputDict.Add("alarms", "whatever");
            inputDict.Add("reminders", "whatever");
            string matchingKeyword = ActionRouter.FindKeyword(inputString, inputDict);
            Assert.AreEqual(keyword, matchingKeyword);
        }

        [TestMethod]
        public void TestFindKeywordReturnsNullIfKeywordIsNotFound()
        {
            const string keyword = "pianoforte";
            string inputString = $"this is the input string, and the keyword is {keyword}";
            // create the dictionary of the values
            Dictionary<string, dynamic> inputDict = new Dictionary<string, dynamic>();
            inputDict.Add("notIt", "whatever");
            inputDict.Add("weather", "whatever");
            // the uppercase P tests case insensitivity
            inputDict.Add("Piano", "whatever");
            inputDict.Add("alarms", "whatever");
            inputDict.Add("reminders", "whatever");
            string matchingKeyword = ActionRouter.FindKeyword(inputString, inputDict);
            Assert.IsNull(matchingKeyword);
        }
    }
}
