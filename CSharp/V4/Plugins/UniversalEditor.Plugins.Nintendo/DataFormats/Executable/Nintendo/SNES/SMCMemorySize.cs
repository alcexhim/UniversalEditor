using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// A company that has licensed the use of the Nintendo SNES game console for development.
	/// </summary>
	/// <completionlist cref="SMCMemorySizes" />
	public class SMCMemorySize
	{
		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the memory size.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private byte mvarValue = 0;
		/// <summary>
		/// The single-byte code that represents this memory size in the Nintendo SNES game console.
		/// </summary>
		public byte Value { get { return mvarValue; } set { mvarValue = value; } }

		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> with the given value if valid.
		/// </summary>
		/// <param name="value">The memory size code to search on.</param>
		/// <returns>If the code is known, returns an instance of the associated <see cref="SMCMemorySize" />. Otherwise, returns null.</returns>
		public static SMCMemorySize FromCode(byte value)
		{
			Type t = typeof(SMCMemorySizes);
			
			MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
			PropertyInfo[] properties = t.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType == typeof(SMCMemorySize))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (getMethod != null && (getMethod.Attributes & methodAttributes) == methodAttributes)
					{
						object[] index = null;
						SMCMemorySize val = (SMCMemorySize)propertyInfo.GetValue(null, index);

						if (val.Value == value) return val;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Creates a new instance of <see cref="SMCMemorySize" /> with the given title and value.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="value"></param>
		public SMCMemorySize(string title, byte value)
		{
			mvarTitle = title;
			mvarValue = value;
		}

		/// <summary>
		/// Translates this <see cref="SMCLicensee" /> into a human-readable string, including the
		/// title of the region and the country code.
		/// </summary>
		/// <returns>A human-readable string representing this <see cref="SMCLicensee" />.</returns>
		public override string ToString()
		{
			return mvarTitle + " (0x" + mvarValue.ToString("X").PadLeft(2, '0') + ")";
		}
	}
	/// <summary>
	/// Licensees that have been defined by the SNES SMC data format. This class cannot be inherited.
	/// </summary>
	public sealed class SMCMemorySizes
	{
		private static SMCMemorySize mvarNone = new SMCMemorySize("(none)", 0x00);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing no memory.
		/// </summary>
		public static SMCMemorySize None { get { return mvarNone; } }

		private static SMCMemorySize mvarK2 = new SMCMemorySize("2 KB (2048 bytes)", 0x01);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 2KB (2048 bytes) worth of memory. This is the amount of RAM used in "Super Mario World".
		/// </summary>
		public static SMCMemorySize K2 { get { return mvarK2; } }

		private static SMCMemorySize mvarK4 = new SMCMemorySize("4 KB (4096 bytes)", 0x02);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 4KB (4096 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize K4 { get { return mvarK4; } }

		private static SMCMemorySize mvarK8 = new SMCMemorySize("8 KB (8192 bytes)", 0x03);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 8KB (8192 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize K8 { get { return mvarK8; } }

		private static SMCMemorySize mvarK16 = new SMCMemorySize("16 KB (16 384 bytes)", 0x04);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 16KB (16 384 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize K16 { get { return mvarK16; } }

		private static SMCMemorySize mvarK32 = new SMCMemorySize("32 KB (32 768 bytes)", 0x05);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 32KB (32 768 bytes) worth of memory. This is the amount of RAM used in "Mario Paint".
		/// </summary>
		public static SMCMemorySize K32 { get { return mvarK32; } }

		private static SMCMemorySize mvarK64 = new SMCMemorySize("64 KB (65 536 bytes)", 0x06);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 64KB (65 536 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize K64 { get { return mvarK64; } }

		private static SMCMemorySize mvarK128 = new SMCMemorySize("128 KB (131 072 bytes)", 0x07);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 128KB (131 072 bytes) worth of memory. This is the amount of RAM used in "Dezaemon - Kaite Tsukutte Asoberu".
		/// </summary>
		public static SMCMemorySize K128 { get { return mvarK128; } }

		private static SMCMemorySize mvarK256 = new SMCMemorySize("256 KB (262 144 bytes)", 0x08);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 256KB (262 144 bytes) worth of memory. This is the minimum amount of ROM.
		/// </summary>
		public static SMCMemorySize K256 { get { return mvarK256; } }

		private static SMCMemorySize mvarK512 = new SMCMemorySize("512 KB (524 288 bytes)", 0x09);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 512KB (524 288 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize K512 { get { return mvarK512; } }

		private static SMCMemorySize mvarM1 = new SMCMemorySize("1 MB (1 048 576 bytes)", 0x0A);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 1MB (1 048 576 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize M1 { get { return mvarM1; } }

		private static SMCMemorySize mvarM2 = new SMCMemorySize("2 MB (2 097 152 bytes)", 0x0B);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 2MB (2 097 152 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize M2 { get { return mvarM2; } }

		private static SMCMemorySize mvarM4 = new SMCMemorySize("4 MB (4 194 304 bytes)", 0x0C);
		/// <summary>
		/// Gets the <see cref="SMCMemorySize" /> representing 4MB (4 194 304 bytes) worth of memory.
		/// </summary>
		public static SMCMemorySize M4 { get { return mvarM4; } }
	}
}
