using System;
using System.Collections.Generic;

namespace EFTApp.Model
{
	class QuestNode
	{
		private List<QuestNode> requiredQuests = new List<QuestNode>();
		private List<QuestNode> followingQuests = new List<QuestNode>();
		private Quest quest;

		public QuestNode(Quest quest)
		{
			this.quest = quest;
		}

		public void AddRequiredQuestNode(QuestNode node)
		{
			requiredQuests.Add(node);
		}

		/** Check if this quest can be active. If so, the following quests is also called. */
		public void initialQuestStateActiveCheck(PlayerInfo playerInfo)
		{
			//Is required quests completed?
			foreach (QuestNode questNode in requiredQuests)
			{
				if (questNode.quest.state != Quest.QuestState.COMPLETED)
				{
					return;
				}
			}

			bool canBeActive = quest.setStateActive(playerInfo);
			if (canBeActive)
			{
				foreach (QuestNode questNode in followingQuests)
				{
					questNode.initialQuestStateActiveCheck(playerInfo);
				}
			}
		}

		/** */
		public void reEvalLockedQuests(PlayerInfo playerInfo)
		{
			if (quest.state == Quest.QuestState.LOCKED)
			{
				initialQuestStateActiveCheck(playerInfo);
				return;
			}
			else
			{
				foreach (QuestNode questNode in followingQuests)
				{
					questNode.reEvalLockedQuests(playerInfo);
				}
			}
		}

		public void completeQuest(PlayerInfo playerInfo)
		{
			if (quest.state == Quest.QuestState.COMPLETED)
			{
				throw new ArgumentException("This action is not possible!");
			}

			if (quest.state == Quest.QuestState.LOCKED)
			{
				throw new ArgumentException("This action is not possible!");
			}

			quest.state = Quest.QuestState.COMPLETED;
			foreach (QuestNode questNode in followingQuests)
			{
				questNode.initialQuestStateActiveCheck(playerInfo); //TODO rename method?
			}
		}

		public void addSelfToRequiredQuests()
		{
			foreach (QuestNode node in requiredQuests)
			{
				node.addFollowingQuestNode(this);
			}
		}

		public void addFollowingQuestNode(QuestNode node)
		{
			followingQuests.Add(node);
		}

		public int getQuestId()
		{
			return quest.id;
		}

		public void completeQuestAndRequiredQuests()
		{
			foreach (QuestNode reqQuest in requiredQuests)
			{
				reqQuest.completeQuestAndRequiredQuests();
			}

			quest.complete();
		}

		public List<QuestNode> getRootNodes()
		{
			List<QuestNode> previousNodes = new List<QuestNode>();
			foreach (QuestNode reQuest in requiredQuests)
			{
				previousNodes.AddRange(reQuest.getRootNodes());
			}

			if (requiredQuests.Count == 0)
			{
				previousNodes.Add(this);
			}

			return previousNodes;
		}

		public Quest getQuest()
		{
			return quest;
		}

		public List<VisualizerQuestNode> getVisualizerNodes(int currentLayer)
		{
			List<VisualizerQuestNode> visualNodes = new List<VisualizerQuestNode>();
			foreach (QuestNode node in followingQuests)
			{
				visualNodes.AddRange(node.getVisualizerNodes(currentLayer + 1));
			}
			visualNodes.Add(new VisualizerQuestNode(currentLayer, quest, followingQuests));

			return visualNodes;
		}

		public List<QuestNode> getFollowingQuests()
		{
			return followingQuests;
		}

		public List<QuestNode> getAllRequiredQuests()
		{
			List<QuestNode> requiredQuestsCopy = new List<QuestNode>(requiredQuests);
			foreach (QuestNode reqQuestNode in requiredQuests)
			{
				requiredQuestsCopy.AddRange(reqQuestNode.getAllRequiredQuests());
			}

			return requiredQuestsCopy;
		}

		/** Completes this quest and all required for this.
		 * Returns the ids of all quests that has been completed. */
		public List<int> completeThisAndAllPriors(PlayerInfo playerInfo)
		{

			List<int> completedQuests = new List<int>();
			if (quest.state == Quest.QuestState.COMPLETED)
			{
				return completedQuests;
			}

			foreach (QuestNode questNode in requiredQuests)
			{
				completedQuests.AddRange(questNode.completeThisAndAllPriors(playerInfo));
			}

			bool canBeActive = quest.setStateActive(playerInfo);
			if (canBeActive)
			{
				quest.state = Quest.QuestState.COMPLETED;
			}
			else
			{
				throw new ArgumentException("Quest cannot be completed. Corrupt save?");
			}
			completedQuests.Add(this.quest.id);
			return completedQuests;
		}

	}
}
