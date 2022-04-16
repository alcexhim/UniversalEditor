using System;
using System.Collections.Generic;

using UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.DataFormats.CompoundDocument;
using UniversalEditor.ObjectModels.CompoundDocument;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.FamilyTreeMaker.Windows
{
	public class FTWDataFormat : CompoundDocumentDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(FamilyTreeObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal (objectModels);
			objectModels.Push (new CompoundDocumentObjectModel ());
		}
		protected override void AfterLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal (objectModels);

			CompoundDocumentObjectModel fsom = (objectModels.Pop () as CompoundDocumentObjectModel);

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
			CompoundDocumentObjectModel fsom = new CompoundDocumentObjectModel();

			objectModels.Push(fsom);

			base.BeforeSaveInternal (objectModels);
		}
	}
}
