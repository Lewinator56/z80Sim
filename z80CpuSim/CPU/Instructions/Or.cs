using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    class Or : IInstruction
    {

        Z80CPU Z80 = Z80CPU.instance; // its just easier to type z80 rather than Z80CPU.instance

        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            {0xB0, 1 },
            {0xB1, 1 },
            {0xB2, 1 },
            {0xB3, 1 },
            {0xB4, 1 },
            {0xB5, 1 },
            {0xB6, 1 },
            {0xB7, 1 },
            {0xF6, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            

            switch (data[0])
            {
                case 0xB0:
                    OrRWithA(Z80.B);
                    break;
                case 0xB1:
                    OrRWithA(Z80.C);
                    break;
                case 0xB2:
                    OrRWithA(Z80.D);
                    break;
                case 0xB3:
                    OrRWithA(Z80.E);
                    break;
                case 0xB4:
                    OrRWithA(Z80.H);
                    break;
                case 0xB5:
                    OrRWithA(Z80.L);
                    break;
                case 0xB6:
                    OrAddressWithA();
                    break;
                case 0xB7:
                    OrRWithA(Z80.A);
                    break;
                case 0XF6:
                    OrValueWithA(data[1]);
                    break;
            }
        }


        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void OrRWithA(EightBitRegister i)
        {
            Z80.A.SetData((byte)(Z80.A.GetData() | i.GetData()));
            SetFlagStates();
        }

        private void OrAddressWithA()
        {
            byte a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());
            Z80.A.SetData((byte)(a | Z80.A.GetData()));
            SetFlagStates();


        }

        private void OrValueWithA(byte value)
        {
            Z80.A.SetData((byte)(Z80.A.GetData() | value));
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

            // set P/V - check if the parity is even, so we will do a modulus with 2 here
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (Z80.A.GetData() % 2) == 0);

            // reset N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);
            //reset C
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, false);

        }

    }
}
