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
using System.Threading;
using System.IO;

namespace z80CpuSim.UI
{
    /// <summary>
    /// Interaction logic for MainContainer.xaml
    /// </summary>
    public partial class MainContainer : UserControl
    {
        public MainContainer()
        {
            InitializeComponent();
            Thread t = new Thread(() => UIUpdateThread());
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        public void UIUpdateThread()
        {
            // update the UI, loop untill told otherwise

            while (true)
            {
                Thread.Sleep(1000);
                this.Dispatcher.Invoke(() => { 
                    RegisterDisplayControl.Update();
                    RamDisplayControl.UpdateText();
                });
            }
        }

        private void openCode_Click(object sender, RoutedEventArgs e)
        {
            CpuControlPanel.EnableControlsForNewExecution();
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Filter = "binary file | *.bin";
            Nullable<bool> r = opf.ShowDialog();
            if (r == true)
            {
                LoadBin(opf.FileName);
            }
        }

        private void LoadBin(string path)
        {
            z80CpuSim.CPU.Z80CPU Z80 = z80CpuSim.CPU.Z80CPU.instance();
            byte[] f = File.ReadAllBytes(path);
            for (int i = 0; i < f.Length; i++)
            {
                try
                {
                    Z80.ram.SetAddress((ushort)i, f[i]);
                } catch
                {
                    ErrorPopup ep = new ErrorPopup();
                    ep.errorText.Text = "The binary file is larger than the maximum memory size of 65535 bytes";
                    MaterialDesignThemes.Wpf.DialogHost.Show(ep);
                }
            }
        }
    }
}
