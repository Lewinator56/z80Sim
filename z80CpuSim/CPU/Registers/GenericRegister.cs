using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Registers
{
    /** Generic register class **/
    class GenericRegister : Register
    {
        private ushort data;

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
