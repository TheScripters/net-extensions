using System;
using System.Data;

namespace TheScripters.NetExtensions
{
	public class NoRecordDataException : DataException
	{
		public NoRecordDataException()
		{
		}
		public NoRecordDataException(string message) : base(message)
		{
		}
	}
}
