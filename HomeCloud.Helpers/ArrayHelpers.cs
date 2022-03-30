using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Helpers
{
    public static class ArrayHelpers
    {
        /// <summary>
        /// Remove an element at a determined index in the array
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">Array on which we work</param>
        /// <param name="index">Index of the element to remove</param>
        /// <returns>A new array without the element at determined index</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Length) throw new ArgumentOutOfRangeException(nameof(index));

            T[] finalArray = new T[array.Length - 1];

            int j = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i == index) continue;
                finalArray[j] = array[i];
                j++;
            }

            return finalArray;
        }
    }
}
