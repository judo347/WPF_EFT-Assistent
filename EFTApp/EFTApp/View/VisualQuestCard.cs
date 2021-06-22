using EFTApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTApp.View
{
    public class VisualQuestCard
    {
		public Quest quest { get; set; }
		public string stateColor { get; set; } = "Black";
		public bool isGhost { get; set; } = false; //If true, the UI is showing multiples of this card.

		public VisualQuestCard(Quest quest)
		{
			this.quest = quest;
			updateStateColorFromQuest();
		}

		public void updateStateColorFromQuest()
		{
			this.stateColor = getStateColor();
		}

		public string getStateColor()
		{
			switch (quest.state)
			{
				case Quest.QuestState.ACTIVE: return "White";
				case Quest.QuestState.COMPLETED: return "Green";
				case Quest.QuestState.LOCKED: return "Yellow";
			}

			throw new Exception("Undefined quest state found.");
		}
	}
}
