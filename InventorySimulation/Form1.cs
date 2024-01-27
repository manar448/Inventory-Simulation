using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace InventorySimulation
{
    public partial class Form1 : Form
    {
        private SimulationSystem obj = new SimulationSystem();
        
        public Form1()
        {
            InitializeComponent();
            obj = new SimulationSystem();
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Demand";
            dataGridView1.Columns[1].Name = "Propability";

            dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Name = "Days";
            dataGridView2.Columns[1].Name = "Propability";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        List<Decimal> prob;
        List<int> demand;
        List<int> time;
        //load data form file
        String data, filename;
        
        //l1 -->demand   l2--->lead
        String[] lst1;  //done
        String[] lst2;
        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                var sr = new StreamReader(filename);
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine();

                    if (data == "") continue;
                    if (data == "OrderUpTo")
                    {
                        data = sr.ReadLine();

                        textBox1.Text = Convert.ToString(data);
                        continue;
                    }
                    if (data == "ReviewPeriod")
                    {
                        data = sr.ReadLine();

                        textBox2.Text = Convert.ToString(data);
                        continue;
                    }
                    if (data == "StartInventoryQuantity")
                    {
                        data = sr.ReadLine();

                        textBox3.Text = Convert.ToString(data);
                        continue;
                    }
                    if (data == "StartLeadDays")
                    {
                        data = sr.ReadLine();

                        textBox4.Text = Convert.ToString(data);
                        continue;
                    }
                    if (data == "StartOrderQuantity")
                    {
                        data = sr.ReadLine();

                        textBox5.Text = Convert.ToString(data);
                        continue;
                    }
                    if (data == "NumberOfDays")
                    {
                        data = sr.ReadLine();

                        textBox6.Text = Convert.ToString(data);
                        continue;
                    }
                    if (data == "DemandDistribution")
                    {
                        int y = 0;
                        while (true)
                        {
                            data = sr.ReadLine();
                            if (data == "") { break; }
                            if (data == null) { break; }
                            lst1 = data.Split(',');

                            dataGridView1.Rows.Add(lst1);
                            y++;
                        }
                        continue;
                    }
                    if (data == "LeadDaysDistribution")
                    {
                        int x = 0;
                        while (true)
                        {
                            data = sr.ReadLine();
                            if (data == "") { break; }
                            if (data == null) { break; }
                            lst2 = data.Split(',');

                            dataGridView2.Rows.Add(lst2);
                            x++;
                        }
                        continue;
                    }

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            prob = new List<Decimal>();
            time = new List<Int32>();
            demand = new List<Int32>();

            obj.OrderUpTo = Int32.Parse(textBox1.Text);
            obj.ReviewPeriod = Int32.Parse(textBox2.Text);
            obj.StartInventoryQuantity = Int32.Parse(textBox3.Text);
            obj.StartLeadDays = Int32.Parse(textBox4.Text);
            obj.StartOrderQuantity = Int32.Parse(textBox5.Text);
            obj.NumberOfDays = Int32.Parse(textBox6.Text);
            for (int x = 0; x < dataGridView1.Rows.Count; x++)
            {
                if (dataGridView1.Rows[x].Cells[0].Value != null)
                {
                    demand.Add(Int32.Parse(dataGridView1.Rows[x].Cells[0].Value.ToString()));
                }
                if (dataGridView1.Rows[x].Cells[1].Value != null)
                {
                    prob.Add(Decimal.Parse(dataGridView1.Rows[x].Cells[1].Value.ToString()));
                }

            }
            obj.Calculate1_CummProbability_RandomDigitAssigmint(demand, prob);
            prob.Clear();
            for (int x = 0; x < dataGridView2.Rows.Count; x++)
            {
                if (dataGridView2.Rows[x].Cells[0].Value != null)
                {
                    time.Add(Int32.Parse(dataGridView2.Rows[x].Cells[0].Value.ToString()));
                }
                if (dataGridView2.Rows[x].Cells[1].Value != null)
                {
                    prob.Add(Decimal.Parse(dataGridView2.Rows[x].Cells[1].Value.ToString()));
                }

            }
            obj.Calculate2_CummProbability_RandomDigitAssigmint(time, prob);
            //obj.system_output();
            Form2 form2 = new Form2(obj);
            form2.Show();
            this.Hide();
            string FileName = "";
            if (filename == "C:\\Users\\me513\\Documents\\sc\\modling\\[Students]_Template\\[Students]_Template\\[Students]_Template\\InventorySimulation\\InventorySimulation\\TestCases\\TestCase1.txt")
            { FileName = Constants.FileNames.TestCase1; }
            else if (filename == "C:\\Users\\me513\\Documents\\sc\\modling\\[Students]_Template\\[Students]_Template\\[Students]_Template\\InventorySimulation\\InventorySimulation\\TestCases\\TestCase2.txt") { FileName = Constants.FileNames.TestCase2; }
            string tmp = TestingManager.Test(obj, FileName);
            MessageBox.Show(tmp);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
