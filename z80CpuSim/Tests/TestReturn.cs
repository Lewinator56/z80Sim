using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestReturn : ITest
    {
        public void Test()
        {
            Ret ret = new Ret();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // test return
            // use the example from the manual
            // RET (C9)
            Z80.PC.SetData(0x3535);
            Z80.SP.SetData(0x2000);
            Z80.ram.SetAddress(0x2000, 0xB5);
            Z80.ram.SetAddress(0x2001, 0x18);

            ret.Handle(new byte[] { 0xC9 });

            System.Diagnostics.Debug.Assert(Z80.SP.GetData() == 0x2002);
            System.Diagnostics.Debug.Assert(Z80.PC.GetData() == 0x18B5);


        }
    }
}
