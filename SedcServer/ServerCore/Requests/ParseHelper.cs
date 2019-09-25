using ServerInterfaces;

namespace ServerCore.Requests
{
    internal static class ParseHelper
    {
        public static Method ParseMethod(string method)
        {
            if (method == "GET")
            {
                return Method.Get;
            }
            if (method == "PUT")
            {
                return Method.Put;
            }
            if (method == "POST")
            {
                return Method.Post;
            }
            return Method.None;
        }
    }
}