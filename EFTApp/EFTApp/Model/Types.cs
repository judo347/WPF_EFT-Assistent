using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EFTApp.Model
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MapType
	{
		Customs, Factory, Interchange, Woods, Shoreline, Reserve, Labs, Mixed
	}
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TraderType
	{
		Prapor, Therapist, Fence, Skier, Peacekeeper, Mechanic, Ragman, Jaeger
	}
}
