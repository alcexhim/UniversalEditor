/***************************************************************************
 *  Folder.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mtp
{
	public class Folder
	{
		private MtpDevice device;
		private uint folderId;
		private uint parentId;
		private string name;

		internal uint FolderId
		{
			get { return folderId; }
		}

		public string Name
		{
			get { return name; }
		}

		internal uint ParentId
		{
			get { return parentId; }
		}

		internal Folder(uint folderId, uint parentId, string name, MtpDevice device)
		{
			this.device = device;
			this.folderId = folderId;
			this.parentId = parentId;
			this.name = name;
		}

		internal Folder(FolderStruct folder, MtpDevice device)
					: this(folder.folder_id, folder.parent_id, folder.name, device)
		{

		}

		public Folder AddChild(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			// First create the folder on the device and check for error
			uint id = CreateFolder(device.Handle, name, FolderId);

			FolderStruct f = new FolderStruct();
			f.folder_id = id;
			f.parent_id = FolderId;
			f.name = name;

			return new Folder(f, device);
		}

		public List<Folder> GetChildren()
		{
			IntPtr root = GetFolderList(device.Handle);
			IntPtr ptr = Find(root, folderId);

			FolderStruct f = (FolderStruct)Marshal.PtrToStructure(ptr, typeof(FolderStruct));

			ptr = f.child;
			List<Folder> folders = new List<Folder>();
			while (ptr != IntPtr.Zero)
			{
				FolderStruct folder = (FolderStruct)Marshal.PtrToStructure(ptr, typeof(FolderStruct));
				folders.Add(new Folder(folder, device));
				ptr = folder.sibling;
			}

			LIBMTP_destroy_folder_t(root);
			return folders;
		}

		private Folder CreateParentFolder()
		{
			return Find(device, parentId);
		}

		public bool HasAncestor(Folder ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}

			if (device != ancestor.device)
			{
				throw new ArgumentException("Folders are on different devices");
			}

			bool hasAncestor = false;
			if (parentId != 0)
			{
				if (parentId == ancestor.FolderId)
				{
					hasAncestor = true;
				}
				else
				{
					Folder parent = CreateParentFolder();

					hasAncestor = parent.HasAncestor(ancestor);
				}
			}
			return hasAncestor;
		}

		public void Remove()
		{
			MtpDevice.DeleteObject(device.Handle, FolderId);
		}

		public override string ToString()
		{
			return String.Format("{0} (id {1}, parent id {2})", Name, FolderId, ParentId);
		}

		internal static List<Folder> GetRootFolders(MtpDevice device)
		{
			List<Folder> folders = new List<Folder>();
			IntPtr root = GetFolderList(device.Handle);
			try
			{
				for (IntPtr ptr = root; ptr != IntPtr.Zero;)
				{
					FolderStruct folder = (FolderStruct)Marshal.PtrToStructure(ptr, typeof(FolderStruct));
					folders.Add(new Folder(folder, device));
					ptr = folder.sibling;
				}
			}
			finally
			{
				// Recursively destroy the folder tree
				LIBMTP_destroy_folder_t(root);
			}
			return folders;
		}

		internal static uint CreateFolder(MtpDeviceHandle handle, string name, uint parentId)
		{
			uint result = LIBMTP_Create_Folder(handle, name, parentId, 0);
			if (result == 0)
			{
				LibMtpException.CheckErrorStack(handle);
				throw new LibMtpException(ErrorCode.General, "Could not create folder on the device");
			}

			return result;
		}

		internal static void DestroyFolder(IntPtr folder)
		{
			LIBMTP_destroy_folder_t(folder);
		}

		internal static IntPtr Find(IntPtr folderList, uint folderId)
		{
			return LIBMTP_Find_Folder(folderList, folderId);
		}

		internal static Folder Find(MtpDevice device, uint folderId)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}

			Folder folder = null;
			IntPtr root = GetFolderList(device.Handle);
			try
			{
				IntPtr ptr = Find(root, folderId);
				if (ptr != IntPtr.Zero)
				{
					FolderStruct folderStruct = (FolderStruct)Marshal.PtrToStructure(ptr, typeof(FolderStruct));
					folder = new Folder(folderStruct, device);
				}
			}
			finally
			{
				DestroyFolder(root);
			}
			return folder;
		}

		internal static IntPtr GetFolderList(MtpDeviceHandle handle)
		{
			return LIBMTP_Get_Folder_List(handle);
		}

		// Folder Management
		//[DllImport (MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		//private static extern IntPtr LIBMTP_new_folder_t (); // LIBMTP_folder_t*

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern void LIBMTP_destroy_folder_t(IntPtr folder);

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Get_Folder_List(MtpDeviceHandle handle); // LIBMTP_folder_t*

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr LIBMTP_Find_Folder(IntPtr folderList, uint folderId); // LIBMTP_folder_t*

		[DllImport(MtpDevice.LibMtpLibrary, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint LIBMTP_Create_Folder(MtpDeviceHandle handle, string name, uint parentId, uint storageId);
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct FolderStruct
	{
		public uint folder_id;
		public uint parent_id;
		public uint storage_id;

		[MarshalAs(UnmanagedType.LPStr)] public string name;
		public IntPtr sibling; // LIBMTP_folder_t*
		public IntPtr child;   // LIBMTP_folder_t*
							   /*
							   public object NextSibling
							   {
								   get
								   {
									   if(sibling == IntPtr.Zero)
										   return null;
									   return (FolderStruct)Marshal.PtrToStructure(sibling, typeof(Folder));
								   }
							   }

							   public object NextChild
							   {
								   get
								   {
									   if(child == IntPtr.Zero)
										   return null;
									   return (FolderStruct)Marshal.PtrToStructure(child, typeof(Folder));
								   }
							   }

							   public Folder? Sibling
							   {
								   get
								   {
									   if (sibling == IntPtr.Zero)
										   return null;
									   return (Folder)Marshal.PtrToStructure(sibling, typeof(Folder));
								   }
							   }

							   public Folder? Child
							   {
								   get
								   {
									   if (child == IntPtr.Zero)
										   return null;
									   return (Folder)Marshal.PtrToStructure(child, typeof(Folder));
								   }
							   }*/

		/*public IEnumerable<Folder> Children()
        {
            Folder? current = Child;
            while(current.HasValue)
            {
                yield return current.Value;
                current = current.Value.Child;
            }
        }*/

		/*public IEnumerable<Folder> Siblings()
        {
            Folder? current = Sibling;
            while(current.HasValue)
            {
                yield return current.Value;
                current = current.Value.Sibling;
            }
        }*/
	}
}
