using System;
namespace MBS.Framework.UserInterface.Theming
{
	[Flags()]
	public enum ControlState
	{
		Normal = 0,
		Hover = 1,
		Pressed = 2,
		Disabled = 4
	}
}
