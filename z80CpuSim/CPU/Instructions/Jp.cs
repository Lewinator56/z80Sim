using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Jp : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC2, 3 },
            { 0xC3, 3 },
            { 0xCA, 3 },

            { 0xD2, 3 },
            { 0xDA, 3 },

            { 0xE2, 3 },
            { 0xE9, 1 },
            { 0xEA, 3 },

            { 0xF2, 3 },
            { 0xFA, 3 }
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
