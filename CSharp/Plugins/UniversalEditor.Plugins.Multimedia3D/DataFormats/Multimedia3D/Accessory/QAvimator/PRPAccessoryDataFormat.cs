using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Accessory;

namespace UniversalEditor.DataFormats.Multimedia3D.Accessory.QAvimator
{
    public class PRPAccessoryDataFormat : DataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Capabilities.Add(typeof(AccessoryObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("QAvimator props", new string[] { "*.prp" });
            return dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            AccessoryObjectModel accs = new AccessoryObjectModel();
            IO.Reader tr = base.Accessor.Reader;
            while (!tr.EndOfStream)
            {
                string propLine = tr.ReadLine();
                string[] propVals = propLine.Split(new char[] { ' ' }, "\"");
                if (propVals.Length != 11) continue;

                int propType = Int32.Parse(propVals[0]);
                double propPosX = Double.Parse(propVals[1]);
                double propPosY = Double.Parse(propVals[2]);
                double propPosZ = Double.Parse(propVals[3]);
                double propSclX = Double.Parse(propVals[4]);
                double propSclY = Double.Parse(propVals[5]);
                double propSclZ = Double.Parse(propVals[6]);
                double propRotX = Double.Parse(propVals[7]);
                double propRotY = Double.Parse(propVals[8]);
                double propRotZ = Double.Parse(propVals[9]);
                int propBoneIndex = Int32.Parse(propVals[10]);

                AccessoryItem acc = new AccessoryItem();
                acc.Title = propType.ToString();
                acc.BoneName = propBoneIndex.ToString();
                acc.Position = new PositionVector3(propPosX, propPosY, propPosZ);
                acc.Rotation = new PositionVector4(propRotX, propRotY, propRotZ);
                acc.Scale = new PositionVector3(propSclX, propSclY, propSclZ);
                accs.Accessories.Add(acc);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            AccessoryObjectModel accs = new AccessoryObjectModel();
            IO.Writer tw = base.Accessor.Writer;
            foreach (AccessoryItem acc in accs.Accessories)
            {
                string accTitle = acc.Title;
                if (accTitle.Contains(" "))
                {
                    accTitle = "\"" + accTitle + "\"";
                }
                string accBoneName = acc.BoneName;
                if (accBoneName.Contains(" "))
                {
                    accBoneName = "\"" + accBoneName + "\"";
                }
                tw.WriteLine(accTitle + " " + acc.Position.ToString(" ", null, null) + " " + acc.Scale.ToString(" ", null, null) + " " + acc.Rotation.ToString(" ", null, null, 3) + accBoneName);
            }
            tw.Flush();
        }
    }
}
