using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.Markup;
namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	public class HTMLPlaylistDataFormat : XMLDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();

			dfr.Filters.Add("HTML playlist", new byte?[][] { new byte?[] { new byte?(60), new byte?(63), new byte?(120), new byte?(109), new byte?(108) } }, new string[] { "*.html" });
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override bool IsObjectModelSupported(UniversalEditor.ObjectModel omb)
		{
			MarkupObjectModel mom = (omb as MarkupObjectModel);
			if (mom == null) return false;

			return (mom.Elements.Count == 1 && mom.Elements[0].FullName == "html");
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
		}

        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);

            PlaylistObjectModel pom = (objectModels.Pop() as PlaylistObjectModel);
            if (pom == null) throw new ObjectModelNotSupportedException();

            MarkupObjectModel mom = new MarkupObjectModel();
            objectModels.Push(mom);
        }
	}
}
