package net.alcetech.UniversalEditor.Core;

import java.io.IOException;

import net.alcetech.UniversalEditor.Core.IO.*;

public abstract class Accessor
{
	public void seek(long position, SeekOrigin origin) throws IOException
	{
		/*
		switch (origin)
		{
			case Begin:
			{
				if (position > getLength())
				break;
			}
		}
		*/
		seekInternal(position, origin);
	}
	protected abstract void seekInternal(long position, SeekOrigin origin) throws IOException;
	
	public void open() throws IOException
	{
		openInternal();
	}
	public void close() throws IOException
	{
		closeInternal();
	}
	
	protected abstract void openInternal() throws IOException;
	protected abstract void closeInternal() throws IOException;
	
	protected abstract int readInternal(byte[] buffer, int start, int length) throws IOException;
	protected abstract int writeInternal(byte[] buffer, int start, int length) throws IOException;
	
	public int read(byte[] buffer, int start, int length) throws IOException
	{
		return readInternal(buffer, start, length);
	}
	public int write(byte[] buffer, int start, int length) throws IOException
	{
		return writeInternal(buffer, start, length);
	}
	public int read(byte[] buffer) throws IOException
	{
		return readInternal(buffer, 0, buffer.length);
	}
	public int write(byte[] buffer) throws IOException
	{
		return write(buffer, 0, buffer.length);
	}
}
