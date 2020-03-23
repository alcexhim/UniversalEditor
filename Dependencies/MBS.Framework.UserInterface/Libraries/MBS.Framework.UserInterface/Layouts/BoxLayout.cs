using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Layouts
{
	public class BoxLayout : Layout
	{
		public enum PackType
		{
			Start,
			End
		}
		
		public class Constraints : MBS.Framework.UserInterface.Constraints
		{
			private bool mvarExpand = false;
			public bool Expand { get { return mvarExpand; } set { mvarExpand = value; } }

			private bool mvarFill = false;
			public bool Fill { get { return mvarFill; } set { mvarFill = value; } }

			private int mvarPadding = 0;
			public int Padding { get { return mvarPadding; } set { mvarPadding = value; } }

			private PackType mvarPackType = PackType.Start;

			public PackType PackType { get { return mvarPackType; } set { mvarPackType = value; } }

			public Constraints(bool expand = false, bool fill = false, int padding = 0, PackType packType = PackType.Start)
			{
				mvarExpand = expand;
				mvarFill = fill;
				mvarPadding = padding;
				mvarPackType = packType;
			}
		}

		public BoxLayout(Orientation orientation, int spacing = 0, bool homogenous = false)
		{
			mvarOrientation = orientation;
			mvarSpacing = spacing;
			mvarHomogeneous = homogenous;
		}

		private Dictionary<Control, Rectangle> bounds = new Dictionary<Control, Rectangle>();

		private Orientation mvarOrientation = Orientation.Horizontal;
		public Orientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; ResetControlBounds(); } }

		private int mvarSpacing = 0;
		/// <summary>
		/// The spacing between each cell of the <see cref="BoxLayout" />.
		/// </summary>
		public int Spacing { get { return mvarSpacing; } set { mvarSpacing = value; } }

		private bool mvarHomogeneous = false;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MBS.Framework.UserInterface.Layouts.BoxLayout"/> is homogeneous.
		/// </summary>
		/// <value><c>true</c> if homogeneous; otherwise, <c>false</c>.</value>
		public bool Homogeneous { get { return mvarHomogeneous; } set { mvarHomogeneous = value; } }

		protected override Rectangle GetControlBoundsInternal(Control ctl)
		{
			if (!bounds.ContainsKey(ctl))
			{
				Rectangle rect = new Rectangle();
				if (ctl.Parent != null)
				{
					foreach (Control ctl1 in ctl.Parent.Controls)
					{
						Rectangle parentRect = GetControlBounds(ctl.Parent);
						switch (mvarOrientation)
						{
							case Orientation.Horizontal:
							{
								rect.Height = parentRect.Height - (ctl.Parent.Padding.Top + ctl.Parent.Padding.Bottom);
								rect.Width = (double)((double)(parentRect.Width - (ctl.Parent.Padding.Left + ctl.Parent.Padding.Right)) / ctl.Parent.Controls.Count);
								if (ctl.Parent.Controls.IndexOf(ctl) < ctl.Parent.Controls.Count - 1) rect.Width -= mvarSpacing;
								break;
							}
							case Orientation.Vertical:
							{
								rect.Width = parentRect.Width - (ctl.Parent.Padding.Left + ctl.Parent.Padding.Right);
								rect.Height = (double)((double)(parentRect.Height - (ctl.Parent.Padding.Top + ctl.Parent.Padding.Bottom)) / ctl.Parent.Controls.Count);
								if (ctl.Parent.Controls.IndexOf(ctl) < ctl.Parent.Controls.Count - 1) rect.Height -= mvarSpacing;
								break;
							}
						}

						if (ctl1 == ctl) break;

						Rectangle rect1 = GetControlBounds(ctl1);
						switch (mvarOrientation)
						{
							case Orientation.Vertical:
							{
								rect.Y += rect1.Height + mvarSpacing;
								break;
							}
							case Orientation.Horizontal:
							{
								rect.X += rect1.Width + mvarSpacing;
								break;
							}
						}
						/*
						if (mvarAlignment == Alignment.Far)
						{
							switch (mvarOrientation)
							{
								case Orientation.Horizontal:
								{
									rect.X = parentRect.Right - rect.Width;
									break;
								}
								case Orientation.Vertical:
								{
									rect.Y = parentRect.Bottom - rect.Height;
									break;
								}
							}
						}
						*/
					}
				}
				bounds[ctl] = rect;
			}
			return bounds[ctl];
		}

		protected override void ResetControlBoundsInternal(Control ctl = null)
		{
			if (ctl == null)
			{
				bounds.Clear();
				return;
			}
			if (bounds.ContainsKey(ctl)) bounds.Remove(ctl);
		}

		private Alignment mvarAlignment = Alignment.Center;
		public Alignment Alignment { get { return mvarAlignment; } set { mvarAlignment = value; } }
	}
}
