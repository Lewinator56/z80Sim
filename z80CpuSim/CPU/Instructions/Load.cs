using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Load : IInstruction
    {
         
        // ive tried to make this look neat and easier to read.... that didnt really work did it
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x01, 3 },
            { 0x02, 1 },
            { 0x06, 2 },
            { 0x0A, 1 },
            { 0x0E, 2 }, 

            { 0x11, 3 },
            { 0x12, 1 },
            { 0x16, 2 },
            { 0x1A, 1 },
            { 0x1E, 2 },

            { 0x21, 3 },
            { 0x22, 3 },
            { 0x26, 2 },
            { 0x2A, 3 },
            { 0x2E, 2 },

            { 0x31, 3 },
            { 0x32, 3 },
            { 0x36, 2 },
            { 0x3A, 3 },
            { 0x3E, 2 },

            { 0x40, 1 },
            { 0x41, 1 },
            { 0x42, 1 },
            { 0x43, 1 },
            { 0x44, 1 },
            { 0x45, 1 },
            { 0x46, 1 },
            { 0x47, 1 },
            { 0x48, 1 },
            { 0x49, 1 },
            { 0x4A, 1 },
            { 0x4B, 1 },
            { 0x4C, 1 },
            { 0x4D, 1 },
            { 0x4E, 1 },
            { 0x4F, 1 },

            { 0x50, 1 },
            { 0x51, 1 },
            { 0x52, 1 },
            { 0x53, 1 },
            { 0x54, 1 }, 
            { 0x55, 1 },
            { 0x56, 1 },
            { 0x57, 1 },
            { 0x58, 1 },
            { 0x59, 1 },
            { 0x5A, 1 },
            { 0x5B, 1 },
            { 0x5C, 1 },
            { 0x5D, 1 },
            { 0x5E, 1 },
            { 0x5F, 1 }, 

            { 0x60, 1 },
            { 0x61, 1 },
            { 0x62, 1 },
            { 0x63, 1 },
            { 0x64, 1 }, 
            { 0x65, 1 },
            { 0x66, 1 },
            { 0x67, 1 },
            { 0x68, 1 },
            { 0x69, 1 },
            { 0x6A, 1 },
            { 0x6B, 1 }, 
            { 0x6C, 1 },
            { 0x6D, 1 },
            { 0x6E, 1 },
            { 0x6F, 1 },
             
            { 0x70, 1 },
            { 0x71, 1 },
            { 0x72, 1 },
            { 0x73, 1 },
            { 0x74, 1 },
            { 0x75, 1 },
            { 0x77, 1 },
            { 0x78, 1 },
            { 0x79, 1 },
            { 0x7A, 1 },
            { 0x7B, 1 },
            { 0x7C, 1 },
            { 0x7D, 1 },
            { 0x7E, 1 },
            { 0x7F, 1 },  // 0x76 is the halt opcode so isn't here

            { 0xF9, 1 }
        };


        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            // These are here to allow for registers AND RAM to be written to, bear in mind however, if accessing IX and IY, these are combinations of IXL, IXH, IYL, ILH
            // and will actually require another specific definition of a EightBitRegister further down. For most cases, this will not be required, and the use of an IMemoryType
            // allows me to instantiate it as a EightBitRegister or as RAM (or ProgramCounter) depending on the inputs and outputs, this will save me some time and code
            IMemoryType O; // single register/address output
            IMemoryType I; // single register/address input

            Z80CPU z80 = Z80CPU.instance; // im too lazy to go through and replace all the z80's with Z80CPU.instance

            switch (data[0]) {
                case 0x40:
                    LoadRToR(z80.B, z80.B);
                    break;
                case 0x41:
                    LoadRToR(z80.C, z80.B);
                    break;
                case 0x42:
                    LoadRToR(z80.D, z80.B);
                    break;
                case 0x43:
                    LoadRToR(z80.E, z80.B);
                    break;
                case 0x44:
                    LoadRToR(z80.H, z80.B);
                    break;
                case 0x45:
                    LoadRToR(z80.L, z80.B);
                    break;
                case 0x46:
                    LoadDataAtAddressToR(new EightBitRegisterPair(z80.H, z80.L), z80.B);
                    break;

            }
            // do the reading


        }

        private void LoadRToR(EightBitRegister i, EightBitRegister o)
        {
            // TODO : Ticks
            i.SetData(o.GetData());
            
        }

        private void LoadDataAtAddressToR(EightBitRegisterPair i, EightBitRegister o)
        {
            // TODO : Ticks
            i.SetData(Z80CPU.instance.ram.GetAddress(o.GetData()));
        }

        


        
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }


    }
}
