using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Inivis
{
    public class AC3DDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null) _dfr = base.MakeReferenceInternal();
            _dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            _dfr.Filters.Add("Inivis AC3D model", new byte?[][] { new byte?[] { (byte)'A', (byte)'C', (byte)'3', (byte)'D', (byte)'b' } }, new string[] { "*.ac" });
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            ModelObjectModel model = (objectModel as ModelObjectModel);
            if (model == null) return;

        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            ModelObjectModel model = (objectModel as ModelObjectModel);
            if (model == null) return;

            IO.Writer tw = base.Accessor.Writer;
            tw.WriteLine("AC3Db");
            tw.WriteLine("OBJECT world");
            tw.WriteLine("kids 1");
            tw.WriteLine("OBJECT poly");
            tw.WriteLine("name \"" + model.Name + "\"");
            tw.WriteLine("loc 0 0 0");
            tw.WriteLine("crease 45.000000");
            tw.WriteLine("numvert " + model.Surfaces[0].Vertices.Count);
            foreach (ModelVertex vert in model.Surfaces[0].Vertices)
            {
                tw.WriteLine(vert.Position.X.ToString() + " " + vert.Position.Y.ToString() + " " + vert.Position.Z.ToString());
            }

            int surfcount = ((int)((double)model.Surfaces[0].Vertices.Count / 3));
            tw.WriteLine("numsurf " + surfcount.ToString());
            for (int i = 0; i < model.Surfaces[0].Vertices.Count; i += 3)
            {
                tw.WriteLine("SURF 0x30");
                tw.WriteLine("mat 0");
                tw.WriteLine("refs 3");

                tw.WriteLine(i.ToString() + " " + model.Surfaces[0].Vertices[i].Texture.U.ToString() + " " + model.Surfaces[0].Vertices[i].Texture.V.ToString());
                tw.WriteLine((i + 1).ToString() + " " + model.Surfaces[0].Vertices[i + 1].Texture.U.ToString() + " " + model.Surfaces[0].Vertices[i + 1].Texture.V.ToString());
                tw.WriteLine((i + 2).ToString() + " " + model.Surfaces[0].Vertices[i + 2].Texture.U.ToString() + " " + model.Surfaces[0].Vertices[i + 2].Texture.V.ToString());
            }
            tw.Flush();
        }
    }
}
