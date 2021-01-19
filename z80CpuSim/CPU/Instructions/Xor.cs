using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Xor : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xA8, 1 },
            { 0xA9, 1 },
            { 0xAA, 1 },
            { 0xAB, 1 },
            { 0xAC, 1 },
            { 0xAD, 1 },
            { 0xAE, 1 },
            { 0xAF, 1 },

            { 0xEE, 2 }
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
