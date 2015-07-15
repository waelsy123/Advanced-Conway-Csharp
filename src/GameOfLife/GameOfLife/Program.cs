using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GameOfLife
{
#if WINDOWS || XBOX
    static class Program
    {
 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        //static bool[,] LastCellStates = null;
        ///// <summary>
        ///// Access routine for global variable.
        ///// </summary>
        //public static bool[,] LastCellStat
        //{
        //    get
        //    {
        //        return LastCellStates;
        //    }
        //    set
        //    {
        //        LastCellStates = value;
        //    }
        //}



        public static Grid grid
        {
            get;
            set;
        }
        public static Variables V
        {    
            get;
            set;
        }
        public static List<daRule> ruleSet
        {
            get;
            set;
        }

        public static Main ff
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            ruleSet = new  List<daRule>() ;
            V = new Variables(); 
           ff = new Main();

           ff.ShowDialog(); 

           
        }
    }
#endif
}

