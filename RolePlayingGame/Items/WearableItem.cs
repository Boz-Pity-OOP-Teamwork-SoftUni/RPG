using System;
using RolePlayingGame.Enums;
using RolePlayingGame.Interfaces;
namespace RolePlayingGame.Items
{
    public class WearableItem:Item,IWearableItem
    {
        public WearableItemType itemType;
        private int attackPoints;
        private int defencePoints;
        private double critChance;
        private double critDmg;
        private double dodgeChance;

        public WearableItem(string id, string name, int attackPoints, int defencePoints, double critChance, double critDmg, double dodgeChance) : base(id, name)
        {
            this.attackPoints = attackPoints;
            this.defencePoints = defencePoints;
            this.critChance = critChance;
            this.critDmg = critDmg;
            this.dodgeChance = dodgeChance;
        }


        public WearableItemType ItemType
        {
            get { return this.ItemType; }
          
        }

        public int AttackPoints
        {
            get
            {
              return  this.attackPoints;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Attack points cannot be negative");
                }
                this.attackPoints=value;
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

        public double CritDmg
        {
            get
            {
                return this.critDmg;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Crit damage cannot be negative");
                }
                this.critDmg = value;
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
            return base.ToString()
                   +
                   string.Format(
                       "Type:{0}, Attack:{1}, Defence:{2}, Crit Chance:{3}, Crit Damage:{4}, Dodge Chance: {5} ",
                       this.ItemType, this.AttackPoints, this.DefencePoints, this.CritChance, this.CritDmg,
                       this.DodgeChance);
        }
    }
}
