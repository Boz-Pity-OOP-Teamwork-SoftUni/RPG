using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RolePlayingGame.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Models.Characters;

    public interface IAnimateable
    {
        Texture2D CurrentAnim
        {
            get;
            set;
        }
         Position Position
        {
            get;
            set;
        }

         int CurrentFrame { get; set; }
         int TotalFrames { get; set; }

        void Animate(GameTime gameTime);
    }
}
