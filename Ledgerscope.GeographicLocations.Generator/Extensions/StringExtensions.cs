using System;
using System.Collections.Generic;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator.Extensions
{
	public static partial class StringExtensions
	{
		public static string SurroundWithQuotes(this string str)
		{
			return "\"" + str + "\"";
		}

		public static StringBuilder AppendWithQuotes(this StringBuilder sb, string txt)
		{
			if (txt.IndexOf('"') >= 0)
				throw new ArgumentException("The text cannot contain double quotes.", nameof(txt));

			return sb.Append('"').Append(txt).Append('"');
		}
	}
}
