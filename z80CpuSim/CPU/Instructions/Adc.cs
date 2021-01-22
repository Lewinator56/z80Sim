using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Adc : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x88, 1 },
            { 0x89, 1 },
            { 0x8A, 1 },
            { 0x8B, 1 },
            { 0x8C, 1 },
            { 0x8D, 1 },
            { 0x8E, 1 },
            { 0x8F, 1 },

            { 0xCE, 2 }
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
