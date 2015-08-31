using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public enum IcarusCommandType
    {
        None = 0,
        Affect = 19,
        Sound = 20,
        Rotate = 22,
        Wait = 23,
        End = 25,
        Set = 26,
        Loop = 27,
        Print = 29,
        Use = 30,
        Flush = 31,
        Run = 32,
        Kill = 33,
        Camera = 35,
        Task = 41,
        Do = 42,
        Declare = 43,
        Signal = 46,
        WaitSignal = 47
    }
}
