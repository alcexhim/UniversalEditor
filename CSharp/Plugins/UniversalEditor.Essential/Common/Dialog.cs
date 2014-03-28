using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Common
{
    public static class Dialog
    {
        // UniversalDataStorage.Common.Methods
        public static string GetCommonDialogFilter(DataFormatReference dataFormatReference)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbCompatible = new StringBuilder();
            for (int i = 0; i < dataFormatReference.Filters.Count; i++)
            {
                sb.Append(dataFormatReference.Filters[i].Title + " (");
                StringBuilder sbFilters = new StringBuilder();
                for (int j = 0; j < dataFormatReference.Filters[i].FileNameFilters.Count; j++)
                {
                    sbFilters.Append(dataFormatReference.Filters[i].FileNameFilters[j]);
                    if (j < dataFormatReference.Filters[i].FileNameFilters.Count - 1)
                    {
                        sbFilters.Append("; ");
                    }
                }
                sb.Append(sbFilters.ToString());
                sb.Append(")|");
                sb.Append(sbFilters.ToString());
                sb.Append("|");
                sbCompatible.Append(sbFilters.ToString());
            }

            sb.Append("All files (*.*)|*.*");
            return sb.ToString();
        }
        public static string GetCommonDialogFilter(ObjectModelReference objectModelReference)
        {
            List<DataFormatReference> dataFormatReferences = new List<DataFormatReference>();
            return GetCommonDialogFilter(objectModelReference, out dataFormatReferences);
        }
        public static string GetCommonDialogFilter(ObjectModelReference objectModelReference, out List<DataFormatReference> dataFormatReferences)
        {
            List<DataFormatReference> dfrs = new List<DataFormatReference>();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbCompatible = new StringBuilder();
            DataFormatReference[] dataFormats = Reflection.GetAvailableDataFormats(objectModelReference);
            DataFormatReference[] array = dataFormats;
            for (int k = 0; k < array.Length; k++)
            {
                DataFormatReference dfb = array[k];
                for (int i = 0; i < dfb.Filters.Count; i++)
                {
                    sb.Append(dfb.Filters[i].Title + " (");
                    StringBuilder sbFilters = new StringBuilder();
                    for (int j = 0; j < dfb.Filters[i].FileNameFilters.Count; j++)
                    {
                        sbFilters.Append(dfb.Filters[i].FileNameFilters[j]);
                        if (j < dfb.Filters[i].FileNameFilters.Count - 1)
                        {
                            sbFilters.Append("; ");
                        }
                    }
                    sb.Append(sbFilters.ToString());
                    sb.Append(")|");
                    sb.Append(sbFilters.ToString());
                    sb.Append("|");
                    sbCompatible.Append(sbFilters.ToString());
                    if (i < dfb.Filters.Count - 1 || Array.IndexOf<DataFormatReference>(dataFormats, dfb) < dataFormats.Length - 1)
                    {
                        sbCompatible.Append("; ");
                    }
                    dfrs.Add(dfb);
                }
            }
            sb.Insert(0, string.Concat(new string[]
	{
		"All ", 
		objectModelReference.Title, 
		" files (", 
		sbCompatible.ToString(), 
		")|", 
		sbCompatible.ToString(), 
		"|"
	}));
            sb.Append("All files (*.*)|*.*");

            dataFormatReferences = dfrs;
            return sb.ToString();
        }
    }
}
