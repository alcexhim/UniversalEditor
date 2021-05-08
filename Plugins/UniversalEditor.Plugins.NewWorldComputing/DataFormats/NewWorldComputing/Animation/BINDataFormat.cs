//
//  BINDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Multimedia.SpriteAnimationCollection;

namespace UniversalEditor.DataFormats.NewWorldComputing.Animation
{
	public class BINDataFormat : DataFormat
	{
		public const byte IDLE_ANIMATION_COUNT_MAX = 5;
		public const byte PROJECTILE_ANGLE_MAX = 12;
		public const byte FRAME_MAX = 16;

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SpriteAnimationCollectionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SpriteAnimationCollectionObjectModel anim = (objectModel as SpriteAnimationCollectionObjectModel);
			if (anim == null)
				throw new ObjectModelNotSupportedException();

			// HACK: because there simply is no better way to differentiate between window layout BINs and animation BINs (except maybe filename ending in FRM.BIN)
			if (!(Accessor.Length == 821 && Accessor.GetFileName().EndsWith("FRM.BIN")))
			{
				throw new InvalidDataFormatException(); // to kick it to the *other* BIN format
			}

			Reader reader = Accessor.Reader;

			byte one = reader.ReadByte();

			short eyePositionX = reader.ReadInt16();
			short eyePositionY = reader.ReadInt16();

			// frame offsets for the future use
			int[][] moveOffsets = new int[7][];
			for (int i = 0; i < 7; i++)
			{
				int[] moveOffset = new int[16];
	            for ( int frame = 0; frame < 16; ++frame)
	            {
					moveOffset[frame] = reader.ReadByte();
	            }
				moveOffsets[i] = moveOffset;
			}

			// at offset 117
			byte idleAnimationCount = reader.ReadByte();
			float[] idleAnimationPriorities = new float[IDLE_ANIMATION_COUNT_MAX];
			uint[] unusedIdleDelays = new uint[IDLE_ANIMATION_COUNT_MAX];
			for (byte i = 0; i < IDLE_ANIMATION_COUNT_MAX; i++)
			{
				idleAnimationPriorities[i] = reader.ReadSingle();
			}
			for (byte i = 0; i < IDLE_ANIMATION_COUNT_MAX; i++)
			{
				unusedIdleDelays[i] = reader.ReadUInt32();
			}

			// at offset 158
			uint idleAnimationDelay = reader.ReadUInt32();

			uint moveSpeed = reader.ReadUInt32();
			uint shootSpeed = reader.ReadUInt32();
			uint flightSpeed = reader.ReadUInt32();

			// projectile data
			for (int i = 0; i < 3; i++)
			{
				short projectileOffsetX = reader.ReadInt16();
				short projectileOffsetY = reader.ReadInt16();
			}

			// describes how to rotate the projectile when
			byte projectileAngleCount = reader.ReadByte();
			for (byte i = 0; i < PROJECTILE_ANGLE_MAX; i++)
			{
				// even though there are only {projectileAngleCount} items, the rest of PROJECTILE_ANGLE_MAX is filled with zeroes
				float projectileAngle = reader.ReadSingle();
			}

			// Positional offsets for sprites & drawing
			uint troopCountOffsetLeft = reader.ReadUInt32();
			uint troopCountOffsetRight = reader.ReadUInt32();

			// Load animation sequences themselves
			int nMaxAnimationTypes = Enum.GetValues(typeof(BINAnimationType)).Length;

			byte[] animCounts = new byte[nMaxAnimationTypes];
			byte[][] animFrames = new byte[nMaxAnimationTypes][];

			SpriteAnimation[] anims = new SpriteAnimation[nMaxAnimationTypes];
			for (int idx = 0; idx < nMaxAnimationTypes; ++idx)
			{
				animCounts[idx] = reader.ReadByte();

				anims[idx] = new SpriteAnimation();
				anims[idx].Name = Enum.GetName(typeof(BINAnimationType), idx);
			}
			for (int idx = 0; idx < nMaxAnimationTypes; ++idx)
			{
				animFrames[idx] = new byte[FRAME_MAX];
				for (byte frame = 0; frame < FRAME_MAX; frame++)
				{
					animFrames[idx][frame] = reader.ReadByte();
				}
				for (byte frame = 0; frame < animCounts[idx]; frame++)
				{
					anims[idx].Frames.Add(animFrames[idx][frame]);
				}
				anim.Animations.Add(anims[idx]);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
