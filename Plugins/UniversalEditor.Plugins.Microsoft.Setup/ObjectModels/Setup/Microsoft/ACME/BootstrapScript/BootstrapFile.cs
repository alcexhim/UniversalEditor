//
//  BootstrapFile.cs - represents a file that is copied during the bootstrap process of a Microsoft ACME setup routine
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

namespace UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript
{
	/// <summary>
	/// Represents a file that is copied during the bootstrap process of a Microsoft ACME setup routine.
	/// </summary>
	public class BootstrapFile : ICloneable
	{
		public class BootstrapFileCollection
			: System.Collections.ObjectModel.Collection<BootstrapFile>
		{
			public BootstrapFile Add(string sourceFileName, string destinationFileName)
			{
				BootstrapFile item = new BootstrapFile();
				item.SourceFileName = sourceFileName;
				item.DestinationFileName = destinationFileName;
				Add(item);
				return item;
			}
		}

		private string mvarSourceFileName = String.Empty;
		public string SourceFileName { get {  return mvarSourceFileName; } set { mvarSourceFileName = value; } }

		private string mvarDestinationFileName = String.Empty;
		public string DestinationFileName { get { return mvarDestinationFileName; } set { mvarDestinationFileName = value; } }

		public object Clone()
		{
			BootstrapFile clone = new BootstrapFile();
			clone.SourceFileName = (mvarSourceFileName.Clone() as string);
			clone.DestinationFileName = (mvarDestinationFileName.Clone() as string);
			return clone;
		}
	}
}
