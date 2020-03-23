using System;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.AppIndicator
{
	internal class Constants
	{
		/// <summary>
		/// The category provides grouping for the indicators so that users can find indicators that are similar
		/// together.
		/// </summary>
		public enum AppIndicatorCategory
		{
			/// <summary>
			/// The indicator is used to display the status of the application.
			/// </summary>
			ApplicationStatus,
			/// <summary>
			/// The application is used for communication with other people.
			/// </summary>
			Communications,
			/// <summary>
			/// A system indicator relating to something in the user's system.
			/// </summary>
			SystemServices,
			/// <summary>
			/// An indicator relating to the user's hardware.
			/// </summary>
			Hardware,
			/// <summary>
			/// Something not defined in this enum, please don't use unless you really need it.
			/// </summary>
			Other
		}

		/// <summary>
		/// These are the states that the indicator can be on in the user's panel. The indicator by default starts
		/// in the state @APP_INDICATOR_STATUS_PASSIVE and can be shown by setting it to @APP_INDICATOR_STATUS_ACTIVE.
		/// </summary>
		public enum AppIndicatorStatus
		{
			/// <summary>
			/// The indicator should not be shown to the user.
			/// </summary>
			Passive,
			/// <summary>
			/// The indicator should be shown in it's default state.
			/// </summary>
			Active,
			/// <summary>
			/// The indicator should show it's attention icon.
			/// </summary>
			Attention
		}
	}
}

