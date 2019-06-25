using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TheScripters.NetExtensions.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Copy a <see cref="System.Data.DataTable"/> to a delimited string
        /// </summary>
        /// <param name="table">The DataTable from which the delimited string will be created</param>
        /// <param name="delimiter">The delimiter used to separate columns in the output string (comma (<c>,</c>) or tab (<c>\t</c>) are most often used)</param>
        /// <returns>String of <c>delimiter</c> delimited text representing the given <see cref="System.Data.DataTable"/></returns>
        public static string ToDelimitedFile(this DataTable table, string delimiter)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? Environment.NewLine : delimiter);
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? Environment.NewLine : delimiter);
                }
            }

            if (result.ToString().EndsWith(delimiter, StringComparison.CurrentCulture))
                return result.ToString().Substring(0, result.ToString().Length - delimiter.Length);
            else return result.ToString();
        }

        /// <summary>
        /// Copies <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> to DataTable. Must be a primitive data type such as int or string.
        /// </summary>
        /// <typeparam name="T">Primitive Type (int, decimal) or string</typeparam>
        /// <param name="Val"><see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> to copy</param>
        /// <returns><see cref="System.Data.DataTable"/> with a single column consisting of the <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/> values</returns>
        public static DataTable ToTable<T>(this IEnumerable<T> Val)
        {
            DataTable t = new DataTable();
            t.Columns.Add(new DataColumn("Id", typeof(T)));
            if (Val.Count() > 0)
            {
                Type type = Val.ToList().GetType().GetGenericArguments()[0];
                if (!type.IsPrimitive && type.FullName != "System.String")
                    throw new ArgumentException("Parameter Val must be of a primitive IEnumerable<T> type.");
            }
            else return t;
            foreach (T val in Val)
            {
                DataRow r = t.NewRow();
                r["Id"] = val;
                t.Rows.Add(r);
            }
            if (t.Rows.Count == 0)
            {
                DataRow r = t.NewRow();
                r["Id"] = DBNull.Value;
                t.Rows.Add(r);
            }
            return t;
        }

        /// <summary>
        /// Copies a <see cref="System.Collections.Generic.Dictionary&lt;TKey, TValue&gt;"/> to a <see cref="System.Data.DataTable"/>. Must be a primitive data type such as int or string.
        /// </summary>
        /// <typeparam name="TKey">Primitive Type (int, string)</typeparam>
        /// <typeparam name="TValue">Primitive Type (int, string)</typeparam>
        /// <param name="Dictionary">The <see cref="System.Collections.Generic.Dictionary&lt;TKey, TValue&gt;"/> object to copy</param>
        /// <param name="keyName">Name of the column which will contain the <c>TKey</c> value</param>
        /// <param name="valueName">Name of the column which will contain the <c>TValue</c> value</param>
        /// <returns><see cref="System.Data.DataTable"/> containing two columns representing the <see cref="System.Collections.Generic.Dictionary&lt;TKey, TValue&gt;"/> object</returns>
        public static DataTable ToTable<TKey, TValue>(this Dictionary<TKey, TValue> Dictionary, string keyName, string valueName)
        {
            TKey key = (TKey)new Object();
            TValue value = (TValue)new Object();
            if (!value.GetType().IsPrimitive || !key.GetType().IsPrimitive)
                throw new ArgumentException("Parameter Dictionary must be of a primitive Dictionary<TKey, TValue> type.");
            DataTable t = new DataTable();
            t.Columns.Add(new DataColumn(keyName, typeof(TKey)));
            t.Columns.Add(new DataColumn(valueName, typeof(TValue)));

            foreach (KeyValuePair<TKey, TValue> v in Dictionary)
            {
                DataRow r = t.NewRow();
                r[keyName] = v.Key;
                r[valueName] = v.Value;
                t.Rows.Add(r);
            }

            return t;
        }

        /// <summary>
        /// Creates a copy of a DataTable object filtered by column and value
        /// </summary>
        /// <param name="source">The source DataTable to filter</param>
        /// <param name="fieldName">Column name to filter by</param>
        /// <param name="value">Column value to filter by</param>
        /// <returns>Filtered copy of <c>source</c> DataTable</returns>
		public static DataTable CloneAndFilter(this DataTable source, string fieldName, object value)
        {
            DataTable dataTable = source.Clone();
            foreach (DataRow dataRow in source.Rows)
            {
                if (dataRow[fieldName].Equals(value))
                {
                    dataTable.ImportRow(dataRow);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Creates a copy of a DataTable object filtered by column and value
        /// </summary>
        /// <param name="source">The source DataTable to filter</param>
        /// <param name="fieldName1">Column name to filter by</param>
        /// <param name="value1">Column value to filter by</param>
        /// <param name="fieldName2">Column name to filter by</param>
        /// <param name="value2">Column value to filter by</param>
        /// <returns>Filtered copy of <c>source</c> DataTable</returns>
		public static DataTable CloneAndFilter(this DataTable source, string fieldName1, object value1, string fieldName2, object value2)
        {
            DataTable dataTable = source.Clone();
            foreach (DataRow dataRow in source.Rows)
            {
                if (dataRow[fieldName1].Equals(value1) && dataRow[fieldName2].Equals(value2))
                {
                    dataTable.ImportRow(dataRow);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// <para>Creates a copy of a DataTable object filtered by column and value and returns the matched row.</para>
        /// <para>Special note: This method will return the first matched row only. To return multiple rows, use the <c>CloneAndFilter</c> method</para>
        /// </summary>
        /// <param name="source">The source DataTable to filter</param>
        /// <param name="fieldName">Column name to filter by</param>
        /// <param name="value">Column value to filter by</param>
        /// <returns>First matched <c>DataRow</c></returns>
		public static DataRow CloneAndGetRow(this DataTable source, string fieldName, object value)
        {
            DataTable dataTable = source.Clone();
            foreach (DataRow dataRow in source.Rows)
            {
                if (dataRow[fieldName].Equals(value))
                {
                    dataTable.ImportRow(dataRow);
                    return dataTable.Rows[0];
                }
            }
            throw new NoRecordDataException();
        }
    }
}
