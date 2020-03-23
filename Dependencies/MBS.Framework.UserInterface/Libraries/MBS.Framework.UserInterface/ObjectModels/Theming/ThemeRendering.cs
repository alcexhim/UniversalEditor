using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeRendering : ICloneable
	{
		public class RenderingCollection
			: System.Collections.ObjectModel.Collection<ThemeRendering>
		{

		}

		private ThemeComponentStateReference.ThemeComponentStateReferenceCollection mvarStates = new ThemeComponentStateReference.ThemeComponentStateReferenceCollection();
		public ThemeComponentStateReference.ThemeComponentStateReferenceCollection States { get { return mvarStates; } }

		private RenderingAction.RenderingActionCollection mvarActions = new RenderingAction.RenderingActionCollection();
		public RenderingAction.RenderingActionCollection Actions { get { return mvarActions; } }

		public object Clone()
		{
			ThemeRendering clone = new ThemeRendering();
			foreach (ThemeComponentStateReference item in mvarStates)
			{
				clone.States.Add(item.Clone() as ThemeComponentStateReference);
			}
			foreach (RenderingAction item in mvarActions)
			{
				clone.Actions.Add(item.Clone() as RenderingAction);
			}
			return clone;
		}
	}
}
