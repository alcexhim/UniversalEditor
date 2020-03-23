namespace MBS.Framework.UserInterface
{
	public class ListViewHitTestInfo
	{
		public TreeModelRow Row { get; private set; } = null;
		public TreeModelColumn Column { get; private set; } = null;
		
		public ListViewHitTestInfo(TreeModelRow row, TreeModelColumn column)
		{
			Row = row;
			Column = column;
		}
	}
}
