﻿using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU
{
    interface IInstruction
    {
        public bool CanHandle(byte opcode);

        public void Handle(byte[] data, ICPU CPU);

        public int GetBytesToRead(byte opcode);
    }
}
