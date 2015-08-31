using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor
{
    internal static class MyCursors
    {
        private static Cursor mvarEraser = null;
        public static Cursor Eraser
        {
            get
            {
                if (mvarEraser == null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Properties.Resources.Eraser);
                    try
                    {
                        mvarEraser = new Cursor(ms);
                    }
                    catch
                    {
                    }
                }
                return mvarEraser;
            }
        }
        private static Cursor mvarPen = null;
        public static Cursor Pen
        {
            get
            {
                if (mvarPen == null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Properties.Resources.Pen);
                    try
                    {
                        mvarPen = new Cursor(ms);
                    }
                    catch
                    {
                    }
                }
                return mvarPen;
            }
        }
    }
}
