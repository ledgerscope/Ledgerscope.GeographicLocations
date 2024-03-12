using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator
{
	public static partial class WindowsCountryHelper
	{
		[TestClass]
		public class UnitTests
		{
			[TestMethod]
			public void Test_GetAllCountries()
			{
				var countries = WindowsCountryHelper.GetAllCountries();

				Assert.IsNotNull(countries);
				Assert.IsTrue(countries.Count > 0);

				var croatias = countries.Where(c => c.CountryName == "Croatia").ToList();
				if (croatias.Count != 1)
				{
					Assert.Fail("There should be exactly one Croatia.");
				}

				var croatia = croatias[0];
				Assert.AreEqual("Euro", croatia.CurrencyName);
				Assert.AreEqual("EUR", croatia.CurrencyCode);
				Assert.AreEqual("€", croatia.CurrencySymbol);
			}

			//[TestMethod]
			//public void Test_DumpClassDefnToFile()
			//{
			//	string classDefn = GetWindowsCountryClassDefinition();
			//	File.WriteAllText(@"E:\temp\classdefn\WindowsCountry.cs", classDefn);
			//}
		}
	}
}
