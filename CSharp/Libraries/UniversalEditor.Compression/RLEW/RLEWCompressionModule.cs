using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override void Compress(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            IO.BinaryReader br = new IO.BinaryReader(inputStream);
            IO.BinaryWriter bw = new IO.BinaryWriter(outputStream);
            
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
                        bw.Write(mvarRLEWTag);
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
                            bw.Write(word);
                        }

                        bw.Write(lastWord);

                        lastWord = 0;
                        wordCount = 0;
                    }
                    else
                    {
                        bw.Write(wordCount);
                        bw.Write(lastWord);
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
                    bw.Write(lastWord);
                }

                lastWord = 0;
                wordCount = 0;
            }
            else
            {
                bw.Write(wordCount);
                bw.Write(lastWord);
            }

            bw.Flush();
        }
        public override void Decompress(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
        {
            IO.BinaryReader br = new IO.BinaryReader(inputStream);
            IO.BinaryWriter bw = new IO.BinaryWriter(outputStream);
            
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
                        bw.Write(value);
                    }
                }
                else
                {
                    // if word is not equal to RLEWtag, then simply write that word out
                    bw.Write(word);
                }
            }

            bw.Flush();
            bw.Close();
        }
    }
}
