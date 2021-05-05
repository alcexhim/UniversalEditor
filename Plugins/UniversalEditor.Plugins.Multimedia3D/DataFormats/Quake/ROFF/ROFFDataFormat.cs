using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.IO;

using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Quake.ROFF
{
	public class ROFFDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Raven Software Rotation/Origin File Format", new byte?[][] { new byte?[] { (byte)'R', (byte)'O', (byte)'F', (byte)'F' } }, new string[] { "*.roff" });
			dfr.Sources.Add("http://www.mrwonko.de/blog/?p=19");
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MotionObjectModel motion = (objectModel as MotionObjectModel);
			if (motion == null) return;

			BinaryReader br = base.Stream.BinaryReader;

			string ROFF = br.ReadFixedLengthString(4);
			int version = br.ReadInt32();
			int numFrames = br.ReadInt32();
			int unknown1 = br.ReadInt32(); // 50?
			int unknown2 = br.ReadInt32(); // 0, sometimes 7 or 8?

			for (int i = 0; i < numFrames; i++)
			{
				MotionFrame frame = new MotionFrame();

				// position and rotation is relative to the last frame
				// in Jedi Knight the entity will be moved in a relative manner.
				float deltaLocX = br.ReadSingle();
				float deltaLocY = br.ReadSingle();
				float deltaLocZ = br.ReadSingle();
				frame.Position = new PositionVector3(deltaLocX, deltaLocY, deltaLocZ);

				// rotation difference, in degrees
				float deltaRotX = br.ReadSingle();
				float deltaRotY = br.ReadSingle();
				float deltaRotZ = br.ReadSingle();
				frame.Rotation = new PositionVector3(deltaRotX, deltaRotY, deltaRotZ);

				//whatever - always the same though
				int unknown3 = br.ReadInt32(); // -1?
				int unknown4 = br.ReadInt32(); // 0?

				motion.Frames.Add(frame);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			MotionObjectModel motion = (objectModel as MotionObjectModel);
			if (motion == null) return;

			BinaryWriter bw = base.Stream.BinaryWriter;

			bw.WriteFixedLengthString("ROFF");
			int version = 2;
			bw.Write(version);

			bw.Write(motion.Frames.Count);

			int unknown1 = 50; // 50?
			bw.Write(unknown1);

			int unknown2 = 0; // 0, sometimes 7 or 8?
			bw.Write(unknown2);

			for (int i = 0; i < motion.Frames.Count; i++)
			{
				MotionFrame frame = new MotionFrame();

				// position and rotation is relative to the last frame
				// in Jedi Knight the entity will be moved in a relative manner.
				bw.Write(frame.Position.X);
				bw.Write(frame.Position.Y);
				bw.Write(frame.Position.Z);

				// rotation difference, in degrees
				bw.Write(frame.Rotation.X);
				bw.Write(frame.Rotation.Y);
				bw.Write(frame.Rotation.Z);

				//whatever - always the same though
				int unknown3 = -1;
				bw.Write(unknown3); // -1?
				int unknown4 = 0;
				bw.Write(unknown4); // 0?
			}

			bw.Flush();
		}
	}
}
