using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    [TestFixture]
    public class LeftBorderTests
    {
        private static IEnumerable<TestCaseData> GetLeftCaseData
        {
            get
            {
                var phrases = new Phrases(
                     new string[] {"a"},
                     new string[] { "a", "ab", "abc"},
                     new string[] { "abc" });
                var prefix = "abc";
                var expectedResult = 2;

                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult);
            }
        }

        [TestCaseSource("GetLeftCaseData")]
        public void GetLeftBorderIndex(IReadOnlyList<string> phrases,
            string prefix, int left, int right, int expectedResult)
        {
            var actualResult = LeftBorderTask
                .GetLeftBorderIndex(phrases, prefix, left, right);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
