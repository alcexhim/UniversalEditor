using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;
using System.Diagnostics;

namespace UniversalEditor
{
	[DebuggerNonUserCode()]
	public abstract class Accessor : References<AccessorReference>
	{
		public virtual AccessorReference MakeReference()
		{
			return new AccessorReference(this.GetType());
		}

		public Accessor()
		{
			mvarReader = new Reader(this);
			mvarWriter = new Writer(this);
		}

		public abstract long Length { get; set; }

		protected abstract long GetPosition();
		public virtual long Position { get { return GetPosition(); } set { Seek(value, SeekOrigin.Begin); } }

		public long Remaining
		{
			get
			{
				long r = Length - Position;
				if (r <= 0) return 0;
				return r;
			}
		}

		public void Seek(int length, SeekOrigin position)
		{
			Seek((long)length, position);
		}
		public abstract void Seek(long length, SeekOrigin position);

		protected internal abstract int ReadInternal(byte[] buffer, int start, int count);
		protected internal abstract int WriteInternal(byte[] buffer, int start, int count);

		internal virtual void FlushInternal()
		{
		}

		private bool mvarIsOpen = false;
		public bool IsOpen { get { return mvarIsOpen; } protected set { mvarIsOpen = value; } }

		public void Open()
		{
			if (mvarIsOpen) return;

			OpenInternal();
			mvarIsOpen = true;
		}
		public void Close()
		{
			if (!mvarIsOpen) return;

			CloseInternal();
			mvarIsOpen = false;
		}

		protected abstract void OpenInternal();
		protected abstract void CloseInternal();

		private Encoding mvarDefaultEncoding = Encoding.Default;
		/// <summary>
		/// The default <see cref="Encoding" /> to use when reading and writing strings.
		/// </summary>
		public Encoding DefaultEncoding { get { return mvarDefaultEncoding; } set { mvarDefaultEncoding = value; } }

		private Reader mvarReader = null;
		public Reader Reader { get { return mvarReader; } }
		private Writer mvarWriter = null;
		public Writer Writer { get { return mvarWriter; } }

		public virtual string Title
		{
			get { return String.Empty; }
		}

		public virtual string GetFileTitle()
		{
			return String.Empty;
		}
		public virtual string GetFileName()
		{
			return String.Empty;
		}
	}
}
