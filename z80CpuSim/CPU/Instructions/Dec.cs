using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Dec : IInstruction
    {
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

        public void Handle(byte[] data, ICPU CPU)
        {

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
    }
}
