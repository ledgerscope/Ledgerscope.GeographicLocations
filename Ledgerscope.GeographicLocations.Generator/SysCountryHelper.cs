using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
			string retval = new string('\0', 256);
			int size = NativeMethods.GetGeoInfoEx(twoLetterCode, geoType, retval, retval.Length);
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
