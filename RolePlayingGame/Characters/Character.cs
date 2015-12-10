using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolePlayingGame.Interfaces;
using RolePlayingGame;

namespace RolePlayingGame.Characters
{
    public abstract class Character : GameObject,IAttack,IDefendable
    {

        private double fullAttack;
        private double fullDefence;
        private double fullCrit;
        private double fullDodge;
        protected Character(string id, int x, int y, int healthPoints
            , double defensePoints, double attackPoints, double critChance, double critMultiplier
            , double dodgeChance) : base(id)
        {
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.DefensePoints = defensePoints;
            this.AttackPoints = attackPoints;
            this.CritChance = critChance;
            this.CritMultiplier = critMultiplier;
            this.DodgeChance = dodgeChance;
            this.IsAlive = true;
            this.Inventory = new Inventory();  
            this.Equipment = new Uquipment();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int HealthPoints { get; set; }
        public double DefensePoints { get; set; }
        public double AttackPoints { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
        public double DodgeChance { get; set; }
        public bool IsAlive { get; set; }
        public Inventory Inventory { get; private set; }
        public Uquipment Equipment { get; set; }       
        public abstract Character GetTarget(IEnumerable<Character> targetsList);

        public void AddToInventory(Item item)
        {
            this.Inventory.AddItem(item);
        }

        public void RemoveFromInventory(Item item)
        {
            this.Inventory.RemoveItem(item);
        }

        protected virtual void ApplyItemEffects(Item item)
        {           
        }

        protected virtual void RemoveItemEffects(Item item)
        {            
        }
        public  void Attack(Character target)
        {
            CalculateStats();
            double chance = new Random().NextDouble();
            bool isCrit = this.fullCrit/100 >= chance;
            if (isCrit)
            {
                this.fullAttack *= this.CritMultiplier;
            }
            target.Defend(this.fullAttack);
        }


        public  void Defend(double damage)
        {
            CalculateStats();
            double dodgeChance = new Random().NextDouble();
            bool isDodge = this.fullDodge/100 >= dodgeChance;
            if (isDodge)
            {
                return;
            }
            else
            {
                double actualDamage = damage - this.DefensePoints;
                if (actualDamage < 0)
                {
                    actualDamage = 0;
                }
                this.HealthPoints -= (int)actualDamage;
                if (this.HealthPoints <= 0)
                {
                    this.IsAlive = false;
                }
            }
        }


        private void CalculateStats()
        {
            this.fullAttack = this.AttackPoints;
            this.fullDefence = this.DefensePoints;
            this.fullCrit = this.CritChance;
            this.fullDodge = this.DodgeChance;
            foreach (var item in this.Equipment.EquipedItems)
            {
                this.fullAttack = item.Value.AttackPoints;
                this.fullDefence = item.Value.DefencePoints;
                this.fullCrit = item.Value.CritChance;
                this.fullDodge = item.Value.DodgeChance;
            }
        }
    }
}
