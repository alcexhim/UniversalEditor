//
//  MHTMLDataFormat.cs - provides a DataFormat for reading and writing formatted text in MHTML format
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.ObjectModels.Text.Formatted;

namespace UniversalEditor.DataFormats.Text.MHTML
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for reading and writing formatted text in MHTML format.
	/// </summary>
	public class MHTMLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(PlainTextObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);
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
			if (text == null) throw new ObjectModelNotSupportedException();

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
