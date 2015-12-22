namespace RolePlayingGame.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    public interface IUpdateable
    {
        void Update(GameTime gameTime);
    }
}
