using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    interface Register
    {
        // data field : ushort
        /** ushort becuse the program counter is 16 bits, and to avoid using generic types all the registers should be 16 bits, HOWEVER it should be implemented
            in the CPU to only read the first 8 bits, additionally using a ushort makes checking for overflows easier and the 8th bit can be checked **/

        // Gets the data in the register instance
        public ushort GetData();

        // Sets the data in the register to the specified byte
        public void SetData(ushort data);
    }
}
