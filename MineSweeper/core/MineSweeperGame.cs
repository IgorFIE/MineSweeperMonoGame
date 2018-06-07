using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MineSweeper.core;

namespace MineSweeper {
    public class MineSweeper : Game {
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private Texture2D defaultTexture;
        private SpriteFont arialFont;
		private MineBoard mineBoard;

        public MineSweeper() {
            initGraphics();
            Content.RootDirectory = "Content";
        }

        private void initGraphics() {
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = (int)(GameProperties.GAME_SCREEN_X * GameProperties.BLOCK_MARGIN);
			graphics.PreferredBackBufferHeight = (int)(GameProperties.GAME_SCREEN_Y * GameProperties.BLOCK_MARGIN);
            graphics.ApplyChanges();
        }

        protected override void Initialize() {
            initDefaultTexture();
			this.IsMouseVisible = true;
			mineBoard = new MineBoard((GameProperties.GAME_SCREEN_X / 10) / 2, 
			                          (GameProperties.GAME_SCREEN_Y / 10) / 2, 
			                          GameProperties.MINES_NUMBER);
            base.Initialize();
        }

        private void initDefaultTexture() {
            defaultTexture = new Texture2D(GraphicsDevice, 1, 1);
            defaultTexture.SetData(new[] { Color.White });
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = Content.Load<SpriteFont>("Fonts/Arial");
        }

        protected override void Update(GameTime gameTime) {
            handleEscape();
			handleLeftClick();
			handleRightClick();
            base.Update(gameTime);
        }

		private void handleRightClick()
        {
			if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
				Console.WriteLine("RightClick");
            }
        }
		private void handleLeftClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("LeftClick");
            }
        }


        private void handleEscape() {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            drawNewScreen();
            base.Draw(gameTime);
        }

        private void drawNewScreen() {
            spriteBatch.Begin();

			foreach(Block block in mineBoard.blocksBoards){
				if(block.blockType.Equals(BlockType.MINE)){
					spriteBatch.Draw(defaultTexture,block.positionRectangle,Color.Yellow);
					spriteBatch.DrawString(arialFont, "M", new Vector2(block.positionRectangle.X, block.positionRectangle.Y), Color.Black);
					continue;
				}

				if (block.blockType.Equals(BlockType.NORMAL))
                {
                    spriteBatch.Draw(defaultTexture, block.positionRectangle, Color.White);
					if(block.blockValue > 0){
						spriteBatch.DrawString(arialFont, block.blockValue.ToString(), new Vector2(block.positionRectangle.X, block.positionRectangle.Y), Color.Black);
                    }
                    continue;
                }
			}

            spriteBatch.End();
        }
    }
}
