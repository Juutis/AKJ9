using System;
namespace CustomExtensions
{
    public static class StringExtension
    {
        public static string Format(this string str, params Object[] args)
        {
            return string.Format(str, args);
        }
    }
}