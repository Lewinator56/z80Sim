using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    
    class And : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance;
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xA0, 1 },
            { 0xA1, 1 },
            { 0xA2, 1 },
            { 0xA3, 1 },
            { 0xA4, 1 },
            { 0xA5, 1 },
            { 0xA6, 1 },
            { 0xA7, 1 },

            { 0xE6, 2 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {

        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        // While I cannot find the exact timings for this, The manual states it takes 
        // A single M-cycle of 4 T-states, which is the time taken to fetch and decode an
        // instruction, subsequently, since 4 Ticks() have already elapsed, this is going to run
        // as if it ran within those execution ticks, so there are no Ticks()
        private void AndRwithA(GenericRegister i)
        {
            Z80.A.SetData((UInt16)(Z80.A.GetData() & i.GetData()));
        }

        private void AndAddressWithA()
        {

        }

        private void AndValueWithA(byte value)
        {

        }
    }
}
