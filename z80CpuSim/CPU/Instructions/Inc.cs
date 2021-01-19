using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Inc : IInstruction
    {
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

        public void Handle(byte[] data, ICPU CPU)
        {

        }
    }
}
