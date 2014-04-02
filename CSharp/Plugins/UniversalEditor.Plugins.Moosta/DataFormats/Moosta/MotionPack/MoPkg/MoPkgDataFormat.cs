using System;
using System.Collections.Generic;
using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Moosta.MotionPack;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Moosta.MotionPack.MoPkg
{
	public class MoPkgDataFormat : DataFormat
	{
		private List<string> mvarCopyrights = new List<string>();
		public List<string> Copyrights { get { return mvarCopyrights; } }

		protected virtual float FormatVersion { get { return 1.0f; } }

		private float mvarPackageVersion = 1.0f;
		public float PackageVersion { get { return mvarPackageVersion; } set { mvarPackageVersion = value; } }

		private bool mvarIsRemovable = false;
		public bool IsRemovable { get { return mvarIsRemovable; } set { mvarIsRemovable = value; } }

		private PictureObjectModel mvarThumbnail = null;
		public PictureObjectModel Thumbnail { get { return mvarThumbnail; } }

		private byte mvarCompressionMode = 0;

		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(MotionPackObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Moosta motion pack", new byte?[][] { new byte?[] { (byte)'M', (byte)'o', (byte)'P', (byte)'k', (byte)'g' } }, new string[] { "*.mopkg" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MotionPackObjectModel mopkg = (objectModel as MotionPackObjectModel);
			if (mopkg == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
            base.Accessor.DefaultEncoding = Encoding.UTF16LittleEndian;

			string signature = br.ReadFixedLengthString(5);
			if (signature != "MoPkg") throw new InvalidDataFormatException("File does not begin with \"MoPkg\"");
			
			float formatVersion = br.ReadSingle();
			if (formatVersion > FormatVersion) throw new InvalidDataFormatException("Format version " + formatVersion.ToString() + " invalid");

			mvarPackageVersion = br.ReadSingle();
			// bw.Read(mopkg.Animations.Count);
			int packageInfoCount1 = br.ReadInt32();
			for (int i = 0; i < packageInfoCount1; i++)
			{
				int langID = br.ReadInt32();
				string value = br.ReadInt16String();

				PackageInformation ste = new PackageInformation();
				ste.LanguageID = langID;
				ste.PackageName = value;
				mopkg.PackageInformation.Add(ste);
			}
			int packageInfoCount2 = br.ReadInt32();
			for (int i = 0; i < packageInfoCount2; i++)
			{
				int langID = br.ReadInt32();
				string value = br.ReadInt16String();
				if (i < mopkg.PackageInformation.Count)
				{
					mopkg.PackageInformation[i].PackageDescription = value;
				}
			}
			int copyrightCount = br.ReadInt32();
			for (int i = 0; i < copyrightCount; i++)
			{
				string copyright = br.ReadInt16String();
				mvarCopyrights.Add(copyright);
			}

			mvarIsRemovable = br.ReadBoolean();
			
			int thumbnailWidth = br.ReadInt32();
			int thumbnailHeight = br.ReadInt32();
			if (thumbnailWidth != 0 && thumbnailHeight != 0)
			{
				mvarThumbnail = new PictureObjectModel();
				mvarThumbnail.Width = thumbnailWidth;
				mvarThumbnail.Height = thumbnailHeight;

				for (int y = 0; y < thumbnailHeight; y++)
				{
					for (int x = 0; x < thumbnailWidth; x++)
					{
						byte a = br.ReadByte();
						byte r = br.ReadByte();
						byte g = br.ReadByte();
						byte b = br.ReadByte();
						mvarThumbnail.SetPixel(Color.FromRGBA(r, g, b, a), x, y);
					}
				}
			}
			mvarCompressionMode = br.ReadByte();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			MotionPackObjectModel mopkg = (objectModel as MotionPackObjectModel);
			if (mopkg == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("MoPkg");
			bw.WriteSingle(FormatVersion);
			bw.WriteSingle(mvarPackageVersion);
			// bw.Write(mopkg.Animations.Count);
			bw.WriteInt32(mopkg.PackageInformation.Count);
			foreach (PackageInformation packageName in mopkg.PackageInformation)
			{
				bw.WriteInt32(packageName.LanguageID);
				bw.WriteLengthPrefixedString(packageName.PackageName);
			}
			bw.WriteInt32(mopkg.PackageInformation.Count);
			foreach (PackageInformation packageName in mopkg.PackageInformation)
			{
				bw.WriteInt32(packageName.LanguageID);
				bw.WriteLengthPrefixedString(packageName.PackageDescription);
			}
			foreach (string copyright in mopkg.Copyrights)
			{
				bw.WriteUInt16SizedString(copyright);
			}
			bw.WriteBoolean(mvarIsRemovable);
			if (mvarThumbnail != null)
			{
				bw.WriteInt32(mvarThumbnail.Width);
				bw.WriteInt32(mvarThumbnail.Height);
				for (int y = 0; y < mvarThumbnail.Height; y++)
				{
					for (int x = 0; x < mvarThumbnail.Width; x++)
					{
						Color pixel = mvarThumbnail.GetPixel(x, y);
						bw.WriteByte((byte)(pixel.Alpha * 255));
						bw.WriteByte((byte)(pixel.Red * 255));
						bw.WriteByte((byte)(pixel.Green * 255));
						bw.WriteByte((byte)(pixel.Blue * 255));
					}
				}
			}
			else
			{
				bw.WriteInt32(0);
				bw.WriteInt32(0);
			}
			bw.WriteByte(mvarCompressionMode);
		}
	}
}
