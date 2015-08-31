using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class AssociationCriteria
	{
		private Accessor mvarAccessor = null;
		/// <summary>
		/// The accessor to use for MagicByteSequence comparisons.
		/// </summary>
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		private DataFormatReference mvarDataFormat = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> which points to the <see cref="DataFormat" /> to search for.
		/// </summary>
		public DataFormatReference DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

		private ObjectModelReference mvarObjectModel = null;
		/// <summary>
		/// The <see cref="ObjectModelReference" /> which points to the <see cref="ObjectModel" /> to search for.
		/// </summary>
		public ObjectModelReference ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }
		
		private string mvarFileName = null;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }
	}
}
