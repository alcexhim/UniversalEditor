using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	public class ContentType : ICloneable
	{
		public class ContentTypeCollection
			: System.Collections.ObjectModel.Collection<ContentType>
		{

		}

		private string mvarExtension = String.Empty;
		public string Extension { get { return mvarExtension; } set { mvarExtension = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("*.");
			sb.Append(mvarExtension);
			sb.Append("; ");
			sb.Append(mvarValue);
			return sb.ToString();
		}

		public object Clone()
		{
			ContentType clone = new ContentType();
			clone.Value = (mvarValue.Clone() as string);
			clone.Extension = (mvarExtension.Clone() as string);
			return clone;
		}
	}
}
