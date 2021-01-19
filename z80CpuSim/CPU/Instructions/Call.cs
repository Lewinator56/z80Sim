using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Call : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC4, 3 },
            { 0xCC, 3 },
            { 0xCE, 3 },

            { 0xD4, 3 },
            { 0xDC, 3 },

            { 0xE4, 3 },
            { 0xEC, 3 },

            { 0xF4, 3 },
            { 0xFC, 3 },
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
