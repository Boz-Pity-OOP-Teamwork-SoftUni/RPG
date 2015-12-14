namespace RolePlayingGame.Models.Items
{
    using System;
    using RolePlayingGame.Enums;
    using RolePlayingGame.Interfaces;

    public class WearableItem : Item, IWearableItem
    {
        public WearableItemType itemType;
        private int attackPoints;
        private int defencePoints;
        private double critChance;
        private double dodgeChance;

        public WearableItem(string id, string name, WearableItemType itemType
            , int attackPoints, int defencePoints, double critChance, double dodgeChance) 
            : base(id, name)
        {
            this.ItemType = itemType;
            this.AttackPoints = attackPoints;
            this.DefencePoints = defencePoints;
            this.CritChance = critChance;
            this.DodgeChance = dodgeChance;
            
        }

        public WearableItemType ItemType
        {
            get { return this.itemType; }
            private set { this.itemType = value; }
        }

        public int AttackPoints
        {
            get
            {
                return this.attackPoints;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Attack points cannot be negative");
                }

                this.attackPoints = value;
            }
        }

        public int DefencePoints
        {
            get { return this.defencePoints; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Defence points cannot be negative");
                }

                this.defencePoints = value;
            }
        }

        public double CritChance
        {
            get
            {
                return this.critChance;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Crit chance cannot be negative");
                }

                this.critChance = value;
            }
        }  

        public double DodgeChance
        {
            get
            {
                return this.dodgeChance;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Dodge chance cannot be negative");
                }

                this.dodgeChance = value;
            }
        }

        public override string ToString()
        {
            string result = string.Format("Item type {0},Attack points {1}, Deffence points {2}, Crit chance {3}, " +
                                 "Dodge chance {4}",
                this.ItemType, this.AttackPoints, this.DefencePoints
                , this.CritChance, this.DodgeChance);

            return result;
        }
    }
}
