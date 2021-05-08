using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace UniversalEditor.Compression.Common
{
	public class CompressionTracingSwitch : Switch
	{
		internal static CompressionTracingSwitch tracingSwitch = new CompressionTracingSwitch("CompressionSwitch", "Compression Library Tracing Switch");
		public static bool Informational
		{
			get
			{
				return CompressionTracingSwitch.tracingSwitch.SwitchSetting >= 1;
			}
		}
		public static bool Verbose
		{
			get
			{
				return CompressionTracingSwitch.tracingSwitch.SwitchSetting >= 2;
			}
		}
		internal CompressionTracingSwitch(string displayName, string description)
			: base(displayName, description)
		{
		}
	}
	public enum CompressionTracingSwitchLevel
	{
		Off,
		Informational,
		Verbose
	}
}
