using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Internal.Windows
{
    internal static class Structures
    {
        public struct TASKDIALOG_BUTTON
        {
            public int nButtonID;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszButtonText;

            public TASKDIALOG_BUTTON(int nButtonID, string szButtonText)
            {
                this.nButtonID = nButtonID;

                // IntPtr pszButtonText = Marshal.StringToHGlobalAuto(szButtonText);
                this.pszButtonText = szButtonText;
            }
        }
        public struct TASKDIALOGCONFIG
        {
            /// <summary>
            /// Specifies the structure size, in bytes.
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// Handle to the parent window. This member can be NULL.
            /// </summary>
            public IntPtr /*HWND*/ hwndParent;
            /// <summary>
            /// Handle to the module that contains the icon resource identified by the pszMainIcon or pszFooterIcon members, and the string resources identified by the
            /// pszWindowTitle, pszMainInstruction, pszContent, pszVerificationText, pszExpandedInformation, pszExpandedControlText, pszCollapsedControlText or pszFooter
            /// members.
            /// </summary>
            public IntPtr /*HINSTANCE*/ hInstance;
            /// <summary>
            /// Specifies the behavior of the task dialog. This parameter can be a combination of flags from the <see cref="Constants.TaskDialogFlags" /> enumeration.
            /// </summary>
            public Constants.TaskDialogFlags dwFlags;
            /// <summary>
            /// Specifies the push buttons displayed in the task dialog. If no common buttons are specified and no custom buttons are specified using the cButtons and
            /// pButtons members, the task dialog will contain the OK button by default. This parameter may be a combination of flags from the
            /// <see cref="Constants.TaskDialogCommonButtonFlags" /> enumeration.
            /// </summary>
            public Constants.TaskDialogCommonButtonFlags dwCommonButtons;
            /// <summary>
            /// Pointer that references the string to be used for the task dialog title. This parameter can be either a null-terminated string or an integer resource
            /// identifier passed to the MAKEINTRESOURCE macro. If this parameter is NULL, the filename of the executable program is used.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszWindowTitle;
            /// <summary>
            /// A handle to an Icon that is to be displayed in the task dialog. This member is ignored unless the TDF_USE_HICON_MAIN flag is specified. If this member is
            /// NULL and the TDF_USE_HICON_MAIN is specified, no icon will be displayed.
            /// 
            /// Pointer that references the icon to be displayed in the task dialog. This parameter is ignored if the USE_HICON_MAIN flag is specified. Otherwise, if this
            /// parameter is NULL or the hInstance parameter is NULL, no icon will be displayed. This parameter must be an integer resource identifier passed to the
            /// MAKEINTRESOURCE macro or one of the following predefined values.
            /// </summary>
            public IntPtr /*<HICON>_OR_<PCWSTR>*/ hMainIcon;
            /// <summary>
            /// Pointer that references the string to be used for the main instruction. This parameter can be either a null-terminated string or an integer resource
            /// identifier passed to the MAKEINTRESOURCE macro.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszMainInstruction;
            /// <summary>
            /// Pointer that references the string to be used for the dialog's primary content. This parameter can be either a null-terminated string or an integer
            /// resource identifier passed to the MAKEINTRESOURCE macro. If the ENABLE_HYPERLINKS flag is specified for the dwFlags member, then this string may contain
            /// hyperlinks in the form: &lt;A HREF="executablestring"&gt;Hyperlink Text&lt;/A&gt;. WARNING: Enabling hyperlinks when using content from an unsafe source may cause
            /// security vulnerabilities.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszContent;
            /// <summary>
            /// The number of entries in the pButtons array that is used to create buttons or command links in the task dialog. If this member is zero and no common
            /// buttons have been specified using the dwCommonButtons member, then the task dialog will have a single OK button displayed.
            /// </summary>
            public uint cButtons;
            /// <summary>
            /// Pointer to an array of TASKDIALOG_BUTTON structures containing the definition of the custom buttons that are to be displayed in the task dialog. This
            /// array must contain at least the number of entries that are specified by the cButtons member.
            /// </summary>
            public IntPtr pButtons;
            /// <summary>
            /// The default button for the task dialog. This may be any of the values specified in nButtonID members of one of the TASKDIALOG_BUTTON structures in the
            /// pButtons array, or one of the IDs corresponding to the buttons specified in the dwCommonButtons member. If this member is zero or its value does not
            /// correspond to any button ID in the dialog, then the first button in the dialog will be the default.
            /// </summary>
            public int nDefaultButton;
            /// <summary>
            /// The number of entries in the pRadioButtons array that is used to create radio buttons in the task dialog.
            /// </summary>
            public uint cRadioButtons;
            /// <summary>
            /// Pointer to an array of TASKDIALOG_BUTTON structures containing the definition of the radio buttons that are to be displayed in the task dialog. This
            /// array must contain at least the number of entries that are specified by the cRadioButtons member. This parameter can be NULL.
            /// </summary>
            public IntPtr pRadioButtons;
            /// <summary>
            /// The button ID of the radio button that is selected by default. If this value does not correspond to a button ID, the first button in the array is
            /// selected by default.
            /// </summary>
            public int nDefaultRadioButton;
            /// <summary>
            /// Pointer that references the string to be used to label the verification checkbox. This parameter can be either a null-terminated string or an integer
            /// resource identifier passed to the MAKEINTRESOURCE macro. If this parameter is NULL, the verification checkbox is not displayed in the task dialog. If the
            /// pfVerificationFlagChecked parameter of TaskDialogIndirect is NULL, the checkbox is not enabled.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszVerificationText;
            /// <summary>
            /// Pointer that references the string to be used for displaying additional information. This parameter can be either a null-terminated string or an integer
            /// resource identifier passed to the MAKEINTRESOURCE macro. The additional information is displayed either immediately below the content or below the footer
            /// text depending on whether the TDF_EXPAND_FOOTER_AREA flag is specified. If the TDF_ENABLE_HYPERLINKS flag is specified for the dwFlags member, then this
            /// string may contain hyperlinks in the form: &lt;A HREF="executablestring"&gt;Hyperlink Text&lt;/A&gt;. WARNING: Enabling hyperlinks when using content
            /// from an unsafe source may cause security vulnerabilities.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszExpandedInformation;
            /// <summary>
            /// Pointer that references the string to be used to label the button for collapsing the expandable information. This parameter can be either a
            /// null-terminated string or an integer resource identifier passed to the MAKEINTRESOURCE macro. This member is ignored when the pszExpandedInformation
            /// member is NULL. If this member is NULL and the pszCollapsedControlText is specified, then the pszCollapsedControlText value will be used for this member
            /// as well.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszExpandedControlText;
            /// <summary>
            /// Pointer that references the string to be used to label the button for expanding the expandable information. This parameter can be either a
            /// null-terminated string or an integer resource identifier passed to the MAKEINTRESOURCE macro. This member is ignored when the pszExpandedInformation
            /// member is NULL. If this member is NULL and the pszCollapsedControlText is specified, then the pszCollapsedControlText value will be used for this member
            /// as well.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszCollapsedControlText;
            /// <summary>
            /// A handle to an Icon that is to be displayed in the footer of the task dialog. This member is ignored unless the TDF_USE_HICON_FOOTER flag is specified
            /// and the pszFooterIcon is not. If this member is NULL and the TDF_USE_HICON_FOOTER is specified, no icon is displayed.
            /// 
            /// Pointer that references the icon to be displayed in the footer area of the task dialog. This parameter is ignored if the TDF_USE_HICON_FOOTER flag is
            /// specified, or if pszFooter is NULL. Otherwise, if this parameter is NULL or the hInstance parameter is NULL, no icon is displayed. This parameter must be
            /// an integer resource identifier passed to the MAKEINTRESOURCE macro or one of the predefined values listed for pszMainIcon.
            /// </summary>
            public IntPtr /*<HICON>_OR_<PCWSTR>*/ hFooterIcon;
            /// <summary>
            /// Pointer to the string to be used in the footer area of the task dialog. This parameter can be either a null-terminated string or an integer resource
            /// identifier passed to the MAKEINTRESOURCE macro. If the TDF_ENABLE_HYPERLINKS flag is specified for the dwFlags member, then this string may contain
            /// hyperlinks in the form: &lt;A HREF="executablestring"&gt;Hyperlink Text&lt;/A&gt; WARNING: Enabling hyperlinks when using content from an unsafe source
            /// may cause security vulnerabilities.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string /*PCWSTR*/ pszFooter;
            /// <summary>
            /// Pointer to an application-defined callback function. For more information see TaskDialogCallbackProc.
            /// </summary>
            public Delegates.PFTASKDIALOGCALLBACK pfCallback;
            /// <summary>
            /// A pointer to application-defined reference data. This value is defined by the caller.
            /// </summary>
            public IntPtr lpCallbackData;
            /// <summary>
            /// The width of the task dialog's client area, in dialog units. If 0, the task dialog manager will calculate the ideal width.
            /// </summary>
            public uint cxWidth;
        }
    }
}
