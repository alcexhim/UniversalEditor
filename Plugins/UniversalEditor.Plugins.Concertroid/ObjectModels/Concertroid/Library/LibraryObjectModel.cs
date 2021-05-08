//
//  LibraryObjectModel.cs - provides an ObjectModel for manipulating Concertroid asset libraries
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

namespace UniversalEditor.ObjectModels.Concertroid.Library
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Concertroid asset libraries.
	/// </summary>
	public class LibraryObjectModel : ObjectModel
	{
		/// <summary>
		/// Gets a collection of <see cref="Character" /> instances representing the characters included with this asset library.
		/// </summary>
		/// <value>The characters included with this asset library.</value>
		public Character.CharacterCollection Characters { get; } = new Character.CharacterCollection();
		/// <summary>
		/// Gets a collection of <see cref="Producer" /> instances representing the producers included with this asset library.
		/// </summary>
		/// <value>The producers included with this asset library.</value>
		public Producer.ProducerCollection Producers { get; } = new Producer.ProducerCollection();

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Concertroid", "Asset library" };
				_omr.Description = "A container to hold various Concertroid assets, such as songs, musicians, producers, characters, costumes, and animations.";
			}
			return _omr;
		}

		public override void Clear()
		{
			Characters.Clear();
			Producers.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			LibraryObjectModel clone = (where as LibraryObjectModel);
			foreach (Character chara in Characters)
			{
				clone.Characters.Add(chara.Clone() as Character);
			}
			foreach (Producer producer in Producers)
			{
				clone.Producers.Add(producer.Clone() as Producer);
			}
		}
	}
}
