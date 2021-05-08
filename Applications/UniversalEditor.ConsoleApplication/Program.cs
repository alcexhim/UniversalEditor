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
using System.Collections.Generic;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Text.Formatted.Items;

namespace UniversalEditor.ConsoleApplication
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				if (args[0] == "--generate-associations")
				{
					string assocs = null;
					if (args.Length > 1)
					{
						if (args[1] == "--reg")
						{
							assocs = GenerateAssociationsREG();
						}
					}

					if (assocs == null)
					{
						string desktop_mime_types = null;
						GenerateAssociations(out assocs, out desktop_mime_types);
					}

					Console.WriteLine(assocs);
				}
				else if (args[0] == "--update-associations")
				{
					if (System.Environment.OSVersion.Platform == PlatformID.Unix)
					{
						string filename = "/usr/share/mime/packages/universal-editor.xml";
						GenerateAssociations(out string assocs, out string desktop_mime_types);
						try
						{
							Console.Write("updating MIME types...    ");

							System.IO.File.WriteAllText(filename, assocs);
							System.Diagnostics.Process.Start("update-mime-database", "/usr/share/mime");

							Console.WriteLine("[OK]");


							Console.Write("updating desktop database...    ");

							StringBuilder sbDesktop = new StringBuilder();
							sbDesktop.AppendLine("[Desktop Entry]");
							sbDesktop.AppendLine("Version=1.0");
							sbDesktop.AppendLine("Name=Universal Editor");
							sbDesktop.AppendLine("Comment=Open and edit any document");
							sbDesktop.AppendLine("GenericName=Document Editor");
							sbDesktop.AppendLine("Keywords=Text;Editor");
							sbDesktop.AppendLine("Exec=universal-editor %F");
							sbDesktop.AppendLine("Terminal=false");
							sbDesktop.AppendLine("X-MultipleArgs=true");
							sbDesktop.AppendLine("Type=Application");
							sbDesktop.AppendLine("Icon=universal-editor");
							sbDesktop.AppendLine("Categories=GTK;Development;IDE");
							sbDesktop.AppendLine(String.Format("MimeType={0}", desktop_mime_types));
							sbDesktop.AppendLine("StartupNotify=true");
							sbDesktop.AppendLine("WMClass=UniversalEditor.exe");
							sbDesktop.AppendLine("Actions=create-project;");
							sbDesktop.AppendLine("X-Desktop-File-Install-Version=0.24");
							sbDesktop.AppendLine();
							sbDesktop.AppendLine("[Desktop Action create-project]");
							sbDesktop.AppendLine("Name=Create a New Project");
							sbDesktop.AppendLine("Exec=universal-editor /command:FileNewProject");
							sbDesktop.AppendLine();

							System.IO.File.WriteAllText("/usr/share/applications/net.alcetech.UniversalEditor.desktop", sbDesktop.ToString());

							System.Diagnostics.Process.Start("update-desktop-database");

							Console.WriteLine("[OK]");

							System.Environment.Exit(0);
						}
						catch (UnauthorizedAccessException ex)
						{
							Console.WriteLine("ue: {0}: Permission denied", filename);
							System.Environment.Exit(1);
						}
					}
					else if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
					{

					}
				}
				else if (args[0] == "--list-associations")
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
				else if (args[0] == "--find-checksum")
				{
					if (args.Length > 0)
					{
						Console.WriteLine("looking for {0}", args[1]);
						Checksum.Modules.Adler32.Adler32 adlr = new Checksum.Modules.Adler32.Adler32();

						string[] files = new string[args.Length - 2];
						for (int i = 2; i <args.Length; i++)
						{
							files[i - 2] = args[i];
						}

						Type[] types = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(Checksum.ChecksumModule) });
						for (int i = 0; i < types.Length; i++)
						{
							Checksum.ChecksumModule module = (types[i].Assembly.CreateInstance(types[i].FullName) as Checksum.ChecksumModule);
							if (module == null) continue;

							Console.WriteLine("=== {0} ===", module.GetType().Name);

							for (int j = 0; j < files.Length; j++)
							{
								Console.Write(System.IO.Path.GetFileName(files[j]).PadRight(30));

								if (System.IO.File.Exists(files[j]))
								{
									long calc = module.Calculate(System.IO.File.ReadAllBytes(files[j]));
									calc = (int)(calc);
									Console.Write(calc.ToString());
								}
								else
								{
									Console.Write("NOT FOUND");
								}
								// Console.Write(calc.ToString("x"));
								Console.WriteLine();
							}
							Console.WriteLine();
						}
					}
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

		private static void GenerateAssociations(out string mime_info, out string desktop_mime_types)
		{
			StringBuilder sb_mime_info = new StringBuilder();
			StringBuilder sb_mime_types = new StringBuilder();

			sb_mime_info.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			sb_mime_info.AppendLine("<mime-info xmlns=\"http://www.freedesktop.org/standards/shared-mime-info\">");
			DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();

			Association[] assocs = Association.GetAllAssociations();
			for (int i = 0; i < assocs.Length; i++)
			{
				for (int j = 0; j < assocs[i].Filters.Count; j++)
				{
					string mimetype = String.Format("application/x-universaleditor-a{0}f{1}", i, j);
					if (assocs[i].Filters[j].ContentType != null)
					{
						mimetype = assocs[i].Filters[j].ContentType;
						sb_mime_types.Append(mimetype);
						sb_mime_types.Append(';');
					}
					sb_mime_info.AppendLine(String.Format("\t<mime-type type=\"{0}\">", mimetype));
					if (assocs[i].Filters[j].PerceivedType != null)
					{
						sb_mime_info.AppendLine(String.Format("\t\t<sub-class-of type=\"{0}\"/>", assocs[i].Filters[j].PerceivedType));
					}
					sb_mime_info.AppendLine(String.Format("\t\t<comment>{0}</comment>", assocs[i].Filters[j].Title));
					if (assocs[i].ObjectModels.Contains(new Guid("{a23026e9-dfe1-4090-af35-8b916d3f1fcd}")))
					{
						sb_mime_info.AppendLine(String.Format("\t\t<generic-icon name=\"package-x-generic\"/>"));
					}

					// Console.Write("registering '{0}' extensions... ", assocs[i].Filters[j].Title);
					for (int k = 0; k < assocs[i].Filters[j].FileNameFilters.Count; k++)
					{
						sb_mime_info.AppendLine(String.Format("\t\t<glob pattern=\"{0}\" />", assocs[i].Filters[j].FileNameFilters[k]));
					}

					// FIXME: this doesn't work!
					bool includeMagicBytes = true;

					if (includeMagicBytes && assocs[i].Filters[j].MagicBytes.Count > 0)
					{
						for (int k = 0; k < assocs[i].Filters[j].MagicBytes.Count; k++)
						{
							string mbcs = GetMagicByteString(assocs[i].Filters[j].MagicBytes[k], assocs[i].Filters[j].MagicByteOffsets[k]);
							if (!String.IsNullOrEmpty(mbcs))
							{
								sb_mime_info.AppendLine("\t\t<magic>");
								sb_mime_info.AppendLine(mbcs);
								sb_mime_info.AppendLine("\t\t</magic>");
							}
						}
					}

					if (assocs[i].Filters[j].IconName != null)
					{
						sb_mime_info.AppendLine(String.Format("\t\t<icon name=\"{0}\" />", assocs[i].Filters[j].IconName));
					}
					sb_mime_info.AppendLine("\t</mime-type>");
				}
			}
			sb_mime_info.Append("</mime-info>");
			mime_info = sb_mime_info.ToString();
			desktop_mime_types = sb_mime_types.ToString();
		}

		private static string GetMagicByteString(byte?[] magicBytes, int offset)
		{
			StringBuilder sb = new StringBuilder();

			int length = magicBytes.Length;
			if (length % 2 != 0)
			{
				// length not divisible by 2, so match one less character
				length--;
			}

			List<string> mgbstrings = new List<string>();
			string lastString = String.Empty;
			for (int i = 0; i < magicBytes.Length; i++)
			{
				lastString = magicBytes[i].GetValueOrDefault().ToString("x").PadRight(2, '0') + lastString;
				if ((i + 1) % 4 == 0)
				{
					mgbstrings.Add(lastString);
					lastString = String.Empty;
				}
			}

			if (!String.IsNullOrEmpty(lastString))
				mgbstrings.Add(lastString);

			if (mgbstrings.Count > 0)
			{
				int opened = 0;
				int ofs = 0;
				int ct = 0;
				for (int i = 0; i < mgbstrings.Count; i++)
				{
					string type = String.Empty;
					if (mgbstrings[i].Length == 4)
					{
						type = "little16";
						ofs = 2;
					}
					else if (mgbstrings[i].Length == 8)
					{
						type = "little32";
						ofs = 4;
					}

					if (type == String.Empty)
						continue;

					ct++;
					sb.Append("<match type=\"");
					sb.Append(type);
					sb.Append("\" offset=\"");
					sb.Append(offset);
					sb.Append("\" value=\"0x");
					sb.Append(mgbstrings[i]);
					sb.Append("\"");
					if (i < mgbstrings.Count - 1)
					{
						opened++;
						sb.Append(">");
					}
					offset += ofs;
				}
				if (ct > 0)
				{
					sb.Append(" />");
					for (int i = 0; i < opened; i++)
					{
						sb.Append("</match>");
					}
				}
			}

			return sb.ToString();
		}

		private static string GenerateAssociationsREG()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Windows Registry Editor Version 5.00");

			DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats();

			Association[] assocs = Association.GetAllAssociations();
			for (int i = 0; i < assocs.Length; i++)
			{
				for (int j = 0; j < assocs[i].Filters.Count; j++)
				{
					for (int k = 0; k < assocs[i].Filters[j].FileNameFilters.Count; k++)
					{
						string ext = assocs[i].Filters[j].FileNameFilters[k];
						if (ext.StartsWith("*."))
						{
							ext = ext.Substring(1);
						}

						sb.AppendLine(String.Format("[HKEY_CLASSES_ROOT\\{0}{1}]", "UniversalEditor", ext));

						sb.AppendLine(String.Format("[HKEY_CLASSES_ROOT\\{0}{1}\\DefaultIcon]", "UniversalEditor", ext));
						sb.AppendLine("@=\"universal-editor.ico\"");

						sb.AppendLine(String.Format("[HKEY_CLASSES_ROOT\\{0}{1}\\Shell\\Open\\command]", "UniversalEditor", ext));
						sb.AppendLine("@=UniversalEditor.exe \"%1\"");

						sb.AppendLine(String.Format("[HKEY_CLASSES_ROOT\\{0}]", ext));
						sb.AppendLine(String.Format("@=\"{0}{1}\"", "UniversalEditor", ext));
					}
				}
			}
			return sb.ToString();
		}
	}
}
