using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Picture.Collection
{
    public class PictureCollectionObjectModel : ObjectModel
    {
    	private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
        	if (_omr == null)
        	{
        		_omr = base.MakeReferenceInternal();
        		_omr.Title = "Picture collection";
        		_omr.Description = "Store multiple pictures in a single file";
        		_omr.Path = new string[] { "Multimedia", "Picture", "Picture Collection" };
        	}
        	return _omr;
        }

        private PictureObjectModel.PictureObjectModelCollection mvarPictures = new PictureObjectModel.PictureObjectModelCollection();
        public PictureObjectModel.PictureObjectModelCollection Pictures { get { return mvarPictures; } }

        public int MaximumPictureWidth
        {
            get
            {
                int value = 0;
                foreach (PictureObjectModel pic in mvarPictures)
                {
                    if (pic.Width > value) value = pic.Width;
                }
                return value;
            }
        }
        public int MaximumPictureHeight
        {
            get
            {
                int value = 0;
                foreach (PictureObjectModel pic in mvarPictures)
                {
                    if (pic.Height > value) value = pic.Height;
                }
                return value;
            }
        }

        public override void Clear()
        {
            mvarPictures.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            PictureCollectionObjectModel clone = (where as PictureCollectionObjectModel);
            if (clone == null) return;

            foreach (PictureObjectModel pic in mvarPictures)
            {
                clone.Pictures.Add(pic.Clone() as PictureObjectModel);
            }
        }
    }
}
