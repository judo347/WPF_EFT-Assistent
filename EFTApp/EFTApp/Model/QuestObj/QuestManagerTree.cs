﻿using EFTApp.Model.Tools.DataTools;
using System;
using System.Collections.Generic;

namespace EFTApp.Model
{
	public class QuestManagerTree
	{
		public QuestModel questModel { get; }

		public QuestManagerTree(PlayerInfo playerInfo)
		{
			List<Quest> allQuests = loadAllQuests();

			questModel = new QuestModel(allQuests, playerInfo);
		}

		/** Loads all quests from data file. */
		private List<Quest> loadAllQuests()
		{
			JSONParserHelper jph = new JSONParserHelper();
			return jph.getAllQuestsFromFile();
		}

		public void playerInfoHasBeenUpdated(PlayerInfo playerInfo)
		{
			questModel.recheckQuestRequirements(playerInfo);
		}

		public void completeQuest(Quest quest, PlayerInfo playerInfo)
		{
			questModel.completeQuest(quest, playerInfo);
		}

		public void acceptQuest(Quest quest, PlayerInfo playerInfo)
		{
			questModel.acceptQuest(quest, playerInfo);
		}

		/** Reloads quest model based on the given completed quest ids. (Sets active and completed)
		 Used when loading saved data. */
		public void reloadFromCompletedQuests(PlayerInfo playerInfo, List<int> completedQuestIds)
		{
			questModel.setQuestStatesFromCompletedQuestIds(playerInfo, new List<int>(completedQuestIds));
		}

		public List<Quest> getCompletedQuests()
		{
			return questModel.getQuestsWithState(Quest.QuestState.COMPLETED);
		}

		public List<Quest> getAvailableQuests()
		{
			return questModel.getQuestsWithState(Quest.QuestState.AVAILABLE);
		}

		public List<Quest> getAcceptedQuests()
		{
			return questModel.getQuestsWithState(Quest.QuestState.ACCEPTED);
		}

		public List<Quest> getLockedQuests()
		{
			return questModel.getQuestsWithState(Quest.QuestState.LOCKED);
		}

		public int getTotalNumberOfQuests()
		{
			return questModel.getTotalNumberOfQuests();
		}
	}
}
