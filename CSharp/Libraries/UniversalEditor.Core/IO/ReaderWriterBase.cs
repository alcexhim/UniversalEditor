using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.IO
{
    public abstract class ReaderWriterBase
    {
        private Endianness mvarEndianness = Endianness.LittleEndian;
        public Endianness Endianness { get { return mvarEndianness; } set { mvarEndianness = value; } }

        public void SwapEndianness()
        {
            if (mvarEndianness == IO.Endianness.LittleEndian)
            {
                mvarEndianness = IO.Endianness.BigEndian;
            }
            else
            {
                mvarEndianness = IO.Endianness.LittleEndian;
            }
        }

        private bool mvarReverse = false;
        public bool Reverse { get { return mvarReverse; } }

        private Accessor mvarAccessor = null;
        public Accessor Accessor { get { return mvarAccessor; } }

        private NewLineSequence mvarNewLineSequence = NewLineSequence.Default;
        public NewLineSequence NewLineSequence { get { return mvarNewLineSequence; } set { mvarNewLineSequence = value; } }
        public string GetNewLineSequence()
        {
            string newline = System.Environment.NewLine;
            switch (mvarNewLineSequence)
            {
                case IO.NewLineSequence.CarriageReturn:
                {
                    newline = "\r";
                    break;
                }
                case IO.NewLineSequence.LineFeed:
                {
                    newline = "\n";
                    break;
                }
                case IO.NewLineSequence.CarriageReturnLineFeed:
                {
                    newline = "\r\n";
                    break;
                }
                case IO.NewLineSequence.LineFeedCarriageReturn:
                {
                    newline = "\n\r";
                    break;
                }
            }
            return newline;
        }

        public ReaderWriterBase(Accessor accessor)
        {
            this.mvarAccessor = accessor;
            this.mvarEndianness = Endianness.LittleEndian;
            this.mvarReverse = false;
        }
    }
}
