using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServerInterfaces
{
    public static class ContentTypes
    {
        public static string JpegImage = "image/jpeg";
        public static string JsonApplication = "application/json";
        public static string PlainText = "text/plain";
        public static string HtmlText = "text/html";
        public static string Anything = "application/octet-stream";

        #region Common Content Types
        private static Dictionary<string, string> CommonContentTypes = new Dictionary<string, string>
        {
            { ".jpg", JpegImage }, { ".jpeg", JpegImage }, { ".jpe", JpegImage },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".htm", HtmlText }, { ".html", HtmlText },
            { ".txt", PlainText }, { ".js", PlainText }
        };
        #endregion


        public static string GetContentType(string fullPath)
        {
            //var imageExts = new string[] { ".jpg", ".png", ".gif" };
            //var htmlExts = new string[] { ".htm", ".html" };
            //var isImage = imageExts.Contains(Path.GetExtension(fullPath));
            //var isHtml = htmlExts.Contains(Path.GetExtension(fullPath));

            //return isImage ? JpegImage : isHtml ? HtmlText : PlainText;

            var extension = Path.GetExtension(fullPath).ToLower();
            if (CommonContentTypes.ContainsKey(extension))
            {
                return CommonContentTypes[extension];
            }
            return Anything;
        }
    }

}

