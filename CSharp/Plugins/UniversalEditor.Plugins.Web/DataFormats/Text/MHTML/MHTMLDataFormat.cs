using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.Text.MHTML
{
	public class MHTMLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(PlainTextObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("MIME-encoded HyperText Markup Language", new string[] { "*.mht", "*.mhtml" });
			}
			return _dfr;
		}

		#region DataFormat-specific properties
		private string mvarSourceEmailAddress = String.Empty;
		/// <summary>
		/// The e-mail address of the source document. This is the "From" field in the MHTML header.
		/// </summary>
		public string SourceEmailAddress { get { return mvarSourceEmailAddress; } set { mvarSourceEmailAddress = value; } }
		private string mvarSubject = String.Empty;
		/// <summary>
		/// The subject of the source document.
		/// </summary>
		public string Subject { get { return mvarSubject; } set { mvarSubject = value; } }
		private DateTime mvarTimestamp = DateTime.Now;
		/// <summary>
		/// The date the source document was generated.
		/// </summary>
		public DateTime Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; } }
		private Version mvarEncodingVersion = new Version(1, 0);
		/// <summary>
		/// The version of the MIME encoder used to generate this document.
		/// </summary>
		public Version EncodingVersion { get { return mvarEncodingVersion; } set { mvarEncodingVersion = value; } }
		private string mvarOriginalLocation = String.Empty;
		/// <summary>
		/// The original location of this document.
		/// </summary>
		public string OriginalLocation { get { return mvarOriginalLocation; } set { mvarOriginalLocation = value; } }
		private MHTMLHeader.MHTMLHeaderCollection mvarCustomHeaders = new MHTMLHeader.MHTMLHeaderCollection();
		/// <summary>
		/// Custom headers that are associated with this document.
		/// </summary>
		public MHTMLHeader.MHTMLHeaderCollection CustomHeaders { get { return mvarCustomHeaders; } }
		#endregion

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PlainTextObjectModel text = (objectModel as PlainTextObjectModel);
			if (text == null) return;

			Reader tr = base.Accessor.Reader;
			string content = String.Empty;

			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (line.Contains(": "))
				{
					string[] parts = line.Split(new char[] { ':' });
					if (parts.Length == 2)
					{
						string fieldName = parts[0];
						string fieldValue = parts[1].Trim();

						switch (fieldName)
						{
							case "From":
							{
								mvarSourceEmailAddress = fieldValue;
								break;
							}
							case "Subject":
							{
								mvarSubject = fieldValue;
								break;
							}
							case "Date":
							{
								mvarTimestamp = DateTime.Parse(fieldValue);
								break;
							}
							case "MIME-Version":
							{
								mvarEncodingVersion = new Version(fieldValue);
								break;
							}
							case "Content-Location":
							{
								mvarOriginalLocation = fieldValue;
								break;
							}
							default:
							{
								mvarCustomHeaders.Add(fieldName, fieldValue);
								break;
							}
						}
					}
				}
				if (String.IsNullOrEmpty(line))
				{
					content = tr.ReadStringToEnd();
					break;
				}
			}

			for (int i = 0; i < content.Length; i++)
			{
				if ((i < content.Length + 2) && (content.Substring(i, 1) == "="))
				{
					string hexValue = content.Substring(i + 1, 2);
					string before = content.Substring(0, i);
					string after = content.Substring(i + 3);

					if (hexValue != "\r\n")
					{

						byte decValue = Byte.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
						string chrValue = System.Text.Encoding.ASCII.GetString(new byte[] { decValue });

						content = before + chrValue + after;
					}
					else
					{
						after = content.Substring(i + 1);
						content = before + after;
					}
					i += 2;
				}
			}

			// Kick off an HTMLDataFormat to parse the resulting HTML in "content" variable
			Document.Load(text, new HTML.HTMLDataFormat(), new StringAccessor(content));
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
