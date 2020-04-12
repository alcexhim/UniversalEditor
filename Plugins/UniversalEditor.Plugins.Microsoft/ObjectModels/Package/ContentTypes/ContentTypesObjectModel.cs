//
//  ContentTypesObjectModel.cs - provides an ObjectModel for manipulating Open Packaging Convention Content_Types.xml file
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

namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	/// <summary>
	/// Provides an ObjectModel for manipulating Open Packaging Convention Content_Types.xml file.
	/// </summary>
	public class ContentTypesObjectModel : ObjectModel
	{
		public DefaultDefinition.DefaultDefinitionCollection DefaultDefinitions { get; } = new DefaultDefinition.DefaultDefinitionCollection ();
		public OverrideDefinition.OverrideDefinitionCollection OverrideDefinitions { get; } = new OverrideDefinition.OverrideDefinitionCollection ();

		public ContentTypesObjectModel()
		{
			DefaultDefinitions.Add ("xml", "application/xml");
			DefaultDefinitions.Add ("rels", "application/vnd.openxmlformats-package.relationships+xml");
		}

		public override void Clear()
		{
			DefaultDefinitions.Clear();
			OverrideDefinitions.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ContentTypesObjectModel clone = (where as ContentTypesObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (DefaultDefinition item in DefaultDefinitions)
			{
				clone.DefaultDefinitions.Add(item.Clone() as DefaultDefinition);
			}
			foreach (OverrideDefinition item in OverrideDefinitions)
			{
				clone.OverrideDefinitions.Add(item.Clone() as OverrideDefinition);
			}
		}
	}
}
