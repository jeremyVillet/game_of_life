using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game_Of_Life_Interface.Items;
using System.IO;
using System.Collections.Generic;

namespace Game_Of_Life_Interface
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game_Of_Life : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<List<Cell>> list_cells = new List<List<Cell>>();
        private Game_Logic game = new Game_Logic(SIZE_MAP);

        private Texture2D spriteCellAlive;
        private Texture2D spriteCellDead;

        // Settings
        private const float timeToNextUpdate = 1.0f / 5.0f; // 5 fps
        private float timeSinceLastUpdate;
        private const int SIZE_CELLS = 30;
        private const int SIZE_MAP = 17;
   

    

        public Game_Of_Life()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SIZE_MAP * SIZE_CELLS;
            graphics.PreferredBackBufferHeight = SIZE_MAP * SIZE_CELLS;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for (int i = 0; i < SIZE_MAP; i++)
            {
                list_cells.Add(new List<Cell>());
                for (int j = 0; j < SIZE_MAP; j++)
                {
                    list_cells[i].Add(new Cell());
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            FileStream fileStream = new FileStream("Content/cell_alive.png", FileMode.Open);
            spriteCellAlive = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();

            fileStream = new FileStream("Content/cell_dead.png", FileMode.Open);
            spriteCellDead = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();

            UpdatePrint();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            graphics.PreferredBackBufferWidth = SIZE_MAP * SIZE_CELLS;
            graphics.PreferredBackBufferHeight = SIZE_MAP * SIZE_CELLS;

            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastUpdate >= timeToNextUpdate)
            {
                //update game
                timeSinceLastUpdate = 0;
                game.PassGeneration();
                UpdatePrint();
            }      

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
    
            for (int i = 0; i < SIZE_MAP; i++)
            {
                for (int j = 0; j < SIZE_MAP; j++)
                {
                    list_cells[i][j].Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdatePrint()
        {
            for (int i = 0; i < SIZE_MAP; i++)
            {
                for (int j = 0; j < SIZE_MAP; j++)
                {
                    if (game.Board[i, j] == 1)
                    {
                        list_cells[i][j].Texture = spriteCellAlive;
                    }
                    else
                    {
                        list_cells[i][j].Texture = spriteCellDead;
                    }
                    list_cells[i][j].Position = new Vector2(i * SIZE_CELLS, j * SIZE_CELLS);
                }
            }
        }
    }
}
