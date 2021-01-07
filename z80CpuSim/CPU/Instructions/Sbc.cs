using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Sbc : IInstruction
    {
        byte[] opcodes =
        {
            0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,
            0xDE
        };
        public bool CanHandle(byte opcode)
        {
            foreach (byte opc in opcodes)
            {
                if (opc == opcode) return true;
            }
            return false;
        }

        public void Handle(byte[] data, ICPU CPU)
        {

        }
    }
}
