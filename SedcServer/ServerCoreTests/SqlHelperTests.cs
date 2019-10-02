using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerPlugins.SqlServer;

namespace ServerCoreTests
{
    [TestClass]
    public class SqlHelperTests
    {
        [TestMethod]
        public void GenerateJsonData_WhenGivenEmptyDictionary_ReturnsParenthesis()
        {
            // 1. Arrange
            var input = new Dictionary<string, string>();
            var expected = "{}";
            // 2. Act
            var actual = SqlHelpers.GenerateJsonData(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenerateJsonData_WhenGivenDictionaryWithSingleEntry_ReturnsCorrectResult()
        {
            // 1. Arrange
            var input = new Dictionary<string, string>();
            input.Add("name", "wekoslav");
            var expected = "{\"name\": \"wekoslav\"}";
            // 2. Act
            var actual = SqlHelpers.GenerateJsonData(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GenerateJsonData_WhenGivenDictionaryWithThreeEntries_ReturnsCorrectResult()
        {
            // 1. Arrange
            var input = new Dictionary<string, string>();
            input.Add("firstName", "wekoslav");
            input.Add("lastName", "stefanovski");
            input.Add("age", "42");
            var expected = "{\"firstName\": \"wekoslav\",\"lastName\": \"stefanovski\",\"age\": \"42\"}";
            // 2. Act
            var actual = SqlHelpers.GenerateJsonData(input);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }
    }

}
