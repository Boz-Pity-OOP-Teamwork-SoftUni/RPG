using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame
{
    public abstract class Item : GameObject
    {
        protected Item(string id, int healthEffect, int defenseEffect, int attackEffect)
            : base(id)
        {
            this.HealthEffect = healthEffect;
            this.DefenseEffect = defenseEffect;
            this.AttackEffect = attackEffect;
        }

        public int HealthEffect { get; set; }
        public int DefenseEffect { get; set; }
        public int AttackEffect { get; set; }
    }
}
