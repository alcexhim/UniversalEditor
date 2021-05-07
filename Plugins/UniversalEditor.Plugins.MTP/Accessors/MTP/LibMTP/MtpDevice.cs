//  **************************************************************************
//    MtpDevice.cs
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
	public delegate int ProgressFunction(ulong sent, ulong total, IntPtr data);

	public class MtpDevice : IDisposable
	{
		internal static IntPtr Offset(IntPtr ptr, int offset)
		{
			if (IntPtr.Size == 8)
			{
				return (IntPtr)(ptr.ToInt64() + offset);
			}
			else
			{
				return (IntPtr)(ptr.ToInt32() + offset);
			}
		}

		internal MtpDeviceHandle Handle;
		private MtpDeviceStruct device;
		private string name;
		private Folder albumFolder;
		private Folder musicFolder;
		private Folder organizerFolder;
		private Folder pictureFolder;
		private Folder playlistFolder;
		private Folder podcastFolder;
		private Folder textFolder;
		private Folder videoFolder;

		static MtpDevice()
		{
			LIBMTP_Init();
		}

		public int BatteryLevel
		{
			get
			{
				ushort level, maxLevel;
				GetBatteryLevel(Handle, out maxLevel, out level);
				return (int)((level * 100.0) / maxLevel);
			}
		}

		public string SerialNumber
		{
			get { return GetSerialnumber(Handle); }
		}

		public string Version
		{
			get { return GetDeviceversion(Handle); }
		}

		public string ModelName
		{
			get; private set;
		}

		public string Name
		{
			get { return name; }
			set
			{
				if (SetFriendlyName(Handle, value))
				{
					name = value;
				}
			}
		}

		public Folder AlbumFolder
		{
			get { return albumFolder; }
		}

		public Folder MusicFolder
		{
			get { return musicFolder; }
		}

		public Folder OrganizerFolder
		{
			get { return organizerFolder; }
		}

		public Folder PictureFolder
		{
			get { return pictureFolder; }
		}

		public Folder PlaylistFolder
		{
			get { return playlistFolder; }
		}

		public Folder PodcastFolder
		{
			get { return podcastFolder; }
		}

		public Folder TextFolder
		{
			get { return textFolder; }
		}

		public Folder VideoFolder
		{
			get { return videoFolder; }
		}

		internal MtpDevice(MtpDeviceHandle handle, MtpDeviceStruct device)
		{
			this.device = device;
			this.Handle = handle;
			this.name = GetFriendlyName(Handle);
			this.ModelName = GetModelName(Handle);
			SetDefaultFolders();
		}

		internal MtpDevice(IntPtr handle, bool ownsHandle, MtpDeviceStruct device)
			: this(new MtpDeviceHandle(handle, ownsHandle), device)
		{

		}

		/// <summary>
		/// This function scans the top level directories and stores the relevant ones so they are readily
		/// accessible
		/// </summary>
		private void SetDefaultFolders()
		{
			List<Folder> folders = GetRootFolders();

			foreach (Folder f in folders)
			{
				if (f.FolderId == this.device.default_album_folder)
					albumFolder = f;
				else if (f.FolderId == device.default_music_folder)
				{
					musicFolder = f;
				}
				else if (f.FolderId == device.default_organizer_folder)
					organizerFolder = f;
				else if (f.FolderId == device.default_picture_folder)
					pictureFolder = f;
				else if (f.FolderId == device.default_playlist_folder)
					playlistFolder = f;
				else if (f.FolderId == device.default_text_folder)
					textFolder = f;
				else if (f.FolderId == device.default_video_folder)
					videoFolder = f;
				else if (f.FolderId == device.default_zencast_folder)
					podcastFolder = f;
			}

			// Fix for devices that don't have an explicit playlist folder (see BGO #590342 and #733883)
			if (playlistFolder == null && musicFolder != null)
			{
				playlistFolder = musicFolder;
			}
		}

		public void Dispose()
		{
			if (!Handle.IsClosed)
				Handle.Close();
		}

		public List<Folder> GetRootFolders()
		{
			return Folder.GetRootFolders(this);
		}

		public List<Track> GetAllTracks()
		{
			return GetAllTracks(null);
		}

		public List<Track> GetAllTracks(ProgressFunction callback)
		{
			IntPtr ptr = Track.GetTrackListing(Handle, callback, IntPtr.Zero);

			List<Track> tracks = new List<Track>();

			while (ptr != IntPtr.Zero)
			{
				// Destroy the struct after we use it to avoid potential referencing of freed memory.
				TrackStruct track = (TrackStruct)Marshal.PtrToStructure(ptr, typeof(TrackStruct));
				tracks.Add(new Track(track, this));
				Track.DestroyTrack(ptr);
				ptr = track.next;
			}

			return tracks;
		}

		public List<Playlist> GetPlaylists()
		{
			return Playlist.GetPlaylists(this);
		}

		public List<Album> GetAlbums()
		{
			return Album.GetAlbums(this);
		}

		public List<DeviceStorage> GetStorage()
		{
			List<DeviceStorage> storages = new List<DeviceStorage>();
			IntPtr ptr = device.storage;
			while (ptr != IntPtr.Zero)
			{
				DeviceStorage storage = (DeviceStorage)Marshal.PtrToStructure(ptr, typeof(DeviceStorage));
				storages.Add(storage);
				ptr = storage.Next;
			}
			return storages;
		}

		public void Remove(Track track)
		{
			DeleteObject(Handle, track.FileId);
		}

		public void UploadTrack(string path, Track track, Folder folder)
		{
			UploadTrack(path, track, folder, null);
		}

		public void UploadTrack(string path, Track track, Folder folder, ProgressFunction callback)
		{
			if (string.IsNullOrEmpty(path))
				throw new ArgumentNullException("path");
			if (track == null)
				throw new ArgumentNullException("track");

			folder = folder ?? MusicFolder;
			if (folder != null)
			{
				track.trackStruct.parent_id = folder.FolderId;
			}

			// We send the trackstruct by ref so that when the file_id gets filled in, our copy is updated
			Track.SendTrack(Handle, path, ref track.trackStruct, callback, IntPtr.Zero);
			// LibMtp.GetStorage (Handle, 0);
		}

		public FileType[] GetFileTypes()
		{
			Int16[] ints = GetFileTypes(Handle);
			FileType[] file_types = new FileType[ints.Length];
			for (int i = 0; i < ints.Length; i++)
			{
				file_types[i] = (FileType)ints[i];
			}

			return file_types;
		}

		public static MtpDevice Connect(RawMtpDevice rawDevice)
		{
			var raw = rawDevice.RawDevice;
			IntPtr device = LIBMTP_Open_Raw_Device(raw);
			if (device == IntPtr.Zero)
				return null;
			return new MtpDevice(new MtpDeviceHandle(device, true), (MtpDeviceStruct)Marshal.PtrToStructure(device, typeof(MtpDeviceStruct)));
		}

		public static List<RawMtpDevice> Detect()
		{
			int count = 0;
			IntPtr ptr = IntPtr.Zero;
			LIBMTP_Detect_Raw_Devices(ref ptr, ref count);

			List<RawMtpDevice> devices = new List<RawMtpDevice>();
			for (int i = 0; i < count; i++)
			{
				IntPtr offset = Offset(ptr, i * Marshal.SizeOf(typeof(RawDeviceStruct)));
				RawDeviceStruct d = (RawDeviceStruct)Marshal.PtrToStructure(offset, typeof(RawDeviceStruct));
				devices.Add(new RawMtpDevice(d));
			}

			return devices;
		}

		internal static void ClearErrorStack(MtpDeviceHandle handle)
		{
			LIBMTP_Clear_Errorstack(handle);
		}

		internal static void DeleteObject(MtpDeviceHandle handle, uint object_id)
		{
			if (LIBMTP_Delete_Object(handle, object_id) != 0)
			{
				LibMtpException.CheckErrorStack(handle);
				throw new LibMtpException(ErrorCode.General, "Could not delete the track");
			}
		}

		internal static void GetBatteryLevel(MtpDeviceHandle handle, out ushort maxLevel, out ushort currentLevel)
		{
			int result = LIBMTP_Get_Batterylevel(handle, out maxLevel, out currentLevel);
			if (result != 0)
			{
				LibMtpException.CheckErrorStack(handle);
				throw new LibMtpException(ErrorCode.General, "Could not retrieve battery stats");
			}
		}

		internal static void GetConnectedDevices(out IntPtr list)
		{
			Error.CheckError(LIBMTP_Get_Connected_Devices(out list));
		}

		internal static IntPtr GetErrorStack(MtpDeviceHandle handle)
		{
			return LIBMTP_Get_Errorstack(handle);
		}

		internal static string GetDeviceversion(MtpDeviceHandle handle)
		{
			IntPtr ptr = LIBMTP_Get_Deviceversion(handle);
			if (ptr == IntPtr.Zero)
				return null;

			return StringFromIntPtr(ptr);
		}


		internal static string GetFriendlyName(MtpDeviceHandle handle)
		{
			IntPtr ptr = LIBMTP_Get_Friendlyname(handle);
			if (ptr == IntPtr.Zero)
				return null;

			return StringFromIntPtr(ptr);
		}

		internal static bool SetFriendlyName(MtpDeviceHandle handle, string name)
		{
			bool success = LIBMTP_Set_Friendlyname(handle, name) == 0;
			return success;
		}

		internal static string GetModelName(MtpDeviceHandle handle)
		{
			IntPtr ptr = LIBMTP_Get_Modelname(handle);
			if (ptr == IntPtr.Zero)
				return null;

			return StringFromIntPtr(ptr);
		}

		internal static string GetSerialnumber(MtpDeviceHandle handle)
		{
			IntPtr ptr = LIBMTP_Get_Serialnumber(handle);
			if (ptr == IntPtr.Zero)
				return null;

			return StringFromIntPtr(ptr);
		}

		internal static void GetStorage(MtpDeviceHandle handle, int sortMode)
		{
			LIBMTP_Get_Storage(handle, sortMode);
		}

		internal static Int16[] GetFileTypes(MtpDeviceHandle handle)
		{
			IntPtr types = IntPtr.Zero;
			ushort count = 0;
			if (LIBMTP_Get_Supported_Filetypes(handle, ref types, ref count) == 0)
			{
				Int16[] type_ary = new Int16[count];
				Marshal.Copy(types, type_ary, 0, (int)count);
				Marshal.FreeHGlobal(types);
				return type_ary;
			}

			return new Int16[0];
		}

		internal static void ReleaseDevice(IntPtr handle)
		{
			LIBMTP_Release_Device(handle);
		}

		private static string StringFromIntPtr(IntPtr ptr)
		{
			int i = 0;
			while (Marshal.ReadByte(ptr, i) != (byte)0) ++i;
			byte[] s_buf = new byte[i];
			Marshal.Copy(ptr, s_buf, 0, s_buf.Length);
			string s = System.Text.Encoding.UTF8.GetString(s_buf);
			Marshal.FreeCoTaskMem(ptr);
			return s;
		}

		internal const string LibMtpLibrary = "libmtp.dll";

		// Device Management
		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern void LIBMTP_Init();

		// Clears out the error stack and frees any allocated memory.
		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern void LIBMTP_Clear_Errorstack(MtpDeviceHandle handle);

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int LIBMTP_Delete_Object(MtpDeviceHandle handle, uint object_id);

		// Gets the first connected device:
		//[DllImport (LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern IntPtr LIBMTP_Get_First_Device (); // LIBMTP_mtpdevice_t *

		// Gets the storage information
		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Get_Storage(MtpDeviceHandle handle, int sortMode);

		// Formats the supplied storage device attached to the device
		//[DllImport (LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern int LIBMTP_Format_Storage (MtpDeviceHandle handle, ref DeviceStorage storage);

		// Counts the devices in the list
		//[DllImport (LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern uint LIBMTP_Number_Devices_In_List (MtpDeviceHandle handle);

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern ErrorCode LIBMTP_Get_Connected_Devices(out IntPtr list); //LIBMTP_mtpdevice_t **

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern ErrorCode LIBMTP_Detect_Raw_Devices(ref IntPtr list, ref int count); //LIBMTP_raw_device_t

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Open_Raw_Device(RawDeviceStruct rawdevice);

		// Deallocates the memory for the device
		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern void LIBMTP_Release_Device(IntPtr device);

		//[DllImport (LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern int LIBMTP_Reset_Device (MtpDeviceHandle handle);

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Get_Batterylevel(MtpDeviceHandle handle, out ushort maxLevel, out ushort currentLevel);

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Modelname(MtpDeviceHandle handle); // char *

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Serialnumber(MtpDeviceHandle handle); // char *

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Deviceversion(MtpDeviceHandle handle); // char *

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Friendlyname(MtpDeviceHandle handle); // char *

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Set_Friendlyname(MtpDeviceHandle handle, string name);

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Errorstack(MtpDeviceHandle handle); // LIBMTP_error_t *

		[DllImport(LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern int LIBMTP_Get_Supported_Filetypes(MtpDeviceHandle handle, ref IntPtr types, ref ushort count); // uint16_t **const

		public static string GetMimeTypeFor(FileType type)
		{
			switch (type)
			{
				case FileType.MP3: return "audio/mpeg";
				case FileType.OGG: return "audio/ogg";
				case FileType.WMA: return "audio/x-ms-wma";
				case FileType.WMV: return "video/x-ms-wmv";
				case FileType.ASF: return "video/x-ms-asf";
				case FileType.AAC: return "audio/x-aac";
				case FileType.MP4: return "video/mp4";
				case FileType.AVI: return "video/avi";
				case FileType.WAV: return "audio/x-wav";
				case FileType.MPEG: return "video/mpeg";
				case FileType.FLAC: return "audio/flac";
				case FileType.QT: return "video/quicktime";
				case FileType.M4A: return "audio/mp4";
			}
			return null;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct DeviceEntry
	{
		[MarshalAs(UnmanagedType.LPStr)] public string vendor;
		public ushort vendor_id;
		[MarshalAs(UnmanagedType.LPStr)] public string product;
		public ushort product_id;
		public uint device_flags;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DeviceStorage
	{
		public uint Id;
		public ushort StorageType;
		public ushort FileSystemType;
		public ushort AccessCapability;
		public ulong MaxCapacity;
		public ulong FreeSpaceInBytes;
		public ulong FreeSpaceInObjects;
		[MarshalAs(UnmanagedType.LPStr)] public string StorageDescription;
		[MarshalAs(UnmanagedType.LPStr)] public string VolumeIdentifier;
		public IntPtr Next; // LIBMTP_devicestorage_t*
		public IntPtr Prev; // LIBMTP_devicestorage_t*
	}

	internal class MtpDeviceHandle : SafeHandle
	{
		private MtpDeviceHandle()
			: base(IntPtr.Zero, true)
		{

		}

		internal MtpDeviceHandle(IntPtr ptr, bool ownsHandle)
			: base(IntPtr.Zero, ownsHandle)
		{
			SetHandle(ptr);
		}

		public override bool IsInvalid
		{
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle()
		{
			MtpDevice.ReleaseDevice(handle);
			return true;
		}
	}

	internal struct MtpDeviceStruct
	{
		public byte object_bitsize;
		public IntPtr parameters;  // void*
		public IntPtr usbinfo;     // void*
		public IntPtr storage;     // LIBMTP_devicestorage_t*
		public IntPtr errorstack;  // LIBMTP_error_t*
		public byte maximum_battery_level;
		public uint default_music_folder;
		public uint default_playlist_folder;
		public uint default_picture_folder;
		public uint default_video_folder;
		public uint default_organizer_folder;
		public uint default_zencast_folder;
		public uint default_album_folder;
		public uint default_text_folder;
		public IntPtr cd; // void*
		public IntPtr next; // LIBMTP_mtpdevice_t*
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct RawDeviceStruct
	{
		public DeviceEntry device_entry; /**< The device entry for this raw device */
		public uint bus_location; /**< Location of the bus, if device available */
		public byte devnum; /**< Device number on the bus, if device available */
	}

	public class RawMtpDevice
	{

		public uint BusNumber
		{
			get { return RawDevice.bus_location; }
		}

		public int DeviceNumber
		{
			get { return RawDevice.devnum; }
		}

		internal RawDeviceStruct RawDevice
		{
			get; private set;
		}

		internal RawMtpDevice(RawDeviceStruct device)
		{
			RawDevice = device;
		}
	}
}
