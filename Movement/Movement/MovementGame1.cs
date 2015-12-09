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
using SimpleRectangleCollision;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MovementGame1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D leftWalk, rightWalk, upWalk, downWalk,currentAnim;
        private Rectangle destRectangle; // where the texture will be printed
        private Rectangle sourceRectangle; // which part of the sprite will be printed
        private double elapsed; 
        private double delay = 200f;
        private int frames;
        private KeyboardState ks;
        private Vector2 position = new Vector2();

        private Animation animation;
        private Animation curAnimation;


        public MovementGame1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.destRectangle = new Rectangle(100, 100, 56, 56);
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

            this.leftWalk = Content.Load<Texture2D>("leftWalk");
            this.rightWalk = Content.Load<Texture2D>("rightWalk");
            this.upWalk = Content.Load<Texture2D>("upWalk");
            this.downWalk = Content.Load<Texture2D>("downWalk");

            this.currentAnim = this.rightWalk;

            this.animation = new Animation(Content, "rightWalk", 100f, 4, true);
            IsMouseVisible = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        private void Animate(GameTime gameTime)
        {
            this.elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (this.frames == 3)
                {
                    this.frames = 0;
                }
                else
                {
                    this.frames++;
                }
                this.elapsed = 0;
            }
            this.sourceRectangle = new Rectangle(56 * frames, 0, 56, 56);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
            {
                this.position.X += 2f;
                this.currentAnim = this.rightWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
            {
                this.position.X -= 2f;
                this.currentAnim = this.leftWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
            {
                this.position.Y -= 2f;
                this.currentAnim = this.upWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
            {
                this.position.Y += 2f;
                this.currentAnim = this.downWalk;
                Animate(gameTime);
            }
            else
            {
                this.sourceRectangle=new Rectangle(0,0,56,56);
            }

            this.destRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, 56, 56);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(this.currentAnim, this.destRectangle, this.sourceRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
