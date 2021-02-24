using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;

namespace z80CpuSim.CPU
{
    class RegisterAbstraction
    {
        dynamic RegInterface;
        string Name;

        public RegisterAbstraction(dynamic ifc, string name)
        {
            this.RegInterface = ifc;
            this.Name = name;
        }
        public Type GetRegisterType()
        {
            return RegInterface.GetType();
        }
        public dynamic GetRegister()
        {
            return RegInterface;
        }
        public string GetRegisterData()
        {
            return Convert.ToString(RegInterface.GetData());
        }
        public string GetRegisterName()
        {
            return this.Name;
        }
    }
}
