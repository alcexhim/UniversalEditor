using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Common
{
	public static class Path
	{
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

		public static string MakeRelativePath(string FileName, string DirectoryName)
		{
			if (FileName.StartsWith(DirectoryName))
			{
				return FileName.Substring(DirectoryName.Length);
			}
			return FileName;
		}
	}
}
