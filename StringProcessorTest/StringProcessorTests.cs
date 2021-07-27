using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringsFormatter;
using System.Collections.Generic;

namespace StringProcessorTest
{
    [TestClass]
    public class StringProcessorTests
    {               
        [TestMethod]
        public void FormatItems_DuplicateStringCollection_RemovesDuplicates()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "AAABCB" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Contains( "ABCB"));
        }

        [TestMethod]
        public void FormatItems_CollectionWithSpecialCharacters_RemovesSpecialCharacters()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "A_ABC4B" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Contains("AABCB"));
        }

        [TestMethod]
        public void FormatItems_CollectionWithDollar_ReplaceswithPoundSign()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "A_ABC4B$" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result[0] == "AABCB£");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void FormatItems_CollectionWithSpecialCharacter_ThrowsException()
        {           
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "_____44444" };
            var result = stringFormatter.FormatItems(testCollection);            
        }


        [TestMethod]
        public void FormatItems_CollectionWithStringLongerThan15Chars_ReturnsTruncatedString()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "abcdefghijklmnopqrstuvxyz" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result[0]=="abcdefghijklmno");
        }


        [TestMethod]
        public void FormatItems_CollectionWithOneItem_ReturnsOneItem()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "abcdefghijklmnopqrstuvxyz" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Count==1);
        }

        [TestMethod]
        public void FormatItems_CollectionWithTwoItems_ReturnsTwoItems()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "abcdefghijklmnopqrstuvxyz", "A_ABC4B$" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Contains("AABCB£") && result.Contains("abcdefghijklmno"));
        }
    }
   
}
