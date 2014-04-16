#include "accessor.h"
#include <malloc.h>

Accessor UE_Accessor_Create()
{
	Accessor accessor = (Accessor)malloc(sizeof(_Accessor));
	return accessor;
}
Accessor UE_Accessor_Create_FileAccessor(String filename)
{
	Accessor accessor = UE_Accessor_Create();
	accessor->open = &_UE_Accessor_FileAccessor_Open;
	accessor->write = &_UE_Accessor_FileAccessor_Write;
	accessor->read = &_UE_Accessor_FileAccessor_Read;
	accessor->close = &_UE_Accessor_FileAccessor_Close;
	return accessor;
}

void _UE_Accessor_FileAccessor_Open(Accessor accessor, String filename)
{
	char* mode = "";
	if (accessor->EnableWrite && accessor->ForceOverwrite)
	{
		mode = "rw";
	}
	else
	{
		mode = "r";
	}
	
	FILE* fptr = fopen(filename, mode);
	accessor->data = fptr;
}
void _UE_Accessor_FileAccessor_Write(Accessor accessor, String filename)
{
}
void _UE_Accessor_FileAccessor_Read(Accessor accessor, String filename)
{
}
void _UE_Accessor_FileAccessor_Close(Accessor accessor, String filename)
{
}

void UE_Accessor_Close(Accessor accessor)
{
	if (accessor == NULL) return;
	accessor->close();
}