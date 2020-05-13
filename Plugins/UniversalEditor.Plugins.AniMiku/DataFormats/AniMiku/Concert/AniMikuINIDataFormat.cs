//
//  AniMikuINIDataFormat.cs - implements the AniMiku performance data format
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
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.DataFormats.PropertyList;

using UniversalEditor.ObjectModels.PropertyList;

using UniversalEditor.ObjectModels.Concertroid;
using UniversalEditor.ObjectModels.Concertroid.Concert;

namespace UniversalEditor.DataFormats.AniMiku.Concert
{
	/// <summary>
	/// Implements the AniMiku performance data format.
	/// </summary>
	public class AniMikuINIDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(ConcertObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);

			objectModels.Push(new PropertyListObjectModel());
			
			string magic = base.Accessor.Reader.ReadLine();
			if (magic != "AMPV2") throw new InvalidDataFormatException();
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);

			Group grpPERF = plom.Items.OfType<Group>("PERF");
			if (grpPERF == null) throw new InvalidDataFormatException();

			Property prpPERFnum = grpPERF.Items.OfType<Property>("num");
			if (prpPERFnum == null) throw new InvalidDataFormatException();

			int perfNum = Int32.Parse(prpPERFnum.Value.ToString());

			for (int i = 0; i < perfNum; i++)
			{
				Group grp = plom.Items.OfType<Group>("CHP-" + i.ToString());

				Performance perf = new Performance();

				// The title of the song used in this performance.
				Property prpName = grp.Items.OfType<Property>("name");
				perf.Title = prpName.Value.ToString();
				
				// File name of the motion data files associated with characters 1 and 2.
				Property prpVmd1 = grp.Items.OfType<Property>("vmd1");
				Property prpVmd2 = grp.Items.OfType<Property>("vmd2");

				// Background audio to play during the performance, and delay in milliseconds between
				// start of animation and start of audio.
				Property prpSound = grp.Items.OfType<Property>("sound");
				Property prpDelay = grp.Items.OfType<Property>("delay");

				// File name of the model data files associated with characters 1 and 2.
				Property prpModel1 = grp.Items.OfType<Property>("model1");
				Property prpModel2 = grp.Items.OfType<Property>("model2");

				// Offset of the model along the X axis. Currently AMP does not support offsetting the
				// model along the Y axis.
				Property prpOffset1 = grp.Items.OfType<Property>("offset1");
				Property prpOffset2 = grp.Items.OfType<Property>("offset2");

				#region Performer 1
				{
					Performer performer = new Performer();
					performer.Character = new Character();
					performer.Character.FullName = System.IO.Path.GetFileNameWithoutExtension(prpModel1.Value.ToString());

					performer.Costume = new Costume();
					performer.Costume.Title = System.IO.Path.GetFileNameWithoutExtension(prpModel1.Value.ToString());
					performer.Costume.ModelFileName = prpModel1.Value.ToString();

					performer.Animation = new Animation();
					performer.Animation.FileName = prpVmd1.Value.ToString();
					performer.Offset = new PositionVector3((double)prpOffset1.Value, 0, 0);
					perf.Performers.Add(performer);
				}
				#endregion
				#region Performer 2
				{
					Performer performer = new Performer();
					performer.Character = new Character();
					performer.Character.FullName = System.IO.Path.GetFileNameWithoutExtension(prpModel2.Value.ToString());

					performer.Costume = new Costume();
					performer.Costume.Title = System.IO.Path.GetFileNameWithoutExtension(prpModel2.Value.ToString());
					performer.Costume.ModelFileName = prpModel2.Value.ToString();

					performer.Animation = new Animation();
					performer.Animation.FileName = prpVmd2.Value.ToString();
					performer.Offset = new PositionVector3((double)prpOffset2.Value, 0, 0);
					perf.Performers.Add(performer);
				}
				#endregion
				#region Song
				Song song = new Song();
				song.AudioFileName = prpSound.Value.ToString();
				song.Delay = (int)prpDelay.Value;
				song.Title = System.IO.Path.GetFileNameWithoutExtension(song.AudioFileName);
				perf.Song = song;
				#endregion

				concert.Performances.Add(perf);
			}
		}
	}
}

