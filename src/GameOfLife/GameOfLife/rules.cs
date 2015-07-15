using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GameOfLife
{
    public partial class rules : Form
    {
        daRule temp= null ;
        int tempIdx = -1; 
        Label[ , ] L ;
        bool[] subrule = new bool [25];
        int Atmost = 0;
 
        public rules()
        {
            L = new Label[5 , 5];
    
            InitializeComponent();
            
            
            //int i =1 ;
            for(int i = 0 ;i< 5;i++)
                for(int j = 0 ;j<5;j++)
                {
                    L[i, j] = new Label();
                    L[i, j].BackColor = Color.White;
                    L[i, j].Name = i.ToString() + j.ToString();
                    L[i, j].Location = new Point(j * 31 + 30 , i * 31 + 30);
                    L[i, j].Size = new Size(30,30);
                    L[i, j].Click += new EventHandler((sender, e) => changeCellStatus(sender, e, i, j));
                    this.Controls.Add(L[i, j]);

                }

            L[2, 2].BackColor = Color.Black; 




        }

        private object changeCellStatus(object sender, EventArgs e, int i, int j)
        {
            Label s = sender as Label;
            
            int x = Convert.ToInt16( s.Name[0].ToString());
            int y = Convert.ToInt16(s.Name[1].ToString());
            if (x == 2 && y == 2) return sender;
            int idx = y*5+x ;
            //if (idx == 12)
            //{
            //    if (!subrule[idx])
            //    { // if false, white 
            //        L[x, y].BackColor = Color.Black;
            //        subrule[y * 5 + x] = true;
            //    }
            //    else
            //    {
            //        L[x, y].BackColor = Color.White;
            //        subrule[y * 5 + x] = false;
            //    }
            //}
            if(!subrule[idx]){ // if false, white 
                L[x,y].BackColor = Color.Yellow ;
                subrule[y*5+x] = true ; 
            }
            else {
                L[x,y].BackColor = Color.White ;
                subrule[y * 5 + x] = false; 
            }

            return sender;

        }


      

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CellsInCor(false);
            L[1, 1].Visible = false;
            L[1, 3].Visible = false;
            L[3, 1].Visible = false;
            L[3, 3].Visible = false;
            resetCell(1, 1);
            resetCell(1, 3);
            resetCell(3, 1);
            resetCell(3, 3);

        }

        void resetCell(int i, int j) {
            subrule[i * 5 + j] = false;
            L[i, j].BackColor = Color.White;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CellsInCor(false );
        }

        private void CellsInCor(bool vis)
        {
            L[1, 1].Visible = true;
            L[1, 3].Visible = true;
            L[3, 1].Visible = true;
            L[3, 3].Visible = true;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0 || j == 0 || i == 4 || j == 4)
                    {
                        
                        L[i, j].Visible = vis;
                        if (!L[i, j].Visible)
                        {
                            subrule[j * 5  + i] = false;
                            L[i, j].BackColor = Color.White;
                        }

                    }
                }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CellsInCor(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (Atmost==0)
            {
                Atmost = 1;
                b.Text = "Exactly";
            }
            else if (Atmost == 1)
            {
                Atmost = 2;
                b.Text = "Atleast";
            }
            else
            {
                Atmost = 0;
                b.Text = "Atmost";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
   
            int totalOnes = 0 ; 
            for(int i = 0 ;i<25;i++)
                totalOnes += (subrule[i]?1:0)   ;
            string input = textBox1.Text.ToString(); 

            bool isNumeric = Regex.IsMatch(input, @"^\d+$");
            if (!isNumeric) {
                MessageBox.Show("Enter a valid number for number of cells!");
                return;
            }
            if (totalOnes < Convert.ToInt16(textBox1.Text))
            {
                MessageBox.Show("Rules is not valid, Select more cells!");
                return;

            }
            String text = button1.Text + " " + textBox1.Text + " on " + totalOnes + " cells." ;
            daRule r = new daRule();
            r.atmost = Atmost;
            r.cells = subrule;
            r.text = text;
            r.survives = radioButton4.Checked;
            r.numOfSelectedCells = Convert.ToInt16(textBox1.Text);
            r.nightborhoodSize = radioButton1.Checked ? 4 : radioButton2.Checked?8:24;
            Program.ruleSet.Add(r); // insert in the global array of rules in progrma.cs
            listBox1.Items.Add(text);
            clearWindow();

        }

        private void clearWindow()
        {

            subrule = new Boolean[25];

            //int i =1 ;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    L[i, j].BackColor = Color.White;
                }

            L[2, 2].BackColor = Color.Black;
            //radioButton3.Checked = true;
            //radioButton5.Checked = false; 
        }

        private void rules_Load(object sender, EventArgs e)
        {

            foreach (daRule r in Program.ruleSet)
            {
                listBox1.Items.Add(r.text);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           int index = this.listBox1.IndexFromPoint(e.Location);
           tempIdx = index;
           if (index != System.Windows.Forms.ListBox.NoMatches)
           {
               temp = Program.ruleSet[index];
               subrule = temp.cells;
               textBox1.Text = temp.numOfSelectedCells.ToString();
               Atmost = temp.atmost;

               if (temp.survives) radioButton4.Checked = true;
               else radioButton5.Checked = true;

               if (temp.atmost == 2 ) // atleast
               {
                   button1.Text = "Atleast";

               }
               else if (temp.atmost == 0 ) // atmost
               {
                   button1.Text = "Atmost";

               }
               else if (temp.atmost == 1 ) // exactly
               {
                   button1.Text = "Exactly";
               }

              
               if(temp.nightborhoodSize == 4)  
                   radioButton1.Checked = true;
               else if (temp.nightborhoodSize == 8) radioButton2.Checked = true;
               else 
                   radioButton3.Checked = true;

               for (int i = 0; i < 5; i++)
                   for (int j = 0; j < 5; j++)
                       if (subrule[j * 5 + i]) L[i, j].BackColor = Color.Yellow;
                       else L[i, j].BackColor = Color.White;
               L[2, 2].BackColor = Color.Black;
           }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count !=0  )
            {
                Program.ruleSet.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            tempIdx = index; 
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                temp = Program.ruleSet[index];
                subrule = temp.cells;
                textBox1.Text = temp.numOfSelectedCells.ToString();
                Atmost = temp.atmost;
                if (temp.atmost == 2) // atleast
                {
                    button1.Text = "Atleast";

                }
                else if (temp.atmost == 0) // atmost
                {
                    button1.Text = "Atmost";

                }
                else if (temp.atmost == 1) // exactly
                {
                    button1.Text = "Exactly";
                }
                if (temp.survives) radioButton4.Checked = true;
                else radioButton5.Checked = true;

                if (temp.nightborhoodSize == 4)
                    radioButton1.Checked = true;
                else if (temp.nightborhoodSize == 8) radioButton2.Checked = true;
                else
                    radioButton3.Checked = true;

                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (subrule[j * 5 + i]) L[i, j].BackColor = Color.Yellow;
                        else L[i, j].BackColor = Color.White;
              L[2, 2].BackColor = Color.Black;
               
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tempIdx>-1)
            { // add 
                int totalOnes = 0;
                for (int i = 0; i < 25; i++)
                    totalOnes += (subrule[i] ? 1 : 0);
                String text = button1.Text + " " + textBox1.Text + " on " + totalOnes + " cells.";
                daRule r = new daRule();
                r.atmost = Atmost;
                r.cells = subrule;
                r.text = text;
                r.survives = radioButton4.Checked;
                r.numOfSelectedCells = Convert.ToInt16(textBox1.Text);
                r.nightborhoodSize = radioButton1.Checked ? 4 : radioButton2.Checked ? 8 : 24;
                Program.ruleSet.Add(r); // insert in the global array of rules in progrma.cs
                listBox1.Items.Add(text);

                // remove 
                Program.ruleSet.RemoveAt(tempIdx);
                listBox1.Items.RemoveAt(tempIdx);

                clearWindow();
            }
            tempIdx = -1; 
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
