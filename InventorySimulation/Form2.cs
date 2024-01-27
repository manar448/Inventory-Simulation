
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;

namespace InventorySimulation
{
    public partial class Form2 : Form
    {
        //private DataGridView dataGridView1;
        //private Button button1;
        //private Label label1;
        private SimulationSystem simulationSystem;
        public Form2(SimulationSystem obj)
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 12;
            dataGridView1.Columns[0].Name = "Day";
            dataGridView1.Columns[1].Name = "Cycle";
            dataGridView1.Columns[2].Name = "Day within Cycle";
            dataGridView1.Columns[3].Name = "Beginning Inventory";
            dataGridView1.Columns[4].Name = "Random digits for Demand";
            dataGridView1.Columns[5].Name = "Demand";
            dataGridView1.Columns[6].Name = "Ending Inventory";
            dataGridView1.Columns[7].Name = "Shortage Quantity";
            dataGridView1.Columns[8].Name = "Order Quantity";
            dataGridView1.Columns[9].Name = "Random digits for Lead time";
            dataGridView1.Columns[10].Name = "Lead time";
            dataGridView1.Columns[11].Name = "Days until Order arrives";

            simulationSystem = obj;
            simulationSystem.system_output();
            System.Threading.Thread.Sleep(100);
            for (int i = 0; i < simulationSystem.NumberOfDays; i++)
            {
                this.dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = simulationSystem.SimulationTable[i].Day.ToString();
                dataGridView1.Rows[i].Cells[1].Value = simulationSystem.SimulationTable[i].Cycle.ToString();
                dataGridView1.Rows[i].Cells[2].Value = simulationSystem.SimulationTable[i].DayWithinCycle.ToString();
                dataGridView1.Rows[i].Cells[3].Value = simulationSystem.SimulationTable[i].BeginningInventory.ToString();
                dataGridView1.Rows[i].Cells[4].Value = simulationSystem.SimulationTable[i].RandomDemand.ToString();
                dataGridView1.Rows[i].Cells[5].Value = simulationSystem.SimulationTable[i].Demand.ToString();
                dataGridView1.Rows[i].Cells[6].Value = simulationSystem.SimulationTable[i].EndingInventory.ToString();
                dataGridView1.Rows[i].Cells[7].Value = simulationSystem.SimulationTable[i].ShortageQuantity.ToString();
                dataGridView1.Rows[i].Cells[8].Value = simulationSystem.SimulationTable[i].OrderQuantity.ToString();
                dataGridView1.Rows[i].Cells[9].Value = simulationSystem.SimulationTable[i].RandomLeadDays.ToString();
                dataGridView1.Rows[i].Cells[10].Value = simulationSystem.SimulationTable[i].LeadDays.ToString();

            }

        }



        
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(simulationSystem);
            form3.Show();
            this.Hide();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }

        private void Form2_Load_2(object sender, EventArgs e)
        {

        }
    }
}
