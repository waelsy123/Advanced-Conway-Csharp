using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife
{
	public class Grid
	{
        //public int UPS; 
        public Variables V; 
        public Point Size { get;  set; }

		public  Cell[,] cells;
		private bool[,] nextCellStates;

		private TimeSpan updateTimer;

		public Grid()
		{
            V = Program.V; 
            // UPS =Convert.ToInt32( f1.textBox1.Text) ;  

            Size = new Point(V.CellsX, V.CellsY);

            cells = new Cell[Size.X, Size.Y];
			nextCellStates = new bool[Size.X, Size.Y];


                for (int i = 0; i < Size.X; i++)
                {
                    for (int j = 0; j < Size.Y; j++)
                    {
                        cells[i, j] = new Cell(new Point(i, j));
                        nextCellStates[i, j] = false;

                    }
                }


			updateTimer = TimeSpan.Zero;
		}

		public void Clear()
		{
			for (int i = 0; i < Size.X; i++)
				for (int j = 0; j < Size.Y; j++)
					nextCellStates[i, j] = false;

			SetNextState();
		}

        public void UpdateCells(GameTime gameTime){
            			
            MouseState mouseState = Mouse.GetState();

			foreach (Cell cell in cells)
				cell.Update(mouseState);
        }

		public void Update(GameTime gameTime)
		{
            UpdateCells(gameTime);


			updateTimer += gameTime.ElapsedGameTime;

            if (updateTimer.TotalMilliseconds > 1000f / V.UPS)
			{
				updateTimer = TimeSpan.Zero;
                
				    // Loop through every cell on the grid.
				    for (int i = 0; i < Size.X; i++)
				    {
					    for (int j = 0; j < Size.Y; j++)
					    {
						    // Check the cell's current state, count its living neighbors, and apply the rules to set its next state.
						    bool living = cells[i, j].IsAlive;
						    
						    bool result = Program.V.defulatRule ;


                                foreach (daRule r in Program.ruleSet)
                                {
                                    //if (r.cells[12] == true) r.numOfSelectedCells--; 
                                    //bool mid =  r.cells[12] ;
                                    int count = GetLivingNeighbors(i, j, r);
                                    if (r.atmost == 2 && r.numOfSelectedCells <= count ) // atleast
                                    {
                                        result = r.survives;

                                    }
                                    else if (r.atmost == 0 && r.numOfSelectedCells >= count ) // atmost
                                    {
                                        result = r.survives;

                                    }
                                    else if (r.atmost == 1 && r.numOfSelectedCells == count ) // exactly
                                    {
                                        result = r.survives;
                                    }
                                   // if (result==r.survives) break; 
                                }
                            
						    nextCellStates[i, j] = result;
					    }
				    }
                }

				SetNextState();
			}
		

		public int GetLivingNeighbors(int x, int y, daRule r)
		{
			int count = 0;
            int cx = 0, cy =0 ; 
            int i = x - 2;
            int j = y - 2;
            r.cells[12] = false;  
            for (; cy < 5; j++)
            {

                for (; cx < 5; i++)
                {
                    if (r.cells[cx * 5 + cy] && inRange(i, j) && cells[i , j ].IsAlive )
                    {
                        count++; 
                    }
                    cx++;
                    
                }
                i = x - 2;
                cx = 0;
                cy++;
            }

			return count;
		}

        private bool inRange(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < Size.X && j < Size.Y) return true;
            return false;

        }

		public void SetNextState()
		{
			for (int i = 0; i < Size.X; i++)
				for (int j = 0; j < Size.Y; j++)
					cells[i, j].IsAlive = nextCellStates[i, j];

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Cell cell in cells)
				cell.Draw(spriteBatch);

			// Draw vertical gridlines.
			for (int i = 0; i < Size.X; i++)
                spriteBatch.Draw(Game1.Pixel, new Rectangle(i * V.CellSize - 1, 0, 1, Size.Y * V.CellSize), Color.DarkGray);

			// Draw horizontal gridlines.
			for (int j = 0; j < Size.Y; j++)
                spriteBatch.Draw(Game1.Pixel, new Rectangle(0, j * V.CellSize - 1, Size.X * V.CellSize, 1), Color.DarkGray);
		}
	}
}
