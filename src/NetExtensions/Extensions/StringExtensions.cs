using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TheScripters.NetExtensions.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// <para>This method takes a string array of characters to replace in a specified string with a specified value.</para>
        /// <para>This method will replace all instances of all values in the array with <c>replacementValue</c>.</para>
        /// </summary>
        /// <example>
        /// <code language="CSharp" title="C#">
        /// string val = "abcdefghiabc";
        /// string[] sArr = { "a", "b", "c" };
        /// string newVal = "x";
        /// Console.Write(val.Replace(sArr, newVal);
        /// 
        /// // Returns: xxxdefghixxx
        /// </code>
        /// </example>
        /// <param name="value">string to process</param>
        /// <param name="symbols">Array of strings to find</param>
        /// <param name="replacementValue">Replacement value for all found strings</param>
        /// <returns>String with values replaced</returns>
        public static string Replace(this string value, string[] symbols, string replacementValue)
        {
            foreach (string c in symbols)
            {
                value = value.Replace(c, replacementValue);
            }
            return value;
        }

        /// <summary>
        /// <para>This method takes a string array of characters to replace in a specified string with a specified value</para>
        /// <para>This method replaces <c>symbols[i]</c> with <c>replacementValue[i]</c></para>
        /// </summary>
        /// <example>
        /// <code language="CSharp" title="C#">
        /// string val = "abcdefghiabc";
        /// string[] sArr = { "a", "b", "c" };
        /// string[] newVal = { "x", "y", "z" };
        /// Console.Write(val.Replace(sArr, newVal);
        /// 
        /// // Replaces "a" with "x", "b" with "y", and "c" with "z"
        /// // Returns: xyzdefghixyz
        /// </code>
        /// </example>
        /// <param name="value">string to process</param>
        /// <param name="symbols">Array of strings to find</param>
        /// <param name="replacementValue">Array of strings to be replaced</param>
        /// <returns>String with values replaced</returns>
        public static string Replace(this string value, string[] symbols, string[] replacementValue)
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                value = value.Replace(symbols[i], replacementValue[i]);
            }
            return value;
        }

        /// <summary>
        /// <para>This method takes a string array of characters to replace in a specified string with a specified value</para>
        /// <para>This method replaces <c>symbols[i]</c> with <c>replacementValue[i]</c></para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbols"></param>
        /// <param name="replacementValue"></param>
        /// <returns></returns>
        public static StringBuilder Replace(this StringBuilder value, string[] symbols, string[] replacementValue)
        {
            if (value == null)
                return value;

            for (int i = 0; i < symbols.Length; i++)
            {
                value = value.Replace(symbols[i], replacementValue[i]);
            }
            return value;
        }

        /// <summary>
        /// Removes punctuation from a given string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static StringBuilder StripPunctuation(this StringBuilder s)
        {
            if (s == null)
                return s;

            var sb = new StringBuilder();
            foreach (char c in s.ToString())
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb;
        }

        /// <summary>
        /// Removes punctuation from a given string
        /// </summary>
        /// <param name="s">String value to process</param>
        /// <returns>String with punctuation removed</returns>
        public static string StripPunctuation(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Trim all string values in a string collection
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IEnumerable<string> Trim(this IEnumerable<string> val)
        {
            List<string> l = val.ToList();
            for (int i = 0; i < l.Count; i++)
                l[i] = l[i].Trim();

            return l;
        }

        /// <summary>
        /// Encode string to base64
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToBase64(this string val)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(val));
        }

        /// <summary>
        /// Decode string from base64
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string FromBase64(this string val)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(val));
        }

        /// <summary>
        /// Changes <see cref="System.Environment.NewLine"/> to the HTML &lt;br /&gt; line break
        /// </summary>
        /// <param name="s">The string to process</param>
        /// <returns>String with line endings converted to HTML &lt;br /&gt; tags</returns>
        public static string Nl2Br(this string s)
        {
            return s.Replace(System.Environment.NewLine, "<br />"); //.Replace("\n", "<br />").Replace("\r", "<br />");
        }

        /// <summary>
        /// Create a <see cref="System.IO.Stream"/> object from a <see cref="System.String"/>
        /// </summary>
        /// <param name="str">String from which to create the Stream</param>
        /// <returns><see cref="System.IO.Stream"/> which represents the given <see cref="System.String"/></returns>
        public static Stream ToStream(this string str)
        {
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(str);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }
        }
    }
}