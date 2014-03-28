using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.AniMiku.PMDExtension;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.AniMiku.PMDExtension
{
    internal class PMDExtensionDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(PMDExtensionObjectModel), DataFormatCapabilities.All);
                // _dfr.Filters.Add("AniMiku PMD extension");
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PMDExtensionObjectModel pmdo = (objectModel as PMDExtensionObjectModel);
            if (pmdo == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            foreach (ModelMaterial mat in mvarModel.Materials)
            {
                mat.AlwaysLight = br.ReadBoolean();
                mat.EnableAnimation = br.ReadBoolean();
                mat.EnableGlow = br.ReadBoolean();

				if (mat.EnableAnimation)
                {
                    int textureCount = br.ReadInt32();
                    string archiveFileName = br.ReadNullTerminatedString(100);

                    PMDExtensionTextureGroup file = new PMDExtensionTextureGroup();
                    file.Material = mat;
                    file.ArchiveFileName = archiveFileName;
                    for (int i = 0; i < textureCount; i++)
                    {
                        string textureFileName = br.ReadNullTerminatedString(256);
                        file.TextureImageFileNames.Add(textureFileName);
                    }
                    pmdo.ArchiveFiles.Add(file);
                }
            }

            int originalModelLength = br.ReadInt32();
            string END = br.ReadFixedLengthString(3);
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
#if READYTOSAVE
            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            foreach (ModelMaterial mat in mvarModel.Materials)
            {
                bw.Write(mat.AlwaysLight);
                bw.Write(mat.EnableAnimation);
                bw.Write(mat.EnableGlow);

                if (mat.EnableAnimation)
                {
                    // TODO: figure out how to get texture count for the model
                    int textureCount = br.ReadInt32();
                    string archiveFileName = br.ReadNullTerminatedString(100);

                    PMDExtensionTextureGroup file = new PMDExtensionTextureGroup();
                    file.Material = mat;
                    file.ArchiveFileName = archiveFileName;
                    for (int i = 0; i < textureCount; i++)
                    {
                        string textureFileName = br.ReadNullTerminatedString(256);
                        file.TextureImageFileNames.Add(textureFileName);
                    }
                    pmdo.ArchiveFiles.Add(file);
                }
            }

            int originalModelLength = br.ReadInt32();
            string END = br.ReadFixedLengthString(3);
#endif
            throw new NotImplementedException();
        }

        private ModelObjectModel mvarModel = null;
        public ModelObjectModel Model { get { return mvarModel; } set { mvarModel = value; } }
    }
}
