//
//  VDrumKitEditor.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using System.Linq;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.Plugins.Roland.ObjectModels.VDrumKit;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Roland.UserInterface.Editors.VDrumKit
{
	[ContainerLayout("~/Editors/Roland/VDrumKit/VDrumKitEditor.glade")]
	public class VDrumKitEditor : Editor
	{
		private ListViewControl tvKits;
		private ComboBox cboModuleType;
		private ListViewControl lvPads;
		private ListViewControl tvChainKits;

		private Container ctChains;
		private Container ctKits;
		private Container ctPads;

		private ComboBox cboChain;

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(VDrumKitObjectModel));
				_er.ConfigurationLoaded += _er_ConfigurationLoaded;
			}
			return _er;
		}

		static VDrumConfig config = new VDrumConfig();
		static void _er_ConfigurationLoaded(object sender, EventArgs e)
		{
			EditorReference er = (EditorReference)sender;
			MarkupTagElement tagConfig = er.Configuration;

			lock (config)
			{
				MarkupTagElement tagModules = tagConfig.Elements["Modules"] as MarkupTagElement;
				if (tagModules != null)
				{
					foreach (MarkupTagElement tagModule in tagModules.Elements.OfType<MarkupTagElement>())
					{
						if (tagModule?.FullName != "Module") continue;

						MarkupAttribute attID = tagModule.Attributes["ID"];
						if (attID == null) continue;

						VDrumModule module = config.Modules[attID.Value];
						if (module == null)
						{
							module = new VDrumModule();
							module.ID = attID.Value;
							config.Modules.Add(module);
						}

						MarkupAttribute attTitle = tagModule.Attributes["Title"];
						if (attTitle != null) module.Title = attTitle.Value;

						MarkupTagElement tagPads = tagModule.Elements["Pads"] as MarkupTagElement;
						if (tagPads != null)
						{
							foreach (MarkupTagElement tagPad in tagPads.Elements.OfType<MarkupTagElement>())
							{
								if (tagPad?.FullName != "Pad") continue;

								MarkupAttribute attPadTitle = tagPad.Attributes["Title"];
								if (attPadTitle == null) continue;

								VDrumPad pad = new VDrumPad();
								pad.Title = attPadTitle.Value;
								module.Pads.Add(pad);
							}
						}

						MarkupTagElement tagSounds = tagModule.Elements["Sounds"] as MarkupTagElement;
						if (tagSounds != null)
						{
							foreach (MarkupTagElement tagSound in tagSounds.Elements.OfType<MarkupTagElement>())
							{
								if (tagSound?.FullName != "Sound") continue;

								MarkupAttribute attSoundIndex = tagSound.Attributes["Index"];
								MarkupAttribute attSoundTitle = tagSound.Attributes["Title"];
								MarkupAttribute attSoundCategoryID = tagSound.Attributes["CategoryID"];
								MarkupAttribute attSoundTypeID = tagSound.Attributes["SoundTypeID"];

								VDrumSound sound = new VDrumSound();
								sound.Index = Int32.Parse(attSoundIndex.Value);
								sound.Title = attSoundTitle?.Value;
								// sound.Category = ;
								// sound.Type = ;
								module.Sounds.Add(sound);
							}
						}
					}

				}
			}
		}


		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			((DefaultTreeModel)cboModuleType.Model).Rows.Clear();
			foreach (VDrumModule module in config.Modules)
			{
				TreeModelRow rowModule = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboModuleType.Model.Columns[0], module.Title)
				});
				((DefaultTreeModel)cboModuleType.Model).Rows.Add(rowModule);
			}

			OnObjectModelChanged(e);
		}

		[EventHandler(nameof(cboChain), nameof(ComboBox.Changed))]
		private void cboChain_Changed(object sender, EventArgs e)
		{
			tvChainKits.Model.Rows.Clear();

			DrumKitChain chain = cboChain.SelectedItem?.GetExtraData<DrumKitChain>("chain");
			if (chain == null) return;

			VDrumKitObjectModel vdk = (ObjectModel as VDrumKitObjectModel);

			foreach (DrumKit kit in chain.Kits)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvChainKits.Model.Columns[0], chain.Kits.IndexOf(kit)),
					new TreeModelRowColumn(tvChainKits.Model.Columns[1], vdk.DrumKits.IndexOf(kit)),
					new TreeModelRowColumn(tvChainKits.Model.Columns[2], kit.Name)
				});
				tvChainKits.Model.Rows.Add(row);
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DocumentExplorer.Nodes.Clear();

			if (!IsCreated) return;

			VDrumKitObjectModel vdk = (ObjectModel as VDrumKitObjectModel);
			if (vdk == null) return;

			TreeModelRow rowPad1 = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(lvPads.Model.Columns[0], "Icon Image"),
				new TreeModelRowColumn(lvPads.Model.Columns[1], MBS.Framework.UserInterface.Drawing.Image.FromStock(MBS.Framework.StockType.Floppy, 64))
			});
			lvPads.Model.Rows.Add(rowPad1);

			VDrumModule moduleForThisDoc = config.Modules["TD20"];

			foreach (DrumKitChain chain in vdk.Chains)
			{
				TreeModelRow rowChain = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(((DefaultTreeModel)cboChain.Model).Columns[0], chain.Name)
				});
				rowChain.SetExtraData<DrumKitChain>("chain", chain);
				((DefaultTreeModel)cboChain.Model).Rows.Add(rowChain);
			}

			foreach (DrumKit kit in vdk.DrumKits)
			{
				EditorDocumentExplorerNode nodeKit = new EditorDocumentExplorerNode(kit.Name);
				TreeModelRow rowKit = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvKits.Model.Columns[0], kit.Name)
				});

				for (int i = 0; i < kit.Instruments.Count; i++)
				{
					EditorDocumentExplorerNode nodeInst = new EditorDocumentExplorerNode(kit.Instruments[i].SampleFileName);
					nodeKit.Nodes.Add(nodeInst);

					if ((i % 2) == 0)
					{
						DrumKitInstrument inst = kit.Instruments[i];
						DrumKitInstrument instRim = kit.Instruments[i + 1];

						TreeModelRow rowInst = new TreeModelRow(new TreeModelRowColumn[]
						{
							new TreeModelRowColumn(tvKits.Model.Columns[0], moduleForThisDoc.Pads[(int)((double)i / 2)].Title),
							new TreeModelRowColumn(tvKits.Model.Columns[1], moduleForThisDoc.Sounds[(inst.SampleIndex + 1)]?.Title ?? String.Format("UNKNOWN ({0})", (inst.SampleIndex + 1))),
							new TreeModelRowColumn(tvKits.Model.Columns[2], moduleForThisDoc.Sounds[(instRim.SampleIndex + 1)]?.Title ?? String.Format("UNKNOWN ({0})", (instRim.SampleIndex + 1)))
						});
						rowKit.Rows.Add(rowInst);
					}
				}
				DocumentExplorer.Nodes.Add(nodeKit);


				tvKits.Model.Rows.Add(rowKit);
			}
		}
	}
}
