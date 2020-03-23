using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

using MBS.Framework.UserInterface.Theming;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming.BuiltinThemes
{
	public class Office2003Theme : OfficeXPTheme
	{
		private bool mvarRoundedEdges = true;
		public bool RoundedEdges
		{
			get { return mvarRoundedEdges; }
			set { mvarRoundedEdges = value; }
		}

		private static Size onePix = new Size(1, 1);

		#region Color Setup
		protected override void InitAeroColors()
		{
			base.InitAeroColors();
		}
		protected override void InitCommonColors()
		{
			base.InitCommonColors();

			if (!UseLowResolution)
			{
				ColorTable.ButtonPressedHighlight = ColorTable.GetAlphaBlendedColor(System.Drawing.SystemColors.Window, ColorTable.GetAlphaBlendedColor(System.Drawing.SystemColors.Highlight, System.Drawing.SystemColors.Window, 160), 50);
				ColorTable.ButtonCheckedHighlight = ColorTable.GetAlphaBlendedColor(System.Drawing.SystemColors.Window, ColorTable.GetAlphaBlendedColor(System.Drawing.SystemColors.Highlight, System.Drawing.SystemColors.Window, 80), 20);
				ColorTable.ButtonSelectedHighlight = ColorTable.ButtonCheckedHighlight;
				return;
			}
			ColorTable.ButtonPressedHighlight = System.Drawing.SystemColors.Highlight;
			ColorTable.ButtonCheckedHighlight = System.Drawing.SystemColors.ControlLight;
			ColorTable.ButtonSelectedHighlight = System.Drawing.SystemColors.ControlLight;
		}
		protected override void InitSystemColors()
		{
			base.InitSystemColors();
			Color buttonFace = System.Drawing.SystemColors.ButtonFace;
			Color buttonShadow = System.Drawing.SystemColors.ButtonShadow;
			Color highlight = System.Drawing.SystemColors.Highlight;
			Color window = System.Drawing.SystemColors.Window;
			Color empty = Color.Empty;
			Color controlText = System.Drawing.SystemColors.ControlText;
			Color buttonHighlight = System.Drawing.SystemColors.ButtonHighlight;
			Color grayText = System.Drawing.SystemColors.GrayText;
			Color highlightText = System.Drawing.SystemColors.HighlightText;
			Color windowText = System.Drawing.SystemColors.WindowText;
			Color value = buttonFace;
			Color value2 = buttonFace;
			Color value3 = buttonFace;
			Color value4 = highlight;
			Color value5 = highlight;

			if (UseLowResolution)
			{
				value4 = window;
			}
			else if (!UseHighContrast)
			{
				value = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 23);
				value2 = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 50);
				value3 = System.Drawing.SystemColors.ButtonFace;
				value4 = ColorTable.GetAlphaBlendedColorHighRes(highlight, window, 30);
				value5 = ColorTable.GetAlphaBlendedColorHighRes(highlight, window, 50);
			}

			if (UseLowResolution || UseHighContrast)
			{
				ColorTable.CommandBarBackground = buttonFace;
				ColorTable.CommandBarControlBackgroundSelectedHover = System.Drawing.SystemColors.ControlLight;
				ColorTable.CommandBarDragHandle = controlText;
				ColorTable.CommandBarPanelGradientEnd = buttonFace;
				ColorTable.CommandBarGradientOptionsBegin = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 70);
				ColorTable.CommandBarGradientOptionsMiddle = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 90);
				ColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 40);
				ColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 70);
				ColorTable.CommandBarGradientMenuIconBackgroundDroppedEnd = ColorTable.GetAlphaBlendedColorHighRes(buttonFace, window, 90);
				ColorTable.CommandBarMenuBorder = ColorTable.GetAlphaBlendedColorHighRes(controlText, buttonShadow, 20);
				ColorTable.CommandBarMenuBackground =ColorTable. GetAlphaBlendedColorHighRes(buttonFace, window, 143);
				ColorTable.CommandBarToolbarSplitterLine = ColorTable.GetAlphaBlendedColorHighRes(buttonShadow, window, 70);
				ColorTable.CommandBarMenuSplitterLine = ColorTable.GetAlphaBlendedColorHighRes(buttonShadow, window, 70);
			}
			ColorTable.CommandBarControlBackgroundSelected = (UseLowResolution ? System.Drawing.SystemColors.ControlLight : highlight);
			ColorTable.CommandBarBorderOuterDocked = buttonFace;
			ColorTable.CommandBarBorderOuterDocked = buttonShadow;
			ColorTable.CommandBarBorderOuterFloating = buttonShadow;
			ColorTable.CommandBarControlBorderPressed = highlight;
			ColorTable.CommandBarControlBorderHover = highlight;
			ColorTable.CommandBarControlBorderSelected = highlight;
			ColorTable.CommandBarControlBorderSelectedHover = highlight;
			ColorTable.CommandBarControlBackground = empty;
			ColorTable.CommandBarControlBackgroundHighlight = window;
			ColorTable.CommandBarControlBackgroundPressed = highlight;
			ColorTable.CommandBarControlBackgroundHover = window;
			ColorTable.CommandBarControlText = controlText;
			ColorTable.CommandBarControlTextDisabled = buttonShadow;
			ColorTable.CommandBarControlTextHover = controlText;
			ColorTable.CommandBarControlTextPressed = highlightText;
			ColorTable.CommandBarControlTextHover = windowText;
			ColorTable.CommandBarDockSeparatorLine = empty;
			ColorTable.CommandBarDragHandleShadow = window;
			ColorTable.CommandBarDropDownArrow = empty;
			ColorTable.CommandBarPanelGradientBegin = buttonFace;
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = value4;
			ColorTable.CommandBarControlBackgroundHoverGradientBegin = value4;
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = value4;
			ColorTable.CommandBarGradientOptionsEnd = buttonShadow;
			ColorTable.CommandBarGradientOptionsHoverBegin = empty;
			ColorTable.CommandBarGradientOptionsHoverEnd = empty;
			ColorTable.CommandBarGradientOptionsHoverMiddle = empty;
			ColorTable.CommandBarGradientOptionsSelectedBegin = empty;
			ColorTable.CommandBarGradientOptionsSelectedEnd = empty;
			ColorTable.CommandBarGradientOptionsSelectedMiddle = empty;
			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = empty;
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = empty;
			ColorTable.CommandBarGradientSelectedMiddle = empty;
			ColorTable.CommandBarGradientVerticalBegin = value;
			ColorTable.CommandBarGradientVerticalMiddle = value2;
			ColorTable.CommandBarGradientVerticalEnd = value3;
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = value5;
			ColorTable.CommandBarGradientPressedMiddle = value5;
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = value5;
			ColorTable.CommandBarGradientMenuTitleBackgroundBegin = value;
			ColorTable.CommandBarGradientMenuTitleBackgroundEnd = value2;
			ColorTable.CommandBarIconDisabledDark = buttonShadow;
			ColorTable.CommandBarIconDisabledHighlight = buttonFace;
			ColorTable.CommandBarLabelBackground = buttonShadow;
			ColorTable.CommandBarLowColorIconDisabled = empty;
			ColorTable.CommandBarMainMenuBackground = buttonFace;
			ColorTable.CommandBarMenuControlText = windowText;
			ColorTable.CommandBarMenuControlTextDisabled = grayText;
			ColorTable.CommandBarMenuIconBackground = empty;
			ColorTable.CommandBarMenuIconBackgroundDropped = buttonShadow;
			ColorTable.CommandBarMenuShadow = empty;
			ColorTable.CommandBarMenuSplitArrow = buttonShadow;
			ColorTable.CommandBarOptionsButtonShadow = empty;
			ColorTable.CommandBarShadow = ColorTable.CommandBarBackground;
			
			ColorTable.CommandBarToolbarSplitterLineHighlight = buttonHighlight;
			ColorTable.CommandBarMenuSplitterLineHighlight = buttonHighlight;

			ColorTable.CommandBarTearOffHandle = empty;
			ColorTable.CommandBarTearOffHandleHover = empty;
			ColorTable.CommandBarTitleBackground = buttonShadow;
			ColorTable.CommandBarTitleText = buttonHighlight;
			ColorTable.DisabledFocuslessHighlightedText = grayText;
			ColorTable.DisabledHighlightedText = grayText;
			ColorTable.DialogGroupBoxText = controlText;
			ColorTable.DocumentTabBorder = buttonShadow;
			ColorTable.DocumentTabBorderDark = buttonFace;
			ColorTable.DocumentTabBorderDarkPressed = highlight;
			ColorTable.DocumentTabBorderDarkHover = System.Drawing.SystemColors.MenuText;
			ColorTable.DocumentTabBorderHighlight = buttonFace;
			ColorTable.DocumentTabBorderHighlightPressed = highlight;
			ColorTable.DocumentTabBorderHighlightHover = System.Drawing.SystemColors.MenuText;
			ColorTable.DocumentTabBorderPressed = highlight;
			ColorTable.DocumentTabBorderHover = System.Drawing.SystemColors.MenuText;
			ColorTable.DocumentTabBorderSelected = buttonShadow;
			ColorTable.DocumentTabBackground = buttonFace;
			ColorTable.DocumentTabBackgroundPressed = highlight;
			ColorTable.DocumentTabBackgroundHover = highlight;
			ColorTable.DocumentTabBackgroundSelected = window;
			ColorTable.DocumentTabText = controlText;
			ColorTable.DocumentTabTextPressed = highlightText;
			ColorTable.DocumentTabTextHover = highlight;
			ColorTable.DocumentTabTextSelected = windowText;
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = buttonShadow;
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = buttonShadow;
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = buttonShadow;
			ColorTable.DockingWindowActiveTabTextNormal = controlText;
			ColorTable.DockingWindowActiveTabTextDisabled = controlText;
			ColorTable.DockingWindowInactiveTabBackgroundGradientBegin = buttonShadow;
			ColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = buttonShadow;
			ColorTable.DockingWindowInactiveTabBackgroundGradientEnd = buttonShadow;
			ColorTable.DockingWindowInactiveTabText = controlText;
			ColorTable.DockingWindowTabBackgroundPressed = buttonFace;
			ColorTable.DockingWindowTabBackgroundHover = buttonFace;
			ColorTable.DockingWindowTabTextPressed = controlText;
			ColorTable.DockingWindowTabTextHover = controlText;
			ColorTable.FocusedHighlightedBackground = highlight;
			ColorTable.FocusedHighlightedBorder = highlight;
			ColorTable.FocusedHighlightedForeground = highlightText;
			ColorTable.FocuslessHighlightedBackground = System.Drawing.SystemColors.InactiveCaption;
			ColorTable.FocuslessHighlightedForeground = System.Drawing.SystemColors.InactiveCaptionText;
			ColorTable.GridHeaderBorder = highlight;
			ColorTable.GridHeaderBackground = window;
			ColorTable.GridHeaderCellBorder = buttonShadow;
			ColorTable.GridHeaderCellBackground = buttonFace;
			ColorTable.GridHeaderCellBackgroundSelected = empty;
			ColorTable.GridHeaderSeeThroughSelection = highlight;
			ColorTable.GSPDarkBackground = window;
			ColorTable.GSPGroupContentDarkBackground = window;
			ColorTable.GSPGroupContentHighlightBackground = window;
			ColorTable.GSPGroupContentText = windowText;
			ColorTable.GSPGroupContentTextDisabled = grayText;
			ColorTable.GSPGroupHeaderDarkBackground = window;
			ColorTable.GSPGroupHeaderLightBackground = window;
			ColorTable.GSPGroupHeaderText = windowText;
			ColorTable.GSPGroupLine = window;
			ColorTable.GSPHyperlink = empty;
			ColorTable.GSPHighlightBackground = window;
			ColorTable.Hyperlink = empty;
			ColorTable.HyperlinkFollowed = empty;
			ColorTable.JotNavUIBorder = windowText;
			ColorTable.JotNavUIGradientBegin = window;
			ColorTable.JotNavUIGradientEnd = window;
			ColorTable.JotNavUIGradientMiddle = window;
			ColorTable.JotNavUIText = windowText;
			ColorTable.ListViewColumnHeaderArrowNormal = controlText;
			ColorTable.NetLookBackground = empty;
			ColorTable.OABBackground = buttonShadow;
			ColorTable.OBBackgroundBorder = buttonShadow;
			ColorTable.OBBackgroundBorderContrast = window;
			ColorTable.OGMDIParentWorkspaceBackground = buttonShadow;
			ColorTable.OGRulerActiveBackground = window;
			ColorTable.OGRulerBorder = controlText;
			ColorTable.OGRulerBackground = buttonFace;
			ColorTable.OGRulerInactiveBackground = buttonShadow;
			ColorTable.OGRulerTabBoxBorder = buttonShadow;
			ColorTable.OGRulerTabBoxBorderHighlight = buttonHighlight;
			ColorTable.OGRulerTabStopTicks = buttonShadow;
			ColorTable.OGRulerText = windowText;
			ColorTable.OGTaskPaneGroupBoxHeaderBackground = buttonFace;
			ColorTable.OGWorkspaceBackground = buttonShadow;
			ColorTable.OutlookFlagNone = buttonHighlight;
			ColorTable.OutlookFolderBarDark = buttonShadow;
			ColorTable.OutlookFolderBarLight = buttonShadow;
			ColorTable.OutlookFolderBarText = window;
			ColorTable.OutlookGridlines = buttonShadow;
			ColorTable.OutlookGroupLine = buttonShadow;
			ColorTable.OutlookGroupNested = buttonFace;
			ColorTable.OutlookGroupShaded = buttonFace;
			ColorTable.OutlookGroupText = buttonShadow;
			ColorTable.OutlookIconBar = buttonFace;
			ColorTable.OutlookInfoBarBackground = buttonFace;
			ColorTable.OutlookInfoBarText = controlText;
			ColorTable.OutlookPreviewPaneLabelText = windowText;
			ColorTable.OutlookTodayIndicatorDark = highlight;
			ColorTable.OutlookTodayIndicatorLight = buttonFace;
			ColorTable.OutlookWBActionDividerLine = buttonShadow;
			ColorTable.OutlookWBButtonDark = buttonFace;
			ColorTable.OutlookWBButtonLight = buttonHighlight;
			ColorTable.OutlookWBDarkOutline = buttonShadow;
			ColorTable.OutlookWBFoldersBackground = window;
			ColorTable.OutlookWBHoverButtonDark = empty;
			ColorTable.OutlookWBHoverButtonLight = empty;
			ColorTable.OutlookWBLabelText = windowText;
			ColorTable.OutlookWBPressedButtonDark = empty;
			ColorTable.OutlookWBPressedButtonLight = empty;
			ColorTable.OutlookWBSelectedButtonDark = empty;
			ColorTable.OutlookWBSelectedButtonLight = empty;
			ColorTable.OutlookWBSplitterDark = buttonShadow;
			ColorTable.OutlookWBSplitterLight = buttonShadow;
			ColorTable.PlacesBarBackground = buttonFace;
			ColorTable.OutlineThumbnailsPaneTabAreaBackground = buttonFace;
			ColorTable.OutlineThumbnailsPaneTabBorder = buttonShadow;
			ColorTable.OutlineThumbnailsPaneTabInactiveBackground = buttonFace;
			ColorTable.OutlineThumbnailsPaneTabText = windowText;
			ColorTable.PowerPointSlideBorderActiveSelected = highlight;
			ColorTable.PowerPointSlideBorderActiveSelectedHover = highlight;
			ColorTable.PowerPointSlideBorderInactiveSelected = grayText;
			ColorTable.PowerPointSlideBorderHover = highlight;
			ColorTable.PublisherPrintDocumentScratchPageBackground = buttonFace;
			ColorTable.PublisherWebDocumentScratchPageBackground = buttonFace;
			ColorTable.ScrollbarBorder = buttonShadow;
			ColorTable.ScrollbarBackground = buttonShadow;
			ColorTable.ToastGradientBegin = buttonFace;
			ColorTable.ToastGradientEnd = buttonFace;
			ColorTable.WordProcessorBorderInnerDocked = empty;
			ColorTable.WordProcessorBorderOuterDocked = buttonFace;
			ColorTable.WordProcessorBorderOuterFloating = buttonShadow;
			ColorTable.WordProcessorBackground = window;
			ColorTable.WordProcessorControlBorder = buttonShadow;
			ColorTable.WordProcessorControlBorderDefault = controlText;
			ColorTable.WordProcessorControlBorderDisabled = buttonShadow;
			ColorTable.WordProcessorControlBackground = buttonFace;
			ColorTable.WordProcessorControlBackgroundDisabled = buttonFace;
			ColorTable.WordProcessorControlText = controlText;
			ColorTable.WordProcessorControlTextDisabled = buttonShadow;
			ColorTable.WordProcessorControlTextPressed = highlightText;
			ColorTable.WordProcessorGroupLine = buttonShadow;
			ColorTable.WordProcessorInfoTipBackground = System.Drawing.SystemColors.Info;
			ColorTable.WordProcessorInfoTipText = System.Drawing.SystemColors.InfoText;
			ColorTable.WordProcessorNavigationBarBackground = buttonFace;
			ColorTable.WordProcessorText = windowText;
			ColorTable.WordProcessorTextDisabled = grayText;
			ColorTable.WordProcessorTitleBackgroundActive = highlight;
			ColorTable.WordProcessorTitleBackgroundInactive = buttonFace;
			ColorTable.WordProcessorTitleTextActive = highlightText;
			ColorTable.WordProcessorTitleTextInactive = controlText;
			ColorTable.FormulaBarBackground = buttonFace;
		}
		protected override void InitBlueLunaColors()
		{
			ColorTable.CommandBarBorderOuterDocked = Color.FromArgb(196, 205, 218);
			ColorTable.CommandBarBorderOuterFloating = Color.FromArgb(42, 102, 201);
			ColorTable.CommandBarBackground = Color.FromArgb(196, 219, 249);
			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(0, 0, 128);
			ColorTable.CommandBarControlBorderHover = Color.FromArgb(0, 0, 128);
			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(0, 0, 128);
			ColorTable.CommandBarControlBorderSelectedHover = Color.FromArgb(0, 0, 128);
			ColorTable.CommandBarControlBackground = Color.FromArgb(196, 219, 249);
			ColorTable.CommandBarControlBackgroundHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(255, 192, 111);
			ColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextDisabled = Color.FromArgb(141, 141, 141);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(128, 128, 128);
			ColorTable.CommandBarControlTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarDockSeparatorLine = Color.FromArgb(0, 53, 145);
			ColorTable.CommandBarDragHandle = Color.FromArgb(39, 65, 118);
			ColorTable.CommandBarDragHandleShadow = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarDropDownArrow = Color.FromArgb(236, 233, 216);
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(158, 190, 245);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(196, 218, 250);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = Color.FromArgb(203, 221, 246);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedEnd = Color.FromArgb(114, 155, 215);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = Color.FromArgb(161, 197, 249);
			ColorTable.CommandBarGradientMenuTitleBackgroundBegin = Color.FromArgb(227, 239, 255);
			ColorTable.CommandBarGradientMenuTitleBackgroundEnd = Color.FromArgb(123, 164, 224);
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(255, 223, 154);
			ColorTable.CommandBarGradientPressedMiddle = Color.FromArgb(255, 177, 109);
			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(255, 255, 222);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(255, 203, 136);
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(255, 225, 172);
			ColorTable.CommandBarGradientOptionsBegin = Color.FromArgb(127, 177, 250);
			ColorTable.CommandBarGradientOptionsEnd = Color.FromArgb(0, 53, 145);
			ColorTable.CommandBarGradientOptionsMiddle = Color.FromArgb(82, 127, 208);
			ColorTable.CommandBarGradientOptionsHoverBegin = Color.FromArgb(255, 255, 222);
			ColorTable.CommandBarGradientOptionsHoverEnd = Color.FromArgb(255, 193, 118);
			ColorTable.CommandBarGradientOptionsHoverMiddle = Color.FromArgb(255, 225, 172);
			ColorTable.CommandBarGradientOptionsSelectedBegin = Color.FromArgb(254, 140, 73);
			ColorTable.CommandBarGradientOptionsSelectedEnd = Color.FromArgb(255, 221, 152);
			ColorTable.CommandBarGradientOptionsSelectedMiddle = Color.FromArgb(255, 184, 116);
			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = Color.FromArgb(255, 223, 154);
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = Color.FromArgb(255, 166, 76);
			ColorTable.CommandBarGradientSelectedMiddle = Color.FromArgb(255, 195, 116);
			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(227, 239, 255);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(123, 164, 224);
			ColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(203, 225, 252);
			ColorTable.CommandBarIconDisabledDark = Color.FromArgb(97, 122, 172);
			ColorTable.CommandBarIconDisabledHighlight = Color.FromArgb(233, 236, 242);
			ColorTable.CommandBarLabelBackground = Color.FromArgb(186, 211, 245);
			ColorTable.CommandBarLowColorIconDisabled = Color.FromArgb(109, 150, 208);
			ColorTable.CommandBarMainMenuBackground = Color.FromArgb(153, 204, 255);
			ColorTable.CommandBarMenuBorder = Color.FromArgb(0, 45, 150);
			ColorTable.CommandBarMenuBackground = Color.FromArgb(246, 246, 246);
			ColorTable.CommandBarMenuControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarMenuControlTextDisabled = Color.FromArgb(141, 141, 141);
			ColorTable.CommandBarMenuIconBackground = Color.FromArgb(203, 225, 252);
			ColorTable.CommandBarMenuIconBackgroundDropped = Color.FromArgb(172, 183, 201);
			ColorTable.CommandBarMenuShadow = Color.FromArgb(95, 130, 234);
			ColorTable.CommandBarMenuSplitArrow = Color.FromArgb(128, 128, 128);
			ColorTable.CommandBarOptionsButtonShadow = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarShadow = Color.FromArgb(59, 97, 156);
			
			ColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(106, 140, 203);
			ColorTable.CommandBarMenuSplitterLine = Color.FromArgb(106, 140, 203);

			ColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromArgb(241, 249, 255);
			ColorTable.CommandBarMenuSplitterLineHighlight = Color.FromArgb(241, 249, 255);

			ColorTable.CommandBarTearOffHandle = Color.FromArgb(169, 199, 240);
			ColorTable.CommandBarTearOffHandleHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarTitleBackground = Color.FromArgb(42, 102, 201);
			ColorTable.CommandBarTitleText = Color.FromArgb(255, 255, 255);
			ColorTable.DisabledFocuslessHighlightedText = Color.FromArgb(172, 168, 153);
			ColorTable.DisabledHighlightedText = Color.FromArgb(187, 206, 236);
			ColorTable.DialogGroupBoxText = Color.FromArgb(0, 70, 213);
			ColorTable.DocumentTabBorder = Color.FromArgb(0, 53, 154);
			ColorTable.DocumentTabBorderDark = Color.FromArgb(117, 166, 241);
			ColorTable.DocumentTabBorderDarkPressed = Color.FromArgb(0, 0, 128);
			ColorTable.DocumentTabBorderDarkHover = Color.FromArgb(0, 0, 128);
			ColorTable.DocumentTabBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabBorderHighlightPressed = Color.FromArgb(0, 0, 128);
			ColorTable.DocumentTabBorderHighlightHover = Color.FromArgb(0, 0, 128);
			ColorTable.DocumentTabBorderPressed = Color.FromArgb(0, 0, 128);
			ColorTable.DocumentTabBorderHover = Color.FromArgb(0, 0, 128);
			ColorTable.DocumentTabBorderSelected = Color.FromArgb(59, 97, 156);
			ColorTable.DocumentTabBackground = Color.FromArgb(186, 211, 245);
			ColorTable.DocumentTabBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.DocumentTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DocumentTabBackgroundSelected = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabText = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextSelected = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(186, 211, 245);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(186, 211, 245);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(186, 211, 245);
			ColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabTextDisabled = Color.FromArgb(94, 94, 94);
			ColorTable.DockingWindowInactiveTabBackgroundGradientBegin = Color.FromArgb(129, 169, 226);
			ColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = Color.FromArgb(129, 169, 226);
			ColorTable.DockingWindowInactiveTabBackgroundGradientEnd = Color.FromArgb(129, 169, 226);
			ColorTable.DockingWindowInactiveTabText = Color.FromArgb(255, 255, 255);
			ColorTable.DockingWindowTabBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.DockingWindowTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DockingWindowTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.FocuslessHighlightedBackground = Color.FromArgb(236, 233, 216);
			ColorTable.FocuslessHighlightedForeground = Color.FromArgb(0, 0, 0);
			ColorTable.GridHeaderBorder = Color.FromArgb(89, 89, 172);
			ColorTable.GridHeaderBackground = Color.FromArgb(239, 235, 222);
			ColorTable.GridHeaderCellBorder = Color.FromArgb(126, 125, 104);
			ColorTable.GridHeaderCellBackground = Color.FromArgb(239, 235, 222);
			ColorTable.GridHeaderCellBackgroundSelected = Color.FromArgb(255, 192, 111);
			ColorTable.GridHeaderSeeThroughSelection = Color.FromArgb(191, 191, 223);
			ColorTable.GSPDarkBackground = Color.FromArgb(74, 122, 201);
			ColorTable.GSPGroupContentDarkBackground = Color.FromArgb(185, 208, 241);
			ColorTable.GSPGroupContentHighlightBackground = Color.FromArgb(221, 236, 254);
			ColorTable.GSPGroupContentText = Color.FromArgb(0, 0, 0);
			ColorTable.GSPGroupContentTextDisabled = Color.FromArgb(150, 145, 133);
			ColorTable.GSPGroupHeaderDarkBackground = Color.FromArgb(101, 143, 224);
			ColorTable.GSPGroupHeaderLightBackground = Color.FromArgb(196, 219, 249);
			ColorTable.GSPGroupHeaderText = Color.FromArgb(0, 45, 134);
			ColorTable.GSPGroupLine = Color.FromArgb(255, 255, 255);
			ColorTable.GSPHyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.GSPHighlightBackground = Color.FromArgb(221, 236, 254);
			ColorTable.Hyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.HyperlinkFollowed = Color.FromArgb(170, 0, 170);
			ColorTable.JotNavUIBorder = Color.FromArgb(59, 97, 156);
			ColorTable.JotNavUIGradientBegin = Color.FromArgb(158, 190, 245);
			ColorTable.JotNavUIGradientEnd = Color.FromArgb(255, 255, 255);
			ColorTable.JotNavUIGradientMiddle = Color.FromArgb(196, 218, 250);
			ColorTable.JotNavUIText = Color.FromArgb(0, 0, 0);
			ColorTable.ListViewColumnHeaderArrowNormal = Color.FromArgb(172, 168, 153);
			ColorTable.NetLookBackground = Color.FromArgb(227, 239, 255);
			ColorTable.OABBackground = Color.FromArgb(128, 128, 128);
			ColorTable.OBBackgroundBorder = Color.FromArgb(128, 128, 128);
			ColorTable.OBBackgroundBorderContrast = Color.FromArgb(255, 255, 255);
			ColorTable.OGMDIParentWorkspaceBackground = Color.FromArgb(144, 153, 174);
			ColorTable.OGRulerActiveBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OGRulerBorder = Color.FromArgb(0, 0, 0);
			ColorTable.OGRulerBackground = Color.FromArgb(216, 231, 252);
			ColorTable.OGRulerInactiveBackground = Color.FromArgb(158, 190, 245);
			ColorTable.OGRulerTabBoxBorder = Color.FromArgb(75, 120, 202);
			ColorTable.OGRulerTabBoxBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.OGRulerTabStopTicks = Color.FromArgb(128, 128, 128);
			ColorTable.OGRulerText = Color.FromArgb(0, 0, 0);
			ColorTable.OGTaskPaneGroupBoxHeaderBackground = Color.FromArgb(186, 211, 245);
			ColorTable.OGWorkspaceBackground = Color.FromArgb(144, 153, 174);
			ColorTable.OutlookFlagNone = Color.FromArgb(242, 240, 228);
			ColorTable.OutlookFolderBarDark = Color.FromArgb(0, 53, 145);
			ColorTable.OutlookFolderBarLight = Color.FromArgb(89, 135, 214);
			ColorTable.OutlookFolderBarText = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookGridlines = Color.FromArgb(234, 233, 225);
			ColorTable.OutlookGroupLine = Color.FromArgb(123, 164, 224);
			ColorTable.OutlookGroupNested = Color.FromArgb(253, 238, 201);
			ColorTable.OutlookGroupShaded = Color.FromArgb(190, 218, 251);
			ColorTable.OutlookGroupText = Color.FromArgb(55, 104, 185);
			ColorTable.OutlookIconBar = Color.FromArgb(253, 247, 233);
			ColorTable.OutlookInfoBarBackground = Color.FromArgb(144, 153, 174);
			ColorTable.OutlookInfoBarText = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookPreviewPaneLabelText = Color.FromArgb(144, 153, 174);
			ColorTable.OutlookTodayIndicatorDark = Color.FromArgb(187, 85, 3);
			ColorTable.OutlookTodayIndicatorLight = Color.FromArgb(251, 200, 79);
			ColorTable.OutlookWBActionDividerLine = Color.FromArgb(215, 228, 251);
			ColorTable.OutlookWBButtonDark = Color.FromArgb(123, 164, 224);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(203, 225, 252);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(203, 225, 252);
			ColorTable.OutlookWBDarkOutline = Color.FromArgb(0, 45, 150);
			ColorTable.OutlookWBFoldersBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookWBHoverButtonDark = Color.FromArgb(247, 190, 87);
			ColorTable.OutlookWBHoverButtonLight = Color.FromArgb(255, 255, 220);
			ColorTable.OutlookWBLabelText = Color.FromArgb(50, 69, 105);
			ColorTable.OutlookWBPressedButtonDark = Color.FromArgb(248, 222, 128);
			ColorTable.OutlookWBPressedButtonLight = Color.FromArgb(232, 127, 8);
			ColorTable.OutlookWBSelectedButtonDark = Color.FromArgb(238, 147, 17);
			ColorTable.OutlookWBSelectedButtonLight = Color.FromArgb(251, 230, 148);
			ColorTable.OutlookWBSplitterDark = Color.FromArgb(0, 53, 145);
			ColorTable.OutlookWBSplitterLight = Color.FromArgb(89, 135, 214);
			ColorTable.PlacesBarBackground = Color.FromArgb(236, 233, 216);
			ColorTable.OutlineThumbnailsPaneTabAreaBackground = Color.FromArgb(195, 218, 249);
			ColorTable.OutlineThumbnailsPaneTabBorder = Color.FromArgb(59, 97, 156);
			ColorTable.OutlineThumbnailsPaneTabInactiveBackground = Color.FromArgb(158, 190, 245);
			ColorTable.OutlineThumbnailsPaneTabText = Color.FromArgb(0, 0, 0);
			ColorTable.PowerPointSlideBorderActiveSelected = Color.FromArgb(61, 108, 192);
			ColorTable.PowerPointSlideBorderActiveSelectedHover = Color.FromArgb(61, 108, 192);
			ColorTable.PowerPointSlideBorderInactiveSelected = Color.FromArgb(128, 128, 128);
			ColorTable.PowerPointSlideBorderHover = Color.FromArgb(61, 108, 192);
			ColorTable.PublisherPrintDocumentScratchPageBackground = Color.FromArgb(144, 153, 174);
			ColorTable.PublisherWebDocumentScratchPageBackground = Color.FromArgb(189, 194, 207);
			ColorTable.ScrollbarBorder = Color.FromArgb(211, 211, 211);
			ColorTable.ScrollbarBackground = Color.FromArgb(251, 251, 248);
			ColorTable.ToastGradientBegin = Color.FromArgb(220, 236, 254);
			ColorTable.ToastGradientEnd = Color.FromArgb(167, 197, 238);
			ColorTable.WordProcessorBorderInnerDocked = Color.FromArgb(185, 212, 249);
			ColorTable.WordProcessorBorderOuterDocked = Color.FromArgb(196, 218, 250);
			ColorTable.WordProcessorBorderOuterFloating = Color.FromArgb(42, 102, 201);
			ColorTable.WordProcessorBackground = Color.FromArgb(221, 236, 254);
			ColorTable.WordProcessorControlBorder = Color.FromArgb(127, 157, 185);
			ColorTable.WordProcessorControlBorderDefault = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlBorderDisabled = Color.FromArgb(128, 128, 128);
			ColorTable.WordProcessorControlBackground = Color.FromArgb(169, 199, 240);
			ColorTable.WordProcessorControlBackgroundDisabled = Color.FromArgb(222, 222, 222);
			ColorTable.WordProcessorControlText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlTextDisabled = Color.FromArgb(172, 168, 153);
			ColorTable.WordProcessorControlTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorGroupLine = Color.FromArgb(123, 164, 224);
			ColorTable.WordProcessorInfoTipBackground = Color.FromArgb(255, 255, 204);
			ColorTable.WordProcessorInfoTipText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorNavigationBarBackground = Color.FromArgb(74, 122, 201);
			ColorTable.WordProcessorText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorTextDisabled = Color.FromArgb(172, 168, 153);
			ColorTable.WordProcessorTitleBackgroundActive = Color.FromArgb(123, 164, 224);
			ColorTable.WordProcessorTitleBackgroundInactive = Color.FromArgb(148, 187, 239);
			ColorTable.WordProcessorTitleTextActive = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorTitleTextInactive = Color.FromArgb(0, 0, 0);
			ColorTable.FormulaBarBackground = Color.FromArgb(158, 190, 245);
		}
		protected override void InitOliveLunaColors()
		{
			ColorTable.CommandBarBorderOuterDocked = Color.FromArgb(81, 94, 51);
			ColorTable.CommandBarBorderOuterFloating = Color.FromArgb(116, 134, 94);
			ColorTable.CommandBarBackground = Color.FromArgb(209, 222, 173);
			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(63, 93, 56);
			ColorTable.CommandBarControlBorderHover = Color.FromArgb(63, 93, 56);
			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(63, 93, 56);
			ColorTable.CommandBarControlBorderSelectedHover = Color.FromArgb(63, 93, 56);
			ColorTable.CommandBarControlBackground = Color.FromArgb(209, 222, 173);
			ColorTable.CommandBarControlBackgroundHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(255, 192, 111);
			ColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextDisabled = Color.FromArgb(141, 141, 141);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(128, 128, 128);
			ColorTable.CommandBarControlTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarDockSeparatorLine = Color.FromArgb(96, 119, 66);
			ColorTable.CommandBarDragHandle = Color.FromArgb(81, 94, 51);
			ColorTable.CommandBarDragHandleShadow = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarDropDownArrow = Color.FromArgb(236, 233, 216);
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(217, 217, 167);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(242, 241, 228);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = Color.FromArgb(230, 230, 209);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedEnd = Color.FromArgb(160, 177, 116);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = Color.FromArgb(186, 201, 143);
			ColorTable.CommandBarGradientMenuTitleBackgroundBegin = Color.FromArgb(237, 240, 214);
			ColorTable.CommandBarGradientMenuTitleBackgroundEnd = Color.FromArgb(181, 196, 143);
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(255, 223, 154);
			ColorTable.CommandBarGradientPressedMiddle = Color.FromArgb(255, 177, 109);
			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(255, 255, 222);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(255, 203, 136);
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(255, 225, 172);
			ColorTable.CommandBarGradientOptionsBegin = Color.FromArgb(186, 204, 150);
			ColorTable.CommandBarGradientOptionsEnd = Color.FromArgb(96, 119, 107);
			ColorTable.CommandBarGradientOptionsMiddle = Color.FromArgb(141, 160, 107);
			ColorTable.CommandBarGradientOptionsHoverBegin = Color.FromArgb(255, 255, 222);
			ColorTable.CommandBarGradientOptionsHoverEnd = Color.FromArgb(255, 193, 118);
			ColorTable.CommandBarGradientOptionsHoverMiddle = Color.FromArgb(255, 225, 172);
			ColorTable.CommandBarGradientOptionsSelectedBegin = Color.FromArgb(254, 140, 73);
			ColorTable.CommandBarGradientOptionsSelectedEnd = Color.FromArgb(255, 221, 152);
			ColorTable.CommandBarGradientOptionsSelectedMiddle = Color.FromArgb(255, 184, 116);
			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = Color.FromArgb(255, 223, 154);
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = Color.FromArgb(255, 166, 76);
			ColorTable.CommandBarGradientSelectedMiddle = Color.FromArgb(255, 195, 116);
			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(255, 255, 237);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(181, 196, 143);
			ColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(206, 220, 167);
			ColorTable.CommandBarIconDisabledDark = Color.FromArgb(131, 144, 113);
			ColorTable.CommandBarIconDisabledHighlight = Color.FromArgb(243, 244, 240);
			ColorTable.CommandBarLabelBackground = Color.FromArgb(218, 227, 187);
			ColorTable.CommandBarLabelBackground = Color.FromArgb(218, 227, 187);
			ColorTable.CommandBarLowColorIconDisabled = Color.FromArgb(159, 174, 122);
			ColorTable.CommandBarMainMenuBackground = Color.FromArgb(236, 233, 216);
			ColorTable.CommandBarMenuBorder = Color.FromArgb(117, 141, 94);
			ColorTable.CommandBarMenuBackground = Color.FromArgb(244, 244, 238);
			ColorTable.CommandBarMenuControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarMenuControlTextDisabled = Color.FromArgb(141, 141, 141);
			ColorTable.CommandBarMenuIconBackground = Color.FromArgb(216, 227, 182);
			ColorTable.CommandBarMenuIconBackgroundDropped = Color.FromArgb(173, 181, 157);
			ColorTable.CommandBarMenuShadow = Color.FromArgb(134, 148, 108);
			ColorTable.CommandBarMenuSplitArrow = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarOptionsButtonShadow = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarShadow = Color.FromArgb(96, 128, 88);
			
			ColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(96, 128, 88);
			ColorTable.CommandBarMenuSplitterLine = Color.FromArgb(96, 128, 88);
			
			ColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromArgb(244, 247, 222);
			ColorTable.CommandBarMenuSplitterLineHighlight = Color.FromArgb(244, 247, 222);

			ColorTable.CommandBarTearOffHandle = Color.FromArgb(197, 212, 159);
			ColorTable.CommandBarTearOffHandleHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarTitleBackground = Color.FromArgb(116, 134, 94);
			ColorTable.CommandBarTitleText = Color.FromArgb(255, 255, 255);
			ColorTable.DisabledFocuslessHighlightedText = Color.FromArgb(172, 168, 153);
			ColorTable.DisabledHighlightedText = Color.FromArgb(220, 224, 208);
			ColorTable.DialogGroupBoxText = Color.FromArgb(153, 84, 10);
			ColorTable.DocumentTabBorder = Color.FromArgb(96, 119, 107);
			ColorTable.DocumentTabBorderDark = Color.FromArgb(176, 194, 140);
			ColorTable.DocumentTabBorderDarkPressed = Color.FromArgb(63, 93, 56);
			ColorTable.DocumentTabBorderDarkHover = Color.FromArgb(63, 93, 56);
			ColorTable.DocumentTabBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabBorderHighlightPressed = Color.FromArgb(63, 93, 56);
			ColorTable.DocumentTabBorderHighlightHover = Color.FromArgb(63, 93, 56);
			ColorTable.DocumentTabBorderPressed = Color.FromArgb(63, 93, 56);
			ColorTable.DocumentTabBorderHover = Color.FromArgb(63, 93, 56);
			ColorTable.DocumentTabBorderSelected = Color.FromArgb(96, 128, 88);
			ColorTable.DocumentTabBackground = Color.FromArgb(218, 227, 187);
			ColorTable.DocumentTabBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.DocumentTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DocumentTabBackgroundSelected = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabText = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextSelected = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(218, 227, 187);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(218, 227, 187);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(218, 227, 187);
			ColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabTextDisabled = Color.FromArgb(128, 128, 128);
			ColorTable.DockingWindowInactiveTabBackgroundGradientBegin = Color.FromArgb(183, 198, 145);
			ColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = Color.FromArgb(183, 198, 145);
			ColorTable.DockingWindowInactiveTabBackgroundGradientEnd = Color.FromArgb(183, 198, 145);
			ColorTable.DockingWindowInactiveTabText = Color.FromArgb(255, 255, 255);
			ColorTable.DockingWindowTabBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.DockingWindowTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DockingWindowTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.FocuslessHighlightedBackground = Color.FromArgb(236, 233, 216);
			ColorTable.FocuslessHighlightedForeground = Color.FromArgb(0, 0, 0);
			ColorTable.GridHeaderBorder = Color.FromArgb(191, 191, 223);
			ColorTable.GridHeaderBackground = Color.FromArgb(239, 235, 222);
			ColorTable.GridHeaderCellBorder = Color.FromArgb(126, 125, 104);
			ColorTable.GridHeaderCellBackground = Color.FromArgb(239, 235, 222);
			ColorTable.GridHeaderCellBackgroundSelected = Color.FromArgb(255, 192, 111);
			ColorTable.GridHeaderSeeThroughSelection = Color.FromArgb(128, 128, 128);
			ColorTable.GSPDarkBackground = Color.FromArgb(159, 171, 128);
			ColorTable.GSPDarkBackground = Color.FromArgb(159, 171, 128);
			ColorTable.GSPGroupContentDarkBackground = Color.FromArgb(217, 227, 187);
			ColorTable.GSPGroupContentHighlightBackground = Color.FromArgb(230, 234, 208);
			ColorTable.GSPGroupContentText = Color.FromArgb(0, 0, 0);
			ColorTable.GSPGroupContentTextDisabled = Color.FromArgb(150, 145, 133);
			ColorTable.GSPGroupHeaderDarkBackground = Color.FromArgb(161, 176, 128);
			ColorTable.GSPGroupHeaderLightBackground = Color.FromArgb(210, 223, 174);
			ColorTable.GSPGroupHeaderText = Color.FromArgb(90, 107, 70);
			ColorTable.GSPGroupHeaderText = Color.FromArgb(90, 107, 70);
			ColorTable.GSPGroupLine = Color.FromArgb(255, 255, 255);
			ColorTable.GSPHyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.GSPHighlightBackground = Color.FromArgb(243, 242, 231);
			ColorTable.Hyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.HyperlinkFollowed = Color.FromArgb(170, 0, 170);
			ColorTable.JotNavUIBorder = Color.FromArgb(96, 128, 88);
			ColorTable.JotNavUIBorder = Color.FromArgb(96, 128, 88);
			ColorTable.JotNavUIGradientBegin = Color.FromArgb(217, 217, 167);
			ColorTable.JotNavUIGradientBegin = Color.FromArgb(217, 217, 167);
			ColorTable.JotNavUIGradientEnd = Color.FromArgb(255, 255, 255);
			ColorTable.JotNavUIGradientMiddle = Color.FromArgb(242, 241, 228);
			ColorTable.JotNavUIGradientMiddle = Color.FromArgb(242, 241, 228);
			ColorTable.JotNavUIText = Color.FromArgb(0, 0, 0);
			ColorTable.ListViewColumnHeaderArrowNormal = Color.FromArgb(172, 168, 153);
			ColorTable.NetLookBackground = Color.FromArgb(255, 255, 237);
			ColorTable.OABBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OBBackgroundBorder = Color.FromArgb(211, 211, 211);
			ColorTable.OBBackgroundBorderContrast = Color.FromArgb(128, 128, 128);
			ColorTable.OGMDIParentWorkspaceBackground = Color.FromArgb(151, 160, 123);
			ColorTable.OGRulerActiveBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OGRulerBorder = Color.FromArgb(0, 0, 0);
			ColorTable.OGRulerBackground = Color.FromArgb(226, 231, 191);
			ColorTable.OGRulerInactiveBackground = Color.FromArgb(171, 192, 138);
			ColorTable.OGRulerTabBoxBorder = Color.FromArgb(117, 141, 94);
			ColorTable.OGRulerTabBoxBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.OGRulerTabStopTicks = Color.FromArgb(128, 128, 128);
			ColorTable.OGRulerText = Color.FromArgb(0, 0, 0);
			ColorTable.OGTaskPaneGroupBoxHeaderBackground = Color.FromArgb(218, 227, 187);
			ColorTable.OGWorkspaceBackground = Color.FromArgb(151, 160, 123);
			ColorTable.OutlookFlagNone = Color.FromArgb(242, 240, 228);
			ColorTable.OutlookFolderBarDark = Color.FromArgb(96, 119, 66);
			ColorTable.OutlookFolderBarLight = Color.FromArgb(175, 192, 130);
			ColorTable.OutlookFolderBarText = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookGridlines = Color.FromArgb(234, 233, 225);
			ColorTable.OutlookGroupLine = Color.FromArgb(181, 196, 143);
			ColorTable.OutlookGroupNested = Color.FromArgb(253, 238, 201);
			ColorTable.OutlookGroupShaded = Color.FromArgb(175, 186, 145);
			ColorTable.OutlookGroupText = Color.FromArgb(115, 137, 84);
			ColorTable.OutlookIconBar = Color.FromArgb(253, 247, 233);
			ColorTable.OutlookInfoBarBackground = Color.FromArgb(151, 160, 123);
			ColorTable.OutlookInfoBarText = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookPreviewPaneLabelText = Color.FromArgb(151, 160, 123);
			ColorTable.OutlookTodayIndicatorDark = Color.FromArgb(187, 85, 3);
			ColorTable.OutlookTodayIndicatorLight = Color.FromArgb(251, 200, 79);
			ColorTable.OutlookWBActionDividerLine = Color.FromArgb(200, 212, 172);
			ColorTable.OutlookWBButtonDark = Color.FromArgb(176, 191, 138);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(234, 240, 207);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(234, 240, 207);
			ColorTable.OutlookWBDarkOutline = Color.FromArgb(96, 128, 88);
			ColorTable.OutlookWBFoldersBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookWBHoverButtonDark = Color.FromArgb(247, 190, 87);
			ColorTable.OutlookWBHoverButtonLight = Color.FromArgb(255, 255, 220);
			ColorTable.OutlookWBLabelText = Color.FromArgb(50, 69, 105);
			ColorTable.OutlookWBPressedButtonDark = Color.FromArgb(248, 222, 128);
			ColorTable.OutlookWBPressedButtonLight = Color.FromArgb(232, 127, 8);
			ColorTable.OutlookWBSelectedButtonDark = Color.FromArgb(238, 147, 17);
			ColorTable.OutlookWBSelectedButtonLight = Color.FromArgb(251, 230, 148);
			ColorTable.OutlookWBSplitterDark = Color.FromArgb(64, 81, 59);
			ColorTable.OutlookWBSplitterLight = Color.FromArgb(120, 142, 111);
			ColorTable.PlacesBarBackground = Color.FromArgb(236, 233, 216);
			ColorTable.OutlineThumbnailsPaneTabAreaBackground = Color.FromArgb(242, 240, 228);
			ColorTable.OutlineThumbnailsPaneTabBorder = Color.FromArgb(96, 128, 88);
			ColorTable.OutlineThumbnailsPaneTabInactiveBackground = Color.FromArgb(206, 220, 167);
			ColorTable.OutlineThumbnailsPaneTabText = Color.FromArgb(0, 0, 0);
			ColorTable.PowerPointSlideBorderActiveSelected = Color.FromArgb(107, 129, 107);
			ColorTable.PowerPointSlideBorderActiveSelectedHover = Color.FromArgb(107, 129, 107);
			ColorTable.PowerPointSlideBorderInactiveSelected = Color.FromArgb(128, 128, 128);
			ColorTable.PowerPointSlideBorderHover = Color.FromArgb(107, 129, 107);
			ColorTable.PublisherPrintDocumentScratchPageBackground = Color.FromArgb(151, 160, 123);
			ColorTable.PublisherWebDocumentScratchPageBackground = Color.FromArgb(193, 198, 176);
			ColorTable.ScrollbarBorder = Color.FromArgb(211, 211, 211);
			ColorTable.ScrollbarBackground = Color.FromArgb(249, 249, 247);
			ColorTable.ToastGradientBegin = Color.FromArgb(237, 242, 212);
			ColorTable.ToastGradientEnd = Color.FromArgb(191, 206, 153);
			ColorTable.WordProcessorBorderInnerDocked = Color.FromArgb(255, 255, 255);
			ColorTable.WordProcessorBorderOuterDocked = Color.FromArgb(242, 241, 228);
			ColorTable.WordProcessorBorderOuterFloating = Color.FromArgb(116, 134, 94);
			ColorTable.WordProcessorBackground = Color.FromArgb(243, 242, 231);
			ColorTable.WordProcessorControlBorder = Color.FromArgb(164, 185, 127);
			ColorTable.WordProcessorControlBorderDefault = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlBorderDefault = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlBorderDisabled = Color.FromArgb(128, 128, 128);
			ColorTable.WordProcessorControlBackground = Color.FromArgb(197, 212, 159);
			ColorTable.WordProcessorControlBackgroundDisabled = Color.FromArgb(222, 222, 222);
			ColorTable.WordProcessorControlText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlTextDisabled = Color.FromArgb(172, 168, 153);
			ColorTable.WordProcessorControlTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorGroupLine = Color.FromArgb(188, 187, 177);
			ColorTable.WordProcessorInfoTipBackground = Color.FromArgb(255, 255, 204);
			ColorTable.WordProcessorInfoTipText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorNavigationBarBackground = Color.FromArgb(116, 134, 94);
			ColorTable.WordProcessorText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorTextDisabled = Color.FromArgb(172, 168, 153);
			ColorTable.WordProcessorTitleBackgroundActive = Color.FromArgb(216, 227, 182);
			ColorTable.WordProcessorTitleBackgroundInactive = Color.FromArgb(188, 205, 131);
			ColorTable.WordProcessorTitleTextActive = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorTitleTextInactive = Color.FromArgb(0, 0, 0);
			ColorTable.FormulaBarBackground = Color.FromArgb(217, 217, 167);
		}
		protected override void InitRoyaleColors()
		{
			ColorTable.CommandBarBackground = Color.FromArgb(238, 237, 240);
			ColorTable.CommandBarDragHandle = Color.FromArgb(189, 188, 191);
			ColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(193, 193, 196);
			ColorTable.CommandBarMenuSplitterLine = Color.FromArgb(193, 193, 196);
			ColorTable.CommandBarTitleBackground = Color.FromArgb(167, 166, 170);
			ColorTable.CommandBarTitleText = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarBorderOuterFloating = Color.FromArgb(142, 141, 145);
			ColorTable.CommandBarBorderOuterDocked = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarTearOffHandle = Color.FromArgb(238, 237, 240);
			ColorTable.CommandBarTearOffHandleHover = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarControlBackground = Color.FromArgb(238, 237, 240);
			ColorTable.CommandBarControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextDisabled = Color.FromArgb(176, 175, 179);
			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarControlBorderHover = Color.FromArgb(51, 94, 168);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(153, 175, 212);
			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(51, 94, 168);
			ColorTable.CommandBarControlTextPressed = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(51, 94, 168);
			ColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(51, 94, 168);
			ColorTable.CommandBarControlBorderSelectedHover = Color.FromArgb(51, 94, 168);
			ColorTable.CommandBarControlBackgroundHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(167, 166, 170);
			ColorTable.CommandBarMainMenuBackground = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarMenuBackground = Color.FromArgb(252, 252, 252);
			ColorTable.CommandBarMenuControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarMenuControlTextDisabled = Color.FromArgb(193, 193, 196);
			ColorTable.CommandBarMenuBorder = Color.FromArgb(134, 133, 136);
			ColorTable.CommandBarMenuIconBackground = Color.FromArgb(238, 237, 240);
			ColorTable.CommandBarMenuIconBackgroundDropped = Color.FromArgb(228, 226, 230);
			ColorTable.CommandBarMenuSplitArrow = Color.FromArgb(167, 166, 170);
			ColorTable.WordProcessorBackground = Color.FromArgb(245, 244, 246);
			ColorTable.WordProcessorText = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorTitleBackgroundActive = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorTitleBackgroundInactive = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorTitleTextActive = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorTitleTextInactive = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorBorderOuterFloating = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorBorderOuterDocked = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlBorder = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlText = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlBackground = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlBorderDisabled = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlTextDisabled = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlBackgroundDisabled = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorControlBorderDefault = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorGroupLine = Color.FromArgb(255, 51, 153);
			ColorTable.ScrollbarBorder = Color.FromArgb(255, 51, 153);
			ColorTable.OBBackgroundBorder = Color.FromArgb(255, 51, 153);
			ColorTable.OBBackgroundBorderContrast = Color.FromArgb(255, 51, 153);
			ColorTable.OABBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GridHeaderBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GridHeaderBorder = Color.FromArgb(255, 51, 153);
			ColorTable.GridHeaderCellBorder = Color.FromArgb(255, 51, 153);
			ColorTable.GridHeaderSeeThroughSelection = Color.FromArgb(255, 51, 153);
			ColorTable.GridHeaderCellBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GridHeaderCellBackgroundSelected = Color.FromArgb(255, 51, 153);
			ColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarMenuSplitterLineHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarShadow = Color.FromArgb(238, 237, 240);
			ColorTable.CommandBarOptionsButtonShadow = Color.FromArgb(245, 244, 246);
			ColorTable.WordProcessorNavigationBarBackground = Color.FromArgb(193, 193, 196);
			ColorTable.WordProcessorBorderInnerDocked = Color.FromArgb(245, 244, 246);
			ColorTable.CommandBarLabelBackground = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarIconDisabledHighlight = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarIconDisabledDark = Color.FromArgb(167, 166, 170);
			ColorTable.CommandBarLowColorIconDisabled = Color.FromArgb(176, 175, 179);
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(251, 250, 251);
			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(252, 252, 252);
			ColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(245, 244, 246);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarGradientOptionsBegin = Color.FromArgb(242, 242, 242);
			ColorTable.CommandBarGradientOptionsMiddle = Color.FromArgb(224, 224, 225);
			ColorTable.CommandBarGradientOptionsEnd = Color.FromArgb(167, 166, 170);
			ColorTable.CommandBarGradientMenuTitleBackgroundBegin = Color.FromArgb(252, 252, 252);
			ColorTable.CommandBarGradientMenuTitleBackgroundEnd = Color.FromArgb(245, 244, 246);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = Color.FromArgb(247, 246, 248);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = Color.FromArgb(241, 240, 242);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedEnd = Color.FromArgb(228, 226, 230);
			ColorTable.CommandBarGradientOptionsSelectedBegin = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarGradientOptionsSelectedMiddle = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarGradientOptionsSelectedEnd = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarGradientOptionsHoverBegin = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarGradientOptionsHoverMiddle = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarGradientOptionsHoverEnd = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarGradientSelectedMiddle = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = Color.FromArgb(226, 229, 238);
			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(194, 207, 229);
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(153, 175, 212);
			ColorTable.CommandBarGradientPressedMiddle = Color.FromArgb(153, 175, 212);
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(153, 175, 212);
			ColorTable.NetLookBackground = Color.FromArgb(235, 233, 237);
			ColorTable.CommandBarMenuShadow = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarDockSeparatorLine = Color.FromArgb(51, 94, 168);
			ColorTable.CommandBarDropDownArrow = Color.FromArgb(235, 233, 237);
			ColorTable.OutlookGridlines = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookGroupText = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookGroupLine = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookGroupShaded = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookGroupNested = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookIconBar = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookFlagNone = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookFolderBarLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookFolderBarDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookFolderBarText = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBButtonDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBSelectedButtonLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBSelectedButtonDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBHoverButtonLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBHoverButtonDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBPressedButtonLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBPressedButtonDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBDarkOutline = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBSplitterLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBSplitterDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBActionDividerLine = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBLabelText = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookWBFoldersBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookTodayIndicatorLight = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookTodayIndicatorDark = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookInfoBarBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookInfoBarText = Color.FromArgb(255, 51, 153);
			ColorTable.OutlookPreviewPaneLabelText = Color.FromArgb(255, 51, 153);
			ColorTable.Hyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.HyperlinkFollowed = Color.FromArgb(170, 0, 170);
			ColorTable.OGWorkspaceBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OGMDIParentWorkspaceBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerActiveBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerInactiveBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerText = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerTabStopTicks = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerBorder = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerTabBoxBorder = Color.FromArgb(255, 51, 153);
			ColorTable.OGRulerTabBoxBorderHighlight = Color.FromArgb(255, 51, 153);
			ColorTable.FormulaBarBackground = Color.FromArgb(255, 51, 153);
			ColorTable.CommandBarDragHandleShadow = Color.FromArgb(255, 255, 255);
			ColorTable.OGTaskPaneGroupBoxHeaderBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OutlineThumbnailsPaneTabAreaBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OutlineThumbnailsPaneTabInactiveBackground = Color.FromArgb(255, 51, 153);
			ColorTable.OutlineThumbnailsPaneTabBorder = Color.FromArgb(255, 51, 153);
			ColorTable.OutlineThumbnailsPaneTabText = Color.FromArgb(255, 51, 153);
			ColorTable.PowerPointSlideBorderActiveSelected = Color.FromArgb(255, 51, 153);
			ColorTable.PowerPointSlideBorderInactiveSelected = Color.FromArgb(255, 51, 153);
			ColorTable.PowerPointSlideBorderHover = Color.FromArgb(255, 51, 153);
			ColorTable.PowerPointSlideBorderActiveSelectedHover = Color.FromArgb(255, 51, 153);
			ColorTable.DialogGroupBoxText = Color.FromArgb(0, 0, 0);
			ColorTable.ScrollbarBackground = Color.FromArgb(237, 235, 239);
			ColorTable.ListViewColumnHeaderArrowNormal = Color.FromArgb(155, 154, 156);
			ColorTable.DisabledHighlightedText = Color.FromArgb(188, 202, 226);
			ColorTable.FocuslessHighlightedBackground = Color.FromArgb(235, 233, 237);
			ColorTable.FocuslessHighlightedForeground = Color.FromArgb(0, 0, 0);
			ColorTable.DisabledFocuslessHighlightedText = Color.FromArgb(167, 166, 170);
			ColorTable.WordProcessorControlTextPressed = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorTextDisabled = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorInfoTipBackground = Color.FromArgb(255, 51, 153);
			ColorTable.WordProcessorInfoTipText = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowActiveTabTextDisabled = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowInactiveTabBackgroundGradientBegin = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowInactiveTabBackgroundGradientEnd = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowInactiveTabText = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowTabBackgroundHover = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowTabTextHover = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowTabBackgroundPressed = Color.FromArgb(255, 51, 153);
			ColorTable.DockingWindowTabTextPressed = Color.FromArgb(255, 51, 153);
			ColorTable.GSPHighlightBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GSPDarkBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupHeaderLightBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupHeaderDarkBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupHeaderText = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupContentHighlightBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupContentDarkBackground = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupContentText = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupContentTextDisabled = Color.FromArgb(255, 51, 153);
			ColorTable.GSPGroupLine = Color.FromArgb(255, 51, 153);
			ColorTable.GSPHyperlink = Color.FromArgb(255, 51, 153);
			ColorTable.DocumentTabBackground = Color.FromArgb(212, 212, 226);
			ColorTable.DocumentTabText = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabBorder = Color.FromArgb(118, 116, 146);
			ColorTable.DocumentTabBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabBorderDark = Color.FromArgb(186, 185, 206);
			ColorTable.DocumentTabBackgroundSelected = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabTextSelected = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabBorderSelected = Color.FromArgb(124, 124, 148);
			ColorTable.DocumentTabBackgroundHover = Color.FromArgb(193, 210, 238);
			ColorTable.DocumentTabTextHover = Color.FromArgb(49, 106, 197);
			ColorTable.DocumentTabBorderHover = Color.FromArgb(49, 106, 197);
			ColorTable.DocumentTabBorderHighlightHover = Color.FromArgb(49, 106, 197);
			ColorTable.DocumentTabBorderDarkHover = Color.FromArgb(49, 106, 197);
			ColorTable.DocumentTabBackgroundPressed = Color.FromArgb(154, 183, 228);
			ColorTable.DocumentTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabBorderPressed = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHighlightPressed = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderDarkPressed = Color.FromArgb(75, 75, 111);
			ColorTable.ToastGradientBegin = Color.FromArgb(246, 244, 236);
			ColorTable.ToastGradientEnd = Color.FromArgb(179, 178, 204);
			ColorTable.JotNavUIGradientBegin = Color.FromArgb(236, 233, 216);
			ColorTable.JotNavUIGradientMiddle = Color.FromArgb(236, 233, 216);
			ColorTable.JotNavUIGradientEnd = Color.FromArgb(255, 255, 255);
			ColorTable.JotNavUIText = Color.FromArgb(0, 0, 0);
			ColorTable.JotNavUIBorder = Color.FromArgb(172, 168, 153);
			ColorTable.PlacesBarBackground = Color.FromArgb(224, 223, 227);
			ColorTable.PublisherPrintDocumentScratchPageBackground = Color.FromArgb(152, 181, 226);
			ColorTable.PublisherWebDocumentScratchPageBackground = Color.FromArgb(193, 210, 238);
		}
		protected override void InitSilverLunaColors()
		{
			ColorTable.CommandBarBorderOuterDocked = Color.FromArgb(173, 174, 193);
			ColorTable.CommandBarBorderOuterFloating = Color.FromArgb(122, 121, 153);
			ColorTable.CommandBarBackground = Color.FromArgb(219, 218, 228);
			ColorTable.CommandBarControlBorderPressed = Color.FromArgb(75, 75, 111);
			ColorTable.CommandBarControlBorderHover = Color.FromArgb(75, 75, 111);
			ColorTable.CommandBarControlBorderSelected = Color.FromArgb(75, 75, 111);
			ColorTable.CommandBarControlBorderSelectedHover = Color.FromArgb(75, 75, 111);
			ColorTable.CommandBarControlBackground = Color.FromArgb(219, 218, 228);
			ColorTable.CommandBarControlBackgroundHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarControlBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarControlBackgroundSelected = Color.FromArgb(255, 192, 111);
			ColorTable.CommandBarControlBackgroundSelectedHover = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextDisabled = Color.FromArgb(141, 141, 141);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(128, 128, 128);
			ColorTable.CommandBarControlTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarControlTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarDockSeparatorLine = Color.FromArgb(110, 109, 143);
			ColorTable.CommandBarDragHandle = Color.FromArgb(84, 84, 117);
			ColorTable.CommandBarDragHandleShadow = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarDropDownArrow = Color.FromArgb(224, 223, 227);
			ColorTable.CommandBarPanelGradientBegin = Color.FromArgb(215, 215, 229);
			ColorTable.CommandBarPanelGradientEnd = Color.FromArgb(243, 243, 247);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedBegin = Color.FromArgb(215, 215, 226);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedEnd = Color.FromArgb(118, 116, 151);
			ColorTable.CommandBarGradientMenuIconBackgroundDroppedMiddle = Color.FromArgb(184, 185, 202);
			ColorTable.CommandBarGradientMenuTitleBackgroundBegin = Color.FromArgb(232, 233, 242);
			ColorTable.CommandBarGradientMenuTitleBackgroundEnd = Color.FromArgb(172, 170, 194);
			ColorTable.CommandBarControlBackgroundPressedGradientBegin = Color.FromArgb(254, 128, 62);
			ColorTable.CommandBarControlBackgroundPressedGradientEnd = Color.FromArgb(255, 223, 154);
			ColorTable.CommandBarGradientPressedMiddle = Color.FromArgb(255, 177, 109);
			ColorTable.CommandBarControlBackgroundHoverGradientBegin = Color.FromArgb(255, 255, 222);
			ColorTable.CommandBarControlBackgroundHoverGradientEnd = Color.FromArgb(255, 203, 136);
			ColorTable.CommandBarControlBackgroundHoverGradientMiddle = Color.FromArgb(255, 225, 172);
			ColorTable.CommandBarGradientOptionsBegin = Color.FromArgb(186, 185, 206);
			ColorTable.CommandBarGradientOptionsEnd = Color.FromArgb(118, 116, 146);
			ColorTable.CommandBarGradientOptionsMiddle = Color.FromArgb(156, 155, 180);
			ColorTable.CommandBarGradientOptionsHoverBegin = Color.FromArgb(255, 255, 222);
			ColorTable.CommandBarGradientOptionsHoverEnd = Color.FromArgb(255, 193, 118);
			ColorTable.CommandBarGradientOptionsHoverMiddle = Color.FromArgb(255, 225, 172);
			ColorTable.CommandBarGradientOptionsSelectedBegin = Color.FromArgb(254, 140, 73);
			ColorTable.CommandBarGradientOptionsSelectedEnd = Color.FromArgb(255, 221, 152);
			ColorTable.CommandBarGradientOptionsSelectedMiddle = Color.FromArgb(255, 184, 116);
			ColorTable.CommandBarControlBackgroundSelectedGradientBegin = Color.FromArgb(255, 223, 154);
			ColorTable.CommandBarControlBackgroundSelectedGradientEnd = Color.FromArgb(255, 166, 76);
			ColorTable.CommandBarGradientSelectedMiddle = Color.FromArgb(255, 195, 116);
			ColorTable.CommandBarGradientVerticalBegin = Color.FromArgb(249, 249, 255);
			ColorTable.CommandBarGradientVerticalEnd = Color.FromArgb(147, 145, 176);
			ColorTable.CommandBarGradientVerticalMiddle = Color.FromArgb(225, 226, 236);
			ColorTable.CommandBarIconDisabledDark = Color.FromArgb(122, 121, 153);
			ColorTable.CommandBarIconDisabledHighlight = Color.FromArgb(247, 245, 249);
			ColorTable.CommandBarLabelBackground = Color.FromArgb(212, 212, 226);
			ColorTable.CommandBarLabelBackground = Color.FromArgb(212, 212, 226);
			ColorTable.CommandBarLowColorIconDisabled = Color.FromArgb(168, 167, 190);
			ColorTable.CommandBarMainMenuBackground = Color.FromArgb(198, 200, 215);
			ColorTable.CommandBarMenuBorder = Color.FromArgb(124, 124, 148);
			ColorTable.CommandBarMenuBackground = Color.FromArgb(253, 250, 255);
			ColorTable.CommandBarMenuControlText = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarMenuControlTextDisabled = Color.FromArgb(141, 141, 141);
			ColorTable.CommandBarMenuIconBackground = Color.FromArgb(214, 211, 231);
			ColorTable.CommandBarMenuIconBackgroundDropped = Color.FromArgb(185, 187, 200);
			ColorTable.CommandBarMenuShadow = Color.FromArgb(154, 140, 176);
			ColorTable.CommandBarMenuSplitArrow = Color.FromArgb(0, 0, 0);
			ColorTable.CommandBarOptionsButtonShadow = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarShadow = Color.FromArgb(124, 124, 148);
			ColorTable.CommandBarToolbarSplitterLine = Color.FromArgb(110, 109, 143);
			ColorTable.CommandBarMenuSplitterLine = Color.FromArgb(110, 109, 143);
			ColorTable.CommandBarToolbarSplitterLineHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarMenuSplitterLineHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.CommandBarTearOffHandle = Color.FromArgb(192, 192, 211);
			ColorTable.CommandBarTearOffHandleHover = Color.FromArgb(255, 238, 194);
			ColorTable.CommandBarTitleBackground = Color.FromArgb(122, 121, 153);
			ColorTable.CommandBarTitleText = Color.FromArgb(255, 255, 255);
			ColorTable.DisabledFocuslessHighlightedText = Color.FromArgb(172, 168, 153);
			ColorTable.DisabledHighlightedText = Color.FromArgb(59, 59, 63);
			ColorTable.DialogGroupBoxText = Color.FromArgb(7, 70, 213);
			ColorTable.DocumentTabBorder = Color.FromArgb(118, 116, 146);
			ColorTable.DocumentTabBorderDark = Color.FromArgb(186, 185, 206);
			ColorTable.DocumentTabBorderDarkPressed = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderDarkHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderDarkHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderDarkHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabBorderHighlightPressed = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHighlightHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHighlightHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHighlightHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderPressed = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderHover = Color.FromArgb(75, 75, 111);
			ColorTable.DocumentTabBorderSelected = Color.FromArgb(124, 124, 148);
			ColorTable.DocumentTabBackground = Color.FromArgb(212, 212, 226);
			ColorTable.DocumentTabBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.DocumentTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DocumentTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DocumentTabBackgroundSelected = Color.FromArgb(255, 255, 255);
			ColorTable.DocumentTabText = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.DocumentTabTextSelected = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin = Color.FromArgb(212, 212, 226);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientMiddle= Color.FromArgb(212, 212, 226);
			ColorTable.DockingWindowActiveTabBackgroundNormalGradientEnd = Color.FromArgb(212, 212, 226);
			ColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabTextNormal = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowActiveTabTextDisabled = Color.FromArgb(148, 148, 148);
			ColorTable.DockingWindowActiveTabTextDisabled = Color.FromArgb(148, 148, 148);
			ColorTable.DockingWindowInactiveTabBackgroundGradientBegin = Color.FromArgb(171, 169, 194);
			ColorTable.DockingWindowInactiveTabBackgroundGradientMiddle = Color.FromArgb(171, 169, 194);
			ColorTable.DockingWindowInactiveTabBackgroundGradientEnd = Color.FromArgb(171, 169, 194);
			ColorTable.DockingWindowInactiveTabText = Color.FromArgb(255, 255, 255);
			ColorTable.DockingWindowInactiveTabText = Color.FromArgb(255, 255, 255);
			ColorTable.DockingWindowTabBackgroundPressed = Color.FromArgb(254, 128, 62);
			ColorTable.DockingWindowTabBackgroundHover = Color.FromArgb(255, 238, 194);
			ColorTable.DockingWindowTabTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.DockingWindowTabTextHover = Color.FromArgb(0, 0, 0);
			ColorTable.FocuslessHighlightedBackground = Color.FromArgb(224, 223, 227);
			ColorTable.FocuslessHighlightedBackground = Color.FromArgb(224, 223, 227);
			ColorTable.FocuslessHighlightedForeground = Color.FromArgb(0, 0, 0);
			ColorTable.FocuslessHighlightedForeground = Color.FromArgb(0, 0, 0);
			ColorTable.GridHeaderBorder = Color.FromArgb(191, 191, 223);
			ColorTable.GridHeaderBackground = Color.FromArgb(239, 235, 222);
			ColorTable.GridHeaderCellBorder = Color.FromArgb(126, 125, 104);
			ColorTable.GridHeaderCellBackground = Color.FromArgb(223, 223, 234);
			ColorTable.GridHeaderCellBackgroundSelected = Color.FromArgb(255, 192, 111);
			ColorTable.GridHeaderSeeThroughSelection = Color.FromArgb(128, 128, 128);
			ColorTable.GSPDarkBackground = Color.FromArgb(162, 162, 181);
			ColorTable.GSPDarkBackground = Color.FromArgb(162, 162, 181);
			ColorTable.GSPGroupContentDarkBackground = Color.FromArgb(212, 213, 229);
			ColorTable.GSPGroupContentHighlightBackground = Color.FromArgb(227, 227, 236);
			ColorTable.GSPGroupContentText = Color.FromArgb(0, 0, 0);
			ColorTable.GSPGroupContentTextDisabled = Color.FromArgb(150, 145, 133);
			ColorTable.GSPGroupHeaderDarkBackground = Color.FromArgb(169, 168, 191);
			ColorTable.GSPGroupHeaderLightBackground = Color.FromArgb(208, 208, 223);
			ColorTable.GSPGroupHeaderText = Color.FromArgb(92, 91, 121);
			ColorTable.GSPGroupHeaderText = Color.FromArgb(92, 91, 121);
			ColorTable.GSPGroupLine = Color.FromArgb(255, 255, 255);
			ColorTable.GSPGroupLine = Color.FromArgb(255, 255, 255);
			ColorTable.GSPHyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.GSPHighlightBackground = Color.FromArgb(238, 238, 244);
			ColorTable.Hyperlink = Color.FromArgb(0, 61, 178);
			ColorTable.HyperlinkFollowed = Color.FromArgb(170, 0, 170);
			ColorTable.JotNavUIBorder = Color.FromArgb(124, 124, 148);
			ColorTable.JotNavUIBorder = Color.FromArgb(124, 124, 148);
			ColorTable.JotNavUIGradientBegin = Color.FromArgb(215, 215, 229);
			ColorTable.JotNavUIGradientBegin = Color.FromArgb(215, 215, 229);
			ColorTable.JotNavUIGradientEnd = Color.FromArgb(255, 255, 255);
			ColorTable.JotNavUIGradientMiddle = Color.FromArgb(243, 243, 247);
			ColorTable.JotNavUIGradientMiddle = Color.FromArgb(243, 243, 247);
			ColorTable.JotNavUIText = Color.FromArgb(0, 0, 0);
			ColorTable.ListViewColumnHeaderArrowNormal = Color.FromArgb(172, 168, 153);
			ColorTable.NetLookBackground = Color.FromArgb(249, 249, 255);
			ColorTable.OABBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OBBackgroundBorder = Color.FromArgb(211, 211, 211);
			ColorTable.OBBackgroundBorderContrast = Color.FromArgb(128, 128, 128);
			ColorTable.OGMDIParentWorkspaceBackground = Color.FromArgb(155, 154, 179);
			ColorTable.OGRulerActiveBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OGRulerBorder = Color.FromArgb(0, 0, 0);
			ColorTable.OGRulerBackground = Color.FromArgb(223, 223, 234);
			ColorTable.OGRulerInactiveBackground = Color.FromArgb(177, 176, 195);
			ColorTable.OGRulerTabBoxBorder = Color.FromArgb(124, 124, 148);
			ColorTable.OGRulerTabBoxBorderHighlight = Color.FromArgb(255, 255, 255);
			ColorTable.OGRulerTabStopTicks = Color.FromArgb(128, 128, 128);
			ColorTable.OGRulerText = Color.FromArgb(0, 0, 0);
			ColorTable.OGTaskPaneGroupBoxHeaderBackground = Color.FromArgb(212, 212, 226);
			ColorTable.OGWorkspaceBackground = Color.FromArgb(155, 154, 179);
			ColorTable.OutlookFlagNone = Color.FromArgb(239, 239, 244);
			ColorTable.OutlookFolderBarDark = Color.FromArgb(110, 109, 143);
			ColorTable.OutlookFolderBarLight = Color.FromArgb(168, 167, 191);
			ColorTable.OutlookFolderBarText = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookGridlines = Color.FromArgb(234, 233, 225);
			ColorTable.OutlookGroupLine = Color.FromArgb(165, 164, 189);
			ColorTable.OutlookGroupNested = Color.FromArgb(253, 238, 201);
			ColorTable.OutlookGroupShaded = Color.FromArgb(229, 229, 235);
			ColorTable.OutlookGroupText = Color.FromArgb(112, 111, 145);
			ColorTable.OutlookIconBar = Color.FromArgb(253, 247, 233);
			ColorTable.OutlookInfoBarBackground = Color.FromArgb(155, 154, 179);
			ColorTable.OutlookInfoBarText = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookPreviewPaneLabelText = Color.FromArgb(155, 154, 179);
			ColorTable.OutlookTodayIndicatorDark = Color.FromArgb(187, 85, 3);
			ColorTable.OutlookTodayIndicatorLight = Color.FromArgb(251, 200, 79);
			ColorTable.OutlookWBActionDividerLine = Color.FromArgb(204, 206, 219);
			ColorTable.OutlookWBButtonDark = Color.FromArgb(147, 145, 176);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(225, 226, 236);
			ColorTable.OutlookWBButtonLight = Color.FromArgb(225, 226, 236);
			ColorTable.OutlookWBDarkOutline = Color.FromArgb(124, 124, 148);
			ColorTable.OutlookWBFoldersBackground = Color.FromArgb(255, 255, 255);
			ColorTable.OutlookWBHoverButtonDark = Color.FromArgb(247, 190, 87);
			ColorTable.OutlookWBHoverButtonLight = Color.FromArgb(255, 255, 220);
			ColorTable.OutlookWBLabelText = Color.FromArgb(50, 69, 105);
			ColorTable.OutlookWBPressedButtonDark = Color.FromArgb(248, 222, 128);
			ColorTable.OutlookWBPressedButtonLight = Color.FromArgb(232, 127, 8);
			ColorTable.OutlookWBSelectedButtonDark = Color.FromArgb(238, 147, 17);
			ColorTable.OutlookWBSelectedButtonLight = Color.FromArgb(251, 230, 148);
			ColorTable.OutlookWBSplitterDark = Color.FromArgb(110, 109, 143);
			ColorTable.OutlookWBSplitterLight = Color.FromArgb(168, 167, 191);
			ColorTable.PlacesBarBackground = Color.FromArgb(224, 223, 227);
			ColorTable.OutlineThumbnailsPaneTabAreaBackground = Color.FromArgb(243, 243, 247);
			ColorTable.OutlineThumbnailsPaneTabBorder = Color.FromArgb(124, 124, 148);
			ColorTable.OutlineThumbnailsPaneTabInactiveBackground = Color.FromArgb(215, 215, 229);
			ColorTable.OutlineThumbnailsPaneTabText = Color.FromArgb(0, 0, 0);
			ColorTable.PowerPointSlideBorderActiveSelected = Color.FromArgb(142, 142, 170);
			ColorTable.PowerPointSlideBorderActiveSelectedHover = Color.FromArgb(142, 142, 170);
			ColorTable.PowerPointSlideBorderInactiveSelected = Color.FromArgb(128, 128, 128);
			ColorTable.PowerPointSlideBorderHover = Color.FromArgb(142, 142, 170);
			ColorTable.PublisherPrintDocumentScratchPageBackground = Color.FromArgb(155, 154, 179);
			ColorTable.PublisherWebDocumentScratchPageBackground = Color.FromArgb(195, 195, 210);
			ColorTable.ScrollbarBorder = Color.FromArgb(236, 234, 218);
			ColorTable.ScrollbarBackground = Color.FromArgb(247, 247, 249);
			ColorTable.ToastGradientBegin = Color.FromArgb(239, 239, 247);
			ColorTable.ToastGradientEnd = Color.FromArgb(179, 178, 204);
			ColorTable.WordProcessorBorderInnerDocked = Color.FromArgb(255, 255, 255);
			ColorTable.WordProcessorBorderOuterDocked = Color.FromArgb(243, 243, 247);
			ColorTable.WordProcessorBorderOuterFloating = Color.FromArgb(122, 121, 153);
			ColorTable.WordProcessorBackground = Color.FromArgb(238, 238, 244);
			ColorTable.WordProcessorControlBorder = Color.FromArgb(165, 172, 178);
			ColorTable.WordProcessorControlBorderDefault = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlBorderDefault = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlBorderDisabled = Color.FromArgb(128, 128, 128);
			ColorTable.WordProcessorControlBackground = Color.FromArgb(192, 192, 211);
			ColorTable.WordProcessorControlBackgroundDisabled = Color.FromArgb(222, 222, 222);
			ColorTable.WordProcessorControlText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorControlTextDisabled = Color.FromArgb(172, 168, 153);
			ColorTable.WordProcessorControlTextPressed = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorGroupLine = Color.FromArgb(161, 160, 187);
			ColorTable.WordProcessorInfoTipBackground = Color.FromArgb(255, 255, 204);
			ColorTable.WordProcessorInfoTipText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorNavigationBarBackground = Color.FromArgb(122, 121, 153);
			ColorTable.WordProcessorText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorText = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorTextDisabled = Color.FromArgb(172, 168, 153);
			ColorTable.WordProcessorTitleBackgroundActive = Color.FromArgb(184, 188, 234);
			ColorTable.WordProcessorTitleBackgroundInactive = Color.FromArgb(198, 198, 217);
			ColorTable.WordProcessorTitleTextActive = Color.FromArgb(0, 0, 0);
			ColorTable.WordProcessorTitleTextInactive = Color.FromArgb(0, 0, 0);
			ColorTable.FormulaBarBackground = Color.FromArgb(215, 215, 229);
		}
		#endregion
		#region Internal Rendering
		protected virtual void RenderSelectedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!ColorTable.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, ColorTable.CommandBarControlBackgroundHoverGradientBegin, ColorTable.CommandBarControlBackgroundHoverGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonSelectedHighlight = ColorTable.ButtonSelectedHighlight;
			using (Brush brush2 = new SolidBrush(buttonSelectedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}
		protected virtual void RenderPressedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!ColorTable.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, ColorTable.CommandBarControlBackgroundPressedGradientBegin, this.ColorTable.CommandBarControlBackgroundPressedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonPressedHighlight = ColorTable.ButtonPressedHighlight;
			using (Brush brush2 = new SolidBrush(buttonPressedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}
		protected virtual void RenderCheckedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!ColorTable.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, ColorTable.CommandBarControlBackgroundSelectedGradientBegin, this.ColorTable.CommandBarControlBackgroundSelectedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonCheckedHighlight = ColorTable.ButtonCheckedHighlight;
			using (Brush brush2 = new SolidBrush(buttonCheckedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}
		protected virtual void RenderPressedGradient(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			using (Brush brush = new LinearGradientBrush(bounds, ColorTable.CommandBarControlBackgroundPressedGradientBegin, ColorTable.CommandBarControlBackgroundPressedGradientEnd, LinearGradientMode.Vertical))
			{
				g.FillRectangle(brush, bounds);
			}
			using (Pen pen = new Pen(ColorTable.CommandBarMenuBorder))
			{
				g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			}
		}
		protected virtual void RenderItemInternal(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent, bool useHotBorder)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			bool flag = false;
			Rectangle clipRect = item.Selected ? item.ContentRectangle : rectangle;
			if (item.BackgroundImage != null)
			{
				DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, clipRect);
			}
			if (item.Pressed)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
				flag = useHotBorder;
			}
			else
			{
				if (item.Selected)
				{
					RenderSelectedButtonFill(graphics, rectangle);
					flag = useHotBorder;
				}
				else
				{
					if (item.Owner != null && item.BackColor != item.Owner.BackColor)
					{
						using (Brush brush = new SolidBrush(item.BackColor))
						{
							graphics.FillRectangle(brush, rectangle);
						}
					}
				}
			}
			if (flag)
			{
				using (Pen pen = new Pen(ColorTable.CommandBarControlBorderHover))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
			}
		}
		private static new Size FlipSize(Size size)
		{
			int width = size.Width;
			size.Width = size.Height;
			size.Height = width;
			return size;
		}
		private static Rectangle RTLTranslate(Rectangle bounds, Rectangle withinBounds)
		{
			bounds.X = withinBounds.Width - bounds.Right;
			return bounds;
		}
		private static bool IsZeroWidthOrHeight(Rectangle rectangle)
		{
			return rectangle.Width == 0 || rectangle.Height == 0;
		}
		protected void RenderBackgroundGradient(Graphics g, System.Windows.Forms.Control control, Color beginColor, Color endColor, Orientation orientation)
		{
			if (control.RightToLeft == RightToLeft.Yes)
			{
				Color color = beginColor;
				beginColor = endColor;
				endColor = color;
			}
			if (orientation == Orientation.Horizontal)
			{
				System.Windows.Forms.Control parentInternal = control.Parent;
				if (parentInternal != null)
				{
					Rectangle rectangle = new Rectangle(Point.Empty, parentInternal.Size);
					if (IsZeroWidthOrHeight(rectangle))
					{
						return;
					}
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, beginColor, endColor, LinearGradientMode.Horizontal))
					{
						Point location = control.Location;
						linearGradientBrush.TranslateTransform((float)parentInternal.Width - (float)location.X, (float)parentInternal.Height - (float)location.Y);
						g.FillRectangle(linearGradientBrush, new Rectangle(Point.Empty, control.Size));
						return;
					}
				}
				Rectangle rectangle2 = new Rectangle(Point.Empty, control.Size);
				if (IsZeroWidthOrHeight(rectangle2))
				{
					return;
				}
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rectangle2, beginColor, endColor, LinearGradientMode.Horizontal))
				{
					g.FillRectangle(linearGradientBrush2, rectangle2);
					return;
				}
			}
			using (Brush brush = new SolidBrush(beginColor))
			{
				g.FillRectangle(brush, new Rectangle(Point.Empty, control.Size));
			}
		}
		private void RenderOverflowBackground(Graphics graphics, ToolStripItem item, ToolStrip parent, bool rightToLeft)
		{
			ToolStripOverflowButton toolStripOverflowButton = (item as ToolStripOverflowButton);
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			Rectangle withinBounds = rectangle;
			bool flag = mvarRoundedEdges && !(toolStripOverflowButton.GetCurrentParent() is MenuStrip);
			bool flag2 = parent.Orientation == System.Windows.Forms.Orientation.Horizontal;
			if (flag2)
			{
				rectangle.X += rectangle.Width - 12 + 1;
				rectangle.Width = 12;
				if (rightToLeft)
				{
					rectangle = RTLTranslate(rectangle, withinBounds);
				}
			}
			else
			{
				rectangle.Y = rectangle.Height - 12 + 1;
				rectangle.Height = 12;
			}
			Color color;
			Color middleColor;
			Color endColor;
			Color color2;
			Color color3;
			if (toolStripOverflowButton.Pressed)
			{
				color = ColorTable.CommandBarControlBackgroundPressedGradientBegin;
				middleColor = ColorTable.CommandBarGradientPressedMiddle;
				endColor = ColorTable.CommandBarControlBackgroundPressedGradientEnd;
				color2 = ColorTable.CommandBarControlBackgroundPressedGradientBegin;
				color3 = color2;
			}
			else
			{
				if (toolStripOverflowButton.Selected)
				{
					color = this.ColorTable.CommandBarControlBackgroundSelectedGradientBegin;
					middleColor = this.ColorTable.CommandBarGradientSelectedMiddle;
					endColor = this.ColorTable.CommandBarControlBackgroundSelectedGradientEnd;
					color2 = this.ColorTable.CommandBarGradientSelectedMiddle;
					color3 = color2;
				}
				else
				{
					color = this.ColorTable.CommandBarGradientOptionsBegin;
					middleColor = this.ColorTable.CommandBarGradientOptionsMiddle;
					endColor = this.ColorTable.CommandBarGradientOptionsEnd;
					color2 = this.ColorTable.CommandBarShadow;
					color3 = (flag2 ? this.ColorTable.CommandBarGradientVerticalMiddle : this.ColorTable.CommandBarGradientVerticalEnd);
				}
			}
			if (flag)
			{
				using (Pen pen = new Pen(color2))
				{
					Point pt = new Point(rectangle.Left - 1, rectangle.Height - 2);
					Point pt2 = new Point(rectangle.Left, rectangle.Height - 2);
					if (rightToLeft)
					{
						pt.X = rectangle.Right + 1;
						pt2.X = rectangle.Right;
					}
					graphics.DrawLine(pen, pt, pt2);
				}
			}
			LinearGradientMode mode = flag2 ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
			DrawingTools.FillWithDoubleGradient(color, middleColor, endColor, graphics, rectangle, 12, 12, mode, false);
			if (flag)
			{
				using (Brush brush = new SolidBrush(color3))
				{
					if (flag2)
					{
						Point point = new Point(rectangle.X - 2, 0);
						Point point2 = new Point(rectangle.X - 1, 1);
						if (rightToLeft)
						{
							point.X = rectangle.Right + 1;
							point2.X = rectangle.Right;
						}
						graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
						graphics.FillRectangle(brush, point2.X, point2.Y, 1, 1);
					}
					else
					{
						graphics.FillRectangle(brush, rectangle.Width - 3, rectangle.Top - 1, 1, 1);
						graphics.FillRectangle(brush, rectangle.Width - 2, rectangle.Top - 2, 1, 1);
					}
				}
				using (Brush brush2 = new SolidBrush(color))
				{
					if (flag2)
					{
						Rectangle rect = new Rectangle(rectangle.X - 1, 0, 1, 1);
						if (rightToLeft)
						{
							rect.X = rectangle.Right;
						}
						graphics.FillRectangle(brush2, rect);
					}
					else
					{
						graphics.FillRectangle(brush2, rectangle.X, rectangle.Top - 1, 1, 1);
					}
				}
			}
		}
		private void RenderToolStripCurve(Graphics graphics, ToolStrip toolStrip)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, toolStrip.Size);
			Rectangle displayRectangle = toolStrip.DisplayRectangle;
			Point empty = Point.Empty;
			Point location = new Point(rectangle.Width - 1, 0);
			Point point = new Point(0, rectangle.Height - 1);
			using (Brush brush = new SolidBrush(ColorTable.CommandBarGradientVerticalMiddle))
			{
				Rectangle rectangle2 = new Rectangle(empty, onePix);
				rectangle2.X++;
				Rectangle rectangle3 = new Rectangle(empty, onePix);
				rectangle3.Y++;
				Rectangle rectangle4 = new Rectangle(location, onePix);
				rectangle4.X -= 2;
				Rectangle rectangle5 = rectangle4;
				rectangle5.Y++;
				rectangle5.X++;
				Rectangle[] array = new Rectangle[]
				{
					rectangle2, 
					rectangle3, 
					rectangle4, 
					rectangle5
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (displayRectangle.IntersectsWith(array[i]))
					{
						array[i] = Rectangle.Empty;
					}
				}
				graphics.FillRectangles(brush, array);
			}
			using (Brush brush2 = new SolidBrush(ColorTable.CommandBarGradientVerticalEnd))
			{
				Point point2 = point;
				point2.Offset(1, -1);
				if (!displayRectangle.Contains(point2))
				{
					graphics.FillRectangle(brush2, new Rectangle(point2, onePix));
				}
				Rectangle rect = new Rectangle(point.X, point.Y - 2, 1, 1);
				if (!displayRectangle.IntersectsWith(rect))
				{
					graphics.FillRectangle(brush2, rect);
				}
			}
		}
		#endregion

		public override void DrawCommandButtonBackground(System.Drawing.Graphics graphics, System.Windows.Forms.ToolStripButton item, System.Windows.Forms.ToolStrip parent)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			if (item.CheckState == CheckState.Unchecked)
			{
				RenderItemInternal(graphics, item, parent, true);
				return;
			}
			Rectangle clipRect = item.Selected ? item.ContentRectangle : rectangle;
			if (item.BackgroundImage != null)
			{
				DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, clipRect);
			}
			if (ColorTable.UseSystemColors)
			{
				if (item.Selected)
				{
					this.RenderPressedButtonFill(graphics, rectangle);
				}
				else
				{
					this.RenderCheckedButtonFill(graphics, rectangle);
				}
				using (Pen pen = new Pen(ColorTable.CommandBarControlBorderHover))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			if (item.Selected)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
				goto IL_FE;
			}
			this.RenderCheckedButtonFill(graphics, rectangle);
			goto IL_FE;
		IL_FE:
			using (Pen pen2 = new Pen(ColorTable.CommandBarControlBorderHover))
			{
				graphics.DrawRectangle(pen2, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			}
		}
		public override void DrawDropDownButtonBackground(Graphics graphics, ToolStripDropDownButton item, ToolStrip parent)
		{
			if (item.Pressed && item.HasDropDownItems)
			{
				Rectangle bounds = new Rectangle(Point.Empty, item.Size);
				RenderPressedGradient(graphics, bounds);
				return;
			}
			this.RenderItemInternal(graphics, item, parent, true);
		}
		public override void DrawGrip(Graphics graphics, Rectangle gripBounds, Orientation orientation, bool rtl)
		{
			int num = (orientation == Orientation.Horizontal) ? gripBounds.Height : gripBounds.Width;
			int num2 = (orientation == Orientation.Horizontal) ? gripBounds.Width : gripBounds.Height;
			int num3 = (num - 8) / 4;
			if (num3 > 0)
			{
				// int num4 = (toolStrip is MenuStrip) ? 2 : 0;
				int num4 = 0;
				Rectangle[] array = new Rectangle[num3];
				int num5 = 5 + num4;
				int num6 = num2 / 2;
				for (int i = 0; i < num3; i++)
				{
					array[i] = ((orientation == Orientation.Horizontal) ? new Rectangle(num6, num5, 2, 2) : new Rectangle(num5, num6, 2, 2));
					num5 += 4;
				}
				int num7 = rtl ? 1 : -1;
				if (rtl)
				{
					for (int j = 0; j < num3; j++)
					{
						array[j].Offset(-num7, 0);
					}
				}
				using (Brush brush = new SolidBrush(ColorTable.CommandBarDragHandle))
				{
					graphics.FillRectangles(brush, array);
				}
				for (int k = 0; k < num3; k++)
				{
					array[k].Offset(num7, -1);
				}
				using (Brush brush2 = new SolidBrush(ColorTable.CommandBarDragHandleShadow))
				{
					graphics.FillRectangles(brush2, array);
				}
			}
		}
		public override void DrawImageMargin(Graphics graphics, Rectangle affectedBounds, ToolStrip toolStrip)
		{
			affectedBounds.Y += 2;
			affectedBounds.Height -= 4;
			RightToLeft rightToLeft = toolStrip.RightToLeft;
			Color beginColor = (rightToLeft == RightToLeft.No) ? ColorTable.CommandBarGradientVerticalBegin : this.ColorTable.CommandBarGradientVerticalEnd;
			Color endColor = (rightToLeft == RightToLeft.No) ? ColorTable.CommandBarGradientVerticalEnd : this.ColorTable.CommandBarGradientVerticalBegin;
			DrawingTools.FillWithDoubleGradient(beginColor, this.ColorTable.CommandBarGradientVerticalMiddle, endColor, graphics, affectedBounds, 12, 12, LinearGradientMode.Horizontal, toolStrip.RightToLeft == RightToLeft.Yes);
		}
		public override void DrawCheck(Graphics graphics, ToolStripItem item, Rectangle imageRectangle)
		{
			int arg_32_1 = imageRectangle.Left - 2;
			int arg_32_2 = 1;
			Rectangle imageRectangle2 = imageRectangle;
			Rectangle rectangle = new Rectangle(arg_32_1, arg_32_2, imageRectangle2.Width + 4, item.Height - 2);
			if (!ColorTable.UseSystemColors)
			{
				Color color = item.Selected ? this.ColorTable.CommandBarControlBackgroundSelectedHover : ColorTable.CommandBarControlBackgroundSelected;
				color = (item.Pressed ? this.ColorTable.CommandBarControlBackgroundSelectedHover : color);
				using (Brush brush = new SolidBrush(color))
				{
					graphics.FillRectangle(brush, rectangle);
				}
				using (Pen pen = new Pen(ColorTable.CommandBarControlBorderHover))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			if (item.Pressed)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
				goto IL_10D;
			}
			this.RenderSelectedButtonFill(graphics, rectangle);
			goto IL_10D;
		IL_10D:
			graphics.DrawRectangle(SystemPens.Highlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
		}
		public override void DrawImage(Graphics graphics, Rectangle imageRectangle, Image image, ToolStripItem item)
		{
			if (item is ToolStripMenuItem)
			{
				ToolStripMenuItem toolStripMenuItem = item as ToolStripMenuItem;
				if (toolStripMenuItem.CheckState != CheckState.Unchecked)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = toolStripMenuItem.GetCurrentParent() as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null && !toolStripDropDownMenu.ShowCheckMargin && toolStripDropDownMenu.ShowImageMargin)
					{
						DrawCheck(graphics, item, imageRectangle);
					}
				}
			}
			if (imageRectangle != Rectangle.Empty && image != null)
			{
				if (!item.Enabled)
				{
					base.DrawImage(graphics, imageRectangle, image, item);
					return;
				}
				if (item.ImageScaling == ToolStripItemImageScaling.None)
				{
					graphics.DrawImage(image, imageRectangle, new Rectangle(Point.Empty, imageRectangle.Size), GraphicsUnit.Pixel);
					return;
				}
				graphics.DrawImage(image, imageRectangle);
			}
		}
		public override void DrawLabel(Graphics graphics, ToolStripItem item)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			Rectangle clipRect = item.Selected ? item.ContentRectangle : rectangle;

			if (item.BackgroundImage != null)
			{
				DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, clipRect);
			}
		}
		public override void DrawMenuItemBackground(Graphics graphics, ToolStripItem item)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return;
			}
			
			if (item.IsOnDropDown)
			{
				Padding dropDownMenuItemPaintPadding = new Padding(2, 0, 1, 0);
				rectangle = DeflateRect(rectangle, dropDownMenuItemPaintPadding);
				if (item.Selected)
				{
					Color color = this.ColorTable.CommandBarControlBorderSelected;
					if (item.Enabled)
					{
						if (ColorTable.UseSystemColors)
						{
							color = System.Drawing.SystemColors.Highlight;
							this.RenderSelectedButtonFill(graphics, rectangle);
						}
						else
						{
							using (Brush brush = new SolidBrush(ColorTable.CommandBarControlBackgroundHover))
							{
								graphics.FillRectangle(brush, rectangle);
							}
						}
					}
					using (Pen pen = new Pen(color))
					{
						graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
						return;
					}
				}
				Rectangle rectangle2 = rectangle;
				if (item.BackgroundImage != null)
				{
					DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle2);
					return;
				}
				if (item.Owner == null || !(item.BackColor != item.Owner.BackColor))
				{
					return;
				}
				using (Brush brush2 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush2, rectangle2);
					return;
				}
			}
			if (item.Pressed)
			{
				this.RenderPressedGradient(graphics, rectangle);
				return;
			}
			if (item.Selected)
			{
				Color color2 = ColorTable.CommandBarControlBorderSelected;
				if (item.Enabled)
				{
					if (ColorTable.UseSystemColors)
					{
						color2 = System.Drawing.SystemColors.Highlight;
						this.RenderSelectedButtonFill(graphics, rectangle);
					}
					else
					{
						using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.CommandBarControlBackgroundHoverGradientBegin, this.ColorTable.CommandBarControlBackgroundHoverGradientEnd, LinearGradientMode.Vertical))
						{
							graphics.FillRectangle(brush3, rectangle);
						}
					}
				}
				using (Pen pen2 = new Pen(color2))
				{
					graphics.DrawRectangle(pen2, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			Rectangle rectangle3 = rectangle;
			if (item.BackgroundImage != null)
			{
				DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle3);
				return;
			}
			if (item.Owner != null && item.BackColor != item.Owner.BackColor)
			{
				using (Brush brush4 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush4, rectangle3);
				}
			}
		}
		public override void DrawOverflowButton(Graphics graphics, ToolStripItem item, ToolStrip parent)
		{
			base.DrawOverflowButton(graphics, item, parent);
			bool flag = (item.RightToLeft == RightToLeft.Yes);
			RenderOverflowBackground(graphics, item, parent, flag);
			bool flag2 = parent.Orientation == System.Windows.Forms.Orientation.Horizontal;
			Rectangle empty = Rectangle.Empty;
			if (flag)
			{
				empty = new Rectangle(0, item.Height - 8, 9, 5);
			}
			else
			{
				empty = new Rectangle(item.Width - 12, item.Height - 8, 9, 5);
			}
			ArrowDirection direction = flag2 ? ArrowDirection.Down : ArrowDirection.Right;
			int num = (flag && flag2) ? -1 : 1;
			empty.Offset(num, 1);
			RenderArrowInternal(graphics, empty, direction, SystemBrushes.ButtonHighlight);
			empty.Offset(-1 * num, -1);
			RenderArrowInternal(graphics, empty, direction, SystemBrushes.ControlText);
			if (flag2)
			{
				num = (flag ? -2 : 0);
				graphics.DrawLine(SystemPens.ControlText, empty.Right - 6, empty.Y - 2, empty.Right - 2, empty.Y - 2);
				graphics.DrawLine(SystemPens.ButtonHighlight, empty.Right - 5 + num, empty.Y - 1, empty.Right - 1 + num, empty.Y - 1);
				return;
			}
			graphics.DrawLine(SystemPens.ControlText, empty.X, empty.Y, empty.X, empty.Bottom - 1);
			graphics.DrawLine(SystemPens.ButtonHighlight, empty.X + 1, empty.Y + 1, empty.X + 1, empty.Bottom);
		}
		public override void DrawSeparator(Graphics graphics, ToolStripItem item, Rectangle rectangle, bool vertical)
		{
			Color separatorDark = Color.FromKnownColor(KnownColor.ControlDarkDark);
			Color separatorLight = Color.FromKnownColor(KnownColor.ControlLightLight);

			if (item.IsOnDropDown)
			{
				separatorDark = ColorTable.CommandBarMenuSplitterLine;
				separatorLight = ColorTable.CommandBarMenuSplitterLineHighlight;
			}
			else
			{
				separatorDark = ColorTable.CommandBarToolbarSplitterLine;
				separatorLight = ColorTable.CommandBarToolbarSplitterLineHighlight;
			}

			Pen pen = new Pen(separatorDark);
			Pen pen2 = new Pen(separatorLight);
			bool flag = true;
			bool flag2 = true;
			bool flag3 = (item is ToolStripSeparator);
			bool flag4 = false;
			if (flag3)
			{
				if (vertical)
				{
					if (!item.IsOnDropDown)
					{
						rectangle.Y += 3;
						rectangle.Height = Math.Max(0, rectangle.Height - 6);
					}
				}
				else
				{
					ToolStripDropDownMenu toolStripDropDownMenu = item.GetCurrentParent() as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						if (toolStripDropDownMenu.RightToLeft == RightToLeft.No)
						{
							int arg_AE_0 = rectangle.X;
							Padding padding = toolStripDropDownMenu.Padding;
							rectangle.X = arg_AE_0 + (padding.Left - 2);
							rectangle.Width = toolStripDropDownMenu.Width - rectangle.X;
						}
						else
						{
							rectangle.X += 2;
							int arg_FC_0 = toolStripDropDownMenu.Width - rectangle.X;
							Padding padding2 = toolStripDropDownMenu.Padding;
							rectangle.Width = arg_FC_0 - padding2.Right;
						}
					}
					else
					{
						flag4 = true;
					}
				}
			}
			try
			{
				if (vertical)
				{
					if (rectangle.Height >= 4)
					{
						rectangle.Inflate(0, -2);
					}
					bool flag5 = item.RightToLeft == RightToLeft.Yes;
					Pen pen3 = flag5 ? pen2 : pen;
					Pen pen4 = flag5 ? pen : pen2;
					int num = rectangle.Width / 2;
					graphics.DrawLine(pen3, num, rectangle.Top, num, rectangle.Bottom - 1);
					num++;
					graphics.DrawLine(pen4, num, rectangle.Top + 1, num, rectangle.Bottom);
				}
				else
				{
					if (flag4 && rectangle.Width >= 4)
					{
						rectangle.Inflate(-2, 0);
					}
					int num2 = rectangle.Height / 2;
					graphics.DrawLine(pen, rectangle.Left, num2, rectangle.Right - 1, num2);
					if (!flag3 || flag4)
					{
						num2++;
						graphics.DrawLine(pen2, rectangle.Left + 1, num2, rectangle.Right - 1, num2);
					}
				}
			}
			finally
			{
				if (flag && pen != null)
				{
					pen.Dispose();
				}
				if (flag2 && pen2 != null)
				{
					pen2.Dispose();
				}
			}
		}
		public override void DrawSplitButtonBackground(Graphics graphics, ToolStripItem item, ToolStrip parent)
		{
			ToolStripSplitButton toolStripSplitButton = item as ToolStripSplitButton;
			if (toolStripSplitButton != null)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton.Size);
				if (toolStripSplitButton.BackgroundImage != null)
				{
					Rectangle clipRect = toolStripSplitButton.Selected ? toolStripSplitButton.ContentRectangle : rectangle;
					DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, rectangle, clipRect);
				}
				bool flag = toolStripSplitButton.Pressed || toolStripSplitButton.ButtonPressed || toolStripSplitButton.Selected || toolStripSplitButton.ButtonSelected;
				if (flag)
				{
					RenderItemInternal(graphics, item, parent, true);
				}
				if (toolStripSplitButton.ButtonPressed)
				{
					Rectangle rectangle2 = toolStripSplitButton.ButtonBounds;
					Padding padding = (toolStripSplitButton.RightToLeft == RightToLeft.Yes) ? new Padding(0, 1, 1, 1) : new Padding(1, 1, 0, 1);
					rectangle2 = DeflateRect(rectangle2, padding);
					this.RenderPressedButtonFill(graphics, rectangle2);
				}
				else
				{
					if (toolStripSplitButton.Pressed)
					{
						this.RenderPressedGradient(graphics, rectangle);
					}
				}
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (flag && !toolStripSplitButton.Pressed)
				{
					using (Brush brush = new SolidBrush(ColorTable.CommandBarControlBorderHover))
					{
						graphics.FillRectangle(brush, toolStripSplitButton.SplitterBounds);
					}
				}
				DrawArrow(graphics, toolStripSplitButton.Enabled, dropDownButtonBounds, ArrowDirection.Down);
			}
		}
		public override void DrawCommandBarBackground(Graphics graphics, System.Drawing.Rectangle rectangle, Orientation orientation, ToolStrip parent)
		{
			if (parent is ToolStripDropDown)
			{
				Rectangle rect = new Rectangle(Point.Empty, parent.Size);
				using (Brush brush = new SolidBrush(this.ColorTable.CommandBarMenuBackground))
				{
					graphics.FillRectangle(brush, rect);
				}
				return;
			}
			if (parent is MenuStrip)
			{
				RenderBackgroundGradient(graphics, parent, ColorTable.CommandBarPanelGradientBegin, ColorTable.CommandBarPanelGradientEnd, parent.Orientation == System.Windows.Forms.Orientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical);
				return;
			}
			if (parent is StatusStrip)
			{
				RenderBackgroundGradient(graphics, parent, this.ColorTable.CommandBarPanelGradientBegin, this.ColorTable.CommandBarPanelGradientEnd, parent.Orientation == System.Windows.Forms.Orientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical);
				return;
			}

			LinearGradientMode mode = (orientation == Orientation.Horizontal) ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
			DrawingTools.FillWithDoubleGradient(ColorTable.CommandBarGradientVerticalBegin, ColorTable.CommandBarGradientVerticalMiddle, ColorTable.CommandBarGradientVerticalEnd, graphics, rectangle, 12, 12, mode, false);
		}
		public override void DrawCommandBarBorder(Graphics graphics, ToolStrip toolStrip, Rectangle connectedArea)
		{
			Rectangle rectangle;
			if (toolStrip is ToolStripDropDown)
			{
				ToolStripDropDown toolStripDropDown = (toolStrip as ToolStripDropDown);
				if (toolStripDropDown != null)
				{
					rectangle = new Rectangle(Point.Empty, toolStripDropDown.Size);
					using (Pen pen = new Pen(ColorTable.CommandBarMenuBorder))
					{
						graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					}
					if (!(toolStripDropDown is ToolStripOverflow))
					{
						using (Brush brush = new SolidBrush(ColorTable.CommandBarMenuBackground))
						{
							graphics.FillRectangle(brush, connectedArea);
						}
					}
				}
				return;
			}
			if (toolStrip is MenuStrip)
			{
				return;
			}
			if (toolStrip is StatusStrip)
			{
				graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, toolStrip.Width, 0);
				return;
			}

			rectangle = new Rectangle(Point.Empty, toolStrip.Size);
			using (Pen pen = new Pen(ColorTable.CommandBarShadow))
			{
				if (toolStrip.Orientation == System.Windows.Forms.Orientation.Horizontal)
				{
					graphics.DrawLine(pen, rectangle.Left, rectangle.Height - 1, rectangle.Right, rectangle.Height - 1);
					if (this.RoundedEdges)
					{
						graphics.DrawLine(pen, rectangle.Width - 2, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 3);
					}
				}
				else
				{
					graphics.DrawLine(pen, rectangle.Width - 1, 0, rectangle.Width - 1, rectangle.Height - 1);
					if (this.RoundedEdges)
					{
						graphics.DrawLine(pen, rectangle.Width - 2, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 3);
					}
				}
			}
			if (this.RoundedEdges)
			{
				if (toolStrip.OverflowButton.Visible)
				{
					ToolStripItem overflowButton = toolStrip.OverflowButton;
					if (!overflowButton.Visible)
					{
						return;
					}
					Color color;
					Color color2;
					if (overflowButton.Pressed)
					{
						color = this.ColorTable.CommandBarControlBackgroundPressedGradientBegin;
						color2 = color;
					}
					else
					{
						if (overflowButton.Selected)
						{
							color = this.ColorTable.CommandBarControlBackgroundHoverGradientMiddle;
							color2 = color;
						}
						else
						{
							color = this.ColorTable.CommandBarShadow;
							color2 = this.ColorTable.CommandBarGradientVerticalMiddle;
						}
					}
					using (Brush brush = new SolidBrush(color))
					{
						graphics.FillRectangle(brush, toolStrip.Width - 1, toolStrip.Height - 2, 1, 1);
						graphics.FillRectangle(brush, toolStrip.Width - 2, toolStrip.Height - 1, 1, 1);
					}
					using (Brush brush2 = new SolidBrush(color2))
					{
						graphics.FillRectangle(brush2, toolStrip.Width - 2, 0, 1, 1);
						graphics.FillRectangle(brush2, toolStrip.Width - 1, 1, 1, 1);
					}
					return;
				}
				Rectangle empty = Rectangle.Empty;
				if (toolStrip.Orientation == System.Windows.Forms.Orientation.Horizontal)
				{
					empty = new Rectangle(rectangle.Width - 1, 3, 1, rectangle.Height - 3);
				}
				else
				{
					empty = new Rectangle(3, rectangle.Height - 1, rectangle.Width - 3, rectangle.Height - 1);
				}
				DrawingTools.FillWithDoubleGradient(ColorTable.CommandBarGradientOptionsBegin, ColorTable.CommandBarGradientOptionsMiddle, ColorTable.CommandBarGradientOptionsEnd, graphics, empty, 12, 12, LinearGradientMode.Vertical, false);
				RenderToolStripCurve(graphics, toolStrip);
			}
		}
		public override void DrawContentAreaBackground(Graphics graphics, Rectangle rectangle)
		{
			graphics.Clear(ColorTable.CommandBarPanelGradientEnd);
		}
		public override void DrawCommandBarPanelBackground(Graphics graphics, ToolStripPanel toolStripPanel)
		{
			RenderBackgroundGradient(graphics, toolStripPanel, ColorTable.CommandBarPanelGradientBegin, ColorTable.CommandBarPanelGradientEnd, Orientation.Horizontal);
		}
		private void RenderArrowInternal(Graphics graphics, Rectangle rectangle, ArrowDirection direction, Brush brush)
		{
			Point point = new Point(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2);
			point.X += rectangle.Width % 2;
			Point[] points = null;
			switch (direction)
			{
				case ArrowDirection.Left:
				{
					points = new Point[]
					{
						new Point(point.X + 2, point.Y - 3), 
						new Point(point.X + 2, point.Y + 3), 
						new Point(point.X - 1, point.Y)
					};
					break;
				}
				case ArrowDirection.Up:
				{
					points = new Point[]
					{
						new Point(point.X - 2, point.Y + 1), 
						new Point(point.X + 3, point.Y + 1), 
						new Point(point.X, point.Y - 2)
					};
					break;
				}
				default:
				{
					switch (direction)
					{
						case ArrowDirection.Right:
						{
							points = new Point[]
							{
								new Point(point.X - 2, point.Y - 3), 
								new Point(point.X - 2, point.Y + 3), 
								new Point(point.X + 1, point.Y)
							};
							goto IL_243;
						}
					}
					points = new Point[]
					{
						new Point(point.X - 2, point.Y - 1), 
						new Point(point.X + 3, point.Y - 1), 
						new Point(point.X, point.Y + 2)
					};
					break;
				}
			}
		IL_243:
			graphics.FillPolygon(brush, points);
		}

		#region System Controls
		#region Button
		public override void DrawButtonBackground(Graphics g, Rectangle rect, ControlState state)
		{
			
		}
		#endregion
		#region TextBox
		public override void DrawTextBoxBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect, ControlState state)
		{
			DrawingTools.DrawSunkenBorder(g, rect);
		}
		#endregion
		#region ListView
		public override void DrawListItemBackground(Graphics g, Rectangle rect, ControlState state, bool selected, bool focused)
		{
			if (selected)
			{
				
			}
			else if (state == ControlState.Hover)
			{
			}
		}
		public override void DrawListColumnBackground(Graphics g, Rectangle rect, ControlState state, bool sorted)
		{
			SystemTheme sys = new SystemTheme();
			sys.DrawListColumnBackground(g, rect, state, sorted);
		}
		public override void DrawListSelectionRectangle(Graphics g, Rectangle rect)
		{
			Pen pen = new Pen(Color.Black);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			g.DrawRectangle(pen, rect);
		}
		public override void DrawListViewTreeGlyph(Graphics g, Rectangle rect, ControlState state, bool expanded)
		{
			SystemTheme sys = new SystemTheme();
			sys.DrawListViewTreeGlyph(g, rect, state, expanded);
		}
		#endregion
		#region ProgressBar
		public override void DrawProgressBarBackground(Graphics g, Rectangle rect, Orientation orientation)
		{
		}
		public override void DrawProgressBarChunk(Graphics g, Rectangle rect, Orientation orientation)
		{
		}
		public override void DrawProgressBarPulse(Graphics g, Rectangle rect, Orientation orientation)
		{
		}
		#endregion
		#endregion
	}
}
