//
//  CarcassTextDataFormat.cs
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
using UniversalEditor.Plugins.RavenSoftware.ObjectModels.Carcass;

namespace UniversalEditor.Plugins.RavenSoftware.DataFormats.Carcass.Text
{
	public class CarcassTextDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(CarcassObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CarcassObjectModel car = (objectModel as CarcassObjectModel);
			if (car == null)
				throw new ObjectModelNotSupportedException();

			IO.Reader reader = Accessor.Reader;
			string signature = reader.ReadLine();

			if (signature != "$aseanimgrabinit")
				throw new InvalidDataFormatException("file does not begin with '$aseanimgrabinit' ");

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (line.Equals("$keepmotion"))
				{
					car.KeepMotionBone = true;
				}
				else if (line.StartsWith("$scale "))
				{
					car.Scale = Double.Parse(line.Substring("$scale ".Length));
				}
				else if (line.StartsWith("$aseanimref_gla "))
				{
					car.GLAFileName = line.Substring("$aseanimref_gla ".Length);
				}
				else if (line.StartsWith("$pcj "))
				{
					car.PCJ.Add(line.Substring("$pcj ".Length));
				}
				else if (line.StartsWith("$aseanimgrab "))
				{
					ModelReference ase = new ModelReference();
					string filenameAndParms = line.Substring("$aseanimgrab ".Length);
					string filenameOnly = filenameAndParms;
					string parmsOnly = String.Empty;
					if (filenameAndParms.IndexOf(' ') != -1)
					{
						filenameOnly = filenameAndParms.Substring(0, filenameAndParms.IndexOf(' '));
						parmsOnly = filenameAndParms.Substring(filenameAndParms.IndexOf(' ') + 1);
					}

					ase.FileName = filenameOnly;
					if (filenameOnly.EndsWith(".xsi"))
						ase.FrameSpeed = ModelReference.DEFAULT_FRAMESPEED_XSI; // weird

					string[] parms = parmsOnly.Split(new char[] { ' ' });
					for (int i = 0; i < parms.Length; i++)
					{
						if (parms[i].Equals("-loop"))
						{
							ase.LoopCount = Int32.Parse(parms[i + 1]);
						}
						if (parms[i].Equals("-framespeed"))
						{
							ase.FrameSpeed = Int32.Parse(parms[i + 1]);
						}
						if (parms[i] == "-genloopframe")
						{
							ase.GenerateLoopFrame = true;
						}
						if (parms[i] == "-prequat")
						{
							car.UseLegacyCompression = true;
						}
						if (parms[i].Equals("-enum"))
						{
							ase.EnumName = parms[i + 1];
						}
						if (parms[i].Equals("-additional"))
						{
							if (i + 5 < parms.Length)
							{
								CarcassFrame addlFrame = new CarcassFrame();

								addlFrame.Target = Int32.Parse(parms[i + 1]);
								addlFrame.Count = Int32.Parse(parms[i + 2]);
								addlFrame.Loop = Int32.Parse(parms[i + 3]);
								addlFrame.Speed = Int32.Parse(parms[i + 4]);
								addlFrame.Animation = parms[i + 5];

								ase.AdditionalFrames.Add(addlFrame);
							}
						}
					}

					car.ModelReferences.Add(ase);
				}
				else if (line.StartsWith("$aseanimconvertmdx_noask "))
				{
					string filenameAndParms = line.Substring("$aseanimconvertmdx_noask ".Length);
					string filenameOnly = filenameAndParms;
					string parmsOnly = String.Empty;
					if (filenameAndParms.IndexOf(' ') != -1)
					{
						filenameOnly = filenameAndParms.Substring(0, filenameAndParms.IndexOf(' '));
						parmsOnly = filenameAndParms.Substring(filenameAndParms.IndexOf(' ') + 1);
					}
					car.BasePath = filenameOnly;

					string[] parms = parmsOnly.Split(new char[] { ' ' });
					for (int i = 0; i < parms.Length; i++)
					{
						if (parms[i] == "-makeskin")
						{
							car.MakeSkin = true;
						}
						if (parms[i] == "-smooth")
						{
							car.SmoothAllSurfaces = true;
						}
						if (parms[i] == "-losedupverts")
						{
							car.RemoveDuplicateVertices = true;
						}
						if (parms[i].Equals("-makeskel"))
						{
							car.SkeletonFileName = parms[i + 1];
						}
						if (parms[i].Equals("-origin"))
						{
							if (i + 3 < parms.Length)
							{
								car.Origin = new PositionVector3(Double.Parse(parms[i + 1]), Double.Parse(parms[i + 2]), Double.Parse(parms[i + 3]));
							}
						}
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			CarcassObjectModel car = (objectModel as CarcassObjectModel);
			if (car == null)
				throw new ObjectModelNotSupportedException();

			IO.Writer writer = Accessor.Writer;
			writer.WriteLine("$aseanimgrabinit");
			if (car.Scale != 1.0)
			{
				writer.WriteLine(String.Format("$scale {0}", car.Scale));
			}
			if (car.KeepMotionBone)
			{
				writer.WriteLine("$keepmotion");
			}
			if (car.GLAFileName != null)
			{
				writer.Write("$aseanimref_gla ");
				writer.WriteLine(car.GLAFileName);
			}
			for (int i = 0; i < car.PCJ.Count; i++)
			{
				writer.WriteLine(String.Format("$pcj {0}", car.PCJ[i]));
			}
			for (int i = 0; i < car.ModelReferences.Count; i++)
			{
				writer.Write(String.Format("$aseanimgrab {0}", car.ModelReferences[i].FileName));
				writer.Write(String.Format(" -loop {0}", car.ModelReferences[i].LoopCount));
				if (car.ModelReferences[i].EnumName != null)
				{
					writer.Write(String.Format(" -enum {0}", car.ModelReferences[i].EnumName));
				}
				if ((!(car.ModelReferences[i].FileName?.ToLower()?.EndsWith(".xsi")).GetValueOrDefault() && car.ModelReferences[i].FrameSpeed != ModelReference.DEFAULT_FRAMESPEED) ||
					((car.ModelReferences[i].FileName?.ToLower()?.EndsWith(".xsi")).GetValueOrDefault() && car.ModelReferences[i].FrameSpeed != ModelReference.DEFAULT_FRAMESPEED_XSI))
				{
					writer.Write(String.Format(" -framespeed {0}", car.ModelReferences[i].FrameSpeed));
				}
				if (car.ModelReferences[i].GenerateLoopFrame)
				{
					writer.Write(" -genloopframe");
				}

				if (i == 0)
				{
					if (car.UseLegacyCompression)
					{
						writer.Write("  -qdskipstart -prequat -qdskipstop");
					}
				}
				writer.WriteLine();
			}
			writer.WriteLine("$aseanimgrabfinalize");
			writer.Write(String.Format("$aseanimconvertmdx_noask {0}", car.BasePath));

			if (car.MakeSkin)
			{
				writer.Write(" -makeskin");
			}
			if (car.SmoothAllSurfaces)
			{
				writer.Write(" -smooth");
			}
			if (car.RemoveDuplicateVertices)
			{
				writer.Write(" -losedupverts");
			}
			if (car.SkeletonFileName != null)
			{
				writer.Write(" -makeskel ");
				writer.Write(car.SkeletonFileName);
			}

			if (car.Origin != PositionVector3.Empty)
			{
				writer.Write(String.Format(" -origin {0} {1} {2}", car.Origin.X, car.Origin.Y, car.Origin.Z));
			}
		}
	}
}
