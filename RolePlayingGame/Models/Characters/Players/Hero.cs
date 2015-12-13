﻿using System;
using System.ComponentModel;
using RolePlayingGame.Models.Events;

namespace RolePlayingGame.Models.Characters.Players
{
    using System.Collections.Generic;
    using Interfaces;

    public class Hero : Character, IMovable
    {
        private const int XpBase = 100;
        private int? xpToNextLvl;
        public event LevelUpEventHandler levelUp;
        public Hero(string id, Position position, string name, int healthPoints
            , double defensePoints, double attackPoints, double critChance,
            double critMultiplier, double dodgeChance, int level)
            : base(id, position, healthPoints,name, defensePoints, attackPoints
                  , critChance, critMultiplier, dodgeChance, level)
        {
            this.XpToNextLevel = level*XpBase;
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
                this.XpToNextLevel = this.Level*XpBase;
                LevelUpEventArgs args = new LevelUpEventArgs(this.Level,this.XpToNextLevel);
            }
        }
    }
}
