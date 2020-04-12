//
//  Heroes4CampaignDataFormat.cs - provides a DataFormat for manipulating Heroes of Might and Magic IV campaign definitions
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.NewWorldComputing.Campaign;

namespace UniversalEditor.DataFormats.NewWorldComputing.Campaign
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Heroes of Might and Magic IV campaign definitions.
	/// </summary>
	public class Heroes4CampaignDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null) _dfr = base.MakeReferenceInternal();
			_dfr.Capabilities.Add(typeof(CampaignObjectModel), DataFormatCapabilities.All);
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CampaignObjectModel campaign = (objectModel as CampaignObjectModel);
			if (campaign == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			string H4CAMPAIGN = br.ReadFixedLengthString(10);
			if (H4CAMPAIGN != "H4CAMPAIGN") throw new InvalidDataFormatException("File does not begin with 'H4CAMPAIGN'");

			int unknown1 = br.ReadInt32();
			byte nul = br.ReadByte();

			byte[] restOfDataCompressed = br.ReadToEnd();
			byte[] restOfDataUncompressed = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(restOfDataCompressed);

			br = new Reader(new MemoryAccessor(restOfDataUncompressed));

			short unknown2 = br.ReadInt16();
			short unknown3 = br.ReadInt16();
			short unknown4 = br.ReadInt16();
			short unknown5 = br.ReadInt16();
			short unknown6 = br.ReadInt16();
			short unknown7 = br.ReadInt16();
			short unknown8 = br.ReadInt16();
			short unknown9 = br.ReadInt16();
			short unknown10 = br.ReadInt16();
			short unknown11 = br.ReadInt16();

			campaign.Title = br.ReadInt16String();
			byte unknown12 = br.ReadByte();
			campaign.Description = br.ReadInt16String();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			CampaignObjectModel campaign = (objectModel as CampaignObjectModel);
			if (campaign == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("H4CAMPAIGN");

			throw new NotImplementedException();
		}
	}
}
