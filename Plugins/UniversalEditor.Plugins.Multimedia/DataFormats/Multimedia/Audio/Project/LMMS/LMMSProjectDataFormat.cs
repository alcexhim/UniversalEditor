//
//  LMMSProjectDataFormat.cs - provides a DataFormat for manipulating Linux MultiMedia Studio (LMMS) project files
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

using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Audio.Project;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Project.LMMS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Linux MultiMedia Studio (LMMS) project files.
	/// </summary>
	public class LMMSProjectDataFormat : XMLDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(AudioProjectObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			MarkupObjectModel mom = new MarkupObjectModel();
			objectModels.Push(mom);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			AudioProjectObjectModel seq = objectModels.Pop() as AudioProjectObjectModel;
		}
	}
}
