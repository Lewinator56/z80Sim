using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.UI
{
    class RegisterModelSource
    {
        public Type Type { get; set;}
        public string Name { get; set; }
        public int Size { get; set; }
        public uint DataIntUnsigned { get; set; }
        public string DataHex { get; set; }

    }
}
