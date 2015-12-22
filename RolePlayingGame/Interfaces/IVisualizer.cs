using Microsoft.Xna.Framework;

namespace RolePlayingGame.Interfaces
{
    using Microsoft.Xna.Framework.Graphics;
    using Models.Characters;

    public interface IVisualizer:IAnimateable, ISpriteDrawable
    {
        Rectangle DestRectangle
        {
            get;
            set;
        }

         int Rows
        {
            get;
            set;
        }
         int Columns
        {
            get;
            set;
        }
         double Elapsed
        {
            get;
            set;
        }
         double Delay
        {
            get;
            set;
        }

        int CurrentFrame { get; set; }


        Texture2D[] SpriteAnimations { get; set; }
         Position Position { get; set; }

    }
}
