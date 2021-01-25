using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim
{
    class IntegerMath
    {
        // ive writtne this because i need a power operator, but that uses integers
        // i DO NOT want to have ANY doubles in this code at all that could screw up the
        // binary values
        public static int Pow(int number, int exponent)
        {
            int accumulator = number;
            for (int i = 0; i < exponent; i++)
            {
                accumulator *= number;
            }

            return accumulator;

        }
    }
}
