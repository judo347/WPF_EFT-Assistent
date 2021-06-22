using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFTApp.Model.Tools.DataTools
{
	//TODO add parse quests
	class JSONParserHelper
	{
		private readonly string questsDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\quests.json");
		private readonly string saveDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\saveData.json");

		//private static final String dataFolderPath = "src\\dk\\data"; //TODO remove
		//private static final String questsDataPostfix = "\\quests.json"; //TODO remove
		//private static final String saveDataPostfix = "\\saveData.json"; //TODO remove

		private static readonly int numberOfSaveSlots = 3;

		class QuestMasterJson
		{
			public List<Quest> quests;
		}

		public List<Quest> getAllQuestsFromFile()
		{
			QuestMasterJson parsedQuests = JsonConvert.DeserializeObject<QuestMasterJson>(File.ReadAllText(questsDataPath));
			return parsedQuests.quests;
		}

		public bool SaveData(int saveSlotId, List<Quest> completedQuests, PlayerInfo playerInfo)
		{
			throw new NotImplementedException();
			/*
			if (saveSlotId > numberOfSaveSlots - 1)
				return false;

			JSONObject rootNodeSavesJSON = getJSONFromFile(saveDataPostfix);
			JSONArray saveSlots = rootNodeSavesJSON.getJSONArray("saves");
			JSONObject saveSlotJSONObject = saveSlots.getJSONObject(saveSlotId);
			JSONObject playerInfoJSONObject = saveSlotJSONObject.getJSONObject("player_info");
			JSONArray completedQuestIdsJSONArray = saveSlotJSONObject.getJSONArray("quests_completed");
			JSONObject loyaltyLevelJSONObject = playerInfoJSONObject.getJSONObject("loyaltyLevel");

			while (completedQuestIdsJSONArray.length() > 0)
				completedQuestIdsJSONArray.remove(0);
			foreach (Quest quest in completedQuests)
			{
				completedQuestIdsJSONArray.put(quest.id);
			}

			playerInfoJSONObject.put("player_level", playerInfo.getPlayerLevel());

			foreach (TraderType traderType in TraderType.Values)
			{
				loyaltyLevelJSONObject.put(traderType.getName().toLowerCase(), playerInfo.getLoyaltyLevelFromTrader(traderType));
			}

			return saveJSONToSaveDataFile(rootNodeSavesJSON);
			*/
		}

		public SaveData loadSlot(int slotNumber)
		{
			throw new NotImplementedException();
			/*
			JSONObject rootNodeSavesJSON = getJSONFromFile(saveDataPostfix);
			JSONArray saveSlots = rootNodeSavesJSON.getJSONArray("saves");

			if (slotNumber > saveSlots.length())
				throw new ArgumentException("Trying to load non-existing slot. Given slot number too high");

			JSONObject saveSlotJSONObject = saveSlots.getJSONObject(slotNumber);
			JSONObject playerInfoJSONObject = saveSlotJSONObject.getJSONObject("player_info");

			SaveData loadedData = new SaveData();
			loadedData.completedQuestIds = parseCompletedQuestIds(saveSlotJSONObject);
			loadedData.playerInfo = parsePlayerInfo(playerInfoJSONObject);

			return loadedData;
			*/
		}

		//private bool saveJSONToSaveDataFile(JSONObject rootNode)
		private bool saveJSONToSaveDataFile(string rootNode)
		{
			throw new NotImplementedException();
			/*
			try
			{
				String canonicalPath = new File(dataFolderPath).getAbsolutePath();
				File file = new File(canonicalPath + saveDataPostfix);
				FileUtils.writeStringToFile(file, rootNode.toString(), false);

				//Convert string to JSON object
				return true;

			}
			catch (IOException e)
			{
				Console.WriteLine(e.Message);
			}

			return false;
			*/
		}
	}
}
