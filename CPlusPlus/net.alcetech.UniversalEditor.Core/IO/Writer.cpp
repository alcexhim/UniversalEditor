/*
 * Writer.cpp
 *
 *  Created on: Mar 31, 2016
 *      Author: beckermj
 */

#include "Writer.h"

#include <stdio.h>
#include <malloc.h>
#include <string.h>

namespace UniversalEditor {
namespace IO {

Writer::Writer(Accessor* accessor)
{
	this->_accessor = accessor;
	this->_endianness = Endianness::LittleEndian;
}

Writer::~Writer()
{
	// TODO Auto-generated destructor stub
}

Endianness Writer::getEndianness()
{
	return this->_endianness;
}
void Writer::setEndianness(Endianness value)
{
	this->_endianness = value;
}

Accessor* Writer::getAccessor()
{
	return this->_accessor;
}

void Writer::writeByte(char value)
{
	char* bytes = (char*)malloc(sizeof(char));
	bytes[0] = value;
	this->_accessor->write(bytes, 0, 1);
}
void Writer::writeByteArray(char* value, int length)
{
	this->_accessor->write(value, 0, length);
}

void Writer::writeInt16(short value)
{
	// an Int16 value is 16 bits or 2 bytes
	char* array = (char*)malloc(2);
	int a = 0, b = 1;
	if (this->_endianness == Endianness::BigEndian)
	{
		a = 1;
		b = 0;
	}
	array[a] = (char)((value & (0xFF << 0)) >> 0);
	array[b] = (char)((value & (0xFF << 8)) >> 8);
	this->writeByteArray(array, 2);
}
void Writer::writeInt16Array(short* value, int length)
{
	for (int i = 0; i < length; i++)
	{
		this->writeInt16(value[i]);
	}
}

void Writer::writeInt32(int value)
{
	// an Int32 value is 32 bits or 4 bytes
	char* array = (char*)malloc(4);
	int a = 0, b = 1, c = 2, d = 3;
	if (this->_endianness == Endianness::LittleEndian)
	{
		a = 3;
		b = 2;
		c = 1;
		d = 0;
	}
	array[a] = (char)((value & (0xFF << 0)) >> 0);
	array[b] = (char)((value & (0xFF << 8)) >> 8);
	array[c] = (char)((value & (0xFF << 16)) >> 16);
	array[d] = (char)((value & (0xFF << 24)) >> 24);
	this->writeByteArray(array, 4);
}
void Writer::writeInt32Array(int* value, int length)
{
	for (int i = 0; i < length; i++)
	{
		this->writeInt32(value[i]);
	}
}

void Writer::writeInt64(long long value)
{
	// an Int64 value is 64 bits or 8 bytes
	char* array = (char*)malloc(8);
	int a = 0, b = 1, c = 2, d = 3, e = 4, f = 5, g = 6, h = 7;
	if (this->_endianness == Endianness::LittleEndian)
	{
		a = 7;
		b = 6;
		c = 5;
		d = 4;
		e = 3;
		f = 2;
		g = 1;
		h = 0;
	}
	array[a] = (char)((value & (0xFF << 0)) >> 0);
	array[b] = (char)((value & (0xFF << 8)) >> 8);
	array[c] = (char)((value & (0xFF << 16)) >> 16);
	array[d] = (char)((value & (0xFF << 24)) >> 24);
	array[e] = (char)((value & (0xFF << 32)) >> 32);
	array[f] = (char)((value & (0xFF << 40)) >> 40);
	array[g] = (char)((value & (0xFF << 48)) >> 48);
	array[h] = (char)((value & (0xFF << 56)) >> 56);
	this->writeByteArray(array, 8);
}
void Writer::writeInt64Array(long long* value, int length)
{
	for (int i = 0; i < length; i++)
	{
		this->writeInt64(value[i]);
	}
}

void Writer::writeUInt16(unsigned short value)
{

}
void Writer::writeUInt16Array(unsigned short* value)
{

}

void Writer::writeUInt32(unsigned int value)
{

}
void Writer::writeUInt32Array(unsigned int* value)
{

}

void Writer::writeUInt64(unsigned long value)
{

}
void Writer::writeUInt64Array(unsigned long* value)
{

}

void Writer::writeChar(char value)
{

}
void Writer::writeCharArray(char* value)
{

}

void Writer::writeFixedLengthString(const char* value)
{
	int length = strlen(value);
	char* bytes = (char*)malloc(sizeof(char) * length);
	for (int i = 0; i < length; i++)
	{
		bytes[i] = value[i];
	}
	this->writeByteArray(bytes, length);
}


} /* namespace IO */
} /* namespace UniversalEditor */
