using System;
using System.Collections.Generic;
using System.Text;

namespace Ledgerscope.GeographicLocations
{
	internal class ProblemCatcher
	{
		public static void TestForProblems()
		{
			// If you are finding that this will not compile, it'll be because the 
			// code generators aren't running, or are (silently) throwing errors when they run.

			_ = GeoLocation.GetAll();
			//_ = WindowsCountry.GetAll();
			_ = SysCountry.GetAll();
		}
	}
}
