using System;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
	public class Fixture : ICloneable
	{
		public class FixtureCollection
			: System.Collections.ObjectModel.Collection<Fixture>
		{
		}

		private int mvarInitialAddress = 0;
		public int InitialAddress { get { return mvarInitialAddress; } set { mvarInitialAddress = value; } }

		public object Clone()
		{
			Fixture clone = new Fixture();
			clone.InitialAddress = InitialAddress;
			return clone;
		}
	}
}
