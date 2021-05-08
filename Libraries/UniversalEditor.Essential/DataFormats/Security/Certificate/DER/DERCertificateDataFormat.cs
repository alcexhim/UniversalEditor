//
//  DERCertificateDataFormat.cs - provides a DataFormat for manipulating security certificates in Distinguished Encoding Rules (DER) format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;

using UniversalEditor.ObjectModels.AbstractSyntax;
using UniversalEditor.DataFormats.AbstractSyntax.DER;

using UniversalEditor.ObjectModels.Security.Certificate;

namespace UniversalEditor.DataFormats.Security.Certificate.DER
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating security certificates in Distinguished Encoding Rules (DER) format.
	/// </summary>
	public class DERCertificateDataFormat : DERDataFormat
	{
		private static DataFormatReference _dfr;
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
