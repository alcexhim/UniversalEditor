#include "system.h"

typedef struct tag_Accessor
{
	void* _impl_open();
	
	void* _impl_write(char* value, int start, int length);
	void* _impl_read(char* value, int start, int length);
	
	void* _impl_close();
} * Accessor;

Accessor UE_Accessor_Create();
Accessor UE_Accessor_Create_FileAccessor(String filename);
