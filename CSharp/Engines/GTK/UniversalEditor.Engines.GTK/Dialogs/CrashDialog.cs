//  
//  CrashDialog.cs
//  
//  Author:
//       beckermj <${AuthorEmail}>
// 
//  Copyright (c) 2014 beckermj
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
namespace UniversalEditor.Engines.GTK.Dialogs
{
	public partial class CrashDialog : Gtk.Dialog
	{
		public CrashDialog()
		{
			this.Build();
		}
		
		private Exception mvarException = null;
		public Exception Exception
		{
			get { return mvarException; }
			set
			{
				mvarException = value;
				
				if (mvarException == null)
				{
					txtException.Text = String.Empty;
					txtMessage.Buffer.Text = String.Empty;
					txtSource.Text = String.Empty;
					txtStackTrace.Buffer.Text = String.Empty;
				}
				else
				{
					txtException.Text = mvarException.GetType().Name;
					txtMessage.Buffer.Text = mvarException.Message;
					txtSource.Text = mvarException.Source;
					txtStackTrace.Buffer.Text = mvarException.StackTrace;
				}
			}
		}
	}
}

