using System;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK
{
	internal static class Constants
	{
		public enum GtkWindowType
		{
			TopLevel = 0,
			Popup = 1
		}
		public enum GtkOrientation
		{
			Horizontal = 0,
			Vertical = 1
		}

		/// <summary>
		/// Prebuilt sets of buttons for the dialog. If none of these choices are appropriate, simply use %GTK_BUTTONS_NONE
		/// then call gtk_dialog_add_buttons().
		/// </summary>
		/// <remarks>
		/// Please note that %GTK_BUTTONS_OK, %GTK_BUTTONS_YES_NO and %GTK_BUTTONS_OK_CANCEL are discouraged by the
		/// <ulink url="http://library.gnome.org/devel/hig-book/stable/">GNOME HIG</ulink>.
		/// </remarks>
		public enum GtkButtonsType
		{
			/// <summary>
			/// no buttons at all
			/// </summary>
			None,
			/// <summary>
			/// an OK button
			/// </summary>
			OK,
			/// <summary>
			/// a Close button
			/// </summary>
			Close,
			/// <summary>
			/// a Cancel button
			/// </summary>
			Cancel,
			/// <summary>
			/// Yes and No buttons
			/// </summary>
			YesNo,
			/// <summary>
			/// OK and Cancel buttons
			/// </summary>
			OKCancel
		}

		/// <summary>
		/// Flags used to influence dialog construction.
		/// </summary>
		[Flags()]
		public enum GtkDialogFlags
		{
			/// <summary>
			/// No flags
			/// </summary>
			None = 0,
			/// <summary>
			/// Make the constructed dialog modal, see <see cref="Methods.gtk_window_set_modal" />
			/// </summary>
			Modal = 1,
			/// <summary>
			/// Destroy the dialog when its parent is destroyed, see <see cref="Methods.gtk_window_set_destroy_with_parent" />
			/// </summary>
			DestroyWithParent = 2,
			/// <summary>
			/// Create dialog with actions in header bar instead of action area. Since 3.12.
			/// </summary>
			UseHeaderBar = 4
		}

		/// <summary>
		/// The type of message being displayed in the dialog.
		/// </summary>
		public enum GtkMessageType
		{
			/// <summary>
			/// Informational message
			/// </summary>
			Info,
			/// <summary>
			/// Non-fatal warning message
			/// </summary>
			Warning,
			/// <summary>
			/// Question requiring a choice
			/// </summary>
			Question,
			/// <summary>
			/// Fatal error message
			/// </summary>
			Error,
			/// <summary>
			/// None of the above, doesn't get an icon
			/// </summary>
			Other
		}

		/// <summary>
		/// justification for label and maybe other widgets (text?)
		/// </summary>
		public enum GtkJustification
		{
			Left,
			Right,
			Center,
			Fill
		}

		/// <summary>
		/// Predefined values for use as response ids in gtk_dialog_add_button(). All predefined values are negative, GTK+ leaves positive values for
		/// application-defined response ids.
		/// </summary>
		public enum GtkResponseType
		{
			/// <summary>
			/// Returned if an action widget has no response id, or if the dialog gets programmatically hidden or destroyed
			/// </summary>
			None = -1,
			/// <summary>
			/// Generic response id, not used by GTK+ dialogs
			/// </summary>
			Reject = -2,
			/// <summary>
			/// Generic response id, not used by GTK+ dialogs
			/// </summary>
			Accept = -3,
			/// <summary>
			/// Returned if the dialog is deleted
			/// </summary>
			DeleteEvent = -4,
			/// <summary>
			/// Returned by OK buttons in GTK+ dialogs
			/// </summary>
			OK = -5,
			/// <summary>
			/// Returned by Cancel buttons in GTK+ dialogs
			/// </summary>
			Cancel = -6,
			/// <summary>
			/// Returned by Close buttons in GTK+ dialogs
			/// </summary>
			Close = -7,
			/// <summary>
			/// Returned by Yes buttons in GTK+ dialogs
			/// </summary>
			Yes = -8,
			/// <summary>
			/// Returned by No buttons in GTK+ dialogs
			/// </summary>
			No = -9,
			/// <summary>
			/// Returned by Apply buttons in GTK+ dialogs
			/// </summary>
			Apply = -10,
			/// <summary>
			/// Returned by Help buttons in GTK+ dialogs
			/// </summary>
			Help = -11
		}

		public enum GtkPackType
		{
			Start,
			End
		}

		/// <summary>
		/// Indicates the relief to be drawn around a GtkButton.
		/// </summary>
		public enum GtkReliefStyle
		{
			/// <summary>
			/// Draw a normal relief.
			/// </summary>
			Normal,
			/// <summary>
			/// A half relief.
			/// </summary>
			Half,
			/// <summary>
			/// No relief.
			/// </summary>
			None
		}

		/// <summary>
		/// Describes whether a #GtkFileChooser is being used to open existing files or to save to a possibly new file.
		/// </summary>
		public enum GtkFileChooserAction
		{
			/// <summary>
			/// Indicates open mode.  The file chooser will only let the user pick an existing file.
			/// </summary>
			Open,
			/// <summary>
			/// Indicates save mode.  The file chooser will let the user pick an existing file, or type in a new filename.
			/// </summary>
			Save,
			/// <summary>
			/// Indicates an Open mode for selecting folders.  The file chooser will let the user pick an existing folder.
			/// </summary>
			SelectFolder,
			/// <summary>
			/// Indicates a mode for creating a new folder.  The file chooser will let the user name an existing or new folder.
			/// </summary>
			CreateFolder
		}

		public enum GtkLicense
		{
			Unknown,
			Custom,

			GPL20,
			GPL30,

			LGPL21,
			LGPL30,

			BSD,
			MITX11,

			Artistic
		}

		public enum GtkAlign
		{
			Fill,
			Start,
			End,
			Center,
			Baseline
		}

		public enum GtkPrintOperationResult
		{
			Error,
			Apply,
			Cancel,
			InProgress
		}

		public enum GtkPrintOperationAction
		{
			/// <summary>
			/// Show the print dialog.
			/// </summary>
			PrintDialog,
			/// <summary>
			/// Start to print without showing the print dialog, based on the current print settings.
			/// </summary>
			Print,
			/// <summary>
			/// Show the print preview.
			/// </summary>
			Preview,
			/// <summary>
			/// Export to a file. This requires the export-filename property to be set.
			/// </summary>
			Export
		}

		[Flags()]
		public enum GtkTargetFlags : uint
		{
			/// <summary>
			/// If this is set, the target will only be selected for drags within a single application.
			/// </summary>
			SameApp = 1 << 0,
			/// <summary>
			/// If this is set, the target will only be selected for drags within a single widget.
			/// </summary>
			SameWidget = 1 << 1,
			/// <summary>
			/// If this is set, the target will not be selected for drags within a single application.
			/// </summary>
			OtherApp = 1 << 2,
			/// <summary>
			/// If this is set, the target will not be selected for drags withing a single widget.
			/// </summary>
			OtherWidget = 1 << 3
		}


		/// <summary>
		/// These flags indicate what parts of a <see cref="Structures.GtkFileFilterInfo"/> struct are filled or need to be filled.
		/// </summary>
		[Flags()]
		public enum GtkFileFilterFlags
		{
			/// <summary>
			/// the filename of the file being tested
			/// </summary>
			FileName = 1 << 0,
			/// <summary>
			/// the URI for the file being tested
			/// </summary>
			FilterURI = 1 << 1,
			/// <summary>
			/// the string that will be used to display the file in the file chooser
			/// </summary>
			DisplayName = 1 << 2,
			/// <summary>
			/// the mime type of the file
			/// </summary>
			MIMEType = 1 << 3
		}

		/// <summary>
		/// Specifies the various types of action that will be taken on behalf of the user for a drag destination site.
		/// </summary>
		[Flags()]
		public enum GtkDestDefaults
		{
			/// <summary>
			/// If set for a widget, GTK+, during a drag over this widget will check if the drag matches this widget’s list of possible targets and actions. GTK+ will then call gdk_drag_status() as appropriate.
			/// </summary>
			Motion = 1 << 0,
			/// <summary>
			/// If set for a widget, GTK+ will draw a highlight on this widget as long as a drag is over this widget and the widget drag format and action are acceptable.
			/// </summary>
			Highlight = 1 << 1,
			/// <summary>
			/// If set for a widget, when a drop occurs, GTK+ will will check if the drag matches this widget’s list of possible targets and actions. If so, GTK+ will call gtk_drag_get_data() on behalf of the widget. Whether or not the drop is successful, GTK+ will call gtk_drag_finish(). If the action was a move, then if the drag was successful, then TRUE will be passed for the delete parameter to gtk_drag_finish().
			/// </summary>
			Drop = 1 << 2,
			/// <summary>
			/// If set, specifies that all default actions should be taken.
			/// </summary>
			All = Motion | Highlight | Drop
		}

		/// <summary>
		/// Used to control what selections users are allowed to make.
		/// </summary>
		public enum GtkSelectionMode
		{
			/// <summary>
			/// No selection is possible.
			/// </summary>
			None,
			/// <summary>
			/// Zero or one element may be selected.
			/// </summary>
			Single,
			/// <summary>
			/// Exactly one element is selected. In some circumstances, such as initially or during a search operation, it’s possible for no element to be selected with GTK_SELECTION_BROWSE. What is really enforced is that the user can’t deselect a currently selected element except by selecting another element.
			/// </summary>
			Browse,
			/// <summary>
			/// Any number of elements may be selected. The Ctrl key may be used to enlarge the selection, and Shift key to select between the focus and the child pointed to. Some widgets may also allow Click-drag to select a range of elements.
			/// </summary>
			Multiple
		}
		/// <summary>
		/// Window placement can be influenced using this enumeration. 
		/// </summary>
		public enum GtkWindowPosition
		{
			/// <summary>
			/// No influence is made on placement.
			/// </summary>
			None,
			/// <summary>
			/// Windows should be placed in the center of the screen.
			/// </summary>
			Center,
			/// <summary>
			/// Windows should be placed at the current mouse position.
			/// </summary>
			Mouse,
			/// <summary>
			/// Keep window centered as it changes size, etc.
			/// </summary>
			/// <remarks>Note that using CenterAlways is almost always a bad idea. It won’t necessarily work well with all window managers or on all windowing systems.</remarks>
			CenterAlways,
			/// <summary>
			/// Center the window on its transient parent (see <see cref="Methods.gtk_window_set_transient_for" />).
			/// </summary>
			CenterOnParent
		}

		public enum GtkAttachOptions
		{
			Expand = 1 << 0,
			Shrink = 1 << 1,
			Fill = 1 << 2
		}
		public enum GtkStyleProviderPriority
		{
			Fallback = 1,
			Theme = 200,
			Settings = 400,
			Application = 600,
			User = 800
		}

		/// <summary>
		/// Used to customize the appearance of a GtkToolbar. Note that setting the toolbar style overrides the user’s preferences for the
		/// default toolbar style. Note that if the button has only a label set and GTK_TOOLBAR_ICONS is used, the label will be visible,
		/// and vice versa.
		/// </summary>
		public enum GtkToolbarStyle
		{
			/// <summary>
			/// Buttons display only icons in the toolbar.
			/// </summary>
			Icons,
			/// <summary>
			/// Buttons display only text labels in the toolbar.
			/// </summary>
			Text,
			/// <summary>
			/// Buttons display text and icons in the toolbar.
			/// </summary>
			Both,
			/// <summary>
			/// Buttons display icons and text alongside each other, rather than vertically stacked
			/// </summary>
			BothHorizontal
		}

		public enum GtkPositionType
		{
			Left,
			Right,
			Top,
			Bottom
		}

		/// <summary>
		/// Focus movement types.
		/// </summary>
		public enum GtkDirectionType
		{
			/// <summary>
			/// Move forward.
			/// </summary>
			TabForward,
			/// <summary>
			/// Move backward.
			/// </summary>
			TabBackward,
			/// <summary>
			/// Move up.
			/// </summary>
			Up,
			/// <summary>
			/// Move down.
			/// </summary>
			Down,
			/// <summary>
			/// Move left.
			/// </summary>
			Left,
			/// <summary>
			/// Move right.
			/// </summary>
			Right
		}

		/// <summary>
		/// The status gives a rough indication of the completion of a running print operation.
		/// </summary>
		public enum GtkPrintStatus
		{
			/// <summary>
			/// The printing has not started yet; this status is set initially, and while the print dialog is shown.
			/// </summary>
			Initial,
			/// <summary>
			/// This status is set while the begin-print signal is emitted and during pagination.
			/// </summary>
			Preparing,
			/// <summary>
			/// This status is set while the pages are being rendered.
			/// </summary>
			GeneratingData,
			/// <summary>
			/// The print job is being sent off to the printer.
			/// </summary>
			SendingData,
			/// <summary>
			/// The print job has been sent to the printer, but is not printed for some reason, e.g. the printer may be stopped.
			/// </summary>
			Pending,
			/// <summary>
			/// Some problem has occurred during printing, e.g. a paper jam.
			/// </summary>
			PendingIssue,
			/// <summary>
			/// The printer is processing the print job.
			/// </summary>
			Printing,
			/// <summary>
			/// The printing has been completed successfully.
			/// </summary>
			Finished,
			/// <summary>
			/// The printing has been aborted.
			/// </summary>
			Aborted
		}

		[Flags()]
		public enum GtkIconLookupFlags
		{
			None = 0,
			/// <summary>
			/// Never get SVG icons, even if gdk-pixbuf supports them. Cannot be used together with GTK_ICON_LOOKUP_FORCE_SVG.
			/// </summary>
			NoSVG = 1 << 0,
			/// <summary>
			/// Get SVG icons, even if gdk-pixbuf doesn’t support them. Cannot be used together with GTK_ICON_LOOKUP_NO_SVG.
			/// </summary>
			ForceSVG = 1 << 1,
			/// <summary>
			/// When passed to gtk_icon_theme_lookup_icon() includes builtin icons as well as files.For a builtin icon, gtk_icon_info_get_filename() is NULL and you need to call gtk_icon_info_get_builtin_pixbuf().
			/// </summary>
			UseBuiltin = 1 << 2,
			/// <summary>
			/// Try to shorten icon name at '-' characters before looking at inherited themes.This flag is only supported in functions that take a single icon name.For more general fallback, see gtk_icon_theme_choose_icon(). Since 2.12.
			/// </summary>
			GenericFallback = 1 << 3,
			/// <summary>
			/// Always get the icon scaled to the requested size. Since 2.14.
			/// </summary>
			ForceSize = 1 << 4,
			/// <summary>
			/// Try to always load regular icons, even when symbolic icon names are given.Since 3.14.
			/// </summary>
			ForceRegular = 1 << 5,
			/// <summary>
			/// Try to always load symbolic icons, even when regular icon names are given.Since 3.14.
			/// </summary>
			ForceSymbolic = 1 << 6,
			/// <summary>
			/// Try to load a variant of the icon for left-to-right text direction.Since 3.14.
			/// </summary>
			LeftToRight = 1 << 7,
			/// <summary>
			/// Try to load a variant of the icon for right-to-left text direction.Since 3.14.
			/// </summary>
			RightToLeft = 1 << 8
		}

		/// <summary>
		/// Describes a widget state. Widget states are used to match the widget
		/// against CSS pseudo-classes. Note that GTK extends the regular CSS
		/// classes and sometimes uses different names.
		/// </summary>
		[Flags()]
		public enum GtkStateFlags
		{
			/// <summary>
			/// State during normal operation.
			/// </summary>
			Normal = 0,
			/// <summary>
			/// Widget is active.
			/// </summary>
			Active = 1 << 0,
			/// <summary>
			/// Widget has a mouse pointer over it.
			/// </summary>
			Hover = 1 << 1,
			/// <summary>
			/// Widget is selected.
			/// </summary>
			Selected = 1 << 2,
			/// <summary>
			/// Widget is insensitive.
			/// </summary>
			Disabled = 1 << 3,
			/// <summary>
			/// Widget is inconsistent.
			/// </summary>
			Inconsistent = 1 << 4,
			/// <summary>
			/// Widget has the keyboard focus.
			/// </summary>
			Focused = 1 << 5,
			/// <summary>
			/// Widget is in a background toplevel window.
			/// </summary>
			Backdrop = 1 << 6,
			/// <summary>
			/// Widget is in left-to-right text direction. Since 3.8
			/// </summary>
			LeftToRight = 1 << 7,
			/// <summary>
			/// Widget is in right-to-left text direction. Since 3.8
			/// </summary>
			RightToLeft = 1 << 8,
			/// <summary>
			/// Widget is a link. Since 3.12
			/// </summary>
			Link = 1 << 9,
			/// <summary>
			/// The location the widget points to has already been visited. Since 3.12
			/// </summary>
			Visited = 1 << 10,
			/// <summary>
			/// Widget is checked. Since 3.14
			/// </summary>
			Checked = 1 << 11,
			/// <summary>
			/// Widget is highlighted as a drop target for DND. Since 3.20
			/// </summary>
			DropActive = 1 << 12
		}

		/// <summary>
		/// Describes a type of line wrapping.
		/// </summary>
		public enum GtkWrapMode
		{
			/// <summary>
			/// do not wrap lines; just make the text area wider
			/// </summary>
			None,
			/// <summary>
			/// wrap text, breaking lines anywhere the cursor can appear (between characters, usually - if you want to be technical, between graphemes, see pango_get_log_attrs())
			/// </summary>
			Character,
			/// <summary>
			/// wrap text, breaking lines in between words
			/// </summary>
			Word,
			/// <summary>
			/// wrap text, breaking lines in between words, or if that is not enough, also between graphemes
			/// </summary>
			WordCharacter
		}
	}
}

