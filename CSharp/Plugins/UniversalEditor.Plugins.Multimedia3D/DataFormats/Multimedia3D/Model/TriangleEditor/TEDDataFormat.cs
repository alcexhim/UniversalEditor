using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.TriangleEditor
{
    public class TEDDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null) _dfr = base.MakeReference();
            _dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            _dfr.Filters.Add("TriangleEditor model", new byte?[][] { new byte?[] { 0x14, (byte)'T', (byte)'r', (byte)'i', (byte)'a', (byte)'n', (byte)'g', (byte)'l', (byte)'e', (byte)'E', (byte)'d', (byte)'i', (byte)'t', (byte)'o', (byte)'r', (byte)'_', (byte)'V', (byte)'.', (byte)'1', (byte)'.', (byte)'0' } }, new string[] { "*.ted" });
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            ModelObjectModel model = (objectModel as ModelObjectModel);
            if (model == null) return;

            IO.Reader br = base.Accessor.Reader;
            
            byte zeroX14 = br.ReadByte();
            if (zeroX14 != 0x14) throw new InvalidDataFormatException();

            string signature = br.ReadFixedLengthString(20);
            if (signature != "TriangleEditor_V.1.0") throw new InvalidDataFormatException();

            ushort triangleCount = br.ReadUInt16();
            if (triangleCount == 0xFFFF) return;

            triangleCount++;

            ModelSurface surf = new ModelSurface();
            for (ushort i = 0; i < triangleCount; i++)
            {
                ModelTriangle tri = new ModelTriangle();
                for (int j = 0; j < 3; j++)
                {
                    // read the next vertex for the triangle
                    double positionY = br.ReadDouble();
                    double positionX = br.ReadDouble();
                    double positionZ = br.ReadDouble();

                    ModelVertex vtx = new ModelVertex();
                    vtx.Position = new PositionVector3(positionX, positionY, positionZ);
                    vtx.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);

                    surf.Vertices.Add(vtx);

                    switch (j)
                    {
                        case 0: tri.Vertex1 = vtx; break;
                        case 1: tri.Vertex2 = vtx; break;
                        case 2: tri.Vertex3 = vtx; break;
                    }
                }
                surf.Triangles.Add(tri);
            }
            model.Surfaces.Add(surf);


            ModelMaterial mat = new ModelMaterial();
            mat.Name = "dummy";
            mat.IndexCount = (uint)surf.Vertices.Count;
            foreach (ModelTriangle tri in surf.Triangles)
            {
                 mat.Triangles.Add(tri);
            }
            mat.AmbientColor = Colors.White;
            mat.DiffuseColor = Colors.White;
            mat.EmissiveColor = Colors.White;
            mat.SpecularColor = Colors.White;
            model.Materials.Add(mat);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
