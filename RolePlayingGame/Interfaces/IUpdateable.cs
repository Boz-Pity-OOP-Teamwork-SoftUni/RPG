using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RolePlayingGame.Interfaces
{
    using Microsoft.Xna.Framework;

    public interface IUpdateable
    {
        void Update(GameTime gameTime);
    }
}
