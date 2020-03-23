using System;
namespace MBS.Framework.UserInterface.Controls
{
	namespace Native
	{
		public interface INumericTextBoxControlImplementation
		{
			void SetMinimum(double value);
			double GetMinimum();

			void SetMaximum(double value);
			double GetMaximum();

			void SetValue(double value);
			double GetValue();

			void SetStep(double value);
			double GetStep();
		}
	}
	public class NumericTextBox : SystemControl
	{
		public event EventHandler Changed;
		protected virtual void OnChanged(EventArgs e)
		{
			Changed?.Invoke(this, e);
		}
		
		private double _Minimum = 0.0;
		public double Minimum
		{
			get
			{
				if (IsCreated && ControlImplementation is Native.INumericTextBoxControlImplementation)
				{
					_Minimum = (ControlImplementation as Native.INumericTextBoxControlImplementation).GetMinimum();
				}
				return _Minimum;
			}
			set
			{
				_Minimum = value;
				(ControlImplementation as Native.INumericTextBoxControlImplementation)?.SetMinimum(value);
			}
		}
		private double _Maximum = 0.0;
		public double Maximum
		{
			get
			{
				if (IsCreated && ControlImplementation is Native.INumericTextBoxControlImplementation)
				{
					_Maximum = (ControlImplementation as Native.INumericTextBoxControlImplementation).GetMaximum();
				}
				return _Maximum;
			}
			set
			{
				_Maximum = value;
				(ControlImplementation as Native.INumericTextBoxControlImplementation)?.SetMaximum(value);
			}
		}
		private double _Step = 1.0;
		public double Step
		{
			get
			{
				if (IsCreated && ControlImplementation is Native.INumericTextBoxControlImplementation)
				{
					_Step = (ControlImplementation as Native.INumericTextBoxControlImplementation).GetStep();
				}
				return _Step;
			}
			set
			{
				_Step = value;
				(ControlImplementation as Native.INumericTextBoxControlImplementation)?.SetStep(value);
			}
		}

		private double _Value = 0.0;
		public double Value
		{
			get
			{
				if (IsCreated && ControlImplementation is Native.INumericTextBoxControlImplementation)
				{
					_Value = (ControlImplementation as Native.INumericTextBoxControlImplementation).GetValue();
				}
				return _Value;
			}
			set
			{
				_Value = value;
				(ControlImplementation as Native.INumericTextBoxControlImplementation)?.SetValue(value);
			}
		}

	}
}
