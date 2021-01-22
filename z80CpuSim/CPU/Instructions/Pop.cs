using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Pop : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC1, 1 },

            { 0xD1, 1 },

            { 0xE1, 1 },

            { 0xF1, 1 }

        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
    }
}
