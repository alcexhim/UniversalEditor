using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.DataFormats.PropertyList.Microsoft;

using UniversalEditor.ObjectModels.PropertyList;

using UniversalEditor.ObjectModels.Concertroid;
using UniversalEditor.ObjectModels.Concertroid.Concert;

namespace UniversalEditor.DataFormats.AniMiku.Concert
{
	public class AniMikuINIDataFormat : WindowsConfigurationDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(ConcertObjectModel), DataFormatCapabilities.All);
			dfr.Filters.Add("AniMiku performance", new byte?[][] { new byte?[] { (byte)'A', (byte)'M', (byte)'P', (byte)'V', (byte)'2', (byte)'\r', (byte)'\n' } }, new string[] { "*.amp" });
			return dfr;
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

				Property prpName = grp.Properties["name"];
				perf.Title = prpName.Value.ToString();
				
				Property prpVmd1 = grp.Properties["vmd1"];
				Property prpVmd2 = grp.Properties["vmd2"];

				Property prpSound = grp.Properties["sound"];
				Property prpDelay = grp.Properties["delay"];

				Property prpModel1 = grp.Properties["model1"];
				Property prpModel2 = grp.Properties["model2"];
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

