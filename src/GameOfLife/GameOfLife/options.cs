using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class options : Form
    {
        public options()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < Program.V.CellsY; j++)
            {
                for (int i = 0; i <Program.V.CellsX ; i++)
                {

                    Program.grid.cells[i, j].IsAlive = false;

                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.ff.timer1.Enabled = false;
            Program.ff.timer1.Interval = trackBar1.Value *4;

            Program.V.defulatRule = radioButton1.Checked; 
            Program.V.CellsX = Convert.ToInt16(textBox1.Text);
            Program.V.CellsY = Convert.ToInt16(textBox2.Text);
            Program.grid = null; 
            Program.ff.wael();
        }

        private void options_Load(object sender, EventArgs e)
        {
            trackBar1.Value = Program.ff.timer1.Interval / 4 ;
            textBox1.Text = Program.V.CellsX.ToString();
            textBox2.Text = Program.V.CellsY.ToString();
            if (Program.V.defulatRule) radioButton1.Checked = true;
            else radioButton2.Checked = true;  


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
