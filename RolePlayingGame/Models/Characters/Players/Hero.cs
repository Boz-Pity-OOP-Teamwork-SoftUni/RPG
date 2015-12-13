using System;
using System.ComponentModel;
using RolePlayingGame.Models.Events;

namespace RolePlayingGame.Models.Characters.Players
{
    using System.Collections.Generic;
    using Interfaces;

    public class Hero : Character, IMovable
    {
        public Hero(string id, Position position, string name, int healthPoints
            , double defensePoints, double attackPoints, double critChance,
            double critMultiplier, double dodgeChance, int level)
            : base(id, position, healthPoints,name, defensePoints, attackPoints
                  , critChance, critMultiplier, dodgeChance, level)
        {
        }

        public override Character GetTarget(IEnumerable<Character> targetsList)
        {
            return (Character)targetsList;
        }

        
    }
}
