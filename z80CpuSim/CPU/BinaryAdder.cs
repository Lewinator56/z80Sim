using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    class BinaryAdder
    {
        Z80CPU Z80 = Z80CPU.instance();


        public ushort Add16Bit(ushort initial, ushort value, bool carry)
        {
            byte[] initialBytes = BitConverter.GetBytes(initial);
            byte[] addBytes = BitConverter.GetBytes(value);

            byte[] rLower = Add(initialBytes[0], addBytes[0], carry);
            byte[] rUpper = Add(initialBytes[1], addBytes[1], rLower[2] == 1); ;


            // set flags
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, rUpper[2] == 1);
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, rUpper[1] == 1);
            // sign is not set here
            return BitConverter.ToUInt16(new byte[] { rUpper[0], rLower[0] });
        }
        public byte Add8Bit(byte initial, byte value, bool carry)
        {
            
            byte[] r = Add(initial, value, carry);
            // set full carry bit
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, r[2] == 1);
            // set half carry bit
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, r[1] == 1);
            // set sign bit
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (r[0] & 0x80) == 0x80);

            //return the result
            return r[0];
        }
        private byte[] Add(byte initial, byte value, bool carry)
        {
            List<bool> result = new List<bool>();
            byte overflowCarry = 0;
            byte halfCarry = 0;

            for (int i = 0; i < 8; i++)
            {
                bool[] r = BitAdd((initial & (1 << i)) != 0, (value & (1 << i)) != 0, carry);
                System.Diagnostics.Debug.WriteLine(r[0]);
                result.Add(r[0]);
                carry = r[1];

                if (i == 3 && r[1])
                {
                    halfCarry = 1;
                }
                if (i == 7 && carry)
                {
                    overflowCarry = 1;
                }
            }

            // work out how to return the byte

            byte toReturn = 0;
            for (int i = 0; i < 8; i++)
            {
                if (result[i])
                {
                    toReturn |= (byte)(1 << (i));
                }
            }
            return new byte[] { toReturn, halfCarry, overflowCarry };

        }

        private bool[] BitAdd(bool a, bool b, bool c)
        {
            bool s = (a ^ b) ^ c;
            bool cOut = (a & b) || ((a ^ b) & c);
            return new bool[] { s, cOut };
        }

        
    }
}
