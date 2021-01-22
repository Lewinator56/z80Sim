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

        public void Handle(byte[] data)
        {
            Z80CPU z80 = Z80CPU.instance; // its just easier to type z80 rather than Z80CPU.instance

            switch (data[0])
            {
                case 0xB0:
                    OrWithRegister(z80.B);
                    break;
                case 0xB1:
                    OrWithRegister(z80.C);
                    break;
                case 0xB2:
                    OrWithRegister(z80.D);
                    break;
                case 0xB3:
                    OrWithRegister(z80.E);
                    break;
                case 0xB4:
                    OrWithRegister(z80.H);
                    break;
                case 0xB5:
                    OrWithRegister(z80.L);
                    break;
                case 0xB6:
                    OrWithMemoryAtLocation(new Pseudo16BitRegister(z80.H, z80.L));
                    break;
                case 0xB7:
                    OrWithRegister(z80.A);
                    break;
                case 0XF6:
                    OrWithValue(data[1]);
                    break;
            }
        }

        private void OrWithRegister(GenericRegister reg)
        {
            // TODO : Change this to 'tick' 
            UInt16 r = (UInt16)(reg.GetData() ^ Z80CPU.instance.A.GetData());
            Z80CPU.instance.A.SetData(r);
            //
        }

        private void OrWithMemoryAtLocation(Pseudo16BitRegister reg)
        {
            UInt16 r = (UInt16)(Z80CPU.instance.A.GetData() ^ Z80CPU.instance.ram.GetAddress(reg.GetData()));
            Z80CPU.instance.A.SetData(r);
        }

        private void OrWithValue(byte value)
        {
            UInt16 r = (UInt16)(Z80CPU.instance.A.GetData() ^ value);
            Z80CPU.instance.A.SetData(r);
        }

        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

    }
}
