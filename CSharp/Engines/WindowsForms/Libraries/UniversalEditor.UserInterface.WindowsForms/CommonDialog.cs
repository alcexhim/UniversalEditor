using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface.WindowsForms
{
	/// <summary>
	/// Contains methods useful for common file dialogs in Windows Forms.
	/// </summary>
	public static class CommonDialog
	{
		// UniversalDataStorage.Common.Methods
		public static string GetCommonDialogFilter(Association[] associations, bool includeAllFiles = true)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Association assoc in associations)
			{
				foreach (DataFormatFilter filter in assoc.Filters)
				{
					sb.Append(filter.Title + "|");
					foreach (string s in filter.FileNameFilters)
					{
						sb.Append(s);
						if (filter.FileNameFilters.IndexOf(s) > filter.FileNameFilters.Count - 1)
						{
							sb.Append("; ");
						}
					}
					if (assoc.Filters.IndexOf(filter) > assoc.Filters.Count - 1) sb.Append("|");
				}
			}
			if (includeAllFiles)
			{
				sb.Append("|All files (*.*)|*.*");
			}
			return sb.ToString();
		}
	}
}
