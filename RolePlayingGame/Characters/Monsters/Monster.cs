﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Characters
{
    using Interfaces;
    public  class Monster : Character, IAttack
    {
        public Monster(string id, int x, int y, int healthPoints
            , int defensePoints, int attackPoints, double critChance
            , double critMultiplier, double dodgeChance, int xp) 
            : base(id, x, y, healthPoints, defensePoints, attackPoints
                  , critChance, critMultiplier, dodgeChance)
        {
            this.Xpirience = xp;
        }

        public int Xpirience { get; set; }
        

        public override Character GetTarget(IEnumerable<Character> targetsList)
        {
            throw new NotImplementedException();
        }

       
    }
}
