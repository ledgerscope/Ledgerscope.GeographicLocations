using System;
using System.Collections.Generic;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator.Extensions
{
	internal static class StringExtensions
	{
		public static string SurroundWithQuotes(this string str)
		{
			return "\"" + str + "\"";
		}
	}
}
