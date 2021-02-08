using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    class Z80ALU
    {
        Z80CPU Z80 = Z80CPU.instance;

        public byte Add(byte initial, byte value, bool carry)
        {
            List<bool> result = new List<bool>();
            bool c = carry;

            for (int i = 0; i < 8; i++)
            {
                bool[] r = BitAdd((initial & (1 << i)) != 0, (value & (1 << i)) != 0, carry);
                result.Add(r[0]);
                carry = r[1];

                if (i == 3 && r[1])
                {
                    Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, true);
                }
                if (i == 7 && carry)
                {
                    Z80.Z80cu.SetFlagBit(FlagBit.Carry, true);
                }
            }

            // work out how to return the byte

        }

        private bool[] BitAdd(bool a, bool b, bool c)
        {
            bool s = (a ^ b) ^ c;
            bool cOut = (a & b) || ((a ^ b) & c);
            return new bool[] { s, cOut };
        }

        
    }
}
