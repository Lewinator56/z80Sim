using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.Tests
{
    interface ITest
    {
        public void Test();
    }
}
