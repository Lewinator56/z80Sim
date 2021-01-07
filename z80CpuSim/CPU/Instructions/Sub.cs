using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Sub : IInstruction
    {
        byte[] opcodes =
        {
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97
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
