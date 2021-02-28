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
using System.Dynamic;


namespace z80CpuSim.UI
{
    /// <summary>
    /// Interaction logic for RAMDisplay.xaml
    /// </summary>
    public partial class RAMDisplay : UserControl
    {
        byte[] data;
        int displayLength = 256;
        int grouping = 1;
        public RAMDisplay()
        {
            InitializeComponent();
            //CreateGrid();
            //Update();
            //UpdateText();
            LineLengthList.Text = Convert.ToString(displayLength);
            ByteGroupingList.Text = Convert.ToString(grouping);
        }

        private void CreateGrid()
        {
            /**
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
            **/
        }
        public void UpdateText()
        {
            int ts = DateTime.Now.Millisecond;
            StringBuilder sb = new StringBuilder();
            data = z80CpuSim.CPU.Z80CPU.instance().ram.GetData();

            // create this line by line.... brilliant
            int actualGroupingLength = (grouping < displayLength ? grouping : displayLength);
            for (int i = 0; i < data.Length; i += displayLength)
            {
                // remember to +1 to i !
                // create the data row
                if (i == 0)
                {
                    sb.Append("ADDRESS     ");
                    for (int j = 0; j < displayLength; j += grouping)
                    {
                        sb.Append(j.ToString("X").PadRight((grouping * 2) + 1));

                    }
                    sb.Append("\n\n");
                }
                // loop for rows
                
                for (int j = 0; j < displayLength; j += actualGroupingLength)
                {
                    if (j == 0)
                    {
                        sb.Append(i.ToString("X4") + "        ");
                    }

                    try
                    {
                        sb.Append(BitConverter.ToString(data[(i + j)..((i + j) + actualGroupingLength)]).Replace("-", "").PadRight((actualGroupingLength * 2) + 1));
                    }
                    catch { }
                    

                }
                sb.Append("\n");
                
                
                
            }
            RamBlock.Text = sb.ToString();
            System.Diagnostics.Debug.WriteLine("Refresh in: " + (System.DateTime.Now.Millisecond - ts));
        }


        private void ByteGroupingList_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                grouping = Convert.ToInt32(ByteGroupingList.Text);
            } catch
            {
                grouping = 1;
                ByteGroupingList.Text = "1";
            }
            
        }

        private void LineLengthList_DropDownClosed(object sender, EventArgs e)
        {
            displayLength = Convert.ToInt32(LineLengthList.Text);
        }

        /**
        private void Update()
        {
            // get the data
            data = z80CpuSim.CPU.Z80CPU.instance().ram.GetData();

            // spilt it into blocks of length specified by the length filter with a byte grouping
            // loop for columns

            // create a datatable

            // create the first column based on the display length
            DataGridTextColumn dgtcLabel = new DataGridTextColumn();
            dgtcLabel.Header = "ADDRESS";
            // create a list for the first column
            List<String> rowHeaders = new List<string>();
            for (int i = 0; i < data.Length; i+= displayLength)
            {
                rowHeaders.Add(i.ToString("X"));
            }
            // set the column binding
            dgtcLabel.Binding = new Binding("rowHeaders");
            RAMGrid.Columns.Add(dgtcLabel);
            RAMGrid.Items.Refresh();
            // prepare the column data
            
            // the column must exist, but we cant set the dataBinding yet
            // create the column headers based on the grouping and display length
            for (int i = 0; i < displayLength; i+=(grouping *2))
            {
                DataGridTextColumn dgtc = new DataGridTextColumn();
                dgtc.Header = i.ToString("X");
                dgtc.Binding = new Binding(i.ToString("X"));
                RAMGrid.Columns.Add(dgtc);
            }
            // ok this is the other way round to i imagined it
            for (int i = 0; i < data.Length; i+= displayLength)
            {
                // remember to +1 to i !
                // create the data row
                dynamic row = new ExpandoObject();
                // loop for rows
                for (int j = 0; j < displayLength; j+=(grouping*2))
                {
                    ((IDictionary<string, object>)row)[j.ToString("X")] = BitConverter.ToString(data[j..(j + grouping)]);
                }
                RAMGrid.Items.Add(row);
            }
            RAMGrid.Items.Refresh();
        }
        **/
    }
}
