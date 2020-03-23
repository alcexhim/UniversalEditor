using System;
namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GDL
{
	public static class Constants
	{
		[Flags()]
		public enum GdlDockItemBehavior
		{
			/// <summary>
			/// Normal dock item
			/// </summary>
			BEH_NORMAL,
			/// <summary>
			/// item cannot be undocked
			/// </summary>
			BEH_NEVER_FLOATING,
			/// <summary>
			/// item cannot be docked vertically
			/// </summary>
			BEH_NEVER_VERTICAL,
			/// <summary>
			/// item cannot be docked horizontally
			/// </summary>
			BEH_NEVER_HORIZONTAL,
			/// <summary>
			/// item is locked, it cannot be moved around
			/// </summary>
			BEH_LOCKED,
			/// <summary>
			/// item cannot be docked at top
			/// </summary>
			BEH_CANT_DOCK_TOP,
			/// <summary>
			/// item cannot be docked at bottom
			/// </summary>
			BEH_CANT_DOCK_BOTTOM,
			/// <summary>
			/// item cannot be docked left
			/// </summary>
			BEH_CANT_DOCK_LEFT,
			/// <summary>
			/// item cannot be docked right
			/// </summary>
			BEH_CANT_DOCK_RIGHT,
			/// <summary>
			/// item cannot be docked at center
			/// </summary>
			BEH_CANT_DOCK_CENTER,
			/// <summary>
			/// item cannot be closed
			/// </summary>
			BEH_CANT_CLOSE,
			/// <summary>
			/// item cannot be iconified
			/// </summary>
			BEH_CANT_ICONIFY,
			/// <summary>
			/// item doesn't have a grip
			/// </summary>
			BEH_NO_GRIP
		}
		public enum GdlDockPlacement
		{
			/// <summary>
			/// No position defined
			/// </summary>
			GDL_DOCK_NONE = 0,
			/// <summary>
			/// Dock object on the top
			/// </summary>
			GDL_DOCK_TOP = 1,
			/// <summary>
			/// Dock object on the bottom
			/// </summary>
			GDL_DOCK_BOTTOM = 2,
			/// <summary>
			/// Dock object on the right
			/// </summary>
			GDL_DOCK_RIGHT = 3,
			/// <summary>
			/// Dock object on the left
			/// </summary>
			GDL_DOCK_LEFT = 4,
			/// <summary>
			/// Dock object on top of the other
			/// </summary>
			GDL_DOCK_CENTER = 5,
			/// <summary>
			/// Dock object in its own window
			/// </summary>
			GDL_DOCK_FLOATING = 6
		}
	}
}
