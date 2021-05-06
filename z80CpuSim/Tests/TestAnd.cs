using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestAnd : ITest
    {
        public void Test()
        {

            // Test AND
            And and = new And();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();
            // Test registers

            Z80.A.SetData(0x0F); // set A to 0F
            Z80.B.SetData(0xF0); // set B to F0

            and.Handle(new byte[] { 0xA0 }); // And A with B
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 0x00); // check A contains 0

            // reset A to 0F
            Z80.A.SetData(0x0F);

            // Test value
            and.Handle(new byte[] { 0xE6, 0x01 });
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 0x01); // check A contains 01
        }
    }
}
