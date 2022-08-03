using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        /// <remarks>
        /// Функция должна быть НЕ рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>

        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            int median = 0;

            while (right != left)
            {
                median = left + (right - left) / 2;
                if (median < 0)
                    break;
                if (String.Compare(prefix, phrases[median]) <= 0)
                    right = median;
                else left = median + 1;
            }
            while (right != phrases.Count)
            {
                if (phrases[right].StartsWith(prefix))
                    right++;
                else break;
            }
            return right;
        }
    }
}