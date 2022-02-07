//
//  MultiTrackMappingDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using System.Linq;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Dialogs
{
	[ContainerLayout("~/Editors/Multimedia/Audio/Waveform/Dialogs/MultiTrackMappingDialog.glade")]
	public class MultiTrackMappingDialog : CustomDialog
	{
		private ComboBox cboChannelPresets;
		private ListViewControl lvChannels;
		private NumericTextBox txtChannelCount;
		private Label lblChannelCount;

		private string[][] knownChannelNames = new string[][]
		{
			new string[] { "Mono" },
			new string[] { "Left", "Right" },
			new string[] { "Front left", "Front right", "Center", "Rear left", "Rear right" },
			new string[] { "Front left", "Front right", "Center", "Rear left", "Rear right" }
		};

		public Editor Editor { get; internal set; } = null;

		private class MultiTrackMappingChannelPreset
		{
			public string Title { get; }
			public List<MultiTrackMappingChannel> Channels { get; } = new List<MultiTrackMappingChannel>();

			public MultiTrackMappingChannelPreset(string title, IEnumerable<MultiTrackMappingChannel> list)
			{
				Title = title;
				Channels.AddRange(list);
			}
		}
		private class MultiTrackMappingChannel
		{
			public Guid ID { get; } = Guid.Empty;
			public string Title { get; } = null;
			public MBS.Framework.Drawing.Color Color { get; set; } = MBS.Framework.Drawing.Color.Empty;

			public MultiTrackMappingChannel(Guid id, string title)
			{
				ID = id;
				Title = title;
			}
		}

		private List<MultiTrackMappingChannelPreset> channelPresets = new List<MultiTrackMappingChannelPreset>();

		private TreeModelRow rowCustom = null;
		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			((DefaultTreeModel)cboChannelPresets.Model).Rows.Clear();

			EditorReference _er = Editor?.MakeReference();
			if (_er != null)
			{
				if (_er.Configuration.Elements["MultiTrackMapping"] is MarkupTagElement tagMultiTrackMapping)
				{
					if (tagMultiTrackMapping.Elements["ChannelPresets"] is MarkupTagElement tagChannelPresets)
					{
						foreach (MarkupTagElement tagChannelPreset in tagChannelPresets.Elements.OfType<MarkupTagElement>())
						{
							MarkupAttribute attTitle = tagChannelPreset.Attributes["Title"];
							string title = attTitle?.Value;

							List<MultiTrackMappingChannel> channels = new List<MultiTrackMappingChannel>();

							if (tagChannelPreset.Elements["Channels"] is MarkupTagElement tagChannels)
							{
								foreach (MarkupTagElement tagChannel in tagChannels.Elements.OfType<MarkupTagElement>())
								{
									MultiTrackMappingChannel channel = new MultiTrackMappingChannel(new Guid(tagChannel.Attributes["ID"].Value), tagChannel.Attributes["Title"].Value);
									string colorName = tagChannel.Attributes["Color"]?.Value;
									if (colorName != null)
									{
										channel.Color = MBS.Framework.Drawing.Color.Parse(colorName);
									}
									channels.Add(channel);
								}
							}

							MultiTrackMappingChannelPreset preset = new MultiTrackMappingChannelPreset(title, channels);
							channelPresets.Add(preset);

							TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
							{
								new TreeModelRowColumn(cboChannelPresets.Model.Columns[0], title),
								new TreeModelRowColumn(cboChannelPresets.Model.Columns[1], channels.Count)
							});
							row.SetExtraData("preset", preset);

							((DefaultTreeModel)cboChannelPresets.Model).Rows.Add(row);
						}
					}
				}
			}

			rowCustom = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(cboChannelPresets.Model.Columns[0], "(custom)")
			});
			((DefaultTreeModel)cboChannelPresets.Model).Rows.Add(rowCustom);

			cboChannelPresets.SelectedItem = rowCustom;
		}

		[EventHandler(nameof(cboChannelPresets), nameof(ComboBox.Changed))]
		private void cboChannelPresets_Changed(object sender, EventArgs e)
		{
			inhibitChannelChange = true;
			MultiTrackMappingChannelPreset preset = cboChannelPresets.SelectedItem?.GetExtraData<MultiTrackMappingChannelPreset>("preset");
			if (preset != null)
			{
				lblChannelCount.Visible = false;
				txtChannelCount.Visible = false;
				txtChannelCount.Value = preset.Channels.Count;
			}
			else
			{
				lblChannelCount.Visible = true;
				txtChannelCount.Visible = true;
			}
			inhibitChannelChange = false;

			UpdateChannels();
		}

		private bool inhibitChannelChange = false;

		[EventHandler(nameof(txtChannelCount), nameof(NumericTextBox.Changed))]
		private void txtChannelCount_Changed(object sender, EventArgs e)
		{
			if (!inhibitChannelChange)
			{
				cboChannelPresets.SelectedItem = rowCustom;
			}
			UpdateChannels();
		}

		private void UpdateChannels()
		{
			MultiTrackMappingChannelPreset preset = cboChannelPresets.SelectedItem?.GetExtraData<MultiTrackMappingChannelPreset>("preset");

			lvChannels.Model.Rows.Clear();
			for (int i = 0; i < txtChannelCount.Value; i++)
			{
				string channelName = String.Format("Channel {0}", (i + 1));
				if (preset != null)
				{
					channelName = preset.Channels[i].Title;
				}

				MBS.Framework.UserInterface.Drawing.Image channelColorImage = null;
				if (preset != null)
				{
					channelColorImage = MBS.Framework.UserInterface.Drawing.Image.Create(24, 24);
					MBS.Framework.UserInterface.Drawing.Graphics g = MBS.Framework.UserInterface.Drawing.Graphics.FromImage(channelColorImage);
					g.Clear(preset.Channels[i].Color);
				}

				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(lvChannels.Model.Columns[0], String.Format("Track {0}", (i + 1))),
					new TreeModelRowColumn(lvChannels.Model.Columns[1], channelName),
					new TreeModelRowColumn(lvChannels.Model.Columns[2], channelColorImage)
				});
				lvChannels.Model.Rows.Add(row);
			}
		}
	}
}
