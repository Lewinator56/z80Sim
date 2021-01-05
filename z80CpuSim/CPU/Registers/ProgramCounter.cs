using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Registers
{
    /** Program counter register class, in addition to the standard methods, this also has an increment method **/
    class ProgramCounter : IRegister
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

        public void Increment()
        {
            data++;
        }
    }
}
