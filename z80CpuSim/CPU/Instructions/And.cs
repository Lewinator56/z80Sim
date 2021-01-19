using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class And : IInstruction
    {
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

        public void Handle(byte[] data, ICPU CPU)
        {

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
    }
}
