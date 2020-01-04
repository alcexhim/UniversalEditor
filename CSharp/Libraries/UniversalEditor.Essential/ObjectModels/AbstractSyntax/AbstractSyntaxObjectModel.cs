using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.AbstractSyntax
{
	/// <summary>
	/// An <see cref="ObjectModel" /> representing data that follows the Abstract Syntax Notation One (ASN.1) specification.
	/// </summary>
	public class AbstractSyntaxObjectModel : ObjectModel
	{
		/// <summary>
		/// Clears all data from this <see cref="ObjectModel" /> and returns it to a pristine state.
		/// </summary>
		public override void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Copies all data from this <see cref="ObjectModel" /> to the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="where">The <see cref="ObjectModel" /> into which to copy the data of this <see cref="ObjectModel" />.</param>
		/// <exception cref="ObjectModelNotSupportedException">The conversion between this <see cref="ObjectModel" /> and the given <see cref="ObjectModel" /> is not supported.</exception>
		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		private static ObjectModelReference _omr = null;
		/// <summary>
		/// Creates a new <see cref="ObjectModelReference" /> containing the appropriate metadata for the <see cref="AbstractSyntaxObjectModel" /> and caches it, returning the cached instance.
		/// </summary>
		/// <returns>The <see cref="ObjectModelReference" /> that provides metadata and other information about this <see cref="AbstractSyntaxObjectModel" />.</returns>
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Abstract Syntax Notation";
			}
			return _omr;
		}
	}
}
