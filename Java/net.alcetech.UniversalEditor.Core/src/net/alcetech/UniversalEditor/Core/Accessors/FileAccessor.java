package net.alcetech.UniversalEditor.Core.Accessors;

import java.io.*;

import net.alcetech.UniversalEditor.Core.*;
import net.alcetech.UniversalEditor.Core.IO.*;

public class FileAccessor extends Accessor
{
	private RandomAccessFile _file = null;
	
	public static FileAccessor fromFile(File file) throws FileNotFoundException {
		FileAccessor fa = new FileAccessor();
		fa._file = new RandomAccessFile(file, "rw");
		return fa;
	}
	
	private FileAccessor()
	{
	}
	public FileAccessor(String fileName)
	{
		mvarFileName = fileName;
	}
	
	private String mvarFileName = "";
	public void setFileName(String value) { mvarFileName = value; }
	public String getFileName() { return mvarFileName; }
	
	protected void openInternal() throws IOException
	{
		String mode = "rw";
		_file = new RandomAccessFile(new File(getFileName()), mode);
	}
	protected void closeInternal() throws IOException
	{
		if (_file != null)
		{
			_file.close();
		}
	}
	
	protected int readInternal(byte[] buffer, int offset, int length) throws IOException
	{
		return _file.read(buffer, offset, length);
	}
	protected int writeInternal(byte[] buffer, int offset, int length) throws IOException
	{
		_file.write(buffer, offset, length);
		return length;
	}
	
	protected void seekInternal(long position, SeekOrigin origin) throws IOException
	{
		_file.seek(position);
	}
}
