using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor
{
	public static class ReaderExtensions
	{
		const UInt16 NullTag = 0;
		const UInt16 NewClassTag = 0xffff;
		const UInt16 ClassTag = 0x8000;
		const UInt32 BigClassTag = 0x80000000;
		const UInt16 BigObjectTag = 0x7fff;
		const UInt32 MaxMapCountTag = 0x3ffffffe;

		private static MfcClassRegistry _classRegistry;
		private static List<MfcClass> _loadedClasses;
		private static List<MfcObject> _loadedObjects;

		static ReaderExtensions()
		{
			_classRegistry = new MfcClassRegistry();
			_loadedClasses = new List<MfcClass>();
			_loadedObjects = new List<MfcObject>();

			// Class index zero isn't used/represents an error
			_loadedClasses.Add(null);

			// Object index zero isn't used/represents a null pointer
			_loadedObjects.Add(null);

			_classRegistry.AutoRegisterClasses(typeof(ReaderExtensions).Assembly);
		}

		public static MfcClass ReadNewMfcClass(this Reader reader)
		{
			ushort schemaVersion = reader.ReadUInt16();
			string className = reader.ReadUInt16String();

			MfcClass mfcClass = _classRegistry.GetMfcClass(className);

			if (mfcClass == null)
			{
				throw new System.IO.InvalidDataException("No registered class for MfcObject " + className);
			}
			if (mfcClass.SchemaVersion != schemaVersion)
			{
				throw new System.IO.InvalidDataException("Schema mismatch: file = " + schemaVersion + ", registered = " + mfcClass.SchemaVersion);
			}

			return mfcClass;
		}

		public static string ReadMfcString(this Reader reader)
		{
			int stringLength = 0;
			byte bLength = reader.ReadByte();
			if (bLength < 0xff)
			{
				stringLength = bLength;
			}
			else
			{
				ushort wLength = reader.ReadUInt16();
				if (wLength < 0xfffe)
				{
					stringLength = wLength;
				}
				else
				{
					if (wLength == 0xfffe)
					{
						// Unicode string prefix -- not currently handled. See
						// CArchive::operator>>(CArchive& ar, CString& string)
						// for details on how to implement this when needed.
					}
					stringLength = reader.ReadInt32();
				}
			}

			return reader.ReadFixedLengthString(stringLength);
		}

		/// <summary>
		/// Tries to read in and return the MfcClass that is next in the stream.
		/// If the next object has already been loaded then null is returned and
		/// alreadyLoadedTag is set to the object ID of the object.
		/// </summary>
		public static MfcClass ReadMfcClass(this Reader reader, out uint alreadyLoadedTag)
		{
			ushort tag = reader.ReadUInt16();
			uint objectTag = ((uint)(tag & ClassTag) << 16) | (uint)(tag & ~ClassTag);
			if (tag == BigObjectTag)
			{
				objectTag = reader.ReadUInt32();
			}

			// If it is an object tag and not a class tag then bail out -- caller will handle it
			alreadyLoadedTag = 0;
			if ((objectTag & BigClassTag) == 0)
			{
				alreadyLoadedTag = objectTag;
				return null;
			}

			MfcClass mfcClass;
			if (tag == NewClassTag)
			{
				// Not a class we've seen before; read it in
				mfcClass = reader.ReadNewMfcClass();
				_loadedClasses.Add(mfcClass);
				return mfcClass;
			}

			// A class we've seen before, look it up by index
			uint classIndex = objectTag & ~BigClassTag;
			if (classIndex == 0)
			{
				throw new System.IO.InvalidDataException("Got a invalid class index: 0");
			}
			if (classIndex > _loadedClasses.Count)
			{
				throw new System.IO.InvalidDataException("Got a class index larger than the currently loaded count: " + classIndex);
			}

			return _loadedClasses[(int)classIndex];
		}

		public static T ReadMfcObject<T>(this Reader reader) where T : MfcObject
		{
			uint objectTag;
			MfcClass mfcClass = reader.ReadMfcClass(out objectTag);

			if (mfcClass == null)
			{
				// An object we've already loaded
				if (objectTag > _loadedObjects.Count)
				{
					throw new System.IO.InvalidDataException("Got an object tag larger than the count of loaded objects: " + objectTag);
				}

				return (T)_loadedObjects[(int)objectTag];
			}

			// An object we haven't yet loaded. Create a new instance and deserialise it.
			// Make sure to add it to the list of loaded objects before deserialising in
			// case it has (possibly indirect) references to itself.
			return reader.ReadNewMfcObject<T>(mfcClass);
		}
		public static T ReadNewMfcObject<T>(this Reader reader, MfcClass mfcClass) where T : MfcObject
		{
			MfcObject newObject = mfcClass.CreateNewObject<MfcObject>();
			_loadedObjects.Add(newObject);
			newObject.LoadObject(reader);
			return (T)newObject;
		}

		/// <summary>
		/// Deserialises an object of type T without reading in the header.
		/// This implies it must be a new object and not one that has already
		/// been loaded since there is no object tag to reference the loaded list.
		/// </summary>
		public static T ReadMfcObjectWithoutHeader<T>(this Reader reader) where T : MfcObject
		{
			MfcClass mfcClass = _classRegistry.GetMfcClass(typeof(T));
			return reader.ReadNewMfcObject<T>(mfcClass);
		}

		public static List<T> ReadMfcList<T>(this Reader reader) where T : MfcObject
		{
			List<T> result = new List<T>();
			ushort listLength = reader.ReadUInt16();

			if (listLength >= 1)
			{
				// First object has a valid runtime class header
				result.Add(reader.ReadMfcObject<T>());
			}
			for (int i = 1; i < listLength; i++)
			{
				// Subsequent objects are missing the runtime class header but have
				// a UInt16 preceding them that looks like an invalid runtime class
				// header.
				uint unknown1 = reader.ReadUInt16();
				result.Add(reader.ReadMfcObjectWithoutHeader<T>());
			}

			return result;
		}
	}
}
