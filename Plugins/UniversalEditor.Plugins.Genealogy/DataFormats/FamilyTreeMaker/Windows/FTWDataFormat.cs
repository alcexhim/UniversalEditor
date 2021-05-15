using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.FamilyTreeMaker.Windows
{
	public class FTWDataFormat : CompoundDocumentDataFormat
	{
		protected override void BeforeLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal (objectModels);
			objectModels.Push (new FileSystemObjectModel ());
		}
		protected override void AfterLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal (objectModels);

			FileSystemObjectModel fsom = (objectModels.Pop () as FileSystemObjectModel);

			File IND_DB = fsom.Files["IND.DB"];
			File INDGROUPS = fsom.Files["QEDIT0.DB"];

			if (IND_DB == null)
				throw new InvalidDataFormatException("IND.DB not found");

			INDDBObjectModel objm = IND_DB.GetObjectModel<INDDBObjectModel>(new INDDBDataFormat());

			int maxNameLength = 0;
			foreach (INDDBRecord item in objm.Items)
			{
				Console.WriteLine("{0}    {1}", item.name.PadRight(40, ' '), item.testdt.ToString());
				if (item.name.Length > maxNameLength) maxNameLength = item.name.Length;
			}

			Console.WriteLine();

			INDGROUPSObjectModel objm1 = INDGROUPS.GetObjectModel<INDGROUPSObjectModel>(new INDGROUPSDataFormat());

			foreach (INDGROUPSRecord rec in objm1.Items)
			{
				Console.WriteLine("{0}", rec.name);
			}

			FamilyTreeObjectModel ft = (objectModels.Pop () as FamilyTreeObjectModel);
		}
		protected override void BeforeSaveInternal (Stack<ObjectModel> objectModels)
		{
			FamilyTreeObjectModel ft = (objectModels.Pop() as FamilyTreeObjectModel);
			FileSystemObjectModel fsom = new FileSystemObjectModel();

			objectModels.Push(fsom);

			base.BeforeSaveInternal (objectModels);
		}
	}
}
