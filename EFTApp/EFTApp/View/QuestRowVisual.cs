namespace EFTApp.View
{
	public class QuestRowVisual
	{
		public string Temp;

		public QuestRowVisual(string temp)
		{
			Temp = temp;
		}

		public override string ToString()
		{
			return Temp;
		}
	}
}
