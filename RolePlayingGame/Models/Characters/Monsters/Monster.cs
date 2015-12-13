using System;
using RolePlayingGame.Models.Events;

namespace RolePlayingGame.Models.Characters.Monsters
{
    using System.Collections.Generic;
    using Interfaces;

    public class Monster : Character
    {
        private int experience;
        private Loot loot;
     
        public Monster(string id, Position position, string name, int healthPoints
            , double defensePoints, double attackPoints, double critChance
            , double critMultiplier, double dodgeChance, int level, int experience            
            ) 
            : base(id, position, healthPoints,name, defensePoints, attackPoints
                  , critChance, critMultiplier, dodgeChance,level)
        {
            this.Experience = experience;
            loot = new Loot(level);
            this.CharacterDiedEventArgs =
                new CharacterDiedEventArgs(string.Format("{0} died",name),name,experience,loot.GetLoot());
        }

        public int Experience
        {
            get { return this.experience; }
            set { this.experience = value; }
        }

       
        
        public override Character GetTarget(IEnumerable<Character> targetsList)
        {
            return (Character)targetsList;
        }       
    }
}
