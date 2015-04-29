using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.KnowledgeAdventure.Actor
{
	public class ActorObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Actor";
				_omr.Path = new string[] { "Knowledge Adventure" };
			}
			return _omr;
		}

		private string mvarName = String.Empty;
		/// <summary>
		/// The name used in scripts to identify this <see cref="Actor" />.
		/// </summary>
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarImageFileName = String.Empty;
		/// <summary>
		/// The file name of the image file.
		/// </summary>
		public string ImageFileName { get { return mvarImageFileName; } set { mvarImageFileName = value; } }

		public override void Clear()
		{
			mvarName = String.Empty;
			mvarImageFileName = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			ActorObjectModel clone = (where as ActorObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			clone.Name = (mvarName.Clone() as string);
			clone.ImageFileName = (mvarImageFileName.Clone() as string);
		}
	}
}
