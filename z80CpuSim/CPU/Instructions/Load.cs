using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Load : IInstruction
    {
         
        // ive tried to make this look neat and easier to read.... that didnt really work did it
        byte[] opcodes = 
        {
            0x01, 0x02, 0x06, 0x0A, 0x0E,
            0x11, 0x12, 0x16, 0x1A, 0x1E,
            0x21, 0x22, 0x26, 0x2A, 0x2E,
            0x31, 0x32, 0x36, 0x3A, 0x3E,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F,
            0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F,
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F // 0x76 is the halt opcode so isn't here
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
            // These are here to allow for registers AND RAM to be written to, bear in mind however, if accessing IX and IY, these are combinations of IXL, IXH, IYL, ILH
            // and will actually require another specific definition of a GenericRegister further down. For most cases, this will not be required, and the use of an IMemoryType
            // allows me to instantiate it as a GenericRegister or as RAM (or ProgramCounter) depending on the inputs and outputs, this will save me some time and code
            IMemoryType O; // single register/address output
            IMemoryType I; // single register/address input

            Z80CPU z80 = (Z80CPU)CPU

            switch (data[0]) {
                case 0x40:
                    I = z80.B;
                    O = z80.B;
                    break;
                case 0x41:
                    I = z80.C;
                    O = z80.B;
                    break;
                case 0x42:
                    I = z80.D;
                    O = z80.B;
                    break;
                case 0x43:
                    I = z80.E;
                    O = z80.B;
                    break;
                case 0x44:
                    I = z80.H;
                    O = z80.B;
                    break;
                case 0x45:
                    I = z80.L;
                    O = z80.B;
                    break;
                case 0x46:
                    I = new Pseudo16BitRegister(z80.H, z80.L);
                    O = z80.B;
                    break;

            }
        }
    }
}
