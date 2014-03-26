using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;

namespace UniversalEditor.Compression.RLEW
{
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
