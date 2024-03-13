using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ledgerscope.GeographicLocations.Generator
{
	public static partial class SysCountryHelper
	{
		public static IReadOnlyList<SysCountry> GetAllCountries()
		{
			IReadOnlyList<string> twoLetterCodes = getAllTwoLetterCodes();

			var countries = new List<SysCountry>();

			foreach (string twoLetterCode in twoLetterCodes)
			{
				string friendlyName = getVal(twoLetterCode, NativeMethods.SYSGEOTYPE.GEO_FRIENDLYNAME);
				string threeLetterCode = getVal(twoLetterCode, NativeMethods.SYSGEOTYPE.GEO_ISO3);
				string currencyCode = getVal(twoLetterCode, NativeMethods.SYSGEOTYPE.GEO_CURRENCYCODE);
				string currencySymbol = getVal(twoLetterCode, NativeMethods.SYSGEOTYPE.GEO_CURRENCYSYMBOL);

				var country = new SysCountry(friendlyName, twoLetterCode, threeLetterCode, currencyCode, currencySymbol);

				country = getFixedCountry(country);

				countries.Add(country);
			}

			return countries.OrderBy(c => c.Name).ToList();
		}

		private static SysCountry getFixedCountry(SysCountry country)
		{
			if (country.Name == "Croatia")
			{
				if (country.CurrencyCode == "EUR")
				{
					// Hurrah, Windows has caught up at last.
					return country;
				}
				else
				{
					return new SysCountry(country.Name, country.TwoLetterCode, country.ThreeLetterCode, "EUR", "€");
				}
			}
			else
			{
				return country;
			}
		}

		private static string getVal(string twoLetterCode, NativeMethods.SYSGEOTYPE geoType)
		{
			/* All to do with working with C ways of doing strings.
			 * We allocate a char array (string) of appropriate size.
			 * We tell GetGeoInfoEx where it is and how long it is, and invite it to write to it.
			 * GetGeoInfoEx writes the name to the array, plus a null terminator, so our string is now like
			 * "São Tomé and Príncipe\0       " (but more spaces than that at the end, to take it up to 256).
			 * GetGeoInfoEx returns the number of characters written including the null terminator,
			 * so we take the first n-1 characters and return that. 
			 * Note that we are talking unicode here, so number of characters may not be same as number of bytes. 
			 * e.g. "Curacao" and "Curaçao" are both 7 characters long, but 
			 * with UTF8 the former takes 7 bytes whereas the latter is 8 bytes.
			 * Windows documentation of GetGeoInfoEx is scarce, but I have treated the output string as UTF8
			 * and that seems to work, including for "São Tomé and Príncipe" etc. */

			string retval = new string(' ', 256);

			int size = NativeMethods.GetGeoInfoEx(
				twoLetterCode, // The country we want info about
				geoType,       // The type of info we want
				retval,        // The allocated char array to write to
				retval.Length  // The size of the char array, in characters
			);

			string val = retval.Substring(0, size - 1);
			return val;
		}

		private static IReadOnlyList<string> getAllTwoLetterCodes()
		{
			var twoLetterCodes = new List<string>();

			int langId = CultureInfo.InvariantCulture.LCID;
			NativeMethods.EnumGeoNameProc callback = enumGeoNameCallback;

			NativeMethods.EnumSystemGeoNames(NativeMethods.GEOCLASS_NATION, callback);

			bool enumGeoNameCallback(string val)
			{
				if (!string.IsNullOrEmpty(val))
				{
					twoLetterCodes.Add(val);
					return true;
				}
				else
				{
					return false;
				}
			}

			return twoLetterCodes;
		}
	}
}
