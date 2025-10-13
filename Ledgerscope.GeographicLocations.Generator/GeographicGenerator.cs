using System;
using Microsoft.CodeAnalysis;

namespace Ledgerscope.GeographicLocations.Generator
{
	[Generator]
	public class GeographicGenerator : IIncrementalGenerator
	{
		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
			context.RegisterPostInitializationOutput((i) =>
			{
				var geographies = GeoLocationHelper.GetGeographicalLocations();

				var partialClass = $@"
using System;
using System.Collections.Generic;
namespace Ledgerscope.GeographicLocations
{{
	public partial class GeoLocation
	{{
		public static IEnumerable<GeoLocation> GetAll()
		{{
			{geographies}
		}}
	}}
}}
";
				i.AddSource("GeoLocation.g.cs", partialClass);
			});
		}
	}
}
