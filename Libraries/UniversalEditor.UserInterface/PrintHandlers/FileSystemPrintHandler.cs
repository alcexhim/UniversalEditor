//
//  FileSystemPrintHandler.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.Printing;

namespace UniversalEditor.UserInterface.PrintHandlers
{
	public class FileSystemPrintHandler : PrintHandler
	{
		private static PrintHandlerReference _pr = null;
		protected override PrintHandlerReference MakeReferenceInternal()
		{
			if (_pr == null)
			{
				_pr = base.MakeReferenceInternal();
				_pr.SupportedObjectModels.Add(typeof(FileSystemObjectModel));
			}
			return _pr;
		}

		protected override void PrintInternal(ObjectModel objectModel, Graphics g)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IFileSystemObject[] fsos = fsom.GetAllObjects();

			Rectangle rect = new Rectangle(0, 0, 1920, 13);
			SolidBrush brush = new SolidBrush(Colors.Black);
			for (int i = 0; i < fsos.Length; i++)
			{
				g.DrawText(fsos[i].Name, null, rect, brush);
				if (fsos[i] is File)
				{
					rect.X += 320;
					g.DrawText(Common.FileInfo.FormatSize((fsos[i] as File).Size), null, rect, brush);
					rect.X += 64;
					g.DrawText(String.Format("{0} file", System.IO.Path.GetExtension(fsos[i].Name)), null, rect, brush);
					rect.X += 64;
					g.DrawText((fsos[i] as File).ModificationTimestamp.ToString(), null, rect, brush);
					rect.X = 0;
				}
				else if (fsos[i] is Folder)
				{
					rect.X += 320;
					g.DrawText(String.Format("{0} file(s), {1} folder(s)", (fsos[i] as Folder).Files.Count, (fsos[i] as Folder).Folders.Count), null, rect, brush);
					rect.X += 64;
					g.DrawText("Folder", null, rect, brush);
					rect.X += 64;
					// g.DrawText((fsos[i] as Folder).ModificationTimestamp.ToString(), null, rect, brush);
					rect.X = 0;
				}

				rect.Y += rect.Height;
			}
		}
	}
}
