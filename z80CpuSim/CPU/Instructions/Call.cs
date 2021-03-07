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
            switch (data[0])
            {
                case 0xC4:
                    CallConditional(FlagBit.Zero, false, data[1..2]);
                    break;
                case 0xCC:
                    CallConditional(FlagBit.Zero, true, data[1..2]);
                    break;
                case 0xCE:
                    Callnn(data[1..2]);
                    break;
                case 0xD4:
                    CallConditional(FlagBit.Carry, false, data[1..2]);
                    break;
                case 0xDC:
                    CallConditional(FlagBit.Carry, true, data[1..2]);
                    break;
                case 0xE4:
                    CallConditional(FlagBit.Parity, false, data[1..2]);
                    break;
                case 0xEC:
                    CallConditional(FlagBit.Parity, true, data[1..2]);
                    break;
                case 0xF4:
                    CallConditional(FlagBit.Sign, false, data[1..2]);
                    break;
                case 0xFC:
                    CallConditional(FlagBit.Sign, true, data[1..2]);
                    break;
            }
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

            // write the upper byte of the register pair to the first address
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pca[1]);

            // decrement the stack pointer (again)
            Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

            // write the lower byte of the register pair to the next address
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pca[0]);
            //----------------------------------------------------------------------
            //


            //
            // load nn into the PC
            //
            Z80.PC.SetData(BitConverter.ToUInt16(value));








        }
        private void CallConditional(FlagBit flag, bool condition, byte[] value)
        {
            if (Z80.Z80cu.GetFlagBit(flag) == condition)
            {
                
                byte[] pca = BitConverter.GetBytes(Z80.PC.GetData());
                // push PC to top of the memory stack
                //
                //----------------------------------------------------------------------
                Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

                // write the upper byte of the register pair to the first address
                Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pca[1]);

                // decrement the stack pointer (again)
                Z80.SP.SetData((ushort)(Z80.SP.GetData() - 1));

                // write the lower byte of the register pair to the next address
                Z80.Z80cu.WriteMemory(Z80.SP.GetData(), pca[0]);
                //----------------------------------------------------------------------
                //


                //
                // load nn into the PC
                //
                Z80.PC.SetData(BitConverter.ToUInt16(value));
            }
        }
    }
}
