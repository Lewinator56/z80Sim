using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class MiscInstructions
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x0F, 1 },
            { 0x1F, 1 },
            { 0x2F, 1 },
            { 0x3F, 1 }, 

            { 0x76, 1 } // halt
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
