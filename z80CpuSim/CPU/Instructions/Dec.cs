using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Dec : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x05, 1 },
            { 0x0B, 1 },
            { 0x0d, 1 },

            { 0x15, 1 },
            { 0x1B, 1 },
            { 0x1d, 1 },

            { 0x25, 1 },
            { 0x2B, 1 },
            { 0x2d, 1 },

            { 0x35, 1 },
            { 0x3B, 1 },
            { 0x3d, 1 }

        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0]) {
                case 0x05:
                    DecrementR8Bit(Z80.B);
                    break;
                case 0x0B:
                    DecrementR16Bit(Z80.BC);
                    break;
                case 0x0D:
                    DecrementR8Bit(Z80.C);
                    break;
                case 0x15:
                    DecrementR8Bit(Z80.C);
                    break;
                case 0x1B:
                    DecrementR16Bit(Z80.DE);
                    break;
                case 0x1D:
                    DecrementR8Bit(Z80.E);
                    break;
                case 0x25:
                    DecrementR8Bit(Z80.H);
                    break;
                case 0x2B:
                    DecrementR16Bit(Z80.HL);
                    break;
                case 0x2D:
                    DecrementR8Bit(Z80.L);
                    break;
                case 0x35:
                    DecrementAddress();
                    break;
                case 0x3B:
                    DecrementR16Bit(Z80.SP);
                    break;
                case 0x3D:
                    DecrementR8Bit(Z80.A);
                    break;
            }

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        //
        // operations
        //
        private void DecrementR8Bit(EightBitRegister r)
        {
            // 4 ticks, no extra tick needed
            
            r.SetData((byte)(r.GetData() - (byte)1));

            //set flags
            SetFlagStates(r.GetData());
        }
        private void DecrementR16Bit(IRegister<ushort> r)
        {
            // 6 ticks, 2 extra ticks here
            // Passing a ushort IRegister to allow for 8 bit pairs AND for true 16 bit registers (such as SP)
            ushort a = r.GetData();
            Z80.Tick();
            a--;
            Z80.Tick();
            r.SetData(a);

            // does not affect flags
        }
        private void DecrementAddress()
        {
            // read the data in the memory address at HL, data lines are the buffer in reality
            byte a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());
            // use the ALU to increment the data lines, this takes a tick
            Z80.Tick();
            // this should overflow if the value FF is present
            a--;
            // write the data back
            Z80.Z80cu.WriteMemory(Z80.HL.GetData(), a);

            // set flags
            SetFlagStates(a);
        }

        //
        // Rethink check for half carry,. at the moment, it doesnt work
        //
        private void SetFlagStates(byte checkByte)
        {
            // check negative
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (checkByte & 0x80) == 0x80);

            // check 0
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (checkByte == 0x00));

            // check half carry, since this is decrement, if the result is < 0x0F then a half carry took place, or if it used to be 0x0F
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, (checkByte < 0x0F) || (checkByte + 1) == 0x0F);

            // set parity
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (checkByte + 1) == 0x7F);

            // reset N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);

            // C is unaffected
        }
    }
}
