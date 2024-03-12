using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator
{
	public partial class SysCountryHelper
	{
		[TestClass]
		public class UnitTests
		{
			[TestMethod]
			public void Test_GetAllTwoLetterCodes()
			{
				var codes = getAllTwoLetterCodes();
				Assert.IsNotNull(codes);
				Assert.IsTrue(codes.Count > 0);
			}

			[TestMethod]
			public void Test_GetAllCountries() 
			{
				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsTrue(countries.Count > 0);

				var croatias = countries.Where(c => c.Name == "Croatia").ToList();
				Assert.AreEqual(1, croatias.Count);

				var croatia = croatias[0];
				Assert.AreEqual("EUR", croatia.CurrencyCode);
				Assert.AreEqual("€", croatia.CurrencySymbol);
			}
		}
	}
}
