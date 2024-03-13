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
			public void Test_Croatia() 
			{
				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsTrue(countries.Count > 0);

				var croatias = countries.Where(c => c.Name == "Croatia").ToList();
				Assert.AreEqual(1, croatias.Count);

				// As of March 2024, Windows has not caught up with the fact that
				// since Jan 2023 Croatia uses the Euro.

				var croatia = croatias[0];
				Assert.AreEqual("EUR", croatia.CurrencyCode);
				Assert.AreEqual("€", croatia.CurrencySymbol);
			}

			[TestMethod]
			public void Test_Cyprus()
			{
				// If you use CultureInfo RegionInfo to get at a Windows list of countries, 
				// Cyprus is missing from the list. So it is a known source of problems, 
				// and worth checking for explicitly.

				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsTrue(countries.Count > 0);

				var cypruses = countries.Where(c => c.Name == "Cyprus").ToList();
				Assert.AreEqual(1, cypruses.Count);

				var cyprus = cypruses[0];
				Assert.AreEqual("CY", cyprus.TwoLetterCode);
				Assert.AreEqual("CYP", cyprus.ThreeLetterCode);
				Assert.AreEqual("EUR", cyprus.CurrencyCode);
				Assert.AreEqual("€", cyprus.CurrencySymbol);
			}

			[TestMethod]
			public void Test_SaoTome()
			{
				// Check that we are doing unicode properly.

				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsTrue(countries.Count > 0);

				var tomes = countries.Where(c => c.TwoLetterCode == "ST").ToList();
				Assert.AreEqual(1, tomes.Count);

				var tome = tomes[0];
				Assert.AreEqual("São Tomé and Príncipe", tome.Name);
				Assert.AreEqual("ST", tome.TwoLetterCode);
				Assert.AreEqual("STP", tome.ThreeLetterCode);
				Assert.AreEqual("STN", tome.CurrencyCode);
				Assert.AreEqual("STN", tome.CurrencySymbol);
			}

			[TestMethod]
			public void Test_UnitedKingdom()
			{
				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsTrue(countries.Count > 0);

				var uks = countries.Where(c => c.TwoLetterCode == "GB").ToList();
				Assert.AreEqual(1, uks.Count);

				var uk = uks[0];
				Assert.AreEqual("United Kingdom", uk.Name);
				Assert.AreEqual("GB", uk.TwoLetterCode);
				Assert.AreEqual("GBR", uk.ThreeLetterCode);
				Assert.AreEqual("GBP", uk.CurrencyCode);
				Assert.AreEqual("£", uk.CurrencySymbol);
			}

			[TestMethod]
			public void Test_StringByteLengths()
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
