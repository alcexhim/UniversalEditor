using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker.Motion
{
    /// <summary>
    /// Motion data loads from and saves to MikuMikuDance "Vocaloid Motion Data" (VMD) file format.
    /// </summary>
	public class VMDMotionDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("MikuMikuDance v1.30 motion data", new byte?[][] { new byte?[] { (byte)'V', (byte)'o', (byte)'c', (byte)'a', (byte)'l', (byte)'o', (byte)'i', (byte)'d', (byte)' ', (byte)'M', (byte)'o', (byte)'t', (byte)'i', (byte)'o', (byte)'n', (byte)' ', (byte)'D', (byte)'a', (byte)'t', (byte)'a', (byte)' ', (byte)'f', (byte)'i', (byte)'l', (byte)'e' } }, new string[] { "*.vmd" });
            dfr.Filters.Add("Polygon Movie Maker motion data", new byte?[][] { new byte?[] { (byte)'V', (byte)'o', (byte)'c', (byte)'a', (byte)'l', (byte)'o', (byte)'i', (byte)'d', (byte)' ', (byte)'M', (byte)'o', (byte)'t', (byte)'i', (byte)'o', (byte)'n', (byte)' ', (byte)'D', (byte)'a', (byte)'t', (byte)'a', (byte)' ', (byte)'0', (byte)'0', (byte)'0', (byte)'2' } }, new string[] { "*.vmd" });
            return dfr;
        }
        private Version mvarVersion = new Version(1, 0);
        public Version Version { get { return mvarVersion; } set { mvarVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
        {
            MotionObjectModel motion = (objectModel as MotionObjectModel);
            if (motion == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            string signature = br.ReadFixedLengthString(30);
            if (signature == "Vocaloid Motion Data file\0\0\0\0\0")
            {
                mvarVersion = new Version(1, 0);
            }
            else if (signature == "Vocaloid Motion Data 0002\0\0\0\0\0")
            {
                mvarVersion = new Version(2, 0);
            }
            else
            {
                throw new NotSupportedException("The signature " + signature + " is not supported");
            }

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("shift_jis");

            int modelNameSize = 10;
            if (mvarVersion.Major == 1)
            {
                modelNameSize = 10;
            }
            else if (mvarVersion.Major == 2)
            {
                modelNameSize = 20;
            }

            string modelName = br.ReadFixedLengthString(modelNameSize);
            int len = modelName.IndexOf('\0');
            if (len == -1) len = modelName.Length;
            modelName = modelName.Substring(0, len);
            byte[] modelNameBytes = System.Text.Encoding.Default.GetBytes(modelName);
            string modelNameInUTF8 = encoding.GetString(modelNameBytes);
            motion.CompatibleModelNames.Add(modelNameInUTF8);

            System.Collections.Generic.List<UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame> rawFrames = new System.Collections.Generic.List<ObjectModels.Motion.Internal.RawMotionFrame>();

            uint numBones = br.ReadUInt32();
            for (uint i = 0; i < numBones; i++)
            {
                UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame rawFrame = new UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame();

                string boneName = br.ReadFixedLengthString(15, encoding);
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
                string faceName = br.ReadFixedLengthString(15, encoding);
                if (faceName.Contains("\0"))
                {
                    faceName = faceName.Substring(0, faceName.IndexOf('\0'));
                }
                uint frameIndex = br.ReadUInt32();

				// Weight - It is used to scale how much a face morph should move a vertex based off of
				// the maximum possible coordinate that it can move by. (thanks Re:VB)
                float weight = br.ReadSingle();

                UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame rawFrame = new ObjectModels.Motion.Internal.RawMotionFrame();
                rawFrame.BoneName = faceName;
                rawFrame.Index = frameIndex;
                rawFrame.Position = new PositionVector3(weight, 0, 0);
                rawFrame.Type = ObjectModels.Motion.Internal.RawMotionFrameType.FaceReposition;
                rawFrames.Add(rawFrame);
            }

            UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrameComparer comparer = new UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrameComparer();
            rawFrames.Sort(comparer);

            uint index = 0;

            MotionFrame frame = new MotionFrame();
            foreach (UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame rawFrame in rawFrames)
            {
                if (rawFrame.Index != index)
                {
                    frame.Index = index;
                    motion.Frames.Add(frame);

                    frame = new MotionFrame();
                    index = rawFrame.Index;
                }

                if (rawFrame.Type == ObjectModels.Motion.Internal.RawMotionFrameType.BoneReposition)
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
                else if (rawFrame.Type == ObjectModels.Motion.Internal.RawMotionFrameType.FaceReposition)
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
            if (motion == null) return;

            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            int modelNameLength = 10;
            switch (mvarVersion.Major)
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
                    throw new NotSupportedException("The version " + mvarVersion.ToString() + " is not supported");
                }
            }

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("shift_jis");
            
            string modelName = String.Empty;
            if (motion.CompatibleModelNames.Count > 0)
            {
                modelName = motion.CompatibleModelNames[0];
            }
            bw.WriteFixedLengthString(modelName, System.Text.Encoding.GetEncoding("shift_jis"), modelNameLength);

            System.Collections.Generic.List<UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame> rawFrames = new System.Collections.Generic.List<ObjectModels.Motion.Internal.RawMotionFrame>();
            foreach (UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.MotionFrame frame in motion.Frames)
            {
                foreach (MotionAction action in frame.Actions)
                {
                    if (action is MotionBoneRepositionAction)
                    {
                        MotionBoneRepositionAction repos = (action as MotionBoneRepositionAction);

                        UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame rawFrame = new UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame();
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

            bw.Write((uint)rawFrames.Count);

            System.Collections.Generic.List<UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame> rawFaceFrames = new System.Collections.Generic.List<ObjectModels.Motion.Internal.RawMotionFrame>();

            foreach (UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion.Internal.RawMotionFrame rawFrame in rawFrames)
            {
                if (rawFrame.Type == ObjectModels.Motion.Internal.RawMotionFrameType.BoneReposition)
                {
                    bw.WriteFixedLengthString(rawFrame.BoneName, System.Text.Encoding.GetEncoding("shift_jis"), 15);

                    bw.Write(rawFrame.Index);

                    bw.Write((float)rawFrame.Position.X);
                    bw.Write((float)rawFrame.Position.Y);
                    bw.Write((float)rawFrame.Position.Z);

                    bw.Write((float)rawFrame.Rotation.X);
                    bw.Write((float)rawFrame.Rotation.Y);
                    bw.Write((float)rawFrame.Rotation.Z);
                    bw.Write((float)rawFrame.Rotation.W);

                    #region Interpolation Data
                    // special thanks to Re:VB (animiku) for helping me understand this
                    bw.Write((byte)rawFrame.Interpolation.XAxis.X1);       // X-axis interpolation bezier curve: P1x   begin point of Bezier curve P1 (x , y)
                    bw.Write((byte)rawFrame.Interpolation.YAxis.X1);       // Y-axis interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation.ZAxis.X1);       // Z-axis interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation.Rotation.X1);    // Rotation interpolation bezier curve: P1x

                    bw.Write((byte)rawFrame.Interpolation.XAxis.Y1);       // X-axis interpolation bezier curve: P1y   begin point of Bezier curve P1 (x , y)
                    bw.Write((byte)rawFrame.Interpolation.YAxis.Y1);       // Y-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation.ZAxis.Y1);       // Z-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation.Rotation.Y1);    // Rotation interpolation bezier curve: P1y

                    bw.Write((byte)rawFrame.Interpolation.XAxis.X2);       // X-axis interpolation bezier curve: P2x   end point of Bezier curve P2 (x , y)
                    bw.Write((byte)rawFrame.Interpolation.YAxis.X2);       // Y-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation.ZAxis.X2);       // Z-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation.Rotation.X2);    // Rotation interpolation bezier curve: P2x

                    bw.Write((byte)rawFrame.Interpolation.XAxis.Y2);       // X-axis interpolation bezier curve: P2y   end point of Bezier curve P2 (x , y)
                    bw.Write((byte)rawFrame.Interpolation.YAxis.Y2);       // Y-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation.ZAxis.Y2);       // Z-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation.Rotation.Y2);    // Rotation interpolation bezier curve: P2y	    	---following is emptiness??

                    bw.Write((byte)rawFrame.Interpolation2.YAxis.X1);      // Y-axis interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation2.ZAxis.X1);      // Z-axis interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation2.Rotation.X1);   // Rotation interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation2.XAxis.Y1);      // X-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation2.YAxis.Y1);      // Y-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation2.ZAxis.Y1);      // Z-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation2.Rotation.Y1);   // Rotation interpolation bezier curve: P1y

                    bw.Write((byte)rawFrame.Interpolation2.XAxis.X2);      // X-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation2.YAxis.X2);      // Y-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation2.ZAxis.X2);      // Z-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation2.Rotation.X2);   // Rotation interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation2.XAxis.Y2);      // X-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation2.YAxis.Y2);      // Y-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation2.ZAxis.Y2);      // Z-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation2.Rotation.Y2);   // Rotation interpolation bezier curve: P2y

                    bw.Write((byte)1);

                    bw.Write((byte)rawFrame.Interpolation3.ZAxis.X1);      // Z-axis interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation3.Rotation.X1);   // Rotation interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation3.XAxis.Y1);      // X-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation3.YAxis.Y1);      // Y-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation3.ZAxis.Y1);      // Z-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation3.Rotation.Y1);   // Rotation interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation3.XAxis.X2);      // X-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation3.YAxis.X2);      // Y-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation3.ZAxis.X2);      // Z-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation3.Rotation.X2);   // Rotation interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation3.XAxis.Y2);      // X-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation3.YAxis.Y2);      // Y-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation3.ZAxis.Y2);      // Z-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation3.Rotation.Y2);   // Rotation interpolation bezier curve: P2y

                    bw.Write((byte)1);
                    bw.Write((byte)0);

                    bw.Write((byte)rawFrame.Interpolation4.Rotation.X1);   // Rotation interpolation bezier curve: P1x
                    bw.Write((byte)rawFrame.Interpolation4.XAxis.Y1);      // X-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation4.YAxis.Y1);      // Y-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation4.ZAxis.Y1);      // Z-axis interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation4.Rotation.Y1);   // Rotation interpolation bezier curve: P1y
                    bw.Write((byte)rawFrame.Interpolation4.XAxis.X2);      // X-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation4.YAxis.X2);      // Y-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation4.ZAxis.X2);      // Z-axis interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation4.Rotation.X2);   // Rotation interpolation bezier curve: P2x
                    bw.Write((byte)rawFrame.Interpolation4.XAxis.Y2);      // X-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation4.YAxis.Y2);      // Y-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation4.ZAxis.Y2);      // Z-axis interpolation bezier curve: P2y
                    bw.Write((byte)rawFrame.Interpolation4.Rotation.Y2);   // Rotation interpolation bezier curve: P2y

                    bw.Write((byte)1);
                    bw.Write((byte)0);
                    bw.Write((byte)0);
                    #endregion
                }
                else if (rawFrame.Type == ObjectModels.Motion.Internal.RawMotionFrameType.FaceReposition)
                {
                    rawFaceFrames.Add(rawFrame);
                }
            }

            uint numTotalFaceKeyframes = (uint)rawFaceFrames.Count;
            bw.Write(numTotalFaceKeyframes);

            for (uint iFaceKeyframe = 0; iFaceKeyframe < numTotalFaceKeyframes; iFaceKeyframe++)
            {
                ObjectModels.Motion.Internal.RawMotionFrame rawFrame = rawFaceFrames[(int)iFaceKeyframe];

                bw.WriteFixedLengthString(rawFrame.BoneName, encoding, 15);
                bw.Write(rawFrame.Index);
                bw.Write((float)rawFrame.Position.X);
            }

            bw.Flush();
		}
	}
}
