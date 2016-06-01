/*
 * FileAccessor.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef FILEACCESSOR_H_
#define FILEACCESSOR_H_

#include "../Accessor.h"

#include <stdio.h>
#include <stdint.h>

namespace UniversalEditor {
namespace Accessors {

class FileAccessor : public Accessor
{
private:
	int _length;
	const char* _fileName;
	FileAccessor(const char* fileName);
	FILE* _handle;

protected:
	virtual void openInternal();
	virtual void closeInternal();

	virtual void readInternal(char* value, int offset, int length);
	virtual void writeInternal(char* value, int offset, int length);

	virtual uint64_t getPositionInternal();
	virtual uint64_t getLengthInternal();

	virtual void setPositionInternal(uint64_t value);

public:
	virtual ~FileAccessor();

	const char* getFileName();

	static FileAccessor* fromFileName(const char* fileName);

	/**
	 * returns true if the file opened by this Accessor exists, false otherwise.
	 */
	bool exists();
};

} /* namespace Accessors */
} /* namespace UniversalEditor */

#endif /* FILEACCESSOR_H_ */
