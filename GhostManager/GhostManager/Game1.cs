using Ghost;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using System.Collections.Generic;

namespace GhostManager
{ 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Spawner ghostSpawner;

     
        Sprite pacMouse;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
  
            ghostSpawner = new GhostSpawner(this);

            this.Components.Add(ghostSpawner);

            pacMouse = new DrawableSprite(this);
            this.Components.Add(pacMouse);
        }

        private void QueueTest()
        {
            //create Queue of Ghosts
            MonogameGhost redGhost = new MonogameGhost(this);
            MonogameGhost purpleGhost = new MonogameGhost(this);
            purpleGhost.strGhostTexture = "PurpleGhost";
            MonogameGhost tealGhost = new MonogameGhost(this);
            tealGhost.strGhostTexture = "TealGhost";

            Queue<GameComponent> ghostQueue = new Queue<GameComponent>();
            ghostQueue.Enqueue(redGhost);
            ghostQueue.Enqueue(purpleGhost);
            ghostQueue.Enqueue(tealGhost);

            List<GameComponent> ghostList = new List<GameComponent>();
            ghostList.Add(redGhost);
            ghostList.Add(purpleGhost);
            ghostList.Add(tealGhost);

            System.Console.WriteLine(redGhost is GameComponent);
        }

       
        protected override void Initialize()
        {

            pacMouse.spriteTexture = Content.Load<Texture2D>("TealGhost");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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

            pacMouse.Location = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            foreach (GameComponent gc in Components)
            {
                if (gc is MonogameGhost)
                {
                    if (((MonogameGhost)gc).Intersects(pacMouse))
                    {
                        gc.Enabled = false;

                    }
                }
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



            base.Draw(gameTime);
        }
    }
}
