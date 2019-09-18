
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public abstract class XCOFFSectionHeader
	{
		private string mvarSectionName = "";
		public string SectionName { get { return mvarSectionName; } set { mvarSectionName = value; } }
	}
}
