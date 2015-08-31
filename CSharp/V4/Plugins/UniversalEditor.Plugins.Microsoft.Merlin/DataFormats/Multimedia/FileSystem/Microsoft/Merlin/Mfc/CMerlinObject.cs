using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia.FileSystem.Microsoft.Merlin.Mfc
{
	public class CMerlinObject : MfcObject
	{
		public string Name { get; set; }

		public override void LoadObject(Reader reader)
		{
			Name = reader.ReadMfcString();
			reader.ReadMfcObjectWithoutHeader<TrailingBytes>();
		}

		public override void SaveObject(Writer writer)
		{
			// writer.WriteMfcString(Name);
			// writer.WriteMfcObjectWithoutHeader(new TrailingBytes());
		}
	}
}
