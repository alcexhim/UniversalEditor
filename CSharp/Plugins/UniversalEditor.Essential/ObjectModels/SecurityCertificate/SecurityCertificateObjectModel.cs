using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SecurityCertificate
{
	public class SecurityCertificateObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Security certificate";
				_omr.Path = new string[] { "Security", "Certificate" };
			}
			return _omr;
		}
		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
