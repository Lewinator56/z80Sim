using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Or : IInstruction
    {
        byte[] opcodes =
        {
            0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7
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
