using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordSearcher.DictionaryHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace WordSearcher.DictionaryHolder.Tests
{
    [TestClass()]
    public class TXTFileTests
    {
        [TestMethod()]
        public void GetWordsArrTest()
        {
            TXTFile file = new TXTFile("../../DictionaryHolder/testInput_1.txt");

            string[] actual = file.GetWordsArr();

            StreamReader sr = new StreamReader("../../DictionaryHolder/testOutput_1.txt", Encoding.Default);

            List<string> expected = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                expected.Add(line);
            }

            string[] expectedArr;
            expectedArr = expected.Distinct().ToArray();

            Assert.IsTrue(actual.Length == expectedArr.Length);
        }
    }
}