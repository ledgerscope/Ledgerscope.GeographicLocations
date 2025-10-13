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
				Assert.IsNotEmpty(codes);
			}

			[TestMethod]
			public void Test_Croatia() 
			{
				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsNotEmpty(countries);

				var croatias = countries.Where(c => c.Name == "Croatia").ToList();
				Assert.HasCount(1, croatias);

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
				Assert.IsNotEmpty(countries);

				var cypruses = countries.Where(c => c.Name == "Cyprus").ToList();
				Assert.HasCount(1, cypruses);

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
				Assert.IsNotEmpty(countries);

				var tomes = countries.Where(c => c.TwoLetterCode == "ST").ToList();
				Assert.HasCount(1, tomes);

				var tome = tomes[0];
				Assert.AreEqual("São Tomé and Príncipe", tome.Name);
				Assert.AreEqual("ST", tome.TwoLetterCode);
				Assert.AreEqual("STP", tome.ThreeLetterCode);
				Assert.AreEqual("STN", tome.CurrencyCode);
				Assert.AreEqual("STN", tome.CurrencySymbol);
			}

			[TestMethod]
			public void Test_Kronas()
			{
				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsNotEmpty(countries);

				var denmark = countries.Where(c => c.TwoLetterCode == "DK").Single();
				var sweden = countries.Where(c => c.TwoLetterCode == "SE").Single();
				var norway = countries.Where(c => c.TwoLetterCode == "NO").Single();

				Assert.AreEqual("Denmark", denmark.Name);
				Assert.AreEqual("DKK", denmark.CurrencyCode);
				// Yeah, Denmark gets a dot but the others don't. <shrug>
				Assert.AreEqual("kr.", denmark.CurrencySymbol);

				Assert.AreEqual("Sweden", sweden.Name);
				Assert.AreEqual("SEK", sweden.CurrencyCode);
				Assert.AreEqual("kr", sweden.CurrencySymbol);

				Assert.AreEqual("Norway", norway.Name);
				Assert.AreEqual("NOK", norway.CurrencyCode);
				Assert.AreEqual("kr", norway.CurrencySymbol);
			}

			[TestMethod]
			public void Test_UnitedKingdom()
			{
				var countries = GetAllCountries();
				Assert.IsNotNull(countries);
				Assert.IsNotEmpty(countries);

				var uks = countries.Where(c => c.TwoLetterCode == "GB").ToList();
				Assert.HasCount(1, uks);

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
				Assert.HasCount(7, "Curacao");
				Assert.HasCount(7, Encoding.UTF8.GetBytes("Curacao"));
				Assert.HasCount(7, "Curaçao");
				Assert.HasCount(8, Encoding.UTF8.GetBytes("Curaçao"));

				Assert.HasCount(21, "Sao Tome and Principe");
				Assert.HasCount(21, Encoding.UTF8.GetBytes("Sao Tome and Principe"));
				Assert.HasCount(21, "São Tomé and Príncipe");
				Assert.HasCount(24, Encoding.UTF8.GetBytes("São Tomé and Príncipe"));
			}
		}
	}
}
