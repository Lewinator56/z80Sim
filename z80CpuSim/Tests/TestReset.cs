using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;
using z80CpuSim.CPU.Instructions;

namespace z80CpuSim.Tests
{
    class TestReset : ITest
    {
        public void Test()
        {
            // use the example from the manual
            // set PC to 0x15b3;

            z80CpuSim.CPU.Z80CPU Z80 = z80CpuSim.CPU.Z80CPU.instance();
            Z80.Setup();

            Z80.PC.SetData(0x15b3);

            //execute RST 18h
            Rst rst = new Rst();
            rst.Handle(new byte[] { 0xDf });

            // check PC
            System.Diagnostics.Debug.Assert(Z80.PC.GetData() == 0x0018);

            // check high order byte of stack pointer
            byte h = Z80.Z80cu.ReadMemory((ushort)(Z80.SP.GetData() + 1));
            System.Diagnostics.Debug.Assert(h == 0x15);

            // check the low worder byteof the stack pointer
            byte l = Z80.Z80cu.ReadMemory((ushort)(Z80.SP.GetData()));
            System.Diagnostics.Debug.Assert(l == 0xb3);
        }
    }
}
