//  **************************************************************************
//    Playlist.cs
//
//    Copyright (C) 2006-2007 Alan McGovern
//    Authors:
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
	public sealed class Playlist : AbstractTrackList
	{
		internal static List<Playlist> GetPlaylists(MtpDevice device)
		{
			List<Playlist> playlists = new List<Playlist>();
			IntPtr ptr = Playlist.LIBMTP_Get_Playlist_List(device.Handle);
			while (ptr != IntPtr.Zero)
			{
				// Destroy the struct *after* we use it to ensure we don't access freed memory
				// for the 'tracks' variable
				PlaylistStruct d = (PlaylistStruct)Marshal.PtrToStructure(ptr, typeof(PlaylistStruct));
				playlists.Add(new Playlist(device, d));
				LIBMTP_destroy_playlist_t(ptr);
				ptr = d.next;
			}
			return playlists;
		}

		private PlaylistStruct playlist;

		public override uint Count
		{
			get { return playlist.no_tracks; }
			protected set { playlist.no_tracks = value; }
		}

		public override string Name
		{
			get { return playlist.Name; }
			set { playlist.Name = value; }
		}

		protected override IntPtr TracksPtr
		{
			get { return playlist.tracks; }
			set { playlist.tracks = value; }
		}

		public Playlist(MtpDevice device, string name) : base(device)
		{
			this.playlist = new PlaylistStruct();
			Name = name;
			Count = 0;
		}

		internal Playlist(MtpDevice device, PlaylistStruct playlist) : base(device, playlist.tracks, playlist.no_tracks)
		{
			// Once we've loaded the tracks, set the TracksPtr to NULL as it
			// will be freed when the Playlist constructor is finished.
			this.playlist = playlist;
			TracksPtr = IntPtr.Zero;
		}

		protected override int Create()
		{
			playlist.parent_id = Device.PlaylistFolder.FolderId;
			return LIBMTP_Create_New_Playlist(Device.Handle, ref playlist);
		}

		protected override int Update()
		{
			return LIBMTP_Update_Playlist(Device.Handle, ref playlist);
		}

		public void Remove()
		{
			MtpDevice.LIBMTP_Delete_Object(Device.Handle, playlist.playlist_id);
		}

		// Playlist Management

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern void LIBMTP_destroy_playlist_t(IntPtr playlist);

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Playlist_List(MtpDeviceHandle handle); // LIBMTP_playlist_t*

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Create_New_Playlist(MtpDeviceHandle handle, ref PlaylistStruct metadata);

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Update_Playlist(MtpDeviceHandle handle, ref PlaylistStruct playlist);
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct PlaylistStruct
	{
		public uint playlist_id;
		public uint parent_id;
		public uint storage_id;

		[MarshalAs(UnmanagedType.LPStr)]
		public string Name;

		public IntPtr tracks; // int*
		public uint no_tracks;
		public IntPtr next;   // LIBMTP_playlist_t*
	}
}
