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
    public class StringUtilsTests
    {
        [TestMethod]
        public void TestIsBlankReturnsTrueForNull()
        {
            Assert.IsTrue(StringUtils.IsBlank(null));
        }

        [TestMethod]
        public void TestIsBlankReturnsTrueForEmpty()
        {
            Assert.IsTrue(StringUtils.IsBlank(""));
        }

        [TestMethod]
        public void TestIsBlankReturnsTrueForOnlyWhitespace()
        {
            Assert.IsTrue(StringUtils.IsBlank("   \t\n"));
        }

        [TestMethod]
        public void TestIsBlankReturnsFalseForNonBlankString()
        {
            Assert.IsFalse(StringUtils.IsBlank("null hi   "));
        }
    }
}
