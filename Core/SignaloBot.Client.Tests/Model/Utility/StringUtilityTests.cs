﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignaloBot.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
namespace SignaloBot.Client.Tests
{
    [TestClass()]
    public class StringUtilityTests
    {
        [TestMethod()]
        public void StringUtility_ShortenSubjectStringTest()
        {
            string stringFormat = "{0}. Static part of the {0} message format.";
            string dynamicPart = "Dynamically generated";

            int staticPartLength = string.Format(stringFormat, string.Empty).Length;
            int dynamicStringRepeatTimes = new Regex(@"\{0\}", RegexOptions.IgnoreCase).Matches(stringFormat).Count;
           
            string shortSuffix = "...";
            int expectedFullStringLength = staticPartLength + dynamicPart.Length * dynamicStringRepeatTimes;            
            int shortenedFullStringLength = expectedFullStringLength - shortSuffix.Length * dynamicStringRepeatTimes;
            
            string shortenedString = StringUtility.ShortenSubjectString(
                dynamicPart, staticPartLength, shortenedFullStringLength, dynamicStringRepeatTimes);

            Assert.IsTrue(shortenedString.EndsWith(shortSuffix));
        }
    }
}
