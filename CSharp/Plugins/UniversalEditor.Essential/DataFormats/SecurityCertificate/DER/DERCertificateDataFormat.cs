using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.AbstractSyntax;
using UniversalEditor.DataFormats.AbstractSyntax.DER;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.SecurityCertificate;

namespace UniversalEditor.DataFormats.SecurityCertificate.DER
{
	public class DERCertificateDataFormat : DERDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(SecurityCertificateObjectModel), DataFormatCapabilities.All);
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
			SecurityCertificateObjectModel cer = (objectModels.Pop() as SecurityCertificateObjectModel);
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			SecurityCertificateObjectModel cer = (objectModels.Pop() as SecurityCertificateObjectModel);
			AbstractSyntaxObjectModel asn = new AbstractSyntaxObjectModel();

			objectModels.Push(asn);
		}
	}
}
