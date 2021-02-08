using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Jp : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance;
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC2, 3 },
            { 0xC3, 3 },
            { 0xCA, 3 },

            { 0xD2, 3 },
            { 0xDA, 3 },

            { 0xE2, 3 },
            { 0xE9, 1 },
            { 0xEA, 3 },

            { 0xF2, 3 },
            { 0xFA, 3 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0xC2:
                    JumpToAddressConditional(data[1..2], FlagBit.Zero, false);
                    break;
                case 0xC3:
                    JumpToAddress(data[1..2]);
                    break;
                case 0xCA:
                    JumpToAddressConditional(data[1..2], FlagBit.Zero, true);
                    break;
                case 0xD2:
                    JumpToAddressConditional(data[1..2], FlagBit.Carry, false);
                    break;
                case 0xDA:
                    JumpToAddressConditional(data[1..2], FlagBit.Carry, true);
                    break;
                case 0xE2:
                    JumpToAddressConditional(data[1..2], FlagBit.Parity, false);
                    break;
                case 0xE9:
                    JumpToHL();
                    break;
                case 0xEA:
                    JumpToAddressConditional(data[1..2], FlagBit.Parity, true);
                    break;
                case 0xF2:
                    JumpToAddressConditional(data[1..2], FlagBit.Sign, false);
                    break;
                case 0xFA:
                    JumpToAddressConditional(data[1..2], FlagBit.Sign, true);
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void JumpToAddress(byte[] address)
        {
            // this one is easy too
            Z80.PC.SetData(BitConverter.ToUInt16(address));
        }

        private void JumpToAddressConditional(byte[] address, FlagBit flag, bool condition)
        {
            // this is a bit more difficult
            if (Z80.Z80cu.GetFlagBit(flag) == condition)
            {
                Z80.PC.SetData(BitConverter.ToUInt16(address));
            }
        }

        private void JumpToHL()
        {
            // at least one of these is easy
            Z80.PC.SetData(Z80.HL.GetData());
        }
    }
}
