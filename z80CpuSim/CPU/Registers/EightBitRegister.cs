using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Registers
{
    /** Generic register class **/
    class EightBitRegister : IRegister<byte>
    {
        private byte data; // make available for subclasses

        public byte GetData()
        {
            return data;
        }

        public void SetData(byte data)
        {
            this.data = data;
        }

        
    }
}
