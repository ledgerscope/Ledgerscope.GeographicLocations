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
	}
}
