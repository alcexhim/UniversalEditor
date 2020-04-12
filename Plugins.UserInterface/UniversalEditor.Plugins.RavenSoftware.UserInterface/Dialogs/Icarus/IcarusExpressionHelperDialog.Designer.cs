//
//  IcarusExpressionHelperDialog.Designer.cs - UWT designer initialization for IcarusExpressionHelperDialog
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Dialogs.Icarus
{
	partial class IcarusExpressionHelperDialog
	{
		/// <summary>
		/// UWT designer initialization for <see cref="IcarusExpressionHelperDialog" />.
		/// </summary>
		/// <remarks>
		/// UWT designer initialization in code is deprecated; continue improving the cross-platform Glade XML parser for <see cref="ContainerLayoutAttribute" />!
		/// </remarks>
		private void InitializeComponent()
		{
			this.Buttons.Add(new Button(StockType.OK, cmdOK_Click));
			this.Buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));

			this.Layout = new BoxLayout(Orientation.Horizontal);

			this.Name = "IcarusExpressionHelperDialog";
			this.Text = "Expression Helper";
		}

		protected override void OnCreating(EventArgs e)
		{
			base.OnCreating(e);
		}
	}
}