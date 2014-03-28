using System;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Metasequoia
{
	public class MQODataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Metasequoia model", new string[] { "*.mqo" });
            dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
            ModelObjectModel model = (objectModel as ModelObjectModel);
            if (model == null) return;

            IO.Reader tr = base.Accessor.Reader;
            string MetasequoiaDocument = tr.ReadLine();
            if (MetasequoiaDocument != "MetasequoiaDocument") throw new InvalidDataFormatException("File does not begin with \"MetasequoiaDocument\"");

            string DocumentFormat = tr.ReadLine();
            if (DocumentFormat != "Format Text Ver 1.0") throw new InvalidDataFormatException("Cannot understand Metasequoia format \"" + DocumentFormat + "\"");

            while (!tr.EndOfStream)
            {
                string line = tr.ReadLine();
                if (String.IsNullOrEmpty(line.Trim())) continue;

                if (line.StartsWith("Scene "))
                {
                }
                else if (line.StartsWith("Material "))
                {
                    string materialCountStr = line.Substring(9, line.IndexOf("{") - 9);
                    int materialCount = Int32.Parse(materialCountStr);
                }
            }
		}
	}
}
