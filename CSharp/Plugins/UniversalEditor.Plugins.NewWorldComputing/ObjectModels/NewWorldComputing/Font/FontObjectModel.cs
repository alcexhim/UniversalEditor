using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Font
{
    public class FontObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "Heroes of Might and Magic font";
            }
            return _omr;
        }

        public override void Clear()
        {
            mvarGlyphWidth = 0;
            mvarGlyphHeight = 0;
            mvarGlyphCollection.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            FontObjectModel clone = (where as FontObjectModel);
            if (clone == null) return;

            clone.GlyphWidth = mvarGlyphWidth;
            clone.GlyphHeight = mvarGlyphHeight;
            foreach (PictureObjectModel picture in mvarGlyphCollection.Pictures)
            {
                clone.GlyphCollection.Pictures.Add(picture.Clone() as PictureObjectModel);
            }
        }

        private ushort mvarGlyphWidth = 0;
        public ushort GlyphWidth { get { return mvarGlyphWidth; } set { mvarGlyphWidth = value; } }

        private ushort mvarGlyphHeight = 0;
        public ushort GlyphHeight { get { return mvarGlyphHeight; } set { mvarGlyphHeight = value; } }

        private string mvarGlyphCollectionFileName = String.Empty;
        public string GlyphCollectionFileName
        {
            get { return mvarGlyphCollectionFileName; }
            set
            {
                mvarGlyphCollectionFileName = value;
                if (System.IO.File.Exists(mvarGlyphCollectionFileName))
                {
                    mvarGlyphCollection = UniversalEditor.Common.Reflection.GetAvailableObjectModel<PictureCollectionObjectModel>(mvarGlyphCollectionFileName);
                }
            }
        }

        private PictureCollectionObjectModel mvarGlyphCollection = new PictureCollectionObjectModel();
        public PictureCollectionObjectModel GlyphCollection
        {
            get { return mvarGlyphCollection; }
        }
    }
}
