using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;
using System.Diagnostics;

namespace UniversalEditor
{
	/// <summary>
	/// Provides the ability to access a <see cref="DataFormat" />-agnostic stream. <see cref="CustomOption" />s for
	/// import and export may be used to allow the interactive user to specify <see cref="Accessor" />-specific
	/// parameters.
	/// </summary>
	[DebuggerNonUserCode()]
	public abstract class Accessor : References<AccessorReference>
	{
		/// <summary>
		/// Creates or returns an existing <see cref="ReferencedBy" /> object referring to this <see cref="References" /> object.
		/// </summary>
		/// <returns>A <see cref="ReferencedBy" /> object that can be used to create additional instances of this <see cref="References" /> object.</returns>
		public AccessorReference MakeReference()
		{
			AccessorReference ar = MakeReferenceInternal();
			return ar;
		}
		protected virtual AccessorReference MakeReferenceInternal()
		{
			return new AccessorReference(this.GetType());
		}

		/// <summary>
		/// Creates a new instance of this <see cref="Accessor" /> and initializes the associated
		/// <see cref="Reader" /> and <see cref="Writer" />.
		/// </summary>
		public Accessor()
		{
			mvarReader = new Reader(this);
			mvarWriter = new Writer(this);
		}

		/// <summary>
		/// Gets or sets the length of the data being accessed, if possible.
		/// </summary>
		public abstract long Length { get; set; }

		/// <summary>
		/// Gets the position within the underlying stream, if possible.
		/// </summary>
		/// <returns>The position within the underlying stream</returns>
		protected abstract long GetPosition();
		/// <summary>
		/// Gets or sets the position within the underlying stream, if possible.
		/// </summary>
		public virtual long Position { get { return GetPosition(); } set { Seek(value, SeekOrigin.Begin); } }

		/// <summary>
		/// Determines how many bytes are remaining to be read from the underlying stream, if possible.
		/// </summary>
		public long Remaining
		{
			get
			{
				long r = Length - Position;
				if (r <= 0) return 0;
				return r;
			}
		}

		/// <summary>
		/// Sets the position within the underlying stream based on the given relative position and origin.
		/// </summary>
		/// <param name="offset">The location within the underlying stream relative to the given <see cref="SeekOrigin" />.</param>
		/// <param name="origin">The reference point from where to seek.</param>
		public void Seek(int offset, SeekOrigin origin)
		{
			Seek((long)offset, origin);
		}
		/// <summary>
		/// Sets the position within the underlying stream based on the given relative position and origin.
		/// </summary>
		/// <param name="offset">The location within the underlying stream relative to the given <see cref="SeekOrigin" />.</param>
		/// <param name="origin">The reference point from where to seek.</param>
		public abstract void Seek(long offset, SeekOrigin origin);

		/// <summary>
		/// Reads the specified amount of bytes from the underlying stream into the specified buffer starting at the
		/// specified offset.
		/// </summary>
		/// <param name="buffer">The buffer into which to read data.</param>
		/// <param name="start">The position in the buffer from which to start copying data.</param>
		/// <param name="count">The number of bytes to read from the underlying stream.</param>
		/// <returns>
		/// The number of bytes actually read from the underlying stream. This may differ from the requested amount
		/// due to encountering the end of stream or a network connection error.
		/// </returns>
		protected internal abstract int ReadInternal(byte[] buffer, int start, int count);
		/// <summary>
		/// Writes the specified amount of bytes to the underlying stream from the specified buffer starting at the
		/// specified offset.
		/// </summary>
		/// <param name="buffer">The buffer from which to write data.</param>
		/// <param name="start">The position in the buffer from which to start copying data.</param>
		/// <param name="count">The number of bytes to write to the underlying stream.</param>
		/// <returns>
		/// The number of bytes actually written to the underlying stream. This may differ from the requested amount
		/// due to lack of disk space or a network connection error.
		/// </returns>
		protected internal abstract int WriteInternal(byte[] buffer, int start, int count);

		/// <summary>
		/// Clears all buffers for this <see cref="Accessor" /> and causes any buffered data to be written to the underlying device.
		/// </summary>
		public void Flush()
		{
			FlushInternal();
		}
		protected virtual void FlushInternal()
		{
		}

		private bool mvarIsOpen = false;
		/// <summary>
		/// Determines whether this <see cref="Accessor" /> is currently open.
		/// </summary>
		public bool IsOpen { get { return mvarIsOpen; } protected set { mvarIsOpen = value; } }

		/// <summary>
		/// Opens this <see cref="Accessor" />.
		/// </summary>
		public void Open()
		{
			if (mvarIsOpen) return;

			OpenInternal();
			mvarIsOpen = true;
		}
		/// <summary>
		/// Closes this <see cref="Accessor" />, making the underlying stream available to be re-opened by another <see cref="Accessor" />.
		/// </summary>
		public void Close()
		{
			if (!mvarIsOpen) return;

			CloseInternal();
			mvarIsOpen = false;
		}

		/// <summary>
		/// The internal implementation of the <see cref="Open" /> command.
		/// </summary>
		protected abstract void OpenInternal();
		/// <summary>
		/// The internal implementation of the <see cref="Close" /> command.
		/// </summary>
		protected abstract void CloseInternal();

		private Encoding mvarDefaultEncoding = Encoding.Default;
		/// <summary>
		/// The default <see cref="Encoding" /> to use when reading and writing strings.
		/// </summary>
		public Encoding DefaultEncoding { get { return mvarDefaultEncoding; } set { mvarDefaultEncoding = value; } }

		private Reader mvarReader = null;
		/// <summary>
		/// A <see cref="Reader" /> which reads from the underlying stream of this <see cref="Accessor" />.
		/// </summary>
		public Reader Reader { get { return mvarReader; } }
		private Writer mvarWriter = null;
		/// <summary>
		/// A <see cref="Reader" /> which writes to the underlying stream of this <see cref="Accessor" />.
		/// </summary>
		public Writer Writer { get { return mvarWriter; } }

		/// <summary>
		/// The title of this <see cref="Accessor" />.
		/// </summary>
		public virtual string Title
		{
			get { return String.Empty; }
		}
		/// <summary>
		/// Gets the file name without path information for this <see cref="Accessor" />, if applicable.
		/// </summary>
		/// <returns>A <see cref="String" /> containing the file name without path information for this <see cref="Accessor" />, if applicable.</returns>
		public virtual string GetFileTitle()
		{
			return String.Empty;
		}
		/// <summary>
		/// Gets the file name including path information for this <see cref="Accessor" />, if applicable.
		/// </summary>
		/// <returns>A <see cref="String" /> containing the file name including path information for this <see cref="Accessor" />, if applicable.</returns>
		public virtual string GetFileName()
		{
			return String.Empty;
		}

		#region Position Saving and Loading
		private Stack<long> _positions = new Stack<long>();
		[DebuggerNonUserCode()]
		public void SavePosition()
		{
			_positions.Push(this.Position);
		}
		[DebuggerNonUserCode()]
		public void LoadPosition()
		{
			if (_positions.Count > 0) this.Position = _positions.Pop();
		}
		public void ClearLastPosition()
		{
			if (_positions.Count > 0) _positions.Pop();
		}
		public void ClearAllPositions()
		{
			if (_positions.Count > 0) _positions.Clear();
		}
		#endregion
	}
}
