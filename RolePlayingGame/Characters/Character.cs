using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Characters
{
    public abstract class Character : GameObject
    {
        protected Character(string id, int x, int y, int healthPoints, int defensePoints, int attackPoints)
            : base(id)
        {
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.DefensePoints = defensePoints;
            this.AttackPoints = attackPoints;
            this.IsAlive = true;           
            this.Inventory = new List<Item>();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int HealthPoints { get; set; }
        public int DefensePoints { get; set; }
        public int AttackPoints { get; set; }
        public bool IsAlive { get; set; }
        public List<Item> Inventory { get; private set; }        
        public abstract Character GetTarget(IEnumerable<Character> targetsList);
        public abstract void AddToInventory(Item item);
        public abstract void RemoveFromInventory(Item item);

        protected virtual void ApplyItemEffects(Item item)
        {           
        }

        protected virtual void RemoveItemEffects(Item item)
        {            
        }
    }
}
