//
//  TD0DataFormat.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using UniversalEditor.Plugins.Roland.ObjectModels.VDrumKit;

namespace UniversalEditor.Plugins.Roland.DataFormats.VDrumKit
{
	public class TD0DataFormat : DataFormat
	{
		public string SetName { get; set; }

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(VDrumKitObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VDrumKitObjectModel vdk = (objectModel as VDrumKitObjectModel);
			if (vdk == null) throw new ObjectModelNotSupportedException();

			SetName = Accessor.Reader.ReadFixedLengthString(12);

			string vers = Accessor.Reader.ReadFixedLengthString(4);
			if (vers != "1.08")
				throw new InvalidDataFormatException();

			byte[] twentyZeroes = Accessor.Reader.ReadBytes(20);

			ushort u0 = Accessor.Reader.ReadUInt16();
			ushort u1 = Accessor.Reader.ReadUInt16();
			ushort u2 = Accessor.Reader.ReadUInt16();
			ushort u3 = Accessor.Reader.ReadUInt16();

			byte[][] chainStates = new byte[16][];
			for (int i = 0; i < 16; i++)
			{
				// From "TD-20 User Manual", Chapter 11:
				//
				// Drum Kit Chain allows you to step through the drum kits of
				// your choice and in the order you want. The TD-20 lets you
				// create and store 16 different chains of up to 32 steps each.

				string chainName = Accessor.Reader.ReadFixedLengthString(12);
				byte[] chainState = Accessor.Reader.ReadBytes(32);
				uint unknown2 = Accessor.Reader.ReadUInt32();

				chainStates[i] = chainState;

				DrumKitChain chain = new DrumKitChain();
				chain.Name = chainName;
				vdk.Chains.Add(chain);
			}

			// skip the bullshit
			Accessor.Reader.Seek(268, IO.SeekOrigin.Current);

			string ojs = Accessor.Reader.ReadFixedLengthString(12);
			List<string> factoryResetNames = new List<string>();
			for (int i = 0; i < 4; i++)
			{
				string nam = Accessor.Reader.ReadFixedLengthString(12);
				Accessor.Reader.Seek(1600, IO.SeekOrigin.Current);
				factoryResetNames.Add(nam);
			}

			for (int i = 0; i < 50; i++)
			{
				DrumKit kit = new DrumKit();

				// each kit info is 2188 bytes
				kit.Name = Accessor.Reader.ReadFixedLengthString(12);
				// ushort us00 = Accessor.Reader.ReadUInt16(); // 0x78 0x00
				// ushort us01 = Accessor.Reader.ReadUInt16(); // 0x00 0x00 or 0x80 0x00


				int maxSamples = 30; // 15 each for Head and Rim
									 // samples start at @7700
				Accessor.Reader.Seek(132, IO.SeekOrigin.Current);

				uint unkw1 = Accessor.Reader.ReadUInt32();

				for (int j = 0; j < maxSamples; j++)
				{
					DrumKitInstrument inst = new DrumKitInstrument();
					inst.SampleFileName = Accessor.Reader.ReadFixedLengthString(12);

					// each sample info is 68 bytes
					// for each sample
					inst.SampleIndex = Accessor.Reader.ReadUInt16(); // 0-556...
					ushort unk1 = Accessor.Reader.ReadUInt16(); // 01
					ushort unk2 = Accessor.Reader.ReadUInt16(); // 78
					ushort unk3 = Accessor.Reader.ReadUInt16(); // 80
					ushort unk4 = Accessor.Reader.ReadUInt16(); // 25630
					ushort unk5 = Accessor.Reader.ReadUInt16(); // 0
					ushort unk6 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk7 = Accessor.Reader.ReadUInt16(); // 272
					ushort unk8 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk9 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk10 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk11 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk12 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk13 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk14 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk15 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk16 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk17 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk18 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk19 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk20 = Accessor.Reader.ReadUInt16(); // 292
					ushort unk21 = Accessor.Reader.ReadUInt16(); // 292

					// @7744
					short tuning = Accessor.Reader.ReadInt16(); // -480 to 480?
					TD0HeadType headType = (TD0HeadType)Accessor.Reader.ReadByte();
					TD0MicrophonePosition micPosition = (TD0MicrophonePosition)Accessor.Reader.ReadByte(); // 0 - outside2, 1 - outside1, 2 - standard, 3 - inside1, 4 - inside2
					TD0MuffleType muffling = (TD0MuffleType)Accessor.Reader.ReadByte(); // 0 - off, 1 - tape1, 2 - tape2, 3 - blanket, 4 - weight
					TD0DrumDepth depth = (TD0DrumDepth)Accessor.Reader.ReadByte(); // 0 - normal, 1 - deep1, 2 - deep2

					byte buzz = Accessor.Reader.ReadByte(); // 0 - off, 1-8
															// for bass drum: beater type (0 - felt, 1 - wood, 2 - plastic)
					TD0BeaterType beaterType = (TD0BeaterType)Accessor.Reader.ReadByte();
					uint unkown = Accessor.Reader.ReadUInt32(); // 0
					kit.Instruments.Add(inst);
				}

				vdk.DrumKits.Add(kit);
			}

			// now that our kits are in, update the chains
			for (int i = 0; i < vdk.Chains.Count; i++)
			{
				for (int j = 0; j < chainStates[i].Length; j++)
				{
					if (chainStates[i][j] >= 0 && chainStates[i][j] < vdk.DrumKits.Count)
					{
						vdk.Chains[i].Kits.Add(vdk.DrumKits[chainStates[i][j]]);
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
