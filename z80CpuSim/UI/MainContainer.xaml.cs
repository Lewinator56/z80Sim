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
        }
    }
}
