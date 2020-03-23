using System;

namespace MBS.Framework.UserInterface
{
	public class NotificationPopup
	{
		private string mvarSummary = String.Empty;
		public string Summary { get { return mvarSummary; } set { mvarSummary = value; } }

		private string mvarContent = String.Empty;
		public string Content { get { return mvarContent; } set { mvarContent = value; } }

		private string mvarIconName = String.Empty;
		public string IconName { get { return mvarIconName; } set { mvarIconName = value; } }

		public void Show()
		{
			Application.Engine.ShowNotificationPopup (this);
		}

		public static void Show(string summary, string content = "", string iconName = "")
		{
			NotificationPopup popup = new NotificationPopup();
			popup.Summary = summary;
			popup.Content = content;
			popup.IconName = iconName;
			popup.Show();
		}
	}
}

