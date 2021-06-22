using System.Collections.Generic;

namespace EFTApp.Model
{
	class VisualizerQuestNode
	{
		public int x;
		public int y;
		public int layer;
		public Quest quest;
		public List<QuestNode> followingQuests = new List<QuestNode>();

		public VisualizerQuestNode(int layer, Quest quest, List<QuestNode> followingQuests)
		{
			this.layer = layer;
			this.quest = quest;
			this.followingQuests = followingQuests;
		}
	}
}
