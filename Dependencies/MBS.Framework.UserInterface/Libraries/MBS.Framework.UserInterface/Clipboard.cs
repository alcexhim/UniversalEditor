//
//  Clipboard.cs
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
using System.Threading.Tasks;

namespace MBS.Framework.UserInterface
{
	public abstract class Clipboard
	{
		private static Clipboard _Default = null;
		public static Clipboard Default
		{
			get
			{
				if (_Default == null)
				{
					_Default = Application.Engine.GetDefaultClipboard();
				}
				return _Default;
			}
		}

		protected abstract void ClearInternal();
		public void Clear()
		{
			ClearInternal();
		}

		protected abstract bool ContainsFileListInternal();
		public bool ContainsFileList { get { return ContainsFileListInternal(); } }

		protected abstract bool ContainsTextInternal();
		public bool ContainsText { get { return ContainsTextInternal(); } }

		protected abstract void SetTextInternal(string value);
		public void SetText(string value)
		{
			SetTextInternal(value);
		}

		protected abstract string GetTextInternal();
		public string GetText()
		{
			return GetTextInternal();
		}

		protected abstract object GetDataInternal(string format);
		public object GetData(string format)
		{
			return GetDataInternal(format);
		}

		protected abstract CrossThreadData GetContentInternal();
		public CrossThreadData GetContent()
		{
			return GetContentInternal();
		}
	}
}
