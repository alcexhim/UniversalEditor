//
//  Program.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using UniversalEditor.ObjectModels.Text.Formatted.Items;

namespace UniversalEditor.ConsoleApplication
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				Console.WriteLine(args[0]);
				if (args[0] == "--list-associations")
				{
					// wake up the UE
					DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();

					Association[] assocs = Association.GetAllAssociations();
					foreach (Association assoc in assocs)
					{
						Console.WriteLine(assoc.ToString());
					}
					return;
				}
				else if (args[0] == "--list-dataformats")
				{
					DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();
					foreach (DataFormatReference dfr in dfrs)
					{
						Console.WriteLine(dfr.ToString());
					}
					return;
				}
			}

			return;

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			System.IO.Stream st = Console.OpenStandardInput();
			System.IO.Stream stout = Console.OpenStandardOutput();

			while (true)
			{
				byte b = (byte) st.ReadByte();
				if (b == 0xFF) break;

				ms.WriteByte(b);
			}

			UniversalEditor.DataFormats.Text.Formatted.RichText.RTFDataFormat rtf = new DataFormats.Text.Formatted.RichText.RTFDataFormat();
			UniversalEditor.ObjectModels.Text.Formatted.FormattedTextObjectModel ftom = new ObjectModels.Text.Formatted.FormattedTextObjectModel();

			FormattedTextItemParagraph p =	new FormattedTextItemParagraph();
			FormattedTextItemLiteral lit = new FormattedTextItemLiteral();
			lit.Text = System.Text.Encoding.UTF8.GetString(ms.ToArray());
			p.Items.Add(lit);

			ftom.Items.Add(p);


			UniversalEditor.Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor();
			UniversalEditor.Document.Save(ftom, rtf, ma);

				byte[] output = ma.ToArray();
			stout.Write(output, 0, output.Length);
		}
	}
}
