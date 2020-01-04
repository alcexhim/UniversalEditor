using System;
namespace UniversalEditor.DataFormats.FileSystem.TexasInstruments.DSK
{
	[Flags()]
	public enum DSKFileStatusFlags
	{
		/*
		 * Bit No. Description
0   ... File type indicator.
    ........ 0 = Data file
    ........ 1 = Program file
1   ... Data type indicator
    ........ 0 = ASCII data (DISPLAY file)
    ........ 1 = Binary data (INTERNAL or PROGRAM file)
2   ... This bit was reserved for expansion of the data
    ... type indicator.
3   ... PROTECT FLAG
    ........ 0 = NOT protected
    ........ 1 = Protected
4,5 and 6 These bits were reserved for expansion of ????
7   .... Fixed/variable flag
     ........ 0 = Fixed record lengths
     ........ 1 = Variable record lengths
    	*/

		None = 0x00,
		Program = 0x01,
		Binary = 0x02,
		Protected = 0x08,
		/*
		Reserved1 = 0x10,
		Reserved2 = 0x20,
		Reserved3 = 0x40,
		*/
		Variable = 0x80
	}
}
