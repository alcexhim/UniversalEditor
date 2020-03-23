using System;

namespace MBS.Framework.UserInterface
{
	public enum NotificationIconStatus
	{
		/// <summary>
		/// The notification icon should not be shown to the user.
		/// </summary>
		Hidden = 0,
		/// <summary>
		/// The notification icon should be shown in its default state.
		/// </summary>
		Visible = 1,
		/// <summary>
		/// The notification icon should be shown with its attention icon.
		/// </summary>
		Attention = 2
	}
	public class NotificationIcon
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarIconNameDefault = String.Empty;
		/// <summary>
		/// The icon name of the stock icon to display in this <see cref="NotificationIcon" />. Certain toolkits
		/// (such as Ubuntu's libappindicator) require a stock icon rather than a custom icon to be displayed. Stock
		/// icons must be installed to the system's icon repository (on Ubuntu, this is /usr/share/icons ).
		/// </summary>
		/// <value>The name of the icon.</value>
		public string IconNameDefault { get { return mvarIconNameDefault; } set { mvarIconNameDefault = value; Application.Engine.UpdateNotificationIcon (this);} }

		private string mvarIconNameAttention = String.Empty;
		/// <summary>
		/// The icon name of the stock icon to display in this <see cref="NotificationIcon" />. Certain toolkits
		/// (such as Ubuntu's libappindicator) require a stock icon rather than a custom icon to be displayed. Stock
		/// icons must be installed to the system's icon repository (on Ubuntu, this is /usr/share/icons ).
		/// </summary>
		/// <value>The name of the icon.</value>
		public string IconNameAttention { get { return mvarIconNameAttention; } set { mvarIconNameAttention = value; Application.Engine.UpdateNotificationIcon (this); } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; Application.Engine.UpdateNotificationIcon (this); } }

		private NotificationIconStatus mvarStatus = NotificationIconStatus.Hidden;
		public NotificationIconStatus Status { get { return mvarStatus; } set { mvarStatus = value; Application.Engine.UpdateNotificationIcon (this); } }

		private Menu mvarContextMenu = null;
		public Menu ContextMenu {
			get { return mvarContextMenu; }
			set {
				mvarContextMenu = value;
				Application.Engine.UpdateNotificationIcon (this, true);
			}
		}

	}
}

