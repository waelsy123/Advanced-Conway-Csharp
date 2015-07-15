using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GameOfLife
{
     
    public partial class Main : Form
    {
        public Variables V;
        public Game1 game;
        Thread theThread ;

        public Main()
        {
            theThread = null;
            wael();

            V = new Variables(); 

            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.V.CellSize--; 
            wael(); 
        }
        public void wael()
        {

            if (theThread == null)
            {

                theThread = new Thread(StartGame);
                theThread.Start();
            }
            else
            {
                theThread.Abort();
                System.Threading.Thread.Sleep(1000);
                theThread = new Thread(StartGame);
                theThread.Start();
            }
        }
        public void StartGame()
        { 
            game = new Game1();
            game.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.V.CellSize++; 
            wael(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StartPause();
 
        }

        public void StartPause()
        {
            if (!timer1.Enabled)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Menu m = new Menu();
            //this.Hide();
            m.ShowDialog(); 

        }

        private void button6_Click(object sender, EventArgs e)
        {
            game.UpdateStep(); 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int steps = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < steps; i++)
                game.UpdateStep();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rules r = new rules();
           // this.Hide();
            r.ShowDialog(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.UpdateStep(); 
        }
    }
}
