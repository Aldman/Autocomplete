using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Autocomplete
{
    [TestFixture]
    class RightBorderTests
    {
        private static IEnumerable<TestCaseData> GetRightCaseData
        {
            get
            {
                var phrases = new Phrases(
                    new string[] { "a", "ab", "abc" },
                    new string[] { "a" },
                    new string[] { "abc" });
                var prefix = "zzz";
                var expectedResult = phrases.Length;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult);

                prefix = "abc";
                expectedResult = 3;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult);

                prefix = "aa";
                expectedResult = 1;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult);

                prefix = "";
                expectedResult = phrases.Length;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult);

                prefix = "abb";
                expectedResult = 2;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult)
                    .SetName("abb");

                phrases = new Phrases(
                    new string[] { },
                    new string[] { },
                    new string[] { });
                prefix = "aa";
                expectedResult = phrases.Length;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult);

                phrases = new Phrases(
                    new string[] { "ab", "ab", "ab", "ab"},
                    new string[] { "a" },
                    new string[] { "abc" });
                prefix = "aa";
                expectedResult = 0;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult).
                    SetName("aa with expected 0");

                phrases = new Phrases(
                    new string[] { "a", "bcd", "bcefg", "bcefh", "bcf", "bcff", "zzz "},
                    new string[] { "a" },
                    new string[] { "abc" });
                prefix = "bc";
                expectedResult = 6;
                yield return new TestCaseData(phrases, prefix, -1, phrases.Length, expectedResult)
                    .SetName("bc");
            }
        }

        [TestCaseSource("GetRightCaseData")]
        public static void GetRightBorderIndex(IReadOnlyList<string> phrases,
            string prefix, int left, int right, int expectedResult)
        {
            var actualResult = RightBorderTask
                .GetRightBorderIndex(phrases, prefix, left, right);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
