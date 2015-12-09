﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Characters
{
    using Interfaces;
    public abstract class Monster : Character, IAttack
    {
        public Monster(string id, int x, int y, int healthPoints, int defensePoints, int attackPoints)
            : base(id, x, y, healthPoints, defensePoints, attackPoints)
        {
        }
    }
}
