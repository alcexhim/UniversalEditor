/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/19/2013
 * Time: 3:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.TMH
{
	/// <summary>
	/// Description of TMHDataFormat.
	/// </summary>
	public class TMHDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("TMH image", new byte?[][] { new byte?[] { (byte)'.', (byte)'T', (byte)'M', (byte)'H', (byte)'0', (byte)'.', (byte)'1', (byte)'4' } }, new string[] { "*.tmh" });
			}
			return _dfr;
		}
		
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(8);
			if (signature != ".TMH0.14") throw new InvalidDataFormatException("File does not begin with \".TMH0.14\"");
			
			uint unknown1 = br.ReadUInt32();
			uint unknown2 = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			uint unknown4 = br.ReadUInt32();
			uint unknown5 = br.ReadUInt32();
			uint unknown6 = br.ReadUInt32();
			uint unknown7 = br.ReadUInt32();
			uint unknown8 = br.ReadUInt32();
			uint unknown9 = br.ReadUInt32();
			ushort unknown10a = br.ReadUInt16();
			ushort unknown10b = br.ReadUInt16();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
