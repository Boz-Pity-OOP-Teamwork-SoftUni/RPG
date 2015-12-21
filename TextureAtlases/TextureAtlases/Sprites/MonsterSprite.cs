

namespace TextureAtlases
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using RolePlayingGame.Models.Characters;
    using RolePlayingGame.Models.Characters.Monsters;

    public class MonsterSprite
    {
        private int currentFrame;
        private readonly int totalFrames;
        private double elapsed;
        private readonly Texture2D standing;
        private Texture2D currentAnim;
        private KeyboardState ks;
        private Vector2 position;
        private readonly int screenHight;
        private readonly int screenWidth;
        



        public MonsterSprite(ContentManager Content, GraphicsDeviceManager graphics, float x, float y, int rows, int columns, double delay)
            
        {
            this.standing = Content.Load<Texture2D>("Sprites\\greenSquare");
            this.currentAnim = this.standing;
            this.position.X = x;
            this.position.Y = y;
            this.Delay = delay;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
            this.screenHight = graphics.PreferredBackBufferHeight;
            this.screenWidth = graphics.PreferredBackBufferWidth;
            this.DestRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, 50, 50); //TODO give nonmagic values

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
       
        public void Draw(SpriteBatch spriteBatch)
        {
            int width = this.currentAnim.Width / this.Columns;
            int height = this.currentAnim.Height / this.Rows;
            int row = (int)((float)this.currentFrame / (float)this.Columns);
            int column = this.currentFrame % this.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, width, height);

            
           // spriteBatch.Begin();
            spriteBatch.Draw(this.currentAnim, destinationRectangle, sourceRectangle, Color.White);
           // spriteBatch.End();
        }
    }
}