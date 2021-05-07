using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU.Registers;

namespace z80CpuSim.Tests
{
    class TestCall : ITest
    {
        public void Test() 
        {
            // Test Call
            Call call = new Call();
            z80CpuSim.CPU.Z80CPU.instance().Setup();
            var Z80 = z80CpuSim.CPU.Z80CPU.instance();

            // call 
            // use the example from the manual
            Z80.PC.SetData(0x1A47);
            Z80.SP.SetData(0x3002);
            Z80.ram.SetAddress(0x1A47, 0xCD);
            Z80.ram.SetAddress(0x1A48, 0x35);
            Z80.ram.SetAddress(0x1A49, 0x21);
            call.Handle(new byte[] { 0xCD, 0x35, 0x21 }); // CALL 2135h (yep, its backwards??)

            // check outputs
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x3001) == 0x1A);
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x3000) == 0x4A);
            System.Diagnostics.Debug.Assert(Z80.SP.GetData() == 0x3000);
            System.Diagnostics.Debug.Assert(Z80.PC.GetData() == 0x2135);

            // clear the RAM for the next test
            Z80.ram.Setup(new byte[65535]);


            // call conditional
            // use the example from the manual
            Z80.PC.SetData(0x1A47);
            Z80.SP.SetData(0x3002);
            Z80.ram.SetAddress(0x1A47, 0xCD);
            Z80.ram.SetAddress(0x1A48, 0x35);
            Z80.ram.SetAddress(0x1A49, 0x21);
            Z80.Z80cu.SetFlagBit(CPU.FlagBit.Carry, false);
            call.Handle(new byte[] { 0xD4, 0x35, 0x21 }); // CALL NC 2135h (yep, its backwards??)

            // check outputs
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x3001) == 0x1A);
            System.Diagnostics.Debug.Assert(Z80.ram.GetAddress(0x3000) == 0x4A);
            System.Diagnostics.Debug.Assert(Z80.SP.GetData() == 0x3000);
            System.Diagnostics.Debug.Assert(Z80.PC.GetData() == 0x2135);
        }

    }

}
