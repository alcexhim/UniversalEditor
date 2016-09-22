package net.alcetech.UniversalEditor.Core.Accessors;

import java.io.*;

import net.alcetech.UniversalEditor.Core.*;
import net.alcetech.UniversalEditor.Core.IO.*;

public class FileAccessor extends Accessor
{
	private RandomAccessFile _file = null;
	
	public static FileAccessor fromFile(String fileName) throws FileNotFoundException {
		return fromFile(new File(fileName));
	}
	public static FileAccessor fromFile(File file) throws FileNotFoundException {
		FileAccessor fa = new FileAccessor();
		fa.setFileName(file.getAbsolutePath());
		fa._file = new RandomAccessFile(file, "rw");
		return fa;
	}
	
	private FileAccessor()
	{
	}
	
	@Override
	protected long getLengthInternal() throws IOException {
		if (_file != null) {
			return _file.getChannel().size();
		}
		return 0;
	}
	@Override
	protected long getPositionInternal() throws IOException {
		if (_file != null) {
			return _file.getChannel().position();
		}
		return 0;
	}
	
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
		long value = position;
		switch (origin) {
			case BEGIN:
			{
				value = position;
				break;
			}
			case CURRENT:
			{
				value = _file.getChannel().position() + position;
				break;
			}
			case END:
			{
				value = _file.getChannel().size() + position;
				break;
			}
		}
		_file.seek(value);
	}
}
