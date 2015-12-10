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

    public class Animation
    {
        private readonly Texture2D animation;
        private Rectangle sourceRectangle;
        private Vector2 position;

        private float elapsed;
        private float frameTime;
        private int numOfFrames;
        private int currentFrame;
        private int frameWidth;
        private int frameHight;
        private bool looping;

        public Animation(ContentManager content, string asset, float frameTime, int numOfFrames, bool looping)
        {
            this.FrameTime = frameTime;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            this.animation = content.Load<Texture2D>(asset);
            this.frameWidth = (this.animation.Width / numOfFrames);
            this.frameHight = (this.animation.Height);
            this.position = new Vector2(100, 100);  //TODO: make starting position changeable
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public int FrameWidth
        {
            get { return this.frameWidth; }
        }

        public int FrameHight
        {
            get { return this.frameHight; }
        }
        public float FrameTime
        {
            get { return this.frameTime; }
            set { this.frameTime = value; }
        }

        public int I { get; private set; }

        public void PlayAnimation(GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(this.currentFrame * this.frameWidth, 0, this.frameWidth, this.frameHight);

            if (this.elapsed >= this.frameTime)
            {
                if (this.currentFrame >= this.numOfFrames - 1)
                {
                    if (looping)
                    {
                        this.currentFrame = 0;
                    }
                }
                else
                {
                    this.currentFrame++;
                }

                this.elapsed = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.animation, this.position, this.sourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }
    }
}
