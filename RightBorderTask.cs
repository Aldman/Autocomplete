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
            // TODO: сделать более изящное решение
            // Найти индекс первого элемента большего заданного или N, если такого нет (правая граница)
            int median = 0;
            while (right != left)
            {
                median = left + (right - left) / 2;
                if (median < 0)
                {
                    right = phrases.Count;
                    break;
                }
                if (String.Compare(prefix, phrases[median]) <= 0)
                    right = median;
                else left = median + 1;
            }
            if ((phrases.Count - right == 0) || (phrases[right] != prefix))
            {
                if (!String.IsNullOrEmpty(phrases[right]))
                return right;
            }
            return right + 1;
        }
    }
}