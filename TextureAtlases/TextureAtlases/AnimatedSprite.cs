

namespace TextureAtlas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Input;

    public class AnimatedSprite
    {
        private int currentFrame;
        private readonly int totalFrames;
        private double elapsed;
        private readonly Texture2D leftWalk, rightWalk, upWalk, downWalk;
        private Texture2D currentAnim;
        private KeyboardState ks;
        private Vector2 position;
        private readonly int screenHight;
        private readonly int screenWidth;



        public AnimatedSprite(ContentManager Content, GraphicsDeviceManager graphics, float x, float y, int rows, int columns, double delay)
        {
            this.rightWalk = Content.Load<Texture2D>("Sprites\\rightWalk");
            this.leftWalk = Content.Load<Texture2D>("Sprites\\leftWalk");
            this.upWalk = Content.Load<Texture2D>("Sprites\\upWalk");
            this.downWalk = Content.Load<Texture2D>("Sprites\\downWalk");
            this.currentAnim = this.rightWalk;
            this.position.X = x;
            this.position.Y = y;
            this.Delay = delay;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
            this.screenHight = graphics.PreferredBackBufferHeight;
            this.screenWidth = graphics.PreferredBackBufferWidth;


        }

        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public double Elapsed { get; set; }
        public double Delay { get; set; }



        public void Animate(GameTime gameTime)
        {
            this.elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= this.Delay)
            {
                this.currentFrame++;
                if (this.currentFrame == this.totalFrames)
                {
                    this.currentFrame = 0;
                }

                this.elapsed = 0;
            }
        }
        public void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
            {
                if (this.position.X + 2f < this.screenWidth-this.rightWalk.Width/4)
                {
                    this.position.X += 2f;
                }
                this.currentAnim = this.rightWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
            {
                if (this.position.X - 2f > 0)
                {
                    this.position.X -= 2f;
                }
                this.currentAnim = this.leftWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
            {
                if (this.position.Y - 2f > 0)
                {
                    this.position.Y -= 2f;
                }
                this.currentAnim = this.upWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
            {
                if (this.position.Y + 2f < this.screenHight - this.rightWalk.Height)
                {
                    this.position.Y += 2f;
                }
                this.currentAnim = this.downWalk;
                Animate(gameTime);
            }
            else
            {
                this.currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = currentAnim.Width / this.Columns;
            int height = currentAnim.Height / this.Rows;
            int row = (int)((float)this.currentFrame / (float)this.Columns);
            int column = this.currentFrame % this.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(currentAnim, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}