using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.FileSystem.BPlus;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.ObjectModels.Help.Compiled;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp
{
	/// <summary>
	/// The Windows Help (WinHelp) HLP data format. Much of the framework for reading these files is contained in BPlusFileSystemDataFormat, since the container file format
	/// itself is remarkably generic (albeit unused, as far as I can tell, apart from WinHelp files), but WinHelp-specific stuff is here.
	/// </summary>
	public class HLPDataFormat : BPlusFileSystemDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new FileSystemObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			
			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			CompiledHelpObjectModel help = (objectModels.Pop() as CompiledHelpObjectModel);
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			CompiledHelpObjectModel help = (objectModels.Pop() as CompiledHelpObjectModel);
			FileSystemObjectModel fsom = new FileSystemObjectModel();

			objectModels.Push(fsom);
		}
	}
}
