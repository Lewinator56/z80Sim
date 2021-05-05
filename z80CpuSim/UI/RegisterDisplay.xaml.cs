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
        List<RegisterModelSource> BusListSource = new List<RegisterModelSource>();
        public RegisterDisplay()
        {
            InitializeComponent();
            DisplayRegistersData();
            DataGrid.ItemsSource = RegisterListSource;
            DataGridBuses.ItemsSource = BusListSource;

        }

        public void DisplayRegistersData()
        {
            // get a list of registers

            // abstracts all the different register types into a class that hides their type from the compiler, no this isnt type safe but i can guarantee the 
            // methods called here wille exist, this is basically a workaround for the inability to have a generic list in c#
            List<RegisterAbstraction> ral = Z80CPU.instance().GetRegisterList();
            List<RegisterAbstraction> bal = Z80CPU.instance().GetBusList();

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

            foreach (RegisterAbstraction bar in bal)
            {
                /**
                Label l = new Label();
                l.Margin = new Thickness(10);
                l.Content = rar.GetRegisterName();

                Label data = new Label();
                data.Margin = new Thickness(10);
                data.Content = rar.GetRegisterData();

                StackPanel sp = new StackPanel();
                DockPanel.SetDock(sp, Dock.Top);
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(l);
                sp.Children.Add(data);

                RegList.Children.Add(sp);
                **/
                RegisterModelSource rms = new RegisterModelSource();
                rms.Type = bar.GetRegisterType();
                rms.Name = bar.GetRegisterName();
                rms.Size = System.Runtime.InteropServices.Marshal.SizeOf(bar.GetRegister().GetData());
                rms.DataIntUnsigned = bar.GetRegister().GetData();
                rms.DataHex = bar.GetRegister().GetData().ToString("X");

                BusListSource.Add(rms);

            }
        }

        private void UpdateRegisterListData()
        {
            RegisterListSource.Clear();
            BusListSource.Clear();
            DisplayRegistersData();



        }

        public void Update()
        {
            UpdateRegisterListData();
            DataGrid.Items.Refresh();
            DataGridBuses.Items.Refresh();
        }
    }
}
