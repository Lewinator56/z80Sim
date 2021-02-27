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

namespace z80CpuSim.UI
{
    /// <summary>
    /// Interaction logic for CPUControl.xaml
    /// </summary>
    public partial class CPUControl : UserControl
    {
        public CPUControl()
        {
            InitializeComponent();
        }

        private void playExecution_Click(object sender, RoutedEventArgs e)
        {
            z80CpuSim.CPU.Z80CPU.instance().Z80cu.StartExecution();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            z80CpuSim.CPU.Z80CPU.instance().SetSpeed(Math.Pow(((Slider)sender).Value, 2));
            CpuSpeedHzLabel.Content = z80CpuSim.CPU.Z80CPU.instance().frequency.ToString() + "Hz";
        }
    }
}
