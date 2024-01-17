using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator
{
	public class NativeMethods
	{
		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern int GetGeoInfo(int location, SYSGEOTYPE geoType, StringBuilder lpGeoData, int cchData, int langId);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern int EnumSystemGeoID(int geoClass, int parentGeoId, EnumGeoInfoProc lpGeoEnumProc);

		public const int GEOCLASS_NATION = 0x10;

		public delegate bool EnumGeoInfoProc(int GeoId);

		public enum SYSGEOTYPE
		{
            GEO_NATION = 0x0001,
            GEO_LATITUDE = 0x0002,
            GEO_LONGITUDE = 0x0003,
            GEO_ISO2 = 0x0004,
            GEO_ISO3 = 0x0005,
            GEO_RFC1766 = 0x0006,
            GEO_LCID = 0x0007,
            GEO_FRIENDLYNAME = 0x0008,
            GEO_OFFICIALNAME = 0x0009,
            GEO_TIMEZONES = 0x000A,
            GEO_OFFICIALLANGUAGES = 0x000B,
            GEO_ISO_UN_NUMBER = 0x000C,
            GEO_PARENT = 0x000D,
            GEO_DIALINGCODE = 0x000E,
            GEO_CURRENCYCODE = 0x000F,
            GEO_CURRENCYSYMBOL = 0x0010,
            GEO_NAME = 0x0011,
            GEO_ID = 0x0012
        }
	}
}
