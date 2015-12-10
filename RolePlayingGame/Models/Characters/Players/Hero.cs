namespace RolePlayingGame.Models.Characters.Players
{
    using System.Collections.Generic;
    using Interfaces;

    public class Hero : Character, IAttackable, IMovable
    {
        public Hero(string id, int x, int y, int healthPoints
            , double defensePoints, double attackPoints, double critChance,
            double critMultiplier, double dodgeChance)
            : base(id, x, y, healthPoints, defensePoints, attackPoints
                  , critChance, critMultiplier, dodgeChance)
        {

        }

        public override Character GetTarget(IEnumerable<Character> targetsList)
        {
            return (Character)targetsList;
        }
    }
}
