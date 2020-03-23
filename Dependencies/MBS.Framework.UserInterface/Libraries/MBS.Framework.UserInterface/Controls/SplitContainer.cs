using System;

namespace MBS.Framework.UserInterface.Controls
{
	namespace Native
	{
		public interface ISplitContainerImplementation
		{
			int GetSplitterPosition();
			void SetSplitterPosition(int value);
		}
	}
	public class SplitContainer : SystemControl
	{
		private int _OldSplitterPosition = 0;

		protected internal override void OnResizing(ResizingEventArgs e)
		{
			base.OnResizing(e);
		}
		public class SplitContainerPanel : Container
		{
			public new SplitContainer Parent { get; private set; } = null;

			private bool _Expanded = true;
			public bool Expanded
			{
				get { return _Expanded; }
				set
				{
					_Expanded = value;
					if (_Expanded)
					{
						Parent.mvarSplitterPosition = Parent._OldSplitterPosition;
					}
					else
					{
						Parent._OldSplitterPosition = Parent.mvarSplitterPosition;
						if (this == Parent.Panel1)
						{
							Parent.mvarSplitterPosition = 0;
						}
						else if (this == Parent.Panel2)
						{
							Parent.mvarSplitterPosition = (int)Parent.Size.Width;
						}
					}
					(ControlImplementation as Native.ISplitContainerImplementation)?.SetSplitterPosition(Parent.mvarSplitterPosition);
				}
			}

			public SplitContainerPanel(SplitContainer parent)
			{
				Parent = parent;
			}
		}

		public SplitContainerPanel Panel1 { get; private set; } = null;
		public SplitContainerPanel Panel2 { get; private set; } = null;

		private Orientation mvarOrientation = Orientation.Horizontal;
		/// <summary>
		/// The orientation of the splitter in the SplitContainer. When vertical, panels are on the left and right; when
		/// horizontal, panels are on the top and bottom.
		/// </summary>
		/// <value>The orientation of the splitter in this <see cref="SplitContainer" />.</value>
		public Orientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; } }

		private int mvarSplitterPosition = 0;
		public int SplitterPosition
		{
			get
			{
				if (IsCreated)
				{
					Native.ISplitContainerImplementation impl = (ControlImplementation as Native.ISplitContainerImplementation);
					if (impl != null) {
						mvarSplitterPosition = impl.GetSplitterPosition ();
					}
				}
				return (Panel1.Expanded && Panel2.Expanded ? mvarSplitterPosition : (Panel1.Expanded ? (int)Size.Width : 0));
			}
			set
			{
				_OldSplitterPosition = value;
				(ControlImplementation as Native.ISplitContainerImplementation)?.SetSplitterPosition (value);
				mvarSplitterPosition = value;
			}
		}

		public SplitContainer (Orientation orientation = Orientation.Horizontal)
		{
			mvarOrientation = orientation;
			Panel1 = new SplitContainerPanel(this);
			Panel2 = new SplitContainerPanel(this);
		}
	}
}

