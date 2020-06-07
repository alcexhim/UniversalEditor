//
//  VMDMotionDataFormat.cs - provides a DataFormat for manipulating animation data in MikuMikuDance "Vocaloid Motion Data" (VMD) format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Motion;

namespace UniversalEditor.DataFormats.Multimedia3D.Motion.PolygonMovieMaker
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating animation data in MikuMikuDance "Vocaloid Motion Data" (VMD) format.
	/// </summary>
	public class VMDMotionDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets the format version of this VMD file.
		/// </summary>
		/// <value>The format version of this VMD file.</value>
		public Version Version { get; set; } = new Version(1, 0);

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MotionObjectModel motion = (objectModel as MotionObjectModel);
			if (motion == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = Encoding.ShiftJIS;
			Reader br = base.Accessor.Reader;

			string signature = br.ReadFixedLengthString(30);
			if (signature == "Vocaloid Motion Data file\0\0\0\0\0")
			{
				Version = new Version(1, 0);
			}
			else if (signature == "Vocaloid Motion Data 0002\0\0\0\0\0")
			{
				Version = new Version(2, 0);
			}
			else if (signature.TrimNull() == "Vocaloid Motion Data 0002")
			{
				Version = new Version(2, 0);
			}
			else
			{
				throw new InvalidDataFormatException("The signature " + signature + " is not supported");
			}

			int modelNameSize = 10;
			if (Version.Major == 1)
			{
				modelNameSize = 10;
			}
			else if (Version.Major == 2)
			{
				modelNameSize = 20;
			}

			string modelName = br.ReadFixedLengthString(modelNameSize);
			int len = modelName.IndexOf('\0');
			if (len == -1) len = modelName.Length;
			modelName = modelName.Substring(0, len);
			byte[] modelNameBytes = System.Text.Encoding.Default.GetBytes(modelName);
			string modelNameInUTF8 = Accessor.DefaultEncoding.GetString(modelNameBytes);
			motion.CompatibleModelNames.Add(modelNameInUTF8);

			System.Collections.Generic.List<UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame> rawFrames = new System.Collections.Generic.List<UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame>();

			uint numBones = br.ReadUInt32();
			for (uint i = 0; i < numBones; i++)
			{
				UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame rawFrame = new UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame();

				string boneName = br.ReadFixedLengthString(15);
				boneName = boneName.Substring(0, boneName.IndexOf('\0'));
				rawFrame.BoneName = boneName;

				uint frameIndex = br.ReadUInt32();
				rawFrame.Index = frameIndex;

				float posX = br.ReadSingle();
				float posY = br.ReadSingle();
				float posZ = br.ReadSingle();
				rawFrame.Position = new PositionVector3(posX, posY, posZ);

				float rotX = br.ReadSingle();
				float rotY = br.ReadSingle();
				float rotZ = br.ReadSingle();
				float rotW = br.ReadSingle();
				rawFrame.Rotation = new PositionVector4(rotX, rotY, rotZ, rotW);

				#region Interpolation data
				// special thanks to Re:VB (animiku) for helping me understand this
				rawFrame.Interpolation.XAxis.X1 = br.ReadByte();       // X-axis interpolation bezier curve: P1x   begin point of Bezier curve P1 (x , y)
				rawFrame.Interpolation.YAxis.X1 = br.ReadByte();       // Y-axis interpolation bezier curve: P1x
				rawFrame.Interpolation.ZAxis.X1 = br.ReadByte();       // Z-axis interpolation bezier curve: P1x
				rawFrame.Interpolation.Rotation.X1 = br.ReadByte();    // Rotation interpolation bezier curve: P1x

				rawFrame.Interpolation.XAxis.Y1 = br.ReadByte();       // X-axis interpolation bezier curve: P1y   begin point of Bezier curve P1 (x , y)
				rawFrame.Interpolation.YAxis.Y1 = br.ReadByte();       // Y-axis interpolation bezier curve: P1y
				rawFrame.Interpolation.ZAxis.Y1 = br.ReadByte();       // Z-axis interpolation bezier curve: P1y
				rawFrame.Interpolation.Rotation.Y1 = br.ReadByte();    // Rotation interpolation bezier curve: P1y

				rawFrame.Interpolation.XAxis.X2 = br.ReadByte();       // X-axis interpolation bezier curve: P2x   end point of Bezier curve P2 (x , y)
				rawFrame.Interpolation.YAxis.X2 = br.ReadByte();       // Y-axis interpolation bezier curve: P2x
				rawFrame.Interpolation.ZAxis.X2 = br.ReadByte();       // Z-axis interpolation bezier curve: P2x
				rawFrame.Interpolation.Rotation.X2 = br.ReadByte();    // Rotation interpolation bezier curve: P2x

				rawFrame.Interpolation.XAxis.Y2 = br.ReadByte();       // X-axis interpolation bezier curve: P2y   end point of Bezier curve P2 (x , y)
				rawFrame.Interpolation.YAxis.Y2 = br.ReadByte();       // Y-axis interpolation bezier curve: P2y
				rawFrame.Interpolation.ZAxis.Y2 = br.ReadByte();       // Z-axis interpolation bezier curve: P2y
				rawFrame.Interpolation.Rotation.Y2 = br.ReadByte();    // Rotation interpolation bezier curve: P2y	    	---following is emptiness??

				rawFrame.Interpolation2.YAxis.X1 = br.ReadByte();      // Y-axis interpolation bezier curve: P1x
				rawFrame.Interpolation2.ZAxis.X1 = br.ReadByte();      // Z-axis interpolation bezier curve: P1x
				rawFrame.Interpolation2.Rotation.X1 = br.ReadByte();   // Rotation interpolation bezier curve: P1x
				rawFrame.Interpolation2.XAxis.Y1 = br.ReadByte();      // X-axis interpolation bezier curve: P1y
				rawFrame.Interpolation2.YAxis.Y1 = br.ReadByte();      // Y-axis interpolation bezier curve: P1y
				rawFrame.Interpolation2.ZAxis.Y1 = br.ReadByte();      // Z-axis interpolation bezier curve: P1y
				rawFrame.Interpolation2.Rotation.Y1 = br.ReadByte();   // Rotation interpolation bezier curve: P1y

				rawFrame.Interpolation2.XAxis.X2 = br.ReadByte();      // X-axis interpolation bezier curve: P2x
				rawFrame.Interpolation2.YAxis.X2 = br.ReadByte();      // Y-axis interpolation bezier curve: P2x
				rawFrame.Interpolation2.ZAxis.X2 = br.ReadByte();      // Z-axis interpolation bezier curve: P2x
				rawFrame.Interpolation2.Rotation.X2 = br.ReadByte();   // Rotation interpolation bezier curve: P2x
				rawFrame.Interpolation2.XAxis.Y2 = br.ReadByte();      // X-axis interpolation bezier curve: P2y
				rawFrame.Interpolation2.YAxis.Y2 = br.ReadByte();      // Y-axis interpolation bezier curve: P2y
				rawFrame.Interpolation2.ZAxis.Y2 = br.ReadByte();      // Z-axis interpolation bezier curve: P2y
				rawFrame.Interpolation2.Rotation.Y2 = br.ReadByte();   // Rotation interpolation bezier curve: P2y

				byte unknown1_01 = br.ReadByte();

				rawFrame.Interpolation3.ZAxis.X1 = br.ReadByte();      // Z-axis interpolation bezier curve: P1x
				rawFrame.Interpolation3.Rotation.X1 = br.ReadByte();   // Rotation interpolation bezier curve: P1x
				rawFrame.Interpolation3.XAxis.Y1 = br.ReadByte();      // X-axis interpolation bezier curve: P1y
				rawFrame.Interpolation3.YAxis.Y1 = br.ReadByte();      // Y-axis interpolation bezier curve: P1y
				rawFrame.Interpolation3.ZAxis.Y1 = br.ReadByte();      // Z-axis interpolation bezier curve: P1y
				rawFrame.Interpolation3.Rotation.Y1 = br.ReadByte();   // Rotation interpolation bezier curve: P1y
				rawFrame.Interpolation3.XAxis.X2 = br.ReadByte();      // X-axis interpolation bezier curve: P2x
				rawFrame.Interpolation3.YAxis.X2 = br.ReadByte();      // Y-axis interpolation bezier curve: P2x
				rawFrame.Interpolation3.ZAxis.X2 = br.ReadByte();      // Z-axis interpolation bezier curve: P2x
				rawFrame.Interpolation3.Rotation.X2 = br.ReadByte();   // Rotation interpolation bezier curve: P2x
				rawFrame.Interpolation3.XAxis.Y2 = br.ReadByte();      // X-axis interpolation bezier curve: P2y
				rawFrame.Interpolation3.YAxis.Y2 = br.ReadByte();      // Y-axis interpolation bezier curve: P2y
				rawFrame.Interpolation3.ZAxis.Y2 = br.ReadByte();      // Z-axis interpolation bezier curve: P2y
				rawFrame.Interpolation3.Rotation.Y2 = br.ReadByte();   // Rotation interpolation bezier curve: P2y

				byte unknown2_01 = br.ReadByte();
				byte unknown3_00 = br.ReadByte();

				rawFrame.Interpolation4.Rotation.X1 = br.ReadByte();   // Rotation interpolation bezier curve: P1x
				rawFrame.Interpolation4.XAxis.Y1 = br.ReadByte();      // X-axis interpolation bezier curve: P1y
				rawFrame.Interpolation4.YAxis.Y1 = br.ReadByte();      // Y-axis interpolation bezier curve: P1y
				rawFrame.Interpolation4.ZAxis.Y1 = br.ReadByte();      // Z-axis interpolation bezier curve: P1y
				rawFrame.Interpolation4.Rotation.Y1 = br.ReadByte();   // Rotation interpolation bezier curve: P1y
				rawFrame.Interpolation4.XAxis.X2 = br.ReadByte();      // X-axis interpolation bezier curve: P2x
				rawFrame.Interpolation4.YAxis.X2 = br.ReadByte();      // Y-axis interpolation bezier curve: P2x
				rawFrame.Interpolation4.ZAxis.X2 = br.ReadByte();      // Z-axis interpolation bezier curve: P2x
				rawFrame.Interpolation4.Rotation.X2 = br.ReadByte();   // Rotation interpolation bezier curve: P2x
				rawFrame.Interpolation4.XAxis.Y2 = br.ReadByte();      // X-axis interpolation bezier curve: P2y
				rawFrame.Interpolation4.YAxis.Y2 = br.ReadByte();      // Y-axis interpolation bezier curve: P2y
				rawFrame.Interpolation4.ZAxis.Y2 = br.ReadByte();      // Z-axis interpolation bezier curve: P2y
				rawFrame.Interpolation4.Rotation.Y2 = br.ReadByte();   // Rotation interpolation bezier curve: P2y
				byte unknown4_01 = br.ReadByte();
				byte unknown5_00 = br.ReadByte();
				byte unknown6_00 = br.ReadByte();
				#endregion

				rawFrames.Add(rawFrame);
			}

			uint numTotalFaceKeyframes = br.ReadUInt32();
			for (uint iFaceKeyframe = 0; iFaceKeyframe < numTotalFaceKeyframes; iFaceKeyframe++)
			{
				string faceName = br.ReadFixedLengthString(15);
				if (faceName.Contains("\0"))
				{
					faceName = faceName.Substring(0, faceName.IndexOf('\0'));
				}
				uint frameIndex = br.ReadUInt32();

				// Weight - It is used to scale how much a face morph should move a vertex based off of
				// the maximum possible coordinate that it can move by. (thanks Re:VB)
				float weight = br.ReadSingle();

				UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame rawFrame = new UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame();
				rawFrame.BoneName = faceName;
				rawFrame.Index = frameIndex;
				rawFrame.Position = new PositionVector3(weight, 0, 0);
				rawFrame.Type = UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameType.FaceReposition;
				rawFrames.Add(rawFrame);
			}

			UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameComparer comparer = new UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameComparer();
			rawFrames.Sort(comparer);

			uint index = 0;

			MotionFrame frame = new MotionFrame();
			foreach (UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame rawFrame in rawFrames)
			{
				if (rawFrame.Index != index)
				{
					frame.Index = index;
					motion.Frames.Add(frame);

					frame = new MotionFrame();
					index = rawFrame.Index;
				}

				if (rawFrame.Type == UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameType.BoneReposition)
				{
					MotionBoneRepositionAction action = new MotionBoneRepositionAction();
					action.BoneName = rawFrame.BoneName;
					action.Position = rawFrame.Position;
					action.Rotation = rawFrame.Rotation;

					#region Interpolation Data
					action.Interpolation.XAxis.X1 = rawFrame.Interpolation.XAxis.X1;
					action.Interpolation.XAxis.X2 = rawFrame.Interpolation.XAxis.X2;
					action.Interpolation.XAxis.Y1 = rawFrame.Interpolation.XAxis.Y1;
					action.Interpolation.XAxis.Y2 = rawFrame.Interpolation.XAxis.Y2;
					action.Interpolation.YAxis.X1 = rawFrame.Interpolation.YAxis.X1;
					action.Interpolation.YAxis.X2 = rawFrame.Interpolation.YAxis.X2;
					action.Interpolation.YAxis.Y1 = rawFrame.Interpolation.YAxis.Y1;
					action.Interpolation.YAxis.Y2 = rawFrame.Interpolation.YAxis.Y2;
					action.Interpolation.ZAxis.X1 = rawFrame.Interpolation.ZAxis.X1;
					action.Interpolation.ZAxis.X2 = rawFrame.Interpolation.ZAxis.X2;
					action.Interpolation.ZAxis.Y1 = rawFrame.Interpolation.ZAxis.Y1;
					action.Interpolation.ZAxis.Y2 = rawFrame.Interpolation.ZAxis.Y2;
					action.Interpolation.Rotation.X1 = rawFrame.Interpolation.Rotation.X1;
					action.Interpolation.Rotation.X2 = rawFrame.Interpolation.Rotation.X2;
					action.Interpolation.Rotation.Y1 = rawFrame.Interpolation.Rotation.Y1;
					action.Interpolation.Rotation.Y2 = rawFrame.Interpolation.Rotation.Y2;
					action.Interpolation2.XAxis.X1 = rawFrame.Interpolation2.XAxis.X1;
					action.Interpolation2.XAxis.X2 = rawFrame.Interpolation2.XAxis.X2;
					action.Interpolation2.XAxis.Y1 = rawFrame.Interpolation2.XAxis.Y1;
					action.Interpolation2.XAxis.Y2 = rawFrame.Interpolation2.XAxis.Y2;
					action.Interpolation2.YAxis.X1 = rawFrame.Interpolation2.YAxis.X1;
					action.Interpolation2.YAxis.X2 = rawFrame.Interpolation2.YAxis.X2;
					action.Interpolation2.YAxis.Y1 = rawFrame.Interpolation2.YAxis.Y1;
					action.Interpolation2.YAxis.Y2 = rawFrame.Interpolation2.YAxis.Y2;
					action.Interpolation2.ZAxis.X1 = rawFrame.Interpolation2.ZAxis.X1;
					action.Interpolation2.ZAxis.X2 = rawFrame.Interpolation2.ZAxis.X2;
					action.Interpolation2.ZAxis.Y1 = rawFrame.Interpolation2.ZAxis.Y1;
					action.Interpolation2.ZAxis.Y2 = rawFrame.Interpolation2.ZAxis.Y2;
					action.Interpolation2.Rotation.X1 = rawFrame.Interpolation2.Rotation.X1;
					action.Interpolation2.Rotation.X2 = rawFrame.Interpolation2.Rotation.X2;
					action.Interpolation2.Rotation.Y1 = rawFrame.Interpolation2.Rotation.Y1;
					action.Interpolation2.Rotation.Y2 = rawFrame.Interpolation2.Rotation.Y2;
					action.Interpolation3.XAxis.X1 = rawFrame.Interpolation3.XAxis.X1;
					action.Interpolation3.XAxis.X2 = rawFrame.Interpolation3.XAxis.X2;
					action.Interpolation3.XAxis.Y1 = rawFrame.Interpolation3.XAxis.Y1;
					action.Interpolation3.XAxis.Y2 = rawFrame.Interpolation3.XAxis.Y2;
					action.Interpolation3.YAxis.X1 = rawFrame.Interpolation3.YAxis.X1;
					action.Interpolation3.YAxis.X2 = rawFrame.Interpolation3.YAxis.X2;
					action.Interpolation3.YAxis.Y1 = rawFrame.Interpolation3.YAxis.Y1;
					action.Interpolation3.YAxis.Y2 = rawFrame.Interpolation3.YAxis.Y2;
					action.Interpolation3.ZAxis.X1 = rawFrame.Interpolation3.ZAxis.X1;
					action.Interpolation3.ZAxis.X2 = rawFrame.Interpolation3.ZAxis.X2;
					action.Interpolation3.ZAxis.Y1 = rawFrame.Interpolation3.ZAxis.Y1;
					action.Interpolation3.ZAxis.Y2 = rawFrame.Interpolation3.ZAxis.Y2;
					action.Interpolation3.Rotation.X1 = rawFrame.Interpolation3.Rotation.X1;
					action.Interpolation3.Rotation.X2 = rawFrame.Interpolation3.Rotation.X2;
					action.Interpolation3.Rotation.Y1 = rawFrame.Interpolation3.Rotation.Y1;
					action.Interpolation3.Rotation.Y2 = rawFrame.Interpolation3.Rotation.Y2;
					action.Interpolation4.XAxis.X1 = rawFrame.Interpolation4.XAxis.X1;
					action.Interpolation4.XAxis.X2 = rawFrame.Interpolation4.XAxis.X2;
					action.Interpolation4.XAxis.Y1 = rawFrame.Interpolation4.XAxis.Y1;
					action.Interpolation4.XAxis.Y2 = rawFrame.Interpolation4.XAxis.Y2;
					action.Interpolation4.YAxis.X1 = rawFrame.Interpolation4.YAxis.X1;
					action.Interpolation4.YAxis.X2 = rawFrame.Interpolation4.YAxis.X2;
					action.Interpolation4.YAxis.Y1 = rawFrame.Interpolation4.YAxis.Y1;
					action.Interpolation4.YAxis.Y2 = rawFrame.Interpolation4.YAxis.Y2;
					action.Interpolation4.ZAxis.X1 = rawFrame.Interpolation4.ZAxis.X1;
					action.Interpolation4.ZAxis.X2 = rawFrame.Interpolation4.ZAxis.X2;
					action.Interpolation4.ZAxis.Y1 = rawFrame.Interpolation4.ZAxis.Y1;
					action.Interpolation4.ZAxis.Y2 = rawFrame.Interpolation4.ZAxis.Y2;
					action.Interpolation4.Rotation.X1 = rawFrame.Interpolation4.Rotation.X1;
					action.Interpolation4.Rotation.X2 = rawFrame.Interpolation4.Rotation.X2;
					action.Interpolation4.Rotation.Y1 = rawFrame.Interpolation4.Rotation.Y1;
					action.Interpolation4.Rotation.Y2 = rawFrame.Interpolation4.Rotation.Y2;
					#endregion

					frame.Actions.Add(action);
				}
				else if (rawFrame.Type == UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameType.FaceReposition)
				{
					MotionFaceRepositionAction action = new MotionFaceRepositionAction();
					action.FaceName = rawFrame.BoneName;
					action.Weight = (float)rawFrame.Position.X;
					frame.Actions.Add(action);
				}
			}
			frame.Index = index;
			motion.Frames.Add(frame);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			MotionObjectModel motion = (objectModel as MotionObjectModel);
			if (motion == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = Encoding.ShiftJIS;
			Writer bw = base.Accessor.Writer;
			int modelNameLength = 10;
			switch (Version.Major)
			{
				case 1:
				{
					bw.WriteFixedLengthString("Vocaloid Motion Data file", 30);
					modelNameLength = 10;
					break;
				}
				case 2:
				{
					bw.WriteFixedLengthString("Vocaloid Motion Data 0002", 30);
					modelNameLength = 20;
					break;
				}
				default:
				{
					throw new NotSupportedException("The version " + Version.ToString() + " is not supported");
				}
			}

			string modelName = String.Empty;
			if (motion.CompatibleModelNames.Count > 0)
			{
				modelName = motion.CompatibleModelNames[0];
			}
			bw.WriteFixedLengthString(modelName, modelNameLength);

			System.Collections.Generic.List<UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame> rawFrames = new System.Collections.Generic.List<UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame>();
			foreach (UniversalEditor.ObjectModels.Multimedia3D.Motion.MotionFrame frame in motion.Frames)
			{
				foreach (MotionAction action in frame.Actions)
				{
					if (action is MotionBoneRepositionAction)
					{
						MotionBoneRepositionAction repos = (action as MotionBoneRepositionAction);

						UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame rawFrame = new UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame();
						rawFrame.Index = frame.Index;
						rawFrame.BoneName = repos.BoneName;
						rawFrame.Position = repos.Position;
						rawFrame.Rotation = repos.Rotation;

						#region Interpolation Data
						rawFrame.Interpolation.XAxis.X1 = repos.Interpolation.XAxis.X1;
						rawFrame.Interpolation.XAxis.X2 = repos.Interpolation.XAxis.X2;
						rawFrame.Interpolation.XAxis.Y1 = repos.Interpolation.XAxis.Y1;
						rawFrame.Interpolation.XAxis.Y2 = repos.Interpolation.XAxis.Y2;
						rawFrame.Interpolation.YAxis.X1 = repos.Interpolation.YAxis.X1;
						rawFrame.Interpolation.YAxis.X2 = repos.Interpolation.YAxis.X2;
						rawFrame.Interpolation.YAxis.Y1 = repos.Interpolation.YAxis.Y1;
						rawFrame.Interpolation.YAxis.Y2 = repos.Interpolation.YAxis.Y2;
						rawFrame.Interpolation.ZAxis.X1 = repos.Interpolation.ZAxis.X1;
						rawFrame.Interpolation.ZAxis.X2 = repos.Interpolation.ZAxis.X2;
						rawFrame.Interpolation.ZAxis.Y1 = repos.Interpolation.ZAxis.Y1;
						rawFrame.Interpolation.ZAxis.Y2 = repos.Interpolation.ZAxis.Y2;
						rawFrame.Interpolation.Rotation.X1 = repos.Interpolation.Rotation.X1;
						rawFrame.Interpolation.Rotation.X2 = repos.Interpolation.Rotation.X2;
						rawFrame.Interpolation.Rotation.Y1 = repos.Interpolation.Rotation.Y1;
						rawFrame.Interpolation.Rotation.Y2 = repos.Interpolation.Rotation.Y2;
						rawFrame.Interpolation2.XAxis.X1 = repos.Interpolation2.XAxis.X1;
						rawFrame.Interpolation2.XAxis.X2 = repos.Interpolation2.XAxis.X2;
						rawFrame.Interpolation2.XAxis.Y1 = repos.Interpolation2.XAxis.Y1;
						rawFrame.Interpolation2.XAxis.Y2 = repos.Interpolation2.XAxis.Y2;
						rawFrame.Interpolation2.YAxis.X1 = repos.Interpolation2.YAxis.X1;
						rawFrame.Interpolation2.YAxis.X2 = repos.Interpolation2.YAxis.X2;
						rawFrame.Interpolation2.YAxis.Y1 = repos.Interpolation2.YAxis.Y1;
						rawFrame.Interpolation2.YAxis.Y2 = repos.Interpolation2.YAxis.Y2;
						rawFrame.Interpolation2.ZAxis.X1 = repos.Interpolation2.ZAxis.X1;
						rawFrame.Interpolation2.ZAxis.X2 = repos.Interpolation2.ZAxis.X2;
						rawFrame.Interpolation2.ZAxis.Y1 = repos.Interpolation2.ZAxis.Y1;
						rawFrame.Interpolation2.ZAxis.Y2 = repos.Interpolation2.ZAxis.Y2;
						rawFrame.Interpolation2.Rotation.X1 = repos.Interpolation2.Rotation.X1;
						rawFrame.Interpolation2.Rotation.X2 = repos.Interpolation2.Rotation.X2;
						rawFrame.Interpolation2.Rotation.Y1 = repos.Interpolation2.Rotation.Y1;
						rawFrame.Interpolation2.Rotation.Y2 = repos.Interpolation2.Rotation.Y2;
						rawFrame.Interpolation3.XAxis.X1 = repos.Interpolation3.XAxis.X1;
						rawFrame.Interpolation3.XAxis.X2 = repos.Interpolation3.XAxis.X2;
						rawFrame.Interpolation3.XAxis.Y1 = repos.Interpolation3.XAxis.Y1;
						rawFrame.Interpolation3.XAxis.Y2 = repos.Interpolation3.XAxis.Y2;
						rawFrame.Interpolation3.YAxis.X1 = repos.Interpolation3.YAxis.X1;
						rawFrame.Interpolation3.YAxis.X2 = repos.Interpolation3.YAxis.X2;
						rawFrame.Interpolation3.YAxis.Y1 = repos.Interpolation3.YAxis.Y1;
						rawFrame.Interpolation3.YAxis.Y2 = repos.Interpolation3.YAxis.Y2;
						rawFrame.Interpolation3.ZAxis.X1 = repos.Interpolation3.ZAxis.X1;
						rawFrame.Interpolation3.ZAxis.X2 = repos.Interpolation3.ZAxis.X2;
						rawFrame.Interpolation3.ZAxis.Y1 = repos.Interpolation3.ZAxis.Y1;
						rawFrame.Interpolation3.ZAxis.Y2 = repos.Interpolation3.ZAxis.Y2;
						rawFrame.Interpolation3.Rotation.X1 = repos.Interpolation3.Rotation.X1;
						rawFrame.Interpolation3.Rotation.X2 = repos.Interpolation3.Rotation.X2;
						rawFrame.Interpolation3.Rotation.Y1 = repos.Interpolation3.Rotation.Y1;
						rawFrame.Interpolation3.Rotation.Y2 = repos.Interpolation3.Rotation.Y2;
						rawFrame.Interpolation4.XAxis.X1 = repos.Interpolation4.XAxis.X1;
						rawFrame.Interpolation4.XAxis.X2 = repos.Interpolation4.XAxis.X2;
						rawFrame.Interpolation4.XAxis.Y1 = repos.Interpolation4.XAxis.Y1;
						rawFrame.Interpolation4.XAxis.Y2 = repos.Interpolation4.XAxis.Y2;
						rawFrame.Interpolation4.YAxis.X1 = repos.Interpolation4.YAxis.X1;
						rawFrame.Interpolation4.YAxis.X2 = repos.Interpolation4.YAxis.X2;
						rawFrame.Interpolation4.YAxis.Y1 = repos.Interpolation4.YAxis.Y1;
						rawFrame.Interpolation4.YAxis.Y2 = repos.Interpolation4.YAxis.Y2;
						rawFrame.Interpolation4.ZAxis.X1 = repos.Interpolation4.ZAxis.X1;
						rawFrame.Interpolation4.ZAxis.X2 = repos.Interpolation4.ZAxis.X2;
						rawFrame.Interpolation4.ZAxis.Y1 = repos.Interpolation4.ZAxis.Y1;
						rawFrame.Interpolation4.ZAxis.Y2 = repos.Interpolation4.ZAxis.Y2;
						rawFrame.Interpolation4.Rotation.X1 = repos.Interpolation4.Rotation.X1;
						rawFrame.Interpolation4.Rotation.X2 = repos.Interpolation4.Rotation.X2;
						rawFrame.Interpolation4.Rotation.Y1 = repos.Interpolation4.Rotation.Y1;
						rawFrame.Interpolation4.Rotation.Y2 = repos.Interpolation4.Rotation.Y2;
						#endregion

						rawFrames.Add(rawFrame);
					}
				}
			}

			bw.WriteUInt32((uint)rawFrames.Count);

			System.Collections.Generic.List<UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame> rawFaceFrames = new System.Collections.Generic.List<UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame>();

			foreach (UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame rawFrame in rawFrames)
			{
				if (rawFrame.Type == UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameType.BoneReposition)
				{
					bw.WriteFixedLengthString(rawFrame.BoneName, 15);

					bw.WriteUInt32(rawFrame.Index);

					bw.WriteSingle((float)rawFrame.Position.X);
					bw.WriteSingle((float)rawFrame.Position.Y);
					bw.WriteSingle((float)rawFrame.Position.Z);

					bw.WriteSingle((float)rawFrame.Rotation.X);
					bw.WriteSingle((float)rawFrame.Rotation.Y);
					bw.WriteSingle((float)rawFrame.Rotation.Z);
					bw.WriteSingle((float)rawFrame.Rotation.W);

					#region Interpolation Data
					// special thanks to Re:VB-P (animiku) for helping me understand this
					bw.WriteByte((byte)rawFrame.Interpolation.XAxis.X1);       // X-axis interpolation bezier curve: P1x   begin point of Bezier curve P1 (x , y)
					bw.WriteByte((byte)rawFrame.Interpolation.YAxis.X1);       // Y-axis interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation.ZAxis.X1);       // Z-axis interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation.Rotation.X1);    // Rotation interpolation bezier curve: P1x

					bw.WriteByte((byte)rawFrame.Interpolation.XAxis.Y1);       // X-axis interpolation bezier curve: P1y   begin point of Bezier curve P1 (x , y)
					bw.WriteByte((byte)rawFrame.Interpolation.YAxis.Y1);       // Y-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation.ZAxis.Y1);       // Z-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation.Rotation.Y1);    // Rotation interpolation bezier curve: P1y

					bw.WriteByte((byte)rawFrame.Interpolation.XAxis.X2);       // X-axis interpolation bezier curve: P2x   end point of Bezier curve P2 (x , y)
					bw.WriteByte((byte)rawFrame.Interpolation.YAxis.X2);       // Y-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation.ZAxis.X2);       // Z-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation.Rotation.X2);    // Rotation interpolation bezier curve: P2x

					bw.WriteByte((byte)rawFrame.Interpolation.XAxis.Y2);       // X-axis interpolation bezier curve: P2y   end point of Bezier curve P2 (x , y)
					bw.WriteByte((byte)rawFrame.Interpolation.YAxis.Y2);       // Y-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation.ZAxis.Y2);       // Z-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation.Rotation.Y2);    // Rotation interpolation bezier curve: P2y	    	---following is emptiness??

					bw.WriteByte((byte)rawFrame.Interpolation2.YAxis.X1);      // Y-axis interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation2.ZAxis.X1);      // Z-axis interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation2.Rotation.X1);   // Rotation interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation2.XAxis.Y1);      // X-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation2.YAxis.Y1);      // Y-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation2.ZAxis.Y1);      // Z-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation2.Rotation.Y1);   // Rotation interpolation bezier curve: P1y

					bw.WriteByte((byte)rawFrame.Interpolation2.XAxis.X2);      // X-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation2.YAxis.X2);      // Y-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation2.ZAxis.X2);      // Z-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation2.Rotation.X2);   // Rotation interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation2.XAxis.Y2);      // X-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation2.YAxis.Y2);      // Y-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation2.ZAxis.Y2);      // Z-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation2.Rotation.Y2);   // Rotation interpolation bezier curve: P2y

					bw.WriteByte((byte)1);

					bw.WriteByte((byte)rawFrame.Interpolation3.ZAxis.X1);      // Z-axis interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation3.Rotation.X1);   // Rotation interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation3.XAxis.Y1);      // X-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation3.YAxis.Y1);      // Y-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation3.ZAxis.Y1);      // Z-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation3.Rotation.Y1);   // Rotation interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation3.XAxis.X2);      // X-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation3.YAxis.X2);      // Y-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation3.ZAxis.X2);      // Z-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation3.Rotation.X2);   // Rotation interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation3.XAxis.Y2);      // X-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation3.YAxis.Y2);      // Y-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation3.ZAxis.Y2);      // Z-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation3.Rotation.Y2);   // Rotation interpolation bezier curve: P2y

					bw.WriteByte((byte)1);
					bw.WriteByte((byte)0);

					bw.WriteByte((byte)rawFrame.Interpolation4.Rotation.X1);   // Rotation interpolation bezier curve: P1x
					bw.WriteByte((byte)rawFrame.Interpolation4.XAxis.Y1);      // X-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation4.YAxis.Y1);      // Y-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation4.ZAxis.Y1);      // Z-axis interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation4.Rotation.Y1);   // Rotation interpolation bezier curve: P1y
					bw.WriteByte((byte)rawFrame.Interpolation4.XAxis.X2);      // X-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation4.YAxis.X2);      // Y-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation4.ZAxis.X2);      // Z-axis interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation4.Rotation.X2);   // Rotation interpolation bezier curve: P2x
					bw.WriteByte((byte)rawFrame.Interpolation4.XAxis.Y2);      // X-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation4.YAxis.Y2);      // Y-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation4.ZAxis.Y2);      // Z-axis interpolation bezier curve: P2y
					bw.WriteByte((byte)rawFrame.Interpolation4.Rotation.Y2);   // Rotation interpolation bezier curve: P2y

					bw.WriteByte((byte)1);
					bw.WriteByte((byte)0);
					bw.WriteByte((byte)0);
					#endregion
				}
				else if (rawFrame.Type == UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrameType.FaceReposition)
				{
					rawFaceFrames.Add(rawFrame);
				}
			}

			uint numTotalFaceKeyframes = (uint)rawFaceFrames.Count;
			bw.WriteUInt32(numTotalFaceKeyframes);

			for (uint iFaceKeyframe = 0; iFaceKeyframe < numTotalFaceKeyframes; iFaceKeyframe++)
			{
				UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal.RawMotionFrame rawFrame = rawFaceFrames[(int)iFaceKeyframe];

				bw.WriteFixedLengthString(rawFrame.BoneName, 15);
				bw.WriteUInt32(rawFrame.Index);
				bw.WriteSingle((float)rawFrame.Position.X);
			}

			bw.Flush();
		}
	}
}
