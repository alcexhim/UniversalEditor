/***************************************************************************
 *  Track.cs
 *
 *  Copyright (C) 2006-2007 Alan McGovern
 *  Authors:
 *  Alan McGovern (alan.mcgovern@gmail.com)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),
 *  to deal in the Software without restriction, including without limitation
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,
 *  and/or sell copies of the Software, and to permit persons to whom the
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;

namespace Mtp
{
	public class Track
	{
		internal TrackStruct trackStruct;
		private MtpDevice device;

		public uint FileId
		{
			get { return trackStruct.item_id; }
		}

		public string Album
		{
			get { return trackStruct.album; }
			set { trackStruct.album = value; }
		}

		public string Artist
		{
			get { return trackStruct.artist; }
			set { trackStruct.artist = value; }
		}

		public uint Bitrate
		{
			get { return trackStruct.bitrate; }
		}

		public ushort BitrateType
		{
			get { return trackStruct.bitratetype; }
		}

		public string ReleaseDate
		{
			get { return trackStruct.date; }
			set { trackStruct.date = value; }
		}

		public int Year
		{
			get { return ReleaseDate == null || ReleaseDate.Length < 4 ? 0 : Int32.Parse(ReleaseDate.Substring(0, 4)); }
			set { ReleaseDate = String.Format("{0:0000}0101T0000.00", value); }
		}

		public uint Duration
		{
			get { return trackStruct.duration; }
			set { trackStruct.duration = value; }
		}

		public string FileName
		{
			get { return trackStruct.filename; }
			set { trackStruct.filename = value; }
		}

		public ulong FileSize
		{
			get { return trackStruct.filesize; }
			set { trackStruct.filesize = value; }
		}

		public FileType FileType
		{
			get { return trackStruct.filetype; }
			set { trackStruct.filetype = value; }
		}

		public string Genre
		{
			get { return trackStruct.genre; }
			set { trackStruct.genre = value; }
		}

		public ushort NoChannels
		{
			get { return trackStruct.nochannels; }
			set { trackStruct.nochannels = value; }
		}

		// 0 to 100
		public ushort Rating
		{
			get { return trackStruct.rating; }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("Rating", "Rating must be between zero and 100");
				trackStruct.rating = value;
			}
		}

		public uint SampleRate
		{
			get { return trackStruct.samplerate; }
			set { trackStruct.samplerate = value; }
		}

		public string Title
		{
			get { return trackStruct.title; }
			set { trackStruct.title = value; }
		}

		public ushort TrackNumber
		{
			get { return trackStruct.tracknumber; }
			set { trackStruct.tracknumber = value; }
		}

		public uint WaveCodec
		{
			get { return trackStruct.wavecodec; }
		}

		public uint UseCount
		{
			get { return trackStruct.usecount; }
			set { trackStruct.usecount = value; }
		}

		public string Composer
		{
			get { return trackStruct.composer; }
			set { trackStruct.composer = value; }
		}

		public Track(string filename, ulong filesize) : this(filename, filesize, null)
		{
		}

		public Track(string filename, ulong filesize, MtpDevice device) : this(new TrackStruct(), device)
		{
			this.trackStruct.filename = filename;
			this.trackStruct.filesize = filesize;
			this.trackStruct.filetype = DetectFileType(this);
		}

		internal Track(TrackStruct track, MtpDevice device)
		{
			this.device = device;
			this.trackStruct = track;
		}

		public bool InFolder(Folder folder)
		{
			return InFolder(folder, false);
		}

		public bool InFolder(Folder folder, bool recursive)
		{
			if (folder == null)
			{
				return false;
			}

			bool is_parent = trackStruct.parent_id == folder.FolderId;
			if (is_parent || !recursive)
			{
				return is_parent;
			}

			return Folder.Find(device, trackStruct.parent_id).HasAncestor(folder);
		}

		public void Download(string path)
		{
			Download(path, null);
		}

		public void Download(string path, ProgressFunction callback)
		{
			if (String.IsNullOrEmpty(path))
				throw new ArgumentException("Cannot be null or empty", "path");

			GetTrack(device.Handle, trackStruct.item_id, path, callback, IntPtr.Zero);
		}

		public void UpdateMetadata()
		{
			UpdateTrackMetadata(device.Handle, ref trackStruct);
		}

		private static FileType DetectFileType(Track track)
		{
			string ext = System.IO.Path.GetExtension(track.FileName);

			// Strip leading .
			if (ext.Length > 0)
				ext = ext.Substring(1, ext.Length - 1);

			// this is a hack; catch all m4(a|b|v|p)
			if (ext != null && ext.ToLower().StartsWith("m4"))
				ext = "mp4";

			FileType type = (FileType)Enum.Parse(typeof(FileType), ext, true);
			//if (type == null)
			//    return FileType.UNKNOWN;
			return type;
		}

		internal static void DestroyTrack(IntPtr track)
		{
			LIBMTP_destroy_track_t(track);
		}

		internal static void GetTrack(MtpDeviceHandle handle, uint trackId, string destPath, ProgressFunction callback, IntPtr data)
		{
			if (LIBMTP_Get_Track_To_File(handle, trackId, destPath, callback, data) != 0)
			{
				LibMtpException.CheckErrorStack(handle);
				throw new LibMtpException(ErrorCode.General, "Could not download track from the device");
			}
		}

		internal static IntPtr GetTrackListing(MtpDeviceHandle handle, ProgressFunction function, IntPtr data)
		{
			return LIBMTP_Get_Tracklisting_With_Callback(handle, function, data);
		}

		internal static void SendTrack(MtpDeviceHandle handle, string path, ref TrackStruct metadata, ProgressFunction callback, IntPtr data)
		{
			if (LIBMTP_Send_Track_From_File(handle, path, ref metadata, callback, data) != 0)
			{
				LibMtpException.CheckErrorStack(handle);
				throw new LibMtpException(ErrorCode.General, "Could not upload the track");
			}
		}

		internal static void UpdateTrackMetadata(MtpDeviceHandle handle, ref TrackStruct metadata)
		{
			if (LIBMTP_Update_Track_Metadata(handle, ref metadata) != 0)
				throw new LibMtpException(ErrorCode.General);
		}

		//[DllImport (MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern IntPtr LIBMTP_new_track_t (); // LIBMTP_track_t *

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern void LIBMTP_destroy_track_t(IntPtr track); // LIBMTP_track_t *

		//[DllImport (MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern IntPtr LIBMTP_Get_Tracklisting (MtpDeviceHandle handle); //LIBMTP_track_t *

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Tracklisting_With_Callback(MtpDeviceHandle handle, ProgressFunction callback, IntPtr data); // LIBMTP_track_t *

		//[DllImport (MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern IntPtr LIBMTP_Get_Trackmetadata (MtpDeviceHandle handle, uint trackId); // LIBMTP_track_t *

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Get_Track_To_File(MtpDeviceHandle handle, uint trackId, string path, ProgressFunction callback, IntPtr data);

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Send_Track_From_File(MtpDeviceHandle handle, string path, ref TrackStruct track, ProgressFunction callback, IntPtr data);

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Update_Track_Metadata(MtpDeviceHandle handle, ref TrackStruct metadata);

		//[DllImport (MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern int LIBMTP_Track_Exists (MtpDeviceHandle handle, uint trackId);

		//int     LIBMTP_Get_Track_To_File_Descriptor (MtpDeviceHandle handle, uint trackId, int const, LIBMTP_progressfunc_t const, void const *const)
		//int     LIBMTP_Send_Track_From_File_Descriptor (MtpDeviceHandle handle, int const, LIBMTP_track_t *const, LIBMTP_progressfunc_t const, void const *const, uint32_t const)
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TrackStruct
	{
		public uint item_id;
		public uint parent_id;
		public uint storage_id;

		[MarshalAs(UnmanagedType.LPStr)] public string title;
		[MarshalAs(UnmanagedType.LPStr)] public string artist;
		[MarshalAs(UnmanagedType.LPStr)] public string composer;
		[MarshalAs(UnmanagedType.LPStr)] public string genre;
		[MarshalAs(UnmanagedType.LPStr)] public string album;
		[MarshalAs(UnmanagedType.LPStr)] public string date;
		[MarshalAs(UnmanagedType.LPStr)] public string filename;

		public ushort tracknumber;
		public uint duration;
		public uint samplerate;
		public ushort nochannels;
		public uint wavecodec;
		public uint bitrate;
		public ushort bitratetype;
		public ushort rating;    // 0 -> 100
		public uint usecount;
		public ulong filesize;
#if LIBMTP_SIZEOF_TIME_T_64
        public ulong modificationdate;
#else
		public uint modificationdate;
#endif
		public FileType filetype;
		public IntPtr next; // Track Null if last
							/*
							public Track? Next
							{
								get
								{
									if (next == IntPtr.Zero)
										return null;
									return (Track)Marshal.PtrToStructure(next, typeof(Track));
								}
							}*/
	}
}
