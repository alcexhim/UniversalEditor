using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Metasequoia
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

            IO.TextReader tr = base.Stream.TextReader;
            string MetasequoiaDocument = tr.ReadLine();
            if (MetasequoiaDocument != "MetasequoiaDocument") throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

            string DocumentFormat = tr.ReadLine();
            if (DocumentFormat != "Format Text Ver 1.0") throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

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
