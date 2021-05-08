//
//  ProgressEvent.cs - provide a way to signal progress to UI from non-UI code
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using System.ComponentModel;

namespace UniversalEditor
{
	/// <summary>
	/// A delegate for the event that is raised when progress is made during an asynchronous operation.
	/// </summary>
	/// <param name="sender">The control firing this event.</param>
	/// <param name="e">A <see cref="ProgressEventArgs" /> that contains additional information about this event.</param>
	public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
	/// <summary>
	/// Event arguments for the event that is raised when progress is made during an asynchronous operation.
	/// </summary>
	public class ProgressEventArgs : CancelEventArgs
	{
		private long mvarTotal = 0;
		/// <summary>
		/// The total possible amount of progress represented by this progress event.
		/// </summary>
		public long Total { get { return mvarTotal; } }

		private long mvarCurrent = 0;
		/// <summary>
		/// The current amount of progress completed.
		/// </summary>
		public long Current { get { return mvarCurrent; } }

		/// <summary>
		/// The amount of progress remaining.
		/// </summary>
		public long Remaining { get { return mvarTotal - mvarCurrent; } }

		private string mvarMessage = String.Empty;
		/// <summary>
		/// The progress message
		/// </summary>
		public string Message { get { return mvarMessage; } }

		/// <summary>
		/// Creates a new instance of the <see cref="ProgressEventArgs" /> event arguments for the event that is
		/// raised when progress is made during an asynchronous operation.
		/// </summary>
		/// <param name="current">The current amount of progress completed.</param>
		/// <param name="total">The total possible amount of progress represented by this progress event.</param>
		/// <param name="message">The progress message</param>
		public ProgressEventArgs(long current, long total, string message = "")
		{
			mvarCurrent = current;
			mvarTotal = total;
			mvarMessage = message;
		}
	}
}
