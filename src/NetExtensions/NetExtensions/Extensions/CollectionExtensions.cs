using System;
using System.Collections.Generic;
using System.Text;

namespace TheScripters.NetExtensions.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Creates delimited string from <see cref="System.Collections.Generic.Dictionary&lt;TKey, TValue&gt;"/>. Must be a primitive type.
        /// </summary>
        /// <typeparam name="TKey">Primitive Type (int, string)</typeparam>
        /// <typeparam name="TValue">Primitive Type (int, string)</typeparam>
        /// <param name="List">The <see cref="System.Collections.Generic.Dictionary&lt;TKey, TValue&gt;"/> object to create a string from</param>
        /// <param name="delimiter">The delimiter to use in the string</param>
        /// <param name="keyName">The key name in the first column of the delimited string</param>
        /// <param name="valueName">The value name in the second column of the delimited string</param>
        /// <returns>String of <c>delimiter</c> delimited text representing a <see cref="System.Data.DataTable"/> with <c>keyName</c> and <c>valueName</c> as columns</returns>
        public static string ToDelimitedFile<TKey, TValue>(this Dictionary<TKey, TValue> List, string delimiter, string keyName, string valueName)
        {
            Type key = typeof(TKey);
            Type value = typeof(TValue);
            if ((!value.IsPrimitive || !key.IsPrimitive) && (key.Name != "String" && value.Name != "String"))
                throw new ArgumentException("Parameter Dictionary must be of a primitive Dictionary<TKey, TValue> type.");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(keyName + delimiter + valueName);

            foreach (KeyValuePair<TKey, TValue> pair in List)
            {
                sb.AppendLine(pair.Key.ToString() + delimiter + pair.Value.ToString());
            }

            return sb.ToString();
        }
    }
}
