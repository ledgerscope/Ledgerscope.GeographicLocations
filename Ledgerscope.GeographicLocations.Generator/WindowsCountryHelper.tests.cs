//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;

//namespace Ledgerscope.GeographicLocations.Generator
//{
//	public static partial class WindowsCountryHelper
//	{
//		[TestClass]
//		public class UnitTests
//		{
//			[TestMethod]
//			public void Test_GetAllCountries()
//			{
//				var countries = WindowsCountryHelper.GetAllCountries();

//				Assert.IsNotNull(countries);
//				Assert.IsTrue(countries.Count > 0);

//				var croatias = countries.Where(c => c.CountryName == "Croatia").ToList();
//				if (croatias.Count != 1)
//				{
//					Assert.Fail("There should be exactly one Croatia.");
//				}

//				var croatia = croatias[0];
//				Assert.AreEqual("Euro", croatia.CurrencyName);
//				Assert.AreEqual("EUR", croatia.CurrencyCode);
//				Assert.AreEqual("€", croatia.CurrencySymbol);
//			}

//			[TestMethod]
//			public void Test_WhereIsCyprus()
//			{
//				var regions = new List<RegionInfo>();
//				var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

//				foreach (var culture in cultures)
//				{
//					var region =  getRegion(culture.LCID);
//					// Console.WriteLine(region.EnglishName);

//					if (region != null)
//						regions.Add(region);
//				}

//				var sortedRegions = regions.OrderBy(r => r.EnglishName).ToList();
//				var cypruses = sortedRegions.Where(r => r.EnglishName == "Cyprus").ToList();
//				Assert.AreEqual(1, cypruses.Count);
//			}

//			private static RegionInfo getRegion(int lcid)
//			{
//				try
//				{
//					return new RegionInfo(lcid);
//				}
//				catch
//				{
//					return null;
//				}
//			}

//			//[TestMethod]
//			//public void Test_DumpClassDefnToFile()
//			//{
//			//	string classDefn = GetWindowsCountryClassDefinition();
//			//	File.WriteAllText(@"E:\temp\classdefn\WindowsCountry.cs", classDefn);
//			//}
//		}
//	}
//}
