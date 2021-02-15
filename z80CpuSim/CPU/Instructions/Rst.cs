using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Rst : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC7, 1 },
            { 0xCF, 1 },

            { 0xD7, 1 },
            { 0xDf, 1 },

            { 0xE7, 1 },
            { 0xEf, 1 },

            { 0xF7, 1 },
            { 0xFF, 1 }
        };

        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xC6:
                    Restart(0x00);
                    break;
                case 0xCf:
                    Restart(0x08);
                    break;
                case 0xD7:
                    Restart(0x10);
                    break;
                case 0xDF:
                    Restart(0x18);
                    break;
                case 0xE7:
                    Restart(0x20);
                    break;
                case 0xEF:
                    Restart(0x28);
                    break;
                case 0xF7:
                    Restart(0x30);
                    break;
                case 0xFF:
                    Restart(0x38);
                    break;



            }
        }

        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
        private void Restart(byte value)
        {
            Z80.Tick();
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));
            byte[] pc = BitConverter.GetBytes((ushort)(Z80.PC.GetData()));
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pc[1]);
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pc[0]);

            Z80.PC.SetData(value);


            
        }
    }
}
