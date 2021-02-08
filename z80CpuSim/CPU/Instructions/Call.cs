using System;
using System.Collections.Generic;
using System.Text;


namespace z80CpuSim.CPU.Instructions
{
    class Call : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0xC4, 3 },
            { 0xCC, 3 },
            { 0xCE, 3 },

            { 0xD4, 3 },
            { 0xDC, 3 },

            { 0xE4, 3 },
            { 0xEC, 3 },

            { 0xF4, 3 },
            { 0xFC, 3 },
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

        //
        // OK, this is a bit complicated, but i think i understand it, mostly
        private void Callnn(byte[] value)
        {
            //
            // Sort out timings, i really have NO idea whats going on with them here
            //
            // read the PC into a byte array
            byte[] pca = BitConverter.GetBytes(Z80.PC.GetData());
            // push PC to top of the memory stack
            //
            //----------------------------------------------------------------------
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

            // write the lower byte of the register pair to the first address
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pca[0]);

            // decrement the stack pointer (again)
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

            // write the upper byte of the register pair to the next address
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pca[1]);
            //----------------------------------------------------------------------
            //


            //
            // load nn into the PC
            //
            Z80.PC.SetData(BitConverter.ToUInt16(value));








        }
        private void CallSFlagStateValue(FlagBit flag, ushort value)
        {

        }
    }
}
