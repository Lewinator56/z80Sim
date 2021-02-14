using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU;

namespace z80CpuSim.CPU.Instructions
{
    class Cp : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            // ok, so as far as i underastand, this is a comparrison, 
            // done by subtracting the register r from A, A is unaffected,
            // but the flags are, so its basically a subtraction, without changing the
            // accumulator
            { 0xB8, 1 },
            { 0xB9, 1 },
            { 0xBA, 1 },
            { 0xBB, 1 },
            { 0xBC, 1 },
            { 0xBD, 1 },
            { 0xBE, 1 },
            { 0xBF, 1 },

            { 0xFE, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xB8:
                    SubRFromA(Z80.B);
                    break;
                case 0xB9:
                    SubRFromA(Z80.C);
                    break;
                case 0xBA:
                    SubRFromA(Z80.D);
                    break;
                case 0xBB:
                    SubRFromA(Z80.E);
                    break;
                case 0xBC:
                    SubRFromA(Z80.H);
                    break;
                case 0xBD:
                    SubRFromA(Z80.L);
                    break;
                case 0xBE:
                    SubAddressFromA();
                    break;
                case 0xBF:
                    SubRFromA(Z80.A);
                    break;
                case 0xFE:
                    SubValueFromA(data[1]);
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        //
        // operations
        // Yes, i can basically just copy the code from sub for this, its basically doing the same thing!

        private void SubRFromA(EightBitRegister i)
        {
            unchecked
            {
                byte b = i.GetData();
                byte a = Z80.A.GetData();
                byte r = Z80.BinAdd.Add8Bit(a, (byte)-b, false);
                //Z80.A.SetData(r);
                SetFlagStates((sbyte)r);
            }

        }

        private void SubAddressFromA()
        {
            // I fully expect there to be overflows here, this must be unchecked for the byte to sbyte conversion
            unchecked
            {
                byte a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());
                byte r = Z80.BinAdd.Add8Bit(Z80.A.GetData(), (byte)-a, false);
                //Z80.A.SetData(r);
                SetFlagStates((sbyte)r);
            }




        }

        private void SubValueFromA(byte value)
        {
            unchecked
            {
                byte b = value;
                byte a = Z80.A.GetData();
                byte r = Z80.BinAdd.Add8Bit(a, (byte)-b, false);
                //Z80.A.SetData(r);
                SetFlagStates((sbyte)r);
            }
        }

        private void SetFlagStates(sbyte r)
        {
            // Set or reset S, 0x80 is 128, this is the 7th value in the A register, if it is 1 the value is negative and the bit is set
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (Z80.A.GetData() & 0x80) == 0x80);

            // Set or reset Z, 0x00 is 0, this checks if A is equal to 0 (guess i could have just done A == 0) 
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (Z80.A.GetData() | 0x00) == 0x00);

            // set H if bit 3 is carried to 4 (check if the value is greater than 0x0f)
            //Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, (r < 0x0F) || (r + s > 0x0F && r < 0x0F)); dont need this anymore either

            // set P/V if the result overflows, basically, if its smaller than -128, which is 0x80
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (r > 127 || r < -128));

            // set N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, true);
            //set C if the value is < -128 (0x80)
            //Z80.Z80cu.SetFlagBit(FlagBit.Carry, (ushort)r > 0xff); dont need this anymore

        }
    }
}
