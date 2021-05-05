//
//  RLEWCompressionModule.cs - provides a CompressionModule for handling RLEW compression
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UniversalEditor.Accessors;

namespace UniversalEditor.Compression.RLEW
{
	/// <summary>
	/// Provides a <see cref="CompressionModule" /> for handling RLEW compression.
	/// </summary>
	public class RLEWCompressionModule : CompressionModule
	{
		public override string Name
		{
			get { return "RLEW"; }
		}

		private short mvarRLEWTag = 0;
		public short RLEWTag { get { return mvarRLEWTag; } set { mvarRLEWTag = value; } }

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			IO.Reader br = new IO.Reader(new StreamAccessor(inputStream));
			IO.Writer bw = new IO.Writer(new StreamAccessor(outputStream));

			short lastWord = 0;
			short wordCount = 0;
			bool hasLastWord = false;

			while (!br.EndOfStream)
			{
				// to make this work efficiently, we need to have at least four equal words
				// (RLEWtag + count + value = 3 words)

				// read the next value in
				short word = 0;
				if (br.Remaining == 0)
				{
					break;
				}
				else if (br.Remaining == 1)
				{
					word = br.ReadByte();
				}
				else if (br.Remaining >= 2)
				{
					word = br.ReadInt16();
				}

				if (!hasLastWord)
				{
					lastWord = word;
					hasLastWord = true;
					wordCount++;
					continue;
				}

				if (word == lastWord)
				{
					// it's equal to the last word, so increment our word count
					wordCount++;

					if (wordCount == 4)
					{
						// we have at least four equal words, so push out an RLEWtag...
						bw.WriteInt16(mvarRLEWTag);
					}
				}
				else
				{
					// word is not equal, flush the remaining words...
					if (wordCount <= 3)
					{
						// we do not have at least four equal words, it would be more efficient to
						// just store the values as-is, so flush the remaining words
						for (ushort i = 0; i < wordCount; i++)
						{
							bw.WriteInt16(word);
						}

						bw.WriteInt16(lastWord);

						lastWord = 0;
						wordCount = 0;
					}
					else
					{
						bw.WriteInt16(wordCount);
						bw.WriteInt16(lastWord);
					}

					// ... and clear the word count
					lastWord = 0;
					wordCount = 0;

					// finally, set the last word to the current word
					lastWord = word;
				}
			}


			// word is not equal, flush the remaining words...
			if (wordCount <= 3)
			{
				// we do not have at least four equal words, it would be more efficient to
				// just store the values as-is, so flush the remaining words
				for (ushort i = 0; i < wordCount; i++)
				{
					bw.WriteInt16(lastWord);
				}

				lastWord = 0;
				wordCount = 0;
			}
			else
			{
				bw.WriteInt16(wordCount);
				bw.WriteInt16(lastWord);
			}

			bw.Flush();
		}
		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			IO.Reader br = new IO.Reader(new StreamAccessor(inputStream));
			IO.Writer bw = new IO.Writer(new StreamAccessor(outputStream));

			while (!br.EndOfStream)
			{
				// read one word from compressed block
				short word = br.ReadInt16();
				if (word == mvarRLEWTag)
				{
					// next two words are a compressed run of data, first word is number
					// of words to write
					short count = br.ReadInt16();
					short value = br.ReadInt16();
					for (short i = 0; i < count; i++)
					{
						bw.WriteInt16(value);
					}
				}
				else
				{
					// if word is not equal to RLEWtag, then simply write that word out
					bw.WriteInt16(word);
				}
			}

			bw.Flush();
			bw.Close();
		}
	}
}
