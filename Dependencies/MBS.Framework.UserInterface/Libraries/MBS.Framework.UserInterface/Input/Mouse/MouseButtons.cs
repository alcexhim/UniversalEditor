using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Input.Mouse
{
	/// <summary>
	/// Specifies constants that define which mouse button was pressed.
	/// </summary>
	[Flags()]
    public enum MouseButtons
    {
        None = 0,
		/// <summary>
		/// For a right-handed mouse setup, the left mouse button. For a left-handed mouse setup, the right mouse button.
		/// </summary>
        Primary = 1,
		/// <summary>
		/// The wheel or middle mouse button.
		/// </summary>
        Wheel = 2,
		/// <summary>
		/// For a right-handed mouse setup, the right mouse button. For a left-handed mouse setup, the left mouse button.
		/// </summary>
        Secondary = 4,
		/// <summary>
		/// The first additional mouse button on a mouse with more than three buttons.
		/// </summary>
        XButton1 = 8,
		/// <summary>
		/// The second additional mouse button on a mouse with more than three buttons.
		/// </summary>
		XButton2 = 16
    }
}
