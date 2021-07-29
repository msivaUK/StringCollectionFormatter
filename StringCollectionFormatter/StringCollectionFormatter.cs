using System;
using System.Collections.Generic;
using System.Text;

namespace StringsFormatter
{
    public class StringCollectionFormatter
    {
        private readonly string _removeChars="_4";
        private readonly char _fromCurrencySymbol='$';
        private readonly char _toCurrencySymbol='£';

                /// <summary>
        /// Checks string collection for  nulls
        /// </summary>
        /// <param name="stringCollection"></param>
        /// <returns></returns>
        private bool  IsInvalidCollection(ICollection<string> stringCollection)
        {
            if (stringCollection is null) return true;
            if (stringCollection.Contains(null)) return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private string FormatString(string original)
        {
            var stringBuilder = new StringBuilder();
            var charArray = original.ToCharArray();
            for (int i = 0; i < charArray.Length && stringBuilder.Length < 15; i++)
            {
                var differentFromPrevious = i == 0 || (charArray[i] != charArray[i - 1]);

                if (differentFromPrevious)
                {
                    var isNotCharToRemove = !_removeChars.Contains(charArray[i]);
                    if (isNotCharToRemove)
                    {
                        var charToAdd = (charArray[i] == _fromCurrencySymbol) ? _toCurrencySymbol : charArray[i];
                        stringBuilder.Append(charToAdd);
                    }
                }
            }

            if (stringBuilder.Length == 0) throw new InvalidOperationException("Empty string returned");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Takes a collection of strings, truncates each string to a maximium of 15 chars, 
        /// removes special characters, duplicates and replaces currency symbol
        /// </summary>
        /// <param name="stringCollection">collection of strings</param>
        /// <returns></returns>
        public List<string> FormatItems(ICollection<string> stringCollection)
        {
            if (IsInvalidCollection(stringCollection)) throw new ArgumentException("String Collection cannot be null, contain nulls or empty strings");
            var formattedStrings = new List<string>();
            foreach (var item in stringCollection)
            {
                formattedStrings.Add(FormatString(item));
            }
            return formattedStrings;
        }
    }
}
