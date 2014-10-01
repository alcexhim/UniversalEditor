using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Web;

namespace UniversalEditor.Controls.Web.StyleSheet.MeasurementUpDown
{
	public partial class MeasurementUpDownControl : UserControl
	{
		private List<MeasurementUnit> mvarMeasurementUnits = new List<MeasurementUnit>();
		private Dictionary<MeasurementUnit, string> mvarMeasurementUnitsAndNames = new Dictionary<MeasurementUnit, string>();
		private Dictionary<MeasurementUnit, int> mvarMeasurementUnitIndices = new Dictionary<MeasurementUnit, int>();

		public event EventHandler UnitChanged;
		protected virtual void OnUnitChanged(EventArgs e)
		{
			if (UnitChanged != null) UnitChanged(this, e);
		}
		public event EventHandler ValueChanged;
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (ValueChanged != null) ValueChanged(this, e);
		}

		public decimal MinimumValue { get { return txt.Minimum; } set { txt.Minimum = value; } }
		public decimal MaximumValue { get { return txt.Maximum; } set { txt.Maximum = value; } }

		public MeasurementUpDownControl()
		{
			InitializeComponent();
			
			RegisterMeasurementUnitAndName(MeasurementUnit.Pixel, "px");
			RegisterMeasurementUnitAndName(MeasurementUnit.Point, "pt");
			RegisterMeasurementUnitAndName(MeasurementUnit.Inch, "in");
			RegisterMeasurementUnitAndName(MeasurementUnit.Cm, "cm");
			RegisterMeasurementUnitAndName(MeasurementUnit.Mm, "mm");
			RegisterMeasurementUnitAndName(MeasurementUnit.Pica, "pc");
			RegisterMeasurementUnitAndName(MeasurementUnit.Em, "em");
			RegisterMeasurementUnitAndName(MeasurementUnit.Ex, "ex");
			RegisterMeasurementUnitAndName(MeasurementUnit.Percentage, "%");

			cboUnit.Items.Clear();
			foreach (KeyValuePair<MeasurementUnit, string> kvp in mvarMeasurementUnitsAndNames)
			{
				cboUnit.Items.Add(kvp.Value);
			}
		}

		private void RegisterMeasurementUnitAndName(MeasurementUnit measurementUnit, string name)
		{
			mvarMeasurementUnits.Add(measurementUnit);
			mvarMeasurementUnitsAndNames.Add(measurementUnit, name);
			mvarMeasurementUnitIndices.Add(measurementUnit, mvarMeasurementUnitIndices.Count + 1);
		}

		public System.Windows.Forms.ComboBox.ObjectCollection Items
		{
			get { return cbo.Items; }
		}

		private void cbo_TextUpdate(object sender, EventArgs e)
		{
			base.Text = cbo.Text;
		}

		private bool txt_InhibitValueChanged = false;
		private void txt_ValueChanged(object sender, EventArgs e)
		{
			if (txt_InhibitValueChanged) return;
			cbo.Text = txt.Value.ToString();
			OnValueChanged(e);
		}

		private void cbo_SelectedIndexChanged(object sender, EventArgs e)
		{
			int tryval = 0;
			if (Int32.TryParse(cbo.Text, out tryval))
			{
				txt_InhibitValueChanged = true;
				txt.Value = tryval;
				txt_InhibitValueChanged = false;

				OnValueChanged(e);
			}
		}

		public Measurement Value
		{
			get
			{
				MeasurementUnit unit = mvarMeasurementUnits[cboUnit.SelectedIndex];
				return new Measurement((double)txt.Value, unit);
			}
			set
			{
				cboUnit.SelectedIndex = mvarMeasurementUnitIndices[value.Unit];
				txt.Value = (decimal)value.Value;
			}
		}

		private void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnUnitChanged(e);
		}
	}
}
