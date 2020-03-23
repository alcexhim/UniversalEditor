using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public enum SystemRecordType : ushort
	{
		/// <summary>
		/// Help file title
		/// </summary>
		Title = 1,
		/// <summary>
		/// Copyright notice shown in AboutBox
		/// </summary>
		Copyright = 2,
		/// <summary>
		/// Topic offset of starting topic
		/// </summary>
		Contents = 3,
		/// <summary>
		/// All macros executed on opening
		/// </summary>
		Config = 4,
		/// <summary>
		/// See WIN31WH on icon file format
		/// </summary>
		Icon = 5,
		/// <summary>
		/// Windows defined in the HPJ-file; Viewer 2.0 Windows defined in MVP-file
		/// </summary>
		Window = 6,
		/// <summary>
		/// The Citation printed
		/// </summary>
		Citation = 8,
		/// <summary>
		/// Language ID, Windows 95 (HCW 4.00)
		/// </summary>
		LanguageID = 9,
		/// <summary>
		/// CNT file name, Windows 95 (HCW 4.00)
		/// </summary>
		TableOfContents = 10,
		/// <summary>
		/// Charset, Windows 95 (HCW 4.00)
		/// </summary>
		CharacterSet = 11,
		/// <summary>
		/// Default dialog font, Windows 95 (HCW 4.00); Multimedia Help Files dtypes
		/// </summary>
		DefaultDialogFontOrFTIndex = 12,
		/// <summary>
		/// Defined GROUPs, Multimedia Help File
		/// </summary>
		Groups = 13,
		/// <summary>
		/// Separators, Windows 95 (HCW 4.00); Multimedia Help Files
		/// </summary>
		IndexSeparatorsOrKeyIndex = 14,
		/// <summary>
		/// Defined language, Multimedia Help Files
		/// </summary>
		Language = 18,
		/// <summary>
		/// Defined DLLMAPS, Multimedia Help Files
		/// </summary>
		DLLMaps = 19
	}
}
