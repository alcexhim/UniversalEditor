using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.AbstractSyntax;
using UniversalEditor.DataFormats.AbstractSyntax.DER;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Security.Certificate;

namespace UniversalEditor.DataFormats.Security.Certificate.DER
{
	public class DERCertificateDataFormat : DERDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(CertificateObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new AbstractSyntaxObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			AbstractSyntaxObjectModel asn = (objectModels.Pop() as AbstractSyntaxObjectModel);
			CertificateObjectModel cer = (objectModels.Pop() as CertificateObjectModel);
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			CertificateObjectModel cer = (objectModels.Pop() as CertificateObjectModel);
			AbstractSyntaxObjectModel asn = new AbstractSyntaxObjectModel();

			objectModels.Push(asn);
		}
	}
}
