using System;

namespace Ledgerscope.GeographicLocations
{
	public partial class SysCountry
	{
		public string Name { get; }
		public string TwoLetterCode { get; }
		public string ThreeLetterCode { get; }
		public string CurrencyCode { get; }
		public string CurrencySymbol { get; }

		public SysCountry(string name, string twoLetterCode, string threeLetterCode, string currencyCode, string currencySymbol)
		{
			Name = name;
			TwoLetterCode = twoLetterCode;
			ThreeLetterCode = threeLetterCode;
			CurrencyCode = currencyCode;
			CurrencySymbol = currencySymbol;
		}

		public override string ToString()
			=> $"{this.Name} ({this.TwoLetterCode}, {this.ThreeLetterCode}), {this.CurrencyCode} ({this.CurrencySymbol})";
	}
}
