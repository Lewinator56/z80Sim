using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class MiscInstructions
    {
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

            { 0x76, 1 } // halt
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data, ICPU CPU)
        {

        }

        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
    }
}
