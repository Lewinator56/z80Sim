using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class And : IInstruction
    {
        byte[] opcodes =
        {
            0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7
        };
        public bool CanHandle(byte opcode)
        {
            foreach (byte opc in opcodes)
            {
                if (opc == opcode) return true;
            }
            return false;
        }

        public void Handle(byte[] data)
        {

        }
    }
}
