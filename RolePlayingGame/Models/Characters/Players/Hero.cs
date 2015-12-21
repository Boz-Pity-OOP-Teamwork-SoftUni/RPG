namespace RolePlayingGame.Models.Characters.Players
{
    using System.Collections.Generic;
    using Interfaces;
    using System;
    using RolePlayingGame.Models.Events;

    public class Hero : Character, IMovable
    {
        private const int XpBase = 100;
        private int? xpToNextLvl;
        public event LevelUpEventHandler levelUp;
        private const string defaultName = "Hero";
        private const int defaultHealthPoints = 5000;
        private const int defaultDefensePoints = 20;
        private const int defaultAttackPoints = 40;
        private const double defaultCritChance = 5.0;
        private const double defaultCritMultiplier = 1.5;
        private const double defaultDodgeChance = 20;
        private const int defaultLevel = 1;
        public Hero(string id, Position position)
            : base(id, position, defaultHealthPoints, defaultName, defaultDefensePoints, defaultAttackPoints,
                  defaultCritChance, defaultCritMultiplier, defaultDodgeChance, defaultLevel)
        {
            this.XpToNextLevel = this.Level * XpBase;
        }

        public int? XpToNextLevel
        {
            get { return this.xpToNextLvl; }
            set
            {
                this.xpToNextLvl = value;

                if (this.xpToNextLvl <= 0)
                {
                    this.OnLevelUp();
                }
            }            
        }

        public override Character GetTarget(IEnumerable<Character> targetsList)
        {
            return (Character)targetsList;
        }

        public void Move(Position position)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLevelUp()
        {
            if (this.levelUp != null)
            {
                this.Level++;
                this.XpToNextLevel = this.Level * XpBase + (XpBase * 2);
                UpdateStats();
                LevelUpEventArgs args = new LevelUpEventArgs(this.Level, this.XpToNextLevel);
            }
        }

        private void UpdateStats()
        {
            this.AttackPoints += Math.Pow(this.Level,4);
            this.DefensePoints += Math.Pow(this.Level, 4);
            this.CriticalChance += Math.Pow(this.Level, 4);
            this.DodgeChance += Math.Pow(this.Level, 4);
            this.CriticalMultiplier += Math.Pow(this.Level, 4) / 10;
        }
    }
}
