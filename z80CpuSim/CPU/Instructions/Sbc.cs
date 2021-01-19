using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Sbc : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x98, 1 },
            { 0x99, 1 },
            { 0x9A, 1 },
            { 0x9B, 1 },
            { 0x9C, 1 },
            { 0x9D, 1 },
            { 0x9E, 1 },
            { 0x9F, 1 },

            { 0xDE, 2 }
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
