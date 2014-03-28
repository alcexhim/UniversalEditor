using System;
using UniversalEditor.ObjectModels.Multimedia.Picture;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
    [Flags()]
	public enum ModelTextureFlags : int
	{
		None = 0,
		Texture = 1,
		Map = 2,
		AddMap = 4
	}
    public class ModelTexture : ICloneable
    {
        public class ModelTextureCollection
            : System.Collections.ObjectModel.Collection<ModelTexture>
        {
            public ModelTexture Add(string TextureFileName, string MapFileName, ModelTextureFlags Flags)
            {
                ModelTexture tex = new ModelTexture();
                tex.TextureFileName = TextureFileName;
                tex.MapFileName = MapFileName;
				tex.Flags = Flags;

                base.Add(tex);
                return tex;
            }
        }

        private uint? mvarTextureID = null;
        public uint? TextureID { get { return mvarTextureID; } set { mvarTextureID = value; } }
        private uint? mvarMapID = null;
        public uint? MapID { get { return mvarMapID; } set { mvarMapID = value; } }

        private string mvarMapFileName = null;
		public string MapFileName { get { return mvarMapFileName; } set { mvarMapFileName = value; } }

		private string mvarTextureFileName = null;
		public string TextureFileName { get { return mvarTextureFileName; } set { mvarTextureFileName = value; } }

        private ModelTextureFlags mvarFlags = ModelTextureFlags.None;
        public ModelTextureFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		private int mvarDuration = 100;
		/// <summary>
		/// How long this texture image frame will appear on the associated material, in milliseconds.
		/// </summary>
		public int Duration { get { return mvarDuration; } set { mvarDuration = value; } }

        public object Clone()
        {
            ModelTexture texture = new ModelTexture();
			texture.MapFileName = mvarMapFileName;
			texture.TextureFileName = mvarTextureFileName;
            texture.Flags = mvarFlags;
            texture.MapID = mvarMapID;
            texture.TextureID = mvarTextureID;
            return texture;
        }

		private PictureObjectModel mvarTexturePicture = null;
		public PictureObjectModel TexturePicture { get { return mvarTexturePicture; } set { mvarTexturePicture = value; } }

		private PictureObjectModel mvarMapPicture = null;
		public PictureObjectModel MapPicture { get { return mvarMapPicture; } set { mvarMapPicture = value; } }
	}
}
