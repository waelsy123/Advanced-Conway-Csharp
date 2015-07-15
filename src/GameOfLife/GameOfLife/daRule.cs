using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class daRule
    {
        // 0 atmost
        // 1 exactly
        // 2 atleast
        public bool survives = true; 
        public int atmost; 
        public bool[] cells;
        public int numOfSelectedCells;
        public String text;
        public int nightborhoodSize; 

        public daRule()
        {
            atmost = 0;
            cells = new bool[25];
        }
    }
}
