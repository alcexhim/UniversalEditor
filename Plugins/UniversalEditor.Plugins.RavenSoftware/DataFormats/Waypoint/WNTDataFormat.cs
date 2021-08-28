//
//  WNTDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Waypoint;

namespace UniversalEditor.DataFormats.Waypoint
{
	public class WNTDataFormat : DataFormat
	{
		public WNTDataFormat()
		{
		}

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(WaypointObjectModel), DataFormatCapabilities.All);
				_dfr.Title = "Raven Software WNT bot route waypoint data";
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaypointObjectModel waypoint = (objectModel as WaypointObjectModel);
			if (waypoint == null)
				throw new ObjectModelNotSupportedException();

			int lineidx = 0;
			while (!Accessor.Reader.EndOfStream)
			{
				lineidx++;

				string line = Accessor.Reader.ReadLine();
				string[] parts = line.Split(new char[] { ' ' });
				if (parts.Length < 9)
				{
					Console.WriteLine("ue: ravensoft: waypoint: line {0} invalid - does not contain at least 9 elements");
					continue;
				}

				if (!parts[3].StartsWith("("))
				{
					Console.WriteLine("ue: ravensoft: waypoint: line {0} invalid - part 3 does not start with '('");
					continue;
				}
				else
				{
					parts[3] = parts[3].Substring(1);
				}
				if (!parts[5].EndsWith(")"))
				{
					Console.WriteLine("ue: ravensoft: waypoint: line {0} invalid - part 3 does not start with '('");
					continue;
				}
				else
				{
					parts[5] = parts[5].Substring(0, parts[5].Length - 1);
				}

				if (parts[6] != "{")
				{
					Console.WriteLine("ue: ravensoft: waypoint: line {0} invalid - part 6 does not equal '{'");
					continue;
				}

				List<string> liststrs = new List<string>();
				for (int i = 7; i < parts.Length - 1; i++)
				{
					if (parts[i] == "}")
						break;

					liststrs.Add(parts[i]);
				}

				WaypointEntry entry = new WaypointEntry();
				entry.Index = Int32.Parse(parts[0]);
				entry.Type = (WaypointType)Int32.Parse(parts[1]);
				entry.A = Double.Parse(parts[2]);
				entry.X = Double.Parse(parts[3]);
				entry.Y = Double.Parse(parts[4]);
				entry.Z = Double.Parse(parts[5]);
				entry.Q = Double.Parse(parts[parts.Length - 1]);

				waypoint.Entries.Add(entry);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{

		}
	}
}
