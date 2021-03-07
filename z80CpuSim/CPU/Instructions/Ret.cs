using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Ret : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC0, 1 },
            { 0xC8, 1 },
            { 0xC9, 1 },

            { 0xD0, 1 },
            { 0xD8, 1 },

            { 0xE0, 1 },
            { 0xE8, 1 },

            { 0xF0, 1 },
            { 0xF8, 1 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xC0:
                    ReturnConditional(FlagBit.Zero, false);
                    break;
                case 0xC8:
                    ReturnConditional(FlagBit.Zero, true);
                    break;
                case 0xC9:
                    Return();
                    break;
                case 0xD0:
                    ReturnConditional(FlagBit.Carry, false);
                    break;
                case 0xD8:
                    ReturnConditional(FlagBit.Carry, true);
                    break;
                case 0xE0:
                    ReturnConditional(FlagBit.Parity, false);
                    break;
                case 0xE8:
                    ReturnConditional(FlagBit.Parity, true);
                    break;
                case 0xF0:
                    ReturnConditional(FlagBit.Sign, false);
                    break;
                case 0xF8:
                    ReturnConditional(FlagBit.Sign, true);
                    break;

            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void ReturnConditional(FlagBit bit, bool condition)
        {
            Z80.Tick(); // needed as the initial M cycle is 5 ticks
            if (Z80.Z80cu.GetFlagBit(bit) == condition)
            {
                byte lower = Z80.Z80cu.ReadMemory(Z80.SP.GetData());
                
                Z80.SP.SetData((ushort)(Z80.SP.GetData() + 1));
                byte upper = Z80.Z80cu.ReadMemory(Z80.PC.GetData());
                Z80.SP.SetData((ushort)(Z80.SP.GetData() + 1));
                Z80.PC.SetData(BitConverter.ToUInt16(new byte[] { lower, upper }));
            }
        }
        private void Return()
        {
            Z80.Tick();
            byte lower = Z80.Z80cu.ReadMemory(Z80.SP.GetData());

            Z80.SP.SetData((ushort)(Z80.SP.GetData() + 1));
            byte upper = Z80.Z80cu.ReadMemory(Z80.PC.GetData());
            Z80.SP.SetData((ushort)(Z80.SP.GetData() + 1));
            Z80.PC.SetData(BitConverter.ToUInt16(new byte[] { lower, upper }));

        }
    }
}
