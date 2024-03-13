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
			// Source Code Generation in Visual Studio is buggy.  Here's what you need to do:
			// * Close the solution
			// * Delete the bin and obj folders
			// * Re-open the solution
			// * Rebuild the Ledgerscope.GeographicLocations.Generator project
			// * Close the solution
			// * Re-open the solution
			// * Rebuild the Ledgerscope.GeographicLocations project

			_ = GeoLocation.GetAll();
			//_ = WindowsCountry.GetAll();
			_ = SysCountry.GetAll();

			// As long as the above lines compile (no red wavy lines), then the code generators are working
			// and it should go through ok on DevOps Pipeline, even if the Error List window is showing errors.
		}
	}
}
