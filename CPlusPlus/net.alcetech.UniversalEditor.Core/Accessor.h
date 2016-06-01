/*
 * Accessor.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef ACCESSOR_H_
#define ACCESSOR_H_

#include "IO/SeekOrigin.h"
#include "IO/Reader.h"
#include "IO/Writer.h"

#include <stdint.h>

namespace UniversalEditor
{

namespace IO {

class Reader;
class Writer;

}

}

using UniversalEditor::IO::Reader;
using UniversalEditor::IO::SeekOrigin;
using UniversalEditor::IO::Writer;

namespace UniversalEditor {

class Accessor
{
private:
	Reader* _reader;
	Writer* _writer;
protected:
	virtual void openInternal();
	virtual void closeInternal();

	virtual void readInternal(char* value, int offset, int length);
	virtual void writeInternal(char* value, int offset, int length);

	virtual uint64_t getPositionInternal();
	virtual void setPositionInternal(uint64_t position);

	virtual uint64_t getLengthInternal();

public:
	Accessor();
	virtual ~Accessor();

	void open();
	void close();

	char* read(int length);
	void read(char* value, int offset, int length);

	void write(char* value, int length);
	void write(char* value, int offset, int length);

	int getPosition();
	void setPosition(int position);

	int getLength();

	bool endOfStream();

	void seek(int position, SeekOrigin origin);

	Reader* getReader();
	Writer* getWriter();
};

} /* namespace UniversalEditor */

#endif /* ACCESSOR_H_ */
