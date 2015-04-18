using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration
{
	public class ExtensibleConfigurationSettings
	{
		private string mvarPropertyNamePrefix = String.Empty;
		public string PropertyNamePrefix { get { return mvarPropertyNamePrefix; } set { mvarPropertyNamePrefix = value; } }

		private string mvarPropertyNameSuffix = String.Empty;
		public string PropertyNameSuffix { get { return mvarPropertyNameSuffix; } set { mvarPropertyNameSuffix = value; } }

		private string mvarPropertyNameValueSeparator = "=";
		public string PropertyNameValueSeparator { get { return mvarPropertyNameValueSeparator; } set { mvarPropertyNameValueSeparator = value; } }

		private string mvarPropertyValuePrefix = "\"";
		public string PropertyValuePrefix { get { return mvarPropertyValuePrefix; } set { mvarPropertyValuePrefix = value; } }

		private string mvarPropertyValueSuffix = "\"";
		public string PropertyValueSuffix { get { return mvarPropertyValueSuffix; } set { mvarPropertyValueSuffix = value; } }

		private string mvarPropertySeparator = ";";
		public string PropertySeparator { get { return mvarPropertySeparator; } set { mvarPropertySeparator = value; } }

		private string mvarMultiLineCommentStart = "/*";
		public string MultiLineCommentStart { get { return mvarMultiLineCommentStart; } set { mvarMultiLineCommentStart = value; } }

		private string mvarMultiLineCommentEnd = "*/";
		public string MultiLineCommentEnd { get { return mvarMultiLineCommentEnd; } set { mvarMultiLineCommentEnd = value; } }

		private string mvarSingleLineCommentStart = "//";
		public string SingleLineCommentStart { get { return mvarSingleLineCommentStart; } set { mvarSingleLineCommentStart = value; } }

		private string mvarGroupStart = "{";
		public string GroupStart { get { return mvarGroupStart; } set { mvarGroupStart = value; } }

		private string mvarGroupEnd = "}";
		public string GroupEnd { get { return mvarGroupEnd; } set { mvarGroupEnd = value; } }

		private bool mvarInlineGroupStart = true;
		/// <summary>
		/// Determines whether the group start character (default '{') should be placed on the same line as the group name.
		/// </summary>
		public bool InlineGroupStart { get { return mvarInlineGroupStart; } set { mvarInlineGroupStart = value; } }

		private bool mvarAllowTopLevelProperties = true;
		/// <summary>
		/// Determines whether top-level properties (i.e., those outside of a group definition) are allowed.
		/// </summary>
		public bool AllowTopLevelProperties { get { return mvarAllowTopLevelProperties; } set { mvarAllowTopLevelProperties = value; } }
	}
}
