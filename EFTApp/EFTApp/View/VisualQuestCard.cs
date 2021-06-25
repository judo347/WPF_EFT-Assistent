using EFTApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EFTApp.Model.Quest;

namespace EFTApp.View
{
    public class VisualQuestCard
    {
		public Quest quest { get; set; }
		public string stateColor { get; set; } = "#9E1C7F"; //Purple
		public bool isGhost { get; set; } = false; //If true, the UI is showing multiples of this card.

		public VisualQuestCard(Quest quest)
		{
			this.quest = quest;
			updateStateColorFromQuest();
		}

		public VisualQuestCard(Quest quest, QuestState newQuestState)
		{
			this.quest = quest;
			this.quest.state = newQuestState;
			updateStateColorFromQuest();
		}

		public void updateStateColorFromQuest()
		{
			Console.WriteLine("Updating quest card color");
			Console.WriteLine("Before: " + stateColor);
			this.stateColor = getStateColor();
			Console.WriteLine("After: " + stateColor);
		}

		public string getStateColor()
		{
			switch (quest.state)
			{
				case Quest.QuestState.AVAILABLE: return "#F7FF6D"; //Yellow
				case Quest.QuestState.ACCEPTED: return "#84B7FF"; //Light blue
				case Quest.QuestState.COMPLETED: return "#10FF00"; //Green
				case Quest.QuestState.LOCKED: return "#A0A0A0"; //Grey
			}

			throw new Exception("Undefined quest state found.");
		}
	}
}
