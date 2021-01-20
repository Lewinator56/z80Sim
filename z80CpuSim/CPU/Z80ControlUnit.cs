using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Instructions;

namespace z80CpuSim.CPU
{
    class Z80ControlUnit : IControlUnit
    {
        Z80CPU z80CPU;

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
        public Z80ControlUnit(Z80CPU cpu)
        {
            this.z80CPU = cpu;
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
            z80CPU.Tick();
            // get the memory address from the program counter and set the addsress bus to this 
            z80CPU.addressBus.SetData(z80CPU.pc.GetData());
            z80CPU.Tick();
            // get the opcode from RAM that the address bus points to
            z80CPU.dataBus.SetData(z80CPU.ram.GetAddress(z80CPU.addressBus.GetData()));
            // tick before decoding akes place
            z80CPU.Tick();
            // decode the instruction
            byte opcode = Convert.ToByte(z80CPU.dataBus.GetData());
            IInstruction instruction = Decode(opcode);
            z80CPU.Tick();

            // Clear the data and address busses (this actually works for the refersh address anyway for the address bus)
            z80CPU.addressBus.SetData(0);
            z80CPU.dataBus.SetData(0);

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
                z80CPU.Tick();
                z80CPU.pc.Increment();

                z80CPU.Tick();
                z80CPU.addressBus.SetData(z80CPU.pc.GetData());

                z80CPU.Tick();
                z80CPU.dataBus.SetData(z80CPU.ram.GetAddress(z80CPU.addressBus.GetData()));

                byte data = Convert.ToByte(z80CPU.dataBus.GetData());
                instructionData.Add(data);

                // Clear the data and address busses
                z80CPU.addressBus.SetData(0);
                z80CPU.dataBus.SetData(0);

            }
            // convert the list into an array to pass to IInstruction.Handle()
            // this array if formated like so:
            // { opcode , extra byte 1, extra byte 2 }
            instruction.Handle(instructionData.ToArray(), z80CPU);
            
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
    }
}
