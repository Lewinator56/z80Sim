using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestPop : ITest
    {
        public void Test()
        {
            Pop pop = new Pop();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // Test pop
            // use the example from the manual

            Z80.SP.SetData(0x1000);
            Z80.ram.SetAddress(0x1000, 0x55);
            Z80.ram.SetAddress(0x1001, 0x33);
            pop.Handle(new byte[] { 0xe1 }); // POP HL

            System.Diagnostics.Debug.Assert(Z80.HL.GetData() == 0x3355);
            System.Diagnostics.Debug.Assert(Z80.SP.GetData() == 0x1002);

        }
    
    }
}
