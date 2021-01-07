using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Add : IInstruction
    {
        byte[] opcodes =
        {
            0x09, 0x19, 0x29, 0x39,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87
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
