namespace RolePlayingGame.Models.Characters.Monsters
{
    using System.Collections.Generic;
    using Interfaces;

    public class Monster : Character, IAttackable
    {
        private int experience;

        public Monster(string id, int x, int y, int healthPoints
            , int defensePoints, int attackPoints, double critChance
            , double critMultiplier, double dodgeChance, int experience) 
            : base(id, x, y, healthPoints, defensePoints, attackPoints
                  , critChance, critMultiplier, dodgeChance)
        {
            this.Experience = experience;
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
