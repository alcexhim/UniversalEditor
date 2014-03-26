using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Checksum
{
    public abstract class ChecksumModule
    {
        public abstract string Name { get; }

        private long mvarValue = 0;
        public virtual long Value { get { return mvarValue; } }

        public long Calculate(byte[] input)
        {
            Reset();
            Update(input);
            return Value;
        }

        /// <summary>
        /// Resets the checksum as if no update was ever called.
        /// </summary>
        public virtual void Reset()
        {
            mvarValue = 0;
        }

        /// <summary>
        /// Updates the checksum with the bytes taken from the array.
        /// </summary>
        /// <param name="buffer">
        /// buffer an array of bytes
        /// </param>
        public void Update(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            Update(buffer, 0, buffer.Length);
        }
        public abstract void Update(byte[] buffer, int offset, int count);
        public abstract void Update(int value);
    }
}
