using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Ledgerscope.GeographicLocations.Generator.Extensions;

namespace Ledgerscope.GeographicLocations.Generator
{
	// Inspired by https://github.com/Ucodia/Blog-GeographicalLocation

	public static partial class GeoLocationHelper
	{
		// This has to be visible to the callback and to GetGeographicalLocations,
		// hence is at module level.
		private static List<int> _geoIds;

		// Who knows what crazy stuff would happen if multi-threaded enumerating the geo locations api call?
		private static readonly object _locker = new object();

		public static string GetGeographicalLocations()
		{
			lock (_locker)
			{
				var geoLocs = new StringBuilder();

				int langId = CultureInfo.InvariantCulture.LCID;
				NativeMethods.EnumGeoInfoProc callback = enumGeoInfoCallback;
				_geoIds = new List<int>();

				int _ = NativeMethods.EnumSystemGeoID(NativeMethods.GEOCLASS_NATION, 0, callback);

				foreach (var geoId in _geoIds)
				{
					string nation = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_NATION, langId).SurroundWithQuotes();
					string latitude = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_LATITUDE, langId).SurroundWithQuotes();
					string longitude = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_LONGITUDE, langId).SurroundWithQuotes();
					string iso2 = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_ISO2, langId).SurroundWithQuotes();
					string iso3 = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_ISO3, langId).SurroundWithQuotes();
					string rfc1766 = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_RFC1766, langId).SurroundWithQuotes();
					string lcid = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_LCID, langId).SurroundWithQuotes();
					string friendlyName = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_FRIENDLYNAME, langId).SurroundWithQuotes();
					string officialName = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_OFFICIALNAME, langId).SurroundWithQuotes();
					string timeZones = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_TIMEZONES, langId).SurroundWithQuotes();
					string officialLanguages = getGeoInfo(geoId, NativeMethods.SYSGEOTYPE.GEO_OFFICIALLANGUAGES, langId).SurroundWithQuotes();

					geoLocs.AppendLine($"yield return new GeoLocation({nation}, {latitude}, {longitude}, {iso2}, {iso3}, {rfc1766}, {lcid}, {friendlyName}, {officialName}, {timeZones}, {officialLanguages});");
				}

				return geoLocs.ToString();
			}
		}

		private static string getGeoInfo(int location, NativeMethods.SYSGEOTYPE geoType, int langId)
		{
			var s = new StringBuilder();
			int bufferSize = NativeMethods.GetGeoInfo(location, geoType, s, 0, langId);

			if (bufferSize > 0)
			{
				s.Capacity = bufferSize;
				int _ = NativeMethods.GetGeoInfo(location, geoType, s, bufferSize, langId);
			}

			return s.ToString();
		}

		private static bool enumGeoInfoCallback(int geoId)
		{
			if (geoId != 0)
			{
				_geoIds.Add(geoId);
				return true;
			}

			return false;
		}
	}
}
