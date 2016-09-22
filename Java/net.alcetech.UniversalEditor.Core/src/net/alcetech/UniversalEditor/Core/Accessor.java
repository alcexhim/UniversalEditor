package net.alcetech.UniversalEditor.Core;

import java.io.IOException;

import net.alcetech.UniversalEditor.Core.IO.*;

public abstract class Accessor
{
	public Accessor() {
		_reader = new Reader(this);
		_writer = new Writer(this);
	}

	/**
	 * Determines if this Accessor is at the end of its underlying stream.
	 * @return true if this Accessor is at the end of its underlying stream; false otherwise.
	 * @throws IOException
	 */
	public boolean getEndOfStream() throws IOException {
		return (this.getPosition() >= this.getLength());
	}
	
	public long getPosition() throws IOException {
		return this.getPositionInternal();
	}
	public void setPosition(long value) throws IOException {
		this.seek(value, SeekOrigin.BEGIN);
	}
	public long getLength() throws IOException {
		return this.getLengthInternal();
	}
	
	protected abstract long getPositionInternal() throws IOException;
	protected abstract long getLengthInternal() throws IOException;
	
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
	
	private Reader _reader = null;
	public Reader getReader() {
		return _reader;
	}
	private Writer _writer = null;
	public Writer getWriter() {
		return _writer;
	}

	private String _FileName = null;
	public String getFileName() {
		return _FileName;
	}
	public void setFileName(String value) {
		_FileName = value;
	}
}
