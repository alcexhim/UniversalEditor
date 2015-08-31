using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Audio.VoicebankIndex.Vocaloid
{
	public struct DDIParameter
	{
		public string ParameterName;
		public string PhonemeName;
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (PhonemeName != null)
			{
				sb.Append(PhonemeName);
				sb.Append(":");
			}
			if (ParameterName != null) sb.Append(ParameterName);
			return sb.ToString();
		}
	}
}
