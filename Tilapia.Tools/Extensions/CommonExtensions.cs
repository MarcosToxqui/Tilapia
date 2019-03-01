namespace Tilapia.Tools.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides a set extension functions.
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// Evaluates if the string is null o empty.
        /// </summary>
        /// <param name="string">String to evaluate.</param>
        /// <returns>True if null or empty, false in case contrary.</returns>
        public static bool IsNullOrEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }

        /// <summary>
        /// Evaluates if an object List derived from IList is empty (zero elements) or null.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="listObject">Object list to evaluated.</param>
        /// <returns>True if empty (oject exists but don't have elements) or null, false in case contrary.</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> listObject)
        {
            if (listObject == null)
            {
                return true;
            }

            if (!listObject.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Evaluates if an Enumerable object is null or empty.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="enumerableObject">Object enumerable to evaluated.</param>
        /// <returns>True if empty (oject exists but don't have elements) or null, false in case contrary.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerableObject)
        {
            if (enumerableObject == null)
            {
                return true;
            }

            if (!enumerableObject.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Evaluates if an Array object is null or empty.
        /// </summary>
        /// <param name="array">Object to evaluate</param>
        /// <returns>True if empty (oject exists but don't have elements) or null, false in case contrary.</returns>
        public static bool IsNullOrEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Evaluates if an common array object is null or empty.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="array">Object to evaluate</param>
        /// <returns>True if empty (oject exists but don't have elements) or null, false in case contrary.</returns>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Evaluates if an Dictionary object is null or empty.
        /// </summary>
        /// <typeparam name="T">Generic type 1</typeparam>
        /// <typeparam name="U">Generic type 2</typeparam>
        /// <param name="dictionary">Object to evaluate</param>
        /// <returns>True if empty (oject exists but don't have elements) or null, false in case contrary.</returns>
        public static bool IsNullOrEmpty<T, U>(this IDictionary<T, U> dictionary)
        {
            return dictionary == null || dictionary.Count < 1;
        }

        /// <summary>
        /// Evaluates if the object is null.
        /// </summary>
        /// <param name="value">Object to evaluate.</param>
        /// <returns>True if object is null.</returns>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// Evaluates if the object is not null.
        /// </summary>
        /// <param name="value">Object to evaluate</param>
        /// <returns>Returns false if object is null.</returns>
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        /// <summary>
        /// Concatenate the items in an array in a single string.
        /// </summary>
        /// <param name="value">String array to concatenate.</param>
        /// <param name="separator">String used to concatenate the items.</param>
        /// <returns>String composed by the items in array.</returns>
        public static string Join(this string[] value, string separator)
        {
            return string.Join(separator, value);
        }

        /// <summary>
        /// Concatenate the items in an array in a single string.
        /// </summary>
        /// <param name="value">String array to concatenate.</param>
        /// <param name="separator">Character used to concatenate the items.</param>
        /// <returns>String composed by the items in array.</returns>
        public static string Join(this string[] value, char separator)
        {
            string innerSeparator = separator.ToString();
            return string.Join(innerSeparator, value);
        }

        /// <summary>
        /// Concatenate the items in a <see cref="List{T}"/> collection.
        /// </summary>
        /// <param name="value">List of string to concatenate.</param>
        /// <param name="separator">String used to concatenate the items.</param>
        /// <returns>String composed by the items in List.</returns>
        public static string Join(this List<string> value, string separator)
        {
            return string.Join(separator, value);
        }

        /// <summary>
        /// Concatenate the items in a <see cref="IEnumerable{T}"/> collection.
        /// </summary>
        /// <param name="value">String array to concatenate.</param>
        /// <param name="separator">Character used to concatenate the items.</param>
        /// <returns>String composed by the items in array.</returns>
        public static string Join(this IEnumerable<string> value, string separator)
        {
            return string.Join(separator, value);
        }

        /// <summary>
        /// Converts a string to bytes.
        /// </summary>
        /// <param name="value">String to convert.</param>
        /// <returns>Byte array.</returns>
        public static byte[] GetBytes(this string value)
        {
            byte[] result = { };

            try
            {
                result = Encoding.ASCII.GetBytes(value);
            }
            catch
            {
                return result;
            }

            return result;
        }

        /// <summary>
        /// Converts an bytes array to comprensible string.
        /// </summary>
        /// <param name="value"><see cref="byte"/> array to convert.</param>
        /// <returns>Legible string.</returns>
        public static string GetStringForm(this byte[] value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            string result = string.Empty;

            try
            {
                result = Encoding.ASCII.GetString(value);
            }
            catch
            {
                return string.Empty;
            }

            return result;
        }

        /// <summary>
        /// Converts the current string to base64.
        /// </summary>
        /// <param name="value">String to converts.</param>
        /// <returns>Base64 string from initial string.</returns>
        public static string ToBase64(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var plainTextBytes = value.GetBytes();

            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Stringify a string based base64 format.
        /// </summary>
        /// <param name="value">String to convert.</param>
        /// <returns>Final string.</returns>
        public static string CastFromBase64(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var base64EncodedBytes = Convert.FromBase64String(value);
            return base64EncodedBytes.GetStringForm();
        }

        /// <summary>
        /// Returns a new <see cref="IList{T}"/> collection without null items based on
        /// original collection.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="value">Object to evaluate.</param>
        /// <returns><see cref="IList{T}"/> collection.</returns>
        public static IList<T> RemoveNulls<T>(this IList<T> value)
        {
            if (value.IsNotNull())
            {
                return value.Where(v => v.IsNotNull()).ToList();
            }

            return new List<T>();
        }

        /// <summary>
        /// Returns a new <see cref="IEnumerable{T}"/> collection without null items based on
        /// original collection.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="value">Object to evaluate.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection.</returns>
        public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> value)
        {
            if (value.IsNotNull())
            {
                return value.Where(v => v != null).ToList();
            }

            return new List<T>().AsEnumerable();
        }

        /// <summary>
        /// Find repeated blank spaces within the string and eliminate them.
        /// </summary>
        /// <param name="value">String to clean.</param>
        /// <returns>Cleaned string.</returns>
        public static string RemoveRedundantWhiteSpaces(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);

            value = regex.Replace(value, " ");

            return value;
        }

        /// <summary>
        /// Capitalize the first letter each word inside sentence/string.
        /// </summary>
        /// <param name="value">String to capitalize.</param>
        /// <returns>Capitalized string.</returns>
        public static string Capitalize(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            value = value.First().ToString().ToUpper() + value.Substring(1);

            return value;
        }

        /// <summary>
        /// Remove the numbers in the string.
        /// </summary>
        /// <param name="value">String evaluated.</param>
        /// <returns>String cleaned.</returns>
        public static string RemoveNumbers(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            value = Regex.Replace(value, @"[\d-]", string.Empty);

            return value;
        }

        /// <summary>
        /// Evaluate if the current string is a valid email address.
        /// </summary>
        /// <param name="value">String evaluated.</param>
        /// <returns>Returns true if the string is valid email address.</returns>
        public static bool IsValidMailAddress(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            return Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Evaluates if string is a valid URL.
        /// </summary>
        /// <param name="value">String evaluated.</param>
        /// <returns>Returns true if the string is valid URL.</returns>
        public static bool IsValidUrl(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            Uri uriResult;

            bool result = Uri.TryCreate(value, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }
    }
}
