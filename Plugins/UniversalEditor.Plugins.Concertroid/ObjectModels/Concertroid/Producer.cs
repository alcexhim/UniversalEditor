//
//  Producer.cs - represents a content creator associated with a particular Song, Character, Costume, or other concert asset
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

using UniversalEditor.ObjectModels.Concertroid.Library;

namespace UniversalEditor.ObjectModels.Concertroid
{
	/// <summary>
	/// Represents a content creator associated with a particular <see cref="Song" />, <see cref="Character" />, <see cref="Costume" />, or other
	/// concert asset.
	/// </summary>
	public class Producer : ICloneable
	{
		public class ProducerCollection
			: System.Collections.ObjectModel.Collection<Producer>
		{
		}

		private static Producer[] _list = null;
		public static Producer[] GetProducers()
		{
			if (_list == null)
			{
				List<string> ConfigurationPaths = new List<string>();
				ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					"Mike Becker's Software",
					"Concertroid"
				}));
				ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
					"Mike Becker's Software",
					"Concertroid"
				}));
				ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
					"Mike Becker's Software",
					"Concertroid"
				}));
				ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
					"..",
					"Configuration"
				}));

				List<Producer> list = new List<Producer>();
				foreach (string ConfigurationPath in ConfigurationPaths)
				{
					if (!System.IO.Directory.Exists(ConfigurationPath)) continue;

					string[] fileNames = System.IO.Directory.GetFiles(ConfigurationPath, "*.library", System.IO.SearchOption.AllDirectories);
					foreach (string fileName in fileNames)
					{
						LibraryObjectModel library = UniversalEditor.Common.Reflection.GetAvailableObjectModel<LibraryObjectModel>(fileName);
						foreach (Producer p in library.Producers)
						{
							list.Add(p);
						}
					}
				}
				_list = list.ToArray();
			}
			return _list;
		}

		/// <summary>
		/// Gets or sets the globally-unique identifier (GUID) associated with this <see cref="Producer" />.
		/// </summary>
		/// <value>The globally-unique identifier (GUID) associated with this <see cref="Producer" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the given name of this <see cref="Producer" />.
		/// </summary>
		/// <value>The given name of this <see cref="Producer" />.</value>
		public string FirstName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the family name of this <see cref="Producer" />.
		/// </summary>
		/// <value>The family name of this <see cref="Producer" />.</value>
		public string LastName { get; set; } = String.Empty;

		public object Clone()
		{
			Producer clone = new Producer();
			clone.ID = ID;
			clone.FirstName = (FirstName.Clone() as string);
			clone.LastName = (LastName.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return FirstName + " " + LastName;
		}
	}
}
