using System;

namespace Ledgerscope.GeographicLocations
{
	/// <summary>
	/// Representation of a country that Windows knows about.
	/// </summary>
	public partial class WindowsCountry
	{
		public int GeoId { get; }
		public string CountryName { get; }
		public string CountryTwoLetterCode { get; }
		public string CountryThreeLetterCode { get; }

		public string CurrencyName { get; }
		public string CurrencyCode { get; }
		public string CurrencySymbol { get; }

		public WindowsCountry(int geoId, string countryName, string countryTwoLetterCode, string countryThreeLetterCode, string currencyName, string currencyCode, string currencySymbol)
		{
			GeoId = geoId;
			CountryName = countryName;
			CountryTwoLetterCode = countryTwoLetterCode;
			CountryThreeLetterCode = countryThreeLetterCode;
			CurrencyName = currencyName;
			CurrencyCode = currencyCode;
			CurrencySymbol = currencySymbol;
		}

		public override string ToString()
			=> $"{CountryName} ({CountryTwoLetterCode})";
	}
}
