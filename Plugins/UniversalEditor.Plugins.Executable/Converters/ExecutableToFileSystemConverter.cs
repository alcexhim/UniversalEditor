using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Converters
{
	public class ExecutableToFileSystemConverter : Converter
	{
		private static ConverterReference _cr = null;
		public override ConverterReference MakeReference()
		{
			if (_cr == null)
			{
				_cr = base.MakeReference();
				_cr.Capabilities.Add(typeof(FileSystemObjectModel), typeof(ExecutableObjectModel));
				_cr.Capabilities.Add(typeof(ExecutableObjectModel), typeof(FileSystemObjectModel));
			}
			return _cr;
		}
		public override void Convert(ObjectModel from, ObjectModel to)
		{
			if (from is ExecutableObjectModel && to is FileSystemObjectModel)
			{
				ExecutableObjectModel exe = (from as ExecutableObjectModel);
				FileSystemObjectModel fsom = (to as FileSystemObjectModel);
				foreach (ExecutableSection section in exe.Sections)
				{
					fsom.Files.Add(section.Name, section.Data);
				}

				// TODO: load resources (?)
				return;
			}
			else if (from is FileSystemObjectModel && to is ExecutableObjectModel)
			{
				FileSystemObjectModel fsom = (from as FileSystemObjectModel);
				ExecutableObjectModel exe = (to as ExecutableObjectModel);

				foreach (File file in fsom.Files)
				{
					ExecutableSection section = new ExecutableSection();
					section.Name = file.Name;
					section.Data = file.GetDataAsByteArray();
					exe.Sections.Add(section);
				}
				return;
			}
			throw new ObjectModelNotSupportedException();
		}
	}
}
