using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Sbc : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance;
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x98, 1 },
            { 0x99, 1 },
            { 0x9A, 1 },
            { 0x9B, 1 },
            { 0x9C, 1 },
            { 0x9D, 1 },
            { 0x9E, 1 },
            { 0x9F, 1 },

            { 0xDE, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0]) {
                case 0x98:

                    break;
                case 0x99:

                    break;
                case 0x9A:

                    break;
                case 0x9B:

                    break;
                case 0x9C:

                    break;
                case 0x9D:

                    break;
                case 0x9E:

                    break;
                case 0x9F:

                    break;
                case 0xDE:

                    break;
            }

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        // 
        // Operations
        //

        // sbyte C is the carry bit, its 0 if its not set, 1 if it is set, used for carry operations
        private void SubRFromA(EightBitRegister i)
        {
            unchecked
            {
                sbyte b = (sbyte)i.GetData();
                sbyte a = (sbyte)Z80.A.GetData();
                sbyte c = (sbyte)(Z80.F.GetData() & 1);
                short r = (short)(a - b - c);
                Z80.A.SetData((byte)r);
                SetFlagStates(r, c);
            }

        }

        private void SubAddressFromA()
        {
            // I fully expect there to be overflows here, this must be unchecked for the byte to sbyte conversion
            unchecked
            {
                sbyte a = (sbyte)Z80.Z80cu.ReadMemory(Z80.HL.GetData());
                sbyte c = (sbyte)(Z80.F.GetData() & 1);
                short r = (short)((sbyte)Z80.A.GetData() - a - c);
                Z80.A.SetData((byte)r);
                SetFlagStates(r, c);
            }




        }

        private void SubValueFromA(byte value)
        {
            unchecked
            {
                sbyte b = (sbyte)value;
                sbyte a = (sbyte)Z80.A.GetData();
                sbyte c = (sbyte)(Z80.F.GetData() & 1);
                short r = (short)(a - b - c);
                Z80.A.SetData((byte)r);
                SetFlagStates(r, c);
            }
        }

        private void SetFlagStates(short r, sbyte s)
        {
            // Set or reset S, 0x80 is 128, this is the 7th value in the A register, if it is 1 the value is negative and the bit is set
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (Z80.A.GetData() & 0x80) == 0x80);

            // Set or reset Z, 0x00 is 0, this checks if A is equal to 0 (guess i could have just done A == 0) 
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (Z80.A.GetData() & 0x00) == 0x00);

            // set H if bit 3 is carried to 4 (check if the value is greater than 0x0f)
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, (r < 0x0F) || (r + s > 0x0F && r < 0x0F));

            // set P/V if the result overflows, basically, if its smaller than -128, which is 0x80
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (r > 127 || r < -128));

            // set N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, true);
            //set C if the value is < -128 (0x80)
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, (ushort)r > 0xff);

        }
    }
}
