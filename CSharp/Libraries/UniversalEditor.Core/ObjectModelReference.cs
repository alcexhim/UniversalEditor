using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class ObjectModelReference
	{
		public class ObjectModelReferenceCollection
			: System.Collections.ObjectModel.Collection<ObjectModelReference>
		{
			private Dictionary<Guid, ObjectModelReference> refsByID = new Dictionary<Guid, ObjectModelReference>();
			private Dictionary<Type, ObjectModelReference> refsByType = new Dictionary<Type, ObjectModelReference>();

			public ObjectModelReference Add(Guid ID)
			{
				if (refsByID.ContainsKey(ID)) return refsByID[ID];

				ObjectModelReference omr = new ObjectModelReference(ID);
				refsByID.Add(ID, omr);
				Add(omr);
				return omr;
			}
			public ObjectModelReference Add(Type type)
			{
				if (refsByType.ContainsKey(type)) return refsByType[type];

				ObjectModelReference omr = new ObjectModelReference(type);
				refsByType.Add(type, omr);
				Add(omr);
				return omr;
			}

			public ObjectModelReference this[Guid ID]
			{
				get
				{
					if (refsByID.ContainsKey(ID)) return refsByID[ID];
					return null;
				}
			}
			public ObjectModelReference this[Type type]
			{
				get
				{
					if (refsByType.ContainsKey(type)) return refsByType[type];
					return null;
				}
			}

			public bool Contains(Guid ID)
			{
				return (refsByID.ContainsKey(ID));
			}
			public bool Contains(Type type)
			{
				if (refsByType.Count == 0)
				{
					foreach (ObjectModelReference omr in this)
					{
						if (omr.ObjectModelType != null)
						{
							refsByType.Add(omr.ObjectModelType, omr);
						}
					}
				}
				return (refsByType.ContainsKey(type));
			}
		}

		private string mvarObjectModelTypeName = null;
		public string ObjectModelTypeName
		{
			get
			{
				if (mvarObjectModelTypeName != null)
				{
					return mvarObjectModelTypeName;
				}
				else if (mvarObjectModelType != null)
				{
					return mvarObjectModelType.FullName;
				}
				return null;
			}
		}

		private Type mvarObjectModelType = null;
		public Type ObjectModelType { get { return mvarObjectModelType; } }

		private Guid mvarObjectModelID = Guid.Empty;
		public Guid ObjectModelID { get { return mvarObjectModelID; } }

		private string mvarTitle = null;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public string GetTitle()
		{
			if (mvarTitle != null) return mvarTitle;
			if (mvarObjectModelType != null) return mvarObjectModelType.FullName;
			return mvarObjectModelID.ToString("B");
		}

		private string[] mvarPath = null;
		public string[] Path
		{
			get
			{
				if (mvarPath == null && mvarObjectModelType != null)
				{
					string[] sz = mvarObjectModelType.FullName.Split(new char[] { '.' });
					if (mvarTitle != null)
					{
						sz[sz.Length - 1] = mvarTitle;
					}
					return sz;
				}
				return mvarPath;
			}
			set { mvarPath = value; }
		}

		public ObjectModelReference(Guid ID)
		{
			mvarObjectModelID = ID;
		}
		public ObjectModelReference(string TypeName)
		{
			mvarObjectModelTypeName = TypeName;
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

			mvarObjectModelType = type;
			mvarObjectModelTypeName = mvarObjectModelType.FullName;
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

			mvarObjectModelType = type;
			mvarObjectModelTypeName = mvarObjectModelType.FullName;
			mvarObjectModelID = ID;
		}

		public ObjectModel Create()
		{
			if (mvarObjectModelType == null && mvarObjectModelTypeName != null)
			{
				mvarObjectModelType = Type.GetType(mvarObjectModelTypeName);
			}
			if (mvarObjectModelType != null)
			{
				return (mvarObjectModelType.Assembly.CreateInstance(mvarObjectModelType.FullName) as ObjectModel);
			}
			return null;
		}

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }
	}
}
