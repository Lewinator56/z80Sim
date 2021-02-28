using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace z80CpuSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Tests.TestAdder ta = new Tests.TestAdder();
            //ta.Test();
            //Tests.TestSub ts = new Tests.TestSub();
            //ts.Test();
            //Tests.TestReset tr = new Tests.TestReset();
            //tr.Test();
            Z80CPU.instance().Setup();
            Z80CPU.instance().ram.SetAddress(10, 10);
            Z80CPU.instance().ram.SetAddress(0, 6);
            Z80CPU.instance().ram.SetAddress(1, 10);
            Z80CPU.instance().ram.SetAddress(2, 128);
            
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MinimiseWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RestoreWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                WindowHeader.Margin = new Thickness(0, 0, 0, 0);
                this.WindowState = WindowState.Normal;
            } else
            {
                WindowHeader.Margin = new Thickness(5, 5, 5, 0);
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
