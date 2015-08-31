using System;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Scene;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker.Scene
{
    public class MMDSceneDataFormat : DataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Polygon Movie Maker single-model scene data", new byte?[][] { new byte?[] { new byte?(80), new byte?(111), new byte?(108), new byte?(121), new byte?(103), new byte?(111), new byte?(110), new byte?(32), new byte?(77), new byte?(111), new byte?(118), new byte?(105), new byte?(101), new byte?(32), new byte?(109), new byte?(97), new byte?(107), new byte?(101), new byte?(114), new byte?(32), new byte?(48), new byte?(48), new byte?(48), new byte?(49) } }, new string[] { "*.mmd" });
            dfr.Capabilities.Add(typeof(SceneObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
