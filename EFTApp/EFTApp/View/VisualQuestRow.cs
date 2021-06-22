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
		public TraderType Trader { get; set; } //TODO temp
		public string identificationString { get; set; } //TODO temp, should be replaced with picture
		public List<VisualQuestCard> Quests { get; set; } = new List<VisualQuestCard>(); //TODO replace with ordered list?

		public VisualQuestRow(MapType map)
		{
			this.Map = map;
			this.identificationString = map.ToString();
		}

		public VisualQuestRow(TraderType trader)
		{
			this.Trader = trader;
			this.identificationString = trader.ToString();
		}

		public void clearQuestsList()
		{
			this.Quests = new List<VisualQuestCard>();
		}

		public void addQuestCard(VisualQuestCard card)
		{
			Quests.Add(card);
		}
	}
}
