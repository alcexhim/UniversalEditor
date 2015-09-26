using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.AbstractSyntax;

namespace UniversalEditor.DataFormats.AbstractSyntax.DER
{
	public class DERDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(AbstractSyntaxObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			AbstractSyntaxObjectModel asn = (objectModel as AbstractSyntaxObjectModel);
			if (asn == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				byte identifier = reader.ReadByte();
				byte tagClass = (byte)identifier.GetBits(7, 2);
				byte primitiveOrConstructed = (byte)identifier.GetBits(6, 1);
				byte tagNumber = (byte)identifier.GetBits(0, 5);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
