/*
 * Reader.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "Reader.h"

#include <malloc.h>

namespace UniversalEditor {
namespace IO {

Reader::Reader(Accessor* accessor) {
	this->_accessor = accessor;
	this->_endianness = Endianness::LittleEndian;
}

Reader::~Reader() {
	// TODO Auto-generated destructor stub
}

Accessor* Reader::getAccessor() {
	return this->_accessor;
}

Endianness Reader::getEndianness() {
	return this->_endianness;
}
void Reader::setEndianness(Endianness value) {
	this->_endianness = value;
}

char Reader::readByte()
{
	char* bytes = (char*)malloc(sizeof(char));
	this->_accessor->read(bytes, 0, 1);
	return bytes[0];
}
char* Reader::readByteArray(int length)
{
	char* bytes = (char*)malloc(sizeof(char) * length);
	this->_accessor->read(bytes, 0, length);
	return bytes;
}

char Reader::readChar()
{
	return this->readByte();
}
char* Reader::readCharArray(int length)
{
	return this->readByteArray(length);
}

short Reader::readInt16()
{
	// an Int16 value is 16 bits or 2 bytes
	char* array = this->readByteArray(2);
	int a = 0, b = 1;
	if (this->_endianness == Endianness::BigEndian)
	{
		a = 1;
		b = 0;
	}
	return (short)((array[a]) | ((array[b]) << 8));
}
short* Reader::readInt16Array(int length)
{
	short* value = (short*)malloc(sizeof(short) * length);
	for (int i = 0; i < length; i++)
	{
		value[i] = this->readInt16();
	}
	return value;
}

int Reader::readInt32()
{
	// an Int32 value is 32 bits or 4 bytes
	char* array = this->readByteArray(4);
	int a = 0, b = 1, c = 2, d = 3;
	if (this->_endianness == Endianness::LittleEndian)
	{
		a = 3;
		b = 2;
		c = 1;
		d = 0;
	}
	return (int)(array[a] | (array[b] << 8)  | (array[c] << 16) | (array[d] << 24));
}
int* Reader::readInt32Array(int length)
{
	int* value = (int*)malloc(sizeof(int) * length);
	for (int i = 0; i < length; i++)
	{
		value[i] = this->readInt32();
	}
	return value;
}

long long Reader::readInt64()
{
	// an Int64 value is 64 bits or 8 bytes
	char* array = this->readByteArray(8);
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
	int i1 = (array[a]) | (array[b] << 8)  | (array[c] << 16) | (array[d] << 24);
	int i2  = (array[e]) | (array[f] << 8)  | (array[g] << 16) | (array[h] << 24);
	return (long long)((unsigned int)i1 | ((long)i2 << 32));
}
long long* Reader::readInt64Array(int length)
{
	long long* value = (long long*)malloc(sizeof(long long) * length);
	for (int i = 0; i < length; i++)
	{
		value[i] = this->readInt64();
	}
	return value;
}

unsigned short Reader::readUInt16()
{
	return (unsigned short)this->readInt16();
}
unsigned short* Reader::readUInt16Array(int length)
{
	unsigned short* value = (unsigned short*)malloc(sizeof(unsigned short) * length);
	for (int i = 0; i < length; i++)
	{
		value[i] = this->readUInt16();
	}
	return value;
}

unsigned int Reader::readUInt32()
{
	return (unsigned int)this->readInt32();
}
unsigned int* Reader::readUInt32Array(int length)
{
	unsigned int* value = (unsigned int*)malloc(sizeof(unsigned int) * length);
	for (int i = 0; i < length; i++)
	{
		value[i] = this->readUInt32();
	}
	return value;
}

unsigned long long Reader::readUInt64()
{
	return (unsigned long long)this->readInt64();
}
unsigned long long* Reader::readUInt64Array(int length)
{
	unsigned long long* value = (unsigned long long*)malloc(sizeof(unsigned long long) * length);
	for (int i = 0; i < length; i++)
	{
		value[i] = this->readUInt64();
	}
	return value;
}

char* Reader::readFixedLengthString(int length)
{
	char* bytes = (char*)malloc(sizeof(char) * (length + 1));
	for (int i = 0; i < length; i++)
	{
		bytes[i] = this->readChar();
	}
	bytes[length] = 0;
	return bytes;
}

String* Reader::readStringToEnd() {
	int len = this->getAccessor()->getLength();
	char* value = this->readFixedLengthString(len);
	return new String(value);
}

} /* namespace IO */
} /* namespace UniversalEditor */
