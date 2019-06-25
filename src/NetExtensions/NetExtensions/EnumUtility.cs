using System;
using System.Collections.Generic;
using System.Linq;

namespace TheScripters.NetExtensions
{
    /// <summary>
    /// Static Class for handling Enumerations
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// Method for getting the values of an enumeration
        /// </summary>
        /// <typeparam name="T">The enumeration type to get values from</typeparam>
        /// <example>
        /// <code language="CSharp" Title="C#">
        /// public enum Suit
        /// {
        ///     Spade,
        ///     Heart,
        ///     Diamond,
        ///     Club
        /// }
        /// var eValues = EnumUtility.GetValues&lt;Suit&gt;();
        /// foreach (Suit s in eValues)
        /// {
        ///     Console.WriteLine(s.ToString());
        /// }
        /// </code>
        /// </example>
        /// <returns><see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> object of items</returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
