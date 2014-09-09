using System;
namespace Flame.ObjectModels.ProgrammingLanguage
{
	public class ProgrammingLanguageBlock
	{
		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set { mvarName = value; }
		}
		
		private Sequence mvarBeginningSequence = new Sequence();
		public Sequence BeginningSequence
		{
			get { return mvarBeginningSequence; }
		}
		private Sequence mvarEndingSequence = new Sequence ();
		public Sequence EndingSequence
		{
			get { return mvarEndingSequence; }
		}
		
		public override string ToString ()
		{
			if (mvarName != null)
			{
				return string.Format ("[Block {0}]", mvarName);
			}
			else
			{
				return "[Block]";
			}
		}

		
		public class BlockCollection
			: System.Collections.ObjectModel.Collection<Block>
		{
			private System.Collections.Generic.Dictionary<string, Block> blocksByName = new System.Collections.Generic.Dictionary<string, Block>();
			
			public new void Add (Block item)
			{
				blocksByName.Add (item.Name, item);
				base.Add (item);
			}
			public Block Add (string Name)
			{
				Block block = new Block ();
				block.Name = Name;
				
				Add (block);
				return block;
			}
			
			public Block this[string Name]
			{
				get
				{
					return blocksByName[Name];
				}
			}
		}
	}
}

