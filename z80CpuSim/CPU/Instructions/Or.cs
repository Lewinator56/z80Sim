using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    class Or : IInstruction
    {
        // change to map


        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            {0xB0, 1 },
            {0xB1, 1 },
            {0xB2, 1 },
            {0xB3, 1 },
            {0xB4, 1 },
            {0xB5, 1 },
            {0xB6, 1 },
            {0xB7, 1 },
            {0xF6, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data, ICPU CPU)
        {
            Z80CPU z80 = (Z80CPU)CPU;

            switch (data[0])
            {
                case 0xB0:
                    OrWithRegister(z80.B, z80);
                    break;
                case 0xB1:
                    OrWithRegister(z80.C, z80);
                    break;
                case 0xB2:
                    OrWithRegister(z80.D, z80);
                    break;
                case 0xB3:
                    OrWithRegister(z80.E, z80);
                    break;
                case 0xB4:
                    OrWithRegister(z80.H, z80);
                    break;
                case 0xB5:
                    OrWithRegister(z80.L, z80);
                    break;
                case 0xB6:
                    OrWithMemoryAtLocation(new Pseudo16BitRegister(z80.H, z80.L), z80);
                    break;
                case 0xB7:
                    OrWithRegister(z80.A, z80);
                    break;
                case 0XF6:
                    OrWithValue(data[1], z80);
                    break;
            }
        }

        private void OrWithRegister(GenericRegister reg, Z80CPU cpu)
        {
            // TODO : Change this to 'tick' 
            UInt16 r = (UInt16)(reg.GetData() ^ cpu.A.GetData());
            cpu.A.SetData(r);
            //
        }

        private void OrWithMemoryAtLocation(Pseudo16BitRegister reg, Z80CPU cpu)
        {
            UInt16 r = (UInt16)(cpu.A.GetData() ^ cpu.ram.GetAddress(reg.GetData()));
            cpu.A.SetData(r);
        }

        private void OrWithValue(byte value, Z80CPU cpu)
        {
            UInt16 r = (UInt16)(cpu.A.GetData() ^ value);
            cpu.A.SetData(r);
        }
        
    }
}
