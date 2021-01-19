using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Cp : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xB8, 1 },
            { 0xB9, 1 },
            { 0xBA, 1 },
            { 0xBB, 1 },
            { 0xBC, 1 },
            { 0xBD, 1 },
            { 0xBE, 1 },
            { 0xBF, 1 },

            { 0xFE, 2 }
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
