using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MBS.Framework.UserInterface.Theming;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class OfficeXPTheme : Office2000Theme
	{
		public OfficeXPTheme()
		{
			MetricTable.DropDownButtonPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
		}
		public override CommandBarMenuAnimationType CommandBarMenuAnimationType { get { return Theming.CommandBarMenuAnimationType.Fade; } }

		protected override void InitAeroColors()
		{
		}
		protected override void InitBlueLunaColors()
		{
		}
		protected override void InitOliveLunaColors()
		{
		}
		protected override void InitRoyaleColors()
		{
		}
		protected override void InitSilverLunaColors()
		{
		}
		protected override void InitSystemColors()
		{
		}

		public override void DrawDropDownBackground(Graphics graphics, Rectangle rectangle, ControlState state)
		{
			switch (state)
			{
				case ControlState.Normal:
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.DropDownBackgroundColorNormal), rectangle);
					graphics.DrawRectangle(new Pen(ColorTable.DropDownBorderColorNormal), rectangle);
					break;
				}
				case ControlState.Hover:
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.DropDownBackgroundColorHover), rectangle);
					graphics.DrawRectangle(new Pen(ColorTable.DropDownBorderColorHover), rectangle);
					break;
				}
				case ControlState.Pressed:
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.DropDownBackgroundColorPressed), rectangle);
					graphics.DrawRectangle(new Pen(ColorTable.DropDownBorderColorPressed), rectangle);
					break;
				}
			}
		}

		private Color HSLTransform(Color color, double percentL, double percentS)
		{
			DrawingTools.HSLColor hslc = new DrawingTools.HSLColor(color);
			if (percentL > 0)
			{
				hslc.Luminosity = (hslc.Luminosity + ((255 - hslc.Luminosity) * percentL) / 100);
			}
			else if (percentL < 0)
			{
				hslc.Luminosity = ((hslc.Luminosity * (100 + percentL)) / 100);
			}
			if (percentS > 0)
			{
				hslc.Saturation = (hslc.Saturation + ((255 - hslc.Saturation) * percentS) / 100);
			}
			else if (percentS < 0)
			{
				hslc.Saturation = ((hslc.Saturation * (100 + percentS)) / 100);
			}
			return hslc;
		}

		protected override void InitCommonColors()
		{
			ColorTable.CommandBarControlBackgroundHover = HSLTransform(Color.FromKnownColor(KnownColor.Highlight), 70, -57);
			ColorTable.CommandBarControlBorderHover = Color.FromKnownColor(KnownColor.Highlight);

			ColorTable.CommandBarMenuControlBorderPressed = Color.FromKnownColor(KnownColor.ControlDark);
			ColorTable.CommandBarMenuControlBackgroundPressed = Color.FromKnownColor(KnownColor.Control);

			ColorTable.CommandBarControlBorderPressed = Color.FromKnownColor(KnownColor.Highlight);
			ColorTable.CommandBarControlBackgroundPressed = HSLTransform(Color.FromKnownColor(KnownColor.Highlight), 50, -50);

			ColorTable.CommandBarBackground = HSLTransform(Color.FromKnownColor(KnownColor.Control), 20, 0);

			ColorTable.CommandBarPanelGradientBegin = Color.FromKnownColor(KnownColor.Control);
			ColorTable.CommandBarPanelGradientEnd = Color.FromKnownColor(KnownColor.Control);

			ColorTable.CommandBarMenuBorder = Color.FromKnownColor(KnownColor.ControlDark);
			ColorTable.CommandBarMenuBackground = Color.White;
			
			ColorTable.CommandBarImageMarginBackground = Color.FromKnownColor(KnownColor.Control);

			ColorTable.DropDownBackgroundColorNormal = Color.FromKnownColor(KnownColor.Window);
			ColorTable.DropDownBackgroundColorHover = Color.FromKnownColor(KnownColor.Window);
			ColorTable.DropDownBackgroundColorPressed = Color.FromKnownColor(KnownColor.Window);

			ColorTable.DropDownBorderColorNormal = Color.FromKnownColor(KnownColor.ControlDark);
			ColorTable.DropDownBorderColorHover = Color.FromKnownColor(KnownColor.Highlight);
			ColorTable.DropDownBorderColorPressed = Color.FromKnownColor(KnownColor.Highlight);

			ColorTable.DropDownMenuBackground = Color.FromKnownColor(KnownColor.Window);
			ColorTable.DropDownMenuBorder = Color.FromKnownColor(KnownColor.ControlDark);

			ColorTable.DropDownButtonBackgroundNormal = Color.FromKnownColor(KnownColor.Control);
			ColorTable.DropDownButtonBackgroundHover = ColorTable.GetAlphaBlendedColorHighRes(Color.FromKnownColor(KnownColor.Highlight), Color.FromKnownColor(KnownColor.Window), 30);
			ColorTable.DropDownButtonBackgroundPressed = ColorTable.GetAlphaBlendedColorHighRes(Color.FromKnownColor(KnownColor.Highlight), Color.FromKnownColor(KnownColor.Window), 50);

			FontTable.Default = new Font("Tahoma", 8, FontStyle.Regular);
			FontTable.CommandBar = new Font("Tahoma", 8, FontStyle.Regular);
			FontTable.DialogFont = new Font("Tahoma", 8, FontStyle.Regular);
			FontTable.DocumentTabTextSelected = new Font("Tahoma", 8, FontStyle.Bold);
		}

		public override void DrawCommandButtonBackground(Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
			Rectangle rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
			if (item.IsOnDropDown)
			{
				rect.X += 2;
				rect.Y += 1;
				rect.Width -= 2;
				rect.Height -= 1;
			}

			if (item.Pressed)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundPressed), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderPressed), rect);
			}
			else if (item.Selected)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), rect);
			}
		}
		public override void DrawSplitButtonBackground(Graphics graphics, System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)
		{
			System.Windows.Forms.ToolStripSplitButton tssb = (item as System.Windows.Forms.ToolStripSplitButton);
			Rectangle rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
			if (item.IsOnDropDown)
			{
				rect.X += 2;
				rect.Y += 1;
				rect.Width -= 2;
				rect.Height -= 1;
			}

			if (tssb.DropDownButtonPressed)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarMenuControlBackgroundPressed), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarMenuControlBorderPressed), rect);
			}
			else if (tssb.ButtonPressed)
			{
				rect = new Rectangle(0, 0, tssb.ButtonBounds.Width, tssb.ButtonBounds.Height);
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundPressed), rect);

				rect = tssb.DropDownButtonBounds;
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);

				graphics.DrawLine(new Pen(ColorTable.CommandBarControlBorderHover), tssb.ButtonBounds.Width, 0, tssb.ButtonBounds.Width, tssb.ButtonBounds.Height);
				
				rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), rect);
			}
			else if (item.Selected)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), rect);

				graphics.DrawLine(new Pen(ColorTable.CommandBarControlBorderHover), tssb.ButtonBounds.Width, 0, tssb.ButtonBounds.Width, tssb.ButtonBounds.Height);
			}

			DrawArrow(graphics, item.Enabled, new Rectangle(tssb.DropDownButtonBounds.X, tssb.DropDownButtonBounds.Y, tssb.DropDownButtonBounds.Width, tssb.DropDownButtonBounds.Height), System.Windows.Forms.ArrowDirection.Down);
		}
		public override void DrawDropDownButton(Graphics graphics, Rectangle rectangle, ControlState dropdownState, ControlState buttonState)
		{
			switch (dropdownState)
			{
				case ControlState.Normal:
				case ControlState.Disabled:
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.DropDownButtonBackgroundNormal), rectangle);
					graphics.DrawRectangle(new Pen(ColorTable.DropDownBorderColorNormal), rectangle);
					break;
				}
				case ControlState.Hover:
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.DropDownButtonBackgroundHover), rectangle);
					graphics.DrawRectangle(new Pen(ColorTable.DropDownBorderColorHover), rectangle);
					break;
				}
				case ControlState.Pressed:
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.DropDownButtonBackgroundPressed), rectangle);
					graphics.DrawRectangle(new Pen(ColorTable.DropDownBorderColorPressed), rectangle);
					break;
				}
			}
		}
		public override void DrawDropDownButtonBackground(Graphics graphics, System.Windows.Forms.ToolStripDropDownButton item, System.Windows.Forms.ToolStrip parent)
		{
			Rectangle rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
			if (item.IsOnDropDown)
			{
				rect.X += 2;
				rect.Y += 1;
				rect.Width -= 2;
				rect.Height -= 1;
			}

			if (item.Pressed)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundPressed), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderPressed), rect);
			}
			else if (item.Selected)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), rect);
			}
			DrawArrow(graphics, item.Enabled, new Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height), System.Windows.Forms.ArrowDirection.Down);
		}
		public override void DrawDropDownMenuBackground(Graphics graphics, Rectangle rectangle)
		{
			base.DrawDropDownMenuBackground(graphics, rectangle);
			graphics.DrawRectangle(new Pen(ColorTable.DropDownMenuBorder), rectangle);
		}

		public override void DrawMenuItemBackground(Graphics graphics, System.Windows.Forms.ToolStripItem item)
		{
			Rectangle rect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
			if (item.IsOnDropDown)
			{
				rect.X += 2;
				rect.Y += 1;
				rect.Width -= 2;
				rect.Height -= 1;
			}

			if (item.Pressed)
			{
				if (item.IsOnDropDown)
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);
					graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), rect);
				}
				else
				{
					graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarMenuControlBackgroundPressed), rect);
					graphics.DrawRectangle(new Pen(ColorTable.CommandBarMenuControlBorderPressed), rect);
				}
			}
			else if (item.Selected)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarControlBackgroundHover), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarControlBorderHover), rect);
			}
		}

		public override void DrawCommandBarBackground(Graphics graphics, System.Drawing.Rectangle rectangle, Orientation orientation, System.Windows.Forms.ToolStrip parent)
		{
			Rectangle rect = new Rectangle(0, 0, parent.Bounds.Width - 1, parent.Bounds.Height - 1);
			if (parent is System.Windows.Forms.MenuStrip)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarGradientMenuBarBackgroundBegin), rect);
			}
			else if (parent is System.Windows.Forms.ToolStripDropDown)
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarMenuBackground), rect);
				graphics.DrawRectangle(new Pen(ColorTable.CommandBarMenuBorder), rect);
				
				System.Windows.Forms.ToolStripItem ownerItem = (parent as System.Windows.Forms.ToolStripDropDown).OwnerItem;
				if (ownerItem != null && !ownerItem.IsOnDropDown)
				{
					graphics.DrawLine(new Pen(ColorTable.CommandBarMenuBackground), rect.X + 1, rect.Y, ownerItem.Width - 2, rect.Y);
				}
			}
			else
			{
				graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarBackground), rect);
			}
		}
		public override void DrawImage(Graphics graphics, Rectangle imageRectangle, Image image, System.Windows.Forms.ToolStripItem item)
		{
			if (item.Selected)
			{
				Bitmap bitmap = new Bitmap(image.Width, image.Height);
				Graphics g = Graphics.FromImage(bitmap);

				float brt = 0f;
				System.Drawing.Imaging.ImageAttributes attrs = new System.Drawing.Imaging.ImageAttributes();
				System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(new float[][]
				{
					new float[] { brt, brt, brt, 0.0f, 0.0f },
					new float[] { brt, brt, brt, 0.0f, 0.0f }, 
					new float[] { brt, brt, brt, 0.0f, 0.0f }, 
					new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f }, 
					new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
				});
				attrs.SetColorMatrix(cm);

				Rectangle rectShadow = new Rectangle(1, 1, imageRectangle.Width, imageRectangle.Height);
				
				Rectangle rectImage = new Rectangle(0, 0, imageRectangle.Width, imageRectangle.Height);
				
				g.DrawImage(image, rectShadow, 0, 0, imageRectangle.Width, imageRectangle.Height, GraphicsUnit.Pixel, attrs);
				g.DrawImage(image, rectImage);
				g.Flush();
				
				base.DrawImage(graphics, imageRectangle, bitmap, item);
			}
			else
			{
				Rectangle rectImage = imageRectangle;
				rectImage.Offset(1, 1);
				base.DrawImage(graphics, rectImage, image, item);
			}
		}
		public override void DrawImageMargin(Graphics graphics, Rectangle affectedBounds, System.Windows.Forms.ToolStrip toolStrip)
		{
			Rectangle rect = new Rectangle(affectedBounds.X + 1, affectedBounds.Y + 1, affectedBounds.Width - 2, affectedBounds.Height - 2);
			graphics.FillRectangle(new SolidBrush(ColorTable.CommandBarImageMarginBackground), rect);
		}
		public override void DrawCommandBarBorder(Graphics graphics, System.Windows.Forms.ToolStrip toolStrip, Rectangle connectedArea)
		{
			// do nothing
		}

		public override void DrawSeparator(Graphics graphics, System.Windows.Forms.ToolStripItem item, Rectangle rectangle, bool vertical)
		{
			if (item.IsOnDropDown)
			{

			}
			graphics.DrawLine(DrawingTools.Pens.ShadowPen, rectangle.X + 16 + 8, rectangle.Height / 2, rectangle.Right, rectangle.Height / 2);
		}

		public override void DrawGrip(Graphics graphics, Rectangle gripBounds, Orientation orientation, bool rtl)
		{
			for (int y = 4; y < gripBounds.Height - 6; y += 2)
			{
				graphics.DrawLine(DrawingTools.Pens.ShadowPen, 0, y, 2, y);
			}
		}
	}
}
