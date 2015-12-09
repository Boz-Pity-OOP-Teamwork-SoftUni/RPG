using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Interfaces
{
    public interface IConsumeable
    {
        void Consume(Characters.Character target);
    }
}
