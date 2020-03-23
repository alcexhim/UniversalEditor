using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class VisualStudio2005Theme : Office2003Theme
	{
		protected override void InitSystemColors()
		{
			base.InitSystemColors();
		}
		protected override void InitCommonColors()
		{
			base.InitCommonColors();
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(229, 229, 215);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(243, 242, 231);
			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(250, 249, 245);
			ColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(236, 231, 224);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(192, 192, 168);

			ColorTable.CommandBarDragHandleShadow = Color.FromKnownColor(KnownColor.ControlDark);
			ColorTable.CommandBarDragHandle = Color.FromKnownColor(KnownColor.ControlLightLight);
		}
		protected override void InitBlueLunaColors()
		{
			base.InitBlueLunaColors();
			ColorTable.CommandBarControlBorderHover = Color.FromArgb(49, 106, 197);
			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(75, 75, 111);
			
			// Colors for toolbar buttons - Hover
			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = Color.FromArgb(193, 210, 238);
			ColorTable.CommandBarGradientSelectedMiddle = Color.FromArgb(193, 210, 238);
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = Color.FromArgb(193, 210, 238);
			// Colors for toolbar buttons - Pressed
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(152, 181, 226);
			ColorTable.CommandBarGradientPressedMiddle = Color.FromArgb(152, 181, 226);
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(152, 181, 226);
			
			// Colors for top-level menu items - Hover
			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(193, 210, 238);
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(193, 210, 238);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(193, 210, 238);
			// Colors for top-level menu items - Pressed
			ColorTable.CommandBarGradientMenuTitleBackgroundBegin = Color.FromArgb(251, 251, 249);
			ColorTable.CommandBarGradientMenuTitleBackgroundEnd = Color.FromArgb(247, 245, 239);

			// Colors for sub-level menu items - Hover
			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(193, 210, 238);
			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(49, 106, 197);

			// The background color of the check square
			ColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(225, 230, 232);
			// The background color of the check square (hover)
			ColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(49, 106, 197);

			ColorTable.CommandBarMenuBorder = Color.FromKnownColor(KnownColor.ControlDark);
		}
		protected override void InitOliveLunaColors()
		{
			base.InitOliveLunaColors();
		}
		protected override void InitSilverLunaColors()
		{
			base.InitSilverLunaColors();
		}
		protected override void InitRoyaleColors()
		{
			base.InitRoyaleColors();
		}
	}
}
