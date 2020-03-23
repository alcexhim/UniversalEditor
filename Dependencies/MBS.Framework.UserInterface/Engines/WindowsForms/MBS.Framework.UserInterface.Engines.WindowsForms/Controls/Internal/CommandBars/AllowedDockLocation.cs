using System;
namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls.Internal.CommandBars
{

	[Flags]
	public enum AllowedDockLocation : byte
	{
		Top = 0x01,
		Left = 0x02,
		Bottom = 0x04,
		Right = 0x08,
		Floating = 0x10
	}
}
