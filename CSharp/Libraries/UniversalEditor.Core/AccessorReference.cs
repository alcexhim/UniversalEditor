using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class AccessorReference : ReferencedBy<Accessor>
	{
		public AccessorReference(Type type)
		{
			if (!type.IsSubclassOf(typeof(Accessor)))
			{
				throw new InvalidCastException("Cannot create an accessor reference to a non-Accessor type");
			}
			else if (type.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create an accessor reference to an abstract type");
			}

			mvarAccessorType = type;
			mvarAccessorTypeName = mvarAccessorType.FullName;
		}

		private Type mvarAccessorType = null;
		public Type AccessorType { get { return mvarAccessorType; } set { mvarAccessorType = value; } }

		private string mvarAccessorTypeName = null;
		public string AccessorTypeName { get { return mvarAccessorTypeName; } set { mvarAccessorTypeName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public bool ShouldFilterObject(string filter)
		{
			return mvarTitle.ToLower().Contains(filter.ToLower());
		}
		public string[] GetDetails()
		{
			return new string[] { mvarTitle };
		}

		private bool mvarVisible = true;
		/// <summary>
		/// Determines whether the <see cref="Accessor" /> is visible in a selection list. Setting this
		/// property to false can be useful for accessors that are intended for use in code, but should
		/// not be exposed to UniversalEditor user interface components, such as the document properties
		/// dialog.
		/// </summary>
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

		private CustomOption.CustomOptionCollection mvarExportOptions = new CustomOption.CustomOptionCollection();
		/// <summary>
		/// A collection of <see cref="CustomOption" />s that are applied to the <see cref="Accessor" />
		/// when it is being used to save or export a file.
		/// </summary>
		public CustomOption.CustomOptionCollection ExportOptions { get { return mvarExportOptions; } }

		private CustomOption.CustomOptionCollection mvarImportOptions = new CustomOption.CustomOptionCollection();/// <summary>
		/// A collection of <see cref="CustomOption" />s that are applied to the <see cref="Accessor" />
		/// when it is being used to open or import a file.
		/// </summary>
		public CustomOption.CustomOptionCollection ImportOptions { get { return mvarImportOptions; } }

		public Accessor Create()
		{
			if (mvarAccessorType == null && mvarAccessorTypeName != null)
			{
				mvarAccessorType = Type.GetType(mvarAccessorTypeName);
			}
			if (mvarAccessorType != null)
			{
				return (mvarAccessorType.Assembly.CreateInstance(mvarAccessorType.FullName) as Accessor);
			}
			return null;
		}
	}
}
