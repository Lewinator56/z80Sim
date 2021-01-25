using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Registers
{
    /** Program counter register class, in addition to the standard methods, this also has an increment method **/
    // change to extend EightBitRegister
    class ProgramCounter : SixteenBitRegister
    {
        public void Increment()
        {
            data++;
        }
    }
}
