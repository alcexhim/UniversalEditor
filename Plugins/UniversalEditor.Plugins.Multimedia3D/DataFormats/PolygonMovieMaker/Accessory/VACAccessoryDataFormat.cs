using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Accessory;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker.Accessory
{
    public class VACAccessoryDataFormat : TextDataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Capabilities.Add(typeof(AccessoryObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("Polygon Movie Maker accessory file", new string[] { "*.vac" });
            return dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            AccessoryObjectModel accs = (objectModel as AccessoryObjectModel);
            if (accs == null) return;

            IO.TextReader tr = new IO.TextReader(base.Stream.BaseStream, System.Text.Encoding.GetEncoding("shift_jis"));

            while (!tr.EndOfStream)
            {
                string title = tr.ReadLine();
                if (String.IsNullOrEmpty(title) || title.StartsWith("//")) return;

                UniversalEditor.Plugins.Multimedia3D.ObjectModels.Accessory.AccessoryItem acc = new UniversalEditor.Plugins.Multimedia3D.ObjectModels.Accessory.AccessoryItem();

                acc.Title = title;
                acc.ModelFileName = tr.ReadLine();

                string magStr = tr.ReadLine();
                double mag = 1.0;
                if (Double.TryParse(magStr, out mag))
                {
                    acc.Scale = new PositionVector3(mag, mag, mag);
                }

                string posStr = tr.ReadLine();
                string[] posStrs = posStr.Split(new char[] { ',' });
                if (posStrs.Length == 3)
                {
                    double posX = 0.0;
                    double posY = 0.0;
                    double posZ = 0.0;

                    Double.TryParse(posStrs[0], out posX);
                    Double.TryParse(posStrs[1], out posY);
                    Double.TryParse(posStrs[2], out posZ);

                    acc.Position = new PositionVector3(posX, posY, posZ);
                }

                string rotStr = tr.ReadLine();
                string[] rotStrs = rotStr.Split(new char[] { ',' });
                if (rotStrs.Length == 3)
                {
                    double rotX = 0.0;
                    double rotY = 0.0;
                    double rotZ = 0.0;

                    Double.TryParse(rotStrs[0], out rotX);
                    Double.TryParse(rotStrs[1], out rotY);
                    Double.TryParse(rotStrs[2], out rotZ);

                    acc.Rotation = new PositionVector4(rotX, rotY, rotZ, 1.0);
                }

                acc.BoneName = tr.ReadLine();

                accs.Accessories.Add(acc);
            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            AccessoryObjectModel accs = (objectModel as AccessoryObjectModel);
            if (accs == null) return;

            IO.TextWriter tw = new IO.TextWriter(base.Stream.BaseStream, Encoding.GetEncoding("shift_jis"));

            foreach (UniversalEditor.Plugins.Multimedia3D.ObjectModels.Accessory.AccessoryItem acc in accs.Accessories)
            {
                tw.WriteLine(acc.Title);
                tw.WriteLine(acc.ModelFileName);
                double mag = acc.Scale.GetLargestComponentValue();
                tw.WriteLine(String.Format("{0:0.0#####################}", mag));
                tw.WriteLine(acc.Position.ToString(",", null, null));
                tw.WriteLine(acc.Rotation.ToString(",", null, null, 3));
                tw.WriteLine(acc.BoneName);
            }

            tw.Flush();
        }
    }
}
