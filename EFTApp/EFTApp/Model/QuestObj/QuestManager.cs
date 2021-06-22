using System;
using System.Collections.Generic;

namespace EFTApp.Model
{
	class QuestManager
	{
		private List<Quest> allQuests;
		private PlayerInfo playerInfo;

		private List<Quest> completed = new List<Quest>();
		private List<Quest> activeQuests = new List<Quest>();
		private List<Quest> lockedQuests = new List<Quest>();

		private readonly int quest_id_postman_pat_part1 = 7;
		private readonly int quest_id_postman_pat_part2 = 38;
		private readonly int quest_id_collector = 185;

		private readonly int quest_id_kind_of_sabotage = 63;
		private readonly int quest_id_supply_plans = 30;

		SpecialCaseChem4Helper specialCaseChem4Helper;

		public QuestManager(PlayerInfo playerInfo)
		{
			this.specialCaseChem4Helper = new SpecialCaseChem4Helper(this);
			this.playerInfo = playerInfo;
			allQuests = loadAllQuests();
			foreach (Quest quest in new List<Quest>(allQuests))
			{
				addQuestToManager(quest, playerInfo);
			}
		}

		/** Adds the given quest to activeQuests if requirements are met.
		 * Should only be used as initializing and loading. */
		private void addQuestToManager(Quest quest, PlayerInfo playerInfo)
		{

			if (specialCaseChem4Helper.addQuestCheck(quest)) { }
			else if (canQuestBeAddedToActive(quest, playerInfo))
				activeQuests.Add(quest);
			else
				lockedQuests.Add(quest);
		}

		/** Returns true if quest fulfills all requirements for being active. */
		private bool canQuestBeAddedToActive(Quest quest, PlayerInfo playerInfo)
		{

			// Special case: quest: collector TODO handle
			if (quest.id == quest_id_collector)
				//method for check
				return false;

			// Check player level requirement
			if (quest.requiredLevel > playerInfo.getPlayerLevel())
			{
				return false;
			}

			//Handle LL requirements for quests
			TraderType questGiver = quest.trader;
			int currentLoyaltyLevelForTrader = playerInfo.getLoyaltyLevelFromTrader(questGiver);
			if (quest.requiredLoyaltyLevel > currentLoyaltyLevelForTrader)
			{
				return false;
			}

			//Handle prerequisite quests
			if (!isRequiredQuestsCompleted(quest))
			{
				return false;
			}

			return true;
		}

		/** Moves *locked* quests which fulfills checks to *active*. */
		public void doPrerequisiteQuestCheckForLocked()
		{
			bool wasListUpdated = false;

			foreach (Quest quest in new List<Quest>(lockedQuests))
			{
				bool shouldBeActive = canQuestBeAddedToActive(quest, playerInfo);
				if (shouldBeActive)
				{
					moveQuestFromLockedToActive(quest);
					wasListUpdated = true;
					break;
				}
			}

			if (wasListUpdated)
				doPrerequisiteQuestCheckForLocked();
		}

		/** Used when a quest becomes active. */
		private void moveQuestFromLockedToActive(Quest quest)
		{
			lockedQuests.Remove(quest);
			activeQuests.Add(quest);

			//Special case 1
			specialCaseChem4Helper.chem4FromLockedToActiveCheck(quest);
		}

		/** Compares the given quest´s required quests with completed quests. */
		private bool isRequiredQuestsCompleted(Quest quest)
		{
			List<int> requiredQuestIds = quest.requiredQuests;

			foreach (Quest compQuest in completed)
			{
				foreach (int regQuestId in requiredQuestIds)
				{
					if (compQuest.id == regQuestId)
						requiredQuestIds.Remove(regQuestId);
					break;
				}
			}

			return requiredQuestIds.Count == 0;
		}

		/** Completes given quest and updates model. (Including prerequisite check)*/
		public void completeQuest(Quest quest)
		{
			int givenQuestId = quest.id;

			//Special case: quest: postman pat
			if (givenQuestId == quest_id_postman_pat_part1)
			{
				if (!canPostmanPatPart1BeCompleted())
					return;
			}

			//Special case: quest: supply plans or kind of sabotage
			specialCaseSupplyPlansAndKindOfSabotageCompleteCheck(quest);

			//Special case 1
			specialCaseChem4Helper.primaryCompleteCheck(quest);

			quest.complete();
			activeQuests.Remove(quest);
			completed.Add(quest);

			doPrerequisiteQuestCheckForLocked();
		}

		/** Special case: quest: Supply plans and Kind of sabotage: one completes the other. */
		private void specialCaseSupplyPlansAndKindOfSabotageCompleteCheck(Quest quest)
		{
			if (quest.id == quest_id_supply_plans)
			{
				bool questFound = false;
				foreach (Quest q in new List<Quest>(activeQuests))
				{
					if (q.id == quest_id_kind_of_sabotage)
					{
						q.complete();
						activeQuests.Remove(q);
						completed.Add(q);
						questFound = true;
					}

					if (questFound)
						break;
				}
			}
			else if (quest.id == quest_id_kind_of_sabotage)
			{
				bool questFound = false;
				foreach (Quest q in new List<Quest>(activeQuests))
				{
					if (q.id == quest_id_supply_plans)
					{
						q.complete();
						activeQuests.Remove(q);
						completed.Add(q);
						questFound = true;
					}

					if (questFound)
						break;
				}
			}
		}

		/** Special case: quest: postman pat
			Postman pat part 1 - cannot be completed before part 2 is */
		private bool canPostmanPatPart1BeCompleted()
		{
			// Check: has postman pat part 2 is completed
			foreach (Quest quest in completed)
			{
				if (quest.id == quest_id_postman_pat_part2)
					return true;
			}

			return true; //Todo: was false. Workaround = true
		}

		/** Reloads quest model based on the given completed quest ids. (Sets active and locked)
		 Used when loading saved data. */
		public void reloadFromCompletedQuests(List<int> completedQuestIds, PlayerInfo playerInfo)
		{
			// Currently done before method call
			//allQuests = loadAllQuests();
			//activeQuests = new List<>();
			//completed = new List<>();

			//Find all quests which has to be completed
			//TODO optimize, dictionary for quests?
			List<Quest> questsToComplete = new List<Quest>();
			foreach (Quest quest in allQuests)
				foreach (int complId in completedQuestIds)
					if (quest.id == complId)
					{
						questsToComplete.Add(quest);
						break;
					}

			foreach (Quest quest in questsToComplete)
			{
				completeQuestRecursively(quest, playerInfo);
			}
		}

		//TODO Write tests for special cases generally
		private void completeQuestRecursively(Quest quest, PlayerInfo playerInfo)
		{

			if (!quest.isCompleted())
			{
				//TODO make quests have refs to each other
				List<Quest> requiredQuests = new List<Quest>();
				foreach (int regQuestId in quest.requiredQuests)
				{
					foreach (Quest allQ in allQuests)
					{
						if (allQ.id == regQuestId)
						{
							requiredQuests.Add(allQ);
							break;
						}
					}
				}
				if (requiredQuests.Count != quest.requiredQuests.Count)
					throw new ArgumentException("Did not find all required quests"); //TODO Is proberly thrown if quest required is special case which is saved in different class

				//Call method on all required quests for this one
				foreach (Quest q in requiredQuests)
					completeQuestRecursively(q, playerInfo);

				if (!canQuestBeAddedToActive(quest, playerInfo))
				{
					Console.WriteLine(quest.id + " " + quest.requiredLevel + " " + playerInfo.getPlayerLevel());
					//TODO debug note: quests is here because it should be completed. But when this is hit, the prereuistite quests are not completed yet??
					throw new ArgumentException("Something went wrong!");  //TODO This will be throw if player loads a save where Collector is completed
				}

				completeQuest(quest);
			}
		}

		/** Loads all quests from data file. */
		private List<Quest> loadAllQuests()
		{
			throw new NotImplementedException();
			/*
			JSONParserHelper jph = new JSONParserHelper();
			return jph.getAllQuestsFromFile();
			*/
		}

		public List<Quest> getActiveQuests()
		{
			return activeQuests;
		}

		public List<Quest> getCompleted()
		{
			return completed;
		}

		public List<Quest> getLockedQuests()
		{
			return lockedQuests;
		}

		public int getNumberOfCompletedQuests()
		{
			return completed.Count;
		}

		public int getTotalNumberOfQuests()
		{
			return allQuests.Count;
		}
	}
}
