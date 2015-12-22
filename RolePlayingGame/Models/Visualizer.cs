namespace RolePlayingGame.Models
{
    using Characters;
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Visualizer:IVisualizer,ISpriteDrawable,IAnimateable
    {
        private const int DelayDefault = 150;
        public Visualizer( Position position, int totalFrames, Texture2D[] spriteAnimations)
        {
            this.DestRectangle = new Rectangle(position.X,position.Y,50,50);
            this.Rows = 1;
            this.Columns = totalFrames;
            this.Elapsed = 0;
            this.Delay = DelayDefault;
            this.CurrentAnim = spriteAnimations[0];
            this.Position = position;
            this.CurrentFrame = 0;
            this.TotalFrames = totalFrames;
            this.SpriteAnimations = spriteAnimations;
        }

        public Rectangle DestRectangle { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public double Elapsed { get; set; }    
        public double Delay { get; set; }
        public Texture2D[] SpriteAnimations { get; set;}
        public Texture2D CurrentAnim{get; set;}
        public Position Position {get; set;}
        public int CurrentFrame { get; set; }
        public int TotalFrames { get; set; }


        public void Draw(SpriteBatch spriteBatch)
        {
            int width = this.CurrentAnim.Width / this.Columns;
            int height = this.CurrentAnim.Height / this.Rows;
            int row = this.CurrentFrame / this.Columns;
            int column = this.CurrentFrame % this.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            this.DestRectangle = new Rectangle(this.Position.X,this.Position.Y, width, height);

            spriteBatch.Draw(this.CurrentAnim,this.DestRectangle,sourceRectangle,Color.White);
        }

        public void Animate(GameTime gameTime)
        {
            this.Elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.Elapsed >= this.Delay)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == this.TotalFrames)
                {
                    this.CurrentFrame = 0;
                }

                this.Elapsed = 0;
            }
        }
    }
}
