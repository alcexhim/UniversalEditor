//
//  SETDataFormat.cs - implements Ark Angles setup directive file format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Setup.ArkAngles;
using UniversalEditor.ObjectModels.Setup.ArkAngles.Actions;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Setup.ArkAngles
{
	public class SETDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SetupObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SetupObjectModel setup = (objectModel as SetupObjectModel);
			if (setup == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();

				string command = line.Substring(0, line.IndexOf(' '));
				string paramzLine = line.Substring(line.IndexOf(' ') + 1);
				string[] paramz = paramzLine.Split(new char[] { ',' });

				switch (command.ToLower())
				{
					case "catalog":
					{
						setup.CatalogExecutableFileName = paramzLine;
						break;
					}
					case "deletefromsfx":
					{
						setup.DeleteFromSFX = true;
						break;
					}
					case "cols":
					{
						// top color, bottom color, title foreground, title background, title shadow offset in pixels, footer color, unknown
						// ARK ANGLES picked the UGLIEST default colors for their setup program :-(

						Color topColor = Colors.Cyan; // Colors.Blue;
						Color bottomColor = Colors.Green; // Colors.Black;
						Color titleForegroundColor = Colors.White;
						Color titleBackgroundColor = Colors.Black;
						int titleShadowOffset = 3;
						Color footerColor = Colors.Black; // Colors.White;
						Color buttonLabelColor = Colors.Black; // Colors.White;

						if (paramz.Length > 0)
						{
							topColor = StringToColor(paramz[0]);
							if (paramz.Length > 1)
							{
								bottomColor = StringToColor(paramz[1]);
								if (paramz.Length > 2)
								{
									titleForegroundColor = StringToColor(paramz[2]);
									if (paramz.Length > 3)
									{
										titleBackgroundColor = StringToColor(paramz[3]);
										if (paramz.Length > 4)
										{
											titleShadowOffset = Int32.Parse(paramz[4]);
											if (paramz.Length > 5)
											{
												footerColor = StringToColor(paramz[5]);
												if (paramz.Length > 6)
												{
													buttonLabelColor = StringToColor(paramz[6]);
												}
											}
										}
									}
								}
							}
						}
						break;
					}
					case "dir":
					{
						setup.DefaultInstallationDirectory = paramzLine;
						break;
					}
					case "doc":
					{
						// NOTE: the DOC command is found in install.set (DOS) but not
						// recognized by setup1.exe (Win16)
						setup.DocumentationFileName = paramzLine;
						break;
					}
					case "function":
					{
						for (int i = 0; i < paramzLine.Length; i++)
						{
							switch (paramzLine[i])
							{
								case 'I':
								{
									setup.AutoStartCommands.Add(AutoStartCommand.Install);
									break;
								}
								case 'C':
								{
									setup.AutoStartCommands.Add(AutoStartCommand.Catalog);
									break;
								}
								case 'X':
								{
									setup.AutoStartCommands.Add(AutoStartCommand.Exit);
									break;
								}
							}
						}
						break;
					}
					case "msg":
					case "footer":
					{
						setup.FooterText = paramzLine;
						break;
					}
					case "picture":
					{
						// I guess this is ignored in non-DOS versions, but we still have to
						// parse it
						int unknown1 = Int32.Parse(paramz[0].Trim());
						int unknown2 = Int32.Parse(paramz[1].Trim());
						int cols = Int32.Parse(paramz[2].Trim());
						int rows = Int32.Parse(paramz[3].Trim());
						for (int i = 0; i < rows; i++)
						{
							string row = reader.ReadLine();
						}
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			SetupObjectModel setup = (objectModel as SetupObjectModel);
			if (setup == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			if (!String.IsNullOrEmpty(setup.Title)) writer.WriteLine("TITLE " + setup.Title);
			if (setup.HeaderFontSize != null) writer.WriteLine("HEADFONT " + setup.HeaderFontSize.Value.ToString());
			if (!String.IsNullOrEmpty(setup.FooterText)) writer.WriteLine("FOOTER " + setup.FooterText);
			// not sure what ICON line does
			if (!String.IsNullOrEmpty(setup.CatalogExecutableFileName)) writer.WriteLine("CATALOG " + setup.CatalogExecutableFileName);

			// COLS

			if (!String.IsNullOrEmpty(setup.DefaultInstallationDirectory)) writer.WriteLine("DIR " + setup.DefaultInstallationDirectory);
			if (setup.DeleteFromSFX) writer.WriteLine("DELETEFROMSFX");

			foreach (UniversalEditor.ObjectModels.Setup.ArkAngles.Action action in setup.Actions)
			{
				if (action is FileAction)
				{
					FileAction act = (action as FileAction);
					writer.WriteLine("INSTALL " + act.SourceFileName + "," + act.DestinationFileName + ",4," + act.FileSize.ToString() + ",,," + act.Description);
				}
				else if (action is FontAction)
				{
					FontAction act = (action as FontAction);
					writer.WriteLine("FONT " + act.Title + "," + act.FileName);
				}
				else if (action is ProgramGroupAction)
				{
					ProgramGroupAction act = (action as ProgramGroupAction);
					writer.WriteLine("GROUP " + act.Title);
					foreach (ProgramGroupShortcut item in act.Shortcuts)
					{
						writer.Write("ITEM " + item.FileName + "," + item.Title);
						if (!String.IsNullOrEmpty(item.IconFileName) || item.IconIndex != null)
						{
							writer.Write(",");
							if (!String.IsNullOrEmpty(item.IconFileName)) writer.Write(item.IconFileName);
							if (item.IconIndex != null)
							{
								writer.Write(",");
								writer.Write(item.IconIndex.Value.ToString());
							}
						}
						writer.WriteLine();
					}
				}
			}
			writer.WriteLine();

			if (setup.AutoStartCommands.Count > 0)
			{
				writer.Write("FUNCTION ");
				foreach (AutoStartCommand cmd in setup.AutoStartCommands)
				{
					if (cmd == AutoStartCommand.Catalog) writer.Write("C");
					if (cmd == AutoStartCommand.Exit) writer.Write("X");
					if (cmd == AutoStartCommand.Install) writer.Write("I");
					if (cmd == AutoStartCommand.Restart) writer.Write("S");
				}
				writer.WriteLine();
			}
			if (setup.RestartAfterInstallation)
			{
				writer.WriteLine("RESTART 1");
			}
			else
			{
				writer.WriteLine("RESTART 0");
			}

			if (!String.IsNullOrEmpty(setup.LogFileName))
			{
				writer.WriteLine("LOG " + setup.LogFileName);
			}
		}

		private Color StringToColor(string colorStr)
		{
			if (!colorStr.StartsWith("$")) throw new ArgumentException("Color string must be a hexadecimal number beginning with $, like $A0B1C2");
			colorStr = colorStr.Substring(1);

			// $bbggrr format (delphi, perhaps?)

			string blueStr = colorStr.Substring(0, 2);
			string greenStr = colorStr.Substring(2, 2);
			string redStr = colorStr.Substring(4, 2);

			Color color = new Color();
			color.R = ((double)Int32.Parse(redStr) / 255);
			color.G = ((double)Int32.Parse(greenStr) / 255);
			color.B = ((double)Int32.Parse(blueStr) / 255);
			return color;
		}
		private string ColorToString(Color color)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("$");
			sb.Append(color.R.ToString("X").ToLower().PadLeft(2, '0'));
			sb.Append(color.G.ToString("X").ToLower().PadLeft(2, '0'));
			sb.Append(color.B.ToString("X").ToLower().PadLeft(2, '0'));
			return sb.ToString();
		}
	}
}
