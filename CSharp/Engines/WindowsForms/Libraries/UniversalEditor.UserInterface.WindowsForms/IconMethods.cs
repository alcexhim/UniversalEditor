using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

internal static class IconMethods
{
    public enum IconSize
    {
        Large,
        Small
    }
    private static class Internal
    {
        #region Old ExtractAssociatedIcon
        public static HandleRef NullHandleRef = default(HandleRef);
        [DllImport("shell32.dll", EntryPoint = "ExtractAssociatedIcon", CharSet = CharSet.Auto)]
        public static extern IntPtr IntExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index);
        public static IntPtr ExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index)
        {
            return IntExtractAssociatedIcon(hInst, iconPath, ref index);
        }
        #endregion
        #region New ExtractIconEx
        [DllImport("shell32.dll", EntryPoint = "ExtractIconEx")]
        public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        #endregion
    }

    #region Old ExtractAssociatedIcon
    private static Icon __ExtractAssociatedIcon(string filePath, int index)
    {
        Uri uri = null;
        if ((filePath == null))
        {
            throw new ArgumentException("Argument cannot be null.", "filePath");
        }
        try
        {
            uri = new Uri(filePath);
        }
        catch (UriFormatException)
        {
            filePath = Path.GetFullPath(filePath);
            uri = new Uri(filePath);
        }
        //If uri.IsUnc Then
        //    Throw New ArgumentException("File path cannot be a UNC path.", "filePath")
        //End If
        if (uri.IsFile | uri.IsUnc)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            System.Text.StringBuilder iconPath = new System.Text.StringBuilder(260);
            iconPath.Append(filePath);
            IntPtr handle = Internal.ExtractAssociatedIcon(Internal.NullHandleRef, iconPath, ref index);
            if ((handle != IntPtr.Zero))
            {
                return Icon.FromHandle(handle);
            }
        }
        return null;
    }
    #endregion
    #region New ExtractIconEx
    private static Icon __EIEExtractAssociatedIcon(string filePath, int index, IconSize size)
    {
        IntPtr[] phiconLarge = new IntPtr[] { IntPtr.Zero };
        IntPtr[] phiconSmall = new IntPtr[] { IntPtr.Zero };

        Internal.ExtractIconEx(filePath, index, phiconLarge, phiconSmall, 1);

        switch (size)
        {
            case IconSize.Large:
                try
                {
                    return Icon.FromHandle(phiconLarge[0]);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            case IconSize.Small:
                try
                {
                    return Icon.FromHandle(phiconSmall[0]);
                }
                catch (ArgumentException)
                {
                    return null;
                }
        }
        return null;
    }
    #endregion

    public static Icon ExtractFileTypeIcon(string filePath)
    {
        string ext = System.IO.Path.GetExtension(filePath);

        string iconPath = String.Empty;
        int iconIndex = 0;

        switch (System.Environment.OSVersion.Platform)
        {
            case PlatformID.MacOSX:
            {
                break;
            }
            case PlatformID.Unix:
            {
                break;
            }
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
            {
                // TODO: Implement something that uses along the lines of MIME info
                // specification

                /*
                    http://standards.freedesktop.org/shared-mime-info-spec/shared-mime-info-spec-latest.html
                    
                    <?xml version="1.0" encoding="UTF-8"?>
                    <mime-info xmlns="http://www.freedesktop.org/standards/shared-mime-info">
                        <mime-type type="application/msword">
                            <comment>MS Word Files</comment>
                            <glob pattern="*.doc"/>
                        </mime-type>
                    </mime-info>
                 */
                
                Microsoft.Win32.RegistryKey rkExt = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                
                string className = rkExt.GetValue(String.Empty, String.Empty).ToString();
                Microsoft.Win32.RegistryKey rkClass = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(className);
                if (rkClass != null)
                {
                    Microsoft.Win32.RegistryKey rkDefaultIcon = rkClass.OpenSubKey("DefaultIcon");
                    if (rkDefaultIcon != null)
                    {
                        string defaultIconPath = rkDefaultIcon.GetValue(String.Empty, String.Empty).ToString();
                        int splitIndex = defaultIconPath.LastIndexOf(',');

                        string defaultIconFileName = defaultIconPath.Substring(0, splitIndex);
                        string defaultIconIndex = defaultIconPath.Substring(splitIndex + 1);

                        Int32.TryParse(defaultIconIndex, out iconIndex);
                        if (defaultIconFileName.StartsWith("\""))
                        {
                            defaultIconFileName = defaultIconFileName.Substring(1);
                        }
                        if (defaultIconFileName.EndsWith("\""))
                        {
                            defaultIconFileName = defaultIconFileName.Substring(0, defaultIconFileName.Length - 1);
                        }

                        iconPath = defaultIconFileName;
                    }
                }
                break;
            }
        }

        
        Icon icon = ExtractAssociatedIcon(iconPath, iconIndex);
        return icon;
    }

    public static Icon ExtractAssociatedIcon(string filePath, int index = 0)
    {
        return ExtractAssociatedIcon(filePath, index, IconSize.Large);
    }
    public static Icon ExtractAssociatedIcon(string filePath, int index, IconSize size)
    {
        Icon icon = null;
        switch (System.Environment.OSVersion.Platform)
        {
            case PlatformID.MacOSX:
                // TODO: Mac OS X support?
                break;
            case PlatformID.Unix:
                break;
            case PlatformID.Win32NT:
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
                icon = __EIEExtractAssociatedIcon(filePath, index, size);
                break;
            case PlatformID.Xbox:
                break;
        }
        return icon;
    }
}