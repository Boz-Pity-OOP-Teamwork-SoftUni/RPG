namespace RolePlayingGame.Models.Characters
{
    using System;
    using System.Collections.Generic;
    using RolePlayingGame.Interfaces;
    using Items;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
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
        private double fullAttack;
        private double fullDefence;
        private double fullCrit;
        private double fullDodge;
        public event CharacterDiedEventHandler CharacterDied;
        

        protected Character(string id, Position position, int healthPoints,
            string name,
            double defensePoints, double attackPoints, double criticalChance,
            double criticalMultiplier,
            double dodgeChance,
            int level,
            int totalFrames,
            Texture2D[] texture2Ds
            )
            : base(id)
        {

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
            this.DestRectangle = new Rectangle(position.X, position.Y, 50, 50); //TODO give nonmagic value
            this.Visualizer = new Visualizer(position, totalFrames, texture2Ds);
        }


        public IVisualizer Visualizer
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Rectangle DestRectangle
        {
            get;
            private set;
        }
        public CharacterDiedEventArgs CharacterDiedEventArgs
        {
            get;
            set;
        }

        public int HealthPoints
        {
            get
            {
                return this.healthPoints;
            }
            set
            {
                // Game end
                if (value <= 0)
                {
                    this.IsAlive = false;
                    this.healthPoints = 0;
                }
                else
                {
                    this.healthPoints = value;
                }

            }
        }

        public double DefensePoints
        {
            get
            {
                return this.defensePoints;
            }
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
            get
            {
                return this.attackPoints;
            }
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
            get
            {
                return this.criticalChance;
            }
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
            get
            {
                return this.criticalMultiplier;
            }
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
            get
            {
                return this.dodgeChange;
            }
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
            get;
            set;
        }

        public bool IsAlive
        {
            get;
            set;
        }

        public Inventory Inventory
        {
            get;
            private set;
        }

        public Equipment Equipment
        {
            get;
            set;
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
            if (this.CharacterDied != null)
            {
                this.IsAlive = false;
                this.HealthPoints = 0;
                this.CharacterDied(this, this.CharacterDiedEventArgs);
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
