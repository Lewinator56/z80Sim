using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Instructions
{
    class Exchange : IInstruction
    {
        Z80CPU Z80 = Z80CPU.instance();
        Dictionary<byte, int> opcodes = new Dictionary<byte, int>
        {
            { 0x08, 1 },

            { 0xD9, 1 },

            { 0xE3, 1 },
            { 0xEB, 1 }
        };
        public bool CanHandle(byte opcode)
        {
            return opcodes.ContainsKey(opcode);
        }

        public void Handle(byte[] data)
        {
            switch (data[0])
            {
                case 0x08:
                    ExchangeAF();
                    break;
                case 0xD9:
                    ExchangeX();
                    break;
                case 0xE3:
                    ExchangeSP();
                    break;
                case 0xEB:
                    ExchangeDE();
                    break;
            }
        }
        public int GetBytesToRead(byte opcode)
        {
            return opcodes.GetValueOrDefault(opcode);
        }

        private void ExchangeSP() // this is very very likely wrong, so i need to test it
        {
            // get the data at (sp)
            byte lower = Z80.Z80cu.ReadMemory(Z80.SP.GetData());
            // get the data at (sp + 1)
            Z80.Tick();
            byte upper = Z80.Z80cu.ReadMemory((ushort)(Z80.SP.GetData() + 1));

            //set the data at memory locations
            Z80.Z80cu.WriteMemory(Z80.SP.GetData(), Z80.L.GetData());
            Z80.Z80cu.WriteMemory((ushort)(Z80.SP.GetData() + 1), Z80.H.GetData());
            Z80.Tick();
            Z80.L.SetData(lower);
            Z80.Tick();
            Z80.H.SetData(upper);
        }
        private void ExchangeAF()
        {
            byte a = Z80.A.GetData();
            byte f = Z80.F.GetData();

            Z80.A.SetData(Z80._A.GetData());
            Z80.F.SetData(Z80._F.GetData());

            Z80._A.SetData(a);
            Z80._F.SetData(f);
        }
        public void ExchangeX()
        {
            byte b = Z80.B.GetData();
            byte c = Z80.C.GetData();

            byte d = Z80.D.GetData();
            byte e = Z80.E.GetData();

            byte h = Z80.H.GetData();
            byte l = Z80.L.GetData();


            Z80.B.SetData(Z80._B.GetData());
            Z80.C.SetData(Z80._C.GetData());

            Z80.D.SetData(Z80._D.GetData());
            Z80.E.SetData(Z80._E.GetData());

            Z80.H.SetData(Z80._H.GetData());
            Z80.L.SetData(Z80._L.GetData());


            Z80._B.SetData(b);
            Z80._C.SetData(c);

            Z80._D.SetData(d);
            Z80._E.SetData(e);

            Z80._H.SetData(h);
            Z80._L.SetData(l);
        }
        private void ExchangeDE()
        {
            ushort hl = Z80.HL.GetData();

            Z80.HL.SetData(Z80.DE.GetData());
            Z80.DE.SetData(hl);
        }
    }
}
