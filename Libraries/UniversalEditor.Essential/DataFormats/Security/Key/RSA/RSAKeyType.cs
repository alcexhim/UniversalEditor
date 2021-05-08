using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Security.Key.RSA
{
	public enum RSAKeyType : byte
	{
		Simple = 0x01,
		PublicKeyBlob = 0x06,
		PrivateKeyBlob = 0x07,
		PlainTextKeyBlob = 0x08,
		OpaqueKeyBlob = 0x09,
		PublicKeyBlobEx = 0x0A,
		SymmetricWrapKeyBlob = 0x0B
	}
}
