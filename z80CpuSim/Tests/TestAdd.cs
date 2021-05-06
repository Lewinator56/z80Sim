using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestAdd : ITest
    {
        public void Test()
        {
            Add add = new Add();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            // test addition

            // test register
            unchecked
            {
                z80CpuSim.CPU.Z80CPU.instance().A.SetData(15); // put 15 in A
                z80CpuSim.CPU.Z80CPU.instance().B.SetData((byte)5); // put 5 in B
                add.Handle(new byte[] { 0x80 });
                System.Diagnostics.Debug.Assert(z80CpuSim.CPU.Z80CPU.instance().A.GetData() == 20);
                //System.Diagnostics.Debug.Assert((z80CpuSim.CPU.Z80CPU.instance().F.GetData() & 2) == 2);
                System.Diagnostics.Debug.WriteLine(z80CpuSim.CPU.Z80CPU.instance().F.GetData());

            }

            // test value
            z80CpuSim.CPU.Z80CPU.instance().A.SetData(15); // put 15 in A
            add.Handle(new byte[] { 0xC6, 5 }); // add 5 from A
            System.Diagnostics.Debug.Assert(z80CpuSim.CPU.Z80CPU.instance().A.GetData() == 20);



        }
    }
}
