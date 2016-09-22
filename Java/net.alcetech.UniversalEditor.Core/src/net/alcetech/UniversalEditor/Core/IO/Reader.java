package net.alcetech.UniversalEditor.Core.IO;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.MappedByteBuffer;
import java.nio.charset.Charset;
import java.util.ArrayList;

import net.alcetech.UniversalEditor.Core.Accessor;

public class Reader
{
	private Accessor _accessor = null;
	public Accessor getAccessor() { return _accessor; }
	public void setAccessor(Accessor value) { _accessor = value; }
	
	/**
	 * Determines if this Reader's Accessor is at the end of its underlying stream.
	 * @return true if this Reader's Accessor is at the end of its underlying stream; false otherwise.
	 * @throws IOException
	 */
	public boolean getEndOfStream() throws IOException {
		return _accessor.getEndOfStream();
	}
	
	public Reader(Accessor accessor)
	{
		_accessor = accessor;
	}
	
	private Endianness mvarEndianness = Endianness.LittleEndian;
	public void setEndianness(Endianness value)
	{
		mvarEndianness = value;
	}
	public Endianness getEndianness()
	{
		return mvarEndianness;
	}
	
	public byte readByte() throws IOException {
		byte[] buffer = new byte[1];
		this.getAccessor().read(buffer);
		return buffer[0];
	}
	public char readChar() throws IOException {
		byte value = this.readByte();
		return (char)value;
	}
	
	public byte[] readUntil(byte[] until) throws IOException {
		return readUntil(until, false);
	}
	public byte[] readUntil(byte[] until, boolean includeSequence) throws IOException {
		ArrayList<Byte> retval = new ArrayList<Byte>();
		while (!getEndOfStream())
		{
			retval.add(readByte());

			boolean matches = true;
			for (int i = 0; i < until.length; i++)
			{
				if (retval.size() < until.length)
				{
					matches = false;
					break;
				}
				if (retval.get(retval.size() - (until.length - i)) != until[i])
				{
					matches = false;
					break;
				}
			}

			if (matches)
			{
				if (!includeSequence)
				{
					ArrayList<Byte> retval1 = new ArrayList<Byte>();
					for (int i = 0; i < retval.size() - until.length; i++) {
						retval1.add(retval.get(i));
					}
					retval = retval1;
					this.getAccessor().seek(-until.length, SeekOrigin.CURRENT);
				}
				break;
			}
		}
		
		// oh Java, thou art a heartless b!tch
		Byte[] array1 = new Byte[retval.size()];
		retval.toArray(array1);
		
		byte[] array2 = new byte[array1.length];
		for (int i = 0; i < array1.length; i++) {
			array2[i] = array1[i].byteValue();
		}
		return array2;
	}
	
	public String readStringUntil(String until) throws IOException {
		return readStringUntil(until, false);
	}
	public String readStringUntil(String until, boolean includeSequence) throws IOException {
		return readStringUntil(until, includeSequence, Charset.defaultCharset());
	}
	public String readStringUntil(String until, boolean includeSequence, Charset charset) throws IOException {
		ByteBuffer bbuf = charset.encode(until);
		CharBuffer cbuf = charset.decode(ByteBuffer.wrap(readUntil(bbuf.array())));
		return cbuf.toString();
	}
	
	public String readStringUntil(char until) throws IOException {
		return readStringUntil(until, false);
	}
	public String readStringUntil(char until, boolean includeSequence) throws IOException {
		return readStringUntil(until, includeSequence, Charset.defaultCharset());
	}
	public String readStringUntil(char until, boolean includeSequence, Charset charset) throws IOException {
		return readStringUntil(String.valueOf(until), includeSequence, charset);
	}
	
	public String readStringUntil(char until, String ignoreBegin, String ignoreEnd) throws IOException {
		return readStringUntil(String.valueOf(until), ignoreBegin, ignoreEnd);
	}
	public String readStringUntil(String until, String ignoreBegin, String ignoreEnd) throws IOException {
		return readStringUntil(new String[] { until }, ignoreBegin, ignoreEnd);
	}
	public String readStringUntil(String[] until, String ignoreBegin, String ignoreEnd) throws IOException {
		String sb = "";
		while (!getEndOfStream())
		{
			sb += readChar();

			for (int i = 0; i < until.length; i++)
			{
				String s = until[i];
				if (sb.endsWith(s))
				{
					return sb.substring(0, sb.length() - 1);
				}
			}

			/*
			char[] buffer = new char[until.Length * 2];
			ReadBlock(buffer, 0, until.Length * 2);

			string w = new string(buffer);
			if (w.Contains(until))
			{
				string ww = w.Substring(0, w.IndexOf(until));
				sb.Append(ww);

				// back up the stream reader
				int indexOfUntil = (w.IndexOf(until) + until.Length);
				int lengthToBackUp = w.Length - indexOfUntil;
				BaseStream.Seek(-1 * lengthToBackUp, SeekOrigin.Current);
				break;
			}
			sb.Append(w);
			*/
		}
		return sb;
	}
	
	public char peekChar() throws IOException {
		char c = readChar();
		getAccessor().seek(-1, SeekOrigin.CURRENT);
		return c;
	}
}
