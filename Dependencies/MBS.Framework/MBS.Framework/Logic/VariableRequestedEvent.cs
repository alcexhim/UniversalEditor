using System;
namespace MBS.Framework.Logic
{
	public class VariableRequestedEventArgs : EventArgs
	{
		public bool Handled { get; set; } = false;
		public string VariableName { get; set; } = null;
		public object Value { get; set; } = null;

		public VariableRequestedEventArgs(string variableName)
		{
			VariableName = variableName;
		}
	}
	public delegate void VariableRequestedEventHandler(object sender, VariableRequestedEventArgs e);
}
