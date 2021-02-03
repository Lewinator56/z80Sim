using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
namespace z80CpuSim.CPU.Instructions
{
    class Pop : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance;
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC1, 1 },

            { 0xD1, 1 },

            { 0xE1, 1 },

            { 0xF1, 1 }

        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xC1:
                    PopRFromA(Z80.BC);
                    break;
                case 0xD1:
                    PopRFromA(Z80.DE);
                    break;
                case 0xE1:
                    PopRFromA(Z80.HL);
                    break;
                case 0xF1:
                    PopRFromA(Z80.AF);
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void PopRFromA(EightBitRegisterPair ebrp)
        {
            // initial M-cycle takes 5 ticks rather than 4, this is the delay for that 5th tick
            Z80.Tick();

            // increment the stack pointer
            Z80.SP.SetData((ushort)(Z80.SP.GetData() + 1));

            //read the memory at SP into the upper part of the register pair
            ebrp.upper.SetData(Z80.Z80cu.ReadMemory(Z80.SP.GetData()));

            // increment the stack pointer (again)
            Z80.SP.SetData((ushort)(Z80.SP.GetData() + 1));

            // read the memory at SP into the lower part of the register pair
            ebrp.lower.SetData(Z80.Z80cu.ReadMemory(Z80.SP.GetData()));
        }

        // no flag states are set in this operation
    }
}
