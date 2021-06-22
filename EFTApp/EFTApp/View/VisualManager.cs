using EFTApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTApp.View
{
	/** Before named: PrimarySceenController or rootController */
	public class VisualManager
	{
		public MainModel mainModel { get; set; }
		public VisualQuestCategoryManager qcm { get; set; }

		private SortingMode currentSortingMode = SortingMode.MAP;
		private bool isInGodMode = true;

		public void setModelAndAStage(MainModel mainModel)
		{
			this.mainModel = mainModel;
			this.qcm = new VisualQuestCategoryManager(this);
			this.qcm.reloadSorting(currentSortingMode);
			/* TODO implement
			reloadPlayerInfoVisuals();
			setMenuItemSortingStatus();
			setMenuItemGodmodeStatus();*/
		}

		/** Reloads all visuals related to quests.
		Should be called each time changes to quests is made in the model. */
		public void reloadQuestVisuals()
		{
			// Clears quest boxes
			qcm.clearQuestCategoryBoxes(currentSortingMode);

			// Re-adds all active quests
			foreach(Quest quest in mainModel.getQmt().getActiveQuests())
			{
				if (quest == null)
				{ //TODO: Bug, workaround
					continue;
				}

				createAndAddQuestVisualCard(quest);
			}

			//Should locked quests be shown?
			if (isInGodMode)
			{ //Locked quests are currently not shown as ghost quests
				foreach (Quest quest in mainModel.getQmt().getLockedQuests())
				{
					MapType mapType;

					if (quest.maps.Count == 1)
						mapType = quest.maps[0];
					else
						mapType = MapType.Mixed;

					qcm.addQuestCard(new VisualQuestCard(quest), currentSortingMode, mapType, quest.trader);
				}
			}

			// Updates quests completed label
			//setQuestCompletionLabel(); //TODO implement!

			//qcm.hideEmptyRows(currentSortingMode); //TODO implement!

		}

		private void createAndAddQuestVisualCard(Quest quest)
		{
			List<VisualQuestCard> generatedQuestCards = new List<VisualQuestCard>();
			if(currentSortingMode == SortingMode.TRADER)
			{
				MapType mapType;
				if (quest.maps.Count == 1)
				{
					mapType = quest.maps[0];
				}
				else
				{
					mapType = MapType.Mixed;
				}

				qcm.addQuestCard(new VisualQuestCard(quest), currentSortingMode, mapType, quest.trader);
			} else if(currentSortingMode == SortingMode.MAP)
			{
				if (quest.maps.Count == 0)
				{
					qcm.addQuestCard(new VisualQuestCard(quest), currentSortingMode, MapType.Mixed, quest.trader);
				}
				else if (quest.maps.Count == 1)
				{
					qcm.addQuestCard(new VisualQuestCard(quest), currentSortingMode, quest.maps[0], quest.trader);
				}
				else
				{
					//Ghost quests: quests with multiple maps are showed in each row of the maps
					foreach (MapType mapType in quest.maps)
					{
						VisualQuestCard card = new VisualQuestCard(quest);
						card.isGhost = true;
						qcm.addQuestCard(card, currentSortingMode, mapType, quest.trader);
					}
				}
			} else
			{
				throw new Exception("New sorting mode has been added but not handled.");
			}
		}
	}
}
