//
//  WMFEscapeFunction.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	public enum WMFEscapeFunction
	{
		NewFrame = 0x0001,
		AbortDocument = 0x0002,
		NextBand = 0x0003,
		SetColorTable = 0x0004,
		GetColorTable = 0x0005,
		FlushOut = 0x0006,
		DraftMode = 0x0007,
		QueryEscapeSupport = 0x0008,
		SetAbortProcedure = 0x0009,
		StartDocument = 0x000A,
		EndDocument = 0x000B,
		GetPhysicalPageSize = 0x000C,
		GetPrintingOffset = 0x000D,
		GetScalingFactor = 0x000E,
		MetaEscapeEnhancedMetafile = 0x000F,
		SetPenWidth = 0x0010,
		SetCopyCount = 0x0011,
		SetPaperSource = 0x0012,
		Passthrough = 0x0013,
		GetTechnology = 0x0014,
		SetLineCap = 0x0015,
		SetLineJoin = 0x0016,
		SetMiterLimit = 0x0017,
		BandInfo = 0x0018,
		DrawPatternRect = 0x0019,
		GetVectorPenSize  = 0x001A,
		GetVectorBrushSize = 0x001B,
		EnableDuplex = 0x001C,
		GetSetPaperBins = 0x001D,
		GetSetPrintOrientation = 0x001E,
		EnumPaperBins = 0x001F,
		SetDIBScaling = 0x0020,
		EPSPrinting = 0x0021,
		EnumPaperMetrics = 0x0022,
		GetSetPaperMetrics = 0x0023,
		PostscriptData = 0x0025,
		PostscriptIgnore = 0x0026,
		GetDeviceUnits = 0x002A,
		GetExtendedTextMetrics = 0x0100,
		GetPairKernTable = 0x0102,
		ExtTextOut = 0x0200,
		GetFaceName = 0x0201,
		DownloadFace = 0x0202,
		MetafileDriver = 0x0801,
		QueryDIBSupport = 0x0C01,
		BeginPath = 0x1000,
		ClipToPath = 0x1001,
		EndPath = 0x1002,
		OpenChannel = 0x100E,
		DownloadHeader = 0x100F,
		CloseChannel = 0x1010,
		PostscriptPassthrough = 0x1013,
		EncapsulatedPostscript = 0x1014,
		PostscriptIdentify = 0x1015,
		PostscriptInjection = 0x1016,
		CheckJPEGFormat = 0x1017,
		CheckPNGFormat = 0x1018,
		GetPostscriptFeatureSetting = 0x1019,
		MXDCEscape = 0x101A,
		SPCLPassthrough2 = 0x11D8
	}
}
