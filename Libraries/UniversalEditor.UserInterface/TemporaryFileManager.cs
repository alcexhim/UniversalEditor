using System;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface
{
	public class TemporaryFileManager
	{
		private static Random rand = new Random();

		private static System.Collections.Specialized.StringCollection mvarTemporaryFileNames = new System.Collections.Specialized.StringCollection();
		private static string mvarTemporaryFilePath = null;

		private static string SanitizeFileName(string FileName)
		{
			if (String.IsNullOrEmpty(FileName)) return "_";
			string[] invalidCharacters = new string[]
			{
				"\\", "/", ":", "*", "\"", "<", ">", "?", "|"
			};
			foreach (string invalidCharacter in invalidCharacters)
			{
				FileName = FileName.Replace(invalidCharacter, "_");
			}

			// Check twice because we might have encountered a filename consisting solely of invalid chars (weird, but possible)
			if (String.IsNullOrEmpty(FileName)) return "_";
			return FileName;
		}

		public static string GetTemporaryDirectory()
		{
			if (mvarTemporaryFilePath == null) throw new InvalidOperationException();
			if (!System.IO.Directory.Exists(mvarTemporaryFilePath))
			{
				System.IO.Directory.CreateDirectory(mvarTemporaryFilePath);
			}
			return mvarTemporaryFilePath;
		}
		public static string GetTemporaryFileName()
		{
			string fpath = System.IO.Path.GetTempFileName();
			string fname = System.IO.Path.GetFileName(fpath);
			System.IO.File.Delete(fpath);
			return GetTemporaryDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + fname;
		}
		public static string CreateTemporaryFile(string FileName, byte[] FileData = null)
		{
			FileName = SanitizeFileName(FileName);
			FileName = System.IO.Path.GetFileName(FileName);

			string filePath = GetTemporaryDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + FileName;
			if (FileData != null) System.IO.File.WriteAllBytes(filePath, FileData);
			mvarTemporaryFileNames.Add(filePath);
			return filePath;
		}

		public static bool RegisterTemporaryDirectory()
		{
			while (true)
			{
				string pathName = System.IO.Path.GetRandomFileName();
				string path = System.IO.Path.Combine(new string[]
				{
					System.IO.Path.GetTempPath(),
					"universal-editor",
					pathName
				});

				if (!System.IO.Directory.Exists(path))
				{
					System.IO.Directory.CreateDirectory(path);

					mvarTemporaryFilePath = path;
					return true;
				}
			}
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

			System.Collections.Generic.List<string> fileNamesNotDeleted = new System.Collections.Generic.List<string>();

			foreach (string fileName in mvarTemporaryFileNames)
			{
				if (System.IO.File.Exists(fileName))
				{
					try
					{
						System.IO.File.Delete(fileName);
					}
					catch (Exception ex)
					{
						fileNamesNotDeleted.Add(fileName);
					}
				}
			}
			if (System.IO.Directory.Exists(mvarTemporaryFilePath) && fileNamesNotDeleted.Count == 0)
			{
				System.IO.Directory.Delete(mvarTemporaryFilePath, true);
			}

			string uetmppath = System.IO.Path.Combine(new string[] { System.IO.Path.GetTempPath(), "universal-editor" });
			if (System.IO.Directory.Exists(uetmppath) && System.IO.Directory.GetFileSystemEntries(uetmppath).Length == 0)
			{
				System.IO.Directory.Delete(uetmppath);
			}
			return true;
		}

		private static void RecursiveWriteContents(string folderPath, IFileSystemObject[] contents)
		{
			if (!System.IO.Directory.Exists(folderPath))
				System.IO.Directory.CreateDirectory(folderPath);

			for (int i = 0; i < contents.Length; i++)
			{
				string fn = SanitizeFileName(contents[i].Name);
				string filePath = folderPath + System.IO.Path.DirectorySeparatorChar.ToString() + fn;
				if (contents[i] is File)
				{
					System.IO.File.WriteAllBytes(filePath, (contents[i] as File).GetData());
				}
				else if (contents[i] is Folder)
				{
					RecursiveWriteFolder(filePath, contents[i] as Folder);
				}
			}
		}
		private static void RecursiveWriteFolder(string filePath, Folder folder)
		{
			if (!System.IO.Directory.Exists(filePath))
				System.IO.Directory.CreateDirectory(filePath);

			IFileSystemObject[] contents = folder.GetContents();
			RecursiveWriteContents(filePath, contents);
		}

		public static string CreateTemporaryFolder(string name, IFileSystemObject[] contents)
		{
			name = SanitizeFileName(name);
			name = System.IO.Path.GetFileName(name);

			string folderPath = GetTemporaryDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + name;
			RecursiveWriteContents(folderPath, contents);
			mvarTemporaryFileNames.Add(folderPath);
			return folderPath;
		}
	}
}
