using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Registers
{

    // This class should provide an abstraction layer for a pseudo 16 bit register
    // 
    // Takes 2 8 bit registers (alhough, all registers are considered 16 bits, its just only the first 8 bits is looked at)
    // and combines them for the set and get operations, implements the IRegister Interface so can be used with ANY 2 registers
    // the most likely use case is when using IX and IY registers, as these are considered IXL and IXH, IYL and IYH.
    
    class _16BitAbstractionRegister : IRegister
    {
        GenericRegister lower;
        GenericRegister upper;

        public _16BitAbstractionRegister(GenericRegister lower, GenericRegister upper)
        {
            this.lower = lower;
            this.upper = upper;
        }

        // I better have got the endiness the correct way round here, i guess i'll find out when i test it, its not too hard to change
        public ushort GetData()
        {
            byte lowerByte = BitConverter.GetBytes(lower.GetData())[1];
            byte upperByte = BitConverter.GetBytes(upper.GetData())[1];
            byte[] toReturn = { lowerByte, upperByte };
            return BitConverter.ToUInt16(toReturn);
        }

        // Same thing for here, endiness might be wrong
        public void SetData(ushort data)
        {
            byte[] bytes = BitConverter.GetBytes(data);
            lower.SetData((UInt16)bytes[0]);
            upper.SetData((UInt16)bytes[1]);
        }

    }
}
