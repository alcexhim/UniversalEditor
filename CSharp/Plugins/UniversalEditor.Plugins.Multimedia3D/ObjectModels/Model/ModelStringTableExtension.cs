using System;
using System.Collections.Generic;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelStringTableExtension : ICloneable
	{
		private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarAuthor = String.Empty;
        public string Author { get { return mvarAuthor; } set { mvarAuthor = value; } }

        private string mvarComments = String.Empty;
        public string Comments { get { return mvarComments; } set { mvarComments = value; } }

        private List<string> mvarBoneNames = new List<string>();
        public List<string> BoneNames
		{
			get { return mvarBoneNames; }
		}
        private List<string> mvarSkinNames = new List<string>();
        public List<string> SkinNames
		{
			get { return mvarSkinNames; }
		}
        private List<string> mvarNodeNames = new List<string>();
        public List<string> NodeNames
		{
			get { return mvarNodeNames; }
		}

        public object Clone()
        {
            ModelStringTableExtension clone = new ModelStringTableExtension();
            clone.Title = mvarTitle.Clone() as string;
            clone.Author = mvarAuthor.Clone() as string;
            clone.Comments = mvarAuthor.Clone() as string;
            foreach (string s in mvarBoneNames)
            {
                clone.BoneNames.Add(s.Clone() as string);
            }
            foreach (string s in mvarSkinNames)
            {
                clone.SkinNames.Add(s.Clone() as string);
            }
            foreach (string s in mvarNodeNames)
            {
                clone.NodeNames.Add(s.Clone() as string);
            }
            return clone;
        }
    }
}
