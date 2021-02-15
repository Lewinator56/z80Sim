using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU;

namespace z80CpuSim.Tests
{
    class TestAdder : ITest
    {
        public void Test()
        {
            Console.WriteLine("this is a test");
            BinaryAdder ba = new BinaryAdder();
            Z80CPU.instance().Setup();
            // test bytes
            byte a = 0x0F;
            byte b = 0x0F;
            // expect 30
            byte c = ba.Add8Bit(a, b, false);
            
            System.Diagnostics.Debug.WriteLine(c);

            // lets try a subtraction
            // do it unchecked as i want to convert from an sbyte because im too lazy to do the conversion
            unchecked
            {
                // 15 - 5
                byte a1 = 15;
                byte b1 = (byte)-5;

                byte c1 = ba.Add8Bit(a1, b1, false);

                System.Diagnostics.Debug.WriteLine(c1);
            }

            // add a negative (subtraction)
            byte a2 = 15;
            byte b2 = 20;

            byte c2 = ba.Add8Bit(a2, (byte)-b2, false);
            System.Diagnostics.Debug.WriteLine((sbyte)c2);


        }
    }
}
