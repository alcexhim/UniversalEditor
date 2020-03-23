using System;
namespace MBS.Framework.UserInterface.Controls.Docking
{
	[Flags()]
	public enum DockingItemBehavior
	{
		/// <summary>
		/// Normal dock item
		/// </summary>
		Normal = 0,
		/// <summary>
		/// item cannot be undocked
		/// </summary>
		NeverFloating = 1,
		/// <summary>
		/// item cannot be docked vertically
		/// </summary>
		NeverVertical = 2,
		/// <summary>
		/// item cannot be docked horizontally
		/// </summary>
		NeverHorizontal = 4,
		/// <summary>
		/// item is locked, it cannot be moved around
		/// </summary>
		Locked = 8,
		/// <summary>
		/// item cannot be docked at top
		/// </summary>
		NoDockTop = 16,
		/// <summary>
		/// item cannot be docked at bottom
		/// </summary>
		NoDockBottom = 32,
		/// <summary>
		/// item cannot be docked left
		/// </summary>
		NoDockLeft = 64,
		/// <summary>
		/// item cannot be docked right
		/// </summary>
		NoDockRight = 128,
		/// <summary>
		/// item cannot be docked at center
		/// </summary>
		NoDockCenter = 256,
		/// <summary>
		/// item cannot be closed
		/// </summary>
		NoClose = 512,
		/// <summary>
		/// item cannot be iconified
		/// </summary>
		NoMinimize = 1024,
		/// <summary>
		/// item doesn't have a grip
		/// </summary>
		NoGrip = 2048
	}
}
