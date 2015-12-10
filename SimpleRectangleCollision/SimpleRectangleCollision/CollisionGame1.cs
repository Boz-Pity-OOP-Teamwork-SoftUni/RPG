namespace SimpleRectangleCollision
{
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

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CollisionGame1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D playerBoundsTex, enemyBoundsTex;
        private Rectangle playerBounds, enemyBounds;
        private Vector2 enemyPosition;
        private float speed=10f;
        private float transparency = .3f;

        private Animation animation;
        private Color backgroundColor = Color.Aquamarine;

        public CollisionGame1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here

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

            this.playerBoundsTex = Content.Load<Texture2D>("greenSquare");
            this.enemyBoundsTex = Content.Load<Texture2D>("redSquare");
            this.enemyPosition = new Vector2(1000,100);

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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit(); 

            this.enemyPosition.X -= this.speed;

            this.playerBounds = new Rectangle((int)this.animation.Position.X, (int)this.animation.Position.Y, this.animation.FrameWidth, this.animation.FrameHight);
            this.enemyBounds = new Rectangle((int)this.enemyPosition.X,(int)this.enemyPosition.Y,50,50);

            if (this.enemyPosition.X + this.enemyBoundsTex.Width < 0)
            {
                this.enemyPosition.X += 1000;
            }

            if (this.playerBounds.Intersects(this.enemyBounds))
            {
                this.backgroundColor = Color.PaleVioletRed;
                this.speed = .3f;
                this.animation.FrameTime = 400f;
            }
            else
            {
                this.backgroundColor = Color.Aquamarine;
                this.speed = 10f;
                this.animation.FrameTime = 100f;
            }

            this.animation.PlayAnimation(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.playerBoundsTex, this.playerBounds, Color.White * transparency);
            this.spriteBatch.Draw(this.enemyBoundsTex, this.enemyBounds, Color.White * transparency);
            this.animation.Draw(this.spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
