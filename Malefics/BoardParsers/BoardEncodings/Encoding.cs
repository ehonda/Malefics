namespace Malefics.BoardParsers.BoardEncodings
{
    public static class Encoding
    {
        public static class Ascii
        {
            public static AsciiEncoding Standard { get; }
                = new('\n', '.');
        }
    }
}
