using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Add : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x09, 1 },
            { 0x19, 1 },
            { 0x29, 1 },
            { 0x39, 1 },

            { 0x80, 1 },
            { 0x81, 1 },
            { 0x82, 1 },
            { 0x83, 1 },
            { 0x84, 1 },
            { 0x85, 1 },
            { 0x86, 1 },
            { 0x87, 1 },

            { 0xC6, 2 }
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
