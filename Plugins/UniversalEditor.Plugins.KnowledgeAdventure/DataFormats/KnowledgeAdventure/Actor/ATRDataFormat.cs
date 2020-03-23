using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.KnowledgeAdventure.Actor;

namespace UniversalEditor.DataFormats.KnowledgeAdventure.Actor
{
	public class ATRDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ActorObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ActorObjectModel actor = (objectModel as ActorObjectModel);
			if (actor == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			
			string signature = reader.ReadFixedLengthString(5);
			if (signature != "ATR11") throw new InvalidDataFormatException("File does not begin with 'ATR11'");

			actor.Name = reader.ReadFixedLengthString(16).TrimNull();
			actor.ImageFileName = reader.ReadFixedLengthString(16).TrimNull();
			string referencedActorName = reader.ReadFixedLengthString(16).TrimNull();

													// b0tlk.atr	beazly.atr
			uint unknown1 = reader.ReadUInt32();	//	1900		911
			uint unknown2 = reader.ReadUInt32();	//	0			2
			uint unknown3 = reader.ReadUInt32();	// 34			69				width?
			uint unknown4 = reader.ReadUInt32();	// 55			86				height?
			uint unknown5 = reader.ReadUInt32();	// 8			8
			uint unknown6 = reader.ReadUInt32();	// 247			1
			uint unknown7 = reader.ReadUInt32();	// 256			316				left?
			uint unknown8 = reader.ReadUInt32();	// 280			213				top?
			uint unknown9 = reader.ReadUInt32();	// 306			0
			uint unknown10 = reader.ReadUInt32();	// 0			0

		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ActorObjectModel actor = (objectModel as ActorObjectModel);
			if (actor == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			writer.WriteFixedLengthString("ATR11");

			writer.WriteFixedLengthString(actor.Name, 16);
			writer.WriteFixedLengthString(actor.ImageFileName, 32);
		}
	}
}
