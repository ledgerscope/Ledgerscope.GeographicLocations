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

			[TestMethod]
			public void Test_StringLengths()
			{
				Assert.AreEqual(7, "Curacao".Length);
				Assert.AreEqual(7, Encoding.UTF8.GetBytes("Curacao").Length);
				Assert.AreEqual(7, "Curaçao".Length);
				Assert.AreEqual(8, Encoding.UTF8.GetBytes("Curaçao").Length);

				Assert.AreEqual(21, "Sao Tome and Principe".Length);
				Assert.AreEqual(21, Encoding.UTF8.GetBytes("Sao Tome and Principe").Length);
				Assert.AreEqual(21, "São Tomé and Príncipe".Length);
				Assert.AreEqual(24, Encoding.UTF8.GetBytes("São Tomé and Príncipe").Length);
			}
		}
	}
}
