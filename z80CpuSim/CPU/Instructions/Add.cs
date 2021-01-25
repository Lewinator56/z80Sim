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

            ushort a = i.GetData();
            ushort b = Z80.A.GetData();

            ushort r = (ushort)(a + b);
            if (r > 255)
            {
                Z80.F.SetData((byte)(Z80.F.GetData() | (byte)0x01)); // carry flag
            }
            Z80.A.SetData((byte)r);


        }

        private void AddHLToA()
        {
            //Z80.addressBus.SetData(Z80.HL.GetData());

            //Z80.dataBus.SetData(Z80.ram.GetAddress(Z80.addressBus.GetData()));

            // Changed to utilize the new ReadMemory method

            ushort a = Z80.Z80cu.ReadMemory(Z80.HL.GetData());

            // 3 ticks have elapsed now
            ushort b = Z80.A.GetData();

            ushort r = (ushort)(a + b);
            if (r > 255)
            {
                Z80.F.SetData((byte)(Z80.F.GetData() | (byte)0x01)); // carry flag
            }
            Z80.A.SetData((byte)r);
        }

        private void AddValueToA(byte value)
        {
            ushort b = Z80.A.GetData();
            ushort r = (ushort)(b + value);
            if (r > 255)
            {
                Z80.F.SetData((byte)(Z80.F.GetData() | (byte)0x01)); // carry flag
            }
            Z80.A.SetData((byte)r);
        }

        private void Add16BitTo16Bit(EightBitRegisterPair i, EightBitRegisterPair o)
        {
            // TODO : write this
        }
    }
}
