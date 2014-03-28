using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neo;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
    public class ModelSurface : ICloneable
    {
        public class ModelSurfaceCollection
            : System.Collections.ObjectModel.Collection<ModelSurface>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private ModelTriangle.ModelTriangleCollection mvarTriangles = new ModelTriangle.ModelTriangleCollection();
        public ModelTriangle.ModelTriangleCollection Triangles { get { return mvarTriangles; } }

        private ModelVertex.ModelVertexCollection mvarVertices = new ModelVertex.ModelVertexCollection();
        public ModelVertex.ModelVertexCollection Vertices { get { return mvarVertices; } }
        
        public object Clone()
        {
            ModelSurface surface = new ModelSurface();
            surface.Name = (mvarName.Clone() as string);
            foreach (ModelTriangle triangle in mvarTriangles)
            {
                surface.Triangles.Add(triangle.Clone() as ModelTriangle);
            }
            foreach (ModelVertex vertex in mvarVertices)
            {
                surface.Vertices.Add(vertex.Clone() as ModelVertex);
            }
            return surface;
        }

        public void Update()
        {
            for (int i = 0; i < mvarVertices.Count; i++)
            {
                if (mvarVertices[i].Weight == 0.0f)
                {
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, mvarVertices[i].Bone1.GetSkinningMatrix());
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, mvarVertices[i].Bone1.GetSkinningMatrix());
                }
                else if (mvarVertices[i].Weight >= 0.9999f)
                {
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, mvarVertices[i].Bone0.GetSkinningMatrix());
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, mvarVertices[i].Bone0.GetSkinningMatrix());
                }
                else
                {
                    Matrix matTemp = Matrix.Lerp(mvarVertices[i].Bone0.GetSkinningMatrix(), mvarVertices[i].Bone1.GetSkinningMatrix(), mvarVertices[i].Weight);
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, matTemp);
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, matTemp);
                }
            }
        }

        public void Reset()
        {
            foreach (ModelVertex vtx in mvarVertices)
            {
                vtx.Reset();
            }
        }
    }
}
