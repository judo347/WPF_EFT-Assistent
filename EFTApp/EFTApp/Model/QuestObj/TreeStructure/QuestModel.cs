using System;
using System.Collections.Generic;

namespace EFTApp.Model
{
	public class QuestModel
	{
		public List<Quest> allQuests = new List<Quest>();

		private List<QuestNode> rootNodes = new List<QuestNode>();
		private Dictionary<int, Quest> questIdMap = new Dictionary<int, Quest>();
		private Dictionary<Quest, QuestNode> questNodeMap = new Dictionary<Quest, QuestNode>();

		public QuestModel(List<Quest> allQuests, PlayerInfo playerInfo)
		{
			this.allQuests = allQuests;

			//Create a node for each quest and store in map
			foreach (Quest quest in allQuests)
			{
				QuestNode questNode = new QuestNode(quest);
				questNodeMap[quest] = questNode;
				questIdMap[quest.id] = quest;
			}

			//Run through each node and add required quests
			foreach (Quest quest in allQuests)
			{
				QuestNode questNode = questNodeMap[quest];
				foreach (int reqId in quest.requiredQuests)
				{
					questNode.AddRequiredQuestNode(questNodeMap[questIdMap[reqId]]);
				}
			}

			//Find root notes //TODO: expensive approach
			Dictionary<int, QuestNode> foundRootNodes = new Dictionary<int, QuestNode>();
			foreach (Quest quest in allQuests)
			{
				List<QuestNode> rootNodes = questNodeMap[quest].getRootNodes();
				foreach (QuestNode rootNode in rootNodes)
				{
					foundRootNodes[rootNode.getQuestId()] = rootNode;
				}
			}

			rootNodes.AddRange(foundRootNodes.Values);

			//Give notes their following notes
			foreach (Quest quest in allQuests)
			{
				QuestNode questNode = questNodeMap[quest];
				questNode.addSelfToRequiredQuests();
			}

			initialQuestStateCheck(playerInfo);
		}

		//Checks for which quests should be active at launch
		private void initialQuestStateCheck(PlayerInfo playerInfo)
		{
			foreach (QuestNode questNode in rootNodes)
			{
				questNode.initialQuestStateActiveCheck(playerInfo);
			}
		}

		/** Searches for locked quests that can be active.*/
		public void recheckQuestRequirements(PlayerInfo playerInfo)
		{
			foreach (QuestNode questNode in rootNodes)
			{
				questNode.reEvalLockedQuests(playerInfo);
			}
		}

		public void completeQuest(Quest quest, PlayerInfo playerInfo)
		{
			questNodeMap[quest].completeQuest(playerInfo);
		}

		public void acceptQuest(Quest quest, PlayerInfo playerInfo)
		{
			questNodeMap[quest].acceptQuest(playerInfo);
		}

		public void colleteQuestAndPreQuestsRecursively(Quest questToComplete)
		{
			questNodeMap[questToComplete].completeQuestAndRequiredQuests();
		}

		public List<QuestNode> getRootNodes()
		{
			return rootNodes;
		}

		public void setQuestStatesFromCompletedQuestIds(PlayerInfo playerInfo, List<int> completedQuestIdsFromSave)
		{
			while (completedQuestIdsFromSave.Count != 0)
			{
				QuestNode questNodeTop = questNodeMap[questIdMap[completedQuestIdsFromSave[0]]];
				List<int> completedQuestsIds = questNodeTop.completeThisAndAllPriors(playerInfo);
				//System.out.println("Completed quests which ids should be removed: " + completedQuestsIds);
				foreach (int completedId in completedQuestsIds)
				{
					bool wasRemoved = false;
					foreach (int comIdsSave in completedQuestIdsFromSave)
					{
						if (completedId == comIdsSave)
						{
							if (!completedQuestIdsFromSave.Remove(comIdsSave))
							{
								throw new ArgumentException("Quest id was not found, and not removed!");
							}
							wasRemoved = true;
							break;
						}
					}

					if (!wasRemoved)
					{
						throw new ArgumentException("This should not be possible!! Removing a quest from the list of ids which were used to find that quest.");
					}
				}
			}

			//recheck for quests that should be active
			recheckQuestRequirements(playerInfo);
		}

		public List<Quest> getQuestsWithState(Quest.QuestState state)
		{
			List<Quest> foundQuests = new List<Quest>();
			foreach (Quest quest in questIdMap.Values)
			{
				if (quest.state == state)
				{
					foundQuests.Add(quest);
				}
			}

			return foundQuests;
		}

		public int getTotalNumberOfQuests()
		{
			return questIdMap.Count;
		}
	}
}
