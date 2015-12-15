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

namespace Level1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Level1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Monster mMonster;
        private Monster mMonster2;
        private Monster mMonster3;
        private SpriteFont font;
        private int score = 0;
        private int health = 0;
        private int level = 1;
        private double attack = 0;
        private double defense = 0;

        public Level1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 900;
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
            mMonster = new Monster();
            mMonster2 = new Monster();
            mMonster3 = new Monster();
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
            background = Content.Load<Texture2D>("Background/level1");
            mMonster.LoadContent(this.Content, "Monsters/monster1");
            mMonster.Position = new Vector2(200, 120);
            mMonster2.LoadContent(this.Content, "Monsters/monster2");
            mMonster2.Position = new Vector2(350, 350);
            mMonster3.LoadContent(this.Content, "Monsters/monster3");
            mMonster3.Position = new Vector2(500, 55);
            font = Content.Load<SpriteFont>("MapFont");
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

            // TODO: Add your update logic here

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
            spriteBatch.Draw(background, new Rectangle(0, 0, 900, 600), Color.White);
            mMonster.Draw(this.spriteBatch);
            mMonster2.Draw(this.spriteBatch);
            mMonster3.Draw(this.spriteBatch);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(70, 510), Color.Yellow);
            spriteBatch.DrawString(font, "Health: " + health, new Vector2(70, 555), Color.Red);
            spriteBatch.DrawString(font, "Level: " + level, new Vector2(800, 5), Color.Red);
            spriteBatch.DrawString(font, "Attack: " + attack, new Vector2(220, 510), Color.Green);
            spriteBatch.DrawString(font, "Defense: " + defense, new Vector2(220, 555), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
