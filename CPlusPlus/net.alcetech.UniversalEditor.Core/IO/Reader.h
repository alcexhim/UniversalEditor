/*
 * Reader.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef IO_READER_H_
#define IO_READER_H_

#include "../Accessor.h"
#include "Endianness.h"

#include <String.h>

using ApplicationFramework::String;

namespace UniversalEditor {

class Accessor;

namespace IO {

class Reader {
private:
	Accessor* _accessor;
	Endianness _endianness;
public:
	Reader(Accessor* accessor);
	virtual ~Reader();

	Accessor* getAccessor();

	Endianness getEndianness();
	void setEndianness(Endianness value);

	char readByte();
	char* readByteArray(int length);

	char readChar();
	char* readCharArray(int length);

	short readInt16();
	short* readInt16Array(int length);

	int readInt32();
	int* readInt32Array(int length);

	long long readInt64();
	long long* readInt64Array(int length);

	unsigned short readUInt16();
	unsigned short* readUInt16Array(int length);

	unsigned int readUInt32();
	unsigned int* readUInt32Array(int length);

	unsigned long long readUInt64();
	unsigned long long* readUInt64Array(int length);

	char* readFixedLengthString(int length);
	String* readStringToEnd();
};

} /* namespace IO */
} /* namespace UniversalEditor */

#endif /* IO_READER_H_ */
