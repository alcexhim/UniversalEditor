using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;

using UniversalEditor.Plugins.Genealogy.ObjectModels.GEDCOM;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.GEDCOM
{
	public class GEDCOMChunkedDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			GEDCOMChunkedObjectModel chunked = (objectModel as GEDCOMChunkedObjectModel);
			
			Reader r = base.Accessor.Reader;
			int lineIndex = 0; // for debugging porpoises

			int lastLineID = 0;
			string lastLineName = null;
			string lastLineParm = null;

			GEDCOMChunk lastChunk = null;

			while (!r.EndOfStream)
			{
				lineIndex++;

				string line = r.ReadLine();
				line = line.Trim();

				string[] lineParts = line.Split(new char[] { ' ' }, 3);

				int lineID = 0;
				if (!Int32.TryParse(lineParts[0], out lineID))
				{
					throw new InvalidDataFormatException("line " + lineIndex + ", part '" + lineParts[0] + "' invalid ; must be integer");
				}

				if (lineParts.Length < 2)
					throw new InvalidDataFormatException("line " + lineIndex + ", content '" + line + "' must have at least 2 parts");

				if (lastLineID < lineID)
				{
					// create a group for the existing node
					GEDCOMChunk chunk = new GEDCOMChunk();
					chunk.Name = lastLineName;
					// chunk
					chunked.Chunks.Add(chunk);

					lastChunk = chunk;
				}

				lastLineID = lineID;
				lastLineName = lineParts[1];
				if (lineParts.Length > 2)
					lastLineParm = lineParts[2];
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
