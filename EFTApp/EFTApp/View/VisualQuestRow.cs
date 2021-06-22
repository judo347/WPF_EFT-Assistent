using EFTApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTApp.View
{
    public class VisualQuestRow
    {
		//TODO add type, trader or map
		public MapType Map { get; set; } //TODO temp
		public List<VisualQuestCard> Quests { get; set; } = new List<VisualQuestCard>(); //TODO replace with ordered list?

		public VisualQuestRow(MapType map)
		{
			this.Map = map;
		}
	}
}
