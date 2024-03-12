using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledgerscope.GeographicLocations.Generator.Extensions
{
	public static partial class StringExtensions
	{
		[TestClass]
		public class UnitTests
		{
			[TestMethod]
			public void Test_SurroundWithQuotes()
			{
				// Arrange
				string str = "test";
				string expected = "\"test\"";

				// Act
				string actual = str.SurroundWithQuotes();

				// Assert
				Assert.AreEqual(expected, actual);
			}
		}
	}
}
