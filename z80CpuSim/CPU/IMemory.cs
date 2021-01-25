using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    /** Memory interface
     * Defines the core methods that any secodary memory type must use
     * */
    interface IMemory : IMemoryType
    {
        // Gets the data from the specified memory address, returns a byte as each memory address can only store a byte of data
        public byte GetAddress(ushort addr);

        // Sets the data at the specified memory address to the supplied byte
        public void SetAddress(ushort addr, byte data);

        // Sets the size of the memory, maximum is 65535
        public void SetSize(int size);

    }
}
