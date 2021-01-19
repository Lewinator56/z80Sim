using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Rst : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC7, 1 },
            { 0xCF, 1 },

            { 0xD7, 1 },
            { 0xDf, 1 },

            { 0xE7, 1 },
            { 0xEf, 1 },

            { 0xF7, 1 },
            { 0xFF, 1 }
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
