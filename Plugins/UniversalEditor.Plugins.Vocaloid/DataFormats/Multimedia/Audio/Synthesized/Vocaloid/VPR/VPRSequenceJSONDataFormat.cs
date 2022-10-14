//
//  VPRSequenceJSONDataFormat.cs
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
using System.Collections.Generic;
using MBS.Framework.Settings;
using UniversalEditor.DataFormats.PropertyList.JavaScriptObjectNotation;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.Plugins.Vocaloid.DataFormats.Multimedia.Audio.Synthesized.Vocaloid.VPR
{
	public class VPRSequenceJSONDataFormat : JSONDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(Vendor), "_Vendor", "Yamaha Corporation"));
			}
			return _dfr;
		}

		public string Vendor { get; set; } = "Yamaha Corporation";

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			SynthesizedAudioObjectModel vsq = (objectModels.Pop() as SynthesizedAudioObjectModel);

			if (plom.Items.Count < 0)
				throw new InvalidDataFormatException();

			Group g = (plom.Items[0] as Group);
			Group gVersion = g.Items.OfType<Group>("version");

			Property pTracks = g.Items.OfType<Property>("tracks");
			object[] gTracks = pTracks.Value as object[];
			if (gTracks == null) throw new InvalidDataFormatException("tracks is not a Group[]");

			for (int i = 0; i < gTracks.Length; i++)
			{
				Group gTrack = gTracks[i] as Group;
				if (gTrack == null) throw new InvalidDataFormatException("gTrack is not a Group");

				SynthesizedAudioTrack track = new SynthesizedAudioTrack();

				Property pParts = gTrack.Items.OfType<Property>("parts");
				object[] gParts = pParts?.Value as object[];
				if (gParts == null) throw new InvalidDataFormatException("parts is not a Group[]");

				for (int j = 0; j < gParts.Length; j++)
				{
					object[] pNotes = ((gParts[j] as Group).Items?.OfType<Property>("notes"))?.Value as object[];
					if (pNotes == null) throw new InvalidDataFormatException("notes");

					for (int k = 0; k < pNotes.Length; k++)
					{
						Group gNote = pNotes[k] as Group;

						SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();

						Property pLyric = gNote.Items.OfType<Property>("lyric");
						note.Lyric = (string)pLyric.Value;

						Property pPhoneme = gNote.Items.OfType<Property>("phoneme");
						note.Phoneme = (string)pPhoneme.Value;

						Property pIsProtected = gNote.Items.OfType<Property>("isProtected");
						note.Protected = (bool)pIsProtected.Value;

						Property pPos = gNote.Items.OfType<Property>("pos");
						// FIXME: we can't take object (short) and convert using (int)
						note.Position = Convert.ToInt32(pPos.Value);

						Property pDuration = gNote.Items.OfType<Property>("duration");
						note.Length = Convert.ToInt32(pDuration.Value);

						Property pNumber = gNote.Items.OfType<Property>("number");
						note.Frequency = Convert.ToInt32(pNumber.Value);

						Property pVelocity = gNote.Items.OfType<Property>("velocity");

						Group gExp = gNote.Items.OfType<Group>("exp");
						Property pExpOpening = gExp.Items.OfType<Property>("opening");
						note.Opening = Convert.ToInt32(pExpOpening.Value);

						Group dvqm = gNote.Items.OfType<Group>("dvqm");
						Group dvqmRelease = dvqm.Items.OfType<Group>("release");
						Property dvqmReleaseCompID = dvqmRelease.Items.OfType<Property>("compID");

						Group gVibrato = gNote.Items.OfType<Group>("vibrato");
						Property pVibratoType = gVibrato.Items.OfType<Property>("type");
						note.VibratoType = (SynthesizedAudioVibratoType) Convert.ToInt32(pVibratoType.Value);
						Property pVibratoDuration = gVibrato.Items.OfType<Property>("duration");
						note.VibratoLength = Convert.ToInt32(pVibratoDuration.Value);

						track.Commands.Add(note);
					}
				}

				vsq.Tracks.Add(track);
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			SynthesizedAudioObjectModel vsq = (objectModels.Pop() as SynthesizedAudioObjectModel);

			PropertyListObjectModel plom = new PropertyListObjectModel();
			plom.Items.AddRange(new PropertyListItem[]
			{
				new Group("version", new PropertyListItem[]
				{
					new Property("major", 5),
					new Property("minor", 0),
					new Property("revision", 0)
				}),
				new Property("vender", Vendor),
				new Property("title", vsq.Information.SongTitle),
				new Group("masterTrack", new PropertyListItem[]
				{
					new Property("samplingRate", 44100),
					new Group("loop", new PropertyListItem[]
					{
						new Property("isEnabled", false),
						new Property("begin", 0),
						new Property("end", 0)
					})
				}),
				new Property("voices", new Group[]
				{
					new Group("", new PropertyListItem[]
					{
						new Property("compID", "BCNFCY43LB2LZCD4"),
						new Property("name", "MIKU_V4X_Original_EVEC")
					})
				}),
				new Property("tracks", new Group[]
				{
					new Group("", new PropertyListItem[]
					{
						new Property("type", 0),
						new Property("name", "MIKU_V4X_ORIGINAL_EVEC"),
						new Property("color", 0),
						new Property("busNo", 0),
						new Property("isFolded", false),
						new Property("height", 0.0),
						new Group("volume", new PropertyListItem[]
						{
							new Property("isFolded", true),
							new Property("height", 0.0),
							new Property("events", new Group[]
							{
								new Group("", new PropertyListItem[]
								{
									new Property("pos" , 0.0),
									new Property("value", 0.0)
								})
							})
						})
					})
				})
			});
			objectModels.Push(plom);
		}
	}
}
