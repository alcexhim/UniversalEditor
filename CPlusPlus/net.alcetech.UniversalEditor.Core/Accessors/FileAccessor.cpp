/*
 * FileAccessor.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "FileAccessor.h"

#include <malloc.h>
#include <sys/stat.h>
#include <stdio.h>

namespace UniversalEditor {
namespace Accessors {

FileAccessor::FileAccessor(const char* fileName) {
	this->_fileName = fileName;
	this->_handle = NULL;
	this->_length = -1;
}

FileAccessor::~FileAccessor() {
	// TODO Auto-generated destructor stub
}

void FileAccessor::openInternal() {
	FILE* pFile = fopen(this->getFileName(), "r");
	if (pFile == NULL) {
		printf("FileAccessor::openInternal() - opening file '%s' for reading FAILED\n", this->getFileName());
	}
	this->_handle = pFile;
}
void FileAccessor::closeInternal() {
	if (this->_handle == NULL)
	{
		printf("ue: file-accessor: close() called before open()");
		return;
	}
	fflush(this->_handle);
	fclose(this->_handle);
}

void FileAccessor::readInternal(char* value, int offset, int length) {
	fread(value, sizeof(char), length, this->_handle);
}
void FileAccessor::writeInternal(char* value, int offset, int length) {
	for (int i = offset; i < (offset + length); i++)
	{
		fprintf(this->_handle, "%c", value[i]);
	}
}

uint64_t FileAccessor::getPositionInternal() {
	if (this->_handle != NULL)
	{
		fpos64_t* pos = new fpos64_t();
		fgetpos64(this->_handle, pos);
		return pos->__pos;
	}

	printf("ue: FileAccessor: getPositionInternal() called on unopened FileAccessor");
	return -1;
}
uint64_t FileAccessor::getLengthInternal() {
	if (this->_length == -1)
	{
		if (this->_handle == NULL) {
			printf("FileAccessor::getLengthInternal() - handle is NULL, did you forget to call open()?\n");
			return 0;
		}
		struct stat64* statbuf = (struct stat64*)malloc(sizeof(struct stat64));
		fstat64(this->_handle->_fileno, statbuf);
		this->_length = statbuf->st_size;
		free(statbuf);
	}
	return this->_length;
}

void FileAccessor::setPositionInternal(uint64_t value) {
	fpos64_t* pos = new fpos64_t();
	pos->__pos = value;
	fsetpos64(this->_handle, pos);
}

const char* FileAccessor::getFileName() {
	return this->_fileName;
}

FileAccessor* FileAccessor::fromFileName(const char* fileName) {
	FileAccessor* acc = new FileAccessor(fileName);
	return acc;
}

bool FileAccessor::exists() {
	if (this->_handle == NULL) return false;
	return true;
}

} /* namespace Accessors */
} /* namespace UniversalEditor */
