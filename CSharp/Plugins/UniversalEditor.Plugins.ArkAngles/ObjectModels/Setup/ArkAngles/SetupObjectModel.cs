using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles
{
	public class SetupObjectModel : ObjectModel
	{
		private string mvarCatalogExecutableFileName = String.Empty;
		/// <summary>
		/// The file name of the catalog executable to launch via the "Catalog" button. If this value is empty, the "Catalog" button is not displayed.
		/// </summary>
		public string CatalogExecutableFileName { get { return mvarCatalogExecutableFileName; } set { mvarCatalogExecutableFileName = value; } }

		private string mvarDocumentationFileName = String.Empty;
		/// <summary>
		/// 
		/// </summary>
		public string DocumentationFileName { get { return mvarDocumentationFileName; } set { mvarDocumentationFileName = value; } }

		private string mvarFooterText = String.Empty;
		/// <summary>
		/// The text to display at the bottom of the installer background window.
		/// </summary>
		public string FooterText { get { return mvarFooterText; } set { mvarFooterText = value; } }
		
		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}
	}
}
