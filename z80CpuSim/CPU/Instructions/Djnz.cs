using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Djnz : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x10, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            // running unchecked because im doing byte -> sbyte -> ushort conversions
            // TODO sort out timings, i think i need to change them anyway
            unchecked
            {
                Z80.Tick(); // extra tick here as the first M cycle has 5 ticks (this is technically in the wrong place)

                // decrement B
                Z80.B.SetData((byte)(Z80.B.GetData() - 1));

                // this takes 5 ticks
                //check if its non-zero
                if (Z80.B.GetData() != 0)
                {
                    // data[1] needs to be converted to an sbyte to allow for a subtraction to take place if needed
                    Z80.PC.SetData((ushort)(Z80.PC.GetData() + (sbyte)data[1]));
                }
            }
            
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }
    }
}
