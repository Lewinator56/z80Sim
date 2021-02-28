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
    /// Interaction logic for CPUControl.xaml
    /// </summary>
    public partial class CPUControl : UserControl
    {
        bool isPaused;
        Thread t;
        public CPUControl()
        {
            InitializeComponent();
        }
        public void EnableControlsForNewExecution()
        {
            playExecution.IsEnabled = true;
            pauseExecution.IsEnabled = false;
            resetExecution.IsEnabled = false;
        }

        private void playExecution_Click(object sender, RoutedEventArgs e)
        {
            t = new Thread(() => z80CpuSim.CPU.Z80CPU.instance().Z80cu.StartExecution());
            t.Start();

            
            playExecution.IsEnabled = false;
            pauseExecution.IsEnabled = true;
            resetExecution.IsEnabled = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            z80CpuSim.CPU.Z80CPU.instance().SetSpeed(((Slider)sender).Value);
            CPUSpeedTxBx.Text = z80CpuSim.CPU.Z80CPU.instance().frequency.ToString("0.##");
        }

        private void pauseExecution_Click(object sender, RoutedEventArgs e)
        {
            if (!isPaused)
            {
                z80CpuSim.CPU.Z80CPU.instance().PauseClock();
                PauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                
            } else
            {
                z80CpuSim.CPU.Z80CPU.instance().ResumeClock();
                PauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
            }
            isPaused = !isPaused;
            
        }

        private void CPUSpeedTxBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void CPUSpeedTxBx_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                SpeedSlider.Value = Convert.ToDouble(CPUSpeedTxBx.Text);
            }
            catch { }
        }

        private void CPUSpeedTxBx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    SpeedSlider.Value = Convert.ToDouble(CPUSpeedTxBx.Text);
                }
                catch { }
            }
        }

        private void resetExecution_Click(object sender, RoutedEventArgs e)
        {
            // stopping the execution still allows the current clock cycle to take place, i know this is inconvinient, but there isnt much i can do about it
            EnableControlsForNewExecution();
            PauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
            isPaused = false;
            z80CpuSim.CPU.Z80CPU.instance().ResumeClock();
            z80CpuSim.CPU.Z80CPU.instance().PC.SetData(0);
            z80CpuSim.CPU.Z80CPU.instance().Z80cu.StopExecution();
        }
    }
}
