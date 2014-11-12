using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UniversalEditor.Accessors
{
	public class HTTPAccessor : Accessor
	{
		private static AccessorReference _ar = null;
		public override AccessorReference MakeReference()
		{
			if (_ar == null)
			{
				_ar = base.MakeReference();
				_ar.Title = "Internet via HyperText Transfer Protocol (HTTP)";
				_ar.ImportOptions.Add(new CustomOptionText("FileName", "&File name: "));
			}
			return _ar;
		}

		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private System.Net.HttpWebRequest http = null;
		private System.IO.Stream mvarStream = null;

		public override long Length
		{
			get
			{
				if (mvarStream == null) throw new InvalidOperationException("Please open before looking");
				return mvarStream.Length;
			}
			set
			{
				if (mvarStream == null) throw new InvalidOperationException("Please open before looking");
				mvarStream.SetLength(value);
			}
		}

		protected override long GetPosition()
		{
			if (mvarStream == null) throw new InvalidOperationException("Please open before looking");
			return mvarStream.Position;
		}

		public override void Seek(long length, IO.SeekOrigin position)
		{
			System.IO.SeekOrigin position1 = System.IO.SeekOrigin.Begin;
			switch (position)
			{
				case IO.SeekOrigin.Begin: position1 = System.IO.SeekOrigin.Begin; break;
				case IO.SeekOrigin.Current: position1 = System.IO.SeekOrigin.Current; break;
				case IO.SeekOrigin.End: position1 = System.IO.SeekOrigin.End; break;
			}
			mvarStream.Seek(length, position1);
		}

		protected override void OpenInternal()
		{
			if (http != null) throw new InvalidOperationException("Please close before opening");
			http = (HttpWebRequest)WebRequest.Create(mvarFileName);
			WebResponse resp = http.GetResponse();
			mvarStream = resp.GetResponseStream();
		}

		protected override void CloseInternal()
		{
			if (http == null) throw new InvalidOperationException("Please open before closing");
			http.Abort();
			if (mvarStream != null) mvarStream.Close();
			mvarStream = null;
			http = null;
		}

		protected override int WriteInternal(byte[] buffer, int start, int count)
		{
			if (mvarStream == null) throw new InvalidOperationException("Please open before writing");
			mvarStream.Write(buffer, start, count);
			return count;
		}
		protected override int ReadInternal(byte[] buffer, int start, int count)
		{
			if (mvarStream == null) throw new InvalidOperationException("Please open before reading");
			return mvarStream.Read(buffer, start, count);
		}
	}
}
