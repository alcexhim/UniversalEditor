using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming
{
	public class ColorTable
	{
		private bool mvarUseSystemColors = false;
		public bool UseSystemColors
		{
			get
			{
				if (System.Windows.Forms.VisualStyles.VisualStyleInformation.IsEnabledByUser)
				{
					return false;
				}
				return mvarUseSystemColors;
			}
			set
			{
				mvarUseSystemColors = value;
			}
		}

		#region Command Bar
		private Color mvarCommandBarBorderOuterDocked;
		public Color CommandBarBorderOuterDocked
		{
			get { return mvarCommandBarBorderOuterDocked; }
			set { mvarCommandBarBorderOuterDocked = value; }
		}
		private Color mvarCommandBarBorderOuterFloating;
		public Color CommandBarBorderOuterFloating
		{
			get { return mvarCommandBarBorderOuterFloating; }
			set { mvarCommandBarBorderOuterFloating = value; }
		}
		private Color mvarCommandBarBackground;
		public Color CommandBarBackground
		{
			get { return mvarCommandBarBackground; }
			set { mvarCommandBarBackground = value; }
		}
		private Color mvarCommandBarControlBorderPressed;
		public Color CommandBarControlBorderPressed
		{
			get { return mvarCommandBarControlBorderPressed; }
			set { mvarCommandBarControlBorderPressed = value; }
		}
		private Color mvarCommandBarControlBorderHover;
		public Color CommandBarControlBorderHover
		{
			get { return mvarCommandBarControlBorderHover; }
			set { mvarCommandBarControlBorderHover = value; }
		}
		private Color mvarCommandBarControlBorderSelected;
		public Color CommandBarControlBorderSelected
		{
			get { return mvarCommandBarControlBorderSelected; }
			set { mvarCommandBarControlBorderSelected = value; }
		}
		private Color mvarCommandBarControlBorderSelectedHover;
		public Color CommandBarControlBorderSelectedHover
		{
			get { return mvarCommandBarControlBorderSelectedHover; }
			set { mvarCommandBarControlBorderSelectedHover = value; }
		}


		private Color mvarCommandBarMenuControlBorderPressed;
		public Color CommandBarMenuControlBorderPressed
		{
			get { return mvarCommandBarMenuControlBorderPressed; }
			set { mvarCommandBarMenuControlBorderPressed = value; }
		}
		private Color mvarCommandBarMenuControlBackgroundPressed;
		public Color CommandBarMenuControlBackgroundPressed
		{
			get { return mvarCommandBarMenuControlBackgroundPressed; }
			set { mvarCommandBarMenuControlBackgroundPressed = value; }
		}

		private Color mvarCommandBarControlBackground;
		public Color CommandBarControlBackground
		{
			get { return mvarCommandBarControlBackground; }
			set { mvarCommandBarControlBackground = value; }
		}
		private Color mvarCommandBarControlBackgroundHighlight;
		public Color CommandBarControlBackgroundHighlight
		{
			get { return mvarCommandBarControlBackgroundHighlight; }
			set { mvarCommandBarControlBackgroundHighlight = value; }
		}
		private Color mvarCommandBarControlBackgroundPressed;
		public Color CommandBarControlBackgroundPressed
		{
			get { return mvarCommandBarControlBackgroundPressed; }
			set { mvarCommandBarControlBackgroundPressed = value; }
		}
		private Color mvarCommandBarControlBackgroundHover;
		public Color CommandBarControlBackgroundHover
		{
			get { return mvarCommandBarControlBackgroundHover; }
			set { mvarCommandBarControlBackgroundHover = value; }
		}
		private Color mvarCommandBarControlBackgroundSelected;
		public Color CommandBarControlBackgroundSelected
		{
			get { return mvarCommandBarControlBackgroundSelected; }
			set { mvarCommandBarControlBackgroundSelected = value; }
		}
		private Color mvarCommandBarControlBackgroundSelectedHover;
		public Color CommandBarControlBackgroundSelectedHover
		{
			get { return mvarCommandBarControlBackgroundSelectedHover; }
			set { mvarCommandBarControlBackgroundSelectedHover = value; }
		}
		private Color mvarCommandBarControlText;
		public Color CommandBarControlText
		{
			get { return mvarCommandBarControlText; }
			set { mvarCommandBarControlText = value; }
		}
		private Color mvarCommandBarControlTextDisabled;
		public Color CommandBarControlTextDisabled
		{
			get { return mvarCommandBarControlTextDisabled; }
			set { mvarCommandBarControlTextDisabled = value; }
		}
		private Color mvarCommandBarControlTextHighlight;
		public Color CommandBarControlTextHover
		{
			get { return mvarCommandBarControlTextHighlight; }
			set { mvarCommandBarControlTextHighlight = value; }
		}
		private Color mvarCommandBarMenuControlTextPressed;
		public Color CommandBarMenuControlTextPressed
		{
			get { return mvarCommandBarMenuControlTextPressed; }
			set { mvarCommandBarMenuControlTextPressed = value; }
		}
		private Color mvarCommandBarMenuControlTextHighlight;
		public Color CommandBarMenuControlTextHighlight
		{
			get { return mvarCommandBarMenuControlTextHighlight; }
			set { mvarCommandBarMenuControlTextHighlight = value; }
		}
		private Color mvarCommandBarControlTextPressed;
		public Color CommandBarControlTextPressed
		{
			get { return mvarCommandBarControlTextPressed; }
			set { mvarCommandBarControlTextPressed = value; }
		}
		private Color mvarCommandBarDockSeparatorLine;
		public Color CommandBarDockSeparatorLine
		{
			get { return mvarCommandBarDockSeparatorLine; }
			set { mvarCommandBarDockSeparatorLine = value; }
		}
		private Color mvarCommandBarDragHandle;
		public Color CommandBarDragHandle
		{
			get { return mvarCommandBarDragHandle; }
			set { mvarCommandBarDragHandle = value; }
		}
		private Color mvarCommandBarDragHandleShadow;
		public Color CommandBarDragHandleShadow
		{
			get { return mvarCommandBarDragHandleShadow; }
			set { mvarCommandBarDragHandleShadow = value; }
		}
		private Color mvarCommandBarDropDownArrow;
		public Color CommandBarDropDownArrow
		{
			get { return mvarCommandBarDropDownArrow; }
			set { mvarCommandBarDropDownArrow = value; }
		}
		private Color mvarCommandBarGradientMainMenuHorizontalBegin;
		public Color CommandBarPanelGradientBegin
		{
			get { return mvarCommandBarGradientMainMenuHorizontalBegin; }
			set { mvarCommandBarGradientMainMenuHorizontalBegin = value; }
		}
		private Color mvarCommandBarGradientMainMenuHorizontalEnd;
		public Color CommandBarPanelGradientEnd
		{
			get { return mvarCommandBarGradientMainMenuHorizontalEnd; }
			set { mvarCommandBarGradientMainMenuHorizontalEnd = value; }
		}
		private Color mvarCommandBarGradientMenuIconBackgroundDroppedBegin;
		public Color CommandBarGradientMenuIconBackgroundDroppedBegin
		{
			get { return mvarCommandBarGradientMenuIconBackgroundDroppedBegin; }
			set { mvarCommandBarGradientMenuIconBackgroundDroppedBegin = value; }
		}
		private Color mvarCommandBarGradientMenuIconBackgroundDroppedMiddle;
		public Color CommandBarGradientMenuIconBackgroundDroppedMiddle
		{
			get { return mvarCommandBarGradientMenuIconBackgroundDroppedMiddle; }
			set { mvarCommandBarGradientMenuIconBackgroundDroppedMiddle = value; }
		}
		private Color mvarCommandBarGradientMenuIconBackgroundDroppedEnd;
		public Color CommandBarGradientMenuIconBackgroundDroppedEnd
		{
			get { return mvarCommandBarGradientMenuIconBackgroundDroppedEnd; }
			set { mvarCommandBarGradientMenuIconBackgroundDroppedEnd = value; }
		}
		private Color mvarCommandBarGradientOptionsBegin;
		public Color CommandBarGradientOptionsBegin
		{
			get { return mvarCommandBarGradientOptionsBegin; }
			set { mvarCommandBarGradientOptionsBegin = value; }
		}
		private Color mvarCommandBarGradientOptionsMiddle;
		public Color CommandBarGradientOptionsMiddle
		{
			get { return mvarCommandBarGradientOptionsMiddle; }
			set { mvarCommandBarGradientOptionsMiddle = value; }
		}
		private Color mvarCommandBarGradientOptionsEnd;
		public Color CommandBarGradientOptionsEnd
		{
			get { return mvarCommandBarGradientOptionsEnd; }
			set { mvarCommandBarGradientOptionsEnd = value; }
		}
		private Color mvarCommandBarGradientOptionsHoverBegin;
		public Color CommandBarGradientOptionsHoverBegin
		{
			get { return mvarCommandBarGradientOptionsHoverBegin; }
			set { mvarCommandBarGradientOptionsHoverBegin = value; }
		}
		private Color mvarCommandBarGradientOptionsHoverEnd;
		public Color CommandBarGradientOptionsHoverEnd
		{
			get { return mvarCommandBarGradientOptionsHoverEnd; }
			set { mvarCommandBarGradientOptionsHoverEnd = value; }
		}
		private Color mvarCommandBarGradientOptionsHoverMiddle;
		public Color CommandBarGradientOptionsHoverMiddle
		{
			get { return mvarCommandBarGradientOptionsHoverMiddle; }
			set { mvarCommandBarGradientOptionsHoverMiddle = value; }
		}
		private Color mvarCommandBarGradientOptionsSelectedBegin;
		public Color CommandBarGradientOptionsSelectedBegin
		{
			get { return mvarCommandBarGradientOptionsSelectedBegin; }
			set { mvarCommandBarGradientOptionsSelectedBegin = value; }
		}
		private Color mvarCommandBarGradientOptionsSelectedEnd;
		public Color CommandBarGradientOptionsSelectedEnd
		{
			get { return mvarCommandBarGradientOptionsSelectedEnd; }
			set { mvarCommandBarGradientOptionsSelectedEnd = value; }
		}
		private Color mvarCommandBarGradientOptionsSelectedMiddle;
		public Color CommandBarGradientOptionsSelectedMiddle
		{
			get { return mvarCommandBarGradientOptionsSelectedMiddle; }
			set { mvarCommandBarGradientOptionsSelectedMiddle = value; }
		}
		private Color mvarCommandBarGradientSelectedBegin;
		public Color CommandBarControlBackgroundSelectedGradientBegin
		{
			get { return mvarCommandBarGradientSelectedBegin; }
			set { mvarCommandBarGradientSelectedBegin = value; }
		}
		private Color mvarCommandBarGradientSelectedEnd;
		public Color CommandBarControlBackgroundSelectedGradientEnd
		{
			get { return mvarCommandBarGradientSelectedEnd; }
			set { mvarCommandBarGradientSelectedEnd = value; }
		}
		private Color mvarCommandBarGradientSelectedMiddle;
		public Color CommandBarGradientSelectedMiddle
		{
			get { return mvarCommandBarGradientSelectedMiddle; }
			set { mvarCommandBarGradientSelectedMiddle = value; }
		}
		private Color mvarCommandBarGradientVerticalBegin;
		public Color CommandBarGradientVerticalBegin
		{
			get { return mvarCommandBarGradientVerticalBegin; }
			set { mvarCommandBarGradientVerticalBegin = value; }
		}
		private Color mvarCommandBarGradientVerticalEnd;
		public Color CommandBarGradientVerticalEnd
		{
			get { return mvarCommandBarGradientVerticalEnd; }
			set { mvarCommandBarGradientVerticalEnd = value; }
		}
		private Color mvarCommandBarGradientVerticalMiddle;
		public Color CommandBarGradientVerticalMiddle
		{
			get { return mvarCommandBarGradientVerticalMiddle; }
			set { mvarCommandBarGradientVerticalMiddle = value; }
		}
		private Color mvarCommandBarGradientPressedBegin;
		public Color CommandBarControlBackgroundPressedGradientBegin
		{
			get { return mvarCommandBarGradientPressedBegin; }
			set { mvarCommandBarGradientPressedBegin = value; }
		}
		private Color mvarCommandBarGradientPressedEnd;
		public Color CommandBarControlBackgroundPressedGradientEnd
		{
			get { return mvarCommandBarGradientPressedEnd; }
			set { mvarCommandBarGradientPressedEnd = value; }
		}
		private Color mvarCommandBarGradientPressedMiddle;
		public Color CommandBarGradientPressedMiddle
		{
			get { return mvarCommandBarGradientPressedMiddle; }
			set { mvarCommandBarGradientPressedMiddle = value; }
		}
		private Color mvarCommandBarGradientMenuTitleBackgroundBegin;
		public Color CommandBarGradientMenuTitleBackgroundBegin
		{
			get { return mvarCommandBarGradientMenuTitleBackgroundBegin; }
			set { mvarCommandBarGradientMenuTitleBackgroundBegin = value; }
		}
		private Color mvarCommandBarGradientMenuTitleBackgroundEnd;
		public Color CommandBarGradientMenuTitleBackgroundEnd
		{
			get { return mvarCommandBarGradientMenuTitleBackgroundEnd; }
			set { mvarCommandBarGradientMenuTitleBackgroundEnd = value; }
		}
		private Color mvarCommandBarIconDisabledDark;
		public Color CommandBarIconDisabledDark
		{
			get { return mvarCommandBarIconDisabledDark; }
			set { mvarCommandBarIconDisabledDark = value; }
		}
		private Color mvarCommandBarIconDisabledHighlight;
		public Color CommandBarIconDisabledHighlight
		{
			get { return mvarCommandBarIconDisabledHighlight; }
			set { mvarCommandBarIconDisabledHighlight = value; }
		}
		private Color mvarCommandBarLabelBackground;
		public Color CommandBarLabelBackground
		{
			get { return mvarCommandBarLabelBackground; }
			set { mvarCommandBarLabelBackground = value; }
		}
		private Color mvarCommandBarLowColorIconDisabled;
		public Color CommandBarLowColorIconDisabled
		{
			get { return mvarCommandBarLowColorIconDisabled; }
			set { mvarCommandBarLowColorIconDisabled = value; }
		}
		private Color mvarCommandBarMainMenuBackground;
		public Color CommandBarMainMenuBackground
		{
			get { return mvarCommandBarMainMenuBackground; }
			set { mvarCommandBarMainMenuBackground = value; }
		}
		private Color mvarCommandBarMenuBorder;
		public Color CommandBarMenuBorder
		{
			get { return mvarCommandBarMenuBorder; }
			set { mvarCommandBarMenuBorder = value; }
		}
		private Color mvarCommandBarMenuBorderLight;
		public Color CommandBarMenuBorderLight
		{
			get { return mvarCommandBarMenuBorderLight; }
			set { mvarCommandBarMenuBorderLight = value; }
		}
		private Color mvarCommandBarMenuBackground;
		public Color CommandBarMenuBackground
		{
			get { return mvarCommandBarMenuBackground; }
			set { mvarCommandBarMenuBackground = value; }
		}
		private Color mvarCommandBarMenuControlBorderOuter;
		public Color CommandBarMenuControlBorderOuter
		{
			get { return mvarCommandBarMenuControlBorderOuter; }
			set { mvarCommandBarMenuControlBorderOuter = value; }
		}
		private Color mvarCommandBarMenuControlBorderCorner;
		public Color CommandBarMenuControlBorderCorner
		{
			get { return mvarCommandBarMenuControlBorderCorner; }
			set { mvarCommandBarMenuControlBorderCorner = value; }
		}
		private Color mvarCommandBarMenuControlBorderInner;
		public Color CommandBarMenuControlBorderInner
		{
			get { return mvarCommandBarMenuControlBorderInner; }
			set { mvarCommandBarMenuControlBorderInner = value; }
		}
		private Color mvarCommandBarMenuControlText;
		public Color CommandBarMenuControlText
		{
			get { return mvarCommandBarMenuControlText; }
			set { mvarCommandBarMenuControlText = value; }
		}
		private Color mvarCommandBarMenuControlTextDisabled;
		public Color CommandBarMenuControlTextDisabled
		{
			get { return mvarCommandBarMenuControlTextDisabled; }
			set { mvarCommandBarMenuControlTextDisabled = value; }
		}
		private Color mvarCommandBarMenuIconBackground;
		public Color CommandBarMenuIconBackground
		{
			get { return mvarCommandBarMenuIconBackground; }
			set { mvarCommandBarMenuIconBackground = value; }
		}
		private Color mvarCommandBarMenuIconBackgroundDropped;
		public Color CommandBarMenuIconBackgroundDropped
		{
			get { return mvarCommandBarMenuIconBackgroundDropped; }
			set { mvarCommandBarMenuIconBackgroundDropped = value; }
		}
		private Color mvarCommandBarImageMarginBackground;
		public Color CommandBarImageMarginBackground
		{
			get { return mvarCommandBarImageMarginBackground; }
			set { mvarCommandBarImageMarginBackground = value; }
		}
		private Color mvarCommandBarMenuShadow;
		public Color CommandBarMenuShadow
		{
			get { return mvarCommandBarMenuShadow; }
			set { mvarCommandBarMenuShadow = value; }
		}
		private Color mvarCommandBarMenuSplitArrow;
		public Color CommandBarMenuSplitArrow
		{
			get { return mvarCommandBarMenuSplitArrow; }
			set { mvarCommandBarMenuSplitArrow = value; }
		}
		private Color mvarCommandBarOptionsButtonShadow;
		public Color CommandBarOptionsButtonShadow
		{
			get { return mvarCommandBarOptionsButtonShadow; }
			set { mvarCommandBarOptionsButtonShadow = value; }
		}
		private Color mvarCommandBarShadow;
		public Color CommandBarShadow
		{
			get { return mvarCommandBarShadow; }
			set { mvarCommandBarShadow = value; }
		}
		private Color mvarCommandBarMenuSplitterLine;
		public Color CommandBarMenuSplitterLine
		{
			get { return mvarCommandBarMenuSplitterLine; }
			set { mvarCommandBarMenuSplitterLine = value; }
		}
		private Color mvarCommandBarToolbarSplitterLine;
		public Color CommandBarToolbarSplitterLine
		{
			get { return mvarCommandBarToolbarSplitterLine; }
			set { mvarCommandBarToolbarSplitterLine = value; }
		}
		private Color mvarCommandBarMenuSplitterLineHighlight;
		public Color CommandBarMenuSplitterLineHighlight
		{
			get { return mvarCommandBarMenuSplitterLineHighlight; }
			set { mvarCommandBarMenuSplitterLineHighlight = value; }
		}
		private Color mvarCommandBarToolbarSplitterLineHighlight;
		public Color CommandBarToolbarSplitterLineHighlight
		{
			get { return mvarCommandBarToolbarSplitterLineHighlight; }
			set { mvarCommandBarToolbarSplitterLineHighlight = value; }
		}
		private Color mvarContentAreaBackground;
		public Color ContentAreaBackground
		{
			get { return mvarContentAreaBackground; }
			set { mvarContentAreaBackground = value; }
		}
		private Color mvarContentAreaBackgroundDark;
		public Color ContentAreaBackgroundDark
		{
			get { return mvarContentAreaBackgroundDark; }
			set { mvarContentAreaBackgroundDark = value; }
		}
		private Color mvarContentAreaBackgroundLight;
		public Color ContentAreaBackgroundLight
		{
			get { return mvarContentAreaBackgroundLight; }
			set { mvarContentAreaBackgroundLight = value; }
		}

		/*
		public Color CommandBarSplitterLine
		{
			get { return mvarCommandBarMenuSplitterLine; }
			set
			{
				mvarCommandBarMenuSplitterLine = value;
				mvarCommandBarToolbarSplitterLine = value;
			}
		}
		public Color CommandBarSplitterLineHighlight
		{
			get { return mvarCommandBarMenuSplitterLineHighlight; }
			set
			{
				mvarCommandBarMenuSplitterLineHighlight = value;
				mvarCommandBarToolbarSplitterLineHighlight = value;
			}
		}
		*/

		private Color mvarCommandBarTearOffHandle;
		public Color CommandBarTearOffHandle
		{
			get { return mvarCommandBarTearOffHandle; }
			set { mvarCommandBarTearOffHandle = value; }
		}
		private Color mvarCommandBarTearOffHandleHover;
		public Color CommandBarTearOffHandleHover
		{
			get { return mvarCommandBarTearOffHandleHover; }
			set { mvarCommandBarTearOffHandleHover = value; }
		}
		private Color mvarCommandBarTitleBackground;
		public Color CommandBarTitleBackground
		{
			get { return mvarCommandBarTitleBackground; }
			set { mvarCommandBarTitleBackground = value; }
		}
		private Color mvarCommandBarTitleText;
		public Color CommandBarTitleText
		{
			get { return mvarCommandBarTitleText; }
			set { mvarCommandBarTitleText = value; }
		}
		#endregion
		#region Common Colors
		private Color mvarDisabledFocuslessHighlightedText;
		public Color DisabledFocuslessHighlightedText
		{
			get { return mvarDisabledFocuslessHighlightedText; }
			set { mvarDisabledFocuslessHighlightedText = value; }
		}
		private Color mvarDisabledHighlightedText;
		public Color DisabledHighlightedText
		{
			get { return mvarDisabledHighlightedText; }
			set { mvarDisabledHighlightedText = value; }
		}
		private Color mvarDialogGroupBoxText;
		public Color DialogGroupBoxText
		{
			get { return mvarDialogGroupBoxText; }
			set { mvarDialogGroupBoxText = value; }
		}
		#endregion
		#region Document Tabs
		private Color mvarDocumentTabBorder;
		public Color DocumentTabBorder
		{
			get { return mvarDocumentTabBorder; }
			set { mvarDocumentTabBorder = value; }
		}
		private Color mvarDocumentTabBorderDark;
		public Color DocumentTabBorderDark
		{
			get { return mvarDocumentTabBorderDark; }
			set { mvarDocumentTabBorderDark = value; }
		}
		private Color mvarDocumentTabBorderDarkPressed;
		public Color DocumentTabBorderDarkPressed
		{
			get { return mvarDocumentTabBorderDarkPressed; }
			set { mvarDocumentTabBorderDarkPressed = value; }
		}
		private Color mvarDocumentTabBorderDarkHover;
		public Color DocumentTabBorderDarkHover
		{
			get { return mvarDocumentTabBorderDarkHover; }
			set { mvarDocumentTabBorderDarkHover = value; }
		}
		private Color mvarDocumentTabBorderHighlight;
		public Color DocumentTabBorderHighlight
		{
			get { return mvarDocumentTabBorderHighlight; }
			set { mvarDocumentTabBorderHighlight = value; }
		}
		private Color mvarDocumentTabBorderHighlightPressed;
		public Color DocumentTabBorderHighlightPressed
		{
			get { return mvarDocumentTabBorderHighlightPressed; }
			set { mvarDocumentTabBorderHighlightPressed = value; }
		}
		private Color mvarDocumentTabBorderHighlightHover;
		public Color DocumentTabBorderHighlightHover
		{
			get { return mvarDocumentTabBorderHighlightHover; }
			set { mvarDocumentTabBorderHighlightHover = value; }
		}
		private Color mvarDocumentTabBorderPressed;
		public Color DocumentTabBorderPressed
		{
			get { return mvarDocumentTabBorderPressed; }
			set { mvarDocumentTabBorderPressed = value; }
		}
		private Color mvarDocumentTabBorderHover;
		public Color DocumentTabBorderHover
		{
			get { return mvarDocumentTabBorderHover; }
			set { mvarDocumentTabBorderHover = value; }
		}
		private Color mvarDocumentTabBorderSelected;
		public Color DocumentTabBorderSelected
		{
			get { return mvarDocumentTabBorderSelected; }
			set { mvarDocumentTabBorderSelected = value; }
		}
		private Color mvarDocumentTabInactiveBorderSelected;
		public Color DocumentTabInactiveBorderSelected
		{
			get { return mvarDocumentTabInactiveBorderSelected; }
			set { mvarDocumentTabInactiveBorderSelected = value; }
		}
		private Color mvarDocumentTabBackground;
		public Color DocumentTabBackground
		{
			get { return mvarDocumentTabBackground; }
			set { mvarDocumentTabBackground = value; }
		}
		private Color mvarDocumentTabBackgroundPressed;
		public Color DocumentTabBackgroundPressed
		{
			get { return mvarDocumentTabBackgroundPressed; }
			set { mvarDocumentTabBackgroundPressed = value; }
		}
		private Color mvarDocumentTabBackgroundHover;
		public Color DocumentTabBackgroundHover
		{
			get { return mvarDocumentTabBackgroundHover; }
			set { mvarDocumentTabBackgroundHover = value; }
		}
		private Color mvarDocumentTabBackgroundHoverGradientBegin;
		public Color DocumentTabBackgroundHoverGradientBegin
		{
			get { return mvarDocumentTabBackgroundHoverGradientBegin; }
			set { mvarDocumentTabBackgroundHoverGradientBegin = value; }
		}
		private Color mvarDocumentTabBackgroundHoverGradientEnd;
		public Color DocumentTabBackgroundHoverGradientEnd
		{
			get { return mvarDocumentTabBackgroundHoverGradientEnd; }
			set { mvarDocumentTabBackgroundHoverGradientEnd = value; }
		}
		private Color mvarDocumentTabBackgroundSelected;
		public Color DocumentTabBackgroundSelected
		{
			get { return mvarDocumentTabBackgroundSelected; }
			set { mvarDocumentTabBackgroundSelected = value; }
		}
		private Color mvarDocumentTabText;
		public Color DocumentTabText
		{
			get { return mvarDocumentTabText; }
			set { mvarDocumentTabText = value; }
		}
		private Color mvarDocumentTabTextPressed;
		public Color DocumentTabTextPressed
		{
			get { return mvarDocumentTabTextPressed; }
			set { mvarDocumentTabTextPressed = value; }
		}
		private Color mvarDocumentTabTextHover;
		public Color DocumentTabTextHover
		{
			get { return mvarDocumentTabTextHover; }
			set { mvarDocumentTabTextHover = value; }
		}
		private Color mvarDocumentTabTextSelected;
		public Color DocumentTabTextSelected
		{
			get { return mvarDocumentTabTextSelected; }
			set { mvarDocumentTabTextSelected = value; }
		}
		#endregion
		#region Docking Windows
		#region Tabs
		private Color mvarDockingWindowActiveTabBackgroundNormalGradientBegin = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
		public Color DockingWindowActiveTabBackgroundNormalGradientBegin { get { return mvarDockingWindowActiveTabBackgroundNormalGradientBegin; } set { mvarDockingWindowActiveTabBackgroundNormalGradientBegin = value; } }
		private Color mvarDockingWindowActiveTabBackgroundNormalGradientMiddle = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
		public Color DockingWindowActiveTabBackgroundNormalGradientMiddle { get { return mvarDockingWindowActiveTabBackgroundNormalGradientMiddle; } set { mvarDockingWindowActiveTabBackgroundNormalGradientMiddle = value; } }
		private Color mvarDockingWindowActiveTabBackgroundNormalGradientEnd = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
		public Color DockingWindowActiveTabBackgroundNormalGradientEnd { get { return mvarDockingWindowActiveTabBackgroundNormalGradientEnd; } set { mvarDockingWindowActiveTabBackgroundNormalGradientEnd = value; } }

		private Color mvarDockingWindowActiveTabBackgroundHoverGradientBegin = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
		public Color DockingWindowActiveTabBackgroundHoverGradientBegin { get { return mvarDockingWindowActiveTabBackgroundHoverGradientBegin; } set { mvarDockingWindowActiveTabBackgroundHoverGradientBegin = value; } }
		private Color mvarDockingWindowActiveTabBackgroundHoverGradientMiddle = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
		public Color DockingWindowActiveTabBackgroundHoverGradientMiddle { get { return mvarDockingWindowActiveTabBackgroundHoverGradientMiddle; } set { mvarDockingWindowActiveTabBackgroundHoverGradientMiddle = value; } }
		private Color mvarDockingWindowActiveTabBackgroundHoverGradientEnd = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ActiveCaption);
		public Color DockingWindowActiveTabBackgroundHoverGradientEnd { get { return mvarDockingWindowActiveTabBackgroundHoverGradientEnd; } set { mvarDockingWindowActiveTabBackgroundHoverGradientEnd = value; } }

		#region Text
		private Color mvarDockingWindowActiveTabTextNormal = System.Drawing.Color.FromKnownColor(KnownColor.ActiveCaptionText);
		public Color DockingWindowActiveTabTextNormal { get { return mvarDockingWindowActiveTabTextNormal; } set { mvarDockingWindowActiveTabTextNormal = value; } }

		private Color mvarDockingWindowActiveTabTextHover = System.Drawing.Color.FromKnownColor(KnownColor.ActiveCaptionText);
		public Color DockingWindowActiveTabTextHover { get { return mvarDockingWindowActiveTabTextHover; } set { mvarDockingWindowActiveTabTextHover = value; } }

		private Color mvarDockingWindowActiveTabTextDisabled = System.Drawing.Color.FromKnownColor(KnownColor.GrayText);
		public Color DockingWindowActiveTabTextDisabled { get { return mvarDockingWindowActiveTabTextDisabled; } set { mvarDockingWindowActiveTabTextDisabled = value; } }
		#endregion

		private Color mvarDockingWindowInactiveTabBackgroundGradientBegin = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.InactiveCaption);
		public Color DockingWindowInactiveTabBackgroundGradientBegin { get { return mvarDockingWindowInactiveTabBackgroundGradientBegin; } set { mvarDockingWindowInactiveTabBackgroundGradientBegin = value; } }
		private Color mvarDockingWindowInactiveTabBackgroundGradientMiddle = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.InactiveCaption);
		public Color DockingWindowInactiveTabBackgroundGradientMiddle { get { return mvarDockingWindowInactiveTabBackgroundGradientMiddle; } set { mvarDockingWindowInactiveTabBackgroundGradientMiddle = value; } }
		private Color mvarDockingWindowInactiveTabBackgroundGradientEnd = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.InactiveCaption);
		public Color DockingWindowInactiveTabBackgroundGradientEnd { get { return mvarDockingWindowInactiveTabBackgroundGradientEnd; } set { mvarDockingWindowInactiveTabBackgroundGradientEnd = value; } }

		private Color mvarDockingWindowInactiveTabText = System.Drawing.Color.FromKnownColor(KnownColor.InactiveCaptionText);
		public Color DockingWindowInactiveTabText
		{
			get { return mvarDockingWindowInactiveTabText; }
			set { mvarDockingWindowInactiveTabText = value; }
		}
		private Color mvarDockingWindowTabBackgroundPressed;
		public Color DockingWindowTabBackgroundPressed
		{
			get { return mvarDockingWindowTabBackgroundPressed; }
			set { mvarDockingWindowTabBackgroundPressed = value; }
		}
		private Color mvarDockingWindowTabBackgroundHover;
		public Color DockingWindowTabBackgroundHover
		{
			get { return mvarDockingWindowTabBackgroundHover; }
			set { mvarDockingWindowTabBackgroundHover = value; }
		}
		private Color mvarDockingWindowTabTextPressed;
		public Color DockingWindowTabTextPressed
		{
			get { return mvarDockingWindowTabTextPressed; }
			set { mvarDockingWindowTabTextPressed = value; }
		}
		private Color mvarDockingWindowTabTextHover;
		public Color DockingWindowTabTextHover
		{
			get { return mvarDockingWindowTabTextHover; }
			set { mvarDockingWindowTabTextHover = value; }
		}
		#endregion
		#region Titlebars
		private Color mvarDockingWindowActiveTitlebarBackgroundGradientBegin;
		public Color DockingWindowActiveTitlebarBackgroundGradientBegin { get { return mvarDockingWindowActiveTitlebarBackgroundGradientBegin; } set { mvarDockingWindowActiveTitlebarBackgroundGradientBegin = value; } }
		private Color mvarDockingWindowActiveTitlebarBackgroundGradientMiddle;
		public Color DockingWindowActiveTitlebarBackgroundGradientMiddle { get { return mvarDockingWindowActiveTitlebarBackgroundGradientMiddle; } set { mvarDockingWindowActiveTitlebarBackgroundGradientMiddle = value; } }
		private Color mvarDockingWindowActiveTitlebarBackgroundGradientEnd;
		public Color DockingWindowActiveTitlebarBackgroundGradientEnd { get { return mvarDockingWindowActiveTitlebarBackgroundGradientEnd; } set { mvarDockingWindowActiveTitlebarBackgroundGradientEnd = value; } }

		private Color mvarDockingWindowInactiveTitlebarBackgroundGradientBegin;
		public Color DockingWindowInactiveTitlebarBackgroundGradientBegin { get { return mvarDockingWindowInactiveTitlebarBackgroundGradientBegin; } set { mvarDockingWindowInactiveTitlebarBackgroundGradientBegin = value; } }
		private Color mvarDockingWindowInactiveTitlebarBackgroundGradientMiddle;
		public Color DockingWindowInactiveTitlebarBackgroundGradientMiddle { get { return mvarDockingWindowInactiveTitlebarBackgroundGradientMiddle; } set { mvarDockingWindowInactiveTitlebarBackgroundGradientMiddle = value; } }
		private Color mvarDockingWindowInactiveTitlebarBackgroundGradientEnd;
		public Color DockingWindowInactiveTitlebarBackgroundGradientEnd { get { return mvarDockingWindowInactiveTitlebarBackgroundGradientEnd; } set { mvarDockingWindowInactiveTitlebarBackgroundGradientEnd = value; } }
		#endregion
		#endregion
		#region Highlight
		private Color mvarFocuslessHighlightedBackground;
		public Color FocuslessHighlightedBackground { get { return mvarFocuslessHighlightedBackground; } set { mvarFocuslessHighlightedBackground = value; } }
		private Color mvarFocuslessHighlightedForeground;
		public Color FocuslessHighlightedForeground { get { return mvarFocuslessHighlightedForeground; } set { mvarFocuslessHighlightedForeground = value; } }
		private Color mvarFocuslessHighlightedBorder;
		public Color FocuslessHighlightedBorder { get { return mvarFocuslessHighlightedBorder; } set { mvarFocuslessHighlightedBorder = value; } }
		private Color mvarFocusedHighlightedBackground;
		public Color FocusedHighlightedBackground { get { return mvarFocusedHighlightedBackground; } set { mvarFocusedHighlightedBackground = value; } }
		private Color mvarFocusedHighlightedForeground;
		public Color FocusedHighlightedForeground { get { return mvarFocusedHighlightedForeground; } set { mvarFocusedHighlightedForeground = value; } }
		private Color mvarFocusedHighlightedBorder;
		public Color FocusedHighlightedBorder { get { return mvarFocusedHighlightedBorder; } set { mvarFocusedHighlightedBorder = value; } }
		#endregion
		#region Grid
		private Color mvarGridHeaderBorder;
		public Color GridHeaderBorder
		{
			get { return mvarGridHeaderBorder; }
			set { mvarGridHeaderBorder = value; }
		}
		private Color mvarGridHeaderBackground;
		public Color GridHeaderBackground
		{
			get { return mvarGridHeaderBackground; }
			set { mvarGridHeaderBackground = value; }
		}
		private Color mvarGridHeaderCellBorder;
		public Color GridHeaderCellBorder
		{
			get { return mvarGridHeaderCellBorder; }
			set { mvarGridHeaderCellBorder = value; }
		}
		private Color mvarGridHeaderCellBackground;
		public Color GridHeaderCellBackground
		{
			get { return mvarGridHeaderCellBackground; }
			set { mvarGridHeaderCellBackground = value; }
		}
		private Color mvarGridHeaderCellBackgroundSelected;
		public Color GridHeaderCellBackgroundSelected
		{
			get { return mvarGridHeaderCellBackgroundSelected; }
			set { mvarGridHeaderCellBackgroundSelected = value; }
		}
		private Color mvarGridHeaderSeeThroughSelection;
		public Color GridHeaderSeeThroughSelection
		{
			get { return mvarGridHeaderSeeThroughSelection; }
			set { mvarGridHeaderSeeThroughSelection = value; }
		}
		#endregion
		#region GSP
		private Color mvarGSPDarkBackground;
		public Color GSPDarkBackground
		{
			get { return mvarGSPDarkBackground; }
			set { mvarGSPDarkBackground = value; }
		}
		private Color mvarGSPGroupContentDarkBackground;
		public Color GSPGroupContentDarkBackground
		{
			get { return mvarGSPGroupContentDarkBackground; }
			set { mvarGSPGroupContentDarkBackground = value; }
		}
		private Color mvarGSPGroupContentHighlightBackground;
		public Color GSPGroupContentHighlightBackground
		{
			get { return mvarGSPGroupContentHighlightBackground; }
			set { mvarGSPGroupContentHighlightBackground = value; }
		}
		private Color mvarGSPGroupContentText;
		public Color GSPGroupContentText
		{
			get { return mvarGSPGroupContentText; }
			set { mvarGSPGroupContentText = value; }
		}
		private Color mvarGSPGroupContentTextDisabled;
		public Color GSPGroupContentTextDisabled
		{
			get { return mvarGSPGroupContentTextDisabled; }
			set { mvarGSPGroupContentTextDisabled = value; }
		}
		private Color mvarGSPGroupHeaderDarkBackground;
		public Color GSPGroupHeaderDarkBackground
		{
			get { return mvarGSPGroupHeaderDarkBackground; }
			set { mvarGSPGroupHeaderDarkBackground = value; }
		}
		private Color mvarGSPGroupHeaderHighlightBackground;
		public Color GSPGroupHeaderLightBackground
		{
			get { return mvarGSPGroupHeaderHighlightBackground; }
			set { mvarGSPGroupHeaderHighlightBackground = value; }
		}
		private Color mvarGSPGroupHeaderText;
		public Color GSPGroupHeaderText
		{
			get { return mvarGSPGroupHeaderText; }
			set { mvarGSPGroupHeaderText = value; }
		}
		private Color mvarGSPGroupLine;
		public Color GSPGroupLine
		{
			get { return mvarGSPGroupLine; }
			set { mvarGSPGroupLine = value; }
		}
		private Color mvarGSPHyperlink;
		public Color GSPHyperlink
		{
			get { return mvarGSPHyperlink; }
			set { mvarGSPHyperlink = value; }
		}
		private Color mvarGSPHighlightBackground;
		public Color GSPHighlightBackground
		{
			get { return mvarGSPHighlightBackground; }
			set { mvarGSPHighlightBackground = value; }
		}
		#endregion
		#region Common Colors
		private Color mvarHyperlink;
		public Color Hyperlink
		{
			get { return mvarHyperlink; }
			set { mvarHyperlink = value; }
		}
		private Color mvarHyperlinkFollowed;
		public Color HyperlinkFollowed
		{
			get { return mvarHyperlinkFollowed; }
			set { mvarHyperlinkFollowed = value; }
		}
		#endregion
		#region ListView
		#region ColumnHeader
		#region Arrow
		private Color mvarListViewColumnHeaderArrowNormal;
		public Color ListViewColumnHeaderArrowNormal
		{
			get { return mvarListViewColumnHeaderArrowNormal; }
			set { mvarListViewColumnHeaderArrowNormal = value; }
		}
		#endregion
		#region Header
		private Color mvarListViewColumnHeaderBackgroundNormal;
		public Color ListViewColumnHeaderBackgroundNormal
		{
			get { return mvarListViewColumnHeaderBackgroundNormal; }
			set { mvarListViewColumnHeaderBackgroundNormal = value; }
		}
		private Color mvarListViewColumnHeaderBackgroundHover;
		public Color ListViewColumnHeaderBackgroundHover
		{
			get { return mvarListViewColumnHeaderBackgroundHover; }
			set { mvarListViewColumnHeaderBackgroundHover = value; }
		}
		private Color mvarListViewColumnHeaderBackgroundSelected;
		public Color ListViewColumnHeaderBackgroundSelected
		{
			get { return mvarListViewColumnHeaderBackgroundSelected; }
			set { mvarListViewColumnHeaderBackgroundSelected = value; }
		}
		private Color mvarListViewColumnHeaderForegroundNormal;
		public Color ListViewColumnHeaderForegroundNormal
		{
			get { return mvarListViewColumnHeaderForegroundNormal; }
			set { mvarListViewColumnHeaderForegroundNormal = value; }
		}
		private Color mvarListViewColumnHeaderForegroundHover;
		public Color ListViewColumnHeaderForegroundHover
		{
			get { return mvarListViewColumnHeaderForegroundHover; }
			set { mvarListViewColumnHeaderForegroundHover = value; }
		}
		private Color mvarListViewColumnHeaderForegroundSelected;
		public Color ListViewColumnHeaderForegroundSelected
		{
			get { return mvarListViewColumnHeaderForegroundSelected; }
			set { mvarListViewColumnHeaderForegroundSelected = value; }
		}
		private Color mvarListViewColumnHeaderBorder;
		public Color ListViewColumnHeaderBorder
		{
			get { return mvarListViewColumnHeaderBorder; }
			set { mvarListViewColumnHeaderBorder = value; }
		}
		#endregion
		#endregion
		#endregion
		#region JotNavUI
		private Color mvarJotNavUIBorder;
		public Color JotNavUIBorder
		{
			get { return mvarJotNavUIBorder; }
			set { mvarJotNavUIBorder = value; }
		}
		private Color mvarJotNavUIGradientBegin;
		public Color JotNavUIGradientBegin
		{
			get { return mvarJotNavUIGradientBegin; }
			set { mvarJotNavUIGradientBegin = value; }
		}
		private Color mvarJotNavUIGradientEnd;
		public Color JotNavUIGradientEnd
		{
			get { return mvarJotNavUIGradientEnd; }
			set { mvarJotNavUIGradientEnd = value; }
		}
		private Color mvarJotNavUIGradientMiddle;
		public Color JotNavUIGradientMiddle
		{
			get { return mvarJotNavUIGradientMiddle; }
			set { mvarJotNavUIGradientMiddle = value; }
		}
		private Color mvarJotNavUIText;
		public Color JotNavUIText
		{
			get { return mvarJotNavUIText; }
			set { mvarJotNavUIText = value; }
		}
		#endregion
		#region Common Colors
		private Color mvarNetLookBackground;
		public Color NetLookBackground
		{
			get { return mvarNetLookBackground; }
			set { mvarNetLookBackground = value; }
		}
		#endregion
		#region OAB/OB
		private Color mvarOABBackground;
		public Color OABBackground
		{
			get { return mvarOABBackground; }
			set { mvarOABBackground = value; }
		}
		private Color mvarOBBackgroundBorder;
		public Color OBBackgroundBorder
		{
			get { return mvarOBBackgroundBorder; }
			set { mvarOBBackgroundBorder = value; }
		}
		private Color mvarOBBackgroundBorderContrast;
		public Color OBBackgroundBorderContrast
		{
			get { return mvarOBBackgroundBorderContrast; }
			set { mvarOBBackgroundBorderContrast = value; }
		}
		#endregion
		#region OG
		private Color mvarOGMDIParentWorkspaceBackground;
		public Color OGMDIParentWorkspaceBackground
		{
			get { return mvarOGMDIParentWorkspaceBackground; }
			set { mvarOGMDIParentWorkspaceBackground = value; }
		}
		private Color mvarOGRulerActiveBackground;
		public Color OGRulerActiveBackground
		{
			get { return mvarOGRulerActiveBackground; }
			set { mvarOGRulerActiveBackground = value; }
		}
		private Color mvarOGRulerBorder;
		public Color OGRulerBorder
		{
			get { return mvarOGRulerBorder; }
			set { mvarOGRulerBorder = value; }
		}
		private Color mvarOGRulerBackground;
		public Color OGRulerBackground
		{
			get { return mvarOGRulerBackground; }
			set { mvarOGRulerBackground = value; }
		}
		private Color mvarOGRulerInactiveBackground;
		public Color OGRulerInactiveBackground
		{
			get { return mvarOGRulerInactiveBackground; }
			set { mvarOGRulerInactiveBackground = value; }
		}
		private Color mvarOGRulerTabBoxBorder;
		public Color OGRulerTabBoxBorder
		{
			get { return mvarOGRulerTabBoxBorder; }
			set { mvarOGRulerTabBoxBorder = value; }
		}
		private Color mvarOGRulerTabBoxBorderHighlight;
		public Color OGRulerTabBoxBorderHighlight
		{
			get { return mvarOGRulerTabBoxBorderHighlight; }
			set { mvarOGRulerTabBoxBorderHighlight = value; }
		}
		private Color mvarOGRulerTabStopTicks;
		public Color OGRulerTabStopTicks
		{
			get { return mvarOGRulerTabStopTicks; }
			set { mvarOGRulerTabStopTicks = value; }
		}
		private Color mvarOGRulerText;
		public Color OGRulerText
		{
			get { return mvarOGRulerText; }
			set { mvarOGRulerText = value; }
		}
		private Color mvarOGRulerTaskPane;
		public Color OGRulerTaskPane
		{
			get { return mvarOGRulerTaskPane; }
			set { mvarOGRulerTaskPane = value; }
		}
		private Color mvarOGTaskPaneGroupBoxHeaderBackground;
		public Color OGTaskPaneGroupBoxHeaderBackground
		{
			get { return mvarOGTaskPaneGroupBoxHeaderBackground; }
			set { mvarOGTaskPaneGroupBoxHeaderBackground = value; }
		}
		private Color mvarOGWorkspaceBackground;
		public Color OGWorkspaceBackground
		{
			get { return mvarOGWorkspaceBackground; }
			set { mvarOGWorkspaceBackground = value; }
		}
		private Color mvarOutlookFlagNone;
		public Color OutlookFlagNone
		{
			get { return mvarOutlookFlagNone; }
			set { mvarOutlookFlagNone = value; }
		}
		private Color mvarOutlookFolderBarDark;
		public Color OutlookFolderBarDark
		{
			get { return mvarOutlookFolderBarDark; }
			set { mvarOutlookFolderBarDark = value; }
		}
		private Color mvarOutlookFolderBarLight;
		public Color OutlookFolderBarLight
		{
			get { return mvarOutlookFolderBarLight; }
			set { mvarOutlookFolderBarLight = value; }
		}
		private Color mvarOutlookFolderBarText;
		public Color OutlookFolderBarText
		{
			get { return mvarOutlookFolderBarText; }
			set { mvarOutlookFolderBarText = value; }
		}
		private Color mvarOutlookGridlines;
		public Color OutlookGridlines
		{
			get { return mvarOutlookGridlines; }
			set { mvarOutlookGridlines = value; }
		}
		private Color mvarOutlookGroupLine;
		public Color OutlookGroupLine
		{
			get { return mvarOutlookGroupLine; }
			set { mvarOutlookGroupLine = value; }
		}
		private Color mvarOutlookGroupNested;
		public Color OutlookGroupNested
		{
			get { return mvarOutlookGroupNested; }
			set { mvarOutlookGroupNested = value; }
		}
		private Color mvarOutlookGroupShaded;
		public Color OutlookGroupShaded
		{
			get { return mvarOutlookGroupShaded; }
			set { mvarOutlookGroupShaded = value; }
		}
		private Color mvarOutlookGroupText;
		public Color OutlookGroupText
		{
			get { return mvarOutlookGroupText; }
			set { mvarOutlookGroupText = value; }
		}
		private Color mvarOutlookIconBar;
		public Color OutlookIconBar
		{
			get { return mvarOutlookIconBar; }
			set { mvarOutlookIconBar = value; }
		}
		private Color mvarOutlookInfoBarBackground;
		public Color OutlookInfoBarBackground
		{
			get { return mvarOutlookInfoBarBackground; }
			set { mvarOutlookInfoBarBackground = value; }
		}
		private Color mvarOutlookInfoBarText;
		public Color OutlookInfoBarText
		{
			get { return mvarOutlookInfoBarText; }
			set { mvarOutlookInfoBarText = value; }
		}
		private Color mvarOutlookPreviewPaneLabelText;
		public Color OutlookPreviewPaneLabelText
		{
			get { return mvarOutlookPreviewPaneLabelText; }
			set { mvarOutlookPreviewPaneLabelText = value; }
		}
		private Color mvarOutlookTodayIndicatorDark;
		public Color OutlookTodayIndicatorDark
		{
			get { return mvarOutlookTodayIndicatorDark; }
			set { mvarOutlookTodayIndicatorDark = value; }
		}
		private Color mvarOutlookTodayIndicatorLight;
		public Color OutlookTodayIndicatorLight
		{
			get { return mvarOutlookTodayIndicatorLight; }
			set { mvarOutlookTodayIndicatorLight = value; }
		}
		private Color mvarOutlookWBActionDividerLine;
		public Color OutlookWBActionDividerLine
		{
			get { return mvarOutlookWBActionDividerLine; }
			set { mvarOutlookWBActionDividerLine = value; }
		}
		private Color mvarOutlookWBButtonDark;
		public Color OutlookWBButtonDark
		{
			get { return mvarOutlookWBButtonDark; }
			set { mvarOutlookWBButtonDark = value; }
		}
		private Color mvarOutlookWBButtonLight;
		public Color OutlookWBButtonLight
		{
			get { return mvarOutlookWBButtonLight; }
			set { mvarOutlookWBButtonLight = value; }
		}
		private Color mvarOutlookWBDarkOutline;
		public Color OutlookWBDarkOutline
		{
			get { return mvarOutlookWBDarkOutline; }
			set { mvarOutlookWBDarkOutline = value; }
		}
		private Color mvarOutlookWBFoldersBackground;
		public Color OutlookWBFoldersBackground
		{
			get { return mvarOutlookWBFoldersBackground; }
			set { mvarOutlookWBFoldersBackground = value; }
		}
		private Color mvarOutlookWBHoverButtonDark;
		public Color OutlookWBHoverButtonDark
		{
			get { return mvarOutlookWBHoverButtonDark; }
			set { mvarOutlookWBHoverButtonDark = value; }
		}
		private Color mvarOutlookWBHoverButtonLight;
		public Color OutlookWBHoverButtonLight
		{
			get { return mvarOutlookWBHoverButtonLight; }
			set { mvarOutlookWBHoverButtonLight = value; }
		}
		private Color mvarOutlookWBLabelText;
		public Color OutlookWBLabelText
		{
			get { return mvarOutlookWBLabelText; }
			set { mvarOutlookWBLabelText = value; }
		}
		private Color mvarOutlookWBPressedButtonDark;
		public Color OutlookWBPressedButtonDark
		{
			get { return mvarOutlookWBPressedButtonDark; }
			set { mvarOutlookWBPressedButtonDark = value; }
		}
		private Color mvarOutlookWBPressedButtonLight;
		public Color OutlookWBPressedButtonLight
		{
			get { return mvarOutlookWBPressedButtonLight; }
			set { mvarOutlookWBPressedButtonLight = value; }
		}
		private Color mvarOutlookWBSelectedButtonDark;
		public Color OutlookWBSelectedButtonDark
		{
			get { return mvarOutlookWBSelectedButtonDark; }
			set { mvarOutlookWBSelectedButtonDark = value; }
		}
		private Color mvarOutlookWBSelectedButtonLight;
		public Color OutlookWBSelectedButtonLight
		{
			get { return mvarOutlookWBSelectedButtonLight; }
			set { mvarOutlookWBSelectedButtonLight = value; }
		}
		private Color mvarOutlookWBSplitterDark;
		public Color OutlookWBSplitterDark
		{
			get { return mvarOutlookWBSplitterDark; }
			set { mvarOutlookWBSplitterDark = value; }
		}
		private Color mvarOutlookWBSplitterLight;
		public Color OutlookWBSplitterLight
		{
			get { return mvarOutlookWBSplitterLight; }
			set { mvarOutlookWBSplitterLight = value; }
		}
		private Color mvarPlacesBarBackground;
		public Color PlacesBarBackground
		{
			get { return mvarPlacesBarBackground; }
			set { mvarPlacesBarBackground = value; }
		}
		private Color mvarOutlineThumbnailsPaneTabAreaBackground;
		public Color OutlineThumbnailsPaneTabAreaBackground
		{
			get { return mvarOutlineThumbnailsPaneTabAreaBackground; }
			set { mvarOutlineThumbnailsPaneTabAreaBackground = value; }
		}
		private Color mvarOutlineThumbnailsPaneTabBorder;
		public Color OutlineThumbnailsPaneTabBorder
		{
			get { return mvarOutlineThumbnailsPaneTabBorder; }
			set { mvarOutlineThumbnailsPaneTabBorder = value; }
		}
		private Color mvarOutlineThumbnailsPaneTabInactiveBackground;
		public Color OutlineThumbnailsPaneTabInactiveBackground
		{
			get { return mvarOutlineThumbnailsPaneTabInactiveBackground; }
			set { mvarOutlineThumbnailsPaneTabInactiveBackground = value; }
		}
		private Color mvarOutlineThumbnailsPaneTabText;
		public Color OutlineThumbnailsPaneTabText
		{
			get { return mvarOutlineThumbnailsPaneTabText; }
			set { mvarOutlineThumbnailsPaneTabText = value; }
		}
		private Color mvarPowerPointSlideBorderActiveSelected;
		public Color PowerPointSlideBorderActiveSelected
		{
			get { return mvarPowerPointSlideBorderActiveSelected; }
			set { mvarPowerPointSlideBorderActiveSelected = value; }
		}
		private Color mvarPowerPointSlideBorderActiveSelectedHover;
		public Color PowerPointSlideBorderActiveSelectedHover
		{
			get { return mvarPowerPointSlideBorderActiveSelectedHover; }
			set { mvarPowerPointSlideBorderActiveSelectedHover = value; }
		}
		private Color mvarPowerPointSlideBorderInactiveSelected;
		public Color PowerPointSlideBorderInactiveSelected
		{
			get { return mvarPowerPointSlideBorderInactiveSelected; }
			set { mvarPowerPointSlideBorderInactiveSelected = value; }
		}
		private Color mvarPowerPointSlideBorderHover;
		public Color PowerPointSlideBorderHover
		{
			get { return mvarPowerPointSlideBorderHover; }
			set { mvarPowerPointSlideBorderHover = value; }
		}
		private Color mvarPublisherPrintDocumentScratchPageBackground;
		public Color PublisherPrintDocumentScratchPageBackground
		{
			get { return mvarPublisherPrintDocumentScratchPageBackground; }
			set { mvarPublisherPrintDocumentScratchPageBackground = value; }
		}
		private Color mvarPublisherWebDocumentScratchPageBackground;
		public Color PublisherWebDocumentScratchPageBackground
		{
			get { return mvarPublisherWebDocumentScratchPageBackground; }
			set { mvarPublisherWebDocumentScratchPageBackground = value; }
		}
		private Color mvarScrollbarBorder;
		public Color ScrollbarBorder
		{
			get { return mvarScrollbarBorder; }
			set { mvarScrollbarBorder = value; }
		}
		private Color mvarScrollbarBackground;
		public Color ScrollbarBackground
		{
			get { return mvarScrollbarBackground; }
			set { mvarScrollbarBackground = value; }
		}
		private Color mvarToastGradientBegin;
		public Color ToastGradientBegin
		{
			get { return mvarToastGradientBegin; }
			set { mvarToastGradientBegin = value; }
		}
		private Color mvarToastGradientEnd;
		public Color ToastGradientEnd
		{
			get { return mvarToastGradientEnd; }
			set { mvarToastGradientEnd = value; }
		}
		private Color mvarWordProcessorBorderInnerDocked;
		public Color WordProcessorBorderInnerDocked
		{
			get { return mvarWordProcessorBorderInnerDocked; }
			set { mvarWordProcessorBorderInnerDocked = value; }
		}
		private Color mvarWordProcessorBorderOuterDocked;
		public Color WordProcessorBorderOuterDocked
		{
			get { return mvarWordProcessorBorderOuterDocked; }
			set { mvarWordProcessorBorderOuterDocked = value; }
		}
		private Color mvarWordProcessorBorderOuterFloating;
		public Color WordProcessorBorderOuterFloating
		{
			get { return mvarWordProcessorBorderOuterFloating; }
			set { mvarWordProcessorBorderOuterFloating = value; }
		}
		private Color mvarWordProcessorBackground;
		public Color WordProcessorBackground
		{
			get { return mvarWordProcessorBackground; }
			set { mvarWordProcessorBackground = value; }
		}
		private Color mvarWordProcessorControlBorder;
		public Color WordProcessorControlBorder
		{
			get { return mvarWordProcessorControlBorder; }
			set { mvarWordProcessorControlBorder = value; }
		}
		private Color mvarWordProcessorControlBorderDefault;
		public Color WordProcessorControlBorderDefault
		{
			get { return mvarWordProcessorControlBorderDefault; }
			set { mvarWordProcessorControlBorderDefault = value; }
		}
		private Color mvarWordProcessorControlBorderDisabled;
		public Color WordProcessorControlBorderDisabled
		{
			get { return mvarWordProcessorControlBorderDisabled; }
			set { mvarWordProcessorControlBorderDisabled = value; }
		}
		private Color mvarWordProcessorControlBackground;
		public Color WordProcessorControlBackground
		{
			get { return mvarWordProcessorControlBackground; }
			set { mvarWordProcessorControlBackground = value; }
		}
		private Color mvarWordProcessorControlBackgroundDisabled;
		public Color WordProcessorControlBackgroundDisabled
		{
			get { return mvarWordProcessorControlBackgroundDisabled; }
			set { mvarWordProcessorControlBackgroundDisabled = value; }
		}
		private Color mvarWordProcessorControlText;
		public Color WordProcessorControlText
		{
			get { return mvarWordProcessorControlText; }
			set { mvarWordProcessorControlText = value; }
		}
		private Color mvarWordProcessorControlTextDisabled;
		public Color WordProcessorControlTextDisabled
		{
			get { return mvarWordProcessorControlTextDisabled; }
			set { mvarWordProcessorControlTextDisabled = value; }
		}
		private Color mvarWordProcessorControlTextPressed;
		public Color WordProcessorControlTextPressed
		{
			get { return mvarWordProcessorControlTextPressed; }
			set { mvarWordProcessorControlTextPressed = value; }
		}
		private Color mvarWordProcessorGroupLine;
		public Color WordProcessorGroupLine
		{
			get { return mvarWordProcessorGroupLine; }
			set { mvarWordProcessorGroupLine = value; }
		}
		private Color mvarWordProcessorInfoTipBackground;
		public Color WordProcessorInfoTipBackground
		{
			get { return mvarWordProcessorInfoTipBackground; }
			set { mvarWordProcessorInfoTipBackground = value; }
		}
		private Color mvarWordProcessorInfoTipText;
		public Color WordProcessorInfoTipText
		{
			get { return mvarWordProcessorInfoTipText; }
			set { mvarWordProcessorInfoTipText = value; }
		}
		private Color mvarWordProcessorNavigationBarBackground;
		public Color WordProcessorNavigationBarBackground
		{
			get { return mvarWordProcessorNavigationBarBackground; }
			set { mvarWordProcessorNavigationBarBackground = value; }
		}
		private Color mvarWordProcessorText;
		public Color WordProcessorText
		{
			get { return mvarWordProcessorText; }
			set { mvarWordProcessorText = value; }
		}
		private Color mvarWordProcessorTextDisabled;
		public Color WordProcessorTextDisabled
		{
			get { return mvarWordProcessorTextDisabled; }
			set { mvarWordProcessorTextDisabled = value; }
		}
		private Color mvarWordProcessorTitleBackgroundActive;
		public Color WordProcessorTitleBackgroundActive
		{
			get { return mvarWordProcessorTitleBackgroundActive; }
			set { mvarWordProcessorTitleBackgroundActive = value; }
		}
		private Color mvarWordProcessorTitleBackgroundInactive;
		public Color WordProcessorTitleBackgroundInactive
		{
			get { return mvarWordProcessorTitleBackgroundInactive; }
			set { mvarWordProcessorTitleBackgroundInactive = value; }
		}
		private Color mvarWordProcessorTitleTextActive;
		public Color WordProcessorTitleTextActive
		{
			get { return mvarWordProcessorTitleTextActive; }
			set { mvarWordProcessorTitleTextActive = value; }
		}
		private Color mvarWordProcessorTitleTextInactive;
		public Color WordProcessorTitleTextInactive
		{
			get { return mvarWordProcessorTitleTextInactive; }
			set { mvarWordProcessorTitleTextInactive = value; }
		}
		private Color mvarFormulaBarBackground;
		public Color FormulaBarBackground
		{
			get { return mvarFormulaBarBackground; }
			set { mvarFormulaBarBackground = value; }
		}
		#endregion
		#region Ribbon
		private Color mvarRibbonTabBarBackground;
		public Color RibbonTabBarBackground
		{
			get { return mvarRibbonTabBarBackground; }
			set { mvarRibbonTabBarBackground = value; }
		}
		private Color mvarRibbonTabBarBorderTop;
		public Color RibbonTabBarBorderTop
		{
			get { return mvarRibbonTabBarBorderTop; }
			set { mvarRibbonTabBarBorderTop = value; }
		}
		private Color mvarRibbonTabBarBorderBottom;
		public Color RibbonTabBarBorderBottom
		{
			get { return mvarRibbonTabBarBorderBottom; }
			set { mvarRibbonTabBarBorderBottom = value; }
		}
		private Color mvarRibbonApplicationButtonBorderTop;
		public Color RibbonApplicationButtonBorderTop
		{
			get { return mvarRibbonApplicationButtonBorderTop; }
			set { mvarRibbonApplicationButtonBorderTop = value; }
		}
		private Color mvarRibbonApplicationButtonBorder;
		public Color RibbonApplicationButtonBorder
		{
			get { return mvarRibbonApplicationButtonBorder; }
			set { mvarRibbonApplicationButtonBorder = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderTopBegin;
		public Color RibbonApplicationButtonGradientBorderTopBegin
		{
			get { return mvarRibbonApplicationButtonGradientBorderTopBegin; }
			set { mvarRibbonApplicationButtonGradientBorderTopBegin = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderTopEnd;
		public Color RibbonApplicationButtonGradientBorderTopEnd
		{
			get { return mvarRibbonApplicationButtonGradientBorderTopEnd; }
			set { mvarRibbonApplicationButtonGradientBorderTopEnd = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderBottomBegin;
		public Color RibbonApplicationButtonGradientBorderBottomBegin
		{
			get { return mvarRibbonApplicationButtonGradientBorderBottomBegin; }
			set { mvarRibbonApplicationButtonGradientBorderBottomBegin = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderBottomEnd;
		public Color RibbonApplicationButtonGradientBorderBottomEnd
		{
			get { return mvarRibbonApplicationButtonGradientBorderBottomEnd; }
			set { mvarRibbonApplicationButtonGradientBorderBottomEnd = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundTopBegin;
		public Color RibbonApplicationButtonGradientBackgroundTopBegin
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundTopBegin; }
			set { mvarRibbonApplicationButtonGradientBackgroundTopBegin = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundTopEnd;
		public Color RibbonApplicationButtonGradientBackgroundTopEnd
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundTopEnd; }
			set { mvarRibbonApplicationButtonGradientBackgroundTopEnd = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundBottomBegin;
		public Color RibbonApplicationButtonGradientBackgroundBottomBegin
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundBottomBegin; }
			set { mvarRibbonApplicationButtonGradientBackgroundBottomBegin = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundBottomEnd;
		public Color RibbonApplicationButtonGradientBackgroundBottomEnd
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundBottomEnd; }
			set { mvarRibbonApplicationButtonGradientBackgroundBottomEnd = value; }
		}
		#region Application Button - Hover State
		private Color mvarRibbonApplicationButtonBorderTopHover;
		public Color RibbonApplicationButtonBorderTopHover
		{
			get { return mvarRibbonApplicationButtonBorderTopHover; }
			set { mvarRibbonApplicationButtonBorderTopHover = value; }
		}
		private Color mvarRibbonApplicationButtonBorderHover;
		public Color RibbonApplicationButtonBorderHover
		{
			get { return mvarRibbonApplicationButtonBorderHover; }
			set { mvarRibbonApplicationButtonBorderHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderTopBeginHover;
		public Color RibbonApplicationButtonGradientBorderTopBeginHover
		{
			get { return mvarRibbonApplicationButtonGradientBorderTopBeginHover; }
			set { mvarRibbonApplicationButtonGradientBorderTopBeginHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderTopEndHover;
		public Color RibbonApplicationButtonGradientBorderTopEndHover
		{
			get { return mvarRibbonApplicationButtonGradientBorderTopEndHover; }
			set { mvarRibbonApplicationButtonGradientBorderTopEndHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderBottomBeginHover;
		public Color RibbonApplicationButtonGradientBorderBottomBeginHover
		{
			get { return mvarRibbonApplicationButtonGradientBorderBottomBeginHover; }
			set { mvarRibbonApplicationButtonGradientBorderBottomBeginHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBorderBottomEndHover;
		public Color RibbonApplicationButtonGradientBorderBottomEndHover
		{
			get { return mvarRibbonApplicationButtonGradientBorderBottomEndHover; }
			set { mvarRibbonApplicationButtonGradientBorderBottomEndHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundTopBeginHover;
		public Color RibbonApplicationButtonGradientBackgroundTopBeginHover
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundTopBeginHover; }
			set { mvarRibbonApplicationButtonGradientBackgroundTopBeginHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundTopEndHover;
		public Color RibbonApplicationButtonGradientBackgroundTopEndHover
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundTopEndHover; }
			set { mvarRibbonApplicationButtonGradientBackgroundTopEndHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundBottomBeginHover;
		public Color RibbonApplicationButtonGradientBackgroundBottomBeginHover
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundBottomBeginHover; }
			set { mvarRibbonApplicationButtonGradientBackgroundBottomBeginHover = value; }
		}
		private Color mvarRibbonApplicationButtonGradientBackgroundBottomEndHover;
		public Color RibbonApplicationButtonGradientBackgroundBottomEndHover
		{
			get { return mvarRibbonApplicationButtonGradientBackgroundBottomEndHover; }
			set { mvarRibbonApplicationButtonGradientBackgroundBottomEndHover = value; }
		}
		#endregion


		private Color mvarRibbonTabText;
		public Color RibbonTabText
		{
			get { return mvarRibbonTabText; }
			set { mvarRibbonTabText = value; }
		}
		#endregion
		#region Button
		private Color mvarButtonPressedHighlight;
		public Color ButtonPressedHighlight
		{
			get { return mvarButtonPressedHighlight; }
			set { mvarButtonPressedHighlight = value; }
		}
		private Color mvarButtonCheckedHighlight;
		public Color ButtonCheckedHighlight
		{
			get { return mvarButtonCheckedHighlight; }
			set { mvarButtonCheckedHighlight = value; }
		}
		private Color mvarButtonSelectedHighlight;
		public Color ButtonSelectedHighlight
		{
			get { return mvarButtonSelectedHighlight; }
			set { mvarButtonSelectedHighlight = value; }
		}
		#endregion

		public static Color GetAlphaBlendedColor(Color src, Color dest, int alpha)
		{
			int red = ((int)src.R * alpha + (255 - alpha) * (int)dest.R) / 255;
			int green = ((int)src.G * alpha + (255 - alpha) * (int)dest.G) / 255;
			int blue = ((int)src.B * alpha + (255 - alpha) * (int)dest.B) / 255;
			int alpha2 = ((int)src.A * alpha + (255 - alpha) * (int)dest.A) / 255;
			return Color.FromArgb(alpha2, red, green, blue);
		}
		public static Color GetAlphaBlendedColorHighRes(Color src, Color dest, int alpha)
		{
			int num;
			int num2;
			if (alpha < 100)
			{
				num = 100 - alpha;
				num2 = 100;
			}
			else
			{
				num = 1000 - alpha;
				num2 = 1000;
			}
			int red = (alpha * (int)src.R + num * (int)dest.R + num2 / 2) / num2;
			int green = (alpha * (int)src.G + num * (int)dest.G + num2 / 2) / num2;
			int blue = (alpha * (int)src.B + num * (int)dest.B + num2 / 2) / num2;
			return Color.FromArgb(red, green, blue);
		}

		// System.Windows.Forms.SafeNativeMethods
		[System.Runtime.InteropServices.DllImport("uxtheme.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);
		/// <summary>
		/// Gets the color scheme of the current visual style.
		/// </summary>
		/// <returns>A string that specifies the color scheme of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		public static string VisualStyleThemeFileName
		{
			get
			{
				if (System.Windows.Forms.VisualStyles.VisualStyleInformation.IsEnabledByUser)
				{
					switch (Environment.OSVersion.Platform)
					{
						case PlatformID.Win32NT:
						case PlatformID.Win32S:
						case PlatformID.Win32Windows:
						case PlatformID.WinCE:
							StringBuilder stringBuilder = new StringBuilder(512);
							GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, null, 0, null, 0);
							return stringBuilder.ToString();
					}
				}
				return string.Empty;
			}
		}

		private Color mvarCommandBarGradientMenuBarBackgroundBegin;
		public Color CommandBarGradientMenuBarBackgroundBegin
		{
			get { return mvarCommandBarGradientMenuBarBackgroundBegin; }
			set { mvarCommandBarGradientMenuBarBackgroundBegin = value; }
		}
		private Color mvarCommandBarGradientMenuBarBackgroundEnd;
		public Color CommandBarGradientMenuBarBackgroundEnd
		{
			get { return mvarCommandBarGradientMenuBarBackgroundEnd; }
			set { mvarCommandBarGradientMenuBarBackgroundEnd = value; }
		}

		private Color mvarCommandBarControlBackgroundHoverGradientBegin;
		public Color CommandBarControlBackgroundHoverGradientBegin
		{
			get { return mvarCommandBarControlBackgroundHoverGradientBegin; }
			set { mvarCommandBarControlBackgroundHoverGradientBegin = value; }
		}
		private Color mvarCommandBarControlBackgroundHoverGradientMiddle;
		public Color CommandBarControlBackgroundHoverGradientMiddle
		{
			get { return mvarCommandBarControlBackgroundHoverGradientMiddle; }
			set { mvarCommandBarControlBackgroundHoverGradientMiddle = value; }
		}
		private Color mvarCommandBarControlBackgroundHoverGradientEnd;
		public Color CommandBarControlBackgroundHoverGradientEnd
		{
			get { return mvarCommandBarControlBackgroundHoverGradientEnd; }
			set { mvarCommandBarControlBackgroundHoverGradientEnd = value; }
		}

		private Color mvarRibbonTabGradientBorderHoverStart;
		public Color RibbonTabGradientBorderHoverStart
		{
			get { return mvarRibbonTabGradientBorderHoverStart; }
			set { mvarRibbonTabGradientBorderHoverStart = value; }
		}

		private Color mvarRibbonTabGradientBorderHoverEnd;
		public Color RibbonTabGradientBorderHoverEnd
		{
			get { return mvarRibbonTabGradientBorderHoverEnd; }
			set { mvarRibbonTabGradientBorderHoverEnd = value; }
		}

		private Color mvarRibbonTabGradientBackgroundHoverStart;
		public Color RibbonTabGradientBackgroundHoverStart
		{
			get { return mvarRibbonTabGradientBackgroundHoverStart; }
			set { mvarRibbonTabGradientBackgroundHoverStart = value; }
		}

		private Color mvarRibbonTabGradientBackgroundHoverEnd;
		public Color RibbonTabGradientBackgroundHoverEnd
		{
			get { return mvarRibbonTabGradientBackgroundHoverEnd; }
			set { mvarRibbonTabGradientBackgroundHoverEnd = value; }
		}

		private Color mvarRibbonTabGradientBorderPressedStart;
		public Color RibbonTabGradientBorderPressedStart
		{
			get { return mvarRibbonTabGradientBorderPressedStart; }
			set { mvarRibbonTabGradientBorderPressedStart = value; }
		}

		private Color mvarRibbonTabGradientBorderPressedEnd;
		public Color RibbonTabGradientBorderPressedEnd
		{
			get { return mvarRibbonTabGradientBorderPressedEnd; }
			set { mvarRibbonTabGradientBorderPressedEnd = value; }
		}

		private Color mvarRibbonTabGradientBackgroundPressedStart;
		public Color RibbonTabGradientBackgroundPressedStart
		{
			get { return mvarRibbonTabGradientBackgroundPressedStart; }
			set { mvarRibbonTabGradientBackgroundPressedStart = value; }
		}

		private Color mvarRibbonTabGradientBackgroundPressedEnd;
		public Color RibbonTabGradientBackgroundPressedEnd
		{
			get { return mvarRibbonTabGradientBackgroundPressedEnd; }
			set { mvarRibbonTabGradientBackgroundPressedEnd = value; }
		}

		private Color mvarRibbonTabPageBackgroundGradientStart;
		public Color RibbonTabPageBackgroundGradientStart
		{
			get { return mvarRibbonTabPageBackgroundGradientStart; }
			set { mvarRibbonTabPageBackgroundGradientStart = value; }
		}

		private Color mvarRibbonTabPageBackgroundGradientMiddle;
		public Color RibbonTabPageBackgroundGradientMiddle
		{
			get { return mvarRibbonTabPageBackgroundGradientMiddle; }
			set { mvarRibbonTabPageBackgroundGradientMiddle = value; }
		}

		private Color mvarRibbonTabPageBackgroundGradientEnd;
		public Color RibbonTabPageBackgroundGradientEnd
		{
			get { return mvarRibbonTabPageBackgroundGradientEnd; }
			set { mvarRibbonTabPageBackgroundGradientEnd = value; }
		}

		private Color mvarRibbonControlGroupText;
		public Color RibbonControlGroupText
		{
			get { return mvarRibbonControlGroupText; }
			set { mvarRibbonControlGroupText = value; }
		}

		private Color mvarRibbonControlGroupActionButton;
		public Color RibbonControlGroupActionButton
		{
			get { return mvarRibbonControlGroupActionButton; }
			set { mvarRibbonControlGroupActionButton = value; }
		}

		private Color mvarRibbonControlGroupBorderGradientBegin;
		public Color RibbonControlGroupBorderGradientBegin
		{
			get { return mvarRibbonControlGroupBorderGradientBegin; }
			set { mvarRibbonControlGroupBorderGradientBegin = value; }
		}
		private Color mvarRibbonControlGroupBorderGradientEnd;
		public Color RibbonControlGroupBorderGradientEnd
		{
			get { return mvarRibbonControlGroupBorderGradientEnd; }
			set { mvarRibbonControlGroupBorderGradientEnd = value; }
		}

		private Color mvarRibbonControlText;
		public Color RibbonControlText
		{
			get { return mvarRibbonControlText; }
			set { mvarRibbonControlText = value; }
		}
		private Color mvarRibbonControlDisabledText;
		public Color RibbonControlDisabledText
		{
			get { return mvarRibbonControlDisabledText; }
			set { mvarRibbonControlDisabledText = value; }
		}

		private Color mvarRibbonControlBorderHover;
		public Color RibbonControlBorderHover
		{
			get { return mvarRibbonControlBorderHover; }
			set { mvarRibbonControlBorderHover = value; }
		}
		private Color mvarRibbonControlBorderPressed;
		public Color RibbonControlBorderPressed
		{
			get { return mvarRibbonControlBorderPressed; }
			set { mvarRibbonControlBorderPressed = value; }
		}
		private Color mvarRibbonControlBorderDisabled;
		public Color RibbonControlBorderDisabled
		{
			get { return mvarRibbonControlBorderDisabled; }
			set { mvarRibbonControlBorderDisabled = value; }
		}
		
		private Color mvarRibbonControlBackgroundHoverBegin;
		public Color RibbonControlBackgroundHoverBegin
		{
			get { return mvarRibbonControlBackgroundHoverBegin; }
			set { mvarRibbonControlBackgroundHoverBegin = value; }
		}
		private Color mvarRibbonControlBackgroundHoverMiddleTop;
		public Color RibbonControlBackgroundHoverMiddleTop
		{
			get { return mvarRibbonControlBackgroundHoverMiddleTop; }
			set { mvarRibbonControlBackgroundHoverMiddleTop = value; }
		}
		private Color mvarRibbonControlBackgroundHoverMiddleBottom;
		public Color RibbonControlBackgroundHoverMiddleBottom
		{
			get { return mvarRibbonControlBackgroundHoverMiddleBottom; }
			set { mvarRibbonControlBackgroundHoverMiddleBottom = value; }
		}
		private Color mvarRibbonControlBackgroundHoverEnd;
		public Color RibbonControlBackgroundHoverEnd
		{
			get { return mvarRibbonControlBackgroundHoverEnd; }
			set { mvarRibbonControlBackgroundHoverEnd = value; }
		}

		private Color mvarRibbonControlBackgroundDisabledBegin;
		public Color RibbonControlBackgroundDisabledBegin
		{
			get { return mvarRibbonControlBackgroundDisabledBegin; }
			set { mvarRibbonControlBackgroundDisabledBegin = value; }
		}
		private Color mvarRibbonControlBackgroundDisabledMiddle;
		public Color RibbonControlBackgroundDisabledMiddle
		{
			get { return mvarRibbonControlBackgroundDisabledMiddle; }
			set { mvarRibbonControlBackgroundDisabledMiddle = value; }
		}
		private Color mvarRibbonControlBackgroundDisabledEnd;
		public Color RibbonControlBackgroundDisabledEnd
		{
			get { return mvarRibbonControlBackgroundDisabledEnd; }
			set { mvarRibbonControlBackgroundDisabledEnd = value; }
		}

		private Color mvarRibbonControlBackgroundPressedBegin;
		public Color RibbonControlBackgroundPressedBegin
		{
			get { return mvarRibbonControlBackgroundPressedBegin; }
			set { mvarRibbonControlBackgroundPressedBegin = value; }
		}
		private Color mvarRibbonControlBackgroundPressedMiddle;
		public Color RibbonControlBackgroundPressedMiddle
		{
			get { return mvarRibbonControlBackgroundPressedMiddle; }
			set { mvarRibbonControlBackgroundPressedMiddle = value; }
		}
		private Color mvarRibbonControlBackgroundPressedEnd;
		public Color RibbonControlBackgroundPressedEnd
		{
			get { return mvarRibbonControlBackgroundPressedEnd; }
			set { mvarRibbonControlBackgroundPressedEnd = value; }
		}

		private Color mvarRibbonControlSplitButtonTopBackgroundHoverGradientBegin;
		public Color RibbonControlSplitButtonTopBackgroundHoverGradientBegin
		{
			get { return mvarRibbonControlSplitButtonTopBackgroundHoverGradientBegin; }
			set { mvarRibbonControlSplitButtonTopBackgroundHoverGradientBegin = value; }
		}
		private Color mvarRibbonControlSplitButtonTopBackgroundHoverGradientMiddle;
		public Color RibbonControlSplitButtonTopBackgroundHoverGradientMiddle
		{
			get { return mvarRibbonControlSplitButtonTopBackgroundHoverGradientMiddle; }
			set { mvarRibbonControlSplitButtonTopBackgroundHoverGradientMiddle = value; }
		}
		private Color mvarRibbonControlSplitButtonTopBackgroundHoverGradientEnd;
		public Color RibbonControlSplitButtonTopBackgroundHoverGradientEnd
		{
			get { return mvarRibbonControlSplitButtonTopBackgroundHoverGradientEnd; }
			set { mvarRibbonControlSplitButtonTopBackgroundHoverGradientEnd = value; }
		}
		private Color mvarRibbonControlSplitButtonBottomBackgroundHoverGradientBegin;
		public Color RibbonControlSplitButtonBottomBackgroundHoverGradientBegin
		{
			get { return mvarRibbonControlSplitButtonBottomBackgroundHoverGradientBegin; }
			set { mvarRibbonControlSplitButtonBottomBackgroundHoverGradientBegin = value; }
		}
		private Color mvarRibbonControlSplitButtonBottomBackgroundHoverGradientEnd;
		public Color RibbonControlSplitButtonBottomBackgroundHoverGradientEnd
		{
			get { return mvarRibbonControlSplitButtonBottomBackgroundHoverGradientEnd; }
			set { mvarRibbonControlSplitButtonBottomBackgroundHoverGradientEnd = value; }
		}

		private Color mvarStatusBarBackground = Color.Transparent;
		public Color StatusBarBackground { get { return mvarStatusBarBackground; } set { mvarStatusBarBackground = value; } }

		private Color mvarStatusBarText = Color.FromKnownColor(KnownColor.ControlText);
		public Color StatusBarText { get { return mvarStatusBarText; } set { mvarStatusBarText = value; } }

		private Color mvarCommandBarGradientMenuBackgroundBegin = System.Drawing.Color.FromKnownColor(KnownColor.Control);
		public Color CommandBarGradientMenuBackgroundBegin { get { return mvarCommandBarGradientMenuBackgroundBegin; } set { mvarCommandBarGradientMenuBackgroundBegin = value; } }
		private Color mvarCommandBarGradientMenuBackgroundEnd = System.Drawing.Color.FromKnownColor(KnownColor.Control);
		public Color CommandBarGradientMenuBackgroundEnd { get { return mvarCommandBarGradientMenuBackgroundEnd; } set { mvarCommandBarGradientMenuBackgroundEnd = value; } }

		private Color mvarDocumentTabBackgroundSelectedGradientBegin;
		public Color DocumentTabBackgroundSelectedGradientBegin { get { return mvarDocumentTabBackgroundSelectedGradientBegin; } set { mvarDocumentTabBackgroundSelectedGradientBegin = value; } }
		private Color mvarDocumentTabBackgroundSelectedGradientMiddle;
		public Color DocumentTabBackgroundSelectedGradientMiddle { get { return mvarDocumentTabBackgroundSelectedGradientMiddle; } set { mvarDocumentTabBackgroundSelectedGradientMiddle = value; } }
		private Color mvarDocumentTabBackgroundSelectedGradientEnd;
		public Color DocumentTabBackgroundSelectedGradientEnd { get { return mvarDocumentTabBackgroundSelectedGradientEnd; } set { mvarDocumentTabBackgroundSelectedGradientEnd = value; } }
		private Color mvarDocumentTabInactiveBackgroundSelectedGradientBegin;
		public Color DocumentTabInactiveBackgroundSelectedGradientBegin { get { return mvarDocumentTabInactiveBackgroundSelectedGradientBegin; } set { mvarDocumentTabInactiveBackgroundSelectedGradientBegin = value; } }
		private Color mvarDocumentTabInactiveBackgroundSelectedGradientMiddle;
		public Color DocumentTabInactiveBackgroundSelectedGradientMiddle { get { return mvarDocumentTabInactiveBackgroundSelectedGradientMiddle; } set { mvarDocumentTabInactiveBackgroundSelectedGradientMiddle = value; } }
		private Color mvarDocumentTabInactiveBackgroundSelectedGradientEnd;
		public Color DocumentTabInactiveBackgroundSelectedGradientEnd { get { return mvarDocumentTabInactiveBackgroundSelectedGradientEnd; } set { mvarDocumentTabInactiveBackgroundSelectedGradientEnd = value; } }

		private Color mvarTextBoxBorderNormal;
		public Color TextBoxBorderNormal { get { return mvarTextBoxBorderNormal; } set { mvarTextBoxBorderNormal = value; } }
		private Color mvarTextBoxBorderHover;
		public Color TextBoxBorderHover { get { return mvarTextBoxBorderHover; } set { mvarTextBoxBorderHover = value; } }

		private Color mvarWindowBackground = Color.FromKnownColor(KnownColor.Window);
		public Color WindowBackground { get { return mvarWindowBackground; } set { mvarWindowBackground = value; } }
		private Color mvarWindowForeground = Color.FromKnownColor(KnownColor.WindowText);
		public Color WindowForeground { get { return mvarWindowForeground; } set { mvarWindowForeground = value; } }

		#region ListView
		private Color mvarListViewItemSelectedBackgroundGradientBegin;
		public Color ListViewItemHoverBackgroundGradientBegin { get { return mvarListViewItemSelectedBackgroundGradientBegin; } set { mvarListViewItemSelectedBackgroundGradientBegin = value; } }
		private Color mvarListViewItemSelectedBackgroundGradientMiddle;
		public Color ListViewItemHoverBackgroundGradientMiddle { get { return mvarListViewItemSelectedBackgroundGradientMiddle; } set { mvarListViewItemSelectedBackgroundGradientMiddle = value; } }
		private Color mvarListViewItemSelectedBackgroundGradientEnd;
		public Color ListViewItemHoverBackgroundGradientEnd { get { return mvarListViewItemSelectedBackgroundGradientEnd; } set { mvarListViewItemSelectedBackgroundGradientEnd = value; } }

		private Color mvarListViewItemSelectedBackground;
		public Color ListViewItemSelectedBackground { get { return mvarListViewItemSelectedBackground; } set { mvarListViewItemSelectedBackground = value; } }
		private Color mvarListViewItemSelectedForeground;
		public Color ListViewItemSelectedForeground { get { return mvarListViewItemSelectedForeground; } set { mvarListViewItemSelectedForeground = value; } }

		private Color mvarListViewItemSelectedBorder;
		public Color ListViewItemHoverBorder { get { return mvarListViewItemSelectedBorder; } set { mvarListViewItemSelectedBorder = value; } }

		private Color mvarListViewRangeSelectionForeground;
		public Color ListViewRangeSelectionBorder { get { return mvarListViewRangeSelectionForeground; } set { mvarListViewRangeSelectionForeground = value; } }
		private Color mvarListViewRangeSelectionBackground;
		public Color ListViewRangeSelectionBackground { get { return mvarListViewRangeSelectionBackground; } set { mvarListViewRangeSelectionBackground = value; } }
		#endregion

		private Color mvarDialogBackground;
		public Color DialogBackground { get { return mvarDialogBackground; } set { mvarDialogBackground = value; } }

		private Color mvarWindowTitlebarForeground;
		public Color WindowTitlebarForeground { get { return mvarWindowTitlebarForeground; } set { mvarWindowTitlebarForeground = value; } }

		private Color mvarDocumentSwitcherBorder;
		public Color DocumentSwitcherBorder { get { return mvarDocumentSwitcherBorder; } set { mvarDocumentSwitcherBorder = value; } }
		private Color mvarDocumentSwitcherBackground;
		public Color DocumentSwitcherBackground { get { return mvarDocumentSwitcherBackground; } set { mvarDocumentSwitcherBackground = value; } }
		private Color mvarDocumentSwitcherText;
		public Color DocumentSwitcherText { get { return mvarDocumentSwitcherText; } set { mvarDocumentSwitcherText = value; } }

		private Color mvarDocumentSwitcherSelectionBorder;
		public Color DocumentSwitcherSelectionBorder { get { return mvarDocumentSwitcherSelectionBorder; } set { mvarDocumentSwitcherSelectionBorder = value; } }
		private Color mvarDocumentSwitcherSelectionBackground;
		public Color DocumentSwitcherSelectionBackground { get { return mvarDocumentSwitcherSelectionBackground; } set { mvarDocumentSwitcherSelectionBackground = value; } }
		private Color mvarDocumentSwitcherSelectionText;
		public Color DocumentSwitcherSelectionText { get { return mvarDocumentSwitcherSelectionText; } set { mvarDocumentSwitcherSelectionText = value; } }

		private Color mvarPropertyGridBackgroundColor;
		public Color PropertyGridBackgroundColor { get { return mvarPropertyGridBackgroundColor; } set { mvarPropertyGridBackgroundColor = value; } }
		private Color mvarPropertyGridForegroundColor;
		public Color PropertyGridForegroundColor { get { return mvarPropertyGridForegroundColor; } set { mvarPropertyGridForegroundColor = value; } }
		private Color mvarPropertyGridBorderColor;
		public Color PropertyGridBorderColor { get { return mvarPropertyGridBorderColor; } set { mvarPropertyGridBorderColor = value; } }
		private Color mvarPropertyGridDisabledForegroundColor;
		public Color PropertyGridDisabledForegroundColor { get { return mvarPropertyGridDisabledForegroundColor; } set { mvarPropertyGridDisabledForegroundColor = value; } }
		private Color mvarPropertyGridItemHighlightBackgroundColor;
		public Color PropertyGridItemHighlightBackgroundColor { get { return mvarPropertyGridItemHighlightBackgroundColor; } set { mvarPropertyGridItemHighlightBackgroundColor = value; } }
		private Color mvarPropertyGridItemHighlightForegroundColor;
		public Color PropertyGridItemHighlightForegroundColor { get { return mvarPropertyGridItemHighlightForegroundColor; } set { mvarPropertyGridItemHighlightForegroundColor = value; } }

		private Color mvarDropDownBackgroundColorNormal;
		public Color DropDownBackgroundColorNormal { get { return mvarDropDownBackgroundColorNormal; } set { mvarDropDownBackgroundColorNormal = value; } }
		private Color mvarDropDownForegroundColorNormal;
		public Color DropDownForegroundColorNormal { get { return mvarDropDownForegroundColorNormal; } set { mvarDropDownForegroundColorNormal = value; } }
		private Color mvarDropDownBorderColorNormal;
		public Color DropDownBorderColorNormal { get { return mvarDropDownBorderColorNormal; } set { mvarDropDownBorderColorNormal = value; } }
		private Color mvarDropDownBackgroundColorHover;
		public Color DropDownBackgroundColorHover { get { return mvarDropDownBackgroundColorHover; } set { mvarDropDownBackgroundColorHover = value; } }
		private Color mvarDropDownForegroundColorHover;
		public Color DropDownForegroundColorHover { get { return mvarDropDownForegroundColorHover; } set { mvarDropDownForegroundColorHover = value; } }
		private Color mvarDropDownBorderColorHover;
		public Color DropDownBorderColorHover { get { return mvarDropDownBorderColorHover; } set { mvarDropDownBorderColorHover = value; } }
		private Color mvarDropDownBackgroundColorPressed;
		public Color DropDownBackgroundColorPressed { get { return mvarDropDownBackgroundColorPressed; } set { mvarDropDownBackgroundColorPressed = value; } }
		private Color mvarDropDownForegroundColorPressed;
		public Color DropDownForegroundColorPressed { get { return mvarDropDownForegroundColorPressed; } set { mvarDropDownForegroundColorPressed = value; } }
		private Color mvarDropDownBorderColorPressed;
		public Color DropDownBorderColorPressed { get { return mvarDropDownBorderColorPressed; } set { mvarDropDownBorderColorPressed = value; } }
		private Color mvarDropDownMenuBackground;
		public Color DropDownMenuBackground { get { return mvarDropDownMenuBackground; } set { mvarDropDownMenuBackground = value; } }
		private Color mvarDropDownMenuBorder;
		public Color DropDownMenuBorder { get { return mvarDropDownMenuBorder; } set { mvarDropDownMenuBorder = value; } }
		private Color mvarDropDownButtonBackgroundNormal;
		public Color DropDownButtonBackgroundNormal { get { return mvarDropDownButtonBackgroundNormal; } set { mvarDropDownButtonBackgroundNormal = value; } }
		private Color mvarDropDownButtonBackgroundHover;
		public Color DropDownButtonBackgroundHover { get { return mvarDropDownButtonBackgroundHover; } set { mvarDropDownButtonBackgroundHover = value; } }
		private Color mvarDropDownButtonBackgroundPressed;
		public Color DropDownButtonBackgroundPressed { get { return mvarDropDownButtonBackgroundPressed; } set { mvarDropDownButtonBackgroundPressed = value; } }

		#region ToolTip
		private Color mvarToolTipBackgroundGradientBegin;
		public Color ToolTipBackgroundGradientBegin { get { return mvarToolTipBackgroundGradientBegin; } set { mvarToolTipBackgroundGradientBegin = value; } }
		private Color mvarToolTipBackgroundGradientMiddle;
		public Color ToolTipBackgroundGradientMiddle { get { return mvarToolTipBackgroundGradientMiddle; } set { mvarToolTipBackgroundGradientMiddle = value; } }
		private Color mvarToolTipBackgroundGradientEnd;
		public Color ToolTipBackgroundGradientEnd { get { return mvarToolTipBackgroundGradientEnd; } set { mvarToolTipBackgroundGradientEnd = value; } }

		private Color mvarToolTipBorder;
		public Color ToolTipBorder { get { return mvarToolTipBorder; } set { mvarToolTipBorder = value; } }
		#endregion
	}
}
