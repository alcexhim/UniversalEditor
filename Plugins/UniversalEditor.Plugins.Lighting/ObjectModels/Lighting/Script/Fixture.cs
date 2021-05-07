namespace UniversalEditor.ObjectModels.Lighting.Script
{
	public class Fixture
	{
		public class FixtureCollection
			: System.Collections.ObjectModel.Collection<Fixture>
		{
		}

		private int mvarInitialAddress = 0;
		public int InitialAddress { get { return mvarInitialAddress; } set { mvarInitialAddress = value; } }
	}
}
