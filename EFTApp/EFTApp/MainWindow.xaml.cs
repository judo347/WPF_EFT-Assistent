using EFTApp.Model;
using EFTApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static EFTApp.Model.Quest;

namespace EFTApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string windowName { get; } = "EFT: Quest Assistant"; //TODO use

		private MainModel mainModel;
		public VisualManager visualManager { get; set; }

		public ObservableCollection<VisualQuestRow> currentShownRows { get; set; } = new ObservableCollection<VisualQuestRow>();
		private bool showOnlyActiveQuests = true;

		public MainWindow()
		{
			visualManager = new VisualManager();
			mainModel = new MainModel();
			visualManager.setModelAndAStage(mainModel, this);
			//mainModel.loadSlot(0); //TODO
			InitializeComponent();
			updateLevels();

			DataContext = this;
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

			reloadMainWindowsQuests();
		}

		private void Label_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var sp = (StackPanel)sender;
			var visualQuestCard = (VisualQuestCard)sp.DataContext;
			Console.WriteLine(visualQuestCard.quest.name);

			QuestState state = visualQuestCard.quest.state;
			if (state == QuestState.AVAILABLE)
			{
				//Quest is accepted
				mainModel.acceptQuest(visualQuestCard.quest);
				visualManager.reloadQuestVisuals();
			}
			else if (state == QuestState.ACCEPTED)
			{
				//Quest is completed
				mainModel.completeQuest(visualQuestCard.quest);
				visualManager.reloadQuestVisuals();
			}
			else if (state == QuestState.LOCKED)
			{
				//Nothing happens unless we are in god-mode. TODO?
				Console.WriteLine("Clicked locked quest!");
			}
			else
				throw new Exception("Should not happen!");

			reloadMainWindowsQuests();
		}

		private void reloadMainWindowsQuests()
		{
			uiRowsListBox.ItemsSource = currentShownRows;
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
