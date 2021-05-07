//  **************************************************************************
//    AbstractTrackList.cs
//
//    Copyright (C) 2008 Novell
//    Copyright (C) 2010 Alan McGovern
//    Authors:
//    Gabriel Burt (gburt@novell.com)
//    Alan McGovern (alan.mcgovern@gmail.com)
//  ***************************************************************************

//  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mtp
{
	public abstract class AbstractTrackList
	{
		private bool saved;
		private List<uint> track_ids;
		private MtpDevice device;

		public abstract uint Count { get; protected set; }
		public abstract string Name { get; set; }
		protected abstract IntPtr TracksPtr { get; set; }

		protected abstract int Create();
		protected abstract int Update();

		public bool Saved { get { return saved; } }
		protected MtpDevice Device { get { return device; } }

		public IList<uint> TrackIds
		{
			get { return track_ids; }
		}

		protected AbstractTrackList(MtpDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}

			this.device = device;

			if (track_ids == null)
			{
				track_ids = new List<uint>();
			}
		}

		internal AbstractTrackList(MtpDevice device, IntPtr tracks, uint count) : this(device)
		{
			this.saved = true;
			this.track_ids = new List<uint>();

			if (tracks != IntPtr.Zero)
			{
				for (int i = 0; i < (int)count; i++)
					track_ids.Add((uint)Marshal.ReadInt32(tracks, sizeof(int) * i));
			}
		}

		public void AddTrack(Track track)
		{
			AddTrack(track.FileId);
		}

		public void AddTrack(uint track_id)
		{
			track_ids.Add(track_id);
			Count++;
		}

		public void RemoveTrack(Track track)
		{
			RemoveTrack(track.FileId);
		}

		public void RemoveTrack(uint track_id)
		{
			track_ids.Remove(track_id);
			Count--;
		}

		public void ClearTracks()
		{
			track_ids.Clear();
			Count = 0;
		}

		public virtual void Save()
		{
			Count = (uint)track_ids.Count;

			if (TracksPtr != IntPtr.Zero)
				throw new InvalidOperationException("TracksPtr must be NULL when Save is called");

			try
			{
				if (Count > 0)
				{
					TracksPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(uint)) * (int)Count);
					for (int i = 0; i < track_ids.Count; i++)
						Marshal.WriteInt32(TracksPtr, i * sizeof(int), (int)track_ids[i]);
				}

				if (saved)
				{
					saved = Update() == 0;
				}
				else
				{
					saved = Create() == 0;
				}
			}
			finally
			{
				if (TracksPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(TracksPtr);
					TracksPtr = IntPtr.Zero;
				}
			}
		}
	}
}
