using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInterfaces
{
    public class HeaderCollection
    {
        private readonly Dictionary<string, string> data = new Dictionary<string, string>();

        public HeaderCollection(Dictionary<string, string> initialData)
        {
            data = initialData;
        }

        public static HeaderCollection Empty
        {
            get
            {
                return new HeaderCollection(new Dictionary<string, string>());
            }
        }

        public string GetCookie()
        {
            return data["Cookie"];
        }

        public void SetCookie(string cookie)
        {
            data["Cookie"] = cookie;
        }

        public string GetHeader(string header)
        {
            return data[header];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in data)
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }
            return sb.ToString();
        }
    }
}