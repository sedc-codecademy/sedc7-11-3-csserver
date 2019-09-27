using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerCore.Requests;
using ServerInterfaces;

namespace ServerCoreTests
{
    [TestClass]
    public class ContentTypesTests
    {
        [TestMethod]
        public void ContentTypes_GetContentType_ReturnsExplicitValue()
        {
            // 1. Arrange
            string fullPath = @"C:\Folder\image.png";
            string expected = "image/png";
            // 2. Act
            string actual = ContentTypes.GetContentType(fullPath);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContentTypes_GetContentType_ReturnsEnumeratedValue()
        {
            // 1. Arrange
            string fullPath = @"C:\Folder\image.jpg";
            string expected = "image/jpeg";
            // 2. Act
            string actual = ContentTypes.GetContentType(fullPath);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
