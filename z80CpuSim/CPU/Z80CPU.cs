using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

using z80CpuSim.CPU.Memory;
using z80CpuSim.CPU.Registers;



namespace z80CpuSim.CPU
{
    public enum FlagBit
    {
        Carry, 
        Subtract, 
        Parity, 
        F3,
        HalfCarry, 
        F5, 
        Zero, 
        Sign
    }
    class Z80CPU : ICPU
    {
        //
        // use singleton approach, make sure there is only ever 1 instance of the cpu class
        private static Z80CPU _Instance;
        public static Z80CPU instance()
        {
            if (_Instance == null)
            {
                System.Diagnostics.Debug.WriteLine("instance");
                _Instance = new Z80CPU(65535, 1);
            }
            return _Instance;
        }
        //
        //


        int frequency;
        public RAM ram;

        // Registers
        // 16 bit
        public ProgramCounter PC = new ProgramCounter();
        public SixteenBitRegister SP = new SixteenBitRegister(); // stack pointer, this is a 16 bit register, EightBitRegister by design is 16 bits
        

        // 8 bit - EightBitRegister is treated as an 9 bit register in these cases
        public EightBitRegister A = new EightBitRegister();
        public EightBitRegister B = new EightBitRegister();
        public EightBitRegister C = new EightBitRegister();
        public EightBitRegister D = new EightBitRegister();
        public EightBitRegister E = new EightBitRegister();
        public EightBitRegister F = new EightBitRegister(); // flags register
        public EightBitRegister H = new EightBitRegister();
        public EightBitRegister L = new EightBitRegister();
        public EightBitRegister I = new EightBitRegister();
        public EightBitRegister R = new EightBitRegister();
        public EightBitRegister IXH = new EightBitRegister();
        public EightBitRegister IXL = new EightBitRegister();
        public EightBitRegister IYH = new EightBitRegister();
        public EightBitRegister IYL = new EightBitRegister();

        // alternate inaccessible registers
        public EightBitRegister _A = new EightBitRegister();
        public EightBitRegister _B = new EightBitRegister();
        public EightBitRegister _C = new EightBitRegister();
        public EightBitRegister _D = new EightBitRegister();
        public EightBitRegister _E = new EightBitRegister();
        public EightBitRegister _F = new EightBitRegister();
        public EightBitRegister _H = new EightBitRegister();
        public EightBitRegister _L = new EightBitRegister();


        // 16 bit registers as 8 bit pairs
        public EightBitRegisterPair AF; 
        public EightBitRegisterPair BC; 
        public EightBitRegisterPair DE; 
        public EightBitRegisterPair HL; 

        // interrupt flip flops
        public bool IFF1 = false;
        public bool IFF2 = false;
        


        // busses (these dont really exist, but they are needed for inspection)
        // they are EightBitRegister types as the EightBitRegister is 16 bits, and so can work as an 8
        // or 16 bit register, obviously in this case, they are not resgiters, but represent the address and
        // data lanes on the CPU

        // These will be cleared (well, set to 0) when they are supposed to hold no value (as shown in the manual, basically when the lines go low), though it is not explicitly required
        // and will not affect operation of the simulated CPU, its just making sure it adheres to the timings and pin states specified in the manual
        public SixteenBitRegister addressBus = new SixteenBitRegister(); // 16 bits
        public EightBitRegister dataBus = new EightBitRegister(); // 8 bits

        public Z80ControlUnit Z80cu;
        

        bool tickInterrupt; // controls if the CPU should pause execution, this is NOT a wait state


        // pins
        bool wait = false;

        //adder
        public BinaryAdder BinAdd;

        
        

        // Constructor, sets up the RAM, and sets the initial frequency (though, this can be changed from the UI, this is just for instantiation)
        protected Z80CPU(int ramSize, int frequency)
        {
            ram = new RAM(ramSize, new byte[0]);
            
            SetSpeed(frequency);

            // initialise stuff i cant do outside here
            AF = new EightBitRegisterPair(A, F);
            BC = new EightBitRegisterPair(B, C);
            DE = new EightBitRegisterPair(D, E);
            HL = new EightBitRegisterPair(H, L);
        }

        // bodge to fix the issue with circular dependancies
        // this must be called AFTER and externally from the constructor
        public void Setup()
        {
            Z80cu = new Z80ControlUnit();
            BinAdd = new BinaryAdder();
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

            // check the wait state, if we need to wait, tick untill the wait pin is low
            while (wait)
            {
                // note that calling Tick() allows for execution to be paused during a wait state
                Tick(); 
            }

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
