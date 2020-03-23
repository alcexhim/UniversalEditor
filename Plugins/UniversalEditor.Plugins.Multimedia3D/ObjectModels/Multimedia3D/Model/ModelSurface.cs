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
                double px = mvarVertices[i].OriginalPosition.X, py = mvarVertices[i].OriginalPosition.Y, pz = mvarVertices[i].OriginalPosition.Z;
                double nx = mvarVertices[i].OriginalNormal.X, ny = mvarVertices[i].OriginalNormal.Y, nz = mvarVertices[i].OriginalNormal.Z;

                // TODO: Figure out transformations of bones, etc.

                px = ((mvarVertices[i].Bone0.Position.X + mvarVertices[i].Bone0.Vector3Offset.X) * mvarVertices[i].Bone0Weight);
                py = ((mvarVertices[i].Bone0.Position.Y + mvarVertices[i].Bone0.Vector3Offset.Y) * mvarVertices[i].Bone0Weight);
                pz = ((mvarVertices[i].Bone0.Position.Z + mvarVertices[i].Bone0.Vector3Offset.Z) * mvarVertices[i].Bone0Weight);

                px += ((mvarVertices[i].Bone1.Position.X + mvarVertices[i].Bone1.Vector3Offset.X) * mvarVertices[i].Bone1Weight);
                py += ((mvarVertices[i].Bone1.Position.Y + mvarVertices[i].Bone1.Vector3Offset.Y) * mvarVertices[i].Bone1Weight);
                pz += ((mvarVertices[i].Bone1.Position.Z + mvarVertices[i].Bone1.Vector3Offset.Z) * mvarVertices[i].Bone1Weight);

                mvarVertices[i].Position = new PositionVector3(px, py, pz);
                mvarVertices[i].Normal = new PositionVector3(nx, ny, nz);

                /*
                if (mvarVertices[i].Bone0Weight == 0.0f)
                {
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, mvarVertices[i].Bone1.GetSkinningMatrix());
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, mvarVertices[i].Bone1.GetSkinningMatrix());
                }
                else if (mvarVertices[i].Bone0Weight >= 1.0f)
                {
                    Neo.Matrix matrix = mvarVertices[i].Bone0.GetSkinningMatrix();
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, matrix);
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, matrix);
                }
                else
                {
                    Matrix matTemp = Matrix.Lerp(mvarVertices[i].Bone0.GetSkinningMatrix(), mvarVertices[i].Bone1.GetSkinningMatrix(), mvarVertices[i].Bone0Weight);
                    mvarVertices[i].Position = PositionVector3.Transform(mvarVertices[i].OriginalPosition, matTemp);
                    mvarVertices[i].Normal = PositionVector3.Rotate(mvarVertices[i].OriginalNormal, matTemp);
                }
                */
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
