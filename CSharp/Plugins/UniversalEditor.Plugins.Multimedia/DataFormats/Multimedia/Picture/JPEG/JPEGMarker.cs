using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.JPEG
{
    /// <summary>
    /// 
    /// </summary>
    public enum JPEGMarker
    {
        /// <summary>
        /// SOI - Start of Image.
        /// </summary>
        StartOfImage = 0xFFD8,
        /// <summary>
        /// SOF0 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
        /// </summary>
		StartOfFrameBaseline = 0xFFC0,
		/// <summary>
		/// SOF1 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline1 = 0xFFC1,
        /// <summary>
        /// SOF2 - Start Of Frame (Progressive DCT). Indicates that this is a progressive DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
        /// </summary>
		StartOfFrameProgressive = 0xFFC2,
		/// <summary>
		/// SOF3 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline3 = 0xFFC3,
        /// <summary>
        /// DHT - Define Huffman Table(s). Specifies one or more Huffman tables.
        /// </summary>
		DefineHuffmanTables = 0xFFC4,
		/// <summary>
		/// SOF5 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline5 = 0xFFC5,
		/// <summary>
		/// SOF6 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline6 = 0xFFC6,
		/// <summary>
		/// SOF7 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline7 = 0xFFC7,
		/// <summary>
		/// SOF9 - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline9 = 0xFFC9,
		/// <summary>
		/// SOFA - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline10 = 0xFFCA,
		/// <summary>
		/// SOFB - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline11 = 0xFFCB,
		/// <summary>
		/// SOFD - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline13 = 0xFFCD,
		/// <summary>
		/// SOFE - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline14 = 0xFFCE,
		/// <summary>
		/// SOFF - Start of Frame (Baseline DCT). Indicates that this is a baseline DCT-based JPEG, and specifies the width, height, number of components, and component subsampling (e.g., 4:2:0).
		/// </summary>
		StartOfFrameBaseline15 = 0xFFCF,
        /// <summary>
        /// DQT - Define Quantization Table(s). Specifies one or more quantization tables.
        /// </summary>
        DefineQuantizationTables = 0xFFDB,
        /// <summary>
        /// DRI - Define Restart Interval. Specifies the interval between RSTn markers, in macroblocks. This marker is followed by two bytes indicating the fixed size so it can be treated like any other variable size segment.
        /// </summary>
        DefineRestartInterval = 0xFFDD,
        /// <summary>
        /// SOS - Start Of Scan. Begins a top-to-bottom scan of the image. In baseline DCT JPEG images, there is generally a single scan. Progressive DCT JPEG images usually contain multiple scans. This marker specifies which slice of data it will contain, and is immediately followed by entropy-coded data.
        /// </summary>
        StartOfScan = 0xFFDA,
        /// <summary>
        /// RST0 - Restart 0. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart0 = 0xFFD0,
        /// <summary>
        /// RST1 - Restart 1. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart1 = 0xFFD1,
        /// <summary>
        /// RST2 - Restart 2. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart2 = 0xFFD2,
        /// <summary>
        /// RST3 - Restart 3. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart3 = 0xFFD3,
        /// <summary>
        /// RST4 - Restart 4. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart4 = 0xFFD4,
        /// <summary>
        /// RST5 - Restart 5. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart5 = 0xFFD5,
        /// <summary>
        /// RST6 - Restart 6. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart6 = 0xFFD6,
        /// <summary>
        /// RST7 - Restart 7. Inserted every r macroblocks, where r is the restart interval set by a DRI marker. Not used if there was no DRI marker. The low 3 bits of the marker code cycle in value from 0 to 7.
        /// </summary>
        Restart7 = 0xFFD7,
        /// <summary>
        /// APP0 - Application-specific 0. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application0 = 0xFFE0,
        /// <summary>
        /// APP1 - Application-specific 1. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application1 = 0xFFE1,
        /// <summary>
        /// APP2 - Application-specific 2. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application2 = 0xFFE2,
        /// <summary>
        /// APP3 - Application-specific 3. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application3 = 0xFFE3,
        /// <summary>
        /// APP4 - Application-specific 4. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application4 = 0xFFE4,
        /// <summary>
        /// APP5 - Application-specific 5. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application5 = 0xFFE5,
        /// <summary>
        /// APP6 - Application-specific 6. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application6 = 0xFFE6,
        /// <summary>
        /// APP7 - Application-specific 7. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application7 = 0xFFE7,
        /// <summary>
        /// APP8 - Application-specific 8. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application8 = 0xFFE8,
        /// <summary>
        /// APP9 - Application-specific 9. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        Application9 = 0xFFE9,
        /// <summary>
        /// APPA - Application-specific A. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        ApplicationA = 0xFFEA,
        /// <summary>
        /// APPB - Application-specific B. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        ApplicationB = 0xFFEB,
        /// <summary>
        /// APPC - Application-specific C. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        ApplicationC = 0xFFEC,
        /// <summary>
        /// APPD - Application-specific D. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        ApplicationD = 0xFFED,
        /// <summary>
        /// APPE - Application-specific E. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        ApplicationE = 0xFFEE,
        /// <summary>
        /// APPF - Application-specific F. For example, an Exif JPEG file uses an APP1 marker to store metadata, laid out in a structure based closely on TIFF.
        /// </summary>
        ApplicationF = 0xFFEF,
        /// <summary>
        /// COM - Comment. Contains a text comment.
        /// </summary>
        Comment = 0xFFFE,
        /// <summary>
        /// EOI - End Of Image.
        /// </summary>
        EndOfImage = 0xFFD9,
        Padding = 0xFFFF
    }
}
