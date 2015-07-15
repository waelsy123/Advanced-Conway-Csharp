using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace GameOfLife
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Thread newThread = new Thread(new ThreadStart(SaveSim));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();


        }

        static void SaveSim()
        {


            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "Conway.con";
            // set filters - this can be done in properties as well
            savefile.Filter = "Conway files (*.con)|*.con|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(savefile.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                
                int x = Program.grid.Size.X;
                int y = Program.grid.Size.Y;
                int n = x;
                sw.Write(x);
                sw.Write(' ');
                sw.Write(y);
                sw.WriteLine(" ");
                for (int j = 0; j < y; j++)
                {
                    for (int i = 0; i < x; i++)
                    {
                        Cell cell = Program.grid.cells[i, j]; 
                        if (0 == n) { sw.WriteLine(" "); n = x; }

                        sw.Write(cell.IsAlive ? "1 " : "0 ");
                        n--;
                    }
                }
                sw.Close();
                fs.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(LoadSim));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();


        }
        static void LoadSim()
        {


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Set filter options and filter index.
            openFileDialog1.Filter = "Conway Files (.con)|*.con|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;


            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK )
            {

                    String line;
                    string[] parts;
                    // Open the selected file to read.
                    FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

                    System.IO.StreamReader reader = new System.IO.StreamReader(fs);
                               
                try
                {



                    line = reader.ReadLine();
                    parts = line.Split(' ');
                    int x = Convert.ToInt16(parts[0]);

                    //MessageBox.Show(x.ToString());
                    int y = Convert.ToInt16(parts[1]);
                    //MessageBox.Show(y.ToString());
                    Program.grid.Size = new Microsoft.Xna.Framework.Point(x, y);
                    for (int j = 0; j < y; j++)
                    {
                        line = reader.ReadLine();
                        parts = line.Split(' ');
                        for (int i = 0; i < x; i++)
                        {
                            //MessageBox.Show(parts[i]);
                            bool t = ((parts[i] == "0") ? false : true);
                            Program.grid.cells[i, j].IsAlive = t;

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Please select a valid file");
                }
       
                reader.Close();
                fs.Close();     

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(SaveRule));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        static void SaveRule()
        {


            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "Conway.rcon";
            // set filters - this can be done in properties as well
            savefile.Filter = "RConway files (*.rcon)|*.rcon|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(savefile.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(Program.ruleSet.Count.ToString());
                foreach (daRule r in Program.ruleSet)
                {
                    sw.WriteLine(r.atmost.ToString());
                    sw.WriteLine(r.survives.ToString());
                    sw.WriteLine(r.numOfSelectedCells.ToString());
                    sw.WriteLine(r.text.ToString());
                    sw.WriteLine(r.nightborhoodSize.ToString());
                    for (int i = 0; i < 25; i++)
                    {
                        sw.Write(r.cells[i].ToString());
                        sw.Write(' ');
                    }
                    sw.WriteLine(" ");

                }

                sw.Close();
                fs.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(LoadRule));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        static void LoadRule()
        {


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Set filter options and filter index.
            openFileDialog1.Filter = "Conway Files (.rcon)|*.rcon|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;


            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // First things first, delete all rules.
                Program.ruleSet = new List<daRule>(); 

                String line;
                string[] parts;
                // Open the selected file to read.
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

                System.IO.StreamReader reader = new System.IO.StreamReader(fs);

                try
                {

                    line = reader.ReadLine();
                    int numOfRules = Convert.ToInt16(line);

                    for (int i = 0; i < numOfRules; i++)
                    {
                        daRule r = new daRule();
                        line = reader.ReadLine();
                        r.atmost = Convert.ToInt16(line);

                        line = reader.ReadLine();
                        r.survives = Convert.ToBoolean(line);

                        line = reader.ReadLine();
                        r.numOfSelectedCells = Convert.ToInt16(line);

                        line = reader.ReadLine();
                        r.text = line;

                        line = reader.ReadLine();
                        r.nightborhoodSize = Convert.ToInt16(line);

                        line = reader.ReadLine();
                        parts = line.Split(' ');
                        for (int j = 0; j < 25; j++)
                        {
                            r.cells[j] = (parts[j] == "False" ? false : true);
                        }
                        Program.ruleSet.Add(r);
                    }
                }
                catch {
                    MessageBox.Show("Please select a valid file!");
                }
                reader.Close();
                fs.Close();

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            options o = new options();
            // this.Hide();
            o.ShowDialog(); 
        }
    }
}
