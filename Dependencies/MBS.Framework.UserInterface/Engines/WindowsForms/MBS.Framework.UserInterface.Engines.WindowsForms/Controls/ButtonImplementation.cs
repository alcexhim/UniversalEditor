//
//  ButtonImplementation.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(Button))]
	public class ButtonImplementation : WindowsFormsNativeImplementation, IButtonControlImplementation
	{
		public ButtonImplementation(Engine engine, Button control) : base(engine, control)
		{
		}

		public RelativePosition GetImagePosition()
		{
			System.Windows.Forms.Button btn = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.Button);
			if (btn == null)
				return RelativePosition.Default;

			switch (btn.TextImageRelation)
			{
				case System.Windows.Forms.TextImageRelation.ImageAboveText: return RelativePosition.Top;
				case System.Windows.Forms.TextImageRelation.ImageBeforeText: return RelativePosition.Left;
				case System.Windows.Forms.TextImageRelation.Overlay: return RelativePosition.Overlay;
				case System.Windows.Forms.TextImageRelation.TextAboveImage: return RelativePosition.Bottom;
				case System.Windows.Forms.TextImageRelation.TextBeforeImage: return RelativePosition.Right;
			}
			return RelativePosition.Default;
		}

		public void SetImagePosition(RelativePosition value)
		{
			System.Windows.Forms.Button btn = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.Button);
			if (btn == null)
				return;

			switch (value)
			{
				case RelativePosition.Top: btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText; break;
				case RelativePosition.Left: btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText; break;
				case RelativePosition.Overlay: btn.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay; break;
				case RelativePosition.Bottom: btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage; break;
				case RelativePosition.Right: btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage; break;
			}
			return;
		}

		protected override void SetControlTextInternal(Control control, string text)
		{
			if (!Control.IsCreated)
				return;

			Button button = (control as Button);
			System.Windows.Forms.Button btn = (Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Button;

			if (button.StockType != StockType.None)
			{
				btn.Text = Engine.StockTypeToLabel(button.StockType).Replace('_', '&');
			}
			else
			{
				base.SetControlTextInternal(control, text);
			}
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Button button = (control as Button);
			System.Windows.Forms.Button btn = new System.Windows.Forms.Button();

			if (button.StockType != StockType.None)
			{
				btn.Text = Engine.StockTypeToLabel(button.StockType).Replace('_', '&');
			}
			else
			{
				btn.Text = button.Text?.Replace('_', '&');
			}
			btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			btn.AutoSize = true;

			WindowsFormsNativeControl nc = new WindowsFormsNativeControl(btn);
			return nc;
		}
	}
}
