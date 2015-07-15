using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameOfLife
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
        GameTime GM; 
        public Variables V; 


		public static SpriteFont Font;
		public static Texture2D Pixel;

		public static Vector2 ScreenSize;

		private Grid grid;

		private KeyboardState keyboardState, lastKeyboardState;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		public Game1()
		{
            
            
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1.0 / Program.V.FPS);

            ScreenSize = new Vector2(Program.V.CellsX, Program.V.CellsY) * Program.V.CellSize;

			graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
			graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;

			IsMouseVisible = true;
  

        
		}

		protected override void Initialize()
		{
			base.Initialize();
            if (Program.grid == null)
            {
                grid = new Grid();
                Program.grid = grid;
            }
            else grid = Program.grid; 

			keyboardState = lastKeyboardState = Keyboard.GetState();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Font = Content.Load<SpriteFont>("Font");

			Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			Pixel.SetData(new[] { Color.White });
		}

        public void UpdateStep()
        {
            grid.Update(GM);


            //base.Draw(GM);
        }
		protected override void Update(GameTime gameTime)
		{
            GM = gameTime; 

			keyboardState = Keyboard.GetState();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();


			// Clear the screen if backspace is pressed.
			if (keyboardState.IsKeyDown(Keys.Back) && lastKeyboardState.IsKeyUp(Keys.Back))
				grid.Clear();
            
			base.Update(gameTime);

            //grid.Update(gameTime);
            grid.UpdateCells(gameTime); 

			lastKeyboardState = keyboardState;
		}

		protected override void Draw(GameTime gameTime)
		{

                GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            grid.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
		}
	}
}
