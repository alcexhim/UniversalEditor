//
//  MyClass.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.DataFormats.Text.Plain;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Database;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.Plugins.Database.DataFormats.SQL
{
	public class SQLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(DatabaseObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public SQLSettings Settings { get; set; } = new SQLSettings();
		public SQLQuotingCharacter QuotingCharacter { get; set; } = SQLQuotingCharacter.Brackets;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			DatabaseObjectModel db = (objectModel as DatabaseObjectModel);
			if (db == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string token = ReadToken(reader).Trim();
				if (token.StartsWith("/*") && token.EndsWith("*/"))
				{
					// is comment
					continue;
				}
				switch (token.ToLower())
				{
					case "use":
					{
						string dbName = ReadToken(reader);
						db.Name = dbName;
						break;
					}
					case "go":
					{
						break;
					}
					case "set":
					{
						StringBuilder sbVariableName = new StringBuilder();
						string variableName = ReadToken(reader);
						string value = ReadToken(reader);

						break;
					}
					case "create":
					{
						string objectType = ReadToken(reader).ToLower();
						switch (objectType)
						{
							case "table":
							{
								DatabaseTable table = new DatabaseTable();

								string tableName = ReadToken(reader, out string rest);
								table.Name = tableName;

								List<string> fullyQualifiedTableName = new List<string>();
								fullyQualifiedTableName.Add(tableName);
								while (rest == ".")
								{
									tableName = ReadToken(reader, out rest);
									fullyQualifiedTableName.Add(tableName);
								}

								if (rest == "(")
								{
									do
									{
										DatabaseField field = new DatabaseField();

										string columnName = ReadToken(reader);
										field.Name = columnName;

										string dataType = ReadToken(reader, out rest);
										if (rest == "(")
										{
											// next token is dataLength
											string dataLength = ReadToken(reader);
										}

										string paramz = ReadToken(reader, out rest);
										if (paramz.ToLower() == "not")
										{
											// not what?
											paramz = ReadToken(reader, out rest);
											if (paramz.ToLower() == "null")
											{

											}
										}

										table.Fields.Add(field);
									}
									while (rest == ",");
								}

								db.Tables.Add(table);
								break;
							}
						}
						break;
					}
				}
			}
		}

		private string ReadToken(Reader reader)
		{
			string rest = null;
			return ReadToken(reader, out rest);
		}
		private string ReadToken(Reader reader, out string rest)
		{
			string[] tokenDelimiters = new string[] { " ", "\n", ".", "(", ")" };

			string token = reader.ReadUntil(tokenDelimiters, out rest).Trim();
			while (String.IsNullOrEmpty(token))
				token = reader.ReadUntil(tokenDelimiters, out rest).Trim();

			if (token.StartsWith("/*"))
			{
				rest = reader.ReadUntil("*/", true);
				string cmntend = reader.ReadFixedLengthString(2);
				token += rest + cmntend;
				return token;
			}
			if (token.StartsWith(Settings.QuotingCharacterStart) && !token.EndsWith(Settings.QuotingCharacterEnd))
			{
				StringBuilder sbWholeToken = new StringBuilder();
				sbWholeToken.Append(token);
				while (!token.Contains(Settings.QuotingCharacterEnd))
				{
					token = ReadToken(reader);
					sbWholeToken.Append(' ');
					sbWholeToken.Append(token);
				}
				token = sbWholeToken.ToString();
			}
			if (token.StartsWith(Settings.QuotingCharacterStart) && token.Contains(Settings.QuotingCharacterEnd))
			{
				token = token.Substring(Settings.QuotingCharacterStart.Length, token.IndexOf(Settings.QuotingCharacterEnd) - Settings.QuotingCharacterStart.Length);
			}

			if (token.EndsWith("\r"))
				token = token.Trim();
			return token;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
