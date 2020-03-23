using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.PSF
{
	public enum PSFPlatform : byte
	{
		/// <summary>
		/// Playstation (PSF1)
		/// </summary>
		Playstation = 0x01,
		/// <summary>
		/// Playstation 2 (PSF2)
		/// </summary>
		Playstation2 = 0x02,
		/// <summary>
		/// Saturn (SSF) (format subject to change)
		/// </summary>
		Saturn = 0x11,
		/// <summary>
		/// Dreamcast (DSF) (format subject to change)
		/// </summary>
		Dreamcast = 0x12,
		/// <summary>
		/// Sega Genesis (format to be announced)
		/// </summary>
		Genesis = 0x13,
		/// <summary>
		/// Nintendo 64 (USF)
		/// </summary>
		Nintendo64 = 0x21,
		/// <summary>
		/// GameBoy Advance (GSF)
		/// </summary>
		GameBoyAdvance = 0x22,
		/// <summary>
		/// Super NES (SNSF)
		/// </summary>
		SuperNES = 0x23,
		/// <summary>
		/// Capcom QSound (QSF)
		/// </summary>
		CapcomQSound = 0x41
	}
}
