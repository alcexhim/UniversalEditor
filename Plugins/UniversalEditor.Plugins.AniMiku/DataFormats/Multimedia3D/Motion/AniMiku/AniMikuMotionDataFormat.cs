//
//  AniMikuMotionDataFormat.cs - implements the AniMiku motion data format (AMD)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2014-2020 Mike Becker's Software
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
using System.Collections.Generic;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Motion;

namespace UniversalEditor.DataFormats.Multimedia3D.Motion.AniMiku
{
	/// <summary>
	/// Implements the AniMiku motion data format (AMD).
	/// </summary>
	public class AniMikuMotionDataFormat : DataFormat
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

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            Reader br = new Reader(base.Accessor);
            base.Accessor.DefaultEncoding = Encoding.UTF8;
            br.Accessor.Position = 0;

            MotionObjectModel motion = (objectModel as MotionObjectModel);
            string amd = br.ReadFixedLengthString(3);
            if (amd != "amd") throw new InvalidDataFormatException("File does not begin with \"amd\"");

            float version = br.ReadSingle();
            if (version != 1.0) Console.WriteLine("AniMiku: do not know how to parse version " + version.ToString());

            uint boneCount = br.ReadUInt32();
            for (uint i = 0; i < boneCount; i++)
            {
                string boneName = br.ReadLengthPrefixedString();
                uint frameCount = br.ReadUInt32();

                for (uint j = 0; j < frameCount; j++)
                {
                    uint frameIndex = br.ReadUInt32();

                    MotionFrame frame = new MotionFrame();
                    frame.Index = frameIndex;

                    float px = br.ReadSingle();
                    float py = br.ReadSingle();
                    float pz = br.ReadSingle();

                    float rx = br.ReadSingle();
                    float ry = br.ReadSingle();
                    float rz = br.ReadSingle();
                    float rw = br.ReadSingle();

                    byte[] interpolationData = br.ReadBytes(64);

                    MotionBoneRepositionAction repos = new MotionBoneRepositionAction();
                    repos.BoneName = boneName;
                    repos.Position = new PositionVector3(px, py, pz);
                    repos.Rotation = new PositionVector4(rx, ry, rz, rw);

                    frame.Actions.Add(repos);
                    motion.Frames.Add(frame);
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
