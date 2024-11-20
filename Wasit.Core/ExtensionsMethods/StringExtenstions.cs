using System.Text.RegularExpressions;
using Wasit.Core.Helpers.Security;

namespace Wasit.Core.ExtensionsMethods
{
    public static partial class ExtensionMethods
    {
        public static string ToUniformedPath(this string path)
        {
            return path.Replace("\\", "/");
        }

        public static string SplitPascal(this string str)
        {
            Regex Reg = new Regex("([a-z,0-9](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", RegexOptions.Compiled);
            return Reg.Replace(str, "$1 ");
        }

        public static string Encrypt(this string text)
        {
            return CryptographyHelper.Encrypt(text);
        }

        public static string Decrypt(this string cipherText)
        {
            return CryptographyHelper.Decrypt(cipherText);
        }

        public static long DecryptToNumber(this string cipherText)
        {
            return long.Parse(CryptographyHelper.Decrypt(cipherText));
        }



        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return Enum.TryParse(value, true, out T result) ? result : defaultValue;
        }
    }
}
