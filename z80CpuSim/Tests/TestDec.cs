using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestDec : ITest
    {
        public void Test()
        {
            Dec dec = new Dec();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();


            // Test decrement 8 bit register

            Z80.A.SetData(0); // set A to 0
            dec.Handle(new byte[] { 0x3D });
            System.Diagnostics.Debug.Assert(Z80.A.GetData() == 255); // check A is 255

            // Test decrement 16 bit register

            Z80.BC.SetData(0); //set BC to 0
            dec.Handle(new byte[] { 0x0B });
            System.Diagnostics.Debug.Assert(Z80.BC.GetData() == 65535); // check BC is 65535
        }
    }
}
