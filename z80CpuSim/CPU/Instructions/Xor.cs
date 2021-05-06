using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    class Xor : IInstruction
    {

        Z80CPU Z80 = Z80CPU.instance(); // its just easier to type z80 rather than Z80CPU.instance

        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xA8, 1 },
            { 0xA9, 1 },
            { 0xAA, 1 },
            { 0xAB, 1 },
            { 0xAC, 1 },
            { 0xAD, 1 },
            { 0xAE, 1 },
            { 0xAF, 1 },

            { 0xEE, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xA8:
                    XorRWithA(Z80.B);
                    break;
                case 0xA9:
                    XorRWithA(Z80.C);
                    break;
                case 0xAA:
                    XorRWithA(Z80.D);
                    break;
                case 0xAB:
                    XorRWithA(Z80.E);
                    break;
                case 0xAC:
                    XorRWithA(Z80.H);
                    break;
                case 0xAD:
                    XorRWithA(Z80.L);
                    break;
                case 0xAE:
                    XorAddressWithA();
                    break;
                case 0xAF:
                    XorRWithA(Z80.A);
                    break;
                case 0xEE:
                    XorValueWithA(data[1]);
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }


        private void XorRWithA(EightBitRegister i)
        {
            Z80.A.SetData((byte)(Z80.A.GetData() ^ i.GetData()));
            SetFlagStates();
        }

        private void XorAddressWithA()
        {
            byte a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());
            Z80.A.SetData((byte)(a ^ Z80.A.GetData()));
            SetFlagStates();


        }

        private void XorValueWithA(byte value)
        {
            Z80.A.SetData((byte)(Z80.A.GetData() ^ value));
            SetFlagStates();
        }

        private void SetFlagStates()
        {
            // Set or reset S, 0x80 is 128, this is the 7th value in the A register, if it is 1 the value is negative and the bit is set
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (Z80.A.GetData() & 0x80) == 0x80);

            // Set or reset Z, 0x00 is 0, this checks if A is equal to 0 (guess i could have just done A == 0) 
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (Z80.A.GetData() & 0x00) == 0x00);

            // reset H
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, false);

            // Reset P/V
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, false);

            // reset N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);
            //reset C
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, false);

        }


    }
}
