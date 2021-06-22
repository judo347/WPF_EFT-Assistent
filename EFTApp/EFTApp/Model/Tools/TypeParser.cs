using System;

namespace EFTApp.Model
{
	class TypeParser
	{
		public static MapType stringToMapType(String mapName)
		{
			//TODO: Could/should be rewritten! Just TryParse and return result if pass returns true
			foreach (MapType type in Enum.GetValues(typeof(MapType)))
			{
				MapType result;
				if(Enum.TryParse(mapName, out result))
					return type;
			}

			throw new ArgumentException();
		}

		public static TraderType stringToTrader(String traderName)
		{
			//TODO: Could/should be rewritten! Just TryParse and return result if pass returns true
			foreach (TraderType type in Enum.GetValues(typeof(TraderType)))
			{
				TraderType result;
				if (Enum.TryParse(traderName, out result))
					return type;
			}

			throw new ArgumentException();
		}
	}
}
