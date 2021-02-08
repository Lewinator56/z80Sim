using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    class Adc : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x88, 1 },
            { 0x89, 1 },
            { 0x8A, 1 },
            { 0x8B, 1 },
            { 0x8C, 1 },
            { 0x8D, 1 },
            { 0x8E, 1 },
            { 0x8F, 1 },

            { 0xCE, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0x88:
                    AddRToA(Z80.B);
                    break;
                case 0x89:
                    AddRToA(Z80.C);
                    break;
                case 0x8A:
                    AddRToA(Z80.D);
                    break;
                case 0x8B:
                    AddRToA(Z80.E);
                    break;
                case 0x8C:
                    AddRToA(Z80.H);
                    break;
                case 0x8D:
                    AddRToA(Z80.L);
                    break;
                case 0x8E:
                    AddHLToA();
                    break;
                case 0x8F:
                    AddRToA(Z80.A);
                    break;
                case 0xCE:
                    AddValueToA(data[1]);
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

        private void AddRToA(EightBitRegister i)
        {

            unchecked
            {
                sbyte b = (sbyte)i.GetData();
                sbyte a = (sbyte)Z80.A.GetData();
                sbyte c = (sbyte)(Z80.F.GetData() & 1);
                short r = (short)(a + b + c);
                Z80.A.SetData((byte)r);
                SetFlagStates(r);
            }


        }

        private void AddHLToA()
        {

            unchecked
            {
                sbyte a = (sbyte)Z80.Z80cu.ReadMemory(Z80.HL.GetData());
                sbyte c = (sbyte)(Z80.F.GetData() & 1);
                short r = (short)(a + (sbyte)Z80.A.GetData() + c);
                Z80.A.SetData((byte)r);
                SetFlagStates(r);
            }
        }

        private void AddValueToA(byte value)
        {
            unchecked
            {
                sbyte b = (sbyte)value;
                sbyte a = (sbyte)Z80.A.GetData();
                sbyte c = (sbyte)(Z80.F.GetData() & 1);
                short r = (short)(a + b + c);
                Z80.A.SetData((byte)r);
                SetFlagStates(r);
            }
        }

        private void SetFlagStates(short r)
        {
            // Set or reset S, 0x80 is 128, this is the 7th value in the A register, if it is 1 the value is negative and the bit is set
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (Z80.A.GetData() & 0x80) == 0x80);

            // Set or reset Z, 0x00 is 0, this checks if A is equal to 0 (guess i could have just done A == 0) 
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (Z80.A.GetData() & 0x00) == 0x00);

            // set H if bit 3 is carried to 4 (check if the value is greater than 0x0f)
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, (Z80.A.GetData() > 0x0F));

            // set P/V if the result overflows, basically, if its smaller than -128, which is 0x80
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (r > 127 || r < -128));

            // set N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);
            //set C if the value is < -128 (0x80)
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, (ushort)r > 0xff);
        }
    }
}
