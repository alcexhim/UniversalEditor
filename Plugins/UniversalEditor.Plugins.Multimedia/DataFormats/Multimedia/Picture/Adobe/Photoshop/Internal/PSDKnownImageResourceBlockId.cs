//
//  PSDKnownImageResourceBlockId.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop.Internal
{
	public enum PSDKnownImageResourceBlockId : short
	{
		/// <summary>
		/// (Obsolete--Photoshop 2.0 only ) Contains five 2-byte values: number of channels, rows, columns, depth, and mode
		/// </summary>
		PSD2ImageHeader = 1000,
		/// <summary>
		/// Macintosh print manager print info record
		/// </summary>
		MacintoshPrintManagerInfoRecord = 1001,
		/// <summary>
		/// Macintosh page format information. No longer read by Photoshop. (Obsolete)
		/// </summary>
		MacintoshPageFormat = 1002,
		/// <summary>
		/// (Obsolete--Photoshop 2.0 only ) Indexed color table
		/// </summary>
		IndexedColorTable = 1003,
		/// <summary>
		/// ResolutionInfo structure. See Appendix A in Photoshop API Guide.pdf.
		/// </summary>
		ResolutionInfo = 1005,
		/// <summary>
		/// Names of the alpha channels as a series of Pascal strings.
		/// </summary>
		AlphaChannelNames = 1006,
		/// <summary>
		/// (Obsolete) See ID 1077. DisplayInfo structure. See Appendix A in Photoshop API Guide.pdf.
		/// </summary>
		[Obsolete("see 1077: DisplayInfoCS3")]
		DisplayInfo = 1007,
		/// <summary>
		/// The caption as a Pascal string.
		/// </summary>
		Caption = 1008,
		/// <summary>
		/// Border information. Contains a fixed number (2 bytes real, 2 bytes fraction) for the border width, and 2 bytes for border units (1 = inches, 2 = cm, 3 = points, 4 = picas, 5 = columns).
		/// </summary>
		BorderInformation = 1009,
		/// <summary>
		/// Background color. See See Color structure.
		/// </summary>
		BackgroundColor = 1010,
		/// <summary>
		/// A series of one-byte boolean values (see Page Setup dialog): labels, crop marks, color bars, registration marks, negative, flip, interpolate, caption, print flags.
		/// </summary>
		PrintFlags = 1011,
		/// <summary>
		/// Grayscale and multichannel halftoning information
		/// </summary>
		GrayscaleHalftoning = 1012,
		/// <summary>
		/// Color halftoning information
		/// </summary>
		ColorHalftoning = 1013,
		/// <summary>
		/// Duotone halftoning information
		/// </summary>
		DuotoneHalftoning = 1014,
		/// <summary>
		/// Grayscale and multichannel transfer function
		/// </summary>
		GrayscaleTransferFunction = 1015,
		/// <summary>
		/// Color transfer functions
		/// </summary>
		ColorTransferFunction = 1016,
		/// <summary>
		/// Duotone transfer functions
		/// </summary>
		DuotoneTransferFunction = 1017,
		/// <summary>
		/// Duotone image information
		/// </summary>
		DuotoneImageInformation = 1018,
		/// <summary>
		/// Two bytes for the effective black and white values for the dot range
		/// </summary>
		EffectiveMonochromeDotRange = 1019,
		/// <summary>
		/// (Obsolete)
		/// </summary>
		Obsolete1020 = 1020,
		/// <summary>
		/// EPS options
		/// </summary>
		EPSOptions = 1021,
		/// <summary>
		/// 2 bytes containing Quick Mask channel ID; 1- byte boolean indicating whether the mask was initially empty.
		/// </summary>
		QuickMask = 1022,
		/// <summary>
		/// (Obsolete)
		/// </summary>
		Obsolete1023 = 1023,
		/// <summary>
		/// Layer state information. 2 bytes containing the index of target layer (0 = bottom layer).
		/// </summary>
		LayerState = 1024,
		/// <summary>
		/// Working path (not saved). See See Path resource format.
		/// </summary>
		WorkingPath = 1025,
		/// <summary>
		/// 2 bytes per layer containing a group ID for the dragging groups. Layers in a group have the same group ID.
		/// </summary>
		LayersGroup = 1026,
		/// <summary>
		/// (Obsolete)
		/// </summary>
		Obsolete1027 = 1027,
		/// <summary>
		/// IPTC-NAA record. Contains the File Info... information. See the documentation in the IPTC folder of the Documentation folder.
		/// </summary>
		IptcNaa = 1028,
		/// <summary>
		/// Image mode for raw format files
		/// </summary>
		ImageMode = 1029,
		/// <summary>
		/// JPEG quality. Private.
		/// </summary>
		JPEGQuality = 1030,
		/// <summary>
		/// (Photoshop 4.0) Grid and guides information. See See Grid and guides resource format.
		/// </summary>
		PSD4GridGuidesInformation = 1032,
		/// <summary>
		/// (Photoshop 4.0) Thumbnail resource for Photoshop 4.0 only. See See Thumbnail resource format.
		/// </summary>
		PSD4ThumbnailResource = 1033,
		/// <summary>
		/// Copyright flag. Boolean indicating whether image is copyrighted. Can be set via Property suite or by user in File Info...
		/// </summary>
		PSD4CopyrightFlag = 1034,
		/// <summary>
		/// (Photoshop 4.0) URL. Handle of a text string with uniform resource locator. Can be set via Property suite or by user in File Info...
		/// </summary>
		PSD4URL = 1035,
		/// <summary>
		/// (Photoshop 5.0) Thumbnail resource (supersedes resource 1033). See See Thumbnail resource format.
		/// </summary>
		PSD5ThumbnailResource = 1036,
		/// <summary>
		/// (Photoshop 5.0) Global Angle. 4 bytes that contain an integer between 0 and 359, which is the global lighting angle for effects layer. If not present, assumed to be 30.
		/// </summary>
		PSD5GlobalAngle = 1037,
		/// <summary>
		/// (Obsolete) See ID 1073 below. (Photoshop 5.0) Color samplers resource. See See Color samplers resource format.
		/// </summary>
		[Obsolete("See ID 1073")]
		ColorSamplersPSD5 = 1038,
		/// <summary>
		/// (Photoshop 5.0) ICC Profile. The raw bytes of an ICC (International Color Consortium) format profile. See ICC1v42_2006-05.pdf in the Documentation folder and icProfileHeader.h in Sample Code\Common\Includes .
		/// </summary>
		PSD5ICCProfile = 1039,
		/// <summary>
		/// (Photoshop 5.0) Watermark. One byte.
		/// </summary>
		PSD5Watermark = 1040,
		/// <summary>
		/// (Photoshop 5.0) ICC Untagged Profile. 1 byte that disables any assumed profile handling when opening the file. 1 = intentionally untagged.
		/// </summary>
		PSD5ICCUntaggedProfile = 1041,
		/// <summary>
		/// (Photoshop 5.0) Effects visible. 1-byte global flag to show/hide all the effects layer. Only present when they are hidden.
		/// </summary>
		PSD5EffectsHidden = 1042,
		/// <summary>
		/// (Photoshop 5.0) Spot Halftone. 4 bytes for version, 4 bytes for length, and the variable length data.
		/// </summary>
		PSD5SpotHalftone = 1043,
		/// <summary>
		/// (Photoshop 5.0) Document-specific IDs seed number. 4 bytes: Base value, starting at which layer IDs will be generated (or a greater value if
		/// existing IDs already exceed it). Its purpose is to avoid the case where we add layers, flatten, save, open, and then add more layers that end
		/// up with the same IDs as the first set.
		/// </summary>
		PSD5DocumentSpecificIDSeedNumber = 1044,
		/// <summary>
		/// (Photoshop 5.0) Unicode Alpha Names. Unicode string
		/// </summary>
		PSD5UnicodeAlphaNames = 1045,
		/// <summary>
		/// (Photoshop 6.0) Indexed Color Table Count. 2 bytes for the number of colors in table that are actually defined
		/// </summary>
		PSD6IndexedColorTableCount = 1046,
		/// <summary>
		/// (Photoshop 6.0) Transparency Index. 2 bytes for the index of transparent color, if any.
		/// </summary>
		TransparencyIndex = 1047,
		/// <summary>
		/// (Photoshop 6.0) Global Altitude. 4 byte entry for altitude
		/// </summary>
		GlobalAltitude = 1049,
		/// <summary>
		/// (Photoshop 6.0) Slices. See See Slices resource format.
		/// </summary>
		Slices = 1050,
		/// <summary>
		/// (Photoshop 6.0) Workflow URL. Unicode string
		/// </summary>
		WorkflowURL = 1051,
		/// <summary>
		/// (Photoshop 6.0) Jump To XPEP. 2 bytes major version, 2 bytes minor version, 4 bytes count. Following is repeated for count: 4 bytes block size, 4 bytes key, if key = 'jtDd', then next is a Boolean for the dirty flag; otherwise it's a 4 byte entry for the mod date.
		/// </summary>
		JumpToXpep = 1052,
		/// <summary>
		/// (Photoshop 6.0) Alpha Identifiers. 4 bytes of length, followed by 4 bytes each for every alpha identifier.
		/// </summary>
		AlphaIdentifiers = 1053,
		/// <summary>
		/// (Photoshop 6.0) URL List. 4 byte count of URLs, followed by 4 byte long, 4 byte ID, and Unicode string for each count.
		/// </summary>
		URLList = 1054,
		/// <summary>
		/// (Photoshop 6.0) Version Info. 4 bytes version, 1 byte hasRealMergedData, Unicode string: writer name, Unicode string: reader name, 4 bytes file version.
		/// </summary>
		VersionInfo = 1057,
		/// <summary>
		/// (Photoshop 7.0) EXIF data 1. See http://www.kodak.com/global/plugins/acrobat/en/service/digCam/exifStandard2.pdf
		/// </summary>
		ExifData1 = 1058,
		/// <summary>
		/// (Photoshop 7.0) EXIF data 3. See http://www.kodak.com/global/plugins/acrobat/en/service/digCam/exifStandard2.pdf
		/// </summary>
		ExifData3 = 1059,
		/// <summary>
		/// (Photoshop 7.0) XMP metadata. File info as XML description. See http://www.adobe.com/devnet/xmp/
		/// </summary>
		XMPMetadata = 1060,
		/// <summary>
		/// (Photoshop 7.0) Caption digest. 16 bytes: RSA Data Security, MD5 message-digest algorithm
		/// </summary>
		CaptionDigest = 1061,
		/// <summary>
		/// (Photoshop 7.0) Print scale. 2 bytes style (0 = centered, 1 = size to fit, 2 = user defined). 4 bytes x location (floating point). 4 bytes y location (floating point). 4 bytes scale (floating point)
		/// </summary>
		PrintScale = 1062,
		/// <summary>
		/// (Photoshop CS) Pixel Aspect Ratio. 4 bytes (version = 1 or 2), 8 bytes double, x / y of a pixel. Version 2, attempting to correct values for NTSC and PAL, previously off by a factor of approx. 5%.
		/// </summary>
		PixelAspectRatio = 1064,
		/// <summary>
		/// (Photoshop CS) Layer Comps. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure)
		/// </summary>
		LayerComps = 1065,
		/// <summary>
		/// (Photoshop CS) Alternate Duotone Colors. 2 bytes (version = 1), 2 bytes count, following is repeated for each count: [Color: 2 bytes for space followed by 4 * 2 byte color component], following this is another 2 byte count, usually 256, followed by Lab colors one byte each for L, a, b. This resource is not read or used by Photoshop.
		/// </summary>
		AlternateDuotoneColors = 1066,
		/// <summary>
		/// (Photoshop CS)Alternate Spot Colors. 2 bytes (version = 1), 2 bytes channel count, following is repeated for each count: 4 bytes channel ID, Color: 2 bytes for space followed by 4 * 2 byte color component. This resource is not read or used by Photoshop.
		/// </summary>
		AlternateSpotColors = 1067,
		/// <summary>
		/// (Photoshop CS2) Layer Selection ID(s). 2 bytes count, following is repeated for each count: 4 bytes layer ID
		/// </summary>
		LayerSelectionIDs = 1069,
		/// <summary>
		/// (Photoshop CS2) HDR Toning information
		/// </summary>
		HDRToning = 1070,
		/// <summary>
		/// (Photoshop CS2) Print info
		/// </summary>
		PrintInfoCS2 = 1071,
		/// <summary>
		/// (Photoshop CS2) Layer Group(s) Enabled ID. 1 byte for each layer in the document, repeated by length of the resource. NOTE: Layer groups have start and end markers
		/// </summary>
		LayerGroupsEnabledIDs = 1072,
		/// <summary>
		/// (Photoshop CS3) Color samplers resource. Also see ID 1038 for old format. See See Color samplers resource format.
		/// </summary>
		ColorSamplersCS3 = 1073,
		/// <summary>
		/// (Photoshop CS3) Measurement Scale. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure)
		/// </summary>
		MeasurementScale = 1074,
		/// <summary>
		/// (Photoshop CS3) Timeline Information. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure)
		/// </summary>
		TimelineInformation = 1075,
		/// <summary>
		/// (Photoshop CS3) Sheet Disclosure. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure)
		/// </summary>
		SheetDisclosure = 1076,
		/// <summary>
		/// (Photoshop CS3) DisplayInfo structure to support floating point clors. Also see ID 1007. See Appendix A in Photoshop API Guide.pdf .
		/// </summary>
		DisplayInfoCS3 = 1077,
		/// <summary>
		/// (Photoshop CS3) Onion Skins. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure)
		/// </summary>
		OnionSkins = 1078,
		/// <summary>
		/// (Photoshop CS4) Count Information. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure) Information about the count in the document. See the Count Tool.
		/// </summary>
		Count = 1080,
		/// <summary>
		/// (Photoshop CS5) Print Information. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure) Information about the current print settings in the document. The color management options.
		/// </summary>
		PrintInfoCS5 = 1082,
		/// <summary>
		/// (Photoshop CS5) Print Style. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure) Information about the current print style in the document. The printing marks, labels, ornaments, etc.
		/// </summary>
		PrintStyle = 1083,
		/// <summary>
		/// (Photoshop CS5) Macintosh NSPrintInfo. Variable OS specific info for Macintosh. NSPrintInfo. It is recommened that you do not interpret or use this data.
		/// </summary>
		NSPrintInfo = 1084,
		/// <summary>
		/// (Photoshop CS5) Windows DEVMODE. Variable OS specific info for Windows. DEVMODE. It is recommened that you do not interpret or use this data.
		/// </summary>
		Devmode = 1085,
		/// <summary>
		/// (Photoshop CS6) Auto Save File Path. Unicode string. It is recommened that you do not interpret or use this data.
		/// </summary>
		AutoSaveFilePath = 1086,
		/// <summary>
		/// (Photoshop CS6) Auto Save Format. Unicode string. It is recommened that you do not interpret or use this data.
		/// </summary>
		AutoSaveFormat = 1087,
		/// <summary>
		/// (Photoshop CC) Path Selection State. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure) Information about the current path selection state.
		/// </summary>
		PathSelectionState = 1088,
		/// <summary>
		/// Starting mask for Path Information (saved paths). See See Path resource format.
		/// </summary>
		PathInformationMaskBegin = 2000,
		/// <summary>
		/// Ending mask for Path Information (saved paths). See See Path resource format.
		/// </summary>
		PathInformationMaskEnd = 2997,
		/// <summary>
		/// Name of clipping path. See See Path resource format.
		/// </summary>
		ClippingPathName = 2999,
		/// <summary>
		/// (Photoshop CC) Origin Path Info. 4 bytes (descriptor version = 16), Descriptor (see See Descriptor structure) Information about the origin path data.
		/// </summary>
		OriginPathInfo = 3000,
		/// <summary>
		/// Starting mask for Plug-In resource(s). Resources added by a plug-in. See the plug-in API found in the SDK documentation
		/// </summary>
		PlugInResourceMaskBegin = 4000,
		/// <summary>
		/// Ending mask for Plug-In resource(s). Resources added by a plug-in. See the plug-in API found in the SDK documentation
		/// </summary>
		PlugInResourceMaskEnd = 4999,
		/// <summary>
		/// Image Ready variables. XML representation of variables definition
		/// </summary>
		ImageReadyVariables = 7000,
		/// <summary>
		/// Image Ready data sets
		/// </summary>
		ImageReadyDatasets = 7001,
		/// <summary>
		/// Image Ready default selected state
		/// </summary>
		ImageReadyDefaultSelectedState = 7002,
		/// <summary>
		/// Image Ready 7 rollover expanded state
		/// </summary>
		ImageReady7RolloverExpandedState = 7003,
		/// <summary>
		/// Image Ready rollover expanded state
		/// </summary>
		ImageReadyRolloverExpandedState = 7004,
		/// <summary>
		/// Image Ready save layer settings
		/// </summary>
		ImageReadySaveLayerSettings = 7005,
		/// <summary>
		/// Image Ready version
		/// </summary>
		ImageReadyVersion = 7006,
		/// <summary>
		/// (Photoshop CS3) Lightroom workflow, if present the document is in the middle of a Lightroom workflow.
		/// </summary>
		LightroomWorkflow = 8000,
		/// <summary>
		/// Print flags information. 2 bytes version ( = 1), 1 byte center crop marks, 1 byte ( = 0), 4 bytes bleed width value, 2 bytes bleed width scale.
		/// </summary>
		PrintFlags2 = 10000,

		VanishingPointMacintosh = 18012,
		VanishingPointWindows = 29806
	}
}
