﻿using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    interface ICPU
    {
        public void SetSpeed(int hertz);

        public void PauseClock();

        public void ResumeClock();

        public void Tick();
    }
}
