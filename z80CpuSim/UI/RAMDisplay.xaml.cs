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
    /// Interaction logic for RAMDisplay.xaml
    /// </summary>
    public partial class RAMDisplay : UserControl
    {
        public RAMDisplay()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            for (int i = 0; i < 256; i++)
            {
                
                    RowDefinition rd = new RowDefinition();
                    rd.Height = new GridLength(20);
                    RAMGrid.RowDefinitions.Add(rd);

                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = new GridLength(40);
                    RAMGrid.ColumnDefinitions.Add(cd);
                
            }
            RAMGrid.Children.Clear();
        }

        

        private void UpdateGrid()
        {

        }
    }
}
