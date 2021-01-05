using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    interface IControlUnit
    {
        // Set the speed of the CPU (should this be in the control unit interface? i guess so, but im not sure)
        public void SetSpeed(int hertz);

        // Begin program execution
        public void StartExecution();
    }
}
