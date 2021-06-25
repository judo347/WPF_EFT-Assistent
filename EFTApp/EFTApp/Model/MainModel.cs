using System;
using System.Collections.Generic;

namespace EFTApp.Model
{
	public class MainModel
	{
		private QuestManagerTree qmt;
		private PlayerInfo playerInfo;

		public MainModel()
		{
			initialize();
		}

		private void initialize()
		{
			playerInfo = new PlayerInfo(1);
			qmt = new QuestManagerTree(playerInfo);
		}

		public void recheckLockedQuests()
		{
			throw new ArgumentException("Should not be called!");
		}

		public void completeQuest(Quest quest)
		{
			qmt.completeQuest(quest, playerInfo);
		}

		public void acceptQuest(Quest quest)
		{
			qmt.acceptQuest(quest, playerInfo);
		}

		public void incrementTraderLoyaltyLevel(TraderType traderType)
		{
			if(playerInfo.getLoyaltyLevelFromTrader(traderType) < 4)
			{
				playerInfo.incrementLoyaltyLevel(traderType);
				qmt.playerInfoHasBeenUpdated(playerInfo);
			}
		}

		public void incrementPlayerLevel()
		{
			playerInfo.incrementPlayerLevel();
			qmt.playerInfoHasBeenUpdated(playerInfo);
		}

		public void loadSlot(int slotNumber)
		{
			throw new NotImplementedException();
			/*
			JSONParserHelper jph = new JSONParserHelper();
			SaveData saveData = jph.loadSlot(slotNumber);

			playerInfo.reload(saveData.playerInfo);
			qmt.reloadFromCompletedQuests(saveData.playerInfo, saveData.completedQuestIds);
			*/
		}

		public bool saveSlot(int slotNumber)
		{
			throw new NotImplementedException();
			/*
			JSONParserHelper jph = new JSONParserHelper();
			bool didSave = jph.SaveData(slotNumber, new List<Quest>(qmt.getCompletedQuests()), playerInfo);
			if (didSave)
			{
				Console.WriteLine("Save successful!");
			}

			else
			{
				Console.WriteLine("Save failed!");
			}
            
			return didSave;*/
		}

		public QuestManagerTree getQmt()
		{
			return qmt;
		}

		public PlayerInfo getPlayerInfo()
		{
			return playerInfo;
		}
	}
}
