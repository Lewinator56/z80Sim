using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;
using z80CpuSim.CPU.Memory;

namespace z80CpuSim.CPU.Instructions
{
    class Add : IInstruction
    {
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x09, 1 },
            { 0x19, 1 },
            { 0x29, 1 },
            { 0x39, 1 },

            { 0x80, 1 },
            { 0x81, 1 },
            { 0x82, 1 },
            { 0x83, 1 },
            { 0x84, 1 },
            { 0x85, 1 },
            { 0x86, 1 },
            { 0x87, 1 },

            { 0xC6, 2 }
        };
        Z80CPU Z80 = Z80CPU.instance;
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0x09:
                    Add16BitTo16Bit(Z80.BC, Z80.HL);
                    break;
                case 0x19:
                    Add16BitTo16Bit(Z80.DE, Z80.HL);
                    break;
                case 0x29:
                    Add16BitTo16Bit(Z80.HL, Z80.HL);
                    break;
                case 0x39:
                    //Add16BitTo16Bit(Z80.SP, Z80.HL); // TODO : fix for true 16 bit registers
                    break;
                case 0x80:
                    AddRToA(Z80.B);
                    break;
                case 0x81:
                    AddRToA(Z80.C);
                    break;
                case 0x82:
                    AddRToA(Z80.D);
                    break;
                case 0x83:
                    AddRToA(Z80.E);
                    break;
                case 0x84:
                    AddRToA(Z80.H);
                    break;
                case 0x85:
                    AddRToA(Z80.L);
                    break;
                case 0x86:
                    AddHLToA();
                    break;
                case 0x87:
                    AddRToA(Z80.A);
                    break;
                case 0xC6:
                    AddValueToA(data[1]);
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        // TODO : tick timings
        private void AddRToA(EightBitRegister i)
        {

            unchecked
            {
                sbyte b = (sbyte)i.GetData();
                sbyte a = (sbyte)Z80.A.GetData();
                short r = (short)(a + b);
                Z80.A.SetData((byte)r);
                SetFlagStates(r);
            }


        }

        private void AddHLToA()
        {
            //Z80.addressBus.SetData(Z80.HL.GetData());

            //Z80.dataBus.SetData(Z80.ram.GetAddress(Z80.addressBus.GetData()));

            // Changed to utilize the new ReadMemory method

            unchecked
            {
                sbyte a = (sbyte)Z80.Z80cu.ReadMemory(Z80.HL.GetData());
                short r = (short)(a + (sbyte)Z80.A.GetData());
                Z80.A.SetData((byte)r);
                SetFlagStates(r);
            }
        }

        private void AddValueToA(byte value)
        {
            unchecked
            {
                sbyte b = (sbyte)value;
                sbyte a = (sbyte)Z80.A.GetData();
                short r = (short)(a + b);
                Z80.A.SetData((byte)r);
                SetFlagStates(r);
            }
        }

        private void Add16BitTo16Bit(EightBitRegisterPair i, EightBitRegisterPair o)
        {
            // TODO : write this
        }

        private void SetFlagStates(short r)
        {
            // Set or reset S, 0x80 is 128, this is the 7th value in the A register, if it is 1 the value is negative and the bit is set
            Z80.Z80cu.SetFlagBit(FlagBit.Sign, (Z80.A.GetData() & 0x80) == 0x80);

            // Set or reset Z, 0x00 is 0, this checks if A is equal to 0 (guess i could have just done A == 0) 
            Z80.Z80cu.SetFlagBit(FlagBit.Zero, (Z80.A.GetData() & 0x00) == 0x00);

            // set H if bit 3 is carried to 4 (check if the value is greater than 0x0f)
            Z80.Z80cu.SetFlagBit(FlagBit.HalfCarry, (Z80.A.GetData() > 0x0F));

            // set P/V if the result overflows, basically, if its smaller than -128, which is 0x80
            Z80.Z80cu.SetFlagBit(FlagBit.Parity, (r > 127 || r < -128));

            // set N
            Z80.Z80cu.SetFlagBit(FlagBit.Subtract, false);
            //set C if the value is < -128 (0x80)
            Z80.Z80cu.SetFlagBit(FlagBit.Carry, (ushort)r > 0xff);
        }
    }
}
