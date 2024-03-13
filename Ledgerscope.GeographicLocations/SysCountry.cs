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
			if (twoLetterCode.Length != 2)
				throw new ArgumentException("Two-letter code must be exactly 2 characters long", nameof(twoLetterCode));

			if (threeLetterCode.Length != 3)
				throw new ArgumentException("Three-letter code must be exactly 3 characters long", nameof(threeLetterCode));

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
