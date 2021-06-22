using Newtonsoft.Json;
using System.Collections.Generic;

namespace EFTApp.Model
{
	public class QuestObjectives
	{
		[JsonProperty("obj")]
		public string objective { get; set; }
		[JsonProperty("subs")]
		public List<string> subObjectives { get; set; } = new List<string>();

		public QuestObjectives(string objective, List<string> subObjectives)
		{
			this.objective = objective;
			this.subObjectives = subObjectives;
		}

		public QuestObjectives()
		{
			subObjectives = new List<string>();
		}
	}
}
