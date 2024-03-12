using System;

namespace Ledgerscope.GeographicLocations
{
	/// <summary>
	/// Ledgerscope snapshot in time of Windows geographic location data.
	/// </summary>
	/// <remarks>Done so that our Linux build servers have the Windows location data too.</remarks>
	[Obsolete("Prefer SysCountry instead - that knows about currency symbols.")]
	public partial class GeoLocation
	{
		public string Nation { get; }
		public string Latitude { get; }
		public string Longitude { get; }
		public string ISO2 { get; }
		public string ISO3 { get; }
		public string Rfc1766 { get; }
		public string Lcid { get; }
		public string FriendlyName { get; }
		public string OfficialName { get; }
		public string TimeZones { get; }
		public string OfficialLanguages { get; }
		public string Currency { get; set; }

		public GeoLocation(string nation, string latitude, string longitude, string iso2, string iso3, string rfc1766, string lcid, 
			string friendlyName, string officialName, string timeZones, string officialLanguages, string currency)
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
			Currency = currency;
		}

		public override string ToString()
		{
			return $"{FriendlyName} [{ISO2}]";
		}
	}
}
