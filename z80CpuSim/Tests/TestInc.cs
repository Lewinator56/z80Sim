using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestInc : ITest
    {
        public void Test()
        {
            Inc inc = new Inc();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // Test increment 8 bit register

            Z80.A.SetData(0); // set A to 0
            inc.Handle(new byte[] { 0x3C });
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 1); // check A is 1

            // Test increment 16 bit register

            Z80.BC.SetData(0); //set BC to 0
            inc.Handle(new byte[] { 0x03 });
            System.Diagnostics.Debug.Assert(Z80.BC.GetData() == 1);


        }
    }
}
