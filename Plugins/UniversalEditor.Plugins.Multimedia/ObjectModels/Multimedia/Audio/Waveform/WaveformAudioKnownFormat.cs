// Based on code found at:
// http://www.koders.com/csharp/fidF004FB228F831C6703D8750BFA87599AA6FD245E.aspx?s=cdef%3Afile
// 
// The license for the code is as follows:
// 
// WaveFormatEx.cs:
//
// Author:
//   Brian Nickel (brian.nickel@gmail.com)
//
// Copyright (C) 2007 Brian Nickel
//
// This library is free software; you can redistribute it and/or modify
// it  under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
// USA
//

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public enum WaveformAudioKnownFormat
	{
		/// <summary>
		/// Unknown Wave Format
		/// </summary>
		Unknown = 0x0000,
		/// <summary>
		/// PCM Audio
		/// </summary>
		PCM = 0x0001,
		/// <summary>
		/// Microsoft Adaptive PCM Audio
		/// </summary>
		MicrosoftAdaptivePCM = 0x0002,
		/// <summary>
		/// PCM Audio in IEEE floating-point format
		/// </summary>
		IEEEFloatingPointPCM = 0x0003,
		/// <summary>
		/// Compaq VSELP Audio
		/// </summary>
		CompaqVSELP = 0x0004,
		/// <summary>
		/// IBM CVSD Audio
		/// </summary>
		IBMCVSD = 0x0005,
		/// <summary>
		/// Microsoft ALAW Audio
		/// </summary>
		MicrosoftALAW = 0x0006,
		/// <summary>
		/// Microsoft MULAW Audio
		/// </summary>
		MicrosoftMULAW = 0x0007,
		/// <summary>
		/// Microsoft DTS Audio
		/// </summary>
		MicrosoftDTS = 0x0008,
		/// <summary>
		/// Microsoft DRM Encrypted Audio
		/// </summary>
		MicrosoftDRMEncrypted = 0x0009,
		/// <summary>
		/// Microsoft Speech Audio
		/// </summary>
		MicrosoftSpeech = 0x000A,
		/// <summary>
		/// Microsoft Windows Media RT Voice Audio
		/// </summary>
		MicrosoftWindowsMediaRTVoice = 0x000B,
		/// <summary>
		/// OKI ADPCM Audio
		/// </summary>
		OKIADPCM = 0x0010,
		/// <summary>
		/// Intel ADPCM Audio
		/// </summary>
		IntelADPCM = 0x0011,
		/// <summary>
		/// VideoLogic ADPCM Audio
		/// </summary>
		VideoLogicADPCM = 0x0012,
		/// <summary>
		/// Sierra ADPCM Audio
		/// </summary>
		SierraADPCM = 0x0013,
		/// <summary>
		/// Antex ADPCM Audio
		/// </summary>
		AntexADPCM = 0x0014,
		/// <summary>
		/// DSP DIGISTD Audio
		/// </summary>
		DSPDIGISTD = 0x0015,
		/// <summary>
		/// DSP DIGIFIX Audio
		/// </summary>
		DSPDIGIFIX = 0x0016,
		/// <summary>
		/// Dialogic OKI ADPCM Audio
		/// </summary>
		DialogicOKIADPCM = 0x0017,
		/// <summary>
		/// Media Vision ADPCM Audio for Jazz 16
		/// </summary>
		MediaVisionADPCM = 0x0018,
		/// <summary>
		/// Hewlett-Packard CU Audio
		/// </summary>
		HewlettPackardCU = 0x0019,
		/// <summary>
		/// Hewlett-Packard Dynamic Voice Audio
		/// </summary>
		HewlettPackardDynamicVoice = 0x001A,
		/// <summary>
		/// Yamaha ADPCM Audio
		/// </summary>
		YamahaADPCM = 0x0020,
		/// <summary>
		/// Speech Compression Audio
		/// </summary>
		SpeechCompression = 0x0021,
		/// <summary>
		/// DSP Group True Speech Audio
		/// </summary>
		DSPGroupTrueSpeech = 0x0022,
		/// <summary>
		/// Echo Speech Audio
		/// </summary>
		EchoSpeech = 0x0023,
		/// <summary>
		/// Ahead AF36 Audio
		/// </summary>
		AheadAF36 = 0x0024,
		/// <summary>
		/// Audio Processing Technology Audio
		/// </summary>
		AudioProcessingTechnology = 0x0025,
		/// <summary>
		/// Ahead AF10 Audio
		/// </summary>
		AheadAF10 = 0x0026,
		/// <summary>
		/// Aculab Prosody CTI Speech Card Audio (format 39)
		/// </summary>
		AculabProsodyCTISpeechCard_39 = 0x0027,
		/// <summary>
		/// Merging Technologies LRC Audio
		/// </summary>
		MergingTechnologiesLRC = 0x0028,
		/// <summary>
		/// Dolby AC2 Audio
		/// </summary>
		DolbyAC2 = 0x0030,
		/// <summary>
		/// Microsoft GSM6.10 Audio
		/// </summary>
		MicrosoftGSM610 = 0x0031,
		/// <summary>
		/// Microsoft MSN Audio
		/// </summary>
		MicrosoftMSN = 0x0032,
		/// <summary>
		/// Antex ADPCME Audio
		/// </summary>
		AntexADPCME = 0x0033,
		/// <summary>
		/// Control Resources VQLPC
		/// </summary>
		ControlResourcesVQLPC = 0x0034,
		/// <summary>
		/// DSP REAL Audio
		/// </summary>
		DSPREAL = 0x0035,
		/// <summary>
		/// DSP ADPCM Audio
		/// </summary>
		DSPADPCM = 0x0036,
		/// <summary>
		/// Control Resources CR10 Audio
		/// </summary>
		ControlResourcesCR10 = 0x0037,
		/// <summary>
		/// Natural MicroSystems VBXADPCM Audio
		/// </summary>
		NaturalMicroSystemsVBXADPCM = 0x0038,
		/// <summary>
		/// Roland RDAC Proprietary Audio Format
		/// </summary>
		RolandRDACProprietaryAudioFormat = 0x0039,
		/// <summary>
		/// Echo Speech Proprietary Audio Compression Format
		/// </summary>
		EchoSpeechProprietaryAudioCompressionFormat = 0x003A,
		/// <summary>
		/// Rockwell ADPCM Audio
		/// </summary>
		RockwellADPCM = 0x003B,
		/// <summary>
		/// Rockwell DIGITALK Audio
		/// </summary>
		RockwellDIGITALK = 0x003C,
		/// <summary>
		/// Xebec Proprietary Audio Compression Format
		/// </summary>
		XebecProprietaryAudioCompressionFormat = 0x003D,
		/// <summary>
		/// Antex G721 ADPCM Audio
		/// </summary>
		AntexG721ADPCM = 0x0040,
		/// <summary>
		/// Antex G728 CELP Audio
		/// </summary>
		AntexG728CELP = 0x0041,
		/// <summary>
		/// Microsoft MSG723 Audio
		/// </summary>
		MicrosoftMSG723 = 0x0042,
		/// <summary>
		/// Microsoft MSG723.1 Audio
		/// </summary>
		MicrosoftMSG7231 = 0x0043,
		/// <summary>
		/// Microsoft MSG729 Audio
		/// </summary>
		MicrosoftMSG729 = 0x0044,
		/// <summary>
		/// Microsoft SPG726 Audio
		/// </summary>
		MicrosoftSPG726 = 0x0045,
		/// <summary>
		/// Microsoft MPEG Audio
		/// </summary>
		MicrosoftMPEG = 0x0050,
		/// <summary>
		/// InSoft RT24 Audio
		/// </summary>
		InSoftRT24 = 0x0052,
		/// <summary>
		/// InSoft PAC Audio
		/// </summary>
		InSoftPAC = 0x0053,
		/// <summary>
		/// ISO/MPEG Layer 3 Audio
		/// </summary>
		ISOMPEGLayer3 = 0x0055,
		/// <summary>
		/// Lucent G723 Audio
		/// </summary>
		LucentG723 = 0x0059,
		/// <summary>
		/// Cirrus Logic Audio
		/// </summary>
		CirrusLogic = 0x0060,
		/// <summary>
		/// ESS Technology PCM Audio
		/// </summary>
		ESSTechnologyPCM = 0x0061,
		/// <summary>
		/// Voxware Audio
		/// </summary>
		Voxware = 0x0062,
		/// <summary>
		/// Canopus ATRAC Audio
		/// </summary>
		CanopusATRAC = 0x0063,
		/// <summary>
		/// APICOM G726 ADPCM Audio
		/// </summary>
		APICOMG726ADPCM = 0x0064,
		/// <summary>
		/// APICOM G722 ADPCM Audio
		/// </summary>
		APICOMG722ADPCM = 0x0065,
		/// <summary>
		/// Microsoft DSAT Display Audio
		/// </summary>
		MicrosoftDSATDisplay = 0x0067,
		/// <summary>
		/// Voxware Byte Aligned Audio
		/// </summary>
		VoxwareByteAligned = 0x0069,
		/// <summary>
		/// Voxware AC8 Audio
		/// </summary>
		VoxwareAC8 = 0x0070,
		/// <summary>
		/// Voxware AC10 Audio
		/// </summary>
		VoxwareAC10 = 0x0071,
		/// <summary>
		/// Voxware AC16 Audio
		/// </summary>
		VoxwareAC16 = 0x0072,
		/// <summary>
		/// Voxware AC20 Audio
		/// </summary>
		VoxwareAC20 = 0x0073,
		/// <summary>
		/// Voxware RT24 Audio
		/// </summary>
		VoxwareRT24 = 0x0074,
		/// <summary>
		/// Voxware RT29 Audio
		/// </summary>
		VoxwareRT29 = 0x0075,
		/// <summary>
		/// Voxware RT29HW Audio
		/// </summary>
		VoxwareRT29HW = 0x0076,
		/// <summary>
		/// Voxware VR12 Audio
		/// </summary>
		VoxwareVR12 = 0x0077,
		/// <summary>
		/// Voxware VR18 Audio
		/// </summary>
		VoxwareVR18 = 0x0078,
		/// <summary>
		/// Voxware TQ40 Audio
		/// </summary>
		VoxwareTQ40 = 0x0079,
		/// <summary>
		/// Voxware SC3 Audio (122)
		/// </summary>
		VoxwareSC3_122 = 0x007A,
		/// <summary>
		/// Voxware SC3 Audio (123)
		/// </summary>
		VoxwareSC3_123 = 0x007B,
		/// <summary>
		/// SoftSound Audio
		/// </summary>
		SoftSound = 0x0080,
		/// <summary>
		/// Voxware TQ60 Audio
		/// </summary>
		VoxwareTQ60 = 0x0081,
		/// <summary>
		/// Microsoft RT24 Audio
		/// </summary>
		MicrosoftRT24 = 0x0082,
		/// <summary>
		/// AT&T G729A Audio
		/// </summary>
		ATTG729A = 0x0083,
		/// <summary>
		/// Motion Pixels MVI2 Audio
		/// </summary>
		MotionPixelsMVI2 = 0x0084,
		/// <summary>
		/// Datafusion Systems G726 Audio
		/// </summary>
		DatafusionSystemsG726 = 0x0085,
		/// <summary>
		/// Datafusion Systems G610 Audio
		/// </summary>
		DatafusionSystemsG610 = 0x0086,
		/// <summary>
		/// Iterated Systems Audio
		/// </summary>
		IteratedSystems = 0x0088,
		/// <summary>
		/// OnLive! Audio
		/// </summary>
		OnLive = 0x0089,
		/// <summary>
		/// Multitude FT SX20 Audio
		/// </summary>
		MultitudeFTSX20 = 0x008A,
		/// <summary>
		/// InfoCom ITS ACM G721 Audio
		/// </summary>
		InfoComITSACMG721 = 0x008B,
		/// <summary>
		/// Convedia G729 Audio
		/// </summary>
		ConvediaG729 = 0x008C,
		/// <summary>
		/// Congruency Audio
		/// </summary>
		Congruency = 0x008D,
		/// <summary>
		/// Siemens Business Communications 24 Audio
		/// </summary>
		SiemensBusinessCommunications24 = 0x0091,
		/// <summary>
		/// Sonic Foundary Dolby AC3 Audio
		/// </summary>
		SonicFoundryDolbyAC3 = 0x0092,
		/// <summary>
		/// MediaSonic G723 Audio
		/// </summary>
		MediaSonicG723 = 0x0093,
		/// <summary>
		/// Aculab Prosody CTI Speech Card Audio (format 148)
		/// </summary>
		AculabProsodyCTISpeechCard_148 = 0x0094,
		/// <summary>
		/// ZyXEL ADPCM
		/// </summary>
		ZyXELADPCM = 0x0097,
		/// <summary>
		/// Philips Speech Processing LPCBB Audio
		/// </summary>
		PhilipsSpeechProcessingLPCBB = 0x0098,
		/// <summary>
		/// Studer Professional PACKED Audio
		/// </summary>
		StuderProfessionalPACKED = 0x0099,
		/// <summary>
		/// Malden Electronics Phony Talk Audio
		/// </summary>
		MaldenElectronicsPhonyTalk = 0x00A0,
		/// <summary>
		/// Racal Recorder GSM Audio
		/// </summary>
		RacalRecorderGSM = 0x00A1,
		/// <summary>
		/// Racal Recorder G720.a Audio
		/// </summary>
		RacalRecorderG720a = 0x00A2,
		/// <summary>
		/// Racal G723.1 Audio
		/// </summary>
		RacalG7231 = 0x00A3,
		/// <summary>
		/// Racal Tetra ACELP Audio
		/// </summary>
		RacalTetraACELP = 0x00A4,
		/// <summary>
		/// NEC AAC Audio
		/// </summary>
		NECAAC = 0x00B0,
		/// <summary>
		/// Rhetorex ADPCM Audio
		/// </summary>
		RhetorexADPCM = 0x0100,
		/// <summary>
		/// BeCubed IRAT Audio
		/// </summary>
		BeCubedIRAT = 0x0101,
		/// <summary>
		/// Vivo G723 Audio
		/// </summary>
		VivoG723 = 0x0111,
		/// <summary>
		/// Vivo Siren Audio
		/// </summary>
		VivoSiren = 0x0112,
		/// <summary>
		/// Philips Speech Processing CELP Audio
		/// </summary>
		PhilipsSpeechProcessingCELP = 0x0120,
		/// <summary>
		/// Philips Speech Processing GRUNDIG Audio
		/// </summary>
		PhilipsSpeechProcessingGRUNDIG = 0x0121,
		/// <summary>
		/// Digital Equipment Corporation G723 Audio
		/// </summary>
		DigitalEquipmentCorporationG723 = 0x0123,
		/// <summary>
		/// Sanyo LD-ADPCM Audio
		/// </summary>
		SanyoLDADPCM = 0x0125,
		/// <summary>
		/// Sipro Lab ACELPNET Audio
		/// </summary>
		SiproLabACELPNET = 0x0130,
		/// <summary>
		/// Sipro Lab ACELP4800 Audio
		/// </summary>
		SiproLabACELP4800 = 0x0131,
		/// <summary>
		/// Sipro Lab ACELP8v3 Audio
		/// </summary>
		SiproLabACELP8v3 = 0x0132,
		/// <summary>
		/// Sipro Lab G729 Audio
		/// </summary>
		SiproLabG729 = 0x0133,
		/// <summary>
		/// Sipro Lab G729A Audio
		/// </summary>
		SiproLabG729A = 0x0134,
		/// <summary>
		/// Sipro Lab KELVIN Audio
		/// </summary>
		SiproLabKELVIN = 0x0135,
		/// <summary>
		/// VoiceAge AMR Audio
		/// </summary>
		VoiceAgeAMR = 0x0136,
		/// <summary>
		/// Dictaphone G726 ADPCM Audio
		/// </summary>
		DictaphoneG726ADPCM = 0x0140,
		/// <summary>
		/// Dictaphone CELP68 Audio
		/// </summary>
		DictaphoneCELP68 = 0x0141,
		/// <summary>
		/// Dictaphone CELP54 Audio
		/// </summary>
		DictaphoneCELP54 = 0x0142,
		/// <summary>
		/// QUALCOMM Pure Voice Audio
		/// </summary>
		QUALCOMMPureVoice = 0x0150,
		/// <summary>
		/// QUALCOMM Half Rate Audio
		/// </summary>
		QUALCOMMHalfRate = 0x0151,
		/// <summary>
		/// Ring Zero TUBGSM Audio
		/// </summary>
		RingZeroTUBGSM = 0x0155,
		/// <summary>
		/// Microsoft WMA1 Audio
		/// </summary>
		MicrosoftWMA1 = 0x0160,
		/// <summary>
		/// Microsoft WMA2 Audio
		/// </summary>
		MicrosoftWMA2 = 0x0161,
		/// <summary>
		/// Microsoft Multichannel WMA Audio
		/// </summary>
		MicrosoftWMAMultichannel = 0x0162,
		/// <summary>
		/// Microsoft Lossless WMA Audio
		/// </summary>
		MicrosoftWMALossless = 0x0163,
		/// <summary>
		/// Unisys NAP ADPCM Audio
		/// </summary>
		UnisysNAPADPCM = 0x0170,
		/// <summary>
		/// Unisys NAP ULAW Audio
		/// </summary>
		UnisysNAPULAW = 0x0171,
		/// <summary>
		/// Unisys NAP ALAW Audio
		/// </summary>
		UnisysNAPALAW = 0x0172,
		/// <summary>
		/// Unisys NAP 16K Audio
		/// </summary>
		UnisysNAP16K = 0x0173,
		/// <summary>
		/// SysCom ACM SYC008 Audio
		/// </summary>
		SysComACMSYC008 = 0X0174,
		/// <summary>
		/// SysCom ACM SYC701 G726L Audio
		/// </summary>
		SysComACMSYC701G726L = 0x0175,
		/// <summary>
		/// SysCom ACM SYC701 CELP54 Audio
		/// </summary>
		SysComACMSYC701CELP54 = 0x0176,
		/// <summary>
		/// SysCom ACM SYC701 CELP68 Audio
		/// </summary>
		SysComACMSYC701CELP68 = 0x0177,
		/// <summary>
		/// Knowledge Adventure ADPCM Audio
		/// </summary>
		KnowledgeAdventureADPCM = 0x0178,
		/// <summary>
		/// MPEG2 AAC Audio
		/// </summary>
		MPEG2AAC = 0x0180,
		/// <summary>
		/// Digital Theater Systems DTS DS Audio
		/// </summary>
		DigitalTheaterSystemsDTSDS = 0x0190,
		/// <summary>
		/// Innings ADPCM Audio
		/// </summary>
		InningsADPCM = 0x1979,
		/// <summary>
		/// Creative ADPCM Audio
		/// </summary>
		CreativeADPCM = 0x0200,
		/// <summary>
		/// Creative FastSpeech8 Audio
		/// </summary>
		CreativeFastSpeech8 = 0x0202,
		/// <summary>
		/// Creative FastSpeech10 Audio
		/// </summary>
		CreativeFastSpeech10 = 0x0203,
		/// <summary>
		/// UHER ADPCM Audio
		/// </summary>
		UHERADPCM = 0x0210,
		/// <summary>
		/// Quarterdeck Audio
		/// </summary>
		Quarterdeck = 0x0220,
		/// <summary>
		/// I-Link VC Audio
		/// </summary>
		ILinkVC = 0x0230,
		/// <summary>
		/// Aureal RAW SPORT Audio
		/// </summary>
		AurealRAWSPORT = 0x0240,
		/// <summary>
		/// Interactive Products HSX Audio
		/// </summary>
		InteractiveProductsHSX = 0x0250,
		/// <summary>
		/// Interactive Products RPELP Audio
		/// </summary>
		InteractiveProductsRPELP = 0x0251,
		/// <summary>
		/// Consistens Software CS2 Audio
		/// </summary>
		ConsistensSoftwareCS2 = 0x0260,
		/// <summary>
		/// Sony SCX Audio
		/// </summary>
		SonySCX = 0x0270,
		/// <summary>
		/// Sony SCY Audio
		/// </summary>
		SonySCY = 0x0271,
		/// <summary>
		/// Sony ATRAC3 Audio
		/// </summary>
		SonyATRAC3 = 0x0272,
		/// <summary>
		/// Sony SPC Audio
		/// </summary>
		SonySPC = 0x0273,
		/// <summary>
		/// Telum Audio
		/// </summary>
		Telum = 0x0280,
		/// <summary>
		/// Telum IA Audio
		/// </summary>
		TelumIA = 0x0281,
		/// <summary>
		/// Norcom Voice Systems ADPCM Audio
		/// </summary>
		NorcomVoiceSystemsADPCM = 0x0285,
		/// <summary>
		/// Fujitsu FM TOWNS SND Audio
		/// </summary>
		FujitsuFMTOWNSSND = 0x0300,
		/// <summary>
		/// Unknown Fujitsu Audio (format 769)
		/// </summary>
		UnknownFujitsu_769 = 0x0301,
		/// <summary>
		/// Unknown Fujitsu Audio (format 770)
		/// </summary>
		UnknownFujitsu_770 = 0x0302,
		/// <summary>
		/// Unknown Fujitsu Audio (format 771)
		/// </summary>
		UnknownFujitsu_771 = 0x0303,
		/// <summary>
		/// Unknown Fujitsu Audio (format 772)
		/// </summary>
		UnknownFujitsu_772 = 0x0304,
		/// <summary>
		/// Unknown Fujitsu Audio (format 773)
		/// </summary>
		UnknownFujitsu_773 = 0x0305,
		/// <summary>
		/// Unknown Fujitsu Audio (format 774)
		/// </summary>
		UnknownFujitsu_774 = 0x0306,
		/// <summary>
		/// Unknown Fujitsu Audio (format 775)
		/// </summary>
		UnknownFujitsu_775 = 0x0307,
		/// <summary>
		/// Unknown Fujitsu Audio (format 776)
		/// </summary>
		UnknownFujitsu_776 = 0x0308,
		/// <summary>
		/// Micronas Semiconductors Development Audio
		/// </summary>
		MicronasSemiconductorsDevelopment = 0x0350,
		/// <summary>
		/// Micronas Semiconductors CELP833 Audio
		/// </summary>
		MicronasSemiconductorsCELP833 = 0x0351,
		/// <summary>
		/// Brooktree Digital Audio
		/// </summary>
		BrooktreeDigital = 0x0400,
		/// <summary>
		/// QDesign Audio
		/// </summary>
		QDesign = 0x0450,
		/// <summary>
		/// AT&T VME VMPCM Audio
		/// </summary>
		ATTVMEVMPCM = 0x0680,
		/// <summary>
		/// AT&T TPC Audio
		/// </summary>
		ATTTPC = 0x0681,
		/// <summary>
		/// Ing. C. Olivetti & C., S.p.A. GSM Audio
		/// </summary>
		OlivettiGSM = 0x1000,
		/// <summary>
		/// Ing. C. Olivetti & C., S.p.A. ADPCM Audio
		/// </summary>
		OlivettiADPCM = 0x1001,
		/// <summary>
		/// Ing. C. Olivetti & C., S.p.A. CELP Audio
		/// </summary>
		OlivettiCELP = 0x1002,
		/// <summary>
		/// Ing. C. Olivetti & C., S.p.A. SBC Audio
		/// </summary>
		OlivettiSBC = 0x1003,
		/// <summary>
		/// Ing. C. Olivetti & C., S.p.A. OPR Audio
		/// </summary>
		OlivettiOPR = 0x1004,
		/// <summary>
		/// Lernout & Hauspie Audio
		/// </summary>
		LernoutHauspie = 0x1100,
		/// <summary>
		/// Lernout & Hauspie CELP Audio
		/// </summary>
		LernoutHauspieCELP = 0x1101,
		/// <summary>
		/// Lernout & Hauspie SB8 Audio
		/// </summary>
		LernoutHauspieSB8 = 0x1102,
		/// <summary>
		/// Lernout & Hauspie SB12 Audio
		/// </summary>
		LernoutHauspieSB12 = 0x1103,
		/// <summary>
		/// Lernout & Hauspie SB16 Audio
		/// </summary>
		LernoutHauspieSB16 = 0x1104,
		/// <summary>
		/// Norris Audio
		/// </summary>
		Norris = 0x1400,
		/// <summary>
		/// AT&T Soundspace Musicompress Audio
		/// </summary>
		ATTSoundspaceMusicompress = 0x1500,
		/// <summary>
		/// Sonic Foundry Lossless Audio
		/// </summary>
		SonicFoundryLossless = 0x1971,
		/// <summary>
		/// FAST Multimedia DVM Audio
		/// </summary>
		FASTMultimediaDVM = 0x2000,
		/// <summary>
		/// Divio AAC
		/// </summary>
		DivioAAC = 0x4143,
		/// <summary>
		/// Nokia Adaptive Multirate Audio
		/// </summary>
		NokiaAdaptiveMultirate = 0x4201,
		/// <summary>
		/// Divio G726 Audio
		/// </summary>
		DivioG726 = 0x4243,
		/// <summary>
		/// 3Com NBX Audio
		/// </summary>
		ThreeComNBX = 0x7000,
		/// <summary>
		/// Microsoft Adaptive Multirate Audio
		/// </summary>
		MicrosoftAdaptiveMultirate = 0x7A21,
		/// <summary>
		/// Microsoft Adaptive Multirate Audio with silence detection
		/// </summary>
		MicrosoftAdaptiveMultirateSilenceDetect = 0x7A22,
		/// <summary>
		/// Comverse Infosys G723 1 Audio
		/// </summary>
		ComverseInfosysG7231 = 0xA100,
		/// <summary>
		/// Comverse Infosys AVQSBC Audio
		/// </summary>
		ComverseInfosysAVQSBC = 0xA101,
		/// <summary>
		/// Comverse Infosys OLDSBC Audio
		/// </summary>
		ComverseInfosysOLDSBC = 0xA102,
		/// <summary>
		/// Symbol Technology G729A Audio
		/// </summary>
		SymbolTechnologyG729A = 0xA103,
		/// <summary>
		/// VoiceAge AMR WB Audio
		/// </summary>
		VoiceAgeAMRWB = 0xA104,
		/// <summary>
		/// Ingenient G726 Audio
		/// </summary>
		IngenientG726 = 0xA105,
		/// <summary>
		/// ISO/MPEG-4 Advanced Audio Coding
		/// </summary>
		ISOMPEG4AdvancedAudioCoding = 0xA106,
		/// <summary>
		/// Encore G726 Audio
		/// </summary>
		EncoreG726 = 0xA107,
		/// <summary>
		/// Extensible or custom format.
		/// </summary>
		/// <remarks>When the format is WAVE_FORMAT_EXTENSIBLE (0xFFFE or 65534) then one should refer to the sub format for how to read the WAV.</remarks>
		Extensible = 0xFFFE
	}
}
