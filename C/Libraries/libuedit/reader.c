#include "reader.h"
#include <malloc.h>

Reader UE_Reader_Create(Accessor accessor)
{
	Reader reader = (Reader)malloc(sizeof(_Reader));
	return reader;
}
void UE_Reader_Close(Reader reader)
{
	UE_Accessor_Close(reader->accessor);
}

short UE_Reader_ReadInt16(Reader reader)
{
}
int UE_Reader_ReadInt32(Reader reader)
{
}
long UE_Reader_ReadInt64(Reader reader)
{
}

unsigned short UE_Reader_ReadUInt16(Reader reader)
{
}
unsigned int UE_Reader_ReadUInt32(Reader reader)
{
}
unsigned long UE_Reader_ReadUInt64(Reader reader)
{
}

String UE_Reader_ReadFixedLengthString(Reader reader, int length)
{
}
String UE_Reader_ReadLengthPrefixedString(Reader reader)
{
}
