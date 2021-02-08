using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestSub : ITest
    {
        
        public void Test()
        {
            Sub sub = new Sub();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            // test subtraction
            // test register
            unchecked
            {
                z80CpuSim.CPU.Z80CPU.instance().A.SetData(15); // put 15 in A
                z80CpuSim.CPU.Z80CPU.instance().B.SetData((byte)5); // put 5 in B
                sub.Handle(new byte[] { 0x90 });
                System.Diagnostics.Debug.Assert(z80CpuSim.CPU.Z80CPU.instance().A.GetData() == 10);
                //System.Diagnostics.Debug.Assert((z80CpuSim.CPU.Z80CPU.instance().F.GetData() & 2) == 2);
                System.Diagnostics.Debug.WriteLine(z80CpuSim.CPU.Z80CPU.instance().F.GetData());

            }

            // test value
            z80CpuSim.CPU.Z80CPU.instance().A.SetData(15); // put 15 in A
            sub.Handle(new byte[] { 0xD6, 5 }); // subtract 5 from A
            System.Diagnostics.Debug.Assert(z80CpuSim.CPU.Z80CPU.instance().A.GetData() == 10);

            

        }
    }
}
