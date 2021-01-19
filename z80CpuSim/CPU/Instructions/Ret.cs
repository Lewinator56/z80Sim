using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Ret : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC0, 1 },
            { 0xC8, 1 },
            { 0xC9, 1 },

            { 0xD0, 1 },
            { 0xD8, 1 },

            { 0xE0, 1 },
            { 0xE8, 1 },

            { 0xF0, 1 },
            { 0xF8, 1 }
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
