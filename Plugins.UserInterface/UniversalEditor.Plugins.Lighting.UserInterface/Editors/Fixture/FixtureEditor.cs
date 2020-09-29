//
//  FixtureEditor.cs
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.Lighting.Fixture;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Lighting.UserInterface.Editors
{
	[ContainerLayout(typeof(FixtureEditor), "UniversalEditor.Plugins.Lighting.UserInterface.Editors.Fixture.FixtureEditor.glade")]
	public class FixtureEditor : Editor
	{
		private ComboBox cboManufacturer;
		private TextBox txtModel;
		private ComboBox cboType;
		private TextBox txtAuthor;
		private Button cmdPhysicalAttributes;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(FixtureObjectModel));
			}
			return _er;
		}

		[EventHandler(nameof(cboManufacturer), "Changed")]
		private void cboManufacturer_Changed(object sender, EventArgs e)
		{
			FixtureObjectModel fixt = (ObjectModel as FixtureObjectModel);
			if (fixt == null) return;

			BeginEdit();
			fixt.Manufacturer = cboManufacturer.Text;
			EndEdit();
		}

		[EventHandler(nameof(txtModel), "Changed")]
		private void txtModel_Changed(object sender, EventArgs e)
		{
			FixtureObjectModel fixt = (ObjectModel as FixtureObjectModel);
			if (fixt == null) return;

			BeginEdit();
			fixt.Model = txtModel.Text;
			EndEdit();
		}

		[EventHandler(nameof(cboType), "Changed")]
		private void cboType_Changed(object sender, EventArgs e)
		{
			FixtureObjectModel fixt = (ObjectModel as FixtureObjectModel);
			if (fixt == null) return;

			BeginEdit();
			fixt.Type = cboType.Text;
			EndEdit();
		}

		[EventHandler(nameof(cmdPhysicalAttributes), "Click")]
		private void cmdPhysicalAttributes_Click(object sender, EventArgs e)
		{
			SettingsDialog dlg = new SettingsDialog();
			dlg.EnableProfiles = false;
			dlg.Text = "Edit Physical Attributes";
			dlg.SettingsProviders.Clear();

			dlg.SettingsProviders.Add(new CustomSettingsProvider(new SettingsGroup[]
			{
				new SettingsGroup(new string[] { "General" }, new Setting[]
				{
					new GroupSetting("Bulb", "Bulb", new Setting[]
					{
						new ChoiceSetting("BulbType", "Type", null, new ChoiceSetting.ChoiceSettingValue[]
						{
							new ChoiceSetting.ChoiceSettingValue("CDM70W", "CDM 70W", "CDM70W"),
							new ChoiceSetting.ChoiceSettingValue("LED", "LED", "LED")
						}),
						new RangeSetting("BulbIntensity", "Lumens", 0.0M, 0.0M, 999999.0M),
						new ChoiceSetting("ColorTemperature", "Color temperature", null, new ChoiceSetting.ChoiceSettingValue[]
						{
							new ChoiceSetting.ChoiceSettingValue("CT2000", "2000", 2000),
							new ChoiceSetting.ChoiceSettingValue("CT9300", "9300", 9300)
						})
					}),
					new GroupSetting("Lens", "Lens", new Setting[]
					{
						new ChoiceSetting("LensType", "Type", null, new ChoiceSetting.ChoiceSettingValue[]
						{
							new ChoiceSetting.ChoiceSettingValue("Other", "Other", "Other"),
							new ChoiceSetting.ChoiceSettingValue("PC", "PC", "PC"),
							new ChoiceSetting.ChoiceSettingValue("Fresnel", "Fresnel", "Fresnel")
						}),
						new RangeSetting("LensMinimum", "Minimum (degrees)", 0.0M, 0.0M, 99.9M),
						new RangeSetting("LensMaximum", "Maximum (degrees)", 0.0M, 0.0M, 99.9M)
					}),
					new GroupSetting("Heads", "Heads", new Setting[]
					{
						new ChoiceSetting("HeadsType", "Type", null, new ChoiceSetting.ChoiceSettingValue[]
						{
							new ChoiceSetting.ChoiceSettingValue("Fixed", "Fixed", "Fixed"),
							new ChoiceSetting.ChoiceSettingValue("Head", "Head", "Head"),
							new ChoiceSetting.ChoiceSettingValue("Mirror", "Mirror", "Mirror"),
							new ChoiceSetting.ChoiceSettingValue("Barrel", "Barrel", "Barrel")
						}),
						new RangeSetting("PanMaxDegrees", "Maximum pan (degrees)", 0.0M, 0.0M, 999.0M),
						new RangeSetting("TiltMaxDegrees", "Maximum tilt (degrees)", 0.0M, 0.0M, 999.0M),
						new RangeSetting("PanMaxDegrees", "Layout columns", 1.0M, 1.0M, 99.0M),
						new RangeSetting("PanMaxDegrees", "Layout rows", 1.0M, 1.0M, 99.0M)
					}),
					new GroupSetting("Electrical", "Electrical", new Setting[]
					{
						new RangeSetting("PowerConsumption", "Power consumption (watts)", 0.0M, 0.0M, 99999.0M),
						new ChoiceSetting("DMXConnector", "DMX connector type", null, new ChoiceSetting.ChoiceSettingValue[]
						{
							new ChoiceSetting.ChoiceSettingValue("DMX3Pin", "3-pin", "DMX3Pin"),
							new ChoiceSetting.ChoiceSettingValue("DMX5Pin", "5-pin", "DMX5Pin"),
							new ChoiceSetting.ChoiceSettingValue("DMX3Pin5Pin", "3-pin and 5-pin", "DMX3Pin5Pin"),
							new ChoiceSetting.ChoiceSettingValue("DMX3PinIP65", "3-pin IP65", "DMX3PinIP65"),
							new ChoiceSetting.ChoiceSettingValue("DMX5PinIP65", "5-pin IP65", "DMX5PinIP65"),
							new ChoiceSetting.ChoiceSettingValue("DMX35mmStereoJack", "3.5mm stereo jack", "DMX35mmStereoJack"),
							new ChoiceSetting.ChoiceSettingValue("DMXOther", "Other", "DMXOther")
						}),
					}),
					new GroupSetting("Dimensions", "Dimensions", new Setting[]
					{
						new RangeSetting("Weight", "Weight (kg)", 0.0M, 0.0M, 999.99M),
						new RangeSetting("Width", "Width (mm)", 0.0M, 0.0M, 9999.0M),
						new RangeSetting("Height", "Height (mm)", 0.0M, 0.0M, 9999.0M),
						new RangeSetting("Depth", "Depth e(mm)", 0.0M, 0.0M, 9999.0M)
					})
				})
			}));

			if (dlg.ShowDialog() == DialogResult.OK)
			{

			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			FixtureObjectModel fixt = (ObjectModel as FixtureObjectModel);
			if (fixt == null) return;

			cboType.Text = fixt.Type;
			txtModel.Text = fixt.Model;
			// txtAuthor.Text = fixt.Author;
			cboManufacturer.Text = fixt.Manufacturer;
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
