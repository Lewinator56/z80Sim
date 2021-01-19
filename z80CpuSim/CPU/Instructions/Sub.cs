using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Sub : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x90, 1 },
            { 0x91, 1 },
            { 0x92, 1 },
            { 0x93, 1 },
            { 0x94, 1 },
            { 0x95, 1 },
            { 0x96, 1 },
            { 0x97, 1 },

            { 0xD6, 2 }
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
