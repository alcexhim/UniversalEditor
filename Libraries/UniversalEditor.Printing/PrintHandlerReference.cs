//
//  PrintHandlerReference.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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

namespace UniversalEditor.Printing
{
	public class PrintHandlerReference : ReferencedBy<PrintHandler>
	{
		public PrintHandlerReference(Guid id)
		{
			mvarID = id;
		}
		public PrintHandlerReference(string dataFormatTypeName)
		{
			mvarTypeName = dataFormatTypeName;
		}
		public PrintHandlerReference(Type dataFormatType)
		{
			if (!dataFormatType.IsSubclassOf(typeof(PrintHandler)))
			{
				throw new InvalidCastException("Cannot create a data format reference to a non-PrintHandler type");
			}
			else if (dataFormatType.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create a data format reference to an abstract type");
			}

			mvarType = dataFormatType;
		}

		public ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get; } = new ObjectModelReference.ObjectModelReferenceCollection();

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private Type mvarType = null;
		public Type Type { get { return mvarType; } }

		private string mvarTypeName = null;
		public string TypeName { get { return mvarTypeName; } set { mvarTypeName = value; } }

		public virtual PrintHandler Create()
		{
			PrintHandler df = (mvarType.Assembly.CreateInstance(mvarType.FullName) as PrintHandler);
			df.Reference = this;
			return df;
		}

		public string[] GetDetails()
		{
			throw new NotImplementedException();
		}


		private static Dictionary<Guid, PrintHandlerReference> _referencesByGUID = new Dictionary<Guid, PrintHandlerReference>();
		private static Dictionary<string, PrintHandlerReference> _referencesByTypeName = new Dictionary<string, PrintHandlerReference>();

		public static bool Register(PrintHandlerReference dfr)
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
		public static bool Unregister(PrintHandlerReference dfr)
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

		public static PrintHandlerReference FromTypeName(string typeName)
		{
			if (_referencesByTypeName.ContainsKey(typeName)) return _referencesByTypeName[typeName];
			return null;
		}
		public static PrintHandlerReference FromGUID(Guid guid)
		{
			if (_referencesByGUID.ContainsKey(guid)) return _referencesByGUID[guid];
			return null;
		}
		public override bool Equals(object obj)
		{
			PrintHandlerReference omr = (obj as PrintHandlerReference);
			if (omr == null) return false;
			if (mvarID == Guid.Empty)
			{
				// do not compare ID
				if (mvarTypeName == null) return false;
				return mvarTypeName.Equals(omr.TypeName);
			}
			return mvarID.Equals(omr.ID);
		}
		public int CompareTo(PrintHandlerReference other)
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
