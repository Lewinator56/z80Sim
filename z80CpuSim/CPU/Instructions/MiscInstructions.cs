using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class MiscInstructions : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x07, 1 }, // rlca
            { 0x0F, 1 }, // rrca

            { 0x1F, 1 }, // rra
            { 0x17, 1 }, // rla

            { 0x2F, 1 }, // cpl
            { 0x27, 1 }, // daa

            { 0x3F, 1 }, // ccf
            { 0x37, 1 }, // scf

            { 0x76, 1 }, // halt

            { 0x00, 1 } // no operation
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            bool cFlag;
            switch (data[0])
            {
                case 0x00:
                    // do nothing
                    break;
                case 0x07:

                    break;
                case 0x0F:

                    break;
                case 0x1F:
                    cFlag = Z80.Z80cu.GetFlagBit(FlagBit.Carry);
                    Z80.Z80cu.SetFlagBit(FlagBit.Carry, (Z80.A.GetData() & 0x01) == 0x01);
                    Z80.A.SetData((byte)((Z80.A.GetData() >> 1) | (cFlag? 0x80 : 0x00)));
                    break;
                case 0x17:
                    cFlag = Z80.Z80cu.GetFlagBit(FlagBit.Carry);
                    Z80.Z80cu.SetFlagBit(FlagBit.Carry, (Z80.A.GetData() & 0x80) == 0x80);
                    Z80.A.SetData((byte)((Z80.A.GetData() << 1) | (cFlag ? 0x01 : 0x00)));
                    break;
                case 0x2F:

                    break;
                case 0x27:

                    break;
                case 0x3F:

                    break;
                case 0x37:

                    break;
                case 0x76:
                    Z80.Z80cu.StopExecution();
                    break;
            }
        }

        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
    }
}
