using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public  class Variables
    {

        public  int UPS ;     
        public  int FPS ;

        public  int CellSize; 
        public  int CellsX ;
        public  int CellsY;
        public bool defulatRule = false ; 
        
        public Variables()
        {
            UPS = 60;        
            FPS = 60;
            CellSize = 30;   
            CellsX = 40;
            CellsY =15;
        }

    }
}
