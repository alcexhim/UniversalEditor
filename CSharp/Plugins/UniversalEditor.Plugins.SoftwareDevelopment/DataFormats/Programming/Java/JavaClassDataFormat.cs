using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.Programming.Java
{
	public class JavaClassDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(CodeObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CodeObjectModel com = (objectModel as CodeObjectModel);
			
			CodeClassElement clss = new CodeClassElement();

			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;

			uint magic = br.ReadUInt32(); // 0xCAFEBABE
			ushort minor_version = br.ReadUInt16();
			ushort major_version = br.ReadUInt16();
			ushort constant_pool_count = br.ReadUInt16();

			List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();

			for (ushort i = 0; i < constant_pool_count; i++)
			{
				byte tag = br.ReadByte();
				switch (tag)
				{
					case 7: // CONSTANT_Class
					{
						/*
							The value of the name_index item must be a valid index into the constant_pool table. The
							constant_pool entry at that index must be a CONSTANT_Utf8_info (§4.4.7) structure representing
							a valid fully qualified class or interface name (§2.8.1) encoded in internal form (§4.2).
						 */
						ushort name_index = br.ReadUInt16();

						Dictionary<string, object> keys = new Dictionary<string, object>();
						keys.Add("type", "Class");
						keys.Add("name_index", name_index);
						objects.Add(keys);
						break;
					}
					case 9: // CONSTANT_Fieldref
					case 10: // CONSTANT_Methodref
					case 11: // CONSTANT_InterfaceMethodref
					{
						/*
							The value of the class_index item must be a valid index into the constant_pool table. The
							constant_pool entry at that index must be a CONSTANT_Class_info (§4.4.1) structure representing
							the class or interface type that contains the declaration of the field or method.
						 
							The class_index item of a CONSTANT_Methodref_info structure must be a class type, not an
							interface type. The class_index item of a CONSTANT_InterfaceMethodref_info structure must be an
							interface type. The class_index item of a CONSTANT_Fieldref_info structure may be either a class
							type or an interface type.
						 */
						ushort class_index = br.ReadUInt16();

						/*
							The value of the name_and_type_index item must be a valid index into the constant_pool table. The
							constant_pool entry at that index must be a CONSTANT_NameAndType_info (§4.4.6) structure. This
							constant_pool entry indicates the name and descriptor of the field or method. In a
							CONSTANT_Fieldref_info the indicated descriptor must be a field descriptor (§4.3.2). Otherwise,
							the indicated descriptor must be a method descriptor (§4.3.3). If the name of the method of a
							CONSTANT_Methodref_info structure begins with a' <' ('\u003c'), then the name must be the special
							name <init>, representing an instance initialization method (§3.9). Such a method must return no
							value.
						 */
						ushort name_and_type_index = br.ReadUInt16();

						Dictionary<string, object> keys = new Dictionary<string, object>();
						keys.Add("type", "InterfaceMethodref");
						keys.Add("class_index", class_index);
						keys.Add("name_and_type_index", name_and_type_index);
						objects.Add(keys);
						break;
					}
					case 8: // CONSTANT_String
					{
						/*
							The value of the string_index item must be a valid index into the constant_pool table. The
							constant_pool entry at that index must be a CONSTANT_Utf8_info (§4.4.7) structure representing
							the sequence of characters to which the String object is to be initialized.
						 */
						ushort string_index = br.ReadUInt16();

						Dictionary<string, object> keys = new Dictionary<string, object>();
						keys.Add("type", "String");
						keys.Add("string_index", string_index);
						objects.Add(keys);
						break;
					}
					case 3: // CONSTANT_Integer
					case 4: // CONSTANT_Float
					{
						uint bytes = br.ReadUInt32();
						/*
							The bytes item of the CONSTANT_Integer_info structure represents the value of the int constant. The bytes of the value are stored in big-endian (high byte first) order.

							The bytes item of the CONSTANT_Float_info structure represents the value of the float constant in IEEE 754 floating-point single format (§3.3.2). The bytes of the single format representation are stored in big-endian (high byte first) order.

							The value represented by the CONSTANT_Float_info structure is determined as follows. The bytes of the value are first converted into an int constant bits. Then:

								If bits is 0x7f800000, the float value will be positive infinity.
								If bits is 0xff800000, the float value will be negative infinity.
								If bits is in the range 0x7f800001 through 0x7fffffff or in the range 0xff800001 through 0xffffffff, the float value will be NaN.
								In all other cases, let s, e, and m be three values that might be computed from bits: 

									int s = ((bits >> 31) == 0) ? 1 : -1;
									int e = ((bits >> 23) & 0xff);
									int m = (e == 0) ?
											(bits & 0x7fffff) << 1 :
											(bits & 0x7fffff) | 0x800000;

							Then the float value equals the result of the mathematical expression s·m·2^(e-150).
						*/

						float value = 0;
						if (bytes == 0x7F800000)
						{
							value = Single.PositiveInfinity;
						}
						else if (bytes == 0xFF800000)
						{
							value = Single.NegativeInfinity;
						}
						else if ((bytes >= 0x7F800001 && bytes <= 0x7FFFFFFF) || (bytes >= 0xFF800001 && bytes <= 0xFFFFFFFF))
						{
							value = Single.NaN;
						}
						else
						{
							int s = ((bytes >> 31) == 0) ? 1 : -1;
							uint e = ((bytes >> 23) & 0xff);
							uint m = (e == 0) ?
									(bytes & 0x7fffff) << 1 :
									(bytes & 0x7fffff) | 0x800000;

							value = (s * m * (float)Math.Pow(2, e - 150));
						}

						if (tag == 3)
						{
							Dictionary<string, object> keys = new Dictionary<string, object>();
							keys.Add("type", "Integer");
							keys.Add("value", (int)value);
							objects.Add(keys);
						}
						else if (tag == 4)
						{
							Dictionary<string, object> keys = new Dictionary<string, object>();
							keys.Add("type", "Float");
							keys.Add("value", value);
							objects.Add(keys);
						}
						break;
					}
					case 5: // CONSTANT_Long
					case 6: // CONSTANT_Double
					{
						uint high_bytes = br.ReadUInt32();
						uint low_bytes = br.ReadUInt32();

						/*
							All 8-byte constants take up two entries in the constant_pool table of the class file. If a
							CONSTANT_Long_info or CONSTANT_Double_info structure is the item in the constant_pool table at
							index n, then the next usable item in the pool is located at index n+2. The constant_pool index
							n+1 must be valid but is considered unusable.

							The items of these structures are as follows:

							tag
								The tag item of the CONSTANT_Long_info structure has the value CONSTANT_Long (5).

								The tag item of the CONSTANT_Double_info structure has the value CONSTANT_Double (6).

							high_bytes, low_bytes
								The unsigned high_bytes and low_bytes items of the CONSTANT_Long_info structure together
								represent the value of the long constant ((long) high_bytes << 32) + low_bytes, where the
								bytes of each of high_bytes and low_bytes are stored in big-endian (high byte first) order.

								The high_bytes and low_bytes items of the CONSTANT_Double_info structure together represent
								the double value in IEEE 754 floating-point double format (§3.3.2). The bytes of each item
								are stored in big-endian (high byte first) order.

								The value represented by the CONSTANT_Double_info structure is determined as follows. The
								high_bytes and low_bytes items are first converted into the long constant bits, which is
								equal to ((long) high_bytes << 32) + low_bytes. Then:

									If bits is 0x7ff0000000000000L, the double value will be positive infinity.
									If bits is 0xfff0000000000000L, the double value will be negative infinity.
									If bits is in the range 0x7ff0000000000001L through 0x7fffffffffffffffL or in the range 0xfff0000000000001L through 0xffffffffffffffffL, the double value will be NaN.
									In all other cases, let s, e, and m be three values that might be computed from bits: 

										int s = ((bits >> 63) == 0) ? 1 : -1;
										int e = (int)((bits >> 52) & 0x7ffL);
										long m = (e == 0) ?
											(bits & 0xfffffffffffffL) << 1 :
											(bits & 0xfffffffffffffL) | 0x10000000000000L;

									Then the floating-point value equals the double value of the mathematical expression s·m·2^(e-1075).
						 */

						long longValue = (high_bytes << 32) + low_bytes;

						int s = ((longValue >> 63) == 0) ? 1 : -1;
						int e = (int)((longValue >> 52) & 0x7ffL);
						long m = (e == 0) ?
							(longValue & 0xfffffffffffffL) << 1 :
							(longValue & 0xfffffffffffffL) | 0x10000000000000L;

						float floatValue = (float)(s * m * Math.Pow(2, (e-1075)));

						if (tag == 5)
						{
							Dictionary<string, object> keys = new Dictionary<string, object>();
							keys.Add("type", "Long");
							keys.Add("value", longValue);
							objects.Add(keys);
						}
						else if (tag == 6)
						{
							Dictionary<string, object> keys = new Dictionary<string, object>();
							keys.Add("type", "Float");
							keys.Add("value", floatValue);
							objects.Add(keys);
						}
						break;
					}
					case 12: // CONSTANT_NameAndType
					{
						/*
							The value of the name_index item must be a valid index into the constant_pool table. The
							constant_pool entry at that index must be a CONSTANT_Utf8_info (§4.4.7) structure representing
							either a valid field or method name (§2.7) stored as a simple name (§2.7.1), that is, as a Java
							programming language identifier (§2.2) or as the special method name <init> (§3.9).
						 */
						ushort name_index = br.ReadUInt16();
						/*
							The value of the descriptor_index item must be a valid index into the constant_pool table. The
							constant_pool entry at that index must be a CONSTANT_Utf8_info (§4.4.7) structure representing
							a valid field descriptor (§4.3.2) or method descriptor (§4.3.3).
						 */
						ushort descriptor_index = br.ReadUInt16();

						Dictionary<string, object> keys = new Dictionary<string, object>();
						keys.Add("type", "NameAndType");
						keys.Add("name_index", name_index);
						keys.Add("descriptor_index", descriptor_index);
						objects.Add(keys);
						break;
					}
					case 1: // CONSTANT_Utf8
					{
						/*
							The value of the length item gives the number of bytes in the bytes array (not the length of the
							resulting string). The strings in the CONSTANT_Utf8_info structure are not null-terminated.
						 */
						ushort length = br.ReadUInt16();
						/*
							The bytes array contains the bytes of the string. No byte may have the value (byte)0 or lie in
							the range (byte)0xf0 - (byte)0xff.
						 */
						byte[] data = br.ReadBytes(length);

						string value = System.Text.Encoding.UTF8.GetString(data);

						Dictionary<string, object> keys = new Dictionary<string, object>();
						keys.Add("type", "Utf8");
						keys.Add("value", value);
						objects.Add(keys);
						break;
					}
				}
			}


			ushort access_flags = br.ReadUInt16();
			ushort this_class = br.ReadUInt16();
			ushort super_class = br.ReadUInt16();
			ushort interfaces_count = br.ReadUInt16();
			for (ushort i = 0; i < interfaces_count; i++)
			{
				ushort interfaceIndex = br.ReadUInt16(); // index into constant pool
			}
			ushort fields_count = br.ReadUInt16();
			for (ushort i = 0; i < fields_count; i++)
			{
				/*
					Fields of classes may set any of the flags in Table 4.4. However, a specific field of a class may have
					at most one of its ACC_PRIVATE, ACC_PROTECTED, and ACC_PUBLIC flags set (§2.7.4) and may not have both
					its ACC_FINAL and ACC_VOLATILE flags set (§2.9.1).
				 */
				ushort field_access_flags = br.ReadUInt16();
				if ((field_access_flags & 0x0001) == 0x0001)
				{
					// ACC_PUBLIC: Declared public; may be accessed from outside its package.
				}
				if ((field_access_flags & 0x0002) == 0x0002)
				{
					// ACC_PRIVATE: Declared private; usable only within the defining class.
				}
				if ((field_access_flags & 0x0004) == 0x0004)
				{
					// ACC_PROTECTED: Declared protected; may be accessed within subclasses.
				}
				if ((field_access_flags & 0x0008) == 0x0008)
				{
					// ACC_STATIC: Declared static.
				}
				if ((field_access_flags & 0x0010) == 0x0010)
				{
					// ACC_FINAL: Declared final; no further assignment after initialization.
				}
				else if ((field_access_flags & 0x0040) == 0x0040)
				{
					// ACC_VOLATILE: Declared volatile; cannot be cached.
				}
				else if ((field_access_flags & 0x0080) == 0x0080)
				{
					// ACC_TRANSIENT: Declared transient; not written or read by a persistent object manager.
				}

				ushort field_name_index = br.ReadUInt16();
				ushort field_descriptor_index = br.ReadUInt16();
				ushort field_attributes_count = br.ReadUInt16();

				for (ushort j = 0; j < field_attributes_count; j++)
				{
					ushort attribute_name_index = br.ReadUInt16();
					uint attribute_length = br.ReadUInt32();
					byte[] attribute_info = br.ReadBytes(attribute_length);
				}
			}

			ushort methods_count = br.ReadUInt16();
			for (ushort i = 0; i < methods_count; i++)
			{
				/*
					Methods of classes may set any of the flags in Table 4.5. However, a specific method of a class may have
					at most one of its ACC_PRIVATE, ACC_PROTECTED, and ACC_PUBLIC flags set (§2.7.4). If such a method has
					its ACC_ABSTRACT flag set it may not have any of its ACC_FINAL, ACC_NATIVE, ACC_PRIVATE, ACC_STATIC,
					ACC_STRICT, or ACC_SYNCHRONIZED flags set (§2.13.3.2).

					All interface methods must have their ACC_ABSTRACT and ACC_PUBLIC flags set and may not have any of the
					other flags in Table 4.5 set (§2.13.3.2).

					A specific instance initialization method (§3.9) may have at most one of its ACC_PRIVATE, ACC_PROTECTED,
					and ACC_PUBLIC flags set and may also have its ACC_STRICT flag set, but may not have any of the other
					flags in Table 4.5 set.

					Class and interface initialization methods (§3.9) are called implicitly by the Java virtual machine; the value of their access_flags item is ignored except for the settings of the ACC_STRICT flag.

					All bits of the access_flags item not assigned in Table 4.5 are reserved for future use. They should be set to zero in generated class files and should be ignored by Java virtual machine implementations.
				 */
				ushort method_access_flags = br.ReadUInt16();
				if ((method_access_flags & 0x0001) == 0x0001)
				{
					// ACC_PUBLIC: Declared public; may be accessed from outside its package.
				}
				if ((method_access_flags & 0x0002) == 0x0002)
				{
					// ACC_PRIVATE: Declared private; accessible only within the defining class.
				}
				if ((method_access_flags & 0x0004) == 0x0004)
				{
					// ACC_PROTECTED: Declared protected; may be accessed within subclasses.
				}
				if ((method_access_flags & 0x0008) == 0x0008)
				{
					// ACC_STATIC: Declared static.
				}
				if ((method_access_flags & 0x0010) == 0x0010)
				{
					// ACC_FINAL: Declared final; may not be overridden.
				}
				if ((method_access_flags & 0x0020) == 0x0020)
				{
					// ACC_SYNCHRONIZED: Declared synchronized; invocation is wrapped in a monitor lock.
				}
				if ((method_access_flags & 0x0100) == 0x0100)
				{
					// ACC_NATIVE: Declared native; implemented in a language other than Java.
				}
				if ((method_access_flags & 0x0400) == 0x0400)
				{
					// ACC_ABSTRACT: Declared abstract; no implementation is provided.
				}
				if ((method_access_flags & 0x0800) == 0x0800)
				{
					// ACC_STRICT: Declared strictfp; floating-point mode is FP-strict
				}

				ushort method_name_index = br.ReadUInt16();
				ushort method_descriptor_index = br.ReadUInt16();
				ushort method_attributes_count = br.ReadUInt16();

				for (ushort j = 0; j < method_attributes_count; j++)
				{
					ushort attribute_name_index = br.ReadUInt16();
					uint attribute_length = br.ReadUInt32();
					byte[] attribute_info = br.ReadBytes(attribute_length);
				}
			}

			ushort attributes_count = br.ReadUInt16();
			for (ushort i = 0; i < attributes_count; i++)
			{
				ushort attribute_name_index = br.ReadUInt16();
				uint attribute_length = br.ReadUInt32();
				byte[] attribute_info = br.ReadBytes(attribute_length);
			}

			com.Elements.Add(clss);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
