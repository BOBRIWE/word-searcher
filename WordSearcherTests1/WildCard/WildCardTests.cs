using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordSearcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearcher.Tests
{
    [TestClass()]
    public class WildCardTests
    {
        [TestMethod()]
        public void CleanPatternTest()
        {
            Assert.AreEqual("ааа*фф*ф", WildCard.CleanPattern("ааа****фф**ф"));
            Assert.AreEqual("а*б*в", WildCard.CleanPattern("а*б*в"));
            Assert.AreEqual("при*вет*мир*", WildCard.CleanPattern("при***вет***мир***"));
            Assert.AreEqual("*бон*жур*", WildCard.CleanPattern("****бон**жур**"));
        }

        [TestMethod()]
        public void MatchTest()
        {
            Assert.IsTrue(WildCard.Match("пост", "по?т"));
            Assert.IsTrue(WildCard.Match("порт", "по?т"));
            Assert.IsFalse(WildCard.Match("пот", "по?т"));

            Assert.IsTrue(WildCard.Match("здравый", "здрав*й*"));
            Assert.IsTrue(WildCard.Match("здравствуй", "здрав*й*"));
            Assert.IsTrue(WildCard.Match("здравствуйте", "здрав*й*"));
            Assert.IsFalse(WildCard.Match("здравие", "здрав*й*"));
        }
    }
}