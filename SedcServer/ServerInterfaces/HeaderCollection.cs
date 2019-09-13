using System.Collections.Generic;

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

    }
}