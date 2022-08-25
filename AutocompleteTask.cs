using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];

            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var prefixForReturning = new List<string>();
            int nextIndex = LeftBorderTask
                .GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var endIndex = RightBorderTask
                .GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            if (endIndex - nextIndex <= 1) return new string[0];
            nextIndex++;

            for (int i = 0; i < count; i++)
            {
                if (phrases[nextIndex].StartsWith(prefix))
                    prefixForReturning.Add(phrases[nextIndex]);
                else break;
                if (++nextIndex == phrases.Count) break; 
            }
            return prefixForReturning.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
            var leftBorder = LeftBorderTask
                .GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rigthBorder = RightBorderTask
                .GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            //var nextIndex = leftBorder + 1;
            var phrasesCountByPrefix = 0;
            for (int i = leftBorder + 1; i < rigthBorder; i++)
            {
                if (phrases[i].StartsWith(prefix))
                    phrasesCountByPrefix++;
                else break;
            }
            return phrasesCountByPrefix;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        #region Тесты Count ByPrefix


        #endregion

        #region Тесты Top Prefix
        private static IEnumerable<TestCaseData> TopPrefixCaseData
        {
            get
            {
                var phrases = new Phrases(
                    new string[] { "aa", "ab", "ac", "bc" },
                    new string[] { "" },
                    new string[] { "" });
                var prefix = "a";
                var count = 2;
                var expectedResult = new string[]
                {
                    "aa  ", "ab  "
                };
                yield return new TestCaseData(phrases, prefix, count, expectedResult);

                phrases = new Phrases(
                    new string[] { "aa" },
                    new string[] { "" },
                    new string[] { "" });
                expectedResult = new string[]
                {
                    "aa  "
                };
                yield return new TestCaseData(phrases, prefix, count, expectedResult);

                phrases = new Phrases(
                    new string[] { "aa", "ab", "ac" },
                    new string[] { "" },
                    new string[] { "" });
                prefix = "z";
                count = 2;
                expectedResult = new string[]
                {

                };
                yield return new TestCaseData(phrases, prefix, count, expectedResult);

                phrases = new Phrases(
                    new string[0],
                    new string[0],
                    new string[0]);
                prefix = "z";
                count = 2;
                expectedResult = new string[]
                {

                };
                yield return new TestCaseData(phrases, prefix, count, expectedResult);

                phrases = new Phrases(
                    new string[] { "a", "b", "c", "c", "d", "e" },
                    new string[] { "" },
                    new string[] { "" });
                prefix = "c";
                count = 10;
                expectedResult = new string[]
                {
                    "c  ", "c  "
                };
                yield return new TestCaseData(phrases, prefix, count, expectedResult);

                phrases = new Phrases(
                    new string[] { "aa", "ab", "bc", "bd", "be", "ca", "cb" },
                    new string[] { "" },
                    new string[] { "" });
                prefix = "a";
                count = 2;
                expectedResult = new string[]
                {
                    "aa  ", "ab  "
                };
                yield return new TestCaseData(phrases, prefix, count, expectedResult);
            }
        }

        [TestCaseSource("TopPrefixCaseData")]
        public void GetTopByPrefix(IReadOnlyList<string> phrases,
            string prefix, int count, string[] expectedResult)
        {
            var actualResult = AutocompleteTask
                .GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, actualResult);
        }
        #endregion
    }
}
