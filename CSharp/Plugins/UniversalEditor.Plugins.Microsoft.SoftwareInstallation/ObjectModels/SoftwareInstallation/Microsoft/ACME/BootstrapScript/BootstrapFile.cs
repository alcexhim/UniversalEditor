using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SoftwareInstallation.Microsoft.ACME.BootstrapScript
{
	public class BootstrapFile : ICloneable
	{
		public class BootstrapFileCollection
			: System.Collections.ObjectModel.Collection<BootstrapFile>
		{
			public BootstrapFile Add(string sourceFileName, string destinationFileName)
			{
				BootstrapFile item = new BootstrapFile();
				item.SourceFileName = sourceFileName;
				item.DestinationFileName = destinationFileName;
				Add(item);
				return item;
			}
		}

		private string mvarSourceFileName = String.Empty;
		public string SourceFileName { get {  return mvarSourceFileName; } set { mvarSourceFileName = value; } }

		private string mvarDestinationFileName = String.Empty;
		public string DestinationFileName { get { return mvarDestinationFileName; } set { mvarDestinationFileName = value; } }

		public object Clone()
		{
			BootstrapFile clone = new BootstrapFile();
			clone.SourceFileName = (mvarSourceFileName.Clone() as string);
			clone.DestinationFileName = (mvarDestinationFileName.Clone() as string);
			return clone;
		}
	}
}
