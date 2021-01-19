using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

using z80CpuSim.CPU.Memory;
using z80CpuSim.CPU.Registers;



namespace z80CpuSim.CPU
{
    class Z80CPU : ICPU
    {
        int frequency;
        public RAM ram;

        // Registers
        // 16 bit
        public ProgramCounter pc = new ProgramCounter();
        

        // 8 bit
        public GenericRegister A = new GenericRegister();
        public GenericRegister B = new GenericRegister();
        public GenericRegister C = new GenericRegister();
        public GenericRegister D = new GenericRegister();
        public GenericRegister E = new GenericRegister();
        public GenericRegister H = new GenericRegister();
        public GenericRegister L = new GenericRegister();
        public GenericRegister I = new GenericRegister();
        public GenericRegister R = new GenericRegister();
        public GenericRegister IXH = new GenericRegister();
        public GenericRegister IXL = new GenericRegister();
        public GenericRegister IYH = new GenericRegister();
        public GenericRegister IYL = new GenericRegister();

        // busses (these dont really exist, but they are needed for inspection)
        // they are GenericRegister types as the GenericRegister is 16 bits, and so can work as an 8
        // or 16 bit register, obviously in this case, they are not resgiters, but represent the address and
        // data lanes on the CPU
        public GenericRegister addressBus = new GenericRegister(); // 16 bits
        public GenericRegister dataBus = new GenericRegister(); // 8 bits

        public Z80ControlUnit z80cu;

        bool tickInterrupt;

        // Constructor, sets up the RAM, and sets the initial frequency (though, this can be changed from the UI, this is just for instantiation)
        public Z80CPU(int ramSize, int frequency)
        {
            ram = new RAM(ramSize, new byte[0]);
            z80cu = new Z80ControlUnit(this);
            SetSpeed(frequency);
        }
        // Not entirely sure if this is the best way of doing this, it will work, im just not sure how well.
        // The problem is, that the thread will block, not given the CPU is running on a separate thread thats fine, it wont block the UI
        // thread, HOWEVER, it will block the CPU thread, which while i dont think is an issue right now, it might be later

        // Called by instructions
        public void Tick()
        {
            while (tickInterrupt)
            {
                // infinite loop on this thread!
            }
            Thread.Sleep(1000 / frequency); // I can guarantee this will cause a problem, ive never got this running at the correct time in the past, but i guess we'll just have top wait and see

        }

        // Sets the speed of the CPU in hertz, I'm not entirely certain the absolute fastest this can run, id assume its 1000hz (or 1MHz)
        public void SetSpeed(int hertz)
        {
            this.frequency = hertz;
        }

        // Methods for setting the tick interrupt, called externally
        public void PauseClock()
        {
            tickInterrupt = true;
        }

        public void ResumeClock()
        {
            tickInterrupt = false;
        }

    }
}
