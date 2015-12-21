namespace RolePlayingGame.Models.Characters
{
    using System;
    using System.Collections.Generic;
    using RolePlayingGame.Interfaces;
    using Items;
    using Microsoft.Xna.Framework;
    using RolePlayingGame.Models.Events;

    public abstract class Character : GameObject, IAttackable, IDefendable

    {       
        private string name;
        private int healthPoints;
        private double defensePoints;
        private double attackPoints;
        private double criticalChance;
        private double criticalMultiplier;
        private double dodgeChange;
        private int level;
        private double fullAttack;
        private double fullDefence;
        private double fullCrit;
        private double fullDodge;
        public event CharacterDiedEventHandler characterDied;

        protected Character(string id, Position position, int healthPoints,
            string name,
            double defensePoints, double attackPoints, double criticalChance,
            double criticalMultiplier,
            double dodgeChance,
            int level)
                : base(id)
        {
            this.Position = position;
            this.Name = name;
            this.HealthPoints = healthPoints;
            this.DefensePoints = defensePoints;
            this.AttackPoints = attackPoints;
            this.CriticalChance = criticalChance;
            this.CriticalMultiplier = criticalMultiplier;
            this.DodgeChance = dodgeChance;
            this.Level = level;
            this.IsAlive = true;
            this.Inventory = new Inventory();
            this.Equipment = new Equipment();
            this.CharacterDiedEventArgs = new CharacterDiedEventArgs(name);

            this.DestRectangle = new Rectangle(Position.X, Position.Y, 50, 50); //TODO give nonmagic value

        }

        public Position Position { get; set; }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }                
        }

        public Rectangle DestRectangle
        {
            get;
            private set;
        }
        public CharacterDiedEventArgs CharacterDiedEventArgs { get; set; }

        public int HealthPoints
        {
            get { return this.healthPoints; }
            set
            {
                // Game end
                if (value <= 0)
                {
                    this.IsAlive = false;
                }

                this.healthPoints = value;
            }
        }

        public double DefensePoints
        {
            get { return this.defensePoints; }
            set
            {
                // TODO: Validation
                if (value < 0.0)
                {
                    return;
                }

                this.defensePoints = value;
            }
        }

        public double AttackPoints
        {
            get { return this.attackPoints; }
            set
            {
                // TODO: Validation
                if (value < 0.0)
                {
                    return;
                }

                this.attackPoints = value;
            }
        }

        public double CriticalChance
        {
            get { return this.criticalChance; }
            set
            {
                // TODO: Validation
                if (value < 0.0)
                {
                    return;
                }

                this.criticalChance = value;
            }
        }

        public double CriticalMultiplier
        {
            get { return this.criticalMultiplier; }
            set
            {
                // TODO: Validation
                if (value < 0.0)
                {
                    return;
                }

                this.criticalMultiplier = value;
            }
        }

        public double DodgeChance
        {
            get { return this.dodgeChange; }
            set
            {
                // TODO: Validation
                if (value < 0.0)
                {
                    return;
                }

                this.dodgeChange = value;
            }
        }

        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }

        public bool IsAlive { get; set; }

        public Inventory Inventory { get; private set; }

        public Equipment Equipment { get; set; }

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

        public void Attack(Character target)
        {
            if (this.IsAlive == false)
            {
                return;
            }

            CalculateStats();

            double chance = new Random().NextDouble();
            bool isCrit = this.fullCrit / 100 >= chance;

            if (isCrit)
            {
                this.fullAttack *= this.CriticalMultiplier;
            }

            target.Defend(this.fullAttack);
        }

        public void Defend(double damage)
        {
            CalculateStats();

            double dodgeChance = new Random().NextDouble();
            bool isDodge = this.fullDodge / 100 >= dodgeChance;

            if (isDodge)
            {
                return;
            }
            else
            {
                double actualDamage = damage - this.fullDefence;

                if (actualDamage < 0.0)
                {
                    actualDamage = 0;
                }

                this.HealthPoints -= (int)actualDamage;

                if (this.HealthPoints <= 0)
                {
                    this.OnCharacterDied();
                }
            }
        }

        protected virtual void OnCharacterDied()
        {           
            if (this.characterDied != null)
            {
                this.IsAlive = false;
                this.HealthPoints = 0;
                this.characterDied(this, this.CharacterDiedEventArgs);               
            }
        }

        private void CalculateStats()
        {
            this.fullAttack = this.AttackPoints;
            this.fullDefence = this.DefensePoints;
            this.fullCrit = this.CriticalChance;
            this.fullDodge = this.DodgeChance;

            foreach (var item in this.Equipment.EquipedItems)
            {
                this.fullAttack += item.Value.AttackPoints;
                this.fullDefence += item.Value.DefencePoints;
                this.fullCrit += item.Value.CritChance;
                this.fullDodge += item.Value.DodgeChance;
            }
        }
    }
}
