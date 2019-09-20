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
        public static readonly Regex RequestLineRegex = new Regex(@"^([A-Z]+)\s\/([^\s?]*)\??([^\s]*)\sHTTP\/1\.1$");
        public static readonly Regex HeaderRegex = new Regex(@"^([^:]*):\s*(.*)$");

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
            var query = new QueryParamCollection(match.Groups[3].Value);
            // to-do: map headers, querystring, body, etc...

            var headerLines = lines.Skip(1).TakeWhile(line => !string.IsNullOrEmpty(line));
            var headerDict = new Dictionary<string, string>();

            foreach (var line in headerLines)
            {
                var hmatch = HeaderRegex.Match(line);
                if (!hmatch.Success)
                {
                    throw new ApplicationException($"Unable to process header line {line}");
                }
                var headerName = hmatch.Groups[1].Value;
                var headerValue = hmatch.Groups[2].Value;
                headerDict.Add(headerName, headerValue);
            }

            HeaderCollection headers = new HeaderCollection(headerDict);

            var bodyLines = lines.SkipWhile(line => !string.IsNullOrEmpty(line)).Skip(1);
            var body = string.Join(Environment.NewLine, bodyLines);

            return new Request
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Body = body
            };
        }
    }
}
