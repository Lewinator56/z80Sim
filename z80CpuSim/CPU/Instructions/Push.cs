using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;
namespace z80CpuSim.CPU.Instructions
{
    class Push : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC5, 1 },

            { 0xD5, 1 },

            { 0xE5, 1 },

            { 0xF5, 1 }

        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0]) {
                case 0xC5:
                    PushRToA(Z80.BC);
                    break;
                case 0xD5:
                    PushRToA(Z80.DE);
                    break;
                case 0xE5:
                    PushRToA(Z80.HL);
                    break;
                case 0xF5:
                    PushRToA(Z80.AF);
                    break;
            }

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void PushRToA(EightBitRegisterPair ebrp)
        {
            // initial M-cycle takes 5 ticks rather than 4, this is the delay for that 5th tick
            Z80.Tick();

            // Decrement the stack pointer
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

            // write the lower byte of the register pair to the first address
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), ebrp.lower.GetData());

            // decrement the stack pointer (again)
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

            // write the upper byte of the register pair to the next address
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), ebrp.upper.GetData());
        }

        // no flag states are set in this operation
    }
}
