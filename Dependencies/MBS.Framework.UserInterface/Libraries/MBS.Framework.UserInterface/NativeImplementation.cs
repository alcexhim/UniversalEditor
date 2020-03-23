using System;
using System.Collections.Generic;

using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface
{
	/// <summary>
	/// Native implementation for the specified Control.
	/// </summary>
	public abstract class NativeImplementation : ControlImplementation
	{
		public NativeImplementation(Engine engine, Control control) : base(engine, control)
		{
		}
	}
}
