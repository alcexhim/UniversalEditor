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
			if (magic != "AMPV2") throw new DataFormatException(UniversalEditor.Localization.StringTable.ErrorDataFormatInvalid);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);

			Group grpPERF = plom.Groups["PERF"];
			if (grpPERF == null) throw new DataFormatException(UniversalEditor.Localization.StringTable.ErrorDataFormatInvalid);

			Property prpPERFnum = grpPERF.Properties["num"];
			if (prpPERFnum == null) throw new DataFormatException(UniversalEditor.Localization.StringTable.ErrorDataFormatInvalid);

			int perfNum = Int32.Parse(prpPERFnum.Value.ToString());

			for (int i = 0; i < perfNum; i++)
			{
				Group grp = plom.Groups["CHP-" + i.ToString()];

				Performance perf = new Performance();

				// The title of the song used in this performance.
				Property prpName = grp.Properties["name"];
				perf.Title = prpName.Value.ToString();
				
				// File name of the motion data files associated with characters 1 and 2.
				Property prpVmd1 = grp.Properties["vmd1"];
				Property prpVmd2 = grp.Properties["vmd2"];

				// Background audio to play during the performance, and delay in milliseconds between
				// start of animation and start of audio.
				Property prpSound = grp.Properties["sound"];
				Property prpDelay = grp.Properties["delay"];

				// File name of the model data files associated with characters 1 and 2.
				Property prpModel1 = grp.Properties["model1"];
				Property prpModel2 = grp.Properties["model2"];

				// Offset of the model along the X axis. Currently AMP does not support offsetting the
				// model along the Y axis.
				Property prpOffset1 = grp.Properties["offset1"];
				Property prpOffset2 = grp.Properties["offset2"];

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

