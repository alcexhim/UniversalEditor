using System;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GDK
{
	internal static class Constants
	{
		[Flags()]
		public enum GdkModifierType : uint
		{
			None = 0,
			Shift    = 1 << 0,
			Lock     = 1 << 1,
			Control  = 1 << 2,
			Alt     = 1 << 3, // Mod1
			Mod2     = 1 << 4,
			Mod3     = 1 << 5,
			Mod4     = 1 << 6,
			Mod5     = 1 << 7,
			Button1  = 1 << 8,
			Button2  = 1 << 9,
			Button3  = 1 << 10,
			Button4  = 1 << 11,
			Button5  = 1 << 12,

			ModifierReserved13  = 1 << 13,
			ModifierReserved14  = 1 << 14,
			ModifierReserved15  = 1 << 15,
			ModifierReserved16  = 1 << 16,
			ModifierReserved17  = 1 << 17,
			ModifierReserved18  = 1 << 18,
			ModifierReserved19  = 1 << 19,
			ModifierReserved20  = 1 << 20,
			ModifierReserved21  = 1 << 21,
			ModifierReserved22  = 1 << 22,
			ModifierReserved23  = 1 << 23,
			ModifierReserved24  = 1 << 24,
			ModifierReserved25  = 1 << 25,

			/// <summary>
			/// The next few modifiers are used by XKB, so we skip to the end. Bits 15 - 25 are currently unused. Bit 29 is used internally.
			/// </summary>
			Super    = 1 << 26,
			Hyper    = 1 << 27,
			Meta     = 1 << 28,

			Reserved29  = 1 << 29,

			Release  = 1 << 30,

			/// <summary>
			/// Combination of GDK_SHIFT_MASK..GDK_BUTTON5_MASK + GDK_SUPER_MASK + GDK_HYPER_MASK + GDK_META_MASK + GDK_RELEASE_MASK
			/// </summary>
			GDK_MODIFIER_MASK = 0x5c001fff
		}

		[Flags()]
		public enum GdkDragAction
		{
			/// <summary>
			/// Means nothing, and should not be used.
			/// </summary>
			Default,
			/// <summary>
			/// Copy the data.
			/// </summary>
			Copy,
			/// <summary>
			/// Move the data, i.e. first copy it, then delete it from the source using the DELETE target of the X selection protocol.
			/// </summary>
			Move,
			/// <summary>
			/// Add a link to the data. Note that this is only useful if source and destination agree on what it means.
			/// </summary>
			Link,
			/// <summary>
			/// Special action which tells the source that the destination will do something that the source doesn’t understand.
			/// </summary>
			Private,
			/// <summary>
			/// Ask the user what to do with the data.
			/// </summary>
			Ask
		}

		public enum GdkEventType
		{
			Nothing = -1,
			Delete = 0,
			Destroy = 1,
			Expose = 2,
			MotionNotify = 3,
			ButtonPress = 4,
			DoubleButtonPress = 5,
			TripleButtonPress = 6,
			ButtonRelease = 7,
			KeyPress = 8,
			KeyRelease = 9,
			EnterNotify = 10,
			LeaveNotify = 11,
			FocusChange = 12,
			Configure = 13,
			Map = 14,
			Unmap = 15,
			PropertyNotify = 16,
			SelectionClear = 17,
			SelectionRequest = 18,
			SelectionNotify = 19,
			ProximityIn = 20,
			ProximityOut = 21,
			DragEnter = 22,
			DragLeave = 23,
			DragMotion = 24,
			DragStatus = 25,
			DropStart = 26,
			DropFinished = 27,
			ClientEvent = 28,
			VisibilityNotify = 29,
			Scroll = 31,
			WindowState = 32,
			Setting = 33,
			OwnerChange = 34,
			GrabBroken = 35,
			Damage = 36,
			TouchBegin = 37,
			TouchUpdate = 38,
			TouchEnd = 39,
			TouchCancel = 40,
			TouchpadSwipe = 41,
			TouchpadPinch = 42,
			PadButtonPress = 43,
			PadButtonRelease = 44,
			PadRing = 45,
			PadStrip = 46,
			PadGroupMode = 47,
			Last                /* helper variable for decls */
		}

		public enum GdkGravity
		{
			NorthWest = 1,
			North,
			NorthEast,
			West,
			Center,
			East,
			SouthWest,
			South,
			SouthEast,
			Static
		}

		[Flags()]
		public enum GdkEventMask
		{
			PointerMotion = 0x4,
			PointerMotionHint = 0x8,
			ButtonPress = 0x100,
			ButtonRelease = 0x200,
			KeyPress = 0x400,
			KeyRelease = 0x800
		}

		public enum GdkColorspace
		{
			RGB
		}
	}
}

