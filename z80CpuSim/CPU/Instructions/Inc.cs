using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Inc : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance;
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x03, 1 },
            { 0x13, 1 },
            { 0x23, 1 },
            { 0x33, 1 },

            { 0x04, 1 },
            { 0x14, 1 },
            { 0x24, 1 },
            { 0x34, 1 },

            { 0x0C, 1 },
            { 0x1C, 1 },
            { 0x2C, 1 },
            { 0x3C, 1 },

        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0]) {
                case 0x03:
                    IncrementR16Bit(Z80.BC);
                    break;
                case 0x13:
                    IncrementR16Bit(Z80.DE);
                    break;
                case 0x23:
                    IncrementR16Bit(Z80.HL);
                    break;
                case 0x33:
                    IncrementR16Bit(Z80.SP);
                    break;
                case 0x04:
                    IncrementR8Bit(Z80.B);
                    break;
                case 0x14:
                    IncrementR8Bit(Z80.D);
                    break;
                case 0x24:
                    IncrementR8Bit(Z80.H);
                    break;
                case 0x34:
                    IncrementAddress();
                    break;
                case 0x0c:
                    IncrementR8Bit(Z80.C);
                    break;
                case 0x1C:
                    IncrementR8Bit(Z80.E);
                    break;
                case 0x2C:
                    IncrementR8Bit(Z80.L);
                    break;
                case 0x3C:
                    IncrementR8Bit(Z80.A);
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
        private void IncrementR8Bit(EightBitRegister r)
        {
            // 4 ticks, no extra tick needed
            r.SetData((byte)(r.GetData() + (byte)1));

            //set flags
            SetFlagStates(r.GetData());
        }
        private void IncrementR16Bit(IRegister<ushort> r)
        {
            // 6 ticks, 2 extra ticks here
            // Passing a ushort IRegister to allow for 8 bit pairs AND for true 16 bit registers (such as SP)
            ushort a = r.GetData();
            Z80.Tick();
            a++;
            Z80.Tick();
            r.SetData(a);

            // does not affect flags
        }
        private void IncrementAddress()
        {
            // read the data in the memory address at HL, data lines are the buffer in reality
            byte a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());
            // use the ALU to increment the data lines, this takes a tick
            Z80.Tick();
            // this should overflow if the value FF is present
            a++;
            // write the data back
            Z80.Z80cu.WriteMemory(Z80.HL.GetData(), a);

            // set flags
            SetFlagStates(a);
        }

        private void SetFlagStates(byte checkByte)
        {
            // check negative
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (checkByte & 0x80) == 0x80);

            // check 0
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (checkByte == 0x00));

            // check half carry
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, (checkByte > 0x0F) || (checkByte - 1) == 0x0F);

            // set parity
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (checkByte - 1) == 0x7F);

            // reset N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);

            // C is unaffected
        }
    }
}
