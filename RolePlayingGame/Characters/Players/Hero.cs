using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Characters
{
    using Interfaces;
    public  class Hero : Character, IAttack, IMovable
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
            throw new NotImplementedException();
        }

       

     

      
    }
}
