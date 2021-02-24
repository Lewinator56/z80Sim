using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Registers
{
    class SixteenBitRegister : IRegister<ushort>
    {
        protected ushort data; // make available for subclasses

        public ushort GetData()
        {
            return data;
        }

        public void SetData(ushort data)
        {
            this.data = data;
        }

    }
}
