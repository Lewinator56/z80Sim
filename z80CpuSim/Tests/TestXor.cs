using System;
using System.Collections.Generic;
using System.Text;
using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestXor : ITest
    {
        public void Test()
        {

            // Test XOR
            Xor xor = new Xor();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();
            // Test registers

            Z80.A.SetData(0x0F); // set A to 0F
            Z80.B.SetData(0xF0); // set B to F0

            xor.Handle(new byte[] { 0xA8 }); // Or A with B
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 0xFF); // check A contains FF

            // reset A to 0F
            Z80.A.SetData(0x0F);

            // Test value
            xor.Handle(new byte[] { 0xEE, 0x01 });
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 0x0E); // check A contains 0E
        }
    }
}
