using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerCore.Requests;
using ServerInterfaces;

namespace ServerCoreTests
{
    [TestClass]
    public class RequestParserTests
    {
        [TestMethod]
        public void GetRequest_ShouldReturnRequest_WithMethodGet()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            Method expectedMethod = Method.Get;
            string expectedPath = "jen/dva";
            string expectedQuery = "param=value";
            string expectedBody = string.Empty;
            string expectedFetchMode = "navigate";
            string requestString = @"GET /jen/dva?param=value HTTP/1.1
Host: localhost:13000
Connection: keep-alive
Cache-Control: max-age=0
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36
Sec-Fetch-Mode: navigate
Sec-Fetch-User: ?1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3
Sec-Fetch-Site: none
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9,mk;q=0.8,sr;q=0.7,hr;q=0.6,bs;q=0.5
Cookie: _ga=GA1.1.405632844.1539592429; x-access-token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiZGpvdmFub3ZAdW5kZXJ0b25lLmNvbSIsImlkIjoxMDAwMDAwMDAwLCJlbWFpbCI6ImRqb3Zhbm92QHVuZGVydG9uZS5jb20iLCJpYXQiOjE1NjYyMTMzMjMsImV4cCI6MTU2NjI5OTcyM30.xt_HboZPPzp_QIQfBaWKNmZC_7zBAS2mZiIAfm0pImE; uid=1000000000
";
            // 2. Act
            Request actual = parser.Parse(requestString);
            // 3. Assert
            Assert.AreEqual(expectedMethod, actual.Method);
            Assert.AreEqual(expectedPath, actual.Path);
            Assert.AreEqual(expectedQuery, actual.Query);
            Assert.AreEqual(expectedBody, actual.Body);
            Assert.AreEqual(expectedFetchMode, actual.Headers.GetHeader("Sec-Fetch-Mode"));
        }

        [TestMethod]
        public void PutRequest_ShouldReturnRequest_WithMethodPut()
        {
            // 1. Arrange
            RequestParser parser = new RequestParser();
            Method expectedMethod = Method.Put;
            string expectedPath = "sadsadsa/sadsads/sadsad/asdsa";
            string expectedQuery = string.Empty;
            string expectedBody = @"{
        ""firstName"": ""Wekoslav"",
        ""lastName"": ""Stefanovski""
}";
            string expectedUserAgent = "PostmanRuntime/7.16.3";
            string requestString = @"PUT /sadsadsa/sadsads/sadsad/asdsa HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Indla29zbGF2IiwiQ291bnRyeSI6Ik1hY2Vkb25pYSIsIm5iZiI6MTU2NzQ1Mzg2MSwiZXhwIjoxNTY3NDU3NDYxLCJpYXQiOjE1Njc0NTM4NjF9.YkAwmtzYGPMWUK1x5deRIu3mEcoRktDLOiZ3U3LVToM
Content-Type: application/json
User-Agent: PostmanRuntime/7.16.3
Accept: */*
Cache-Control: no-cache
Postman-Token: 4c085ee0-a559-486e-a9c6-406823700d7a
Host: localhost:13000
Accept-Encoding: gzip, deflate
Content-Length: 56
Connection: keep-alive

{
        ""firstName"": ""Wekoslav"",
        ""lastName"": ""Stefanovski""
}";
            // 2. Act
            Request actual = parser.Parse(requestString);
            // 3. Assert
            Assert.AreEqual(expectedMethod, actual.Method);
            Assert.AreEqual(expectedPath, actual.Path);
            Assert.AreEqual(expectedQuery, actual.Query);
            Assert.AreEqual(expectedBody, actual.Body);
            Assert.AreEqual(expectedUserAgent, actual.Headers.GetHeader("User-Agent"));
        }

    }
}
