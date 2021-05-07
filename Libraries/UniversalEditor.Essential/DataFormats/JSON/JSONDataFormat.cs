//
//  JSONDataFormat.cs - provides a DataFormat for manipulating markup in JavaScript Object Notation (JSON) format
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

using UniversalEditor.ObjectModels.JSON;
using UniversalEditor.ObjectModels.JSON.Fields;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.JSON
{

	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating markup in JavaScript Object Notation (JSON) format.
	/// </summary>
	public class JSONDataFormat : PlainTextDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(JSONObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public void ApplySettings(JSONPresetSettings settings)
		{
			switch (settings)
			{
				case JSONPresetSettings.JSON:
				{
					Settings.ObjectNamePrefix = "\"";
					Settings.ObjectNameSuffix = "\"";
					Settings.ObjectNameValueSeparator = ":";
					Settings.FieldNamePrefix = "\"";
					Settings.FieldNameSuffix = "\"";
					Settings.FieldNameValueSeparator = ":";
					Settings.FieldSeparator = ",";
					Settings.StringLiteralPrefix = "\"";
					Settings.StringLiteralSuffix = "\"";
					break;
				}
				case JSONPresetSettings.ExtendedINI:
				{
					Settings.ObjectNamePrefix = "";
					Settings.ObjectNameSuffix = "";
					Settings.ObjectNameValueSeparator = " ";
					Settings.FieldNamePrefix = "";
					Settings.FieldNameSuffix = "";
					Settings.FieldNameValueSeparator = "=";
					Settings.FieldSeparator = ";";
					Settings.StringLiteralPrefix = "\"";
					Settings.StringLiteralSuffix = "\"";
					break;
				}
			}
		}

		/// <summary>
		/// Represents settings for the <see cref="JSONDataFormat" /> parser.
		/// </summary>
		/// <value>The settings for the <see cref="JSONDataFormat" /> parser.</value>
		public JSONSettings Settings { get; } = new JSONSettings();

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PlainTextObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PlainTextObjectModel ptom = (objectModels.Pop() as PlainTextObjectModel);
			JSONObjectModel json = (objectModels.Pop() as JSONObjectModel);

			throw new NotImplementedException();
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			JSONObjectModel json = (objectModels.Pop() as JSONObjectModel);
			PlainTextObjectModel ptom = (objectModels.Pop() as PlainTextObjectModel);
			ptom.Lines.Clear();

			for (int iObj = 0; iObj < json.Objects.Count; iObj++)
			{
				WriteObject(ptom, json.Objects[iObj]);
				if (iObj < json.Objects.Count - 1)
					ptom.WriteLine();
			}
		}

		private void WriteObject(PlainTextObjectModel ptom, JSONObject obj)
		{
			WriteObject(ptom, obj, 0);
		}
		private void WriteObject(PlainTextObjectModel ptom, JSONObject obj, int indentLevel)
		{
			string szIndent = "";
			if (Settings.IndentChildFields)
			{
				szIndent = ptom.GetIndent(indentLevel);
			}
			if (obj.Name != "")
			{
				ptom.Lines.Add(Settings.ObjectNamePrefix + obj.Name + Settings.ObjectNameSuffix + (Settings.AppendSpaceAfterObjectName ? " " : "") + Settings.ObjectNameValueSeparator + " ");
			}
			if (Settings.AppendLineAfterObjectName && obj.Name != "")
			{
				ptom.WriteLine();
				ptom.Write(szIndent);
			}
			ptom.WriteLine(Settings.ObjectPrefix);

			// Content body
			for (int i = 0; i < obj.Fields.Count; i++)
			{
				JSONField f = obj.Fields[i];
				WriteField(ptom, f, indentLevel + 1);

				if (i < obj.Fields.Count - 1)
				{
					ptom.Write(Settings.FieldSeparator);
				}
				if (Settings.AppendLineAfterField) ptom.WriteLine();
			}

			ptom.Write(szIndent + Settings.ObjectSuffix);
		}
		private void WriteField(PlainTextObjectModel ptom, JSONField f, int indentLevel)
		{
			if (f.GetType() != typeof(JSONObjectField))
			{
				if (Settings.IndentChildFields)
				{
					ptom.Write(ptom.GetIndent(indentLevel));
				}
				ptom.Write(Settings.FieldNamePrefix + f.Name + Settings.FieldNameSuffix + Settings.FieldNameValueSeparator + " ");
				if (Settings.AppendLineAfterFieldName)
				{
					ptom.WriteLine();
					ptom.Write(ptom.GetIndent(indentLevel));
				}
			}
			if (f.GetType() == typeof(JSONArrayField))
			{
				ptom.Write(Settings.ArrayPrefix);
				if (Settings.AppendLineAfterStartArray) ptom.WriteLine();

				JSONArrayField af = (f as JSONArrayField);
				for (int i = 0; i < af.Values.Count; i++)
				{
					if (Settings.IndentArrayValues)
					{
						ptom.Write(ptom.GetIndent(indentLevel + 1));
					}
					ptom.Write(Settings.ArrayValuePrefix + af.Values[i] + Settings.ArrayValueSuffix);

					if (i < af.Values.Count - 1) ptom.Write(", ");
					if (Settings.AppendLineAfterArrayValue) ptom.WriteLine();
				}
				ptom.Write(ptom.GetIndent(indentLevel) + Settings.ArraySuffix);
			}
			else if (f.GetType() == typeof(JSONBooleanField))
			{
				ptom.Write((f as JSONBooleanField).Value.ToString().ToLower());
			}
			else if (f.GetType() == typeof(JSONNumberField))
			{
				ptom.Write((f as JSONNumberField).Value.ToString());
			}
			else if (f.GetType() == typeof(JSONObjectField))
			{
				WriteObject(ptom, (f as JSONObjectField).Value, indentLevel);
			}
			else if (f.GetType() == typeof(JSONStringField))
			{
				ptom.Write(Settings.StringLiteralPrefix + (f as JSONStringField).Value + Settings.StringLiteralSuffix);
			}
		}
	}
}
