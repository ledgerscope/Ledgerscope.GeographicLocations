using Ledgerscope.GeographicLocations.Generator.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator
{
	public static partial class WindowsCountryHelper
	{
		public static string GetCountryListConstruction()
		{
			var countries = GetAllCountries();

			var s = new StringBuilder();
			foreach (var country in countries)
			{
				s.Append("yield return new WindowsCountry(");
				s.Append(country.GeoId).Append(", ");
				s.AppendWithQuotes(country.CountryName).Append(", ");
				s.AppendWithQuotes(country.CountryTwoLetterCode).Append(", ");
				s.AppendWithQuotes(country.CountryThreeLetterCode).Append(", ");
				s.AppendWithQuotes(country.CurrencyName).Append(", ");
				s.AppendWithQuotes(country.CurrencyCode).Append(", ");
				s.AppendWithQuotes(country.CurrencySymbol);
				s.AppendLine(");");
			}

			return s.ToString();
		}

		private static readonly HashSet<string> _excludedRegionNames = new HashSet<string>
		{
			"Caribbean",
		};

		public static IReadOnlyList<WindowsCountry> GetAllCountries()
		{
			var regions = new List<RegionInfo>();
			var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
				.Where(c => !c.Equals(CultureInfo.InvariantCulture))
				.Where(c => !c.IsNeutralCulture)
				.Where(c => !c.CultureTypes.HasFlag(CultureTypes.UserCustomCulture))
				.ToList();

			foreach (var culture in cultures)
			{
				var region = new RegionInfo(culture.LCID);
				// Console.WriteLine(region.EnglishName);

				if (string.IsNullOrWhiteSpace(region.ThreeLetterISORegionName))
				{
					// These are things like 'World' and 'Latin America'.
					// Debugger.Break();
				}
				else if (_excludedRegionNames.Contains(region.EnglishName))
				{
					// Skip these.
				}
				else
				{
					regions.Add(region);
				}
			}

			regions.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));

			var countries = new Dictionary<string, WindowsCountry>();
			foreach (var region in regions)
			{
				var country = new WindowsCountry(
					region.GeoId,
					region.EnglishName,
					region.TwoLetterISORegionName,
					region.ThreeLetterISORegionName,
					region.CurrencyEnglishName,
					region.ISOCurrencySymbol,
					region.CurrencySymbol
				);

				country = getFixedCountry(country);

				if (countries.TryGetValue(country.CountryTwoLetterCode, out var existing))
				{
					if (areSame(country, existing, true, out string difference))
					{
						// that's fine
					}
					else if (areSame(country, existing, false, out string diff))
					{
						// It's just the currency symbol that's different.
						string bestSymbol = getBestSymbol(country.CurrencySymbol, existing.CurrencySymbol);
						if (existing.CurrencySymbol != bestSymbol)
						{
							countries[country.CountryTwoLetterCode] = getCopyWithCurrencyChange(country, currencySymbol: bestSymbol);
						}
					}
					else
					{
						throw new ApplicationException($"Country discrepancy for {country.CountryName} ({country.CurrencyCode}): {difference}");
					}
				}
				else
				{
					countries.Add(country.CountryTwoLetterCode, country);
				}
			}

			return countries.Values.ToList();
		}

		private static string getBestSymbol(string a, string b)
		{
			// Many countries arrive to us via two or more culture routes, and one may say the currency 
			// is 'CA$' and the other says it is '$', etc.  So we say that the shortest is best.
			// We do it on the byte length, not the character length, so that the more exotically unicodey it is, 
			// the less likely we are to choose it.

			// If you disagree with the outcome, you are welcome to add it as
			// a special case in the getFixedCountry method.

			int lenA = Encoding.UTF8.GetBytes(a).Length;
			var lenB = Encoding.UTF8.GetBytes(b).Length;

			return lenA <= lenB ? a : b;
		}

		private static WindowsCountry getFixedCountry(WindowsCountry country)
		{
			if (country.CountryTwoLetterCode == "BA")
			{
				// Inconsistency with '&' and 'and' in country name.
				const string expectedCountryName = "Bosnia and Herzegovina";
				if (country.CountryName != expectedCountryName)
				{
					return getCopyWithCountryNameChange(country, expectedCountryName);
				}
				else
				{
					return country;
				}
			}
			else if (country.CountryName == "Croatia")
			{
				if (country.CurrencyCode == "EUR")
				{
					// Hurrah, Windows has caught up at last.
					return country;
				}
				else if (country.CurrencyCode == "HRK")
				{
					// As of March 2024, Windows has not caught up with Jan 2023 joining of Euro.
					return getCopyWithCurrencyChange(country, currencyName: "Euro", currencyCode: "EUR", currencySymbol: "€");
				}
				else
				{
					throw new ApplicationException($"For country {country}, we expected the currency code to be 'EUR' or 'HRK' but it is '{country.CurrencyCode}'");
				}
			}
			else
			{
				return country;
			}
		}

		private static bool areSame(WindowsCountry a, WindowsCountry b, bool includeCurrencySymbol, out string difference)
		{
			var s = new StringBuilder();

			if (a.GeoId != b.GeoId)
				appendWithPrecedingComma(s, $"GeoId: {a.GeoId} != {b.GeoId}");

			if (a.CountryName != b.CountryName)
				appendWithPrecedingComma(s, $"CountryName: '{a.CountryName}' != '{b.CountryName}'");

			if (a.CountryTwoLetterCode != b.CountryTwoLetterCode)
				appendWithPrecedingComma(s, $"CountryTwoLetterCode: '{a.CountryTwoLetterCode}' != '{b.CountryTwoLetterCode}'");

			if (a.CountryThreeLetterCode != b.CountryThreeLetterCode)
				appendWithPrecedingComma(s, $"CountryThreeLetterCode: '{a.CountryThreeLetterCode}' != '{b.CountryThreeLetterCode}'");

			if (a.CurrencyName != b.CurrencyName)
				appendWithPrecedingComma(s, $"CurrencyName: '{a.CurrencyName}' != '{b.CurrencyName}'");

			if (a.CurrencyCode != b.CurrencyCode)
				appendWithPrecedingComma(s, $"CurrencyCode: '{a.CurrencyCode}' != '{b.CurrencyCode}'");

			if (includeCurrencySymbol)
			{
				if (a.CurrencySymbol != b.CurrencySymbol)
					appendWithPrecedingComma(s, $"CurrencySymbol: '{a.CurrencySymbol}' != '{b.CurrencySymbol}'");
			}

			if (s.Length > 0)
			{
				difference = s.ToString();
				return false;
			}
			else
			{
				difference = null;
				return true;
			}
		}

		private static void appendWithPrecedingComma(StringBuilder s, string txt)
		{
			if (s.Length > 0)
				s.Append(", ");

			s.Append(txt);
		}

		private static WindowsCountry getCopyWithCurrencyChange(
			WindowsCountry country,
			string currencyName = null,
			string currencyCode = null,
			string currencySymbol = null
		)
		{
			return new WindowsCountry(
				country.GeoId,
				country.CountryName,
				country.CountryTwoLetterCode,
				country.CountryThreeLetterCode,
				currencyName ?? country.CurrencyName,
				currencyCode ??	country.CurrencyCode,
				currencySymbol ?? country.CurrencySymbol
			);
		}

		private static WindowsCountry getCopyWithCountryNameChange(
			WindowsCountry country,
			string countryName
		)
		{
			return new WindowsCountry(
				country.GeoId,
				countryName,
				country.CountryTwoLetterCode,
				country.CountryThreeLetterCode,
				country.CurrencyName,
				country.CurrencyCode,
				country.CurrencySymbol
			);
		}
	}
}
