﻿namespace RolePlayingGame.Models.Characters
{
    using System;
    using System.Collections.Generic;
    using RolePlayingGame.Interfaces;
    using Items;
    using Players;

    public abstract class Character : GameObject, IAttackable, IDefendable
    {
        private int x;
        private int y;
        private int healthPoints;
        private double defensePoints;
        private double attackPoints;
        private double criticalChance;
        private double criticalMultiplier;
        private double dodgeChange;

        private double fullAttack;
        private double fullDefence;
        private double fullCrit;
        private double fullDodge;

        protected Character(string id, int x, int y, int healthPoints
            , double defensePoints, double attackPoints, double criticalChance, double criticalMultiplier
            , double dodgeChance) 
            : base(id)
        {
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.DefensePoints = defensePoints;
            this.AttackPoints = attackPoints;
            this.CriticalChance = criticalChance;
            this.CriticalMultiplier = criticalMultiplier;
            this.DodgeChance = dodgeChance;
            this.IsAlive = true;
            this.Inventory = new Inventory();
            this.Equipment = new Equipment();
        }

        public int X
        {
            get { return this.x; }
            set
            {
                // TODO: Validation
                if (value < 0)
                {
                    return;
                }

                this.x = value;
            }
        }

        public int Y
        {
            get { return this.y; }
            set
            {
                // TODO: Validation
                if (value < 0)
                {
                    return;
                }

                this.y = value;
            }
        }

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
            this.fullCrit = this.CriticalChance;
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