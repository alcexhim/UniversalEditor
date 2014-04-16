#include "writer.h"
#include <malloc.h>

Writer UE_Writer_Create(Accessor accessor)
{
	Writer writer = (Writer)malloc(sizeof(_Writer));
	return writer;
}

void UE_Writer_WriteInt16(short value);
void UE_Writer_WriteInt32(int value);
void UE_Writer_WriteInt64(long value);

void UE_Writer_WriteUInt16(unsigned short value);
void UE_Writer_WriteUInt32(unsigned int value);
void UE_Writer_WriteUInt64(unsigned long value);

void UE_Writer_WriteFixedString(String value, int length);
void UE_Writer_WriteLengthPrefixedString(String value);
