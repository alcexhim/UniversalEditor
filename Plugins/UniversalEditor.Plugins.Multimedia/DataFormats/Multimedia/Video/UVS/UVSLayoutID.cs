using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Video.UVS
{
    public struct UVSLayoutID : IComparable<UVSLayoutID>, IComparable<uint>
    {
        private uint mvarID;
        public uint ID { get { return mvarID; } }
        
        public UVSLayoutID(uint id)
        {
            mvarID = id;
        }

        /// <summary>
        /// The layout is defined in the Data Layout Packet.
        /// </summary>
        public static readonly UVSLayoutID Custom = new UVSLayoutID(0);
        /// <summary>
        /// 8-bpp Y plane, followed by 8-bpp 2×2 V and U planes.
        /// </summary>
        public static readonly UVSLayoutID YV12 = new UVSLayoutID(0x32315659);
        /// <summary>
        /// 8-bpp Y plane, followed by 8-bpp 2×2 U and V planes.
        /// </summary>
        public static readonly UVSLayoutID IYUV = new UVSLayoutID(0x56555949);
        /// <summary>
        /// UV downsampled 2:1 horizontally, ordered Y0 U0 Y1 V0
        /// </summary>
        public static readonly UVSLayoutID YUY2 = new UVSLayoutID(0x32595559);
        /// <summary>
        /// UV downsampled 2:1 horizontally, ordered U0 Y0 V0 Y1
        /// </summary>
        public static readonly UVSLayoutID UYVY = new UVSLayoutID(0x59565955);
        /// <summary>
        /// UV downsampled 2:1 horizontally, ordered Y0 V0 Y1 U0
        /// </summary>
        public static readonly UVSLayoutID YVYU = new UVSLayoutID(0x55595659);
        /// <summary>
        /// 8 bits per component, stored BGR, rows aligned to a 32 bit boundary, rows stored bottom first.
        /// </summary>
        public static readonly UVSLayoutID RGB24DIB = new UVSLayoutID(0x80808081);
        /// <summary>
        /// 8 bits per component, stored BGRx (x is don't care), rows stored bottom first.
        /// </summary>
        public static readonly UVSLayoutID RGB32DIB = new UVSLayoutID(0x80808082);
        /// <summary>
        /// 8 bits per component, stored BGRA, rows stored bottom first.
        /// </summary>
        public static readonly UVSLayoutID ARGBDIB = new UVSLayoutID(0x80808083);

        public int CompareTo(UVSLayoutID other)
        {
            return mvarID.CompareTo(other.ID);
        }
        public int CompareTo(uint other)
        {
            return mvarID.CompareTo(other);
        }

        public override int GetHashCode()
        {
            return mvarID.GetHashCode();
        }

        public static bool operator ==(UVSLayoutID left, UVSLayoutID right)
        {
            return (left.mvarID == right.mvarID);
        }
        public static bool operator !=(UVSLayoutID left, UVSLayoutID right)
        {
            return (left.mvarID != right.mvarID);
        }
    }
}
