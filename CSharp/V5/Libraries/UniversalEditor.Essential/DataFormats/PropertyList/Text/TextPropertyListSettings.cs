using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.PropertyList.Text
{
	public class TextPropertyListSettings
	{
		private string[] mvarCommentSignals = new string[] { ";" };
		public string[] CommentSignals { get { return mvarCommentSignals; } set { mvarCommentSignals = value; } }

		private string[] mvarPropertyNameValueSeparators = new string[] { " ", "\t" };
		public string[] PropertyNameValueSeparators { get { return mvarPropertyNameValueSeparators; } set { mvarPropertyNameValueSeparators = value; } }

		private string mvarIgnoreBegin = "\"";
		public string IgnoreBegin { get { return mvarIgnoreBegin; } set { mvarIgnoreBegin = value; } }

		private string mvarIgnoreEnd = "\"";
		public string IgnoreEnd { get { return mvarIgnoreEnd; } set { mvarIgnoreEnd = value; } }
	}
}
