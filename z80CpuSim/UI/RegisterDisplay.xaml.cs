using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using z80CpuSim.CPU;

namespace z80CpuSim.UI
{
    /// <summary>
    /// Interaction logic for RegisterDisplay.xaml
    /// </summary>
    public partial class RegisterDisplay : UserControl
    {
        List<RegisterModelSource> RegisterListSource = new List<RegisterModelSource>();
        public RegisterDisplay()
        {
            InitializeComponent();
            DisplayRegistersData();
            DataGrid.ItemsSource = RegisterListSource;

        }

        public void DisplayRegistersData()
        {
            // get a list of registers

            // abstracts all the different register types into a class that hides their type from the compiler, no this isnt type safe but i can guarantee the 
            // methods called here wille exist, this is basically a workaround for the inability to have a generic list in c#
            List<RegisterAbstraction> ral = Z80CPU.instance().GetRegisterList();

            foreach (RegisterAbstraction rar in ral)
            {
                RegisterModelSource rms = new RegisterModelSource();
                rms.Type = rar.GetRegisterType();
                rms.Name = rar.GetRegisterName();
                rms.Size = System.Runtime.InteropServices.Marshal.SizeOf(rar.GetRegister().GetData());
                rms.DataIntUnsigned = rar.GetRegister().GetData();
                rms.DataHex = rar.GetRegister().GetData().ToString("X");

                RegisterListSource.Add(rms);

            }
        }

        private void UpdateRegisterListData()
        {
            RegisterListSource.Clear();
            DisplayRegistersData();
        }

        public void Update()
        {
            UpdateRegisterListData();
            DataGrid.Items.Refresh();
        }
    }
}
