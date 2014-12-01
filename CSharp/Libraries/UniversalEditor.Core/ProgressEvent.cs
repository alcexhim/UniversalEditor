using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
