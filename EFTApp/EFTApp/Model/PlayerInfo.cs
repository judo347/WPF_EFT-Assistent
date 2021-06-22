using System;

namespace EFTApp.Model
{
	public class PlayerInfo
	{
		private int playerLevel = 1;
		private int praporLoyaltyLevel = 1;
		private int therapistLoyaltyLevel = 1;
		private int skierLoyaltyLevel = 1;
		private int peacekeeperLoyaltyLevel = 1;
		private int mechanicLoyaltyLevel = 1;
		private int ragmanLoyaltyLevel = 1;
		private int jaegerLoyaltyLevel = 1;
		private int fenceLoyaltyLevel = 1;

		public PlayerInfo()
		{
		}

		public PlayerInfo(int playerLevel)
		{
			this.playerLevel = playerLevel;
		}

		public int getPlayerLevel()
		{
			return playerLevel;
		}

		public void setPlayerLevel(int playerLevel)
		{
			this.playerLevel = playerLevel;
		}

		public int getLoyaltyLevelFromTrader(TraderType traderType)
		{
			switch (traderType)
			{
				case TraderType.Mechanic: return mechanicLoyaltyLevel;
				case TraderType.Skier: return skierLoyaltyLevel;
				case TraderType.Fence: return fenceLoyaltyLevel;
				case TraderType.Jaeger: return jaegerLoyaltyLevel;
				case TraderType.Prapor: return praporLoyaltyLevel;
				case TraderType.Ragman: return ragmanLoyaltyLevel;
				case TraderType.Therapist: return therapistLoyaltyLevel;
				case TraderType.Peacekeeper: return peacekeeperLoyaltyLevel;
				default: throw new ArgumentException();
			}
		}

		public void incrementLoyaltyLevel(TraderType traderType)
		{
			switch (traderType)
			{
				case TraderType.Mechanic:
					mechanicLoyaltyLevel++;
					break;
				case TraderType.Skier:
					skierLoyaltyLevel++;
					break;
				case TraderType.Fence:
					fenceLoyaltyLevel++;
					break;
				case TraderType.Jaeger:
					jaegerLoyaltyLevel++;
					break;
				case TraderType.Prapor:
					praporLoyaltyLevel++;
					break;
				case TraderType.Ragman:
					ragmanLoyaltyLevel++;
					break;
				case TraderType.Therapist:
					therapistLoyaltyLevel++;
					break;
				case TraderType.Peacekeeper:
					peacekeeperLoyaltyLevel++;
					break;
				default:
					throw new ArgumentException();
			}
		}

		public void setLoyaltyLevel(TraderType traderType, int desiredLevel)
		{
			switch (traderType)
			{
				case TraderType.Mechanic:
					mechanicLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Skier:
					skierLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Fence:
					fenceLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Jaeger:
					jaegerLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Prapor:
					praporLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Ragman:
					ragmanLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Therapist:
					therapistLoyaltyLevel = desiredLevel;
					break;
				case TraderType.Peacekeeper:
					peacekeeperLoyaltyLevel = desiredLevel;
					break;
				default:
					throw new ArgumentException();
			}
		}

		public void incrementPlayerLevel()
		{
			playerLevel++;
		}

		public void reload(PlayerInfo playerInfo)
		{
			this.playerLevel = playerInfo.playerLevel;
			this.peacekeeperLoyaltyLevel = playerInfo.peacekeeperLoyaltyLevel;
			this.therapistLoyaltyLevel = playerInfo.therapistLoyaltyLevel;
			this.ragmanLoyaltyLevel = playerInfo.ragmanLoyaltyLevel;
			this.fenceLoyaltyLevel = playerInfo.fenceLoyaltyLevel;
			this.praporLoyaltyLevel = playerInfo.praporLoyaltyLevel;
			this.jaegerLoyaltyLevel = playerInfo.jaegerLoyaltyLevel;
			this.skierLoyaltyLevel = playerInfo.skierLoyaltyLevel;
			this.mechanicLoyaltyLevel = playerInfo.mechanicLoyaltyLevel;
		}
	}
}
