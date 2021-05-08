//
//  SMDBaseDataFormat.cs - the base class for SMD format files
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

using UniversalEditor.ObjectModels.SMD;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.SMD
{
	/// <summary>
	/// The base class for SMD format files.
	/// </summary>
	public class SMDBaseDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SMDObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SMDObjectModel smd = (objectModel as SMDObjectModel);
			if (smd == null) throw new ObjectModelNotSupportedException();

			IO.Reader tr = base.Accessor.Reader;
			string version = tr.ReadLine();
			if (version != "version 1") throw new InvalidDataFormatException("File does not begin with \"version 1\"");

			string nextLine = String.Empty;
			while (!tr.EndOfStream)
			{
				SMDSection section = new SMDSection();
				section.Name = tr.ReadLine();
				while (!tr.EndOfStream)
				{
					nextLine = tr.ReadLine();
					if (nextLine == "end") break;

					section.Lines.Add(nextLine);
				}
				smd.Sections.Add(section);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
