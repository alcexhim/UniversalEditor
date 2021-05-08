//
//  MyClass.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.Plugins.Blockchain.Bitcoin.ObjectModels;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Blockchain.UserInterface
{
	[ContainerLayout("~/Editors/Blockchain/BlockchainEditor.glade")]
	public class BlockchainEditor : Editor
	{
		private ListViewControl tv;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(BitcoinBlockchainObjectModel));
			}
			return _er;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			BitcoinBlockchainObjectModel bc = (ObjectModel as BitcoinBlockchainObjectModel);
			if (bc == null) return;

			if (!IsCreated) return;

			for (int i = 0; i < Math.Min(6500, bc.Blocks.Count); i++)
			{
				tv.Model.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tv.Model.Columns[0], bc.Blocks[i].Version.ToString()),
					new TreeModelRowColumn(tv.Model.Columns[1], bc.Blocks[i].PreviousBlockHash.ToString()),
					new TreeModelRowColumn(tv.Model.Columns[2], bc.Blocks[i].MerkelRoot.ToString()),
					new TreeModelRowColumn(tv.Model.Columns[3], bc.Blocks[i].Timestamp.ToString())
				}));
			}
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}
	}
}
