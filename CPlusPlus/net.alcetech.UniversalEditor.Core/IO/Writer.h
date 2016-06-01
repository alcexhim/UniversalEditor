/*
 * Writer.h
 *
 *  Created on: Mar 31, 2016
 *      Author: beckermj
 */

#ifndef WRITER_H_
#define WRITER_H_

#include "../Accessor.h"
#include "Endianness.h"

namespace UniversalEditor {

class Accessor;

namespace IO {

class Writer
{
private:
	Accessor* _accessor;
	Endianness _endianness;
public:
	Writer(Accessor* accessor);
	virtual ~Writer();

	Accessor* getAccessor();

	Endianness getEndianness();
	void setEndianness(Endianness value);

	void writeByte(char value);
	void writeByteArray(char* value, int length);

	void writeInt16(short value);
	void writeInt16Array(short* value, int length);

	void writeInt32(int value);
	void writeInt32Array(int* value, int length);

	void writeInt64(long long value);
	void writeInt64Array(long long* value, int length);

	void writeUInt16(unsigned short value);
	void writeUInt16Array(unsigned short* value);

	void writeUInt32(unsigned int value);
	void writeUInt32Array(unsigned int* value);

	void writeUInt64(unsigned long value);
	void writeUInt64Array(unsigned long* value);

	void writeChar(char value);
	void writeCharArray(char* value);

	void writeFixedLengthString(const char* value);
};

} /* namespace IO */
} /* namespace UniversalEditor */

#endif /* WRITER_H_ */
