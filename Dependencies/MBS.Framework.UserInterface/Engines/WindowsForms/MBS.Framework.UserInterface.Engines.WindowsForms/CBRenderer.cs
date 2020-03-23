using System;
using System.Collections.Generic;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	internal class CBRenderer : System.Windows.Forms.ToolStripRenderer
	{
		private static CBRenderer mvarInstance = new CBRenderer();
		public static CBRenderer Instance
		{
			get { return mvarInstance; }
		}

		protected override void OnRenderItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				if (e.Item is System.Windows.Forms.ToolStripButton)
				{
					Theming.Theme.CurrentTheme.DrawCommandButtonBackground(e.Graphics, (e.Item as System.Windows.Forms.ToolStripButton), e.ToolStrip);
				}
				else if (e.Item is System.Windows.Forms.ToolStripDropDownButton)
				{
					Theming.Theme.CurrentTheme.DrawDropDownButtonBackground(e.Graphics, (e.Item as System.Windows.Forms.ToolStripDropDownButton), e.ToolStrip);

				}
				else if (e.Item is System.Windows.Forms.ToolStripSplitButton)
				{
					Theming.Theme.CurrentTheme.DrawSplitButtonBackground(e.Graphics, (e.Item as System.Windows.Forms.ToolStripSplitButton), e.ToolStrip);
				}
			}
		}
		protected override void OnRenderArrow(System.Windows.Forms.ToolStripArrowRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				Theming.Theme.CurrentTheme.DrawArrow(e.Graphics, e.Item.Enabled, e.ArrowRectangle, e.Direction);
			}
		}
		protected override void OnRenderButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				Theming.Theme.CurrentTheme.DrawCommandButtonBackground(e.Graphics, (e.Item as System.Windows.Forms.ToolStripButton), e.ToolStrip);
			}
		}
		protected override void OnRenderDropDownButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				Theming.Theme.CurrentTheme.DrawDropDownButtonBackground(e.Graphics, (e.Item as System.Windows.Forms.ToolStripDropDownButton), e.ToolStrip);
			}
		}
		protected override void OnRenderGrip(System.Windows.Forms.ToolStripGripRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				Theming.Theme.CurrentTheme.DrawGrip(e.Graphics, e.GripBounds, e.ToolStrip.Orientation == System.Windows.Forms.Orientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical, e.ToolStrip.RightToLeft == System.Windows.Forms.RightToLeft.Yes);
			}
		}
		protected override void OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawImageMargin(e.Graphics, e.AffectedBounds, e.ToolStrip);
		}
		protected override void OnRenderItemCheck(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawCheck(e.Graphics, e.Item, e.ImageRectangle);
		}
		protected override void OnRenderItemImage(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				if (e.Image == null) return;
				Theming.Theme.CurrentTheme.DrawImage(e.Graphics, e.ImageRectangle, e.Image, e.Item);
			}
		}
		protected override void OnRenderItemText(System.Windows.Forms.ToolStripItemTextRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawText(e.Graphics, e.Text, e.TextColor, e.TextFont, e.TextRectangle, e.TextFormat, e.TextDirection, e.Item);
		}
		protected override void OnRenderLabelBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawLabel(e.Graphics, e.Item);
		}
		protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawMenuItemBackground(e.Graphics, e.Item);
		}
		protected override void OnRenderOverflowButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawOverflowButton(e.Graphics, e.Item, e.ToolStrip);
		}
		protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null)
			{
				if (!e.Item.Visible) return;
				Theming.Theme.CurrentTheme.DrawSeparator(e.Graphics, e.Item, e.Item.Bounds, e.Vertical);
			}
		}
		protected override void OnRenderSplitButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawSplitButtonBackground(e.Graphics, e.Item, e.ToolStrip);
		}
		protected override void OnRenderStatusStripSizingGrip(System.Windows.Forms.ToolStripRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawSizingGrip(e.Graphics, e.AffectedBounds);
		}
		protected override void OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawCommandBarBackground(e.Graphics, new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), e.ToolStrip.Bounds.Size), e.ToolStrip.Orientation == System.Windows.Forms.Orientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical, e.ToolStrip);
		}
		protected override void OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawCommandBarBorder(e.Graphics, e.ToolStrip, e.ConnectedArea);
		}
		protected override void OnRenderToolStripContentPanelBackground(System.Windows.Forms.ToolStripContentPanelRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawContentAreaBackground(e.Graphics, e.ToolStripContentPanel.Bounds);
			e.Handled = true;
		}
		protected override void OnRenderToolStripPanelBackground(System.Windows.Forms.ToolStripPanelRenderEventArgs e)
		{
			if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawCommandBarPanelBackground(e.Graphics, e.ToolStripPanel);
			e.Handled = true;
		}
		protected override void OnRenderToolStripStatusLabelBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
		{
			// if (Theming.Theme.CurrentTheme != null) Theming.Theme.CurrentTheme.DrawStatusBarLabelBackground();
		}
	}
}
