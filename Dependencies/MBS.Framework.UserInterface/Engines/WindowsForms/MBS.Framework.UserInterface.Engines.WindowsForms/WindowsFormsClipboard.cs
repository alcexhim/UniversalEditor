//
//  WindowsFormsClipboard.cs
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
namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public class WindowsFormsClipboard : Clipboard
	{
		protected override void ClearInternal()
		{
			System.Windows.Forms.Clipboard.Clear();
		}
		protected override bool ContainsTextInternal()
		{
			return System.Windows.Forms.Clipboard.ContainsText();
		}
		protected override bool ContainsFileListInternal()
		{
			return System.Windows.Forms.Clipboard.ContainsFileDropList();
		}
		protected override string GetTextInternal()
		{
			return System.Windows.Forms.Clipboard.GetText();
		}
		protected override void SetTextInternal(string value)
		{
			System.Windows.Forms.Clipboard.SetText(value);
		}
		protected override object GetDataInternal(string format)
		{
			return System.Windows.Forms.Clipboard.GetData(format);
		}

		protected override CrossThreadData GetContentInternal()
		{
			System.Windows.Forms.IDataObject ido = System.Windows.Forms.Clipboard.GetDataObject();
			string[] formats = ido.GetFormats();

			CrossThreadData data = new CrossThreadData();
			foreach (string format in formats)
			{
				// data.SetData(format, ido.GetData(format));
			}
			return data;
		}
	}
}
