using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestDjnz : ITest
    {
        public void Test()
        {
            Djnz djnz = new Djnz();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // Test DJNZ
            // use the example from the manual


        }
    }
}
