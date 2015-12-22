namespace TextureAtlases.Sprites
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using RolePlayingGame.Models.Characters;
    using RolePlayingGame.Models.Characters.Players;

    public class HeroSprite
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

        public HeroSprite(Texture2D leftWalk, Texture2D rightWalk, Texture2D upWalk, Texture2D downWalk, GraphicsDeviceManager graphics, float x, float y, int rows, int columns, double delay, string id)
        {
            this.leftWalk = leftWalk;
            this.rightWalk = rightWalk;
            this.upWalk = upWalk;
            this.downWalk = downWalk;
            this.currentAnim = this.rightWalk;
            this.Delay = delay;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
            this.screenHight = graphics.PreferredBackBufferHeight;
            this.screenWidth = graphics.PreferredBackBufferWidth;

            // this.DestRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, 50, 50); //TODO give nonmagic value
        }

        public Texture2D Texture { get; set; }

        public Rectangle DestRectangle { get; private set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public double Elapsed { get; set; }

        public double Delay { get; set; }

        public void Animate(GameTime gameTime)
        {
            this.elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsed >= this.Delay)
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
            this.ks = Keyboard.GetState();

            if (this.ks.IsKeyDown(Keys.Right) || this.ks.IsKeyDown(Keys.D))
            {
                if (this.position.X + 2f < this.screenWidth - this.rightWalk.Width / 4)
                {
                    this.position.X += 2f;
                }

                this.currentAnim = this.rightWalk;
                Animate(gameTime);
            }

            else if (this.ks.IsKeyDown(Keys.Left) || this.ks.IsKeyDown(Keys.A))
            {
                if (this.position.X - 2f > 0)
                {
                    this.position.X -= 2f;
                }

                this.currentAnim = this.leftWalk;
                Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Up) || this.ks.IsKeyDown(Keys.W))
            {
                if (this.position.Y - 2f > 0)
                {
                    this.position.Y -= 2f;
                }

                this.currentAnim = this.upWalk;
                Animate(gameTime);
            }
            else if (this.ks.IsKeyDown(Keys.Down) || this.ks.IsKeyDown(Keys.S))
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
            int width = this.currentAnim.Width / this.Columns;
            int height = this.currentAnim.Height / this.Rows;
            int row = (int)((float)this.currentFrame / (float)this.Columns);
            int column = this.currentFrame % this.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            this.DestRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);
            
            // spriteBatch.Begin();
            spriteBatch.Draw(this.currentAnim, this.DestRectangle, sourceRectangle, Color.White);
            // spriteBatch.End();
        }
    }
}