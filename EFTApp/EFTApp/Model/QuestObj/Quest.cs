using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace EFTApp.Model
{
	public class Quest
	{
		public enum QuestState
		{
			ACTIVE, LOCKED, COMPLETED
		}

		public string name { get; set; }
		public List<MapType> maps { get; set; } = new List<MapType>();
		[JsonProperty("giver")]
		public TraderType trader { get; set; }
		[JsonProperty("req_level")]
		public int requiredLevel { get; set; }
		[JsonProperty("req_LL")]
		public int requiredLoyaltyLevel { get; set; } = 0;
		public int id { get; set; }
		public List<int> requiredQuests { get; set; } = new List<int>();
		public List<QuestObjectives> objectives { get; set; }
		public List<string> requirements { get; set; } = new List<string>();

		private bool completed = false;
		public QuestState state { get; set; } = QuestState.LOCKED;

		public Quest() {}

		public Quest(string name, List<MapType> maps, TraderType trader, int requiredLevel, int requiredLoyaltyLevel, int id, List<int> requiredQuests, List<QuestObjectives> objectives, List<string> requirements)
		{
			this.name = name;
			this.maps = maps;
			this.trader = trader;
			this.requiredLevel = requiredLevel;
			this.requiredLoyaltyLevel = requiredLoyaltyLevel;
			this.id = id;
			this.requiredQuests = requiredQuests;
			this.objectives = objectives;
			this.requirements = requirements;
		}

		public bool isCompleted() { return completed; }
		public void complete() { completed = true; }

		//TODO check if code ok?
		public bool setStateActive(PlayerInfo playerInfo)
		{
			if (requiredLoyaltyLevel > 0)
			{
				if (playerInfo.getLoyaltyLevelFromTrader(this.trader) < requiredLoyaltyLevel)
				{
					return false;
				}
			}

			if (playerInfo.getPlayerLevel() < requiredLevel)
			{
				return false;
			}

			state = QuestState.ACTIVE;
			return true;
		}
	}
}
