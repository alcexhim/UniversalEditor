//
//  DEFDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.Plugins.ChaosWorks.ObjectModels.ChaosWorksScene;

namespace UniversalEditor.Plugins.ChaosWorks.DataFormats.ChaosWorksScene
{
	public class DEFDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ChaosWorksSceneObjectModel scene = (objectModel as ChaosWorksSceneObjectModel);
			if (scene == null)
				throw new ObjectModelNotSupportedException();

			IO.Reader reader = Accessor.Reader;
			string groupName = null;
			int levelIndex = -1;
			int planeIndex = -1;
			int currentLevelIndex = -1;
			int currentPlaneIndex = 0;
			string previousGroupName = null;

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				line = StripComment(line);
				line = line.Trim();

				if (String.IsNullOrEmpty(line))
					continue;

				if (line.EndsWith(":"))
				{
					// group start
					groupName = line.Substring(0, line.Length - 1);
				}
				else if (line.Contains("="))
				{
					// property set
				}
				else
				{
					string[] parms = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (groupName == "types")
					{
						previousGroupName = groupName;
						if (parms.Length >= 4)
						{
							ChaosWorksSceneType type = new ChaosWorksSceneType();
							type.Flags1 = Int16.Parse(parms[0]);
							type.Flags2 = Int16.Parse(parms[1]);
							type.Flags3 = Int16.Parse(parms[2]);
							type.Name = parms[3];
							scene.Types.Add(type);
						}
					}
					else if (groupName == "sprites")
					{
						previousGroupName = groupName;
						if (parms.Length >= 2)
						{
							ChaosWorksSceneSprite sprite = new ChaosWorksSceneSprite();
							if (parms[0] == "*")
							{
								// idk
								sprite.SpriteName = parms[1];
								sprite.TypeName = parms[2];
							}
							else
							{
								sprite.SpriteName = parms[0];
								sprite.TypeName = parms[1];
							}
							scene.Sprites.Add(sprite);
						}
					}
					else
					{
						if (groupName != previousGroupName)
						{
							levelIndex = -1;
							planeIndex = -1;
							previousGroupName = groupName;
						}

						if (levelIndex == -1 && planeIndex == -1)
						{
							if (groupName.StartsWith("level"))
							{
								if (groupName.Contains("_plane"))
								{
									int indexOfPlane = groupName.IndexOf("_plane");
									int levelLength = indexOfPlane - "level".Length;

									string szLevelIndex = groupName.Substring("level".Length, levelLength);
									string szPlaneIndex = groupName.Substring("_plane".Length + indexOfPlane);
									levelIndex = Int32.Parse(szLevelIndex);
									planeIndex = Int32.Parse(szPlaneIndex);
								}
								else
								{
									levelIndex = Int32.Parse(groupName.Substring("level".Length));
									if (levelIndex != currentLevelIndex)
									{
										currentLevelIndex = levelIndex;
										currentPlaneIndex = 0;
										if (parms.Length == 3)
										{
											ChaosWorksSceneLevel currentLevel = new ChaosWorksSceneLevel();
											currentLevel.ActivePlaneIndex = Int32.Parse(parms[0]);
											currentLevel.CenterPosition = new PositionVector2(Double.Parse(parms[1]), Double.Parse(parms[2]));
											scene.Levels.Add(currentLevel);
											continue;
										}
									}
								}
							}
						}
						else if (planeIndex == -1)
						{
							// reading level information
							ChaosWorksSceneLevelPlane plane = new ChaosWorksSceneLevelPlane();
							plane.Position = new PositionVector2(Double.Parse(parms[0]), Double.Parse(parms[1]));
							plane.u1 = Int32.Parse(parms[2]);
							plane.u2 = Int32.Parse(parms[3]);
							plane.u3 = Int32.Parse(parms[4]);
							scene.Levels[levelIndex - 1].Planes.Add(plane);
							currentPlaneIndex++;
						}
						else
						{
							// reading plane information
							if (parms.Length >= 9)
							{
								ChaosWorksSceneObject obj = new ChaosWorksSceneObject();
								obj.SpriteName = parms[0];
								obj.Phase = Int32.Parse(parms[1]);
								obj.Mirror = Int32.Parse(parms[2]);
								obj.Position = new PositionVector2(Double.Parse(parms[3]), Double.Parse(parms[4]));
								obj.Link = Int32.Parse(parms[5]);
								obj.LPos = Int32.Parse(parms[6]);
								obj.S = Int32.Parse(parms[7]);
								obj.TypeName = parms[8];
								scene.Levels[levelIndex - 1].Planes[planeIndex - 1].Objects.Add(obj);
							}
						}
					}
				}
			}
		}

		private string StripComment(string line)
		{
			int index = line.IndexOf(';');
			if (index == -1)
				return line;

			return line.Substring(0, index);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ChaosWorksSceneObjectModel scene = (objectModel as ChaosWorksSceneObjectModel);
			if (scene == null)
				throw new ObjectModelNotSupportedException();

			int availableLevels = 9, availableTypes = 1024, availableSprites = 1024, availableObjects = 65534;
			int usedLevels = scene.Levels.Count, usedTypes = scene.Types.Count, usedSprites = scene.Sprites.Count, usedObjects = 0;
			System.Collections.Generic.Dictionary<string, int> usage = new System.Collections.Generic.Dictionary<string, int>();

			for (int h = 0; h < scene.Levels.Count; h++)
			{
				for (int i = 0; i < scene.Levels[h].Planes.Count; i++)
				{
					for (int j = 0; j < scene.Levels[h].Planes[i].Objects.Count; j++)
					{
						usedObjects++;
						if (usage.ContainsKey(scene.Levels[h].Planes[i].Objects[j].TypeName))
						{
							usage[scene.Levels[h].Planes[i].Objects[j].TypeName] = usage[scene.Levels[h].Planes[i].Objects[j].TypeName] + 1;
						}
						else
						{
							usage[scene.Levels[h].Planes[i].Objects[j].TypeName] = 1;
						}
					}
				}
			}

			IO.Writer writer = Accessor.Writer;
			writer.WriteLine(";");
			writer.WriteLine(";                           used    avail   free");
			writer.WriteLine(";              ------------------------------------");

			PrintComment(writer, "levels", usedLevels, availableLevels);
			PrintComment(writer, "types", usedTypes, availableTypes);
			PrintComment(writer, "sprites", usedSprites, availableSprites);
			PrintComment(writer, "objects", usedObjects, availableObjects);
			writer.WriteLine();
			writer.WriteLine("; --- type definitions ------------------------------------------------------");
			writer.WriteLine();
			writer.WriteLine("types:");
			writer.WriteLine();
			for (int i = 0; i < scene.Types.Count; i++)
			{
				writer.Write(String.Format("  {0} {1} {2} {3}", scene.Types[i].Flags1.ToString().PadLeft(4, '0'), scene.Types[i].Flags2.ToString().PadLeft(4, '0'), scene.Types[i].Flags3.ToString().PadLeft(4, '0'), scene.Types[i].Name.PadRight(25, ' ')));

				int nUsageTimes = 0;
				if (usage.ContainsKey(scene.Types[i].Name))
				{
					nUsageTimes = usage[scene.Types[i].Name];
				}
				string usageTimes = null;
				if (nUsageTimes == 0)
				{
					usageTimes = "*** NOT USED ***";
				}
				else if (nUsageTimes == 1)
				{
					usageTimes = "used once";
				}
				else
				{
					usageTimes = String.Format("used {0} times", nUsageTimes);
				}
				writer.WriteLine(String.Format("; {0}", usageTimes));
			}

			writer.WriteLine();
			writer.WriteLine("; --- sprites and default types ---------------------------------------------");
			writer.WriteLine();
			writer.WriteLine("sprites:                                              ;  TOT L1 L2 L3 L4 L5 L6 L7 L8 L9 (kB)");
			writer.WriteLine("                                                      ; ------------------------------------");
			writer.WriteLine();
			for (int i = 0; i < scene.Sprites.Count; i++)
			{
				writer.Write(String.Format("  {0} {1}", scene.Sprites[i].SpriteName, scene.Sprites[i].TypeName).PadRight(54, ' '));
				writer.WriteLine(";   10  3  1  0  2  2  2  0  0  0    4");
			}

			writer.WriteLine();

			writer.WriteLine("; ---available levels------------------------------------------------------");
			writer.WriteLine();
			writer.WriteLine(String.Format("main_level = {0}", scene.Levels.IndexOf(scene.MainLevel)));
			writer.WriteLine();

			for (int i = 0; i < scene.Levels.Count; i++)
			{
				ChaosWorksSceneLevel level = scene.Levels[i];
				WriteLevel(writer, i, level);
			}
			for (int i = 0; i < scene.Levels.Count; i++)
			{
				ChaosWorksSceneLevel level = scene.Levels[i];
				WriteLevelData(writer, i, level);
			}
		}

		private void WriteLevelData(Writer writer, int index, ChaosWorksSceneLevel level)
		{
			for (int i = 0; i < level.Planes.Count; i++)
			{
				if (level.Planes[i].Objects.Count == 0)
					continue;

				writer.WriteLine(String.Format("level{0}_plane{1}: ;-------------------------------------------------------------", index + 1, i + 1));
				writer.WriteLine();
				writer.WriteLine("; sprite                phas   mirr  x y     link lpos s type");
				writer.WriteLine();
				for (int j = 0; j < level.Planes[i].Objects.Count; j++)
				{
					writer.WriteLine(String.Format("  {0} {1} {2} {3} {4} {5} {6} {7} {8}",
						level.Planes[i].Objects[j].SpriteName.PadRight(24, ' '),
						level.Planes[i].Objects[j].Phase.ToString().PadLeft(3, '0'),
						level.Planes[i].Objects[j].Mirror.ToString(),
						level.Planes[i].Objects[j].Position.X.ToString().PadLeft(5, ' '),
						level.Planes[i].Objects[j].Position.Y.ToString().PadRight(8, ' '),
						level.Planes[i].Objects[j].Link.ToString(),
						level.Planes[i].Objects[j].LPos.ToString().PadRight(4, ' '),
						level.Planes[i].Objects[j].S.ToString(),
						level.Planes[i].Objects[j].TypeName));
				}
				writer.WriteLine();
			}
		}
		private void WriteLevel(Writer writer, int index, ChaosWorksSceneLevel level)
		{
			writer.WriteLine(String.Format("level{0}:", index + 1));
			writer.WriteLine();
			writer.Write(String.Format("  {0} {1} {2}", level.ActivePlaneIndex, level.CenterPosition.X.ToString(), level.CenterPosition.Y.ToString()).PadRight(23, ' '));
			writer.WriteLine("; active plane and center position");
			for (int i = 0; i < level.Planes.Count; i++)
			{
				int planeSize = 0;
				writer.Write(String.Format("  {0} {1} {2} {3} {4}", level.Planes[i].Position.X.ToString("F3"), level.Planes[i].Position.Y.ToString("F3"), level.Planes[i].u1, level.Planes[i].u2, level.Planes[i].u3).PadRight(25, ' '));
				writer.WriteLine(String.Format("; plane #{0}, size {1}", (i + 1), planeSize));
			}
			writer.WriteLine();
		}

		private void PrintComment(Writer writer, string title, int nUsed, int nAvailable)
		{
			writer.WriteLine(String.Format(";              {3}{0}{1}{2}", nUsed.ToString().PadRight(8, ' '), nAvailable.ToString().PadRight(8, ' '), (nAvailable - nUsed).ToString().PadRight(8, ' '), title.PadRight(13, ' ')));
		}
	}
}
