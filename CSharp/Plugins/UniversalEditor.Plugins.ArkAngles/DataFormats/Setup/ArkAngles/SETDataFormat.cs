using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Setup.ArkAngles;

namespace UniversalEditor.DataFormats.Setup.ArkAngles
{
	public class SETDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SetupObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SetupObjectModel setup = (objectModel as SetupObjectModel);
			if (setup == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();

				string command = line.Substring(0, line.IndexOf(' '));
				string paramzLine = line.Substring(line.IndexOf(' ') + 1);
				string[] paramz = paramzLine.Split(new char[] { ',' });

				switch (command.ToLower())
				{
					case "catalog":
					{
						setup.CatalogExecutableFileName = paramzLine;
						break;
					}
					case "doc":
					{
						// NOTE: the DOC command is found in install.set (DOS) but not
						// recognized by setup1.exe (Win16)
						setup.DocumentationFileName = paramzLine;
						break;
					}
					case "msg":
					case "footer":
					{
						setup.FooterText = paramzLine;
						break;
					}
					case "picture":
					{
						// I guess this is ignored in non-DOS versions, but we still have to
						// parse it
						int unknown1 = Int32.Parse(paramz[0].Trim());
						int unknown2 = Int32.Parse(paramz[1].Trim());
						int cols = Int32.Parse(paramz[2].Trim());
						int rows = Int32.Parse(paramz[3].Trim());
						for (int i = 0; i < rows; i++)
						{
							string row = reader.ReadLine();
						}
						break;
					}
				}
			}

		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
