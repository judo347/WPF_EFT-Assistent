using EFTApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTApp.View
{
	public enum SortingMode
	{
		MAP, TRADER
	}

	public class VisualQuestCategoryManager
	{
		private VisualManager visualManager { get; set; }

		private Dictionary<TraderType, VisualQuestRow> categoryRows_trader = new Dictionary<TraderType, VisualQuestRow>();
		private Dictionary<MapType, VisualQuestRow> categoryRows_map = new Dictionary<MapType, VisualQuestRow>();

		private MainWindow mainWindow { get; set; }

		public VisualQuestCategoryManager(VisualManager visualManager, MainWindow mainWindow)
		{
			this.mainWindow = mainWindow;
			this.visualManager = visualManager;
		}

		public void reloadSorting(SortingMode sortingMode)
		{
			mainWindow.currentShownRows = new List<VisualQuestRow>();
			setupCategoryBoxes(sortingMode);
			visualManager.reloadQuestVisuals();
		}

		//TODO implement other methods

		public void addQuestCard(VisualQuestCard card, SortingMode sortingMode, MapType mapType, TraderType traderType)
		{
			//HBox desiredHBox;

			if (sortingMode == SortingMode.MAP)
			{
				categoryRows_map[mapType].addQuestCard(card);
			}
			else if (sortingMode == SortingMode.TRADER)
			{
				categoryRows_trader[traderType].addQuestCard(card);
			}
			else
			{
				throw new Exception("A new sorting map has been implemented but not handled!");
			}
		}

		private void setupCategoryBoxes(SortingMode sortingMode)
		{
			clearQuestRows();

			if (sortingMode == SortingMode.TRADER)
			{
				var traderValues = Enum.GetValues(typeof(TraderType));
				foreach (TraderType trader in traderValues)
				{
					//TODO set image for current row
					categoryRows_trader.Add(trader, new VisualQuestRow(trader));
				}

				mainWindow.currentShownRows = new List<VisualQuestRow>();
				foreach (KeyValuePair<TraderType, VisualQuestRow> kvp in categoryRows_trader)
				{
					mainWindow.currentShownRows.Add(kvp.Value);
				}
			}
			else if (sortingMode == SortingMode.MAP)
			{
				var mapValues = Enum.GetValues(typeof(MapType));
				foreach (MapType map in mapValues)
				{
					categoryRows_map.Add(map, new VisualQuestRow(map));
				}

				mainWindow.currentShownRows = new List<VisualQuestRow>();
				foreach (KeyValuePair<MapType, VisualQuestRow> kvp in categoryRows_map)
				{
					mainWindow.currentShownRows.Add(kvp.Value);
				}
			}
			else
			{
				throw new Exception("A new sorting map has been implemented!");
			}
		}

		private void clearQuestRows()
		{
			categoryRows_trader = new Dictionary<TraderType, VisualQuestRow>();
			categoryRows_map = new Dictionary<MapType, VisualQuestRow>();
		}

		//Previously known as clearHboxs
		public void clearQuestCategoryBoxes(SortingMode sortingMode)
		{
			if(sortingMode == SortingMode.MAP)
			{
				var mapValues = Enum.GetValues(typeof(MapType));
				foreach (MapType map in mapValues)
				{
					categoryRows_map[map].clearQuestsList();
				}
			} else if(sortingMode == SortingMode.TRADER)
			{
				var traderValues = Enum.GetValues(typeof(TraderType));
				foreach (TraderType trader in traderValues)
				{
					categoryRows_trader[trader].clearQuestsList();
				}

			} else
			{
				throw new Exception("A new sorting mode has been added but not handled!");
			}
		}
	}
}
