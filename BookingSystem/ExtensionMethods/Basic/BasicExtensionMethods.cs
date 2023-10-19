namespace System
{
    public static class BasicExtensionMethods
    {

        public static bool IsBase64String(this string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

    }
}
