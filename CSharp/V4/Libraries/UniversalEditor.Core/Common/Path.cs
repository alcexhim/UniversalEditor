using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Common
{
    /// <summary>
    /// Provides common path operations.
    /// </summary>
	public static class Path
	{
        /// <summary>
        /// Generates an absolute path from the given relative (or absolute) path and a parent path.
        /// </summary>
        /// <param name="sourcePath">The relative (or absolute) path to make absolute.</param>
        /// <param name="parentPath">The parent path of the given relative path.</param>
        /// <returns>A concatenation of the source path and the parent path if the source path is relative; otherwise,
        /// just the source path.</returns>
		public static string MakeAbsolutePath(string sourcePath, string parentPath = null)
		{
			string result;
			if (String.IsNullOrEmpty(parentPath))
			{
				parentPath = Environment.CurrentDirectory;
			}
			if ((sourcePath.StartsWith("/") && (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)) || (sourcePath.Length > 1 && sourcePath[1] == ':'))
			{
				result = sourcePath;
			}
			else
			{
				result = parentPath + System.IO.Path.DirectorySeparatorChar.ToString() + sourcePath;
			}

			result = result.Replace(System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar);
			return result;
		}

        /// <summary>
        /// Generates a relative path from the specified absolute (or relative) path and parent directory name.
        /// </summary>
        /// <param name="FileName">The absolute (or relative) path to make relative.</param>
        /// <param name="DirectoryName">The parent path for the relative path.</param>
        /// <returns>A substring of the relative path minus the parent path if the absolute path starts with the
        /// parent path; otherwise, the relative (or absolute) path.</returns>
		public static string MakeRelativePath(string FileName, string DirectoryName)
		{
            // This only goes "down" the folder hierarchy (e.g. C:/path/to/parent with C:/path as the parent becomes
            // to/parent). Should we bother implementing "up" the hierarchy (will take some psychic powers) to allow
            // us to use dot-dot paths (../../some/other/folder)?
			if (FileName.StartsWith(DirectoryName))
			{
				return FileName.Substring(DirectoryName.Length);
			}
			return FileName;
		}
	}
}
