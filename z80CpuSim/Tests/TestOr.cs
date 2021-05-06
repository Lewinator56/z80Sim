using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestOr : ITest
    {
        public void Test() {

            // Test OR
            Or or = new Or();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();
            // Test registers

            Z80.A.SetData(0x0F); // set A to 0F
            Z80.B.SetData(0xF0); // set B to F0

            or.Handle(new byte [] { 0xB0 }); // Or A with B
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 0xFF); // check A contains FF

            // reset A to 0F
            Z80.A.SetData(0x0F);

            // Test value
            or.Handle(new byte[] { 0xF6, 0x10 });
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 0x1F); // check A contains 1F
        }
    }
}
