using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Jr : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x18, 2 },

            { 0x20, 2 },
            { 0x28, 2 },

            { 0x30, 2 },
            { 0x38, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0x18:
                    Jump(data[1]);
                    break;
                case 0x20:
                    JumpConditional(FlagBit.Zero, false, data[1]);
                    break;
                case 0x28:
                    JumpConditional(FlagBit.Zero, true, data[1]);
                    break;
                case 0x30:
                    JumpConditional(FlagBit.Carry, false, data[1]);
                    break;
                case 0x38:
                    JumpConditional(FlagBit.Carry, true, data[1]);
                    break;

            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void JumpConditional(FlagBit bit, bool condition, byte jumpSize)
        {
            if (Z80.Z80cu.GetFlagBit(bit) == condition)
            {
                Z80.Z80cu.SetFlagBit(bit, !Z80.Z80cu.GetFlagBit(bit));
                Z80.PC.SetData((ushort)(Z80.PC.GetData() + jumpSize));
            }
        }
        private void Jump(byte jumpSize)
        {
            Z80.PC.SetData((ushort)(Z80.PC.GetData() + jumpSize));
        }
    }
}
