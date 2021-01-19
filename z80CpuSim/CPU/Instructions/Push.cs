using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Push : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC5, 1 },

            { 0xD5, 1 },

            { 0xE5, 1 },

            { 0xF5, 1 }

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
