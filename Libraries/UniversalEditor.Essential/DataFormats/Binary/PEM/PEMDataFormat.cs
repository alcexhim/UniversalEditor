//
//  PEMDataFormat.cs - provides a DataFormat for manipulating data in Privacy-Enhanced Mail (PEM) encoding
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

using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Text.Plain;
using UniversalEditor.ObjectModels.Binary;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.Binary.PEM
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating data in Privacy-Enhanced Mail (PEM) encoding.
	/// </summary>
	public class PEMDataFormat : PlainTextDataFormat
	{
		public string Signature { get; set; } = null;
		public int MaximumLineLength { get; set; } = 64;

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(typeof(PEMDataFormat));
				_dfr.Capabilities.Add(typeof(BinaryObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PlainTextObjectModel), DataFormatCapabilities.All);
				_dfr.Title = "Privacy-Enhanced Mail encoding";
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Signature), "_Signature"));
			}
			return _dfr;
		}

		/// <summary>
		/// Method called BEFORE the LoadInternal method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a
		/// <see cref="DataFormat" /> subclass (e.g. XMLDataFormat), you need to create a new instance of the appropriate <see cref="ObjectModel" /> that the
		/// subclass expects, and push that onto the <paramref name="objectModels"/> stack, i.e. <code>objectModels.Push(new MarkupObjectModel());</code> This is
		/// usually the only line of code in the overridden <see cref="BeforeLoadInternal" /> method's body.
		/// </remarks>
		/// <example>
		/// objectModels.Push(new BaseObjectModel()); // this is all we need to do
		/// </example>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PlainTextObjectModel());
		}
		/// <summary>
		/// Method called AFTER the LoadInternal method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a <see cref="DataFormat" /> subclass (e.g. XMLDataFormat), you need to first pop the <see cref="ObjectModel" /> that you pushed
		/// onto the <paramref name="objectModels"/> stack in your <see cref="BeforeLoadInternal" /> implementation, then pop the <see cref="ObjectModel" /> that
		/// your class expects to get passed. Now you can read data from the original <see cref="ObjectModel" /> and modify the second <see cref="ObjectModel" />.
		/// Because these objects are passed by reference, you do not need to push them back onto the stack for them to get properly loaded.
		/// </remarks>
		/// <example>
		/// BaseObjectModel bom = (objectModels.Pop() as BaseObjectModel); // base object model comes first
		/// MyVerySpecificObjectModel myOM = (objectModels.Pop() as MyVerySpecificObjectModel);
		/// 
		/// // populate MyVerySpecificObjectModel... and we're done. nothing else needs to be pushed back onto the stack.
		/// </example>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PlainTextObjectModel ptom = (objectModels.Pop() as PlainTextObjectModel);

			ObjectModel om = objectModels.Pop();

			if (!ptom.Lines[0].StartsWith("-----", StringComparison.OrdinalIgnoreCase) && ptom.Lines[0].EndsWith("-----", StringComparison.OrdinalIgnoreCase))
				throw new InvalidDataFormatException("file does not begin with a -----{SIGNATURE}----- line");

			if (!ptom.Lines[ptom.Lines.Count - 1].StartsWith("-----", StringComparison.OrdinalIgnoreCase) && ptom.Lines[ptom.Lines.Count - 1].EndsWith("-----", StringComparison.OrdinalIgnoreCase))
				throw new InvalidDataFormatException("file does not end with a -----{SIGNATURE}----- line");

			string beginSignature = ptom.Lines[0].Substring(5 + 6, ptom.Lines[0].Length - 10 - 6);
			string endSignature = ptom.Lines[ptom.Lines.Count - 1].Substring(5 + 4, ptom.Lines[ptom.Lines.Count - 1].Length - 10 - 4);
			if (!endSignature.Equals(beginSignature))
				throw new InvalidDataFormatException(String.Format("begin signature '{0}' and end signature '{1}' do not match", beginSignature, endSignature));

			Signature = beginSignature;

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 1; i < ptom.Lines.Count - 1; i++)
			{
				sb.Append(ptom.Lines[i]);
			}

			byte[] data = Convert.FromBase64String(sb.ToString());

			if (om is BinaryObjectModel)
			{
				(om as BinaryObjectModel).Data = data;
			}
			else if (om is PlainTextObjectModel)
			{
				(om as PlainTextObjectModel).Text = System.Text.Encoding.UTF8.GetString(data);
			}
			else
			{
				throw new ObjectModelNotSupportedException();
			}
		}

		/// <summary>
		/// Method called BEFORE the SaveInternal method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a <see cref="DataFormat" /> subclass (e.g. XMLDataFormat), you need to first pop the <see cref="ObjectModel" /> that your class
		/// expects to get passed, then create a new instance of the proper type of <see cref="ObjectModel" /> the base class is expecting. Now you can retrieve
		/// data from the <see cref="ObjectModel" /> that your class expects and properly format it for the <see cref="ObjectModel" /> the base class expects.
		/// When you're done, you need to push the newly-created <see cref="ObjectModel" /> onto the stack so that the underlying SaveInternal
		/// method will be able to see it.
		/// </remarks>
		/// <example>
		/// MyVerySpecificObjectModel myOM = (objectModels.Pop() as MyVerySpecificObjectModel);
		/// BaseObjectModel bom = new BaseObjectModel();
		/// 
		/// // populate BaseObjectModel...
		/// 
		/// objectModels.Push(bom); // aaand we're done
		/// </example>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PlainTextObjectModel ptom = new PlainTextObjectModel();
			ptom.Lines.Add(String.Format("-----BEGIN{0}-----", (String.IsNullOrEmpty(Signature) ? String.Empty : " " + Signature.ToUpper())));

			byte[] data = null;

			ObjectModel om = objectModels.Pop();
			if (om is BinaryObjectModel)
			{
				data = (om as BinaryObjectModel).Data;
			}
			else if (om is PlainTextObjectModel)
			{
				data = System.Text.Encoding.UTF8.GetBytes((om as PlainTextObjectModel).Text);
			}
			else
			{
				throw new ObjectModelNotSupportedException();
			}

			string base64 = Convert.ToBase64String(data);

			for (int i = 0; i < base64.Length; i += MaximumLineLength)
			{
				ptom.Lines.Add(base64.Substring(i, Math.Min(base64.Length - i, MaximumLineLength)));
			}

			ptom.Lines.Add(String.Format("-----END{0}-----", (String.IsNullOrEmpty(Signature) ? String.Empty : " " + Signature.ToUpper())));

			objectModels.Push(ptom);
		}
	}
}
