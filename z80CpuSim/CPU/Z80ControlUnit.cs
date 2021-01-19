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
            new Load(),
            new Cp(),
            new And(),
            new Add(),
            new Adc(),
            new Or(), 
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
            // execute the instruction
            Execute(instruction, opcode);

        }

        public void Execute(IInstruction instruction, byte opcode)
        {
            // read the data into an array
            instruction.Handle(new byte[] { opcode }, z80CPU);
        }

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
