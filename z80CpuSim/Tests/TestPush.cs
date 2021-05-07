using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestPush : ITest
    {
        public void Test()
        {
            Push push = new Push();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // Test push
            // use the example from the manual

            Z80.AF.SetData(0x2233);
            Z80.SP.SetData(0x1007);
            push.Handle(new byte[] { 0xf5 }); // PUSH AF

            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x1006) == 0x22);
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x1005) == 0x33);
            System.Diagnostics.Debug.Assert(Z80.SP.GetData() == 0x1005);

        }
    }
}
