using System;

internal static class TemporaryFileManager
{
	private static Random rand = new Random();

	private static System.Collections.Specialized.StringCollection mvarTemporaryFileNames = new System.Collections.Specialized.StringCollection();
	private static string mvarTemporaryFilePath = null;

	public static string CreateTemporaryFile(string FileName, byte[] FileData)
	{
		if (mvarTemporaryFilePath == null) throw new InvalidOperationException();
		if (!System.IO.Directory.Exists(mvarTemporaryFilePath))
		{
			System.IO.Directory.CreateDirectory(mvarTemporaryFilePath);
		}

		FileName = System.IO.Path.GetFileName(FileName);
		if (FileData == null) FileData = new byte[0];

		string filePath = mvarTemporaryFilePath + System.IO.Path.DirectorySeparatorChar.ToString() + FileName;
		System.IO.File.WriteAllBytes(filePath, FileData);
		mvarTemporaryFileNames.Add(filePath);
		return filePath;
	}

	public static bool RegisterTemporaryDirectory(string prefix, int maxNameLength)
	{
		if (mvarTemporaryFilePath != null)
		{
			return false;
		}
		string name = String.Empty;
		do
		{
			name = prefix + rand.Next(0, (maxNameLength * 10) - 1).ToString().PadLeft(maxNameLength - prefix.Length, '0');
		}
		while (System.IO.Directory.Exists(name));

		string pathName = System.IO.Path.GetTempPath() + System.IO.Path.DirectorySeparatorChar.ToString() + name;
		System.IO.Directory.CreateDirectory(pathName);

		mvarTemporaryFilePath = pathName;
		return true;
	}
	public static bool UnregisterTemporaryDirectory()
	{
		if (mvarTemporaryFilePath == null) return false;

		foreach (string fileName in mvarTemporaryFileNames)
		{
			if (System.IO.File.Exists(fileName))
			{
				System.IO.File.Delete(fileName);
			}
		}
		if (System.IO.Directory.Exists(mvarTemporaryFilePath))
		{
			System.IO.Directory.Delete(mvarTemporaryFilePath, true);
		}
		return true;
	}
}