using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringsFormatter;
using System.Collections.Generic;

namespace StringProcessorTest
{
    [TestClass]
    public class StringProcessorTests
    {               
        [TestMethod]
        public void FormatItems_DuplicateStringCollection_ReturnsStringWithDuplicatesRemoved()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "AAAc91%cWwWkLq$1ci3_848v3d__K" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Contains("Ac91%cWwWkLq£1c"));
        }

        [TestMethod]
        public void FormatItems_CollectionWithSpecialCharacters_ReturnsStringWithSpecialCharactersRemoved()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "A_ABC4B" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Contains("AABCB"));
        }

        [TestMethod]
        public void FormatItems_CollectionWithDollarSign_ReturnsStringReplacedWithPoundSign()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "A_ABC4B$" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result[0] == "AABCB£");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void FormatItems_CollectionOnlySpecialCharacters_ThrowsInvalidOperationException()
        {           
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "_____44444" };
            var result = stringFormatter.FormatItems(testCollection);            
        }

      
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void FormatItems_NullCollection_ThrowsArgumentException()
        {
            var stringFormatter = new StringCollectionFormatter();
           var result = stringFormatter.FormatItems(null);
        }


        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void FormatItems_CollectionWithOneElementNull_ThrowsArgumentException()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "A_ABC4B$", null};            
            var result = stringFormatter.FormatItems(null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void FormatItems_CollectionWithOneElementEmpty_ThrowsInvalidOperationException()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "A_ABC4B$", string.Empty };
            stringFormatter.FormatItems(testCollection);         
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
        public void FormatItems_CollectionWithTwoValidItems_ReturnsTwoItems()
        {
            var stringFormatter = new StringCollectionFormatter();
            var testCollection = new List<string>() { "abcdefghijklmnopqrstuvxyz", "A_ABC4B$" };
            var result = stringFormatter.FormatItems(testCollection);
            Assert.IsTrue(result.Contains("AABCB£") && result.Contains("abcdefghijklmno"));
        }
    }
   
}
