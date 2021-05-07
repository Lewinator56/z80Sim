using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestExchange : ITest
    {
        public void Test()
        {
            // Test exchange instruction
            Exchange exc = new Exchange();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // Test exchange AF
            Z80.AF.SetData(0xFFEE); // Set AF to 100
            exc.Handle(new byte[] { 0x08 }); // exchange AF with AF'
            System.Diagnostics.Debug.Assert(Z80._A.GetData() == 0xFF && Z80._F.GetData() == 0xEE);


            // Test Exchange SP
            // use the example from the manual
            Z80.SP.SetData(0x8856);
            Z80.HL.SetData(0x7012);
            Z80.ram.SetAddress(0x8856, 0x11);
            Z80.ram.SetAddress(0x8857, 0x22);
            exc.Handle(new byte[] { 0xE3 });
            // 3 checks
            System.Diagnostics.Debug.Assert(Z80.SP.GetData() == 0x8856);
            System.Diagnostics.Debug.Assert(Z80.HL.GetData() == 0x2211);
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x8856) == 0x12);
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x8857) == 0x70);

            // Test Exchange X


            // Test Exchange DE
            // use the example from the manual
            Z80.DE.SetData(0x2822);
            Z80.HL.SetData(0x499A);
            exc.Handle(new byte[] { 0xEB });

            System.Diagnostics.Debug.Assert(Z80.DE.GetData() == 0x499A);
            System.Diagnostics.Debug.Assert(Z80.HL.GetData() == 0x2822);

        }
    }
}
