using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ServerInterfaces;

namespace ServerCore.Requests
{
    public class RequestParser
    {
        public static readonly Regex RequestLineRegex = new Regex(@"^(.*) (.*) HTTP\/1.1$");

        public Request Parse(string requestData)
        {
            var lines = requestData.Split(Environment.NewLine);
            var requestLine = lines.First();
            var match = RequestLineRegex.Match(requestLine);
            if (!match.Success)
            {
                throw new ApplicationException("Unable to process request");
            }
            var method = ParseHelper.ParseMethod(match.Groups[1].Value);
            if (method == Method.None)
            {
                throw new ApplicationException($"Unable to match {match.Groups[1].Value} to an available method");
            }

            var path = match.Groups[2].Value;
            // to-do: map headers, querystring, body, etc...

            return new Request
            {
                Method = method,
                Path = path
            };
        }
    }
}
