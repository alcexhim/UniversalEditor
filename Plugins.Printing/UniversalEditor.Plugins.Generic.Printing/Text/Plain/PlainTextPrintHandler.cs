//
//  PlainTextPrintHandler.cs - provides a PrintHandler for printing plain text documents
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

using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.Printing;
using MBS.Framework.UserInterface.Drawing;

namespace UniversalEditor.Plugins.Generic.Printing.Text.Plain
{
	/// <summary>
	/// Provides a <see cref="PrintHandler" /> for printing plain text documents.
	/// </summary>
	public class PlainTextPrintHandler : PrintHandler
	{
		private static PrintHandlerReference _phr = null;
		protected override PrintHandlerReference MakeReferenceInternal()
		{
			if (_phr == null)
			{
				_phr = base.MakeReferenceInternal();
				_phr.SupportedObjectModels.Add(typeof(PlainTextObjectModel));
			}
			return _phr;
		}

		protected override void PrintInternal(ObjectModel objectModel, Graphics g)
		{
			PlainTextObjectModel text = (objectModel as PlainTextObjectModel);
			if (text == null)
				throw new ObjectModelNotSupportedException();

			g.DrawText(text.Text, Font.FromFamily("Liberation Serif", 12), new Rectangle(64, 64, 1400, 1400), Brushes.Black);
		}
	}
}
