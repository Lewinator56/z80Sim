using System;
using System.Collections.Generic;
using System.Text;

using z80CpuSim.CPU.Registers;

namespace z80CpuSim.CPU.Instructions
{
    class Load : IInstruction
    {

        Z80CPU Z80 = Z80CPU.instance();
        // ive tried to make this look neat and easier to read.... that didnt really work did it
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x01, 3 },
            { 0x02, 1 },
            { 0x06, 2 },
            { 0x0A, 1 },
            { 0x0E, 2 }, 

            { 0x11, 3 },
            { 0x12, 1 },
            { 0x16, 2 },
            { 0x1A, 1 },
            { 0x1E, 2 },

            { 0x21, 3 },
            { 0x22, 3 },
            { 0x26, 2 },
            { 0x2A, 3 },
            { 0x2E, 2 },

            { 0x31, 3 },
            { 0x32, 3 },
            { 0x36, 2 },
            { 0x3A, 3 },
            { 0x3E, 2 },

            { 0x40, 1 },
            { 0x41, 1 },
            { 0x42, 1 },
            { 0x43, 1 },
            { 0x44, 1 },
            { 0x45, 1 },
            { 0x46, 1 },
            { 0x47, 1 },
            { 0x48, 1 },
            { 0x49, 1 },
            { 0x4A, 1 },
            { 0x4B, 1 },
            { 0x4C, 1 },
            { 0x4D, 1 },
            { 0x4E, 1 },
            { 0x4F, 1 },

            { 0x50, 1 },
            { 0x51, 1 },
            { 0x52, 1 },
            { 0x53, 1 },
            { 0x54, 1 }, 
            { 0x55, 1 },
            { 0x56, 1 },
            { 0x57, 1 },
            { 0x58, 1 },
            { 0x59, 1 },
            { 0x5A, 1 },
            { 0x5B, 1 },
            { 0x5C, 1 },
            { 0x5D, 1 },
            { 0x5E, 1 },
            { 0x5F, 1 }, 

            { 0x60, 1 },
            { 0x61, 1 },
            { 0x62, 1 },
            { 0x63, 1 },
            { 0x64, 1 }, 
            { 0x65, 1 },
            { 0x66, 1 },
            { 0x67, 1 },
            { 0x68, 1 },
            { 0x69, 1 },
            { 0x6A, 1 },
            { 0x6B, 1 }, 
            { 0x6C, 1 },
            { 0x6D, 1 },
            { 0x6E, 1 },
            { 0x6F, 1 },
             
            { 0x70, 1 },
            { 0x71, 1 },
            { 0x72, 1 },
            { 0x73, 1 },
            { 0x74, 1 },
            { 0x75, 1 },
            { 0x77, 1 },
            { 0x78, 1 },
            { 0x79, 1 },
            { 0x7A, 1 },
            { 0x7B, 1 },
            { 0x7C, 1 },
            { 0x7D, 1 },
            { 0x7E, 1 },
            { 0x7F, 1 },  // 0x76 is the halt opcode so isn't here

            { 0xF9, 1 }
        };


        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            // These are here to allow for registers AND RAM to be written to, bear in mind however, if accessing IX and IY, these are combinations of IXL, IXH, IYL, ILH
            // and will actually require another specific definition of a EightBitRegister further down. For most cases, this will not be required, and the use of an IMemoryType
            // allows me to instantiate it as a EightBitRegister or as RAM (or ProgramCounter) depending on the inputs and outputs, this will save me some time and code
            IMemoryType O; // single register/address output
            IMemoryType I; // single register/address input

             // im too lazy to go through and replace all the z80's with Z80CPU.instance

            switch (data[0]) {
                case 0x01:
                    LoadValueToR(data[1..3], Z80.BC);
                    break;
                case 0x02:
                    LoadRToAddress(Z80.A, Z80.BC.GetData());
                    break;
                case 0x06:
                    LoadValueToR(data[1], Z80.B);
                    break;
                case 0x0A:
                    LoadDataAtAddressToR(Z80.BC, Z80.A);
                    break;
                case 0x0E:
                    LoadValueToR(data[1], Z80.C);
                    break;

                case 0x11:
                    LoadValueToR(data[1..3], Z80.DE);
                    break;
                case 0x12:
                    LoadRToAddress(Z80.A, Z80.DE.GetData());
                    break;
                case 0x16:
                    LoadValueToR(data[1], Z80.D);
                    break;
                case 0x1A:
                    LoadDataAtAddressToR(Z80.DE, Z80.A);
                    break;
                case 0x1E:
                    LoadValueToR(data[1], Z80.E);
                    break;

                case 0x21:
                    LoadValueToR(data[1..3], Z80.HL);
                    break;
                case 0x22:
                    LoadRToAddress(Z80.HL, BitConverter.ToUInt16(data[1..3]));
                    break;
                case 0x26:
                    LoadValueToR(data[1], Z80.H);
                    break;
                case 0x2A:
                    LoadDataAtAddressToR(data[1..3], Z80.HL);
                    break;
                case 0x2E:
                    LoadValueToR(data[1], Z80.L);
                    break;

                case 0x31:
                    LoadValueToR(data[1..3], Z80.SP);
                    break;
                case 0x32:
                    LoadRToAddress(Z80.A, BitConverter.ToUInt16(data[1..3]));
                    break;
                case 0x36:
                    LoadValueToAddress(data[1], Z80.HL.GetData());
                    break;
                case 0x3A:
                    LoadDataAtAddressToR(data[1..3], Z80.A);
                    break;
                case 0x3E:
                    LoadValueToR(data[1], Z80.A);
                    break;



                case 0x40:
                    // ld b, b
                    LoadRToR(Z80.B, Z80.B);
                    break;
                case 0x41:
                    // ld b, c
                    LoadRToR(Z80.C, Z80.B);
                    break;
                case 0x42:
                    // ld b, d
                    LoadRToR(Z80.D, Z80.B);
                    break;
                case 0x43:
                    // ld b, e
                    LoadRToR(Z80.E, Z80.B);
                    break;
                case 0x44:
                    // ld b, h
                    LoadRToR(Z80.H, Z80.B);
                    break;
                case 0x45:
                    // ld b, l
                    LoadRToR(Z80.L, Z80.B);
                    break;
                case 0x46:
                    // ld b, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.B);
                    break;
                case 0x47:
                    // ld b, a
                    LoadRToR(Z80.A, Z80.B);
                    break;

                case 0x48:
                    // ld c, b
                    LoadRToR(Z80.B, Z80.C);
                    break;
                case 0x49:
                    // ld c, c
                    LoadRToR(Z80.C, Z80.C);
                    break;
                case 0x4A:
                    // ld c, d
                    LoadRToR(Z80.D, Z80.C);
                    break;
                case 0x4B:
                    // ld c, e
                    LoadRToR(Z80.E, Z80.C);
                    break;
                case 0x4C:
                    // ld c, h
                    LoadRToR(Z80.H, Z80.C);
                    break;
                case 0x4D:
                    // ld c, l
                    LoadRToR(Z80.L, Z80.C);
                    break;
                case 0x4E:
                    // ld c, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.C);
                    break;
                case 0x4F:
                    // ld c, a
                    LoadRToR(Z80.A, Z80.C);
                    break;



                case 0x50:
                    // ld d, b
                    LoadRToR(Z80.B, Z80.D);
                    break;
                case 0x51:
                    // ld d, c
                    LoadRToR(Z80.C, Z80.D);
                    break;
                case 0x52:
                    // ld d, d
                    LoadRToR(Z80.D, Z80.D);
                    break;
                case 0x53:
                    // ld d, e
                    LoadRToR(Z80.E, Z80.D);
                    break;
                case 0x54:
                    // ld d, h
                    LoadRToR(Z80.H, Z80.D);
                    break;
                case 0x55:
                    // ld d, l
                    LoadRToR(Z80.L, Z80.D);
                    break;
                case 0x56:
                    // ld d, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.D);
                    break;
                case 0x57:
                    // ld d, a
                    LoadRToR(Z80.A, Z80.D);
                    break;

                case 0x58:
                    // ld e, b
                    LoadRToR(Z80.B, Z80.E);
                    break;
                case 0x59:
                    // ld e, c
                    LoadRToR(Z80.C, Z80.E);
                    break;
                case 0x5A:
                    // ld e, d
                    LoadRToR(Z80.D, Z80.E);
                    break;
                case 0x5B:
                    // ld e, e
                    LoadRToR(Z80.E, Z80.E);
                    break;
                case 0x5C:
                    // ld e, h
                    LoadRToR(Z80.H, Z80.E);
                    break;
                case 0x5D:
                    // ld e, l
                    LoadRToR(Z80.L, Z80.E);
                    break;
                case 0x5E:
                    // ld e, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.E);
                    break;
                case 0x5F:
                    // ld e, a
                    LoadRToR(Z80.A, Z80.E);
                    break;



                case 0x60:
                    // ld h, b
                    LoadRToR(Z80.B, Z80.H);
                    break;
                case 0x61:
                    // ld h, c
                    LoadRToR(Z80.C, Z80.H);
                    break;
                case 0x62:
                    // ld h, d
                    LoadRToR(Z80.D, Z80.H);
                    break;
                case 0x63:
                    // ld h, e
                    LoadRToR(Z80.E, Z80.H);
                    break;
                case 0x64:
                    // ld h, h
                    LoadRToR(Z80.H, Z80.H);
                    break;
                case 0x65:
                    // ld h, l
                    LoadRToR(Z80.L, Z80.H);
                    break;
                case 0x66:
                    // ld h, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.H);
                    break;
                case 0x67:
                    // ld h, a
                    LoadRToR(Z80.A, Z80.H);
                    break;

                case 0x68:
                    // ld l, b
                    LoadRToR(Z80.B, Z80.L);
                    break;
                case 0x69:
                    // ld l, c
                    LoadRToR(Z80.C, Z80.L);
                    break;
                case 0x6A:
                    // ld l, d
                    LoadRToR(Z80.D, Z80.L);
                    break;
                case 0x6B:
                    // ld l, e
                    LoadRToR(Z80.E, Z80.L);
                    break;
                case 0x6C:
                    // ld l, h
                    LoadRToR(Z80.H, Z80.L);
                    break;
                case 0x6D:
                    // ld l, l
                    LoadRToR(Z80.L, Z80.L);
                    break;
                case 0x6E:
                    // ld l, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.L);
                    break;
                case 0x6F:
                    // ld l, a
                    LoadRToR(Z80.A, Z80.L);
                    break;



                case 0x70:
                    // ld (hl), b
                    LoadRToAddress(Z80.B, Z80.HL.GetData());
                    break;
                case 0x71:
                    // ld (hl), c
                    LoadRToAddress(Z80.C, Z80.HL.GetData());
                    break;
                case 0x72:
                    // ld (hl), d
                    LoadRToAddress(Z80.D, Z80.HL.GetData());
                    break;
                case 0x73:
                    // ld (hl), e
                    LoadRToAddress(Z80.E, Z80.HL.GetData());
                    break;
                case 0x74:
                    // ld (hl), h
                    LoadRToAddress(Z80.H, Z80.HL.GetData());
                    break;
                case 0x75:
                    // ld (hl), l
                    LoadRToAddress(Z80.D, Z80.HL.GetData());
                    break;
                case 0x77:
                    // ld (hl), l
                    LoadRToAddress(Z80.A, Z80.HL.GetData());
                    break;

                case 0x78:
                    // ld a, b
                    LoadRToR(Z80.B, Z80.A);
                    break;
                case 0x79:
                    // ld a, c
                    LoadRToR(Z80.C, Z80.A);
                    break;
                case 0x7A:
                    // ld a, d
                    LoadRToR(Z80.D, Z80.A);
                    break;
                case 0x7B:
                    // ld a, e
                    LoadRToR(Z80.E, Z80.A);
                    break;
                case 0x7C:
                    // ld a, h
                    LoadRToR(Z80.H, Z80.A);
                    break;
                case 0x7D:
                    // ld a, l
                    LoadRToR(Z80.L, Z80.A);
                    break;
                case 0x7E:
                    // ld a, (hl)
                    LoadDataAtAddressToR(Z80.HL, Z80.A);
                    break;
                case 0x7F:
                    // ld a, a
                    LoadRToR(Z80.A, Z80.A);
                    break;



                case 0xF9:
                    // ld sp, hl
                    LoadRToR(Z80.HL, Z80.SP);
                    break;



            }
            // do the reading


        }

        private void LoadRToR(EightBitRegister i, EightBitRegister o)
        {
            // TODO : Ticks
            i.SetData(o.GetData());
            
        }
        private void LoadRToR(IRegister<ushort> i, IRegister<ushort> o)
        {
            // TODO : Ticks
            i.SetData(o.GetData());

        }

        private void LoadDataAtAddressToR(EightBitRegisterPair i, EightBitRegister o)
        {
            // TODO : Ticks
            o.SetData(Z80.Z80cu.ReadMemory(i.GetData()));
        }
        private void LoadDataAtAddressToR(byte[] adr, EightBitRegister o )
        {
            o.SetData(Z80.Z80cu.ReadMemory(BitConverter.ToUInt16(adr)));
        }
        private void LoadDataAtAddressToR(byte[] adr, EightBitRegisterPair o)
        {
            o.SetData(Z80.Z80cu.ReadMemory(BitConverter.ToUInt16(adr)));
        }

        // method overriding
        private void LoadValueToR(byte value, EightBitRegister r)
        {
            r.SetData(value);
        }
        private void LoadValueToR(byte[] value, IRegister<ushort> r)
        {
            r.SetData(BitConverter.ToUInt16(value));
        }
        

        private void LoadValueToAddress(byte value, ushort adr)
        {
            Z80.Z80cu.WriteMemory(adr, value);
        }

        // more method overriding
        private void LoadRToAddress(EightBitRegister r, ushort adr)
        {
            Z80.Z80cu.WriteMemory(adr, r.GetData());
        }
        private void LoadRToAddress(EightBitRegisterPair i, ushort adr)
        {
            byte[] data = BitConverter.GetBytes(i.GetData());
            Z80.Z80cu.WriteMemory(adr, data[0]);
            Z80.Z80cu.WriteMemory(adr, data[1]);
        }

        


        
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }


    }
}
