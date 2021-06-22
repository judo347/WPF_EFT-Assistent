using EFTApp.Model;
using EFTApp.View;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace EFTApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainModel mainModel;

		public List<VisualQuestRow> QuestRows { get; set; }
		private bool showOnlyActiveQuests = true;

		public MainWindow()
		{
			mainModel = new MainModel();
			//mainModel.loadSlot(0); //TODO
			InitializeComponent();
			updateLevels();

			initializeRows();

			DataContext = this;
		}

		private void initializeRows()
		{
			QuestRows = new List<VisualQuestRow>();

			var mapValues = Enum.GetValues(typeof(MapType));
			foreach(MapType map in mapValues)
			{
				QuestRows.Add(new VisualQuestRow(map));
			}

			reloadQuestVisuals();
		}

		public void reloadQuestVisuals()
		{
			foreach (Quest quest in mainModel.getQmt().questModel.allQuests)
			{
				if(showOnlyActiveQuests)
				{
					if(quest.state == Quest.QuestState.ACTIVE)
					{
						foreach (MapType mapType in quest.maps)
						{
							foreach (VisualQuestRow row in QuestRows)
							{
								if (row.Map == mapType)
								{
									row.Quests.Add(new VisualQuestCard(quest));
								}
							}
						}
					}

				} else
				{
					foreach (MapType mapType in quest.maps)
					{
						foreach (VisualQuestRow row in QuestRows)
						{
							if (row.Map == mapType)
							{
								row.Quests.Add(new VisualQuestCard(quest));
							}
						}
					}
				}
			}
		}

		/** Updates the level side panel with data from model. */
		private void updateLevels()
		{
			playerLevel.Text = mainModel.getPlayerInfo().getPlayerLevel().ToString();
			praporLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Prapor).ToString();
			therapistLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Therapist).ToString();
			skierLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Skier).ToString();
			peacekeeperLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Peacekeeper).ToString();
			mechanicLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Mechanic).ToString();
			ragmanLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Ragman).ToString();
			jaegerLevel.Text = mainModel.getPlayerInfo().getLoyaltyLevelFromTrader(TraderType.Jaeger).ToString();
		}

		private void Button_Click_Player_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementPlayerLevel();
			updateLevels();
		}

		private void Button_Click_Prapor_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Prapor);
			updateLevels();
		}

		private void Button_Click_Therapist_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Therapist);
			updateLevels();
		}

		private void Button_Click_Skier_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Skier);
			updateLevels();
		}

		private void Button_Click_Peacekeeper_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Peacekeeper);
			updateLevels();
		}

		private void Button_Click_Mechanic_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Mechanic);
			updateLevels();
		}

		private void Button_Click_Ragman_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Ragman);
			updateLevels();
		}

		private void Button_Click_Jaeger_Level_Up(object sender, RoutedEventArgs e)
		{
			mainModel.incrementTraderLoyaltyLevel(TraderType.Jaeger);
			updateLevels();
		}

		private void Button_Click_Exit(object sender, RoutedEventArgs e)
		{
			//mainModel.saveSlot(); //TODO: save to last chosen slot
			System.Windows.Application.Current.Shutdown();
		}
	}
}
