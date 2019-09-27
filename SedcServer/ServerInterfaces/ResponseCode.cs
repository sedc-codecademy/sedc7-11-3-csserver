using System;

namespace ServerInterfaces
{
    public enum ResponseCode
    {
        Ok = 200,
        BadRequest = 400,
        NotFound = 404
    }

    public static class ResponseCodeHelper
    {
        public static string GetMessage(this ResponseCode responseCode)
        {
            switch (responseCode)
            {
                case ResponseCode.Ok:
                    return "Ok";
                case ResponseCode.NotFound:
                    return "Not Found";
                //case ResponseCode.BadRequest:
                //    return "Bad Request";
                default:
                    return string.Empty;
                    throw new ApplicationException($"Response code {responseCode} is not valid");
            }
        }

        public static int GetValue(this ResponseCode responseCode)
        {
            return (int)responseCode;
        }
    }
}