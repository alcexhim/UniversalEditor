using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.SourceCode
{
	public abstract class CodeDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(CodeObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CodeObjectModel code = (objectModel as CodeObjectModel);
			if (code == null) return;

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
		protected override void SaveInternal(ObjectModel objectModel)
		{
			CodeObjectModel code = (objectModel as CodeObjectModel);
			if (code == null) return;

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

		private UniversalEditor.Collections.Generic.AutoDictionary<string, object> mvarTemporaryVariables = new UniversalEditor.Collections.Generic.AutoDictionary<string, object>();
		protected UniversalEditor.Collections.Generic.AutoDictionary<string, object> TemporaryVariables { get { return mvarTemporaryVariables; } }

		protected virtual void ProcessToken(string token, Reader tr)
		{
		}
	}
}
