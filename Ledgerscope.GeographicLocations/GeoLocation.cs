using System;

namespace Ledgerscope.GeographicLocations
{
	/// <summary>
	/// Ledgerscope snapshot in time of Windows geographic location data.
	/// </summary>
	/// <remarks>Done so that our Linux build servers have the Windows location data too.</remarks>
	public partial class GeoLocation
	{
		/// <summary>
		/// Like "242" for United Kingdom.
		/// </summary>
		public string Nation { get; }

		public string Latitude { get; }
		public string Longitude { get; }

		/// <summary>
		/// Like "GB" for United Kingdom.
		/// </summary>
		public string ISO2 { get; }

		/// <summary>
		/// Like "GBR" for United Kingdom.
		/// </summary>
		public string ISO3 { get; }

		public string Rfc1766 { get; }
		public string Lcid { get; }

		/// <summary>
		/// Like "United Kingdom" for the UK.
		/// </summary>
		public string FriendlyName { get; }

		/// <summary>
		/// Like "United Kingdom of Great Britain and Northern Ireland" for the UK.
		/// </summary>
		public string OfficialName { get; }

		public string TimeZones { get; }
		public string OfficialLanguages { get; }

		[Obsolete("Use CurrencyCode instead.")]
		public string Currency => this.CurrencyCode;

		/// <summary>
		/// Like "GBP" for the UK.
		/// </summary>
		public string CurrencyCode { get; set; }

		/// <summary>
		/// Like "£" for the UK.
		/// </summary>
		public string CurrencySymbol { get; set; }

		public GeoLocation(string nation, string latitude, string longitude, string iso2, string iso3, string rfc1766, string lcid, 
			string friendlyName, string officialName, string timeZones, string officialLanguages, string currencyCode, string currencySymbol)
		{
			Nation = nation;
			Latitude = latitude;
			Longitude = longitude;
			ISO2 = iso2;
			ISO3 = iso3;
			Rfc1766 = rfc1766;
			Lcid = lcid;
			FriendlyName = friendlyName;
			OfficialName = officialName;
			TimeZones = timeZones;
			OfficialLanguages = officialLanguages;
			CurrencyCode = currencyCode;
			CurrencySymbol = currencySymbol;
		}

		public override string ToString()
		{
			return $"{FriendlyName} [{ISO2}]";
		}
	}
}
