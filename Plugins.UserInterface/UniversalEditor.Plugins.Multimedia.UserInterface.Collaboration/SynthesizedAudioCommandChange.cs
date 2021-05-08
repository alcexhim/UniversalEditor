//
//  SynthesizedAudioCommandChange.cs
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.Plugins.Collaboration;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Collaboration
{
	public class SynthesizedAudioCommandChange
	{
		public class SynthesizedAudioCommandChangeCollection : Collection<SynthesizedAudioCommandChange>
		{
			private Dictionary<SynthesizedAudioCommand, SynthesizedAudioCommandChange> _itemsByCommand = new Dictionary<SynthesizedAudioCommand, SynthesizedAudioCommandChange>();
			public bool Contains(SynthesizedAudioCommand command)
			{
				return _itemsByCommand.ContainsKey(command);
			}

			public SynthesizedAudioCommandChange this[SynthesizedAudioCommand command]
			{
				get
				{
					if (_itemsByCommand.ContainsKey(command))
						return _itemsByCommand[command];
					return null;
				}
			}

			protected override void InsertItem(int index, SynthesizedAudioCommandChange item)
			{
				base.InsertItem(index, item);
				_itemsByCommand[item.Command] = item;
			}
			protected override void RemoveItem(int index)
			{
				_itemsByCommand.Remove(this[index].Command);
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByCommand.Clear();
			}

			public SynthesizedAudioCommandChange[] GetDeletions()
			{
				List<SynthesizedAudioCommandChange> list = new List<SynthesizedAudioCommandChange>();
				for (int i =0; i < Count; i++)
				{
					if (this[i].ChangeType == CollaborationChangeType.Deletion)
					{
						list.Add(this[i]);
					}
				}
				return list.ToArray();
			}
		}

		public SynthesizedAudioCommandChange(SynthesizedAudioCommand command, Rectangle bounds, CollaborationChangeType changeType)
		{
			this.Command = command;
			this.Bounds = bounds;
			this.ChangeType = changeType;
		}

		public SynthesizedAudioCommand Command { get; set; } = null;
		public Rectangle Bounds { get; set; }
		public CollaborationChangeType ChangeType { get; set; } = CollaborationChangeType.Unknown;
	}
}
