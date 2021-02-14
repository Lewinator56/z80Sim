using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    
    class And : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xA0, 1 },
            { 0xA1, 1 },
            { 0xA2, 1 },
            { 0xA3, 1 },
            { 0xA4, 1 },
            { 0xA5, 1 },
            { 0xA6, 1 },
            { 0xA7, 1 },

            { 0xE6, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xA0:
                    AndRwithA(Z80.B);
                    break;
                case 0xA1:
                    AndRwithA(Z80.C);
                    break;
                case 0xA2:
                    AndRwithA(Z80.D);
                    break;
                case 0xA3:
                    AndRwithA(Z80.E);
                    break;
                case 0xA4:
                    AndRwithA(Z80.H);
                    break;
                case 0xA5:
                    AndRwithA(Z80.L);
                    break;
                case 0xA6:
                    AndAddressWithA();
                    break;
                case 0xA7:
                    AndRwithA(Z80.A);
                    break;
                case 0xE6:
                    AndValueWithA(data[1]);
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        // While I cannot find the exact timings for this, The manual states it takes 
        // A single M-cycle of 4 T-states, which is the time taken to fetch and decode an
        // instruction, subsequently, since 4 Ticks() have already elapsed, this is going to run
        // as if it ran within those execution ticks, so there are no Ticks()
        private void AndRwithA(EightBitRegister i)
        {
            Z80.A.SetData((byte)(Z80.A.GetData() & i.GetData()));
            SetFlagStates();
        }

        private void AndAddressWithA()
        {
            byte a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());
            Z80.A.SetData((byte)(a & Z80.A.GetData()));
            SetFlagStates();


        }

        private void AndValueWithA(byte value)
        {
            Z80.A.SetData((byte)(Z80.A.GetData() & value));
            SetFlagStates();
        }

        private void SetFlagStates()
        {
            // Set or reset S, 0x80 is 128, this is the 7th value in the A register, if it is 1 the value is negative and the bit is set
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (Z80.A.GetData() & 0x80) == 0x80);

            // Set or reset Z, 0x00 is 0, this checks if A is equal to 0 (guess i could have just done A == 0) 
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (Z80.A.GetData() | 0x00) == 0x00);

            // set H
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, true);

            // Reset P/V
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, false);

            // reset N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);
            //reset C
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, false);

        }
    }
}
