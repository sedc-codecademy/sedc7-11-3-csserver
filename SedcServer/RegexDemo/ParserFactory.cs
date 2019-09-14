using System;

namespace RegexDemo
{
    internal class ParserFactory
    {
        internal static IPointParser GetParser(string algorithm)
        {
            if (algorithm == "substring")
            {
                return new SubstringPointParser();
            }
            if (algorithm == "regex")
            {
                return new RegexPointParser();
            }
            // or we could have a default algorithm?
            throw new ApplicationException($"'{algorithm}' is not a known algorithm name");
        }
    }
}