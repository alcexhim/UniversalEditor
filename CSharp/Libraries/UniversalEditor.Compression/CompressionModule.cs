using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression
{
    public abstract class CompressionModule
    {
        public abstract string Name { get; }

        public virtual bool CanCompress { get { return true; } }
        public virtual bool CanDecompress { get { return true; } }

        public byte[] Compress(byte[] inputData)
        {
            System.IO.MemoryStream inputStream = new System.IO.MemoryStream(inputData);
            System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
            Compress(inputStream, outputStream);
            outputStream.Close();
            return outputStream.ToArray();
        }
        public byte[] Decompress(byte[] inputData)
        {
            System.IO.MemoryStream inputStream = new System.IO.MemoryStream(inputData);
            System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
            Decompress(inputStream, outputStream);
            outputStream.Close();
            return outputStream.ToArray();
        }

        /*
        public void Compress(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
        }
        */
        public void Decompress(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            int inputLength = (int)inputStream.Length;
            int outputLength = (int)outputStream.Length;
            Decompress(inputStream, outputStream, inputLength, outputLength);
        }

        public abstract void Compress(System.IO.Stream inputStream, System.IO.Stream outputStream);
        public abstract void Decompress(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength);
    }
}
