//
//  ObjectModelReference.cs - stores information (metadata) about an ObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace UniversalEditor
{
	public class ObjectModelReference : ReferencedBy<ObjectModel>, IComparable<ObjectModelReference>
	{
		public class ObjectModelReferenceCollection
			: System.Collections.ObjectModel.Collection<ObjectModelReference>
		{
			private Dictionary<Guid, ObjectModelReference> refsByID = new Dictionary<Guid, ObjectModelReference>();
			private Dictionary<Type, ObjectModelReference> refsByType = new Dictionary<Type, ObjectModelReference>();

			/// <summary>
			/// Associates a new <see cref="ObjectModelReference" /> with the specified <see cref="Guid" /> ID and returns it.
			/// </summary>
			/// <param name="ID">The <see cref="Guid" /> of the <see cref="ObjectModel" /> for which to create an <see cref="ObjectModelReference" />.</param>
			/// <returns>The newly-created <see cref="ObjectModelReference" />.</returns>
			public ObjectModelReference Add(Guid ID)
			{
				if (refsByID.ContainsKey(ID)) return refsByID[ID];

				ObjectModelReference omr = new ObjectModelReference(ID);
				refsByID.Add(ID, omr);
				Add(omr);
				return omr;
			}
			/// <summary>
			/// Associates a new <see cref="ObjectModelReference" /> with the specified <see cref="Type" /> and returns it.
			/// </summary>
			/// <param name="type">The <see cref="Type" /> of the <see cref="ObjectModel" /> for which to create an <see cref="ObjectModelReference" />.</param>
			/// <returns>The newly-created <see cref="ObjectModelReference" />.</returns>
			public ObjectModelReference Add(Type type)
			{
				if (refsByType.ContainsKey(type)) return refsByType[type];

				ObjectModelReference omr = new ObjectModelReference(type);
				refsByType.Add(type, omr);
				Add(omr);
				return omr;
			}

			/// <summary>
			/// Gets the <see cref="ObjectModelReference" /> associated with the specified ID.
			/// </summary>
			/// <param name="ID">The <see cref="Guid" /> to search for.</param>
			/// <returns>The <see cref="ObjectModelReference" /> associated with the specified ID, or null if no <see cref="ObjectModelReference" /> is associated with the specified ID.</returns>
			public ObjectModelReference this[Guid ID]
			{
				get
				{
					if (refsByID.ContainsKey(ID)) return refsByID[ID];
					return null;
				}
			}

			/// <summary>
			/// Gets the <see cref="ObjectModelReference" /> associated with the specified <see cref="Type" />.
			/// </summary>
			/// <param name="type">The <see cref="Type" /> to search for.</param>
			/// <returns>The <see cref="ObjectModelReference" /> associated with the specified <see cref="Type" />, or null if no <see cref="ObjectModelReference" /> is associated with the specified <see cref="Type" />.</returns>
			public ObjectModelReference this[Type type]
			{
				get
				{
					if (refsByType.ContainsKey(type)) return refsByType[type];
					return null;
				}
			}
			/// <summary>
			/// Determines if a <see cref="ObjectModelReference" /> with the specified ID exists in the collection.
			/// </summary>
			/// <param name="ID">The <see cref="Guid" /> of the <see cref="ObjectModelReference" /> to search for.</param>
			/// <returns>True if an <see cref="ObjectModelReference" /> with the specified ID exists in the collection; false otherwise.</returns>
			public bool Contains(Guid ID)
			{
				if (refsByID.Count == 0)
				{
					foreach (ObjectModelReference omr in this)
					{
						if (omr.ID != Guid.Empty)
						{
							refsByID.Add(omr.ID, omr);
						}
					}
				}
				return (refsByID.ContainsKey(ID));
			}
			/// <summary>
			/// Determines if a <see cref="ObjectModelReference" /> associated with the specified <see cref="Type" /> exists in the collection.
			/// </summary>
			/// <param name="ID">The <see cref="Type" /> of the <see cref="ObjectModelReference" /> to search for.</param>
			/// <returns>True if an <see cref="ObjectModelReference" /> associated with the specified <see cref="Type" /> exists in the collection; false otherwise.</returns>
			public bool Contains(Type type)
			{
				if (refsByType.Count == 0)
				{
					foreach (ObjectModelReference omr in this)
					{
						if (omr.Type != null)
						{
							refsByType.Add(omr.Type, omr);
						}
					}
				}
				return (refsByType.ContainsKey(type));
			}
		}

		private string mvarTypeName = null;
		public string TypeName
		{
			get
			{
				if (mvarTypeName != null)
				{
					return mvarTypeName;
				}
				else if (mvarType != null)
				{
					return mvarType.FullName;
				}
				return null;
			}
			set { mvarTypeName = value; }
		}

		public string[] GetDetails()
		{
			return new string[] { Title, mvarDescription };
		}

		private Type mvarType = null;
		public Type Type { get { return mvarType; } }

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		public string Title { get { return Path[Path.Length - 1]; } }

		public string GetTitle()
		{
			if (Title != null) return Title;
			if (mvarType != null) return mvarType.FullName;
			return mvarID.ToString("B");
		}

		private string[] mvarPath = null;
		public string[] Path
		{
			get
			{
				if (mvarPath == null && mvarType != null)
				{
					string[] sz = mvarType.FullName.Split(new char[] { '.' });
					return sz;
				}
				return mvarPath;
			}
			set { mvarPath = value; }
		}

		public ObjectModelReference(Guid ID)
		{
			mvarID = ID;
		}
		public ObjectModelReference(string TypeName)
		{
			mvarTypeName = TypeName;
		}
		public ObjectModelReference(Type type)
		{
			if (!type.IsSubclassOf(typeof(ObjectModel)))
			{
				throw new InvalidCastException("Cannot create an object model reference to a non-ObjectModel type");
			}
			else if (type.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create an object model reference to an abstract type");
			}

			mvarType = type;
			mvarTypeName = mvarType.FullName;
		}
		public ObjectModelReference(Type type, Guid ID)
		{
			if (!type.IsSubclassOf(typeof(ObjectModel)))
			{
				throw new InvalidCastException("Cannot create an object model reference to a non-ObjectModel type");
			}
			else if (type.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create an object model reference to an abstract type");
			}

			mvarType = type;
			mvarTypeName = mvarType.FullName;
			mvarID = ID;
		}

		public ObjectModel Create()
		{
			if (mvarType == null && mvarTypeName != null)
			{
				mvarType = MBS.Framework.Reflection.FindType(mvarTypeName);
			}
			if (mvarType != null)
			{
				return (mvarType.Assembly.CreateInstance(mvarType.FullName) as ObjectModel);
			}
			return null;
		}

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }


		private static Dictionary<Guid, ObjectModelReference> _referencesByGUID = new Dictionary<Guid, ObjectModelReference>();
		private static Dictionary<string, ObjectModelReference> _referencesByTypeName = new Dictionary<string, ObjectModelReference>();

		public static bool Register(ObjectModelReference dfr)
		{
			bool retval = false;
			if (dfr.Type != null)
			{
				dfr.TypeName = dfr.Type.FullName;
			}
			if (dfr.ID != Guid.Empty)
			{
				_referencesByGUID[dfr.ID] = dfr;
				retval = true;
			}
			if (dfr.TypeName != null)
			{
				_referencesByTypeName[dfr.TypeName] = dfr;
				retval = true;
			}
			return retval;
		}
		public static bool Unregister(ObjectModelReference dfr)
		{
			bool retval = false;
			if (dfr.ID != Guid.Empty && _referencesByGUID.ContainsKey(dfr.ID))
			{
				_referencesByGUID.Remove(dfr.ID);
				retval = true;
			}
			if (dfr.TypeName != null && _referencesByTypeName.ContainsKey(dfr.TypeName))
			{
				_referencesByTypeName.Remove(dfr.TypeName);
				retval = true;
			}
			return retval;
		}

		public static ObjectModelReference FromTypeName(string typeName)
		{
			if (_referencesByTypeName.ContainsKey(typeName)) return _referencesByTypeName[typeName];
			return null;
		}
		public static ObjectModelReference FromGUID(Guid guid)
		{
			if (_referencesByGUID.ContainsKey(guid)) return _referencesByGUID[guid];
			return null;
		}
		public override bool Equals(object obj)
		{
			ObjectModelReference omr = (obj as ObjectModelReference);
			if (omr == null) return false;
			if (mvarID == Guid.Empty)
			{
				// do not compare ID
				if (mvarTypeName == null) return false;
				return mvarTypeName.Equals(omr.TypeName);
			}
			return mvarID.Equals(omr.ID);
		}
		public int CompareTo(ObjectModelReference other)
		{
			if (mvarID == Guid.Empty)
			{
				// do not compare ID
				if (mvarTypeName == null)
				{
					if (other.ID == Guid.Empty && other.TypeName == null) return 0;
					return -1;
				}
				return mvarTypeName.CompareTo(other.TypeName);
			}
			return mvarID.CompareTo(other.ID);
		}
	}
}
