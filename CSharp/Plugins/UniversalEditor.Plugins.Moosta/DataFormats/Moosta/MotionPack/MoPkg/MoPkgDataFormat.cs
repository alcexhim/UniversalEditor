using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

			IO.Reader br = base.Stream.Reader;
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
				string value = br.ReadInt16UnicodeString();

				PackageInformation ste = new PackageInformation();
				ste.LanguageID = langID;
				ste.PackageName = value;
				mopkg.PackageInformation.Add(ste);
			}
			int packageInfoCount2 = br.ReadInt32();
			for (int i = 0; i < packageInfoCount2; i++)
			{
				int langID = br.ReadInt32();
				string value = br.ReadInt16UnicodeString();
				if (i < mopkg.PackageInformation.Count)
				{
					mopkg.PackageInformation[i].PackageDescription = value;
				}
			}
			int copyrightCount = br.ReadInt32();
			for (int i = 0; i < copyrightCount; i++)
			{
				string copyright = br.ReadInt16UnicodeString();
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

			IO.Writer bw = base.Stream.Writer;
			bw.WriteFixedLengthString("MoPkg");
			bw.Write(FormatVersion);
			bw.Write(mvarPackageVersion);
			// bw.Write(mopkg.Animations.Count);
			bw.Write(mopkg.PackageInformation.Count);
			foreach (PackageInformation packageName in mopkg.PackageInformation)
			{
				bw.Write(packageName.LanguageID);
				bw.Write(packageName.PackageName);
			}
			bw.Write(mopkg.PackageInformation.Count);
			foreach (PackageInformation packageName in mopkg.PackageInformation)
			{
				bw.Write(packageName.LanguageID);
				bw.Write(packageName.PackageDescription);
			}
			foreach (string copyright in mopkg.Copyrights)
			{
				bw.WriteUInt16SizedString(copyright);
			}
			bw.Write(mvarIsRemovable);
			if (mvarThumbnail != null)
			{
				bw.Write(mvarThumbnail.Width);
				bw.Write(mvarThumbnail.Height);
				for (int y = 0; y < mvarThumbnail.Height; y++)
				{
					for (int x = 0; x < mvarThumbnail.Width; x++)
					{
						Color pixel = mvarThumbnail.GetPixel(x, y);
						bw.Write((byte)(pixel.Alpha * 255));
						bw.Write((byte)(pixel.Red * 255));
						bw.Write((byte)(pixel.Green * 255));
						bw.Write((byte)(pixel.Blue * 255));
					}
				}
			}
			else
			{
				bw.Write(0);
				bw.Write(0);
			}
			bw.Write(mvarCompressionMode);
		}
	}
}
