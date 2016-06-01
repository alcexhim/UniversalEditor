/*
 * Accessor.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "Accessor.h"

#include <malloc.h>
#include <stdio.h>

using UniversalEditor::IO::SeekOrigin;

namespace UniversalEditor {

Accessor::Accessor() {
	// TODO Auto-generated constructor stub
	this->_reader = NULL;
	this->_writer = NULL;
}

Accessor::~Accessor() {
	// TODO Auto-generated destructor stub
}

void Accessor::openInternal()
{
	printf("ue: accessor: call to Accessor::openInternal requires subclass implementation\n");
}
void Accessor::closeInternal()
{
	printf("ue: accessor: call to Accessor::closeInternal requires subclass implementation\n");
}
void Accessor::readInternal(char* value, int offset, int length)
{
	printf("ue: accessor: call to Accessor::readInternal requires subclass implementation\n");
}
void Accessor::writeInternal(char* value, int offset, int length)
{
	printf("ue: accessor: call to Accessor::writeInternal requires subclass implementation\n");
}
uint64_t Accessor::getPositionInternal()
{
	printf("ue: accessor: call to Accessor::getPositionInternal requires subclass implementation\n");
	return -1;
}
void Accessor::setPositionInternal(uint64_t position)
{
	printf("ue: accessor: call to Accessor::setPositionInternal requires subclass implementation\n");
}
uint64_t Accessor::getLengthInternal()
{
	printf("ue: accessor: call to Accessor::getLengthInternal requires subclass implementation\n");
	return -1;
}

void Accessor::open()
{
	this->openInternal();
}
void Accessor::close()
{
	this->closeInternal();
}

char* Accessor::read(int length)
{
	char* value = (char*)malloc(sizeof(char) * length);
	this->read(value, 0, length);
	return value;
}
void Accessor::read(char* value, int offset, int length)
{
	this->readInternal(value, 0, length);
}

void Accessor::write(char* value, int length)
{
	this->write(value, 0, length);
}
void Accessor::write(char* value, int offset, int length)
{
	this->writeInternal(value, offset, length);
}

int Accessor::getPosition()
{
	return this->getPositionInternal();
}
void Accessor::setPosition(int position)
{
	this->setPositionInternal(position);
}

int Accessor::getLength()
{
	return this->getLengthInternal();
}

bool Accessor::endOfStream()
{
	return this->getPosition() >= this->getLength();
}

void Accessor::seek(int position, SeekOrigin origin)
{
	switch (origin)
	{
		case SeekOrigin::Start:
		{
			int realpos = position;
			if (realpos < 0 || realpos > this->getLength())
			{
				printf("ue: Accessor::seek() - from start, position must be between %d and %d; you asked for %d\n", 0, this->getLength(), position);
				return;
			}
			this->setPosition(realpos);
			break;
		}
		case SeekOrigin::Current:
		{
			int realpos = (this->getPosition() + position);
			if (realpos < 0 || realpos > this->getLength())
			{
				printf("ue: Accessor::seek() - from current, position must be between %d and %d; you asked for %d\n", -(this->getPosition()), (this->getLength() - this->getPosition()), position);
				return;
			}
			this->setPosition(realpos);
			break;
		}
		case SeekOrigin::End:
		{
			int realpos = (this->getLength() - position);
			if (realpos < 0 || realpos > this->getLength())
			{
				printf("ue: Accessor::seek() - from end, position must be between %d and %d; you asked for %d\n", -(this->getLength()), 0, position);
				return;
			}
			this->setPosition(realpos);
			break;
		}
	}
}

Reader* Accessor::getReader() {
	if (this->_reader == NULL) {
		this->_reader = new Reader(this);
	}
	return this->_reader;
}
Writer* Accessor::getWriter() {
	if (this->_writer == NULL) {
		this->_writer = new Writer(this);
	}
	return this->_writer;
}

} /* namespace UniversalEditor */
