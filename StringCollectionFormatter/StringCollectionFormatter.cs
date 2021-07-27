using System;
using System.Collections.Generic;
using System.Text;

namespace StringsFormatter
{
    public class StringCollectionFormatter
    {

        private string _removeChars;
        private char _fromCurrencySymbol;
        private char _toCurrencySymbol;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="removeChars">characters to remove, defaults to "_4"</param>
        /// <param name="fromCurrencySymbol">currency symbol to be replaced defaults to '$'</param>
        /// <param name="toCurrencySymbol">currency symbol after replacement defaults to '£'</param>
        public StringCollectionFormatter(string removeChars = "_4", char fromCurrencySymbol = '$', char toCurrencySymbol = '£')
        {
            _removeChars = removeChars;
            _fromCurrencySymbol = fromCurrencySymbol;
            _toCurrencySymbol = toCurrencySymbol;
        }

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

            if (stringBuilder.Length == 0) throw new InvalidOperationException("Empty String returned");
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
            var formattedStrings = new List<string>();
            foreach (var item in stringCollection)
            {
                formattedStrings.Add(FormatString(item));
            }
            return formattedStrings;
        }
    }
}
