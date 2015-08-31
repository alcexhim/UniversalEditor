using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.DataFormats.Markup.XML
{
	public class XMLDataFormatSettings
	{
		private char mvarTagBeginChar = '<';
		private char mvarTagSpecialDeclarationStartChar = '!';
		private string mvarTagSpecialDeclarationCommentStart = "--";
		private char mvarTagEndChar = '>';
		private char mvarTagCloseChar = '/';
		private char mvarPreprocessorChar = '?';
		private char mvarTagNamespaceSeparatorChar = ':';
		private char mvarAttributeNameValueSeparatorChar = '=';
		private char mvarEntityBeginChar = '&';
		private char mvarEntityEndChar = ';';
		public char TagBeginChar
		{
			get
			{
				return this.mvarTagBeginChar;
			}
			set
			{
				this.mvarTagBeginChar = value;
			}
		}
		public char TagSpecialDeclarationStartChar
		{
			get
			{
				return this.mvarTagSpecialDeclarationStartChar;
			}
			set
			{
				this.mvarTagSpecialDeclarationStartChar = value;
			}
		}
		public string TagSpecialDeclarationCommentStart
		{
			get
			{
				return this.mvarTagSpecialDeclarationCommentStart;
			}
			set
			{
				this.mvarTagSpecialDeclarationCommentStart = value;
			}
		}
		public char TagEndChar
		{
			get
			{
				return this.mvarTagEndChar;
			}
			set
			{
				this.mvarTagEndChar = value;
			}
		}
		public char TagCloseChar
		{
			get
			{
				return this.mvarTagCloseChar;
			}
			set
			{
				this.mvarTagCloseChar = value;
			}
		}
		public char PreprocessorChar
		{
			get
			{
				return this.mvarPreprocessorChar;
			}
			set
			{
				this.mvarPreprocessorChar = value;
			}
		}
		public char TagNamespaceSeparatorChar
		{
			get
			{
				return this.mvarTagNamespaceSeparatorChar;
			}
			set
			{
				this.mvarTagNamespaceSeparatorChar = value;
			}
		}
		public char AttributeNameValueSeparatorChar
		{
			get
			{
				return this.mvarAttributeNameValueSeparatorChar;
			}
			set
			{
				this.mvarAttributeNameValueSeparatorChar = value;
			}
		}
		public char EntityBeginChar
		{
			get
			{
				return this.mvarEntityBeginChar;
			}
			set
			{
				this.mvarEntityBeginChar = value;
			}
		}
		public char EntityEndChar
		{
			get
			{
				return this.mvarEntityEndChar;
			}
			set
			{
				this.mvarEntityEndChar = value;
			}
		}

		private bool mvarIsStandalone = false;
		public bool IsStandalone { get { return mvarIsStandalone; } set { mvarIsStandalone = value; } }

		private System.Collections.Specialized.StringCollection mvarAutoCloseTagNames = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection AutoCloseTagNames { get { return mvarAutoCloseTagNames; } }

		private Collections.Generic.BidirectionalDictionary<string, string> mvarEntities = new Collections.Generic.BidirectionalDictionary<string, string>();
		public Collections.Generic.BidirectionalDictionary<string, string> Entities { get { return mvarEntities; } }

		private char mvarCDataBeginChar = '[';
		public char CDataBeginChar { get { return mvarCDataBeginChar; } set { mvarCDataBeginChar = value; } }
		private char mvarCDataEndChar = ']';
		public char CDataEndChar { get { return mvarCDataEndChar; } set { mvarCDataEndChar = value; } }

		private bool mvarPrettyPrint = true;
		/// <summary>
		/// Determines whether to insert tabs and spaces to "pretty-print" the output XML.
		/// </summary>
		public bool PrettyPrint { get { return mvarPrettyPrint; } set { mvarPrettyPrint = value; } }
	}
}
