using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;
using z80CpuSim.CPU;

namespace z80CpuSim.CPU
{
    class Z80ControlUnit : IControlUnit
    {
        Z80CPU Z80;
        // list of the possible instructions
        IInstruction[] instructions = 
        {
            new Adc(),
            new And(),
            new Add(),
            new Call(),
            new Cp(),
            new Dec(),
            new Di(),
            new Djnz(),
            new Exchange(),
            new Inc(),
            new Jp(),
            new Jr(),
            new Load(),
            new MiscInstructions(), // this contains msicallanious instructions that are mostly unreleated, but that dont need their own instruction classes as its unnecessary work
            new Or(),
            new Out(),
            new Pop(),
            new Push(),
            new Ret(),
            new Rst(),
            new Sbc(),
            new Sub(),
            new Xor()
        };
        


        // Start program execution
        public Z80ControlUnit()
        {
            this.Z80 = Z80CPU.instance;
        }
        public void StartExecution()
        {

        }

        // The reason im using, what look like fairly convoluted methods for reading and writing data to the CPU, by sampling and setting busses
        // is because i'd like to have an easily inspectable pin state. If for example, the user pauses execution, I treat this as timebeing stopped, and hence,
        // the state of busses should remain consistent. Additionally, once a bus is set to a state, it remains that state unless set otherwise, as shown in the 
        // manual.

        // The other option is just using variables to hold the states, which certainly makes the code look clearer, however, does not achieve the outcome i would
        // like; giving me the ability to sample the busses with a constant access point throughout execution
        public void Fetch()
        {
            Z80.Tick();
            // get the memory address from the program counter and set the addsress bus to this 
            Z80.addressBus.SetData(Z80.PC.GetData());
            Z80.Tick();
            // get the opcode from RAM that the address bus points to
            Z80.dataBus.SetData(Z80.ram.GetAddress(Z80.addressBus.GetData()));
            // tick before decoding akes place
            Z80.Tick();
            // decode the instruction
            byte opcode = Convert.ToByte(Z80.dataBus.GetData());
            IInstruction instruction = Decode(opcode);
            Z80.Tick();

            // Clear the data and address busses (this actually works for the refersh address anyway for the address bus)
            Z80.addressBus.SetData(0);
            Z80.dataBus.SetData(0);

            // execute the instruction
            Execute(instruction, opcode);

        }

        public void Execute(IInstruction instruction, byte opcode)
        {
            // get the number of bytes to read
            int iLength = instruction.GetBytesToRead(opcode);
            // create the list to hold the extended instruction size, this will always contain at least the primary opcode
            List<byte> instructionData = new List<byte>();
            instructionData.Add(opcode); // Make sure the list contains the primary opcode
            for (int i = 0; i < iLength; i++)
            {
                // i dont exactly know where the data is read to, its not documented, so its just going to populate an array passed into the instruction.Handle method, timings here will be correct though
                // increment PC, then a memory read, its basically the opcode fetch

                // Actually, it is documented, i just couldnt find it, the W and Z registers are used for the extra bytes for 2 or 3 byte instructions, this byte array
                // being built represents the W and Z registers. They are not accessible to the user and ae temporary storage so there is no reason to define explicit
                // W and Z registers as part of the CPU.


                // TODO : Determine if my ticks are correct, it would seem that they are however
                Z80.Tick();
                Z80.PC.Increment();

                Z80.Tick();
                Z80.addressBus.SetData(Z80.PC.GetData());

                Z80.Tick();
                Z80.dataBus.SetData(Z80.ram.GetAddress(Z80.addressBus.GetData()));

                Z80.Tick();
                byte data = Convert.ToByte(Z80.dataBus.GetData());
                instructionData.Add(data);

                // Clear the data and address busses
                Z80.addressBus.SetData(0);
                Z80.dataBus.SetData(0);

            }
            // convert the list into an array to pass to IInstruction.Handle()
            // this array if formated like so:
            // { opcode , extra byte 1, extra byte 2 }
            instruction.Handle(instructionData.ToArray());
            
        }

        // This simply checks if the instruction can be handled by looping through all of the possible instructions, as defined in the array above
        public IInstruction Decode(byte opcode)
        {
            foreach (IInstruction instruction in instructions)
            {
                if (instruction.CanHandle(opcode))
                {
                    return instruction;
                }
            }
            return null;
        }

        // Read data from memory
        // The address bus is set to the address to read, 2 ticks elapse before the memory responds, 
        // The data is then read, and will be visible for inspection on the 3rd tick
        public byte ReadMemory(UInt16 address)
        {
            Z80.addressBus.SetData(address);
            Z80.Tick();
            Z80.Tick();

            Z80.dataBus.SetData(Z80.ram.GetAddress(Z80.addressBus.GetData()));
            Z80.Tick();

            return (byte)Z80.dataBus.GetData();
        }

        // Write data to memory
        // the address bus is placed on the address to write, a single tick occurs before the data
        // is sent, at which point im setting the data bus, another tick occurs before its actually written to the
        // RAM, the data is visible in RAM after the 3rd tick
        public void WriteMemory(UInt16 address, byte data)
        {
            Z80.addressBus.SetData(address);
            Z80.Tick();

            Z80.dataBus.SetData(data);
            Z80.Tick();

            Z80.ram.SetAddress(Z80.addressBus.GetData(), (byte)Z80.dataBus.GetData());
            Z80.Tick();

        }

        // flag setting
        // This is here because its easier, clearer to read AND better practice (because im not typing the same 
        // code over and over again) to set flags with this.
        // The enumeration expects a flag bit identifier, these can be found in the Z80CPU file, but not in the 
        // class,.
        public void SetFlagBit(FlagBit bit, bool set)
        {
            
            if (set)
            {
                // Set a bit to 1, this ORs the current state of the register with the correct
                // integer value calculated by 2 ^ bit flag value
                // these values will be numbers represented by 1 at the index of the bit flag
                Z80.F.SetData((UInt16)(Z80.F.GetData() | 2 ^ (UInt16)bit));
            } else
            {
                // Reset the flag bit, uses a bit shift by the number of the bit index in the 
                // flag register, which is the enum value, to specify the number of times to shift the bit
                UInt16 n = Z80.F.GetData();
                n = (UInt16)(n & ~(1U << (UInt16)bit));
                Z80.F.SetData(n);
            }
        }
    }
}
