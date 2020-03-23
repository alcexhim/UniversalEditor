using System;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(NumericTextBox))]
	public class NumericTextBoxImplementation : WindowsFormsNativeImplementation, MBS.Framework.UserInterface.Controls.Native.INumericTextBoxControlImplementation
	{
		public NumericTextBoxImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		public double GetMaximum()
		{
			return (double)(((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown)?.Maximum);
		}

		public double GetMinimum()
		{
			return (double)(((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown)?.Minimum);
		}

		public double GetStep()
		{
			return (double)(((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown)?.Increment);
		}

		public double GetValue()
		{
			return (double)(((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown)?.Value);
		}

		public void SetMaximum(double value)
		{
			System.Windows.Forms.NumericUpDown nud = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown);
			if (nud != null) nud.Maximum = (decimal)value;
		}

		public void SetMinimum(double value)
		{
			System.Windows.Forms.NumericUpDown nud = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown);
			if (nud != null) nud.Minimum = (decimal)value;
		}

		public void SetStep(double value)
		{
			System.Windows.Forms.NumericUpDown nud = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown);
			if (nud != null) nud.Increment = (decimal)value;
		}

		public void SetValue(double value)
		{
			System.Windows.Forms.NumericUpDown nud = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.NumericUpDown);
			if (nud != null) nud.Value = (decimal)value;
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			NumericTextBox ctl = (control as NumericTextBox);

			System.Windows.Forms.NumericUpDown txt = new System.Windows.Forms.NumericUpDown();
			txt.Maximum = (decimal) ctl.Maximum;
			txt.Minimum = (decimal)ctl.Minimum;
			txt.Value = (decimal)ctl.Value;

			return new WindowsFormsNativeControl(txt);
		}
	}
}
