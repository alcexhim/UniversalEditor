using System;
using System.Collections.Generic;

using UniversalEditor.DataFormats.Text.Plain;

using UniversalEditor.ObjectModels.JSON;
using UniversalEditor.ObjectModels.JSON.Fields;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.PropertyList.JSON
{

	/// <summary>
	/// Represents a JSON document, a text-based, human-readable format for representing simple data structures and associative arrays.
	/// </summary>
	public class JSONDataFormat : PlainTextDataFormat
	{
		private static DataFormatReference _dfr = null;
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
			switch(settings)
			{
				case JSONPresetSettings.JSON:
					mvarSettings.ObjectNamePrefix = "\"";
					mvarSettings.ObjectNameSuffix = "\"";
					mvarSettings.ObjectNameValueSeparator = ":";
					mvarSettings.FieldNamePrefix = "\"";
					mvarSettings.FieldNameSuffix = "\"";
					mvarSettings.FieldNameValueSeparator = ":";
					mvarSettings.FieldSeparator = ",";
					mvarSettings.StringLiteralPrefix = "\"";
					mvarSettings.StringLiteralSuffix = "\"";
					break;
				case JSONPresetSettings.ExtendedINI:
					mvarSettings.ObjectNamePrefix = "";
					mvarSettings.ObjectNameSuffix = "";
					mvarSettings.ObjectNameValueSeparator = " ";
					mvarSettings.FieldNamePrefix = "";
					mvarSettings.FieldNameSuffix = "";
					mvarSettings.FieldNameValueSeparator = "=";
					mvarSettings.FieldSeparator = ";";
					mvarSettings.StringLiteralPrefix = "\"";
					mvarSettings.StringLiteralSuffix = "\"";
					break;
			}
		}

        private JSONSettings mvarSettings = new JSONSettings();
        public JSONSettings Settings { get { return mvarSettings; } }

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
            if (mvarSettings.IndentChildFields)
        	{
        		szIndent = ptom.GetIndent(indentLevel);
        	}
            if (obj.Name != "")
            {
            	ptom.Lines.Add(mvarSettings.ObjectNamePrefix + obj.Name + mvarSettings.ObjectNameSuffix + (mvarSettings.AppendSpaceAfterObjectName ? " " : "") + mvarSettings.ObjectNameValueSeparator + " ");
            }
            if (mvarSettings.AppendLineAfterObjectName && obj.Name != "")
            {
				ptom.WriteLine();
            	ptom.Write(szIndent);
            }
            ptom.WriteLine(mvarSettings.ObjectPrefix);

            // Content body
            for (int i = 0; i < obj.Fields.Count; i++)
            {
            	JSONField f = obj.Fields[i];
                WriteField(ptom, f, indentLevel + 1);
                
                if (i < obj.Fields.Count - 1) 
                {
                	ptom.Write(mvarSettings.FieldSeparator);
                }
                if (mvarSettings.AppendLineAfterField) ptom.WriteLine();
            }

            ptom.Write(szIndent + mvarSettings.ObjectSuffix);
        }
        private void WriteField(PlainTextObjectModel ptom, JSONField f, int indentLevel)
        {
        	if (f.GetType() != typeof(JSONObjectField))
        	{
                if (mvarSettings.IndentChildFields)
	        	{
                    ptom.Write(ptom.GetIndent(indentLevel));
	        	}
                ptom.Write(mvarSettings.FieldNamePrefix + f.Name + mvarSettings.FieldNameSuffix + mvarSettings.FieldNameValueSeparator + " ");
                if (mvarSettings.AppendLineAfterFieldName) 
        		{
                    ptom.WriteLine();
                    ptom.Write(ptom.GetIndent(indentLevel));
        		}
        	}
        	if (f.GetType() == typeof(JSONArrayField))
        	{
                ptom.Write(mvarSettings.ArrayPrefix);
                if (mvarSettings.AppendLineAfterStartArray) ptom.WriteLine();
        		
        		JSONArrayField af = (f as JSONArrayField);
        		for(int i = 0; i < af.Values.Count; i++)
        		{
                    if (mvarSettings.IndentArrayValues)
        			{
                        ptom.Write(ptom.GetIndent(indentLevel + 1));
        			}
                    ptom.Write(mvarSettings.ArrayValuePrefix + af.Values[i] + mvarSettings.ArrayValueSuffix);

                    if (i < af.Values.Count - 1) ptom.Write(", ");
                    if (mvarSettings.AppendLineAfterArrayValue) ptom.WriteLine();
        		}
                ptom.Write(ptom.GetIndent(indentLevel) + mvarSettings.ArraySuffix);
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
                ptom.Write(mvarSettings.StringLiteralPrefix + (f as JSONStringField).Value + mvarSettings.StringLiteralSuffix);
        	}
        }
	}
}
