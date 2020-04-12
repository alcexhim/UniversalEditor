//
//  CodeDataFormat.cs - the abstract base class from which all code file parsing implementations derive
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
using System.Text;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

using MBS.Framework.Collections.Generic;

using UniversalEditor.DataFormats.Text.Plain;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.SourceCode
{
	/// <summary>
	/// The abstract base class from which all code file parsing implementations derive.
	/// </summary>
	public abstract class CodeDataFormat : PlainTextDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(CodeObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PlainTextObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PlainTextObjectModel ptom = (objectModels.Pop() as PlainTextObjectModel);

			CodeObjectModel code = (objectModels.Pop() as CodeObjectModel);
			if (code == null) throw new ObjectModelNotSupportedException();

			Reader tr = base.Accessor.Reader;

			string nextToken = String.Empty;
			while (!tr.EndOfStream)
			{
				nextToken += tr.ReadChar();
				ProcessToken(nextToken, tr);
				if (!mvarInhibitTokenProcessing)
				{
					nextToken = String.Empty;
				}
			}

			List<CodeElement> ces = (List<CodeElement>)TemporaryVariables["CurrentElements"];
			foreach (CodeElement el in ces)
			{
				code.Elements.Add(el);
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			CodeObjectModel code = (objectModels.Pop() as CodeObjectModel);
			if (code == null) throw new ObjectModelNotSupportedException();

			PlainTextObjectModel ptom = new PlainTextObjectModel();

			Writer tw = base.Accessor.Writer;

			foreach (CodeElement element in code.Elements)
			{
				tw.Write(GenerateCode(element));
				if (code.Elements.IndexOf(element) < code.Elements.Count - 1)
				{
					tw.WriteLine();
				}
			}

			tw.Flush();
			objectModels.Push(ptom);
		}

		protected internal string GenerateCode(object obj)
		{
			return GenerateCode(obj, 0);
		}
		protected internal abstract string GenerateCode(object obj, int indentCount);

		private string mvarIndentString = "\t";
		public string IndentString
		{
			get { return mvarIndentString; }
		}
		protected string GetIndentString(int indentCount)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < indentCount; i++)
			{
				sb.Append(mvarIndentString);
			}
			return sb.ToString();
		}

		public string MakeFriendlyDataType(string DataType)
		{
			if (String.IsNullOrEmpty (DataType))
				DataType = "System.Void";

			return MakeFriendlyDataTypeInternal (DataType);
		}
		protected virtual string MakeFriendlyDataTypeInternal(string DataType)
		{
			return DataType;
		}

		protected virtual string MakeKnownDataType(string DataType)
		{
			return DataType;
		}

		protected virtual string ExpressionToString(object expr)
		{
			return expr.ToString();
		}
		protected CodeElementReference StringToExpression(string expr)
		{
			CodeDataType dummy = CodeDataType.Object;
			return StringToExpression(expr, out dummy);
		}
		protected virtual CodeElementReference StringToExpression(string expr, out CodeDataType dataType)
		{
			dataType = CodeDataType.String;
			return new CodeElementReference(new CodeLiteralElement(expr));
		}

		private bool mvarInhibitTokenProcessing = false;
		protected bool InhibitTokenProcessing { get { return mvarInhibitTokenProcessing; } set { mvarInhibitTokenProcessing = value; } }

		private AutoDictionary<string, object> mvarTemporaryVariables = new AutoDictionary<string, object>();
		protected AutoDictionary<string, object> TemporaryVariables { get { return mvarTemporaryVariables; } }

		protected virtual void ProcessToken(string token, Reader tr)
		{
		}
	}
}
